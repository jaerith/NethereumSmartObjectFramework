using FluentValidation;
using Nethereum.UI.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Nethereum.Erc20.Blazor.EveFrontier;

namespace Nethereum.Erc20.Blazor.EveFrontierValidators
{
    public class EveFrontierWorldValidator : AbstractValidator<EveFrontierWorldModel>
    {
        public EveFrontierWorldValidator()
        {
            RuleFor(t => t.Address).IsEthereumAddress();
            RuleFor(t => t.Url).IsUri();
        }
    }
}