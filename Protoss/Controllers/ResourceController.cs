using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.Cors;
using Protoss.Common;
using YooPoon.Core.Site;

namespace Protoss.Controllers
{
    [AllowAnonymous]
    [EnableCors("*", "*", "*", SupportsCredentials = true)]
    public class ResourceController : ApiController
    {
        private string GetUniquelyString() //获取一个不重复的文件名
        {
            Random rnd = new Random(); //获取一个随机数
            const int randomMaxValue = 1000;
            DateTime dt = DateTime.Now;
            int rndNumber = rnd.Next(randomMaxValue);
            var strYear = dt.Year.ToString();
            var strMonth = (dt.Month > 9) ? dt.Month.ToString() : "0" + dt.Month.ToString();
            var strDay = (dt.Day > 9) ? dt.Day.ToString() : "0" + dt.Day.ToString();
            var strHour = (dt.Hour > 9) ? dt.Hour.ToString() : "0" + dt.Hour.ToString();
            var strMinute = (dt.Minute > 9) ? dt.Minute.ToString() : "0" + dt.Minute.ToString();
            var strSecond = (dt.Second > 9) ? dt.Second.ToString() : "0" + dt.Second.ToString();
            var strMillisecond = dt.Millisecond.ToString();
            var strTemp = strYear + strMonth + strDay + "_" + strHour + strMinute + strSecond + "_" + strMillisecond + "_" + rndNumber.ToString();
            return strTemp;
        }
        /// <summary>
        /// 文件上传
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<HttpResponseMessage> Upload()
        {

            var streamProvider = new MultipartMemoryStreamProvider();
            await Request.Content.ReadAsMultipartAsync(streamProvider);
            var fileNames = new List<string>();
            foreach (var item in streamProvider.Contents)
            {
                if (item.Headers.ContentDisposition.FileName != null)
                {
                    var ms = item.ReadAsStreamAsync().Result;
                    var fileLength = ms.Length;
                    var info = new FileInfo(item.Headers.ContentDisposition.FileName.Replace("\"", ""));
                    var allowFomat = new[] { ".png", ".jpg", ".jepg", ".gif" };
                    var isImage = allowFomat.Contains(info.Extension.ToLower());
                    var fileNewName = GetUniquelyString();
                    var localPath = HostingEnvironment.MapPath("~/upload/" + DateTime.Now.ToString("yyyyMMdd")) + "/" + fileNewName + info.Extension;
                    if (isImage)
                    {
                        if (!Directory.Exists(Path.GetDirectoryName(localPath)))
                        {
                            Directory.CreateDirectory(Path.GetDirectoryName(localPath));
                        }
                        byte[] fileBytes = new byte[fileLength];
                        ms.Read(fileBytes, 0, (int)fileLength);
//                        File.Create(localPath + "/" + fileNewName + info.Extension);
                        File.WriteAllBytes(localPath,fileBytes);
                        ms.Close();
                        fileNames.Add(DateTime.Now.ToString("yyyyMMdd") +"/" + fileNewName + info.Extension);                   
                    }

                }
            }
            return PageHelper.toJson(PageHelper.ReturnValue(true, string.Join("|", fileNames)));
        }
        public HttpResponseMessage Search()
        {
            return null;
        }
        public HttpResponseMessage Deltte(int id)
        {
            return null;
        }
    }
}
