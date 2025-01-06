using System.Collections.Generic;
using System.Numerics;

using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Mud;
using Nethereum.Mud.Contracts.Core.Tables;
using Nethereum.Web3;

namespace CCP.EveFrontier.SOF.SmartGate.Tables
{
    public partial class GateAccessTableService : TableService<GateAccessTableRecord, GateAccessTableRecord.GateAccessKey, GateAccessTableRecord.GateAccessValue>
    {
        public GateAccessTableService(IWeb3 web3, string contractAddress) : base(web3, contractAddress) { }
    }

    public partial class GateAccessTableRecord : TableRecord<GateAccessTableRecord.GateAccessKey, GateAccessTableRecord.GateAccessValue>
    {
        public GateAccessTableRecord() : base("test", "GateAccess")
        {

        }

        public partial class GateAccessKey
        {
            [Parameter("uint256", "smartObjectId", 1)]
            public virtual BigInteger SmartObjectId { get; set; }
        }

        public partial class GateAccessValue
        {
            [Parameter("uint256", "corp", 1)]
            public virtual BigInteger Corp { get; set; }
        }
    }
}
