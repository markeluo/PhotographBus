using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace webservice
{
    public class ImageManager
    {
        /// <summary>
        /// 是否为请求图片
        /// </summary>
        /// <param name="strurl">url路径</param>
        /// <returns></returns>
        public bool IsImagePath(string strurl)
        {
            bool bolisimg = false;
            strurl = strurl.ToLower();
            if (strurl.IndexOf("/miniimg/") == 0 || strurl.IndexOf("/detailimg/") == 0)
            {
                //图片处理
                string filefullname = strurl.Substring(strurl.LastIndexOf("/") + 1);
                if (filefullname.IndexOf(".") > -1)
                {
                    string fileextname = filefullname.Substring(filefullname.LastIndexOf(".") + 1);
                    if (fileextname == "jpg" || fileextname == "png" || fileextname == "gif" || fileextname == "bmp")
                    {
                        bolisimg = true;
                    }
                }
            }
            return bolisimg;
        }

        public void OutPutImage(HttpResponse _Response,string _imgurl)
        {
            _imgurl = _imgurl.ToLower();
            string strImageDirPath = "";
            string strExtType="";
            DBConfigControl.DBConfig config=DBConfigControl.DBConfigFactory.Instance.LoadDbconfig();
            if (_imgurl.IndexOf("/miniimg/") == 0)
            {
                strImageDirPath = Path.Combine(config.MiniImgPath, _imgurl.Substring(_imgurl.LastIndexOf("/")+1));
            }
            else if (_imgurl.IndexOf("/detailimg/") == 0)
            {
                strImageDirPath = Path.Combine(config.DetailImgPath, _imgurl.Substring(_imgurl.LastIndexOf("/") + 1));
            }
            else
            {   
            }
            if (strImageDirPath != "" && File.Exists(strImageDirPath))
            {
                _Response.ClearContent();//清除响应报

                byte[] imgdata=GetPictureData(strImageDirPath);
                switch (strExtType)
                {
                    case "bmp":
                        _Response.ContentType = "image/bmp";
                        break;
                    case "png":
                        _Response.ContentType = "image/png";
                        break;
                    case "gif":
                        _Response.ContentType = "image/gif";
                        break;
                    case "jpg":
                        _Response.ContentType = "image/jpeg";
                        break;
                }
                _Response.BinaryWrite(imgdata);  
            }
        }

        public byte[] GetPictureData(string imagepath)
        {
            //根据图片文件的路径使用文件流打开，并保存为byte[]     
            FileStream fs = new FileStream(imagepath, FileMode.Open);
            byte[] byData = new byte[fs.Length];
            fs.Read(byData, 0, byData.Length);
            fs.Close();
            return byData;
        }  
    }
}