using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace CCP.EveFrontier.SOF.SmartTurret.Systems.SmartTurretSystem.ContractDefinition
{
    public partial class TargetPriority : TargetPriorityBase { }

    public class TargetPriorityBase 
    {
        [Parameter("tuple", "target", 1)]
        public virtual SmartTurretTarget Target { get; set; }
        [Parameter("uint256", "weight", 2)]
        public virtual BigInteger Weight { get; set; }
    }
}
