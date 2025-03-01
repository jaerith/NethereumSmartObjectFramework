using FluentValidation;
using Nethereum.Erc20.Blazor.EveFrontierModels;
using Nethereum.UI.Validation;

namespace Nethereum.Erc20.Blazor
{
    public class EveFrontierERC20ContractValidator : AbstractValidator<EveFrontierERC20ContractModel>
    {
        public EveFrontierERC20ContractValidator()
        {
            RuleFor(t => t.EveFrontierWorld.Address).IsEthereumAddress();
            RuleFor(t => t.DecimalPlaces).GreaterThan(0).WithMessage("Decimal Places must be greater than 0");
        }
    }
}