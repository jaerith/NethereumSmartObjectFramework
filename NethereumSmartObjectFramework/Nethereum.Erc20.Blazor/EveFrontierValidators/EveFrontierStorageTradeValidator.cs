using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nethereum.UI.Validation;
using Microsoft.AspNetCore.Components;

using Nethereum.Erc20.Blazor.EveFrontierModels;

namespace Nethereum.Erc20.Blazor.EveFrontierValidators
{
    public class EveFrontierStorageTradeValidator : AbstractValidator<EveFrontierStorageTradeModel>
    {
        public EveFrontierStorageTradeValidator()
        {
            RuleFor(t => (long) t.ItemInQuantity).GreaterThan(0).WithMessage("Amount must be greater than 0");

            RuleFor(t => t.EveFrontierWorld).SetValidator(new EveFrontierWorldValidator());
        }
    }
}
