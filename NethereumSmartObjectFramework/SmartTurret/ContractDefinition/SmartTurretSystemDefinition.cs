using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts;
using System.Threading;

namespace CCP.EveFrontier.SOF.SmartTurret.Systems.SmartTurretSystem.ContractDefinition
{
    public partial class SmartTurretSystemDeployment : SmartTurretSystemDeploymentBase
    {
        public SmartTurretSystemDeployment() : base(BYTECODE) { }
        public SmartTurretSystemDeployment(string byteCode) : base(byteCode) { }
    }

    public class SmartTurretSystemDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "0x608060405234801561000f575f80fd5b506106408061001d5f395ff3fe608060405234801561000f575f80fd5b506004361061006f575f3560e01c8063b97822361161004d578063b9782236146100db578063e1af802c14610100578063ea49f0a814610108575f80fd5b806301ffc9a714610073578063119df25f1461009b57806345ec9354146100c8575b5f80fd5b610086610081366004610251565b61011f565b60405190151581526020015b60405180910390f35b6100a36101b7565b60405173ffffffffffffffffffffffffffffffffffffffff9091168152602001610092565b604051601f193601358152602001610092565b6100f36100e9366004610498565b5091949350505050565b6040516100929190610519565b6100a36101c5565b6100f361011636600461059a565b50909392505050565b5f7fffffffff0000000000000000000000000000000000000000000000000000000082167fb5dee1270000000000000000000000000000000000000000000000000000000014806101b157507fffffffff0000000000000000000000000000000000000000000000000000000082167f01ffc9a700000000000000000000000000000000000000000000000000000000145b92915050565b5f6101c06101ce565b905090565b5f6101c0610200565b7fffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffcc36013560601c806101fd5750335b90565b5f6101c05f807f629a4c26e296b22a8e0856e9f6ecb2d1008d7e00081111962cd175fa7488e1755473ffffffffffffffffffffffffffffffffffffffff1690508061024c573391505090565b919050565b5f60208284031215610261575f80fd5b81357fffffffff0000000000000000000000000000000000000000000000000000000081168114610290575f80fd5b9392505050565b7f4e487b71000000000000000000000000000000000000000000000000000000005f52604160045260245ffd5b6040805190810167ffffffffffffffff811182821017156102e7576102e7610297565b60405290565b604051601f8201601f1916810167ffffffffffffffff8111828210171561031657610316610297565b604052919050565b5f60c0828403121561032e575f80fd5b60405160c0810181811067ffffffffffffffff8211171561035157610351610297565b8060405250809150823581526020830135602082015260408301356040820152606083013560608201526080830135608082015260a083013560a08201525092915050565b5f82601f8301126103a5575f80fd5b8135602067ffffffffffffffff8211156103c1576103c1610297565b6103cf818360051b016102ed565b82815260e092830285018201928282019190878511156103ed575f80fd5b8387015b858110156104315781818a031215610407575f80fd5b61040f6102c4565b6104198a8361031e565b815260c08201358682015284529284019281016103f1565b5090979650505050505050565b5f6060828403121561044e575f80fd5b6040516060810181811067ffffffffffffffff8211171561047157610471610297565b80604052508091508235815260208301356020820152604083013560408201525092915050565b5f805f805f8061024087890312156104ae575f80fd5b8635955060208701359450604087013567ffffffffffffffff8111156104d2575f80fd5b6104de89828a01610396565b9450506104ee886060890161043e565b92506104fd8860c0890161031e565b915061050d88610180890161031e565b90509295509295509295565b602080825282518282018190525f919060409081850190868401855b8281101561058d57815180518051865287810151888701528681015187870152606080820151908701526080808201519087015260a0908101519086015286015160c085015260e09093019290850190600101610535565b5091979650505050505050565b5f805f805f61018086880312156105af575f80fd5b8535945060208601359350604086013567ffffffffffffffff8111156105d3575f80fd5b6105df88828901610396565b9350506105ef876060880161043e565b91506105fe8760c0880161031e565b9050929550929590935056fea2646970667358221220c48307005c9bace3c6a0d844748c4ec2b608c3b0370c3f729a2b180720c0b3de64736f6c63430008180033";
        public SmartTurretSystemDeploymentBase() : base(BYTECODE) { }
        public SmartTurretSystemDeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class MsgSenderFunction : MsgSenderFunctionBase { }

