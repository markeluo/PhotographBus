using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Script.Services;
using System.Web.Services;

namespace webservice
{
    /// <summary>
    /// service 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    [System.Web.Script.Services.ScriptService]

    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class service : System.Web.Services.WebService
    {
        JavaScriptSerializer serializer = new JavaScriptSerializer();

        /// <summary>
        /// 获取热门标签列表
        /// </summary>
        /// <param name="jsondata"></param>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetHotTaglist_JSON(string jsondata)
        {
            int TopNumber=5;
            if(!string.IsNullOrEmpty(jsondata))
            {
                 Dictionary<string, object> json = (Dictionary<string, object>)serializer.DeserializeObject(jsondata);
                TopNumber=Convert.ToInt32(json["topnumber"]);
            }
            BLL.SearchManager manager = new BLL.SearchManager();
            List<BLL.TagSumInfo> list= manager.GetHotTaglist(TopNumber);

            string returnjsonData = ObjToJson.ToJson(list);

            OutManager.Instance.Output("{\"result\":\"true\",\"message\":\"执行成功\",\"data\":" + returnjsonData + "}");
        }

        /// <summary>
        /// 获取文章列表__根据标签
        /// </summary>
        /// <param name="jsondata"></param>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetListByTag_JSON(string jsondata)
        {
            string strtag="";
            int page=1;
            int pagesize=5;
            if (!string.IsNullOrEmpty(jsondata))
            {
                Dictionary<string, object> json = (Dictionary<string, object>)serializer.DeserializeObject(jsondata);
                strtag =json["tag"].ToString();
                page = Convert.ToInt32(json["page"]);
                pagesize = Convert.ToInt32(json["pagesize"]);
            }
            BLL.SearchManager manager = new BLL.SearchManager();
            BLL.EssayListInfo listinfo = manager.GetListByTag(strtag, page, pagesize);


            string returnjsonData = ObjToJson.ToJson(listinfo);
OutManager.Instance.Output("{\"result\":\"true\",\"message\":\"执行成功\",\"data\":" + returnjsonData + "}");
        }

        /// <summary>
        /// 获取文章列表__根据搜索关键字
        /// </summary>
        /// <param name="jsondata"></param>
        [WebMethod]
        [ScriptMethod(ResponseFormat = ResponseFormat.Json)]
        public void GetListByNameKey_JSON(string jsondata)
        {
            string strkey = "";
            int page = 1;
            int pagesize = 5;
            if (!string.IsNullOrEmpty(jsondata))
            {
                Dictionary<string, object> json = (Dictionary<string, object>)serializer.DeserializeObject(jsondata);
                strkey = json["key"].ToString();
                page = Convert.ToInt32(json["page"]);
                pagesize = Convert.ToInt32(json["pagesize"]);
            }
            BLL.SearchManager manager = new BLL.SearchManager();
            BLL.EssayListInfo listinfo = manager.GetListByNameKey(strkey, page, pagesize);


            string returnjsonData = ObjToJson.ToJson(listinfo);

            OutManager.Instance.Output("{\"result\":\"true\",\"message\":\"执行成功\",\"data\":" + returnjsonData + "}");
        }

        /// <summary>
        /// 获取文章详情
        /// </summary>
        /// <param name="jsondata"></param>
        [WebMethod]
        [ScriptMethod(ResponseFormat=ResponseFormat.Json)]
        public void GetDetailByKey_JSON(string jsondata)
        {
            string strkey = "";
            if (!string.IsNullOrEmpty(jsondata))
            {
                Dictionary<string, object> json = (Dictionary<string, object>)serializer.DeserializeObject(jsondata);
                strkey = json["key"].ToString();
            }
            BLL.SearchManager manager = new BLL.SearchManager();
            BLL.EssayInfoDetail detail = manager.GetDetailByKey(strkey);

            //string returnjsonData = ObjToJson.ToJson(detail.EDetail);

            OutManager.Instance.Output(detail.EDetail);
        }
    }
}
