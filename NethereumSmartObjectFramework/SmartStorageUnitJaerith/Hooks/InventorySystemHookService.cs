using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.Contracts;
using System.Threading;

using CCP.EveFrontier.SOF.SmartStorageUnitJaerith.Hooks.InventorySystemHook.ContractDefinition;

namespace CCP.EveFrontier.SOF.SmartStorageUnitJaerith.Hooks.InventorySystemHook
{
    public partial class InventorySystemHookService : InventorySystemHookServiceBase
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.IWeb3 web3, InventorySystemHookDeployment inventorySystemHookDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<InventorySystemHookDeployment>().SendRequestAndWaitForReceiptAsync(inventorySystemHookDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.IWeb3 web3, InventorySystemHookDeployment inventorySystemHookDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<InventorySystemHookDeployment>().SendRequestAsync(inventorySystemHookDeployment);
        }

        public static async Task<InventorySystemHookService> DeployContractAndGetServiceAsync(Nethereum.Web3.IWeb3 web3, InventorySystemHookDeployment inventorySystemHookDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, inventorySystemHookDeployment, cancellationTokenSource);
            return new InventorySystemHookService(web3, receipt.ContractAddress);
        }

        public InventorySystemHookService(Nethereum.Web3.IWeb3 web3, string contractAddress) : base(web3, contractAddress)
        {
        }

    }

    public partial class InventorySystemHookServiceBase : ContractWeb3ServiceBase
    {

        public InventorySystemHookServiceBase(Nethereum.Web3.IWeb3 web3, string contractAddress) : base(web3, contractAddress)
        {
        }

        public virtual Task<string> OnAfterCallSystemRequestAsync(OnAfterCallSystemFunction onAfterCallSystemFunction)
        {
            return ContractHandler.SendRequestAsync(onAfterCallSystemFunction);
        }

        public virtual Task<TransactionReceipt> OnAfterCallSystemRequestAndWaitForReceiptAsync(OnAfterCallSystemFunction onAfterCallSystemFunction, CancellationTokenSource cancellationToken = null)
        {
            return ContractHandler.SendRequestAndWaitForReceiptAsync(onAfterCallSystemFunction, cancellationToken);
        }

        public virtual Task<string> OnAfterCallSystemRequestAsync(string msgSender, byte[] systemId, byte[] callData)
        {
            var onAfterCallSystemFunction = new OnAfterCallSystemFunction();
            onAfterCallSystemFunction.MsgSender = msgSender;
            onAfterCallSystemFunction.SystemId = systemId;
            onAfterCallSystemFunction.CallData = callData;

            return ContractHandler.SendRequestAsync(onAfterCallSystemFunction);
        }

        public virtual Task<TransactionReceipt> OnAfterCallSystemRequestAndWaitForReceiptAsync(string msgSender, byte[] systemId, byte[] callData, CancellationTokenSource cancellationToken = null)
        {
            var onAfterCallSystemFunction = new OnAfterCallSystemFunction();
            onAfterCallSystemFunction.MsgSender = msgSender;
            onAfterCallSystemFunction.SystemId = systemId;
            onAfterCallSystemFunction.CallData = callData;

            return ContractHandler.SendRequestAndWaitForReceiptAsync(onAfterCallSystemFunction, cancellationToken);
        }

        public virtual Task<string> OnBeforeCallSystemRequestAsync(OnBeforeCallSystemFunction onBeforeCallSystemFunction)
        {
            return ContractHandler.SendRequestAsync(onBeforeCallSystemFunction);
        }

        public virtual Task<TransactionReceipt> OnBeforeCallSystemRequestAndWaitForReceiptAsync(OnBeforeCallSystemFunction onBeforeCallSystemFunction, CancellationTokenSource cancellationToken = null)
        {
            return ContractHandler.SendRequestAndWaitForReceiptAsync(onBeforeCallSystemFunction, cancellationToken);
        }

        public virtual Task<string> OnBeforeCallSystemRequestAsync(string msgSender, byte[] systemId, byte[] callData)
        {
            var onBeforeCallSystemFunction = new OnBeforeCallSystemFunction();
            onBeforeCallSystemFunction.MsgSender = msgSender;
            onBeforeCallSystemFunction.SystemId = systemId;
            onBeforeCallSystemFunction.CallData = callData;

            return ContractHandler.SendRequestAsync(onBeforeCallSystemFunction);
        }

        public virtual Task<TransactionReceipt> OnBeforeCallSystemRequestAndWaitForReceiptAsync(string msgSender, byte[] systemId, byte[] callData, CancellationTokenSource cancellationToken = null)
        {
            var onBeforeCallSystemFunction = new OnBeforeCallSystemFunction();
            onBeforeCallSystemFunction.MsgSender = msgSender;
            onBeforeCallSystemFunction.SystemId = systemId;
            onBeforeCallSystemFunction.CallData = callData;

            return ContractHandler.SendRequestAndWaitForReceiptAsync(onBeforeCallSystemFunction, cancellationToken);
        }

        public Task<bool> SupportsInterfaceQueryAsync(SupportsInterfaceFunction supportsInterfaceFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<SupportsInterfaceFunction, bool>(supportsInterfaceFunction, blockParameter);
        }


        public virtual Task<bool> SupportsInterfaceQueryAsync(byte[] interfaceId, BlockParameter blockParameter = null)
        {
            var supportsInterfaceFunction = new SupportsInterfaceFunction();
            supportsInterfaceFunction.InterfaceId = interfaceId;

            return ContractHandler.QueryAsync<SupportsInterfaceFunction, bool>(supportsInterfaceFunction, blockParameter);
        }

        public override List<Type> GetAllFunctionTypes()
        {
            return new List<Type>
            {
                typeof(OnAfterCallSystemFunction),
                typeof(OnBeforeCallSystemFunction),
                typeof(SupportsInterfaceFunction)
            };
        }

        public override List<Type> GetAllEventTypes()
        {
            return new List<Type>
            {
                typeof(InventoryAfterEventDTO),
                typeof(InventoryBeforeEventDTO)
            };
        }

        public override List<Type> GetAllErrorTypes()
        {
            return new List<Type>
            {

            };
        }
    }
}

