using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BLL
{
    public class EssayInfoDetailManager
    {
        private EssayInfo detailinfo;
        public delegate void DelEssayDetailArg(EssayInfoDetail _detail);
        public event DelEssayDetailArg DetailLoadEvent;

        public EssayInfoDetailManager(EssayInfo _detail)
        {
            detailinfo = _detail;
        }
        public void Load()
        {
            HttpHelper httpmanager = new HttpHelper();
            httpmanager.HttpEndEvent += httpmanager_HttpEndEvent;

            httpmanager.GetDetail(detailinfo.Address);
        }

        void httpmanager_HttpEndEvent(string _Data, int page, int type)
        {
            EssayInfoDetail _detail = null;
            if (_Data != null)
            {
                string strDataContent = _Data.Replace("\r\n", "").Replace("\n", "");
                strDataContent = Regex.Replace(strDataContent, "<\\!-- Baidu Button BEGIN -->.*", "");
                strDataContent = Regex.Replace(strDataContent, "<p align=center><a version=.*", "");
                strDataContent = Regex.Replace(strDataContent, "<p align=\"center\"><a version=.*", "");

                strDataContent = Regex.Replace(strDataContent, "<div id=\"container\">?(.*)?<h1>",
                    "<div id=\"container\" style='width:100%;'><div id=\"mainbox\" style='width:100%;'><div class=\"content\" style='width:100%;'><div class=\"post-content\" style='width:100%;'><h1>");
                strDataContent = Regex.Replace(strDataContent, "<\\!-- 顶部LOGO -->.*<\\!--#single main start-->", "");
                strDataContent = Regex.Replace(strDataContent, "<div id=\"sidebox.*", "");

                strDataContent = Regex.Replace(strDataContent, "<link rel=\"bookmark\" href=\"\\/favicon.ico\" type=\"image\\/x-icon\" \\/>.*</head>", "<link rel=\"bookmark\" href=\"/favicon.ico\" type=\"image/x-icon\" /></head>");

                strDataContent = Regex.Replace(strDataContent, "<div id=\"comments\">.*", "");

                strDataContent = strDataContent + "</p></div></div></div></div></body></html>";

                _detail = new EssayInfoDetail(detailinfo.Key, strDataContent, "");
            }

            if (DetailLoadEvent != null)
            {
                DetailLoadEvent(_detail);
            }

        }
    }
}