    [Function("eveworld___msgSender", "address")]
    public class MsgSenderFunctionBase : FunctionMessage
    {

    }

    public partial class MsgValueFunction : MsgValueFunctionBase { }

    [Function("eveworld___msgValue", "uint256")]
    public class MsgValueFunctionBase : FunctionMessage
    {

    }

    public partial class WorldFunction : WorldFunctionBase { }

    [Function("eveworld___world", "address")]
    public class WorldFunctionBase : FunctionMessage
    {

    }

    public partial class AggressionFunction : AggressionFunctionBase { }

    [Function("eveworld__aggression", typeof(AggressionOutputDTO))]
    public class AggressionFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "smartTurretId", 1)]
        public virtual BigInteger SmartTurretId { get; set; }
        [Parameter("uint256", "characterId", 2)]
        public virtual BigInteger CharacterId { get; set; }
        [Parameter("tuple[]", "priorityQueue", 3)]
        public virtual List<TargetPriority> PriorityQueue { get; set; }
        [Parameter("tuple", "turret", 4)]
        public virtual Turret Turret { get; set; }
        [Parameter("tuple", "aggressor", 5)]
        public virtual SmartTurretTarget Aggressor { get; set; }
        [Parameter("tuple", "victim", 6)]
        public virtual SmartTurretTarget Victim { get; set; }
    }

    public partial class InProximityFunction : InProximityFunctionBase { }

    [Function("eveworld__inProximity", typeof(InProximityOutputDTO))]
    public class InProximityFunctionBase : FunctionMessage
    {
        [Parameter("uint256", "smartTurretId", 1)]
        public virtual BigInteger SmartTurretId { get; set; }
        [Parameter("uint256", "characterId", 2)]
        public virtual BigInteger CharacterId { get; set; }
        [Parameter("tuple[]", "priorityQueue", 3)]
        public virtual List<TargetPriority> PriorityQueue { get; set; }
        [Parameter("tuple", "turret", 4)]
        public virtual Turret Turret { get; set; }
        [Parameter("tuple", "turretTarget", 5)]
        public virtual SmartTurretTarget TurretTarget { get; set; }
    }

    public partial class SupportsInterfaceFunction : SupportsInterfaceFunctionBase { }

    [Function("eveworld__supportsInterface", "bool")]
    public class SupportsInterfaceFunctionBase : FunctionMessage
    {
        [Parameter("bytes4", "interfaceId", 1)]
        public virtual byte[] InterfaceId { get; set; }
    }

    public partial class MsgSenderOutputDTO : MsgSenderOutputDTOBase { }

    [FunctionOutput]
    public class MsgSenderOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("address", "sender", 1)]
        public virtual string Sender { get; set; }
    }

    public partial class MsgValueOutputDTO : MsgValueOutputDTOBase { }

    [FunctionOutput]
    public class MsgValueOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("uint256", "value", 1)]
        public virtual BigInteger Value { get; set; }
    }

    public partial class WorldOutputDTO : WorldOutputDTOBase { }

    [FunctionOutput]
    public class WorldOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("address", "", 1)]
        public virtual string ReturnValue1 { get; set; }
    }

    public partial class AggressionOutputDTO : AggressionOutputDTOBase { }

    [FunctionOutput]
    public class AggressionOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("tuple[]", "updatedPriorityQueue", 1)]
        public virtual List<TargetPriority> UpdatedPriorityQueue { get; set; }
    }

    public partial class InProximityOutputDTO : InProximityOutputDTOBase { }

    [FunctionOutput]
    public class InProximityOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("tuple[]", "updatedPriorityQueue", 1)]
        public virtual List<TargetPriority> UpdatedPriorityQueue { get; set; }
    }

    public partial class SupportsInterfaceOutputDTO : SupportsInterfaceOutputDTOBase { }

    [FunctionOutput]
    public class SupportsInterfaceOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("bool", "", 1)]
        public virtual bool ReturnValue1 { get; set; }
    }
}
