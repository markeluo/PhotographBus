using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BLL
{
     public class PageParserManager
    {
         private static PageParserManager manager = new PageParserManager();
         public static PageParserManager Instance
         {
             get
             {
                 return manager;
             }
         }

         /// <summary>
         /// 解析新文章列表
         /// </summary>
         /// <param name="_Data"></param>
         /// <returns></returns>
         public EssayClass NewEssayParser(string _Data)
         {
             EssayClass _class = new EssayClass();
            string strDataContent = _Data.ToString().Replace("\r\n", "").Replace("\n", "");
            //Regex reg = new Regex("<div class=\"post_box\".*?</div><!-- ad -->", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            //MatchCollection mc = reg.Matches(strDataContent);

            Regex reg = new Regex("<div id=\"mainbox\">.*<div id=\"sidebox\"", RegexOptions.IgnoreCase | RegexOptions.Singleline);
             MatchCollection mc = reg.Matches(strDataContent);
             if (mc != null && mc.Count > 0)
             {
                 string LinkListStr = mc[0].ToString();
                 //文章列表
                 string[] strArray = LinkListStr.Split(new string[] { "<div class=\"post_box\"" }, StringSplitOptions.RemoveEmptyEntries);
                 List<string> ItemList = strArray.ToList<string>();
                 ItemList.RemoveAt(0);
                 foreach (string strm in ItemList)
                 {
                     _class.DataCollection.Add(new EssayInfo(strm, true));
                 }
             }
            return _class;
         }

         /// <summary>
         /// 解析类别文章
         /// </summary>
         /// <param name="_Data"></param>
         /// <returns></returns>
         public EssayClass EssayClassParser(string _Data)
         {
             EssayClass _class = new EssayClass();

             _Data = _Data.ToString().Replace("\r\n", "").Replace("\n", "");
             string strDataContent = _Data.ToString();

             Regex reg = new Regex("<div id=\"mainbox\">.*<div id=\"sidebox\"", RegexOptions.IgnoreCase | RegexOptions.Singleline);
             MatchCollection mc = reg.Matches(strDataContent);
             if(mc!=null && mc.Count>0)
             {
                 string LinkListStr = mc[0].ToString();
                 //文章列表
                 string[] strArray = LinkListStr.Split(new string[] { "<div class=\"post_box\"" }, StringSplitOptions.RemoveEmptyEntries);

                 //获取当前页与总页数信息
                 reg = new Regex(@"<div class=\Swp-pagenavi\S><span>.*?</span>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                 mc = reg.Matches(strDataContent);
                 string PageTolsInfo = mc[0].ToString();
                 PageTolsInfo = PageTolsInfo.Substring(PageTolsInfo.LastIndexOf("<span>") + 6);
                 PageTolsInfo = PageTolsInfo.Substring(0, PageTolsInfo.LastIndexOf("</span>")).Trim();
                 string[] PageTotalPagesArray = PageTolsInfo.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);

                 if (PageTotalPagesArray != null && PageTotalPagesArray.Length == 2)
                 {
                     _class.TolPages = Convert.ToInt32(PageTotalPagesArray[1]);
                     _class.ThisPage = Convert.ToInt32(PageTotalPagesArray[0]);
                 }

                
                 List<string> ItemList = strArray.ToList<string>();
                 ItemList.RemoveAt(0);

                 foreach (string _strEssayHtml in ItemList)
                 {
                     _class.DataCollection.Add(new EssayInfo(_strEssayHtml));
                 }
             }
             return _class;
         }
    }
}
