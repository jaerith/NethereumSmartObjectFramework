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

namespace CCP.EveFrontier.SOF.SmartStorageUnitJaerith.Hooks.InventorySystemHook.ContractDefinition
{
    public partial class InventorySystemHookDeployment : InventorySystemHookDeploymentBase
    {
        public InventorySystemHookDeployment() : base(BYTECODE) { }
        public InventorySystemHookDeployment(string byteCode) : base(byteCode) { }
    }

    public class InventorySystemHookDeploymentBase : ContractDeploymentMessage
    {
        public static string BYTECODE = "0x608060405234801561000f575f80fd5b506103988061001d5f395ff3fe608060405234801561000f575f80fd5b506004361061003f575f3560e01c806301ffc9a714610043578063973d8f991461006a578063c33230311461007f575b5f80fd5b6100566100513660046101c5565b610092565b604051901515815260200160405180910390f35b61007d610078366004610238565b61012a565b005b61007d61008d366004610238565b61017e565b5f7fffffffff0000000000000000000000000000000000000000000000000000000082167f540fbfa800000000000000000000000000000000000000000000000000000000148061012457507fffffffff0000000000000000000000000000000000000000000000000000000082167f01ffc9a700000000000000000000000000000000000000000000000000000000145b92915050565b818373ffffffffffffffffffffffffffffffffffffffff167f8f131e14eae4a9fab22b789a1e9c0b2a420ee80939d9ca7ae77c969c260231c7836040516101719190610316565b60405180910390a3505050565b818373ffffffffffffffffffffffffffffffffffffffff167f515989f9cfa1f4016ec6cefdec58e84fd0710f22efd5a07326ef5fd61b4bdabe836040516101719190610316565b5f602082840312156101d5575f80fd5b81357fffffffff0000000000000000000000000000000000000000000000000000000081168114610204575f80fd5b9392505050565b7f4e487b71000000000000000000000000000000000000000000000000000000005f52604160045260245ffd5b5f805f6060848603121561024a575f80fd5b833573ffffffffffffffffffffffffffffffffffffffff8116811461026d575f80fd5b925060208401359150604084013567ffffffffffffffff80821115610290575f80fd5b818601915086601f8301126102a3575f80fd5b8135818111156102b5576102b561020b565b604051601f8201601f19908116603f011681019083821181831017156102dd576102dd61020b565b816040528281528960208487010111156102f5575f80fd5b826020860160208301375f6020848301015280955050505050509250925092565b5f602080835283518060208501525f5b8181101561034257858101830151858201604001528201610326565b505f604082860101526040601f19601f830116850101925050509291505056fea2646970667358221220c1889cb98940f7f610266c1aa0fb32ab054c718e3ff3f9aef3b6c93a0c51711d64736f6c63430008180033";
        public InventorySystemHookDeploymentBase() : base(BYTECODE) { }
        public InventorySystemHookDeploymentBase(string byteCode) : base(byteCode) { }

    }

    public partial class OnAfterCallSystemFunction : OnAfterCallSystemFunctionBase { }

    [Function("test__onAfterCallSystem")]
    public class OnAfterCallSystemFunctionBase : FunctionMessage
    {
        [Parameter("address", "msgSender", 1)]
        public virtual string MsgSender { get; set; }
        [Parameter("bytes32", "systemId", 2)]
        public virtual byte[] SystemId { get; set; }
        [Parameter("bytes", "callData", 3)]
        public virtual byte[] CallData { get; set; }
    }

    public partial class OnBeforeCallSystemFunction : OnBeforeCallSystemFunctionBase { }

    [Function("test__onBeforeCallSystem")]
    public class OnBeforeCallSystemFunctionBase : FunctionMessage
    {
        [Parameter("address", "msgSender", 1)]
        public virtual string MsgSender { get; set; }
        [Parameter("bytes32", "systemId", 2)]
        public virtual byte[] SystemId { get; set; }
        [Parameter("bytes", "callData", 3)]
        public virtual byte[] CallData { get; set; }
    }

    public partial class SupportsInterfaceFunction : SupportsInterfaceFunctionBase { }

    [Function("test__supportsInterface", "bool")]
    public class SupportsInterfaceFunctionBase : FunctionMessage
    {
        [Parameter("bytes4", "interfaceId", 1)]
        public virtual byte[] InterfaceId { get; set; }
    }

    public partial class InventoryAfterEventDTO : InventoryAfterEventDTOBase { }

    [Event("InventoryAfterEvent")]
    public class InventoryAfterEventDTOBase : IEventDTO
    {
        [Parameter("address", "_msgSender", 1, true)]
        public virtual string MsgSender { get; set; }
        [Parameter("bytes32", "_systemId", 2, true)]
        public virtual byte[] SystemId { get; set; }
        [Parameter("bytes", "callData", 3, false)]
        public virtual byte[] CallData { get; set; }
    }

    public partial class InventoryBeforeEventDTO : InventoryBeforeEventDTOBase { }

    [Event("InventoryBeforeEvent")]
    public class InventoryBeforeEventDTOBase : IEventDTO
    {
        [Parameter("address", "_msgSender", 1, true)]
        public virtual string MsgSender { get; set; }
        [Parameter("bytes32", "_systemId", 2, true)]
        public virtual byte[] SystemId { get; set; }
        [Parameter("bytes", "callData", 3, false)]
        public virtual byte[] CallData { get; set; }
    }

    public partial class SupportsInterfaceOutputDTO : SupportsInterfaceOutputDTOBase { }

    [FunctionOutput]
    public class SupportsInterfaceOutputDTOBase : IFunctionOutputDTO
    {
        [Parameter("bool", "", 1)]
        public virtual bool ReturnValue1 { get; set; }
    }
}

