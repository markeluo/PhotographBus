using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BLL
{
    public class DetailImgsmanager
    {
        public delegate void DelStateChangeArg(int sumnumber,int errorcount,bool bolsucced, bool isEnd);
        public event DelStateChangeArg StateChangeEvent;
        private int sumnumber = 0;
        private int errornumber = 0;
        

        #region 解析
        public void StartParse()
        {
            System.Threading.Thread _loadthread = new System.Threading.Thread(new System.Threading.ThreadStart(parsethread));
            _loadthread.IsBackground = true;
            _loadthread.Start();
        }
        private void parsethread()
        {
            bool bolcontiniu = true;
            int page = 1;
            EssayInfoManager manager = null;
            string strHTML = "";

            while (bolcontiniu)
            {
                manager = new EssayInfoManager();
                List<EssayInfoDetail> list = manager.GetNoParseDetailList(page, 10);
                if (list != null && list.Count > 0)
                {
                    for (int i = 0; i < list.Count; i++)
                    {
                        strHTML = list[i].EDetail;

                        Regex regImg = new Regex(@"<img\b[^<>]*?\bsrc[\s\t\r\n]*=[\s\t\r\n]*[""']?[\s\t\r\n]*(?<imgUrl>[^\s\t\r\n""'<>]*)[^<>]*?/?[\s\t\r\n]*>", RegexOptions.IgnoreCase);
                        // 搜索匹配的字符串
                        MatchCollection matches = regImg.Matches(strHTML);

                        //int i = 0;
                        //string[] sUrlList = new string[matches.Count];
                        string strURLs = "";

                        // 取得匹配项列表
                        foreach (Match match in matches)
                        {
                            //sUrlList[i++] = match.Groups["imgUrl"].Value;
                            if (strURLs == "")
                            {
                                strURLs = match.Groups["imgUrl"].Value;
                            }
                            else
                            {
                                strURLs = strURLs + "###" + match.Groups["imgUrl"].Value;
                            }
                        }
                        list[i].EImages = strURLs;

                        manager = new EssayInfoManager();
                        bool bolsucced=manager.UpdateDetailImgList(list[i]);
                        if (bolsucced)
                        {
                        }
                        else
                        {
                            errornumber++;
                        }

                        sumnumber++;
                        if (StateChangeEvent != null)
                        {
                            StateChangeEvent(sumnumber,errornumber, bolsucced, false);
                        }
                    }
                }
                else
                {
                    bolcontiniu = false;
                }
                System.Threading.Thread.Sleep(500);
            }
            if (StateChangeEvent != null)
            {
                StateChangeEvent(sumnumber, errornumber, true, true);
            }
        }
        #endregion

        #region 下载
        public void StartDownload()
        {
            System.Threading.Thread _loadthread = new System.Threading.Thread(new System.Threading.ThreadStart(downloadthread));
            _loadthread.IsBackground = true;
            _loadthread.Start();
        }
        private void downloadthread()
        {
            bool bolcontiniu = true;
            int page = 1;
            EssayInfoManager manager = null;
            string strHTML = "";

            ImageDownload download = null;
            while (bolcontiniu)
            {
                manager = new EssayInfoManager();
                List<EssayInfoDetail> list = manager.GetNoDownloadDetailList(page,1);
                if (list != null && list.Count > 0)
                {
                    download = new ImageDownload();
                    download.DownloadImgsEvent += download_DownloadImgsEvent;
                    download.DownloadDetailImgs(list[0]);
                }
                else
                {
                    bolcontiniu = false;
                }
                System.Threading.Thread.Sleep(1000);
            }
            if (StateChangeEvent != null)
            {
                StateChangeEvent(sumnumber,errornumber, true, true);
            }
        }

        void download_DownloadImgsEvent(EssayInfoDetail detail, List<string> _imgs)
        {
            bool bolsucced = false;
            if (_imgs != null && _imgs.Count > 0)
            {
                 List<string> oldlist= detail.EImages.Split(new string[1] {"###"}, StringSplitOptions.RemoveEmptyEntries).ToList();

                string strImgs="";
                for (int i = 0; i < _imgs.Count; i++)
                {
                    _imgs[i] = "detailimg/" + _imgs[i];
                    detail.EDetail=detail.EDetail.Replace(oldlist[i], _imgs[i]);
                   oldlist[i] = _imgs[i];
                   if (i == 0)
                   {
                       strImgs = _imgs[i];
                   }
                   else
                   {
                       strImgs = strImgs + "###" + _imgs[i];
                   }
                }
                detail.EImages = strImgs;

                EssayInfoManager manager = new EssayInfoManager();
                bolsucced = manager.UpdateDetailImgsPath(detail);
            }
            else
            {
                errornumber++;
            }
            sumnumber++;
            if (StateChangeEvent != null)
            {
                StateChangeEvent(sumnumber,errornumber, bolsucced, false);
            }
        }
        #endregion
    }
}
