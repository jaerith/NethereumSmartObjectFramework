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
using CCP.EveFrontier.SOF.SmartStorageUnit.Systems.SmartStorageUnitSystem.ContractDefinition;

namespace CCP.EveFrontier.SOF.SmartStorageUnit.Systems.SmartStorageUnitSystem
{
    public partial class SmartStorageUnitSystemService: ContractWeb3ServiceBase
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.IWeb3 web3, SmartStorageUnitSystemDeployment smartStorageUnitSystemDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<SmartStorageUnitSystemDeployment>().SendRequestAndWaitForReceiptAsync(smartStorageUnitSystemDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.IWeb3 web3, SmartStorageUnitSystemDeployment smartStorageUnitSystemDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<SmartStorageUnitSystemDeployment>().SendRequestAsync(smartStorageUnitSystemDeployment);
        }

        public static async Task<SmartStorageUnitSystemService> DeployContractAndGetServiceAsync(Nethereum.Web3.IWeb3 web3, SmartStorageUnitSystemDeployment smartStorageUnitSystemDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, smartStorageUnitSystemDeployment, cancellationTokenSource);
            return new SmartStorageUnitSystemService(web3, receipt.ContractAddress);
        }

        public SmartStorageUnitSystemService(Nethereum.Web3.IWeb3 web3, string contractAddress) : base(web3, contractAddress)
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

        public Task<CalculateOutputOutputDTO> CalculateOutputQueryAsync(CalculateOutputFunction calculateOutputFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryDeserializingToObjectAsync<CalculateOutputFunction, CalculateOutputOutputDTO>(calculateOutputFunction, blockParameter);
        }

        public Task<CalculateOutputOutputDTO> CalculateOutputQueryAsync(ulong inputRatio, ulong outputRatio, ulong inputAmount, BlockParameter blockParameter = null)
        {
            var calculateOutputFunction = new CalculateOutputFunction();
                calculateOutputFunction.InputRatio = inputRatio;
                calculateOutputFunction.OutputRatio = outputRatio;
                calculateOutputFunction.InputAmount = inputAmount;
            
            return ContractHandler.QueryDeserializingToObjectAsync<CalculateOutputFunction, CalculateOutputOutputDTO>(calculateOutputFunction, blockParameter);
        }

        public Task<string> ExecuteRequestAsync(ExecuteFunction executeFunction)
        {
             return ContractHandler.SendRequestAsync(executeFunction);
        }

        public Task<TransactionReceipt> ExecuteRequestAndWaitForReceiptAsync(ExecuteFunction executeFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(executeFunction, cancellationToken);
        }

        public Task<string> ExecuteRequestAsync(BigInteger smartObjectId, ulong quantity, BigInteger inventoryItemIdIn)
        {
            var executeFunction = new ExecuteFunction();
                executeFunction.SmartObjectId = smartObjectId;
                executeFunction.Quantity = quantity;
                executeFunction.InventoryItemIdIn = inventoryItemIdIn;
            
             return ContractHandler.SendRequestAsync(executeFunction);
        }

        public Task<TransactionReceipt> ExecuteRequestAndWaitForReceiptAsync(BigInteger smartObjectId, ulong quantity, BigInteger inventoryItemIdIn, CancellationTokenSource cancellationToken = null)
        {
            var executeFunction = new ExecuteFunction();
                executeFunction.SmartObjectId = smartObjectId;
                executeFunction.Quantity = quantity;
                executeFunction.InventoryItemIdIn = inventoryItemIdIn;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(executeFunction, cancellationToken);
        }

        public Task<string> SetRatioRequestAsync(SetRatioFunction setRatioFunction)
        {
             return ContractHandler.SendRequestAsync(setRatioFunction);
        }

        public Task<TransactionReceipt> SetRatioRequestAndWaitForReceiptAsync(SetRatioFunction setRatioFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setRatioFunction, cancellationToken);
        }

        public Task<string> SetRatioRequestAsync(BigInteger smartObjectId, BigInteger inventoryItemIdIn, BigInteger inventoryItemIdOut, ulong ratioIn, ulong ratioOut)
        {
            var setRatioFunction = new SetRatioFunction();
                setRatioFunction.SmartObjectId = smartObjectId;
                setRatioFunction.InventoryItemIdIn = inventoryItemIdIn;
                setRatioFunction.InventoryItemIdOut = inventoryItemIdOut;
                setRatioFunction.RatioIn = ratioIn;
                setRatioFunction.RatioOut = ratioOut;
            
             return ContractHandler.SendRequestAsync(setRatioFunction);
        }

        public Task<TransactionReceipt> SetRatioRequestAndWaitForReceiptAsync(BigInteger smartObjectId, BigInteger inventoryItemIdIn, BigInteger inventoryItemIdOut, ulong ratioIn, ulong ratioOut, CancellationTokenSource cancellationToken = null)
        {
            var setRatioFunction = new SetRatioFunction();
                setRatioFunction.SmartObjectId = smartObjectId;
                setRatioFunction.InventoryItemIdIn = inventoryItemIdIn;
                setRatioFunction.InventoryItemIdOut = inventoryItemIdOut;
                setRatioFunction.RatioIn = ratioIn;
                setRatioFunction.RatioOut = ratioOut;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setRatioFunction, cancellationToken);
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
                typeof(CalculateOutputFunction),
                typeof(ExecuteFunction),
                typeof(SetRatioFunction),
                typeof(SupportsInterfaceFunction)
            };
        }

        public override List<Type> GetAllEventTypes()
        {
            return new List<Type>
            {
                typeof(StoreSetrecordEventDTO)
            };
        }

        public override List<Type> GetAllErrorTypes()
        {
            return new List<Type>
            {
                typeof(InvalidRatioError),
                typeof(InventoryInvaliditemError),
                typeof(SliceOutofboundsError)
            };
        }
    }
}