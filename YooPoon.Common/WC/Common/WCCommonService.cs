using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Newtonsoft.Json;
using YooPoon.Core.Logging;

namespace YooPoon.Common.WC.Common
{
    public class WCCommonService:IWCCommonService
    {
        private readonly ILog _log;
        private readonly IWCHelper _helper;

        private string _accessToken;
        private int _tokenExpiresIn = 0;
        private DateTime _tokenUpdTime;
        private bool _tokenRefreshLock = false;
        
        private string _jsAPITicket;


        public WCCommonService(ILog log,IWCHelper helper)
        {
            _log = log;
            _helper = helper;
        }

        public string AccessToken
        {
            get
            {
                if (!string.IsNullOrEmpty(_accessToken) && _tokenUpdTime != DateTime.MinValue && _tokenUpdTime.AddSeconds(_tokenExpiresIn) < DateTime.Now)
                    return _accessToken;
                if (!_tokenRefreshLock)
                    RefreshToken();
                return _accessToken;
            }
        }

        public string JsAPITicket { get; private set; }
        public string AppId { get; private set; }
        public string AppSecret { get; private set; }

        /// <summary>
        /// 获取AccessToken
        /// </summary>
        public void RefreshToken()
        {
            _tokenRefreshLock = true;
            var param = new Dictionary<string, string>
            {
                {"grant_type", "client_credential"},
                {"appid", AppId},
                {"secret", AppSecret}
            };
            var reponseStr = _helper.SendGet("https://api.weixin.qq.com/cgi-bin/token", param);
            if (reponseStr == null)
            {
                _log.Error("获取AccessToken出错，请检查错误");
                return;
            }
            var responseObj = new {access_token = "", expires_in = 0};
            var responseJson = JsonConvert.DeserializeAnonymousType(reponseStr, responseObj);
            if (responseJson != null)
            {
                _accessToken = responseJson.access_token;
                _tokenExpiresIn = responseJson.expires_in;
            }
            else
            {
                var responseErrorObj = new { errcode = "", errmsg =""};
                var errorJson = JsonConvert.DeserializeAnonymousType(reponseStr, responseErrorObj);
                _log.Error("获取AccessToken出错，错误代码{0}，错误信息：{1}",errorJson.errcode,errorJson.errmsg);
            }
            _tokenRefreshLock = false;
        }
    }
}