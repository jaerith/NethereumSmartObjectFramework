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
using CCP.EveFrontier.SOF.SmartGate.Systems.SmartGateSystem.ContractDefinition;

namespace CCP.EveFrontier.SOF.SmartGate.Systems.SmartGateSystem
{
    public partial class SmartGateSystemService: ContractWeb3ServiceBase
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.IWeb3 web3, SmartGateSystemDeployment smartGateSystemDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<SmartGateSystemDeployment>().SendRequestAndWaitForReceiptAsync(smartGateSystemDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.IWeb3 web3, SmartGateSystemDeployment smartGateSystemDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<SmartGateSystemDeployment>().SendRequestAsync(smartGateSystemDeployment);
        }

        public static async Task<SmartGateSystemService> DeployContractAndGetServiceAsync(Nethereum.Web3.IWeb3 web3, SmartGateSystemDeployment smartGateSystemDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, smartGateSystemDeployment, cancellationTokenSource);
            return new SmartGateSystemService(web3, receipt.ContractAddress);
        }

        public SmartGateSystemService(Nethereum.Web3.IWeb3 web3, string contractAddress) : base(web3, contractAddress)
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

        public Task<bool> CanJumpQueryAsync(CanJumpFunction canJumpFunction, BlockParameter blockParameter = null)
        {
            return ContractHandler.QueryAsync<CanJumpFunction, bool>(canJumpFunction, blockParameter);
        }

        
        public Task<bool> CanJumpQueryAsync(BigInteger characterId, BigInteger sourceGateId, BigInteger destinationGateId, BlockParameter blockParameter = null)
        {
            var canJumpFunction = new CanJumpFunction();
                canJumpFunction.CharacterId = characterId;
                canJumpFunction.SourceGateId = sourceGateId;
                canJumpFunction.DestinationGateId = destinationGateId;
            
            return ContractHandler.QueryAsync<CanJumpFunction, bool>(canJumpFunction, blockParameter);
        }

        public Task<string> SetAllowedCorpRequestAsync(SetAllowedCorpFunction setAllowedCorpFunction)
        {
             return ContractHandler.SendRequestAsync(setAllowedCorpFunction);
        }

        public Task<TransactionReceipt> SetAllowedCorpRequestAndWaitForReceiptAsync(SetAllowedCorpFunction setAllowedCorpFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setAllowedCorpFunction, cancellationToken);
        }

        public Task<string> SetAllowedCorpRequestAsync(BigInteger sourceGateId, BigInteger corpID)
        {
            var setAllowedCorpFunction = new SetAllowedCorpFunction();
                setAllowedCorpFunction.SourceGateId = sourceGateId;
                setAllowedCorpFunction.CorpID = corpID;
            
             return ContractHandler.SendRequestAsync(setAllowedCorpFunction);
        }

        public Task<TransactionReceipt> SetAllowedCorpRequestAndWaitForReceiptAsync(BigInteger sourceGateId, BigInteger corpID, CancellationTokenSource cancellationToken = null)
        {
            var setAllowedCorpFunction = new SetAllowedCorpFunction();
                setAllowedCorpFunction.SourceGateId = sourceGateId;
                setAllowedCorpFunction.CorpID = corpID;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(setAllowedCorpFunction, cancellationToken);
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
                typeof(CanJumpFunction),
                typeof(SetAllowedCorpFunction),
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
