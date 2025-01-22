using System;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Numerics;

using Nethereum.Mud.Contracts.Core.Systems;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.ABI.Model;
using Nethereum.ABI.FunctionEncoding;
using Nethereum.Contracts.Create2Deployment;
using Nethereum.Mud;
using Nethereum.Web3;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Contracts.CQS;
using Nethereum.Contracts.ContractHandlers;
using Nethereum.Contracts;

using CCP.EveFrontier.SOF.SmartStorageUnitJaerith.Hooks.InventorySystemHook.ContractDefinition;

namespace CCP.EveFrontier.SOF.SmartStorageUnitJaerith.Hooks.InventorySystemHook
{
    public class InventorySystemHookServiceResource : SystemResource
    {
        public InventorySystemHookServiceResource() : base("InventorySystemHook", "test") { }
    }

    public partial class InventorySystemHookService : ISystemService<InventorySystemHookServiceResource>
    {
        public IResource Resource => this.GetResource();

        public ISystemServiceResourceRegistration SystemServiceResourceRegistrator
        {
            get
            {
                return this.GetSystemServiceResourceRegistration<InventorySystemHookServiceResource, InventorySystemHookService>();
            }
        }


        public virtual List<FunctionABI> GetSystemFunctionABIs()
        {
            return GetAllFunctionABIs();
        }

        public virtual string CalculateCreate2Address(string deployerAddress, string salt, params ByteCodeLibrary[] byteCodeLibraries)
        {
            return new InventorySystemHookDeployment().CalculateCreate2Address(deployerAddress, salt, byteCodeLibraries);
        }

        public virtual Task<Create2ContractDeploymentTransactionResult> DeployCreate2ContractAsync(string deployerAddress, string salt, params ByteCodeLibrary[] byteCodeLibraries)
        {
            var create2ProxyDeployerService = Web3.Eth.Create2DeterministicDeploymentProxyService;
            var deployment = new InventorySystemHookDeployment();
            return create2ProxyDeployerService.DeployContractRequestAsync(deployment, deployerAddress, salt, byteCodeLibraries);
        }
        public virtual Task<Create2ContractDeploymentTransactionReceiptResult> DeployCreate2ContractAndWaitForReceiptAsync(string deployerAddress, string salt, ByteCodeLibrary[] byteCodeLibraries, CancellationToken cancellationToken = default)
        {
            var create2ProxyDeployerService = Web3.Eth.Create2DeterministicDeploymentProxyService;
            var deployment = new InventorySystemHookDeployment();
            return create2ProxyDeployerService.DeployContractRequestAndWaitForReceiptAsync(deployment, deployerAddress, salt, byteCodeLibraries, cancellationToken);
        }
    }
}

