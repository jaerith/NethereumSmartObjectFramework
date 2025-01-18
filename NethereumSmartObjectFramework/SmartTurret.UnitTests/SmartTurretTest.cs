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

using CCP.EveFrontier.SOF.SmartTurret.Systems.SmartTurretSystem;
using CCP.EveFrontier.SOF.SmartTurret.Systems.SmartTurretSystem.ContractDefinition;

namespace CCP.EveFrontier.SOF.SmartTurret.UnitTests
{
    public class SmartTurretTest
    {
        public string OwnerPK      = "0xac0974bec39a17e36ba4a6b4d238ff944bacb478cbed5efcae784d7bf4f2ff80";
        public string WorldUrl     = "http://localhost:8545";
        public string WorldAddress = "0x8a791620dd6260079bf849dc5567adc3f2fdc318";

        [Fact]
        public async Task TestInProximityWithAlreadyDeployedAndConfiguredTurret()
        {
            try
            {
                var privateKey   = OwnerPK;
                var worldAddress = WorldAddress;

                var account   = new Account(privateKey);
                var localhost = WorldUrl;

                BigInteger smartTurretId =
                    BigInteger.Parse("9640513001323359261895106592991319872122110296371423549860904054745193604808");

                BigInteger? targetCharacterId = BigInteger.Parse("1112");

                var web3 = new Nethereum.Web3.Web3(account, localhost);

                var storeLogProcessingService = new StoreEventsLogProcessingService(web3, worldAddress);
                var inMemoryStore = new InMemoryTableRepository();
                await storeLogProcessingService.ProcessAllStoreChangesAsync(inMemoryStore, null, null, CancellationToken.None);

                var tables = storeLogProcessingService.GetTableRecordsFromLogsAsync<TablesTableRecord>(null, null, CancellationToken.None).Result;

                var charactersTableSchema = tables.Where(x => x.Keys.GetTableIdResource().Name == "CharactersTable").FirstOrDefault();

                Assert.True(tables.Count() == 67);

                var smartTurretService = new SmartTurretSystemService(web3, worldAddress);

                #region Test for InProximity

                Turret turret = new Turret() { WeaponTypeId = 1, AmmoTypeId = 1, ChargesLeft = 100 };

                SmartTurretTarget turretTarget =
                    new SmartTurretTarget()
                    { ShipId = 1, ShipTypeId = 1, CharacterId = (BigInteger) targetCharacterId, HpRatio = 100, ShieldRatio = 100, ArmorRatio = 100 };

                List<TargetPriority> priorityQueue = new List<TargetPriority>();
                priorityQueue.Add(new TargetPriority() { Target = turretTarget, Weight = 100 });

                InProximityFunction inProximityFunction =
                    new InProximityFunction()
                    { SmartTurretId = smartTurretId, CharacterId = 11111, PriorityQueue = priorityQueue, Turret = turret, TurretTarget = turretTarget };

                var targetProximityPriorityCollection =
                    await smartTurretService
                    .ContractHandler
                    .QueryDeserializingToObjectAsync<InProximityFunction, InProximityOutputDTO>(inProximityFunction);

                Assert.True(targetProximityPriorityCollection.UpdatedPriorityQueue.Count() > 0);

                Assert.True(targetProximityPriorityCollection.UpdatedPriorityQueue[0].Target?.CharacterId == targetCharacterId);

                #endregion

                #region Test for Aggression

                SmartTurretTarget aggressor =
                    new SmartTurretTarget()
                    { ShipId = 1, ShipTypeId = 1, CharacterId = 5555, HpRatio = 100, ShieldRatio = 100, ArmorRatio = 100 };

                SmartTurretTarget victim =
                    new SmartTurretTarget()
                    { ShipId = 1, ShipTypeId = 1, CharacterId = 6666, HpRatio = 80, ShieldRatio = 100, ArmorRatio = 100 };

                AggressionFunction aggressionFunction =
                    new AggressionFunction()
                    {
                        SmartTurretId = smartTurretId,
                        CharacterId = 11111,
                        PriorityQueue = priorityQueue,
                        Turret = turret,
                        Aggressor = aggressor,
                        Victim = victim
                    };

                var targetAggressionPriorityCollection =
                    await smartTurretService
                    .ContractHandler
                    .QueryDeserializingToObjectAsync<AggressionFunction, AggressionOutputDTO>(aggressionFunction);

                Assert.True(targetAggressionPriorityCollection.UpdatedPriorityQueue.Count() > 0);

                Assert.True(targetAggressionPriorityCollection.UpdatedPriorityQueue[0].Target?.CharacterId == targetCharacterId);

                #endregion
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
        }
    }
}