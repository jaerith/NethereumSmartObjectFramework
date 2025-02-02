using System.Numerics;

using Nethereum.UI;

using CCP.EveFrontier.SOF.SmartStorageUnitJaerith.Systems.SmartStorageUnitSystem.ContractDefinition;

namespace Nethereum.Erc20.Blazor.EveFrontierModels
{
    public class EveFrontierStorageTradeModel
    {
        public static readonly BigInteger DEFAULT_STORAGE_SMART_OBJECT_ID =
            BigInteger.Parse("17614304337475056394242299294383532840873792487945557467064313427436901763824");

        public static readonly BigInteger DEFAULT_ITEM_IN_SMART_OBJECT_ID =
            BigInteger.Parse("72303041834441799565597028082148290553073890313361053989246429514519533100781");

        public EveFrontierWorldModel EveFrontierWorld { get; set; } = new EveFrontierWorldModel();

        public BigInteger StorageSmartObjectId { get; set; } = DEFAULT_STORAGE_SMART_OBJECT_ID;

        public BigInteger ItemInSmartObjectId { get; set; } = DEFAULT_ITEM_IN_SMART_OBJECT_ID;

        public ulong ItemInQuantity { get; set; }

        public ExecuteFunction GetExecuteFunction()
        {
            return new ExecuteFunction()
            {
                SmartObjectId = StorageSmartObjectId,
                InventoryItemIdIn = ItemInSmartObjectId,
                Quantity = ItemInQuantity
            };
        }
    }
}