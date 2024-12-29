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

using CCP.EveFrontier.SOF.Toggle.Systems.ToggleSystem.ContractDefinition;
using CCP.EveFrontier.SOF.Toggle.Systems.ToggleSystem;

namespace CCP.EveFrontier.SOF.Toggle.UnitTests
{
    public class ToggleTest
    {
        [Fact]
        public void TestToggleWithAlreadyDeployedAndConfigured()
        {
            try
            {
                var privateKey   = "0xac0974bec39a17e36ba4a6b4d238ff944bacb478cbed5efcae784d7bf4f2ff80";
                var worldAddress = "0x8a791620dd6260079bf849dc5567adc3f2fdc318";

                var toggleContractAddress = "0x2C67E7989e6030476B3E7803E507dC929994C2B0";

                var account = new Account(privateKey);
                var localhost = "http://localhost:8545";

                var web3 = new Nethereum.Web3.Web3(account, localhost);

                var storeLogProcessingService = new StoreEventsLogProcessingService(web3, worldAddress);
                var inMemoryStore = new InMemoryTableRepository();
                storeLogProcessingService.ProcessAllStoreChangesAsync(inMemoryStore, null, null, CancellationToken.None);

                var toggleService = new ToggleSystemService(web3, worldAddress);

                var tables = storeLogProcessingService.GetTableRecordsFromLogsAsync<TablesTableRecord>(null, null, CancellationToken.None).Result;

                var charactersTableSchema = tables.Where(x => x.Keys.GetTableIdResource().Name == "CharactersTable").FirstOrDefault();

                BigInteger smartObjectId =
                    BigInteger.Parse("17614304337475056394242299294383532840873792487945557467064313427436901763821");

                var setFalseReceipt = toggleService.SetFalseRequestAndWaitForReceiptAsync(smartObjectId).Result;
                Assert.NotNull(setFalseReceipt);

                var setTrueReceipt = toggleService.SetTrueRequestAndWaitForReceiptAsync(smartObjectId).Result;
                Assert.NotNull(setTrueReceipt);

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}