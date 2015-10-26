using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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
        [AllowAnonymous]
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
                {"total_fee",(order.TotalPrice * 100).ToString("F0")},
                {"notify_url",Request.Url.Host + "/wepay/notifyurl"},
                {"trade_type","JSAPI"},
                {"openid",openId}
            };
            var unifiedReponse = (XmlDocument)_wePayService.UnifiedOrder(payParamDic);
            XmlNode xmlNode = unifiedReponse.FirstChild;//获取到根节点<xml>

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

        [AllowAnonymous]
        public ActionResult GetOpenId(string code, string state)
        {
            if (string.IsNullOrEmpty(code))
                return null;
            string a = state;
            if (string.IsNullOrEmpty(a))
                return null;
            string[] sArray = a.Split('&');
            var openid = _commonService.GetOAuthAccessToken(code).openid;
            var customerParamString = "";
            var redirectUrl = sArray[1] + "?openId=" + openid;
            if (sArray.Length > 2)
            {
                customerParamString = sArray[2].Replace(",", "&");
            }
            if (!string.IsNullOrEmpty(customerParamString))
                redirectUrl += "&" + customerParamString;
            return Redirect(redirectUrl);
        }

        public string NotifyUurl()
        {
            //获取流
            Stream s = System.Web.HttpContext.Current.Request.InputStream;
            //转换成Byte数组
            byte[] b = new byte[s.Length];
            //读取流
            s.Read(b, 0, (int)s.Length);
            //转化成utf8编码
            string postStr = Encoding.UTF8.GetString(b);
            //XML
            XmlDocument xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(postStr);
            var returnCode = xmlDoc.SelectSingleNode("/xml/return_code");
            var resultCode = xmlDoc.SelectSingleNode("/xml/result_code");
            if (returnCode.InnerText == "SUCCESS" && resultCode.InnerText == "SUCCESS")
            {
                var sign = xmlDoc.SelectSingleNode("/xml/sign").InnerText;
                var dic = FromXml(xmlDoc);
                if (!CheckSign(sign, dic))
                {
                    var error = new SortedDictionary<string, string>
                    {
                        {"return_code", "FAIL"},
                        {"return_msg", "签名验证失败"}
                    };
                    return _helper.ConvertToXml(error);
                }
                var orderNo = xmlDoc.SelectSingleNode("/xml/out_trade_no");
                var con = new OrderSearchCondition()
                {
                    OrderNum = orderNo.InnerText
                };
                var order = _orderService.GetOrdersByCondition(con).FirstOrDefault();
                if (order != null)
                {
//                    if (order.Status == EnumOrderStatus.)
//                    {
//                        var error = new SortedDictionary<string, string>
//                        {
//                             {"return_code", "FAIL"},
//                             {"return_msg", "没有获取到PrepayId"}
//                        };
//                        return _wcHelper.ConvertToXml(error);
//                    }
                    if (order.Status == EnumOrderStatus.Payed)
                    {
                        var successMsg = new SortedDictionary<string, string>
                            {
                                {"return_code", "SUCCESS"},
                                {"return_msg", "OK"}
                            };
                        return _helper.ConvertToXml(successMsg);
                    }
                    if (order.Status == EnumOrderStatus.Created)
                    {
                        //更新本地订单状态
                        order.Status = EnumOrderStatus.Payed;
                        order.Updtime = DateTime.Now;
                        _orderService.Update(order);
                        var successMsg = new SortedDictionary<string, string>
                            {
                                {"return_code", "SUCCESS"},
                                {"return_msg", "OK"}
                            };
                        return _helper.ConvertToXml(successMsg);
                    }
                }
                var msg = new SortedDictionary<string, string>
                    {
                        {"return_code", "FAIL"},
                        {"return_msg", "本地不存在订单信息"}
                    };
                return _helper.ConvertToXml(msg);
            }
            var errorMsg = new SortedDictionary<string, string>
            {
                 {"return_code", "FAIL"},
                 {"return_msg", "交易失败"}
            };
            return _helper.ConvertToXml(errorMsg);

        }

        /// <summary>
        /// 获取xml中的节点
        /// </summary>
        /// <param name="xml"></param>
        /// <returns></returns>
        private SortedDictionary<string, string> FromXml(XmlDocument xml)
        {
            SortedDictionary<string, string> dic = new SortedDictionary<string, string>();
            XmlNode xmlNode = xml.FirstChild;//获取到根节点<xml>
            XmlNodeList nodes = xmlNode.ChildNodes;
            foreach (XmlNode xn in nodes)
            {
                XmlElement xe = (XmlElement)xn;
                dic.Add(xe.Name, xe.InnerText);
            }
            return dic;
        }
        /// <summary>
        /// 验证签名
        /// </summary>
        /// <param name="sign"></param>
        /// <param name="dic"></param>
        /// <returns></returns>
        private bool CheckSign(string sign, SortedDictionary<string, string> dic)
        {
            //获取接收到的签名
            var returnSign = sign;

            //在本地计算新的签名
            var calSign = _wePayService.MakeSign(dic);

            return calSign == returnSign;
        }
    }
}