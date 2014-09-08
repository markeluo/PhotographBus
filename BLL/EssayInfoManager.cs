using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SQLite;
using System.Data.Common;
using System.Data;

namespace BLL
{
    public class EssayInfoManager
    {
        /// <summary>
        /// 保存列表
        /// </summary>
        /// <param name="_EssayClas"></param>
        public void SaveLogs(EssayClass _EssayClas)
        {
            if (_EssayClas != null && _EssayClas.DataCollection != null && _EssayClas.DataCollection.Count > 0)
            {
                DBConfigControl.DBConfig dbconfig = DBConfigControl.DBConfigFactory.Instance.LoadDbconfig();
                string connectionString = dbconfig.GetConnectionstring();
                SQLiteConnection _conn = new SQLiteConnection(connectionString);
                _conn.Open();
                DbTransaction transcation = _conn.BeginTransaction();

                bool _UpstractSucced = false;
                SQLiteParameter[] pars = null;
                string strSQL = "";
                for (int i = 0; i < _EssayClas.DataCollection.Count;i++)
                {
                    if (i == (_EssayClas.DataCollection.Count- 1))
                    {
                        _UpstractSucced = true;
                    }
                    //EKey nvarchar(50),
                    //ETitle nvarchar(100),
                    //EAddress nvarchar(200),
                    //ETag nvarchar(200),
                    //ECommentaryNumber INT64,
                    //EBrowseNumber INT64,
                    //ESummary nvarchar(500),
                    //EThumbnails nvarchar(100),
                    //EThumbnailsData nvarchar(100),
                    //EEssayDetail nvarchar(100),
                    //EPubDate nvarchar(20)
                    pars = new SQLiteParameter[11];
                    pars[0] = new SQLiteParameter("@EKey", DbType.String);
                    pars[0].Value = _EssayClas.DataCollection[i].Key;
                    pars[1] = new SQLiteParameter("@ETitle", DbType.String);
                    pars[1].Value = _EssayClas.DataCollection[i].Title;
                    pars[2] = new SQLiteParameter("@EAddress", DbType.String);
                    pars[2].Value = _EssayClas.DataCollection[i].Address;
                    pars[3] = new SQLiteParameter("@ETag", DbType.String);
                    pars[3].Value = _EssayClas.DataCollection[i].Tag;
                    pars[4] = new SQLiteParameter("@ECommentaryNumber", DbType.Int64);
                    pars[4].Value = _EssayClas.DataCollection[i].CommentaryNumber;
                    pars[5] = new SQLiteParameter("@EBrowseNumber", DbType.Int64);
                    pars[5].Value = _EssayClas.DataCollection[i].BrowseNumber;
                    pars[6] = new SQLiteParameter("@ESummary", DbType.String);
                    pars[6].Value = _EssayClas.DataCollection[i].Summary;
                    pars[7] = new SQLiteParameter("@EThumbnails", DbType.String);
                    pars[7].Value = _EssayClas.DataCollection[i].Thumbnails;
                    pars[8] = new SQLiteParameter("@EThumbnailsData", DbType.String);
                    pars[8].Value = _EssayClas.DataCollection[i].ThumbnailsData;
                    pars[9] = new SQLiteParameter("@EEssayDetail", DbType.String);
                    pars[9].Value = _EssayClas.DataCollection[i].EssayDetail;
                    pars[10] = new SQLiteParameter("@EPubDate", DbType.String);
                    pars[10].Value = _EssayClas.DataCollection[i].PubDate;

                    strSQL = "insert into EssayInfo (EKey,ETitle,EAddress,ETag,ECommentaryNumber,EBrowseNumber,ESummary,EThumbnails,EThumbnailsData,EEssayDetail,EPubDate) values "+
                        "(@EKey,@ETitle,@EAddress,@ETag,@ECommentaryNumber,@EBrowseNumber,@ESummary,@EThumbnails,@EThumbnailsData,@EEssayDetail,@EPubDate)";

                    _UpstractSucced = ExeCuteCommand(_conn, transcation, strSQL, pars, _UpstractSucced);
                }
            }
        }

