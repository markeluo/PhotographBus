using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    /// <summary>
    /// 数据访问类
    /// </summary>
    public class HttpHelper
    {
        public delegate void DelHttpDataEval(string _Data,int page,int type);
        public event DelHttpDataEval HttpEndEvent;

        private int thispage = 0;
        private int thistype = 0;
        private EssayInfo _essayinfo = null;

        #region 综合展示，页面组态环境数据访问 webservice 调用
        /// <summary>
        /// 获取webservice 数据
        /// </summary>
        /// <param name="_Url">URL路径</param>
        /// <param name="_page">页面</param>
        /// <returns></returns>
        public void GetServiceDataPost(string _Url,int _page,int _type)
        {
            string strPageInfo = "page/" + _page;
            _Url = Path.Combine(_Url, strPageInfo);
            thispage = _page;
            thistype = _type;
           HttpPost(_Url);
        }
        public void GetDetail(string _Url)
        {
            HttpPost(_Url);
        }

        private string _postDataValue="";
        /// <summary>
        /// POST 请求  
        /// </summary>
        /// <param name="Url"></param>
        /// <returns></returns>
        private void HttpPost(string Url)
        {        
            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(Url);
            httpWebRequest.Method = "POST";
            httpWebRequest.AllowAutoRedirect = true;
            httpWebRequest.ContentType = "application/x-www-form-urlencoded";
            httpWebRequest.BeginGetRequestStream(StreamCallback, httpWebRequest);
        }

        private void StreamCallback(IAsyncResult result)
        {
            HttpWebRequest httpWebRequest = (HttpWebRequest)result.AsyncState;
            using (Stream stream = httpWebRequest.EndGetRequestStream(result))
            {
                byte[] entryBytes = Encoding.UTF8.GetBytes(_postDataValue);
                stream.Write(entryBytes, 0, entryBytes.Length);
            }

            //这里就和上面GET方法获得回复回调一样，用 private void ResponseCallback(IAsyncResult result) 这个方法。
            httpWebRequest.BeginGetResponse(ResponseCallback, httpWebRequest);
        }

        private void ResponseCallback(IAsyncResult result)
        {
            string response = null;
            Stream stream = null;

            try
            {
                //获取异步操作返回的的信息 
                HttpWebRequest httpWebRequest = (HttpWebRequest)result.AsyncState;
                //结束对 Internet 资源的异步请求
                WebResponse webResponse = httpWebRequest.EndGetResponse(result);
                stream = webResponse.GetResponseStream();
                using (StreamReader sr = new StreamReader(stream))
                {
                    response = sr.ReadToEnd();
                }
            }
            catch 
            { 
            }
            finally
            {
                if (stream != null)
                {
                    stream.Close();
                }
            }
            if (HttpEndEvent != null)
            {
                HttpEndEvent(response, thispage,thistype);
            }

        }
        #endregion
    
    }
}
