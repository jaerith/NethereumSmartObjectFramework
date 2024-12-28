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
using CCP.EveFrontier.SOF.Toggle.Systems.ToggleSystem.ContractDefinition;

namespace CCP.EveFrontier.SOF.Toggle.Systems.ToggleSystem
{
    public partial class ToggleSystemService : ContractWeb3ServiceBase
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.IWeb3 web3, ToggleSystemDeployment toggleSystemDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<ToggleSystemDeployment>().SendRequestAndWaitForReceiptAsync(toggleSystemDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.IWeb3 web3, ToggleSystemDeployment toggleSystemDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<ToggleSystemDeployment>().SendRequestAsync(toggleSystemDeployment);
        }

        public static async Task<ToggleSystemService> DeployContractAndGetServiceAsync(Nethereum.Web3.IWeb3 web3, ToggleSystemDeployment toggleSystemDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, toggleSystemDeployment, cancellationTokenSource);
            return new ToggleSystemService(web3, receipt.ContractAddress);
        }

        public ToggleSystemService(Nethereum.Web3.IWeb3 web3, string contractAddress) : base(web3, contractAddress)
        {
        }

        public Task<string> MsgSenderQueryAsync(MsgSenderFunction msgSenderFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<MsgSenderFunction, string>(msgSenderFunction, blockParameter);
        }


        public Task<string> MsgSenderQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<MsgSenderFunction, string>(null, blockParameter);
        }

        public Task<BigInteger> MsgValueQueryAsync(MsgValueFunction msgValueFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<MsgValueFunction, BigInteger>(msgValueFunction, blockParameter);
        }


        public Task<BigInteger> MsgValueQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<MsgValueFunction, BigInteger>(null, blockParameter);
        }

        public Task<string> WorldQueryAsync(WorldFunction worldFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<WorldFunction, string>(worldFunction, blockParameter);
        }


        public Task<string> WorldQueryAsync(BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<WorldFunction, string>(null, blockParameter);
        }

        public Task<string> SetFalseRequestAsync(SetFalseFunction setFalseFunction)
        {
            return ContractHandler.SendRequestAsync(setFalseFunction);
        }

        public Task<TransactionReceipt> SetFalseRequestAndWaitForReceiptAsync(SetFalseFunction setFalseFunction, CancellationTokenSource cancellationToken = null)
        {
            return ContractHandler.SendRequestAndWaitForReceiptAsync(setFalseFunction, cancellationToken);
        }

        public Task<string> SetFalseRequestAsync(BigInteger smartObjectId)
        {
            var setFalseFunction = new SetFalseFunction();
            setFalseFunction.SmartObjectId = smartObjectId;

            return ContractHandler.SendRequestAsync(setFalseFunction);
        }

        public Task<TransactionReceipt> SetFalseRequestAndWaitForReceiptAsync(BigInteger smartObjectId, CancellationTokenSource cancellationToken = null)
        {
            var setFalseFunction = new SetFalseFunction();
            setFalseFunction.SmartObjectId = smartObjectId;

            return ContractHandler.SendRequestAndWaitForReceiptAsync(setFalseFunction, cancellationToken);
        }

        public Task<string> SetTrueRequestAsync(SetTrueFunction setTrueFunction)
        {
            return ContractHandler.SendRequestAsync(setTrueFunction);
        }

        public Task<TransactionReceipt> SetTrueRequestAndWaitForReceiptAsync(SetTrueFunction setTrueFunction, CancellationTokenSource cancellationToken = null)
        {
            return ContractHandler.SendRequestAndWaitForReceiptAsync(setTrueFunction, cancellationToken);
        }

        public Task<string> SetTrueRequestAsync(BigInteger smartObjectId)
        {
            var setTrueFunction = new SetTrueFunction();
            setTrueFunction.SmartObjectId = smartObjectId;

            return ContractHandler.SendRequestAsync(setTrueFunction);
        }

        public Task<TransactionReceipt> SetTrueRequestAndWaitForReceiptAsync(BigInteger smartObjectId, CancellationTokenSource cancellationToken = null)
        {
            var setTrueFunction = new SetTrueFunction();
            setTrueFunction.SmartObjectId = smartObjectId;

            return ContractHandler.SendRequestAndWaitForReceiptAsync(setTrueFunction, cancellationToken);
        }

        public Task<bool> SupportsInterfaceQueryAsync(SupportsInterfaceFunction supportsInterfaceFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<SupportsInterfaceFunction, bool>(supportsInterfaceFunction, blockParameter);
        }


        public Task<bool> SupportsInterfaceQueryAsync(byte[] interfaceId, BlockParameter blockParameter = null)
        {
            var supportsInterfaceFunction = new SupportsInterfaceFunction();
            supportsInterfaceFunction.InterfaceId = interfaceId;

            return ContractHandler.QueryAsync<SupportsInterfaceFunction, bool>(supportsInterfaceFunction, blockParameter);
        }

        public override List<Type> GetAllFunctionTypes()
        {
            return new List<Type>
            {
                typeof(MsgSenderFunction),
                typeof(MsgValueFunction),
                typeof(WorldFunction),
                typeof(SetFalseFunction),
                typeof(SetTrueFunction),
                typeof(SupportsInterfaceFunction)
            };
        }

        public override List<Type> GetAllEventTypes()
        {
            return new List<Type>
            {
                typeof(StoreSplicestaticdataEventDTO)
            };
        }

        public override List<Type> GetAllErrorTypes()
        {
            return new List<Type>
            {
                typeof(SliceOutofboundsError)
            };
        }
    }
}
