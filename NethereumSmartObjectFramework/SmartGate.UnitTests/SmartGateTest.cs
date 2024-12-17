using System.Numerics;

using Nethereum.Web3.Accounts;
using Nethereum.Contracts.Create2Deployment;
using Nethereum.Mud.Contracts.Core.StoreEvents;
using Nethereum.Mud.Contracts.Store.Tables;
using Nethereum.Mud.Contracts.World;
using Nethereum.Mud.Contracts.World.Tables;
using Nethereum.Mud.Contracts.World.Systems.RegistrationSystem;
using Nethereum.Mud.TableRepository;
using Nethereum.Mud.EncodingDecoding;
using Nethereum.Web3;

using CCP.EveFrontier.SOF.SmartGate.Systems.SmartGateSystem;
using CCP.EveFrontier.SOF.SmartGate.Systems.SmartGateSystem.ContractDefinition;

namespace CCP.EveFrontier.SOF.SmartGate.UnitTests
{
    public class SmartGateTest
    {
        [Fact]
        public void TestCanJumpWithAlreadyDeployedAndConfiguredGate()
        {
            try
            {
                var privateKey   = "0xac0974bec39a17e36ba4a6b4d238ff944bacb478cbed5efcae784d7bf4f2ff80";
                var worldAddress = "0x8a791620dd6260079bf849dc5567adc3f2fdc318";

                var smartGateContractAddress = "0xE87373529b4eC78960DEE5369AE3036603730f2F";

                var account   = new Account(privateKey);
                var localhost = "http://localhost:8545";

                BigInteger sourceSmartGateId =
                    BigInteger.Parse("34818344039668088032259299209624217066809194721387714788472158182502870248994");

                BigInteger destinationSmartGateId =
                    BigInteger.Parse("67387866010353549996346280963079126762450299713900890730943797543376801696007");
                
                BigInteger allowedCharacterId = BigInteger.Parse("100");

                BigInteger refusedCharacterId = BigInteger.Parse("1234");

                var web3 = new Nethereum.Web3.Web3(account, localhost);

                var storeLogProcessingService = new StoreEventsLogProcessingService(web3, worldAddress);
                var inMemoryStore = new InMemoryTableRepository();
                storeLogProcessingService.ProcessAllStoreChangesAsync(inMemoryStore, null, null, CancellationToken.None);

                var smartGateService = new SmartGateSystemService(web3, smartGateContractAddress);

                var tables = storeLogProcessingService.GetTableRecordsFromLogsAsync<TablesTableRecord>(null, null, CancellationToken.None).Result;

                var charactersTableSchema = tables.Where(x => x.Keys.GetTableIdResource().Name == "CharactersTable").FirstOrDefault();

                CanJumpFunction canJumpFunction = 
                    new CanJumpFunction() { SourceGateId = sourceSmartGateId 
                                            , DestinationGateId = destinationSmartGateId
                                            , CharacterId = allowedCharacterId
                                          };

                var canJump = smartGateService.CanJumpQueryAsync(canJumpFunction).Result;
                Assert.True(canJump);

                canJumpFunction.CharacterId = refusedCharacterId;

                canJump = smartGateService.CanJumpQueryAsync(canJumpFunction).Result;
                Assert.False(canJump);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}