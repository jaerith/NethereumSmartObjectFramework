using System.Diagnostics;
using System.Numerics;
using System.Text;
using System.Security.Principal;

using Nethereum.BlockchainProcessing.Processor;
using Nethereum.Contracts;
using Nethereum.Contracts.Create2Deployment;
using Nethereum.Mud.Contracts.Core.StoreEvents;
using Nethereum.Mud.Contracts.Store.Tables;
using Nethereum.Mud.Contracts.World.Systems.RegistrationSystem;
using Nethereum.Mud.Contracts.World;
using Nethereum.Mud.Contracts.World.Tables;
using Nethereum.Mud.Contracts.World.Systems.RegistrationSystem.ContractDefinition;
using Nethereum.Mud.TableRepository;
using Nethereum.Mud.EncodingDecoding;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;

using CCP.EveFrontier.SOF.SmartStorageUnitJaerith.Systems.SmartStorageUnitSystem;
using CCP.EveFrontier.SOF.SmartStorageUnitJaerith.Systems.SmartStorageUnitSystem.ContractDefinition;

using CCP.EveFrontier.SOF.SmartStorageUnitJaerith.Tables;

namespace CCP.EveFrontier.SOF.SmartStorageUnitJaerith.UnitTests
{
    public class SmartStorageUnitTradeTest
    {
        public string OwnerPK      = "0xac0974bec39a17e36ba4a6b4d238ff944bacb478cbed5efcae784d7bf4f2ff80";
        public string WorldUrl     = "http://localhost:8545";
        public string WorldAddress = "0x8a791620dd6260079bf849dc5567adc3f2fdc318";

        public string SSU_ID   = "17614304337475056394242299294383532840873792487945557467064313427436901763824";
        public string ItemInID = "72303041834441799565597028082148290553073890313361053989246429514519533100781";

