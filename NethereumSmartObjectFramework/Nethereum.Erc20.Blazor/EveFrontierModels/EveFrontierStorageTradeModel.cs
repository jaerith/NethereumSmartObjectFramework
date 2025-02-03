using System.Numerics;

using Nethereum.UI;

using CCP.EveFrontier.SOF.SmartStorageUnitJaerith.Systems.SmartStorageUnitSystem.ContractDefinition;

namespace Nethereum.Erc20.Blazor.EveFrontier
{
    public class EveFrontierStorageTradeModel
    {
        public const string DEFAULT_STORAGE_SMART_OBJECT_ID =
            "17614304337475056394242299294383532840873792487945557467064313427436901763824";

        public const string DEFAULT_ITEM_IN_SMART_OBJECT_ID =
            "72303041834441799565597028082148290553073890313361053989246429514519533100781";

        public EveFrontierWorldModel EveFrontierWorld { get; set; } = new EveFrontierWorldModel();

        public string StorageSmartObjectId { get; set; } = DEFAULT_STORAGE_SMART_OBJECT_ID;

        public string ItemInSmartObjectId { get; set; } = DEFAULT_ITEM_IN_SMART_OBJECT_ID;

        public ulong ItemInQuantity { get; set; }

        public ExecuteFunction GetExecuteFunction()
        {
            return new ExecuteFunction()
            {
                SmartObjectId = BigInteger.Parse(StorageSmartObjectId),
                InventoryItemIdIn = BigInteger.Parse(ItemInSmartObjectId),
                Quantity = ItemInQuantity
            };
        }
    }
}