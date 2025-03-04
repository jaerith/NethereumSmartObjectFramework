using Nethereum.Mud.Contracts.Core.Systems;
using Nethereum.Hex.HexTypes;
using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.ABI.Model;
using Nethereum.ABI.FunctionEncoding;
using Nethereum.Contracts.Create2Deployment;
using Nethereum.Mud;
using Nethereum.Contracts;

using CCP.EveFrontier.SOF.SmartStorageUnitJaerith.ERC20.GoblinKingCorpERC20System.ContractDefinition;

namespace CCP.EveFrontier.SOF.SmartStorageUnitJaerith.ERC20.GoblinKingCorpERC20System
{
    public class ERC20SystemServiceResource : SystemResource
    {
        public ERC20SystemServiceResource() : base("ERC20System", "GoblinKingCorp") { }
    }
    public partial class ERC20SystemService: ISystemService<ERC20SystemServiceResource> 
    {
        public IResource Resource => this.GetResource();

        public ISystemServiceResourceRegistration SystemServiceResourceRegistrator
        {
            get
            {
                return this.GetSystemServiceResourceRegistration<ERC20SystemServiceResource, ERC20SystemService>();
            }
        }
        

        public virtual List<FunctionABI> GetSystemFunctionABIs()
        {
            return GetAllFunctionABIs();
        }

        public virtual string CalculateCreate2Address(string deployerAddress, string salt, params ByteCodeLibrary[] byteCodeLibraries)
        {
            return new ERC20SystemDeployment().CalculateCreate2Address(deployerAddress, salt, byteCodeLibraries);
        }

        public virtual Task<Create2ContractDeploymentTransactionResult> DeployCreate2ContractAsync(string deployerAddress, string salt, params ByteCodeLibrary[] byteCodeLibraries)
        {
            var create2ProxyDeployerService = Web3.Eth.Create2DeterministicDeploymentProxyService;
            var deployment = new ERC20SystemDeployment();
            return create2ProxyDeployerService.DeployContractRequestAsync(deployment, deployerAddress, salt, byteCodeLibraries);
        }
        public virtual Task<Create2ContractDeploymentTransactionReceiptResult> DeployCreate2ContractAndWaitForReceiptAsync(string deployerAddress, string salt, ByteCodeLibrary[] byteCodeLibraries, CancellationToken cancellationToken = default)
        {
            var create2ProxyDeployerService = Web3.Eth.Create2DeterministicDeploymentProxyService;
            var deployment = new ERC20SystemDeployment();
            return create2ProxyDeployerService.DeployContractRequestAndWaitForReceiptAsync(deployment, deployerAddress, salt, byteCodeLibraries, cancellationToken);
        }
    }
}
