using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;

namespace CCP.EveFrontier.SOF.SmartTurretSystem.Systems.ContractDefinition.SmartTurretSystem.ContractDefinition
{
    public partial class SmartTurretTarget : SmartTurretTargetBase { }

    public class SmartTurretTargetBase 
    {
        [Parameter("uint256", "shipId", 1)]
        public virtual BigInteger ShipId { get; set; }
        [Parameter("uint256", "shipTypeId", 2)]
        public virtual BigInteger ShipTypeId { get; set; }
        [Parameter("uint256", "characterId", 3)]
        public virtual BigInteger CharacterId { get; set; }
        [Parameter("uint256", "hpRatio", 4)]
        public virtual BigInteger HpRatio { get; set; }
        [Parameter("uint256", "shieldRatio", 5)]
        public virtual BigInteger ShieldRatio { get; set; }
        [Parameter("uint256", "armorRatio", 6)]
        public virtual BigInteger ArmorRatio { get; set; }
    }
}
