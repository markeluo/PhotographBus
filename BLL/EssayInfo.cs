using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;

namespace BLL
{
    /// <summary>
    /// 文章信息
    /// </summary>
    public class EssayInfo 
    {
        public EssayInfo()
        { }

        /// <summary>
        /// 文章信息 构造函数
        /// </summary>
        /// <param name="_strtitle">标题</param>
        /// <param name="_straddress">文章详情地址</param>
        /// <param name="_strtag">标签</param>
        /// <param name="_comnumber">评论总数</param>
        /// <param name="_brownumber">浏览总数</param>
        /// <param name="_summary">简介信息</param>
        /// <param name="_tbimgpath">缩略图url</param>
        public EssayInfo(string _strtitle, string _straddress, string _strtag, int _comnumber, int _brownumber, string _summary, string _tbimgpath)
        {
            _Title = _strtitle;
            _Address = _straddress;
            _Tag = _strtag;
            _CommentaryNumber = _comnumber;
            _BrowseNumber = _brownumber;
            _Summary = _summary;
            _Thumbnails = _tbimgpath;
        }

        /// <summary>
        /// 从HTML5中格式化出文章信息
        /// </summary>
        /// <param name="_FormatHTMLContent">需要格式化的HTML内容</param>
        public EssayInfo(string _FormatHTMLContent)
        {
            formatesay(_FormatHTMLContent);
        }
        private void formatesay(string _FormatHTMLContent)
        {
            Regex reg = new Regex(@"<a\s*[^>]*>([\s\S]+?)</a>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
            MatchCollection mc = reg.Matches(_FormatHTMLContent);


            MatchCollection mcitem1 = null;
            MatchCollection mcitemimg = null;
            for (int i = 0; i < mc.Count; i++)
            {
                #region 缩略图、标题、详情地址、评论数量 解析
                reg = new Regex(@"(?is)<a[^>]*?href=(['""]?)(?<url>[^'""\s>]+)\1[^>]*>(?<text>(?:(?!</?a\b).)*)</a>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                mcitem1 = reg.Matches(mc[i].ToString());
                if (i == 0)
                {
                    _Thumbnails = mcitem1[0].Groups["text"].Value;
                    reg = new Regex(@"(?i)<img[^>]*?\ssrc\s*=\s*(['""]?)(?<src>[^'""\s>]+)\1[^>]*>");
                    mcitemimg = reg.Matches(_Thumbnails);
                    foreach (Match m in mcitemimg)
                    {
                        _Thumbnails = m.Groups["src"].Value;
                    }
                }
                else
                {
                    if (i == 1)
                    {
                        _Address = mcitem1[0].Groups["url"].Value;//得到href值  
                        _Title = mcitem1[0].Groups["text"].Value;
                    }
                    else if (i == 2)
                    {
                        _Tag = mcitem1[0].Groups["text"].Value;
                    }
                    else if (i == 4)
                    {
                        try
                        {
                            _CommentaryNumber = Convert.ToInt32(mcitem1[0].Groups["text"].Value);
                        }
                        catch (Exception ex)
                        { }
                    }
                }
                #endregion
            }

            try
            {
                _Summary = _FormatHTMLContent.Substring(_FormatHTMLContent.LastIndexOf("<p>") + 3);
                _Summary = _Summary.Substring(0, _Summary.IndexOf("</p>"));
                //_Summary = _Summary.Substring(0, _Summary.LastIndexOf("</p>"));

                //reg = new Regex(@"</p><p>.*?</p></div></div>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                //mc = reg.Matches(_FormatHTMLContent);
                //_Summary = mc[0].Groups[0].Value.Replace("</p><p>", "").Replace("</p></div></div>", "").Trim();
            }
            catch (Exception ex)
            {

            }
            _Summary = "    " + _Summary;
        }

        /// <summary>
        /// 首页最新文章列表格式化
        /// </summary>
        /// <param name="_FormatHTMLContent">需要格式化的文章内容</param>
        /// <param name="IsFirsList">是否为首页文章列表</param>
        public EssayInfo(string _FormatHTMLContent, bool IsFirsList)
        {
            if (IsFirsList)
            {
                Regex reg = new Regex(@"<a\s*[^>]*>([\s\S]+?)</a>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                MatchCollection mc = reg.Matches(_FormatHTMLContent);


                MatchCollection mcitem1 = null;
                MatchCollection mcitemimg = null;
                for (int i = 0; i < mc.Count; i++)
                {
                    #region 缩略图、标题、详情地址、评论数量 解析
                    reg = new Regex(@"(?is)<a[^>]*?href=(['""]?)(?<url>[^'""\s>]+)\1[^>]*>(?<text>(?:(?!</?a\b).)*)</a>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                    mcitem1 = reg.Matches(mc[i].ToString());
                    if (i == 0)
                    {
                        _Thumbnails = mcitem1[0].Groups["text"].Value;
                        reg = new Regex(@"(?i)<img[^>]*?\ssrc\s*=\s*(['""]?)(?<src>[^'""\s>]+)\1[^>]*>");
                        mcitemimg = reg.Matches(_Thumbnails);
                        foreach (Match m in mcitemimg)
                        {
                            _Thumbnails = m.Groups["src"].Value;
                        }
                    }
                    else
                    {
                        if (i == 1)
                        {
                            _Address = mcitem1[0].Groups["url"].Value;//得到href值  
                            _Title = mcitem1[0].Groups["text"].Value;
                        }
                        else if (i == 2)
                        {
                            _Tag = mcitem1[0].Groups["text"].Value;
                        }
                    }
                    #endregion
                }

                try
                {
                    reg = new Regex(@"<p>(\d\d\d\d年\d\d月\d\d日)? 标签：", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                    mc = reg.Matches(_FormatHTMLContent);
                    _PubDate = mc[0].Groups[0].Value.Replace("<p>", "").Replace(" 标签：", "").Trim();

                    reg = new Regex(@"</p><p>.*?</p>", RegexOptions.IgnoreCase | RegexOptions.Singleline);
                    mc = reg.Matches(_FormatHTMLContent);
                    _Summary = mc[0].Groups[0].Value.Replace("</p>", "").Replace("<p>", "").Replace("</div></div>", "").Trim();
                }
                catch (Exception ex)
                {

                }
                _Summary = "    " + _Summary;
            }
            else
            {
                formatesay(_FormatHTMLContent);
            }
        }

        /// <summary>
        /// 标题
        /// </summary>
        private string _Title;
        public string Title
        {
            get { return _Title; }
            set{_Title = value;}
        }
        private string _Address;
        /// <summary>
        /// 文章内容地址
        /// </summary>
        public string Address
        {
            get { return _Address; }
            set{ _Address = value;}
        }

        /// <summary>
        ///文章标识
        /// </summary>
        public string Key
        {
            get 
            {
               return _Address.Substring(_Address.LastIndexOf("/")+1, (_Address.LastIndexOf(".") -_Address.LastIndexOf("/"))-1);
                //return _Key;
            }
        }

        /// <summary>
        /// 标签
        /// </summary>
        private string _Tag;
        public string Tag
        {
            get { return _Tag; }
            set { _Tag = value; }
        }
        /// <summary>
        /// 评论
        /// </summary>
        private int _CommentaryNumber;
        public int CommentaryNumber
        {
            get { return _CommentaryNumber; }
            set{  _CommentaryNumber = value;}
        }
        /// <summary>
        /// 浏览总数
        /// </summary>
        private int _BrowseNumber;
        public int BrowseNumber
        {
            get { return _BrowseNumber; }
            set{ _BrowseNumber = value;}
        }
        /// <summary>
        /// 简介
        /// </summary>
        private string _Summary;
        /// <summary>
        /// 简介
        /// </summary>
        public string Summary
        {
            get { return _Summary; }
            set{_Summary = value;}
        }
        /// <summary>
        /// 缩略图
        /// </summary>
        private string _Thumbnails;
        /// <summary>
        /// 缩略图
        /// </summary>
        public string Thumbnails
        {
            get { return _Thumbnails; }
            set { _Thumbnails = value;}
        }
        private string _ThumbnailsData;
        /// <summary>
        /// 缩略图数据
        /// </summary>
        public string ThumbnailsData
        {
            get { return _ThumbnailsData; }
            set { _ThumbnailsData = value; }
        }

        private string _PubDate;
        /// <summary>
        /// 发布日期
        /// </summary>
        public string PubDate
        {
            get { return _PubDate; }
            set { _PubDate = value; }
        }
        private string _EssayDetail;
        /// <summary>
        /// 文章详情
        /// </summary>
        public string EssayDetail
        {
            get { return _EssayDetail; }
            set { _EssayDetail = value; }
        }
    }
 
}
