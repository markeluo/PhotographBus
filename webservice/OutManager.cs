using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace webservice
{
    /// <summary>
    /// 输出管理类
    /// </summary>
    public class OutManager
    {
        private static OutManager manager = new OutManager();
        /// <summary>
        /// 输出实例--单例
        /// </summary>
        public static OutManager Instance
        {
            get
            {
                return manager;
            }
        }
        /// <summary>
        /// 将消息输出到客户端
        /// </summary>
        /// <param name="StrContent">输出内容</param>
        public void Output(string StrContent)
        {
            System.Web.HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");   //跨域策略
            HttpRequest Request = HttpContext.Current.Request;
            string callback = Request["callback"];
            HttpResponse Response = HttpContext.Current.Response;
            Encoding utf8 = Encoding.GetEncoding("utf-8");
            Response.ContentEncoding = utf8;

            Response.Write(callback + "(" + JsonConvert.SerializeObject(StrContent) + ")");  //此方法是在jquery.min.js版本中使用
            //Response.Write(StrContent);
            //Response.End();   //线程正在终止
            HttpContext.Current.ApplicationInstance.CompleteRequest();

        }

        public void OutPutjson(string strcontent)
        {
            System.Web.HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");   //跨域策略
            HttpRequest Request = HttpContext.Current.Request;
            string callback = Request["callback"];
            HttpResponse Response = HttpContext.Current.Response;
            Encoding utf8 = Encoding.GetEncoding("utf-8");
            Response.ContentEncoding = utf8;
  
            Response.Write(JsonConvert.SerializeObject(strcontent));
            HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

        public void ResultOK()
        {
            string result = "{ \"result\" : \"true\",\"message\":\"执行成功\" }";
            Output(result);
        }

        public void ResultError(string ErrorMsg)
        {
            string result = "{ \"result\" : \"false\", \"message\" : " + ErrorMsg + " }";
            Output(result);
        }
    }
}