using System.Diagnostics;
using System.Numerics;
using System.Text;
using System.Security.Principal;

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

using CCP.EveFrontier.SOF.SmartTurret.Systems.SmartTurretSystem;
using CCP.EveFrontier.SOF.SmartTurret.Systems.SmartTurretSystem.ContractDefinition;

namespace SmartTurret.UnitTests
{
    public class SmartTurretTest
    {
        [Fact]
        public void TestInProximityWithAlreadyDeployedAndConfiguredTurret()
        {
            try
            {
                var privateKey = "0xac0974bec39a17e36ba4a6b4d238ff944bacb478cbed5efcae784d7bf4f2ff80";
                var worldAddress = "0x8a791620dd6260079bf849dc5567adc3f2fdc318";

                var account = new Account(privateKey);
                var localhost = "http://localhost:8545";

                BigInteger smartTurretId =
                    BigInteger.Parse("9640513001323359261895106592991319872122110296371423549860904054745193604808");

                var web3 = new Nethereum.Web3.Web3(account, localhost);

                var storeLogProcessingService = new StoreEventsLogProcessingService(web3, worldAddress);
                var inMemoryStore = new InMemoryTableRepository();
                storeLogProcessingService.ProcessAllStoreChangesAsync(inMemoryStore, null, null, CancellationToken.None);

                var smartTurretService = new SmartTurretSystemService(web3, worldAddress);

                var tables = storeLogProcessingService.GetTableRecordsFromLogsAsync<TablesTableRecord>(null, null, CancellationToken.None).Result;

                var charactersTableSchema = tables.Where(x => x.Keys.GetTableIdResource().Name == "CharactersTable").FirstOrDefault();

                Turret turret = new Turret() { WeaponTypeId = 1, AmmoTypeId = 1, ChargesLeft = 100 };

                SmartTurretTarget turretTarget =
                    new SmartTurretTarget()
                    { ShipId = 1, ShipTypeId = 1, CharacterId = 11112, HpRatio = 100, ShieldRatio = 100, ArmorRatio = 100 };

                List<TargetPriority> priorityQueue = new List<TargetPriority>();
                priorityQueue.Add(new TargetPriority() { Target = turretTarget, Weight = 100 });

                InProximityFunction inProximityFunction =
                    new InProximityFunction()
                    { SmartTurretId = smartTurretId, CharacterId = 11111, PriorityQueue = priorityQueue, Turret = turret, TurretTarget = turretTarget };

                var receipt = smartTurretService.InProximityRequestAndWaitForReceiptAsync(inProximityFunction).Result;
                Assert.NotNull(receipt);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}