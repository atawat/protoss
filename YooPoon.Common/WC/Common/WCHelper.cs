﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using YooPoon.Core.Logging;

namespace YooPoon.Common.WC.Common
{


    public class WCHelper : IWCHelper
    {
        private readonly ILog _log;

        public WCHelper(ILog log)
        {
            _log = log;
        }
        public string SendGet(string url, Dictionary<string, string> paramsDic)
        {

            try
            {
                var responseString = "";
                var formatUrl = url; //扩展参数后的Url地址
                var paramArray = paramsDic.Select(d => d.Key + "=" + d.Value).ToArray();
                if (paramArray.Any())
                {
                    var paramString = string.Join("&", paramArray);
                    formatUrl += "?" + paramString;
                }

                //创建请求
                var request = (HttpWebRequest) WebRequest.Create(formatUrl);
                request.Method = WebRequestMethods.Http.Get;
                request.ContentType = "text/plain";

                var response = (HttpWebResponse) request.GetResponse();
                var responseStream = response.GetResponseStream();


                using (responseStream)
                {
                    if (responseStream != null)
                        using (var reader = new StreamReader(responseStream, Encoding.UTF8))
                        {
                            responseString = reader.ReadToEnd();
                        }
                    else
                    {
                        return null;
                    }
                }
                return responseString;
            }
            catch (Exception e)
            {
                _log.Error(e,"微信服务发送HttpGet出错");
                return null;
            }
        }

        public string SendPost(string url, string postData)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = WebRequestMethods.Http.Post;
                request.ContentType = "application/x-www-form-urlencoded";
                request.ContentLength = Encoding.UTF8.GetByteCount(postData);

                Stream myRequestStream = request.GetRequestStream();
                using (var myStreamWriter = new StreamWriter(myRequestStream, Encoding.UTF8))
                {
                    myStreamWriter.Write(postData);
                }


                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                var retString = "";
                using (var myResponseStream = response.GetResponseStream())
                {
                    if (myResponseStream != null)
                        using (var myStreamReader = new StreamReader(myResponseStream, Encoding.UTF8))
                        {
                            retString = myStreamReader.ReadToEnd();
                        }
                    else
                    {
                        return null;
                    }
                }

                return retString;
            }
            catch (Exception e)
            {
                _log.Error(e, "微信服务发送HttpPost出错");
                return null;
            }

        }
    }
}