using FluentValidation;
using Nethereum.Erc20.Blazor.EveFrontierModels;
using Nethereum.UI.Validation;

namespace Nethereum.Erc20.Blazor
{
    public class EveFrontierERC20TransferValidator : AbstractValidator<EveFrontierERC20TransferModel>
    {
        public EveFrontierERC20TransferValidator()
        {
            RuleFor(t => t.To).IsEthereumAddress();
            RuleFor(t => t.Value).GreaterThan(0).WithMessage("Amount must be greater than 0");
            RuleFor(t => t.ERC20Contract).SetValidator(new EveFrontierERC20ContractValidator());
        }
    }
}
