using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Text;
using YooPoon.Common.WC.Common;
using YooPoon.Core.Data;
using YooPoon.Core.Logging;

namespace YooPoon.Common.WC.WePay
{
    public class WePayService:IWePayService
    {
        private readonly DataSettings _dataSettings;
        private readonly ILog _log;
        private readonly IWCHelper _helper;

        public WePayService(ILog log, IWCHelper helper, DataSettings dataSettings)
        {
            _log = log;
            _helper= helper;
            _dataSettings = dataSettings;
        }
        public string Mchid
        {
            get
            {
                return _dataSettings.RawDataSettings["Mchid"];
            }
        }

        public string Key
        {
            get
            {
                return _dataSettings.RawDataSettings["Key"]; 
            }
        }

        public object UnifiedOrder()
        {
            throw new System.NotImplementedException();
        }

        public string MakeSign(SortedDictionary<string,string> dic)
        {
            var urlFormatString = string.Join("&", dic.Select(d => d.Key + "=" + d.Value));
            //MD5加密
            var md5 = MD5.Create();
            var bs = md5.ComputeHash(Encoding.UTF8.GetBytes(urlFormatString));
            var sb = new StringBuilder();
            foreach (byte b in bs)
            {
                sb.Append(b.ToString("x2"));
            }
            //所有字符转为大写
            return sb.ToString().ToUpper();
        }
    }
}