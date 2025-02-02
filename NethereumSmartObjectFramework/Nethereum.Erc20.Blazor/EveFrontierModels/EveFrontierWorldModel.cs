using FluentValidation;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace Nethereum.Erc20.Blazor.EveFrontierModels
{
    public class EveFrontierWorldModel
    {
        public const string DEFAULT_WORLD_CONTRACT_ADDRESS = "0x8a791620dd6260079bf849dc5567adc3f2fdc318";

        public const string DEFAULT_WORLD_URL = "http://localhost:8545";

        public EveFrontierWorldModel() { }

        public string Address { get; set; } = DEFAULT_WORLD_CONTRACT_ADDRESS;

        public string Url { get; set; } = DEFAULT_WORLD_URL;
    }
}