        [Fact]
        public async Task TestTradeWithAlreadyDeployedAndConfiguredUnit()
        {
            try
            {
                var privateKey   = OwnerPK;
                var worldAddress = WorldAddress;

                var account   = new Account(privateKey);
                var localhost = WorldUrl;

                BigInteger lastBlockTransferMilestone = 0;

                BigInteger ssuId    = BigInteger.Parse(SSU_ID);
                BigInteger itemInId = BigInteger.Parse(ItemInID);
                ulong      quantity = 5;

                var web3 = new Nethereum.Web3.Web3(account, localhost);

                var storeLogProcessingService = new StoreEventsLogProcessingService(web3, worldAddress);
                var inMemoryStore = new InMemoryTableRepository();

                await storeLogProcessingService.ProcessAllStoreChangesAsync(inMemoryStore, null, null, CancellationToken.None);

                var resultsSystems = inMemoryStore.GetTableRecordsAsync<SystemsTableRecord>(new SystemsTableRecord().ResourceIdEncoded).Result;
                Assert.True(resultsSystems.ToList().Count > 0);

                var smartStorageUnitService = new SmartStorageUnitSystemService(web3, worldAddress);

                #region Test registering a hook

                // NOTE: This address must be provided in order to properly test the hook
                var hookContractAddress = "";

                if (!String.IsNullOrEmpty(hookContractAddress))
                {
                    var inventorySystemId = ResourceEncoder.EncodeRootSystem("Inventory");

                    byte enabledFlags = 0xFF;

                    RegisterSystemHookFunction registerSystemHookFunction =
                        new RegisterSystemHookFunction()
                        {
                            SystemId = inventorySystemId,
                            HookAddress = hookContractAddress,
                            EnabledHooksBitmap = enabledFlags
                        };

                    var registrationSystemService = new RegistrationSystemService(web3, worldAddress);
                    await registrationSystemService.RegisterSystemHookRequestAndWaitForReceiptAsync(registerSystemHookFunction);
                }

                #endregion

                #region Testing Simple MUD Access to the World

                var tables = storeLogProcessingService.GetTableRecordsFromLogsAsync<TablesTableRecord>(null, null, CancellationToken.None).Result;

                Assert.True(tables.Count() == 68);

                #endregion

                #region Testing Access to MUD Tables

                var ratioTableService = new RatioConfigTableService(web3, worldAddress);

                var tableKey = new RatioConfigTableRecord.RatioConfigKey() { SmartObjectId = ssuId, ItemIn = itemInId };

                var tableRecord = await ratioTableService.GetTableRecordAsync(tableKey);

                Assert.True(tableRecord.RatioOut == 1);

                #endregion

                #region Testing Usage of MUD System 

                CalculateOutputFunction calculateOutputFunction =
                    new CalculateOutputFunction() { InputRatio = 5, OutputRatio = 1, InputAmount = 5 };

                var outputDTO = await smartStorageUnitService.CalculateOutputQueryAsync(calculateOutputFunction);

                Assert.True(outputDTO.OutputAmount == 1);

                #endregion

                #region Testing Usage of MUD System Transactions and Capturing Generated Events

                ExecuteFunction executeFunction =
                    new ExecuteFunction() { Quantity = quantity, InventoryItemIdIn = itemInId, SmartObjectId = ssuId };

                var receipt = await smartStorageUnitService.ExecuteRequestAndWaitForReceiptAsync(executeFunction);
                Assert.NotNull(receipt);

                var foundTradeLogs = new List<EventLog<TradeEventDTO>>();

                var tradeHandler = 
                    new EventLogProcessorHandler<TradeEventDTO>(eventLog => foundTradeLogs.Add(eventLog));

                var processingHandlers = new ProcessorHandler<FilterLog>[] { tradeHandler };

                // This address might need to be changed
                var smartStorageUnitSystemAddress = "0x1EeB2e59ce76a815CceEA7D39FbD1630aD0152Cb";

                var contractFilter = new NewFilterInput
                {
                    Address = new[] { smartStorageUnitSystemAddress }
                };

                var logsProcessor =
                    web3.Processing.Logs.CreateProcessor(logProcessors: processingHandlers, filter: contractFilter);

                //if we need to stop the processor mid execution - call cancel on the token
                var cancellationToken = new CancellationToken();

                var latestBlockNumber = await web3.Eth.Blocks.GetBlockNumber.SendRequestAsync();

                if (lastBlockTransferMilestone == 0)
                {
                    lastBlockTransferMilestone = latestBlockNumber.Value - 10;
                }

                if (lastBlockTransferMilestone > 0)
                {
                    //crawl the required block range
                    await logsProcessor.ExecuteAsync(
                        toBlockNumber: latestBlockNumber.Value,
                        cancellationToken: cancellationToken,
                        startAtBlockNumberIfNotProcessed: lastBlockTransferMilestone);

                    lastBlockTransferMilestone = latestBlockNumber;
                }

                Assert.True(foundTradeLogs.Any());

                var tradeEvent = foundTradeLogs[0].Event;

                Assert.True(tradeEvent.SsuOwner == "0xf39Fd6e51aad88F6F4ce6aB8827279cffFb92266");

                Assert.True(tradeEvent.SsuSmartObjectId == ssuId);

                Assert.True(tradeEvent.CalculatedOutput == 1);

                #endregion
            }
            catch (SmartContractCustomErrorRevertException ex)
            {               
                if (ex.IsCustomErrorFor<Nethereum.Mud.Contracts.World.Systems.RegistrationSystem.ContractDefinition.WorldAccessdeniedError>())
                {
                    var error = ex.DecodeError<Nethereum.Mud.Contracts.World.Systems.RegistrationSystem.ContractDefinition.WorldAccessdeniedError>();
                    Assert.Equal("tb:world:ResourceAccess", error.Resource);
                }
                else
                {
                    Console.WriteLine(ex);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }

    }
}