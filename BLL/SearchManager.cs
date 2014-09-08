using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Linq;
using System.Text;

namespace BLL
{
    /// <summary>
    /// 资源搜索管理
    /// </summary>
    public class SearchManager
    {
        /// <summary>
        /// 获取热门标签列表
        /// </summary>
        /// <param name="_intnumber"></param>
        /// <returns></returns>
        public List<TagSumInfo> GetHotTaglist(int _intnumber)
        {
            List<TagSumInfo> taglist = null;
            string strSQL = string.Format("select ETag,count(EKey) as sumnumber  from EssayInfo group by ETag order by sumnumber desc limit {0} offset 0", _intnumber);
            DataTable dtt = DAL.SQLiteHelper.Instance.ExecuteDataTable(strSQL, null);
            if (dtt != null && dtt.Rows.Count > 0)
            {
                taglist = new List<TagSumInfo>();
                DataTable _imgtab=null;
                for (int i = 0; i < dtt.Rows.Count; i++)
                {
                    TagSumInfo _item = FormatByRow(dtt.Rows[i]);
                    if (_item != null)
                    {
                        strSQL = string.Format("select EKey,EThumbnailsData from EssayInfo where ETag='{0}' order by EKey limit 5 offset 0", _item.Etag);
                        _imgtab = DAL.SQLiteHelper.Instance.ExecuteDataTable(strSQL, null);
                        if (_imgtab != null && _imgtab.Rows.Count > 0)
                        {
                            for(int j=0;j<_imgtab.Rows.Count;j++)
                            {
                                _item.Images.Add(new IOCImageInfo(_imgtab.Rows[j]["EKey"].ToString(), _imgtab.Rows[j]["EThumbnailsData"].ToString()));
                            }
                        }
                        taglist.Add(_item);
                    }
                }
            }

            return taglist;
        }

        /// <summary>
        /// 根据标签获取列表
        /// </summary>
        /// <param name="_strtag">标签</param>
        /// <param name="_page">页码</param>
        /// <param name="_pagesize">每页显示记录数</param>
        /// <returns></returns>
        public EssayListInfo GetListByTag(string _strtag, int _page, int _pagesize)
        {
            EssayListInfo _list = null;
            List<EssayInfo> items = null;
            int sumnumber = 0;

            SQLiteParameter[] pars = null;
            pars = new SQLiteParameter[1];
            pars[0] = new SQLiteParameter("@ETag", DbType.String);
            pars[0].Value = _strtag;
 

            string strSQL = string.Format("select count(EKey) as sumnumber  from EssayInfo where ETag=@ETag ");
            DataTable dtt = DAL.SQLiteHelper.Instance.ExecuteDataTable(strSQL, pars);
            if (dtt != null && dtt.Rows.Count > 0)
            {
                sumnumber = Convert.ToInt32(dtt.Rows[0]["sumnumber"]);
                if (sumnumber > 0)
                {
                     int StartPoint = 0;
                    if (_page > 1)
                    {
                        StartPoint = (_page - 1) * _pagesize;
                    }
                    strSQL = string.Format("select * from EssayInfo where ETag=@ETag order by EKey limit {0} offset {1}", _pagesize, StartPoint);
                    dtt = DAL.SQLiteHelper.Instance.ExecuteDataTable(strSQL, pars);
                    if (dtt != null && dtt.Rows.Count > 0)
                    {
                        items = new List<EssayInfo>();
                        for (int i = 0; i < dtt.Rows.Count; i++)
                        {
                            EssayInfo item = EssayinfoFormatByRow(dtt.Rows[i]);
                            if (item != null)
                            {
                                items.Add(item);
                            }
                        }
                    }
                }
            }
            _list = new EssayListInfo(items, sumnumber, _page, _pagesize);
            return _list;
        }

        /// <summary>
        /// 根据名称关键字搜索文章列表
        /// </summary>
        /// <param name="_strkey">搜索关键字</param>
        /// <param name="_page"></param>
        /// <param name="_pagesize"></param>
        /// <returns></returns>
        public EssayListInfo GetListByNameKey(string _strkey, int _page, int _pagesize)
        {
            EssayListInfo _list = null;
            List<EssayInfo> items = null;
            int sumnumber = 0;

            string strSQL = string.Format("select count(EKey) as sumnumber  from EssayInfo where ETag like '%{0}%' or ETitle like '%{0}%' or ESummary like '%{0}%'",_strkey);
            DataTable dtt = DAL.SQLiteHelper.Instance.ExecuteDataTable(strSQL, null);
            if (dtt != null && dtt.Rows.Count > 0)
            {
                sumnumber = Convert.ToInt32(dtt.Rows[0]["sumnumber"]);
                if (sumnumber > 0)
                {
                    int StartPoint = 0;
                    if (_page > 1)
                    {
                        StartPoint = (_page - 1) * _pagesize;
                    }
                    strSQL = string.Format("select * from EssayInfo where ETag like '%{0}%' or ETitle like '%{0}%' or ESummary like '%{0}%' order by EKey limit {1} offset {2}",_strkey,_pagesize, StartPoint);
                    dtt = DAL.SQLiteHelper.Instance.ExecuteDataTable(strSQL, null);
                    if (dtt != null && dtt.Rows.Count > 0)
                    {
                        items = new List<EssayInfo>();
                        for (int i = 0; i < dtt.Rows.Count; i++)
                        {
                            EssayInfo item = EssayinfoFormatByRow(dtt.Rows[i]);
                            if (item != null)
                            {
                                items.Add(item);
                            }
                        }
                    }
                }
            }
            _list = new EssayListInfo(items, sumnumber, _page, _pagesize);
            return _list;
        }

