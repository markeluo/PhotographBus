using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class ImageDownload
    {
        public delegate void DelDownloadImgArg(EssayInfo eaayinfo,string _Imagename);
        public event DelDownloadImgArg DownloadMiniImgEvent;
        public delegate void DelDownloadDetailImgs(EssayInfoDetail detail, List<string> _imgs);
        public event DelDownloadDetailImgs DownloadImgsEvent;

        private EssayInfo thisEssayInfo = null;

        /// <summary>
        /// 下载缩略图
        /// </summary>
        /// <param name="_essayinfo"></param>
        public void DownLoadMiniImage(EssayInfo _essayinfo)
        {
            thisEssayInfo = _essayinfo;
            using (System.Net.WebClient wc = new System.Net.WebClient())
            {
                wc.Encoding = Encoding.UTF8;
                wc.Headers.Add("User-Agent", "Chrome");
                string strImgName=GetGUID() + ".jpg";
                string strImgSavePath = DBConfigControl.DBConfigFactory.Instance.LoadDbconfig().MiniImgPath;
                strImgSavePath = Path.Combine(strImgSavePath,strImgName);
                wc.DownloadFile(thisEssayInfo.Thumbnails, strImgSavePath);//保存到本地的文件名和路径，请自行更改

                if (DownloadMiniImgEvent != null)
                {
                    DownloadMiniImgEvent(_essayinfo, strImgName);
                }
            }
        }

        /// <summary>
        /// 文章详情图片下载
        /// </summary>
        /// <param name="_detailInfo"></param>
        public void DownloadDetailImgs(EssayInfoDetail _detailInfo)
        {
            List<string> items = new List<string>();
            string[] imgs = _detailInfo.EImages.Split(new string[1] { "###" }, StringSplitOptions.RemoveEmptyEntries);


            for (int i = 0; i < imgs.Length; i++)
            {
                try
                {
                    string strImgName = GetGUID() + imgs[i].Substring(imgs[i].LastIndexOf("."));
                    string strImgSavePath = DBConfigControl.DBConfigFactory.Instance.LoadDbconfig().DetailImgPath;
                    strImgSavePath = Path.Combine(strImgSavePath, strImgName);

                    using (System.Net.WebClient wc = new System.Net.WebClient())
                    {
                        wc.Encoding = Encoding.UTF8;
                        wc.Headers.Add("User-Agent", "Chrome");
                        wc.DownloadFile(imgs[i], strImgSavePath);//保存到本地的文件名和路径，请自行更改
                    }
                    imgs[i] = strImgName;
                    items.Add(imgs[i]);
                }
                catch (Exception ex)
                {
                    items.Add(imgs[i]);
                }
            }
 
            if (DownloadImgsEvent != null)
            {
                DownloadImgsEvent(_detailInfo, items);
            }
        }
        

        /// <summary>
        /// 唯一ID值
        /// </summary>
        /// <returns></returns>
        private String GetGUID()
        {
            return System.Guid.NewGuid().ToString("N");
        }
    }
}
