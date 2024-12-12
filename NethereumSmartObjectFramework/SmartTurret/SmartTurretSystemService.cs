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
using CCP.EveFrontier.SOF.SmartTurretSystem.Systems.ContractDefinition.SmartTurretSystem.ContractDefinition;

namespace CCP.EveFrontier.SOF.SmartTurretSystem.Systems.ContractDefinition.SmartTurretSystem
{
    public partial class SmartTurretSystemService: ContractWeb3ServiceBase
    {
        public static Task<TransactionReceipt> DeployContractAndWaitForReceiptAsync(Nethereum.Web3.IWeb3 web3, SmartTurretSystemDeployment smartTurretSystemDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            return web3.Eth.GetContractDeploymentHandler<SmartTurretSystemDeployment>().SendRequestAndWaitForReceiptAsync(smartTurretSystemDeployment, cancellationTokenSource);
        }

        public static Task<string> DeployContractAsync(Nethereum.Web3.IWeb3 web3, SmartTurretSystemDeployment smartTurretSystemDeployment)
        {
            return web3.Eth.GetContractDeploymentHandler<SmartTurretSystemDeployment>().SendRequestAsync(smartTurretSystemDeployment);
        }

        public static async Task<SmartTurretSystemService> DeployContractAndGetServiceAsync(Nethereum.Web3.IWeb3 web3, SmartTurretSystemDeployment smartTurretSystemDeployment, CancellationTokenSource cancellationTokenSource = null)
        {
            var receipt = await DeployContractAndWaitForReceiptAsync(web3, smartTurretSystemDeployment, cancellationTokenSource);
            return new SmartTurretSystemService(web3, receipt.ContractAddress);
        }

        public SmartTurretSystemService(Nethereum.Web3.IWeb3 web3, string contractAddress) : base(web3, contractAddress)
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

        public Task<string> AggressionRequestAsync(AggressionFunction aggressionFunction)
        {
             return ContractHandler.SendRequestAsync(aggressionFunction);
        }

        public Task<TransactionReceipt> AggressionRequestAndWaitForReceiptAsync(AggressionFunction aggressionFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(aggressionFunction, cancellationToken);
        }

        public Task<string> AggressionRequestAsync(BigInteger smartTurretId, BigInteger characterId, List<TargetPriority> priorityQueue, Turret turret, SmartTurretTarget aggressor, SmartTurretTarget victim)
        {
            var aggressionFunction = new AggressionFunction();
                aggressionFunction.SmartTurretId = smartTurretId;
                aggressionFunction.CharacterId = characterId;
                aggressionFunction.PriorityQueue = priorityQueue;
                aggressionFunction.Turret = turret;
                aggressionFunction.Aggressor = aggressor;
                aggressionFunction.Victim = victim;
            
             return ContractHandler.SendRequestAsync(aggressionFunction);
        }

        public Task<TransactionReceipt> AggressionRequestAndWaitForReceiptAsync(BigInteger smartTurretId, BigInteger characterId, List<TargetPriority> priorityQueue, Turret turret, SmartTurretTarget aggressor, SmartTurretTarget victim, CancellationTokenSource cancellationToken = null)
        {
            var aggressionFunction = new AggressionFunction();
                aggressionFunction.SmartTurretId = smartTurretId;
                aggressionFunction.CharacterId = characterId;
                aggressionFunction.PriorityQueue = priorityQueue;
                aggressionFunction.Turret = turret;
                aggressionFunction.Aggressor = aggressor;
                aggressionFunction.Victim = victim;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(aggressionFunction, cancellationToken);
        }

        public Task<string> InProximityRequestAsync(InProximityFunction inProximityFunction)
        {
             return ContractHandler.SendRequestAsync(inProximityFunction);
        }

        public Task<TransactionReceipt> InProximityRequestAndWaitForReceiptAsync(InProximityFunction inProximityFunction, CancellationTokenSource cancellationToken = null)
        {
             return ContractHandler.SendRequestAndWaitForReceiptAsync(inProximityFunction, cancellationToken);
        }

        public Task<string> InProximityRequestAsync(BigInteger smartTurretId, BigInteger characterId, List<TargetPriority> priorityQueue, Turret turret, SmartTurretTarget turretTarget)
        {
            var inProximityFunction = new InProximityFunction();
                inProximityFunction.SmartTurretId = smartTurretId;
                inProximityFunction.CharacterId = characterId;
                inProximityFunction.PriorityQueue = priorityQueue;
                inProximityFunction.Turret = turret;
                inProximityFunction.TurretTarget = turretTarget;
            
             return ContractHandler.SendRequestAsync(inProximityFunction);
        }

        public Task<TransactionReceipt> InProximityRequestAndWaitForReceiptAsync(BigInteger smartTurretId, BigInteger characterId, List<TargetPriority> priorityQueue, Turret turret, SmartTurretTarget turretTarget, CancellationTokenSource cancellationToken = null)
        {
            var inProximityFunction = new InProximityFunction();
                inProximityFunction.SmartTurretId = smartTurretId;
                inProximityFunction.CharacterId = characterId;
                inProximityFunction.PriorityQueue = priorityQueue;
                inProximityFunction.Turret = turret;
                inProximityFunction.TurretTarget = turretTarget;
            
             return ContractHandler.SendRequestAndWaitForReceiptAsync(inProximityFunction, cancellationToken);
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
                typeof(AggressionFunction),
                typeof(InProximityFunction),
                typeof(SupportsInterfaceFunction)
            };
        }

        public override List<Type> GetAllEventTypes()
        {
            return new List<Type>
            {

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
