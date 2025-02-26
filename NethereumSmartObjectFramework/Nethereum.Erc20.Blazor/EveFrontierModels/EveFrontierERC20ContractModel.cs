using Nethereum.Erc20.Blazor.EveFrontier;

namespace Nethereum.Erc20.Blazor.EveFrontierModels
{
    public class EveFrontierERC20ContractModel
    {
        public EveFrontierWorldModel EveFrontierWorld { get; set; } = new EveFrontierWorldModel();

        public EveFrontierERC20ContractModel() { }

        public string ERC20Namespace { get; set; }
    }
}
