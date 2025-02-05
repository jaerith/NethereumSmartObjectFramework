using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

using Nethereum.Web3;

using CCP.EveFrontier.SOF.SmartStorageUnitJaerith.Systems.SmartStorageUnitSystem.ContractDefinition;

namespace ExampleProjectSiwe.Wasm.Services;

public interface ITradeEventStorageService
{
    List<TradeEventDTO> GetAllTradeEvents();

    Task PullLatestTradeEvents(IWeb3 web3, string storageContractAddress);
}
