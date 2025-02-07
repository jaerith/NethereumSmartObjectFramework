using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using Nethereum.Web3;
using Nethereum.BlockchainProcessing.Processor;
using Nethereum.Contracts;
using Nethereum.RPC.Eth.DTOs;

using CCP.EveFrontier.SOF.SmartStorageUnitJaerith.Systems.SmartStorageUnitSystem.ContractDefinition;

namespace ExampleProjectSiwe.Wasm.Services
{
    public class TradeEventStorageService : ITradeEventStorageService
    {
        private object _locker = new object();

        private List<EventLog<TradeEventDTO>> sessionTradeLogs;

        public TradeEventStorageService()
        {
            sessionTradeLogs = new List<EventLog<TradeEventDTO>>();
        }

        public List<TradeEventDTO> GetAllTradeEvents()
        {
            List<TradeEventDTO> allTradeEvents = new List<TradeEventDTO>();

            sessionTradeLogs.ForEach(t => allTradeEvents.Add(t.Event));

            return allTradeEvents;
        }

        public async Task PullLatestTradeEvents(IWeb3 web3, string storageContractAddress)
        {
            var foundTradeLogs = new List<EventLog<TradeEventDTO>>();

            var tradeHandler =
                new EventLogProcessorHandler<TradeEventDTO>(eventLog => foundTradeLogs.Add(eventLog));

            var processingHandlers = new ProcessorHandler<FilterLog>[] { tradeHandler };

            // This address might need to be changed
            var smartStorageUnitSystemAddress = storageContractAddress;

            var contractFilter = new NewFilterInput
            {
                Address = new[] { smartStorageUnitSystemAddress }
            };

            var logsProcessor =
                web3.Processing.Logs.CreateProcessor(logProcessors: processingHandlers, filter: contractFilter);

            //if we need to stop the processor mid execution - call cancel on the token
            var cancellationToken = new CancellationToken();

            var latestBlockNumber = await web3.Eth.Blocks.GetBlockNumber.SendRequestAsync();

            var lastBlockTransferMilestone = latestBlockNumber.Value - 10;

            if (lastBlockTransferMilestone > 0)
            {
                //crawl the required block range
                await logsProcessor.ExecuteAsync(
                    toBlockNumber: latestBlockNumber.Value,
                    cancellationToken: cancellationToken,
                    startAtBlockNumberIfNotProcessed: lastBlockTransferMilestone);
            }

            lock (_locker)
            {
                if (foundTradeLogs.Count > 0)
                {
                    sessionTradeLogs.AddRange(foundTradeLogs);
                }
            }
        }
    }
}
