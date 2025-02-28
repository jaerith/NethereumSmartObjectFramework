using Nethereum.Erc20.Blazor.EveFrontier;

namespace Nethereum.Erc20.Blazor.EveFrontierModels
{
    public class EveFrontierERC20ContractModel
    {
        public const int DEFAULT_DECIMALS = 18;

        public int DecimalPlaces { get; set; } = DEFAULT_DECIMALS;

        public EveFrontierWorldModel EveFrontierWorld { get; set; } = new EveFrontierWorldModel();

        public EveFrontierERC20ContractModel() { }

        public string ERC20Namespace { get; set; }
    }
}
