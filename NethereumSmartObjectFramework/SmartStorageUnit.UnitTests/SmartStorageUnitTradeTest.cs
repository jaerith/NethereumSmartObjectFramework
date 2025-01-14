using System.Diagnostics;
using System.Numerics;
using System.Text;
using System.Security.Principal;

using Nethereum.Web3.Accounts;
using Nethereum.Contracts.Create2Deployment;
using Nethereum.Mud.Contracts.World.Systems.RegistrationSystem;
using Nethereum.Mud.Contracts.World;
using Nethereum.Mud.Contracts.Core.StoreEvents;
using Nethereum.Mud.Contracts.World.Tables;
using Nethereum.Mud.TableRepository;
using Nethereum.Mud.EncodingDecoding;
using Nethereum.Web3;

using CCP.EveFrontier.SOF.SmartStorageUnit.Systems.SmartStorageUnitSystem;
using CCP.EveFrontier.SOF.SmartStorageUnit.Systems.SmartStorageUnitSystem.ContractDefinition;
using Nethereum.Mud.Contracts.Store.Tables;

namespace CCP.EveFrontier.SOF.SmartStorageUnit.UnitTests
{
    public class SmartStorageUnitTradeTest
    {
        [Fact]
        public void TestTradeWithExistingDeployment()
        {
            var privateKey   = "0xac0974bec39a17e36ba4a6b4d238ff944bacb478cbed5efcae784d7bf4f2ff80";
            var worldAddress = "0x8a791620dd6260079bf849dc5567adc3f2fdc318";

            var account   = new Account(privateKey);
            var localhost = "http://localhost:8545";

            BigInteger ssuId    = BigInteger.Parse("17614304337475056394242299294383532840873792487945557467064313427436901763824");
            BigInteger itemInId = BigInteger.Parse("72303041834441799565597028082148290553073890313361053989246429514519533100781");
            ulong      quantity = 5;

            var web3 = new Nethereum.Web3.Web3(account, localhost);

            var storeLogProcessingService = new StoreEventsLogProcessingService(web3, worldAddress);
            var inMemoryStore = new InMemoryTableRepository();
            storeLogProcessingService.ProcessAllStoreChangesAsync(inMemoryStore, null, null, CancellationToken.None);

            var smartStorageUnitService = new SmartStorageUnitSystemService(web3, worldAddress);

            var tables = storeLogProcessingService.GetTableRecordsFromLogsAsync<TablesTableRecord>(null, null, CancellationToken.None).Result;

            var inventoryTableSchema = tables.Where(x => x.Keys.GetTableIdResource().Name == "Inventory").FirstOrDefault();

            ExecuteFunction executeFunction =
                new ExecuteFunction() { Quantity = quantity, InventoryItemIdIn = itemInId, SmartObjectId = ssuId };

            var receipt = smartStorageUnitService.ExecuteRequestAndWaitForReceiptAsync(executeFunction).Result;
            Assert.NotNull(receipt);
        }
    }
}