        /// <summary>
        /// 获取详情
        /// </summary>
        /// <param name="_strkey"></param>
        /// <returns></returns>
        public EssayInfoDetail GetDetailByKey(string _strkey) 
        {
            EssayInfoDetail _Essaydetailinfo = null;
            string strSQL = string.Format("select * from EssayInfoDetail where  EKey='{0}'", _strkey);
            DataTable dtt = DAL.SQLiteHelper.Instance.ExecuteDataTable(strSQL, null);
            if (dtt != null && dtt.Rows.Count > 0)
            {
                _Essaydetailinfo = EssayDetailFormatByRow(dtt.Rows[0]);
            }
            return _Essaydetailinfo;
        }

        private TagSumInfo FormatByRow(DataRow _row)
        {
            TagSumInfo _tag = null;
            if (_row != null)
            {
                _tag = new TagSumInfo();
                _tag.Etag = _row["ETag"].ToString();
                _tag.Sumnumber = Convert.ToInt32(_row["sumnumber"]);
            }
            return _tag;
        }

        private EssayInfo EssayinfoFormatByRow(DataRow _row)
        {
            EssayInfo _item = null;
            if (_row != null)
            {
                _item = new EssayInfo();
                _item.Address = _row["EAddress"].ToString();
                //EThumbnails
                _item.Thumbnails = _row["EThumbnails"].ToString();
                _item.BrowseNumber = Convert.ToInt32(_row["EBrowseNumber"]);
                _item.CommentaryNumber = Convert.ToInt32(_row["ECommentaryNumber"]);
                _item.EssayDetail = _row["EEssayDetail"].ToString();
                _item.PubDate = _row["EPubDate"].ToString();
                _item.Summary = _row["ESummary"].ToString();
                _item.Tag = _row["ETag"].ToString();
                _item.ThumbnailsData = _row["EThumbnailsData"].ToString();
                _item.Title = _row["ETitle"].ToString();
            }
            return _item;
        }

        /// <summary>
        /// 格式化详情信息
        /// </summary>
        /// <param name="_row"></param>
        /// <returns></returns>
        private EssayInfoDetail EssayDetailFormatByRow(DataRow _row) 
        {
            EssayInfoDetail _detail = null;
            if (_row != null)
            {
                _detail = new EssayInfoDetail();
                _detail.EDetail = _row["EDetail"].ToString();
                // 
                _detail.EKey = _row["EKey"].ToString();
                _detail.EImages = _row["EImages"].ToString();
 
            }
            return _detail;
        }
    }


    /// <summary>
    /// 标签统计信息
    /// </summary>
    public class TagSumInfo 
    {
        private string _etag;
        /// <summary>
        /// 标签名
        /// </summary>
        public string Etag
        {
            get { return _etag; }
            set { _etag = value; }
        }
        private int _sumnumber;
        /// <summary>
        /// 统计总数
        /// </summary>
        public int Sumnumber
        {
            get { return _sumnumber; }
            set { _sumnumber = value; }
        }
        private List<IOCImageInfo> _images = new List<IOCImageInfo>();
        /// <summary>
        /// 代表性图片集
        /// </summary>
        public List<IOCImageInfo> Images
        {
            get { return _images; }
            set { _images = value; }
        }
    }
    public class IOCImageInfo
    {
        public IOCImageInfo()
        { }
        public IOCImageInfo(string strkey, string strpath)
        {
            _Key = strkey;
            _Path = strpath;
        }
        private string _Key;
        /// <summary>
        /// 文章标识
        /// </summary>
        public string Key
        {
            get { return _Key; }
            set { _Key = value; }
        }

        private string _Path;
        /// <summary>
        /// 图片路径
        /// </summary>
        public string Path
        {
            get { return _Path; }
            set { _Path = value; }
        }
    }
    
    /// <summary>
    /// 文章列表信息
    /// </summary>
    public class EssayListInfo
    {
        public EssayListInfo()
        { }
        /// <summary>
        /// 列表信息 构造函数
        /// </summary>
        /// <param name="sublist"></param>
        /// <param name="sumnumber"></param>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        public EssayListInfo(List<EssayInfo> _sublist, int _sumnumber, int _page, int _pagesize)
        {
            items = _sublist;
            sumnumber = _sumnumber;
            pagesize = _pagesize;
            thispage = _page;
        }

        private List<EssayInfo> items;
        /// <summary>
        /// 记录集
        /// </summary>
        public List<EssayInfo> Items
        {
            get { return items; }
            set { items = value; }
        }
        private int sumnumber=0;
        /// <summary>
        /// 总记录数
        /// </summary>
        public int Sumnumber
        {
            get { return sumnumber; }
            set { sumnumber = value; }
        }
        private int pagesize;
        /// <summary>
        /// 每页显示记录数
        /// </summary>
        public int Pagesize
        {
            get { return pagesize; }
            set { pagesize = value; }
        }
        private int thispage;
        /// <summary>
        /// 当前页
        /// </summary>
        public int Thispage
        {
            get { return thispage; }
            set { thispage = value; }
        }
        private int sumpage;
        /// <summary>
        /// 总页数
        /// </summary>
        public int Sumpage
        {
            get 
            {
                if (sumnumber % pagesize == 0) 
                {
                    sumpage = sumnumber / pagesize;
                }
                else
                {
                    sumpage = sumnumber / pagesize+1;
                }
                return sumpage; 
            }
        }
    }

}
