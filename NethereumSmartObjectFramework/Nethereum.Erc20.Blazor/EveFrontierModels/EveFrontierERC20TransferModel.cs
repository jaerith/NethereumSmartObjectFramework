using CCP.EveFrontier.SOF.SmartStorageUnitJaerith.ERC20.ERC20System.ContractDefinition;

namespace Nethereum.Erc20.Blazor.EveFrontierModels
{
    public class EveFrontierERC20TransferModel
    {
        public EveFrontierERC20ContractModel ERC20Contract { get; set; } = new EveFrontierERC20ContractModel();

        public EveFrontierERC20TransferModel() { }

        public string To { get; set; }

        public decimal Value { get; set; }

        public TransferFunction GetTransferFunction()
        {
            return new TransferFunction()
            {
                To = To,
                Value = Web3.Web3.Convert.ToWei(Value, ERC20Contract.DecimalPlaces),
                AmountToSend = 0
            };
        }

    }
}
