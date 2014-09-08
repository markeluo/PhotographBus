using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class ImagesDownloadManager
    {
        public delegate void DelUpdateArg(int number, bool sate);
        public delegate void DelUpdateSateChangeArg(bool IsEnd, int ErrorCount);
        public event DelUpdateArg UpdateMsgEvent;
        public event DelUpdateSateChangeArg UpdateStateChangeEvent;

        public void DownloadMiniImages()
        {

            System.Threading.Thread _loadthread = new System.Threading.Thread(new System.Threading.ThreadStart(downloadminithread));
            _loadthread.IsBackground = true;
            _loadthread.Start();
        }

        private int sumnumber = 0;
        private int errornumber = 0;
        private void downloadminithread()
        {
            bool bolSucced = true;
            int page = 1;
            while (bolSucced)
            {
                EssayInfoManager manager = new EssayInfoManager();
                List<EssayInfo> list = manager.GetNoDownloadMiniImgList(page, 1);
                if (list != null && list.Count > 0)
                {
                    ImageDownload minidownload = new ImageDownload();
                    minidownload.DownloadMiniImgEvent += dwonload_DownloadMiniImgEvent;
                    minidownload.DownLoadMiniImage(list[0]);
                }
                else
                {
                    bolSucced = false;
                }
                System.Threading.Thread.Sleep(500);
            }
            if (UpdateStateChangeEvent != null)
            {
                UpdateStateChangeEvent(true, errornumber);
            }
        }

        void dwonload_DownloadMiniImgEvent(EssayInfo essayinfo, string _Imagename)
        {
            sumnumber++;
            essayinfo.ThumbnailsData ="miniimg/"+_Imagename;
            EssayInfoManager manager = new EssayInfoManager();
            bool bolsucced= manager.UpdateEssayMiniImg(essayinfo);
            if (bolsucced) { }
            else
            {
                errornumber++;
            }
            
            if (UpdateMsgEvent != null)
            {
                UpdateMsgEvent(sumnumber, bolsucced);
            }
        }


    }
}