        /// <summary>
        /// 存储详情
        /// </summary>
        /// <param name="_detail"></param>
        public void SaveDetail(EssayInfoDetail _detail)
        {
            if (_detail != null)
            {
                DBConfigControl.DBConfig dbconfig = DBConfigControl.DBConfigFactory.Instance.LoadDbconfig();
                string connectionString = dbconfig.GetConnectionstring();
                SQLiteConnection _conn = new SQLiteConnection(connectionString);
                _conn.Open();
                DbTransaction transcation = _conn.BeginTransaction();

                bool _UpstractSucced = true;
                SQLiteParameter[] pars = null;
                string strSQL = "";

                //EKey nvarchar(50),
                //EDetail nvarchar(100),
                //EImages nvarchar(1000)

                pars = new SQLiteParameter[3];
                pars[0] = new SQLiteParameter("@EKey", DbType.String);
                pars[0].Value = _detail.EKey;
                pars[1] = new SQLiteParameter("@EDetail", DbType.String);
                pars[1].Value =_detail.EDetail;
                pars[2] = new SQLiteParameter("@EImages", DbType.String);
                pars[2].Value =_detail.EImages;

                strSQL = "insert into EssayInfoDetail(EKey,EDetail,EImages) values (@EKey,@EDetail,@EImages)";

                _UpstractSucced = ExeCuteCommand(_conn, transcation, strSQL, pars, _UpstractSucced);
            }
        }

        /// <summary>
        /// 获取未下载详情的文章列表
        /// </summary>
        /// <param name="_page"></param>
        /// <param name="_pagesize"></param>
        /// <returns></returns>
        public List<EssayInfo> GetListNoDetail(int _page, int _pagesize)
        {
            List<EssayInfo> list = new List<EssayInfo>();
            int StartPoint = 0;
            //if (_page > 1)
            //{
            //    StartPoint = (_page - 1) * _pagesize;
            //}
            string strSQL = string.Format("select * from EssayInfo where EKey not in(select EKey from EssayInfoDetail) order by EKey limit {0} offset {1}", _pagesize, StartPoint);
            DataTable dtt = DAL.SQLiteHelper.Instance.ExecuteDataTable(strSQL, null);
            if (dtt != null && dtt.Rows.Count > 0)
            {
                for (int i = 0; i < dtt.Rows.Count; i++)
                {
                    EssayInfo _item = FormatByRow(dtt.Rows[i]);
                    if (_item != null)
                    {
                        list.Add(_item);
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="_page"></param>
        /// <param name="_pagesize"></param>
        /// <returns></returns>
        public List<EssayInfo> GetNoDownloadMiniImgList(int _page, int _pagesize)
        {
            List<EssayInfo> list = new List<EssayInfo>();
            int StartPoint = 0;
            if (_page > 1)
            {
                StartPoint = (_page - 1) * _pagesize;
            }
            string strSQL = string.Format("select * from EssayInfo where EThumbnailsData='' order by EKey limit {0} offset {1}", _pagesize, StartPoint);
            DataTable dtt = DAL.SQLiteHelper.Instance.ExecuteDataTable(strSQL, null);
            if (dtt != null && dtt.Rows.Count > 0)
            {
                for (int i = 0; i < dtt.Rows.Count; i++)
                {
                    EssayInfo _item = FormatByRow(dtt.Rows[i]);
                    if (_item != null)
                    {
                        list.Add(_item);
                    }
                }
            }
            return list;
        
        }

        public List<EssayInfoDetail> GetNoDownloadDetailList(int _page, int _pagesize)
        {
            List<EssayInfoDetail> list = new List<EssayInfoDetail>();
            int StartPoint = 0;
            if (_page > 1)
            {
                StartPoint = (_page - 1) * _pagesize;
            }
            string strSQL = string.Format("select EKey,EDetail,EImages from EssayInfoDetail where EIsLoadImg='0' and EImages!='' order by EKey limit {0} offset {1}", _pagesize, StartPoint);
            DataTable dtt = DAL.SQLiteHelper.Instance.ExecuteDataTable(strSQL, null);
            if (dtt != null && dtt.Rows.Count > 0)
            {
                for (int i = 0; i < dtt.Rows.Count; i++)
                {
                    EssayInfoDetail _item = FormatDetailByRow(dtt.Rows[i]);
                    if (_item != null)
                    {
                        list.Add(_item);
                    }
                }
            }
            return list;
        }
        /// <summary>
        /// 获取未解析图片列表的文字详情列表
        /// </summary>
        /// <param name="_page"></param>
        /// <param name="_pagesize"></param>
        /// <returns></returns>
        public List<EssayInfoDetail> GetNoParseDetailList(int _page, int _pagesize)
        {
            List<EssayInfoDetail> list = new List<EssayInfoDetail>();
            int StartPoint = 0;
            if (_page > 1)
            {
                StartPoint = (_page - 1) * _pagesize;
            }
            string strSQL = string.Format("select EKey,EDetail,EImages from EssayInfoDetail where EIsParseImg='0' order by EKey limit {0} offset {1}", _pagesize, StartPoint);
            DataTable dtt = DAL.SQLiteHelper.Instance.ExecuteDataTable(strSQL, null);
            if (dtt != null && dtt.Rows.Count > 0)
            {
                for (int i = 0; i < dtt.Rows.Count; i++)
                {
                    EssayInfoDetail _item = FormatDetailByRow(dtt.Rows[i]);
                    if (_item != null)
                    {
                        list.Add(_item);
                    }
                }
            }
            return list;
        }

        /// <summary>
        /// 更新缩略图
        /// </summary>
        /// <param name="?"></param>
        /// <returns></returns>
        public bool UpdateEssayMiniImg(EssayInfo _essayinfo)
        {
            bool bolSucced = false;
            try
            {
                DBConfigControl.DBConfig dbconfig = DBConfigControl.DBConfigFactory.Instance.LoadDbconfig();
                string connectionString = dbconfig.GetConnectionstring();
                SQLiteConnection _conn = new SQLiteConnection(connectionString);
                _conn.Open();
                DbTransaction transcation = _conn.BeginTransaction();

                bool _UpstractSucced = true;
                SQLiteParameter[] pars = null;
                string strSQL = "";
                pars = new SQLiteParameter[2];
                pars[0] = new SQLiteParameter("@EKey", DbType.String);
                pars[0].Value = _essayinfo.Key;

                pars[1] = new SQLiteParameter("@EThumbnailsData", DbType.String);
                pars[1].Value = _essayinfo.ThumbnailsData;

                strSQL = "update EssayInfo set EThumbnailsData=@EThumbnailsData where EKey=@EKey";

                _UpstractSucced = ExeCuteCommand(_conn, transcation, strSQL, pars, _UpstractSucced);

                bolSucced = true;
            }
            catch (Exception ex)
            {
            }

            return bolSucced;
        }

        /// <summary>
        /// 更新详情图片列表
        /// </summary>
        /// <param name="_details"></param>
        /// <returns></returns>
        public bool UpdateDetailImgList(EssayInfoDetail _details)
        {
            bool bolSucced = false;
            try
            {
                DBConfigControl.DBConfig dbconfig = DBConfigControl.DBConfigFactory.Instance.LoadDbconfig();
                string connectionString = dbconfig.GetConnectionstring();
                SQLiteConnection _conn = new SQLiteConnection(connectionString);
                _conn.Open();
                DbTransaction transcation = _conn.BeginTransaction();

                bool _UpstractSucced = true;
                SQLiteParameter[] pars = null;
                string strSQL = "";
                pars = new SQLiteParameter[2];
                pars[0] = new SQLiteParameter("@EKey", DbType.String);
                pars[0].Value = _details.EKey;

                pars[1] = new SQLiteParameter("@EImages", DbType.String);
                pars[1].Value = _details.EImages;

                strSQL = "update EssayInfoDetail set EImages=@EImages,EIsParseImg='1' where EKey=@EKey";

                _UpstractSucced = ExeCuteCommand(_conn, transcation, strSQL, pars, _UpstractSucced);

                bolSucced = true;
            }
            catch (Exception ex)
            {
            }

            return bolSucced;
        }

        /// <summary>
        /// 更新详情图片路径信息
        /// </summary>
        /// <param name="_details"></param>
        /// <returns></returns>
        public bool UpdateDetailImgsPath(EssayInfoDetail _details)
        {
            bool bolSucced = false;
            try
            {
                DBConfigControl.DBConfig dbconfig = DBConfigControl.DBConfigFactory.Instance.LoadDbconfig();
                string connectionString = dbconfig.GetConnectionstring();
                SQLiteConnection _conn = new SQLiteConnection(connectionString);
                _conn.Open();
                DbTransaction transcation = _conn.BeginTransaction();

                bool _UpstractSucced = true;
                SQLiteParameter[] pars = null;
                string strSQL = "";
                pars = new SQLiteParameter[3];
                pars[0] = new SQLiteParameter("@EKey", DbType.String);
                pars[0].Value = _details.EKey;

                pars[1] = new SQLiteParameter("@EImages", DbType.String);
                pars[1].Value = _details.EImages;

                pars[2] = new SQLiteParameter("@EDetail", DbType.String);
                pars[2].Value = _details.EDetail;

                strSQL = "update EssayInfoDetail set EDetail=@EDetail,EImages=@EImages,EIsLoadImg='1' where EKey=@EKey";

                _UpstractSucced = ExeCuteCommand(_conn, transcation, strSQL, pars, _UpstractSucced);

                bolSucced = true;
            }
            catch (Exception ex)
            {
            }

            return bolSucced;
        }

        private EssayInfo FormatByRow(DataRow _row)
        {
            EssayInfo _item = null;
            if (_row != null)
            {
                _item = new EssayInfo();
                _item.Address = _row["EAddress"].ToString();
                //EThumbnails
                _item.Thumbnails = _row["EThumbnails"].ToString();
                
            }
            return _item;
        }
        private EssayInfoDetail FormatDetailByRow(DataRow _row)
        {
            EssayInfoDetail _item = null;
            if (_row != null)
            {
                _item = new EssayInfoDetail();
                _item.EKey = _row["EKey"].ToString();
                //EKey,EImages
                _item.EImages = _row["EImages"].ToString();
                if (_row["EDetail"] != null)
                {
                    _item.EDetail = _row["EDetail"].ToString();
                }

            }
            return _item;
        }

        /// <summary>
        /// 执行命令
        /// </summary>
        /// <param name="_conn">连接对象</param>
        /// <param name="_Action">事务</param>
        /// <param name="strSQL">命令语句</param>
        /// <param name="_pars">命令参数</param>
        /// <param name="bolSubmit">是否提交事务</param>
        /// <returns>true:提交成功;false:提交失败</returns>
        private bool ExeCuteCommand(SQLiteConnection _conn, DbTransaction _Action, string strSQL, SQLiteParameter[] _pars, bool bolSubmit)
        {
            bool bolSucced = true;
            SQLiteCommand cmd = new SQLiteCommand(_conn);
            cmd.CommandText = strSQL;
            if (_pars != null)
            {
                cmd.Parameters.AddRange(_pars);
            }
            try
            {
                bolSucced = DAL.SQLiteHelper.Instance.ExeCuteCmd(cmd, _Action, bolSubmit);
            }
            catch
            {
                bolSucced = false;
            }
            return bolSucced;
        }
 
    }
}
