using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Protoss.Common;
using YooPoon.Common.WC.Common;

namespace Protoss.Controllers
{
    [AllowAnonymous]
    public class WCController : ApiController
    {
        private readonly IWCCommonService _wcCommonService;

        public WCController(IWCCommonService wcCommonService)
        {
            _wcCommonService = wcCommonService;
        }

        public HttpResponseMessage Get()
        {
            var ticket = _wcCommonService.JsAPITicket;
            return PageHelper.toJson(new ResultModel() {Msg = "获取Ticket成功",Object = ticket, Status = true});
        }
    }
}
