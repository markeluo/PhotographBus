using System;
using System.Collections.Generic;
using System.Text;

namespace DBConfigControl
{
    /// <summary>
    /// 数据库配置信息
    /// </summary>
    public class DBConfig
    {
        private string dbTypeName;
        /// <summary>
        /// 数据库类型名称
        /// </summary>
        public virtual string DbTypeName
        {
            get { return dbTypeName; }
        }

        private bool isUse = false;
        /// <summary>
        /// 是否为当前使用的数据库
        /// </summary>
        public bool IsUse
        {
            get { return isUse; }
            set { isUse = value; }
        }

        private string dBReamrke;
        /// <summary>
        /// 数据库备注信息
        /// </summary>
        public string DBReamrke
        {
            get { return dBReamrke; }
            set { dBReamrke = value; }
        }
        private string miniImgPath;
        /// <summary>
        /// 缩略图存储路径
        /// </summary>
        public string MiniImgPath
        {
            get { return miniImgPath; }
            set { miniImgPath = value; }
        }
        private string detailImgPath;
        /// <summary>
        /// 文章详情图片
        /// </summary>
        public string DetailImgPath
        {
            get { return detailImgPath; }
            set { detailImgPath = value; }
        }

        public virtual string GetConnectionstring()
        {
            string strConnectionstring = string.Empty;
            return strConnectionstring;
        }

        /// <summary>
        /// 配置是否正确
        /// </summary>
        /// <returns></returns>
        public virtual bool IsTrue
        {
            get
            {
                return true;
            }
        }
    }
}
