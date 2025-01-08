using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;

using Nethereum.ABI.FunctionEncoding.Attributes;
using Nethereum.Mud;
using Nethereum.Mud.Contracts.Core.Tables;
using Nethereum.RPC.Eth.DTOs;
using Nethereum.Web3;

namespace CCP.EveFrontier.SOF.SmartStorageUnit.Tables
{
    public partial class RatioConfigTableService : TableService<RatioConfigTableRecord, RatioConfigTableRecord.RatioConfigKey, RatioConfigTableRecord.RatioConfigValue>
    {
        public RatioConfigTableService(IWeb3 web3, string contractAddress) : base(web3, contractAddress) { }
        public virtual Task<RatioConfigTableRecord> GetTableRecordAsync(BigInteger smartObjectId, BigInteger itemIn, BlockParameter blockParameter = null)
        {
            var _key = new RatioConfigTableRecord.RatioConfigKey();
            _key.SmartObjectId = smartObjectId;
            _key.ItemIn = itemIn;
            return GetTableRecordAsync(_key, blockParameter);
        }
        public virtual Task<string> SetRecordRequestAsync(BigInteger smartObjectId, BigInteger itemIn, BigInteger itemOut, ulong ratioIn, ulong ratioOut)
        {
            var _key = new RatioConfigTableRecord.RatioConfigKey();
            _key.SmartObjectId = smartObjectId;
            _key.ItemIn = itemIn;

            var _values = new RatioConfigTableRecord.RatioConfigValue();
            _values.ItemOut = itemOut;
            _values.RatioIn = ratioIn;
            _values.RatioOut = ratioOut;
            return SetRecordRequestAsync(_key, _values);
        }
        public virtual Task<TransactionReceipt> SetRecordRequestAndWaitForReceiptAsync(BigInteger smartObjectId, BigInteger itemIn, BigInteger itemOut, ulong ratioIn, ulong ratioOut)
        {
            var _key = new RatioConfigTableRecord.RatioConfigKey();
            _key.SmartObjectId = smartObjectId;
            _key.ItemIn = itemIn;

            var _values = new RatioConfigTableRecord.RatioConfigValue();
            _values.ItemOut = itemOut;
            _values.RatioIn = ratioIn;
            _values.RatioOut = ratioOut;
            return SetRecordRequestAndWaitForReceiptAsync(_key, _values);
        }
    }

    public partial class RatioConfigTableRecord : TableRecord<RatioConfigTableRecord.RatioConfigKey, RatioConfigTableRecord.RatioConfigValue>
    {
        public RatioConfigTableRecord() : base("test", "RatioConfig")
        {

        }
        /// <summary>
        /// Direct access to the key property 'SmartObjectId'.
        /// </summary>
        public virtual BigInteger SmartObjectId => Keys.SmartObjectId;
        /// <summary>
        /// Direct access to the key property 'ItemIn'.
        /// </summary>
        public virtual BigInteger ItemIn => Keys.ItemIn;
        /// <summary>
        /// Direct access to the value property 'ItemOut'.
        /// </summary>
        public virtual BigInteger ItemOut => Values.ItemOut;
        /// <summary>
        /// Direct access to the value property 'RatioIn'.
        /// </summary>
        public virtual ulong RatioIn => Values.RatioIn;
        /// <summary>
        /// Direct access to the value property 'RatioOut'.
        /// </summary>
        public virtual ulong RatioOut => Values.RatioOut;

        public partial class RatioConfigKey
        {
            [Parameter("uint256", "smartObjectId", 1)]
            public virtual BigInteger SmartObjectId { get; set; }
            [Parameter("uint256", "itemIn", 2)]
            public virtual BigInteger ItemIn { get; set; }
        }

        public partial class RatioConfigValue
        {
            [Parameter("uint256", "itemOut", 1)]
            public virtual BigInteger ItemOut { get; set; }
            [Parameter("uint64", "ratioIn", 2)]
            public virtual ulong RatioIn { get; set; }
            [Parameter("uint64", "ratioOut", 3)]
            public virtual ulong RatioOut { get; set; }
        }
    }
}

