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
using Nethereum.Mud.TableRepository;
using Nethereum.Mud.EncodingDecoding;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;
using Nethereum.Web3.Accounts;

using CCP.EveFrontier.SOF.SmartStorageUnitJaerith.Systems.SmartStorageUnitSystem;
using CCP.EveFrontier.SOF.SmartStorageUnitJaerith.Systems.SmartStorageUnitSystem.ContractDefinition;

namespace CCP.EveFrontier.SOF.SmartStorageUnitJaerith.UnitTests
{
    public class SmartStorageUnitTradeTest
    {
        [Fact]
        public async Task TestTradeWithExistingDeployment()
        {
            try
            {
                var privateKey   = "0xac0974bec39a17e36ba4a6b4d238ff944bacb478cbed5efcae784d7bf4f2ff80";
                var worldAddress = "0x8a791620dd6260079bf849dc5567adc3f2fdc318";

                var account   = new Account(privateKey);
                var localhost = "http://localhost:8545";

                BigInteger lastBlockTransferMilestone = 0;

                BigInteger ssuId    = BigInteger.Parse("17614304337475056394242299294383532840873792487945557467064313427436901763824");
                BigInteger itemInId = BigInteger.Parse("72303041834441799565597028082148290553073890313361053989246429514519533100781");
                ulong      quantity = 5;

                var web3 = new Nethereum.Web3.Web3(account, localhost);

                var storeLogProcessingService = new StoreEventsLogProcessingService(web3, worldAddress);
                var inMemoryStore = new InMemoryTableRepository();

                await storeLogProcessingService.ProcessAllStoreChangesAsync(inMemoryStore, null, null, CancellationToken.None);

                var resultsSystems = inMemoryStore.GetTableRecordsAsync<SystemsTableRecord>(new SystemsTableRecord().ResourceIdEncoded).Result;
                Assert.True(resultsSystems.ToList().Count > 0);

                var smartStorageUnitService = new SmartStorageUnitSystemService(web3, worldAddress);

                var tables = storeLogProcessingService.GetTableRecordsFromLogsAsync<TablesTableRecord>(null, null, CancellationToken.None).Result;

                Assert.True(tables.Count() == 68);

                CalculateOutputFunction calculateOutputFunction =
                    new CalculateOutputFunction() { InputRatio = 5, OutputRatio = 1, InputAmount = 5 };

                var outputDTO = await smartStorageUnitService.CalculateOutputQueryAsync(calculateOutputFunction);

                Assert.True(outputDTO.OutputAmount == 1);

                ExecuteFunction executeFunction =
                    new ExecuteFunction() { Quantity = quantity, InventoryItemIdIn = itemInId, SmartObjectId = ssuId };

                var receipt = await smartStorageUnitService.ExecuteRequestAndWaitForReceiptAsync(executeFunction);
                Assert.NotNull(receipt);

                var foundTradeLogs = new List<EventLog<TradeEventDTO>>();

                var tradeHandler = 
                    new EventLogProcessorHandler<TradeEventDTO>(eventLog => foundTradeLogs.Add(eventLog));

                var processingHandlers = new ProcessorHandler<FilterLog>[] { tradeHandler };

                var contractFilter = new NewFilterInput
                {
                    Address = new[] { smartStorageUnitService.ContractAddress }
                };

                var logsProcessor =
                    web3.Processing.Logs.CreateProcessor(logProcessors: processingHandlers, filter: contractFilter);

                //if we need to stop the processor mid execution - call cancel on the token
                var cancellationToken = new CancellationToken();

                var latestBlockNumber = await web3.Eth.Blocks.GetBlockNumber.SendRequestAsync();

                if (lastBlockTransferMilestone == 0)
                {
                    lastBlockTransferMilestone = latestBlockNumber.Value - 25;
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
            }
            catch (SmartContractCustomErrorRevertException ex)
            {
                // Assert.True(e.IsCustomErrorFor<Nethereum.Mud.Contracts.World.Systems.RegistrationSystem.ContractDefinition.WorldAccessdeniedError>());
                var error = ex.DecodeError<Nethereum.Mud.Contracts.World.Systems.RegistrationSystem.ContractDefinition.WorldAccessdeniedError>();
                Assert.Equal("tb:world:ResourceAccess", error.Resource);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}