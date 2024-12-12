using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace CCP.EveFrontier.SOF.SmartTurretSystem.Systems.ContractDefinition.SmartTurretSystem.ContractDefinition
{
    public partial class Turret : TurretBase { }

    public class TurretBase 
    {
        [Parameter("uint256", "weaponTypeId", 1)]
        public virtual BigInteger WeaponTypeId { get; set; }
        [Parameter("uint256", "ammoTypeId", 2)]
        public virtual BigInteger AmmoTypeId { get; set; }
        [Parameter("uint256", "chargesLeft", 3)]
        public virtual BigInteger ChargesLeft { get; set; }
    }
}
