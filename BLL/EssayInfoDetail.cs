using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class EssayInfoDetail
    {
        public EssayInfoDetail()
        { }
        public EssayInfoDetail(string strkey, string strdetail, string strimgs) 
        {
            _EKey = strkey;
            _EDetail = strdetail;
            _EImages = strimgs;
        }

        private string _EKey;
        /// <summary>
        /// 文章编号
        /// </summary>
        public string EKey
        {
            get { return _EKey; }
            set { _EKey = value; }
        }
        private string _EDetail;
        /// <summary>
        /// 文章详情
        /// </summary>
        public string EDetail
        {
            get { return _EDetail; }
            set { _EDetail = value; }
        }
        private string _EImages;
        /// <summary>
        /// 图片列表
        /// </summary>
        public string EImages
        {
            get { return _EImages; }
            set { _EImages = value; }
        }
    }
}
