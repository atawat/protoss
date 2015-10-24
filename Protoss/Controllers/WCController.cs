using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using Protoss.Common;
using Protoss.Models;
using YooPoon.Common.WC.Common;

namespace Protoss.Controllers
{
    [AllowAnonymous]
    [EnableCors("*", "*", "*", SupportsCredentials = true)]
    public class WCController : ApiController
    {
        private readonly IWCCommonService _wcCommonService;
        private readonly IWCHelper _helper;

        public WCController(IWCCommonService wcCommonService,IWCHelper helper)
        {
            _wcCommonService = wcCommonService;
            _helper = helper;
        }

        public HttpResponseMessage GetJSConfig()
        {
            try
            {
                var model = new JsConfigModel
                {
                    appId = _wcCommonService.AppId,
                    debug = true,
                    jsApiList = new[]{"onMenuShareTimeline",
                        "onMenuShareAppMessage",
                        "onMenuShareQQ",
                        "onMenuShareWeibo",
                        "startRecord",
                        "stopRecord",
                        "onVoiceRecordEnd",
                        "playVoice",
                        "pauseVoice",
                        "stopVoice",
                        "onVoicePlayEnd",
                        "uploadVoice",
                        "downloadVoice",
                        "chooseImage",
                        "previewImage",
                        "uploadImage",
                        "downloadImage",
                        "translateVoice",
                        "getNetworkType",
                        "openLocation",
                        "getLocation",
                        "hideOptionMenu",
                        "showOptionMenu",
                        "hideMenuItems",
                        "showMenuItems",
                        "hideAllNonBaseMenuItem",
                        "showAllNonBaseMenuItem",
                        "closeWindow",
                        "scanQRCode",
                        "chooseWXPay",
                        "openProductSpecificView",
                        "addCard",
                        "chooseCard",
                        "openCard"},
                    nonceStr = _helper.GenerateNonceStr(),
                    timestamp = _helper.GenerateTimeStamp()
                };
                var dic = new SortedDictionary<string, string>
                {
                    {"timestamp", model.timestamp},
                    {"nonceStr", model.nonceStr},
                    {"jsapi_ticket", _wcCommonService.JsAPITicket},
                    {"url", Request.RequestUri.AbsoluteUri}
                };
                model.signature = _wcCommonService.MakeSign(dic);
                return PageHelper.toJson(new ResultModel() {Msg = "获取配置文件成功", Object = model, Status = true});
            }
            catch (Exception e)
            {
                return PageHelper.toJson(new ResultModel() { Msg = "获取配置文件失败", Status = false });
            }
        }

        public HttpResponseMessage GetAppId()
        {
            return PageHelper.toJson(new { appId=_wcCommonService.AppId });
        }
    }
}
