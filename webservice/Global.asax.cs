using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;
using System.Web.Security;
using webservice;

namespace webservice
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            // 在应用程序启动时运行的代码
            AuthConfig.RegisterOpenAuth();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }

        void Application_End(object sender, EventArgs e)
        {
            //  在应用程序关闭时运行的代码

        }

        void Application_Error(object sender, EventArgs e)
        {
            // 在出现未处理的错误时运行的代码

        }
        /// <summary>
        /// 请求开始
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Application_BeginRequest(object sender, EventArgs e) 
        {
            
        }
        /// <summary>
        /// 请求结束
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        void Application_EndRequest(object sender, EventArgs e)
        {
            //  在应用程序关闭时运行的代码
            //   /images/1.jpg
            ImageManager manager = new ImageManager();
            if (manager.IsImagePath(this.Request.RawUrl))
            {
                //如果为请求图片
                manager.OutPutImage(this.Response, this.Request.RawUrl);
            }
        }
    }
}
