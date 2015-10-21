using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using Protoss.Entity.Model;
using Protoss.Models;
using Protoss.Service.Order;
using YooPoon.Common.WC.Common;
using YooPoon.Common.WC.WePay;
using YooPoon.Core.Site;

namespace Protoss.Controllers
{
    public class WePayController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IWorkContext _workContext;
        private readonly IWePayService _wePayService;
        private readonly IWCCommonService _commonService;
        private readonly IWCHelper _helper;

        public WePayController(IOrderService orderService, IWorkContext workContext, IWePayService wePayService,IWCCommonService commonService,IWCHelper helper)
        {
            _orderService = orderService;
            _workContext = workContext;
            _wePayService = wePayService;
            _commonService = commonService;
            _helper = helper;
        }

        // GET: WePay
        public ActionResult Index(string orderNo,string openId)
        {
            if (string.IsNullOrEmpty(orderNo))
                return RedirectToAction("Error");
            if (_workContext.CurrentUser == null)
                return RedirectToAction("Error", new { msg = "无法获取当前用户，支付失败" });
            var order =
                _orderService.GetOrdersByCondition(new OrderSearchCondition { OrderNum = orderNo }).FirstOrDefault();
            if (order == null)
                return RedirectToAction("Error", new { msg = "无法获取订单信息，支付失败" });
            if (order.Adduser != _workContext.CurrentUser)
                return RedirectToAction("Error", new { msg = "非订单所属客户，支付失败" });
            var payParamDic = new SortedDictionary<string, string>
            {
                {"device_info","WEB"},
                {"body",order.Details.First().Product.Name},
                {"detail",order.Details.First().Product.Name+"等商品"},
                {"attach",""},
                {"out_trade_no",order.OrderNum},
                {"total_fee",order.TotalPrice.ToString("F")},
                {"notify_url",Request.Url.Host},
                {"trade_type","JSAPI"},
                {"openid",openId}
            };
            var unifiedReponse = (XmlDocument)_wePayService.UnifiedOrder(payParamDic);
            XmlNode xmlNode = unifiedReponse.FirstChild;//获取到根节点<xml>
            XmlNodeList nodes = xmlNode.ChildNodes;
            if(xmlNode.Attributes["return_code"].Value != "SUCCESS" || xmlNode.Attributes["result_code"].Value != "SUCCESS")
                return RedirectToAction("Error", new { msg = xmlNode.Attributes["return_msg"].Value });
            var payModel = new PayModel
            {
                AppId = _commonService.AppId,
                NonceStr = _helper.GenerateNonceStr(),
                Package = "prepay_id=" + xmlNode.Attributes["prepay_id"].Value,
                SignType = "MD5",
                TimeStamp = _helper.GenerateTimeStamp()
            };
            var signDic = new SortedDictionary<string, string>
            {
                {"appId", payModel.AppId},
                {"timeStamp", payModel.TimeStamp},
                {"nonceStr", payModel.NonceStr},
                {"package", payModel.Package},
                {"signType", payModel.SignType},
            };
            payModel.PaySign = _wePayService.MakeSign(signDic);
            return View(payModel);
        }

        public ActionResult Error(string msg)
        {
            var errorMsg = msg;
            if (string.IsNullOrEmpty(msg))
                errorMsg = "出错啦，无法完成支付";
            return View(errorMsg);
        }
    }
}