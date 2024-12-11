using System.Security.Principal;

using Nethereum.Web3.Accounts;

using CCP.EveFrontier.SOF.SmartStorageUnit.ContractDefinition;

namespace CCP.EveFrontier.SOF.SmartStorageUnit.UnitTests
{
    public class SmartStorageUnitTradeTest
    {
        [Fact]
        public void TestTrade()
        {
            var privateKey   = "0xac0974bec39a17e36ba4a6b4d238ff944bacb478cbed5efcae784d7bf4f2ff80";
            var worldAddress = "blah";

            var account   = new Account(privateKey);
            var localhost = "http://localhost:8546";

            var web3 = new Nethereum.Web3.Web3(account, localhost);

            // var incrementSystemService = new Smart(web3, worldAddress);

        }
    }
}