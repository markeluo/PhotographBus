using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ScanInfoDownLoad
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBox1.SelectedIndex = 0;
        }

        private void InitDBConfig()
        {
            string strdir = System.Environment.CurrentDirectory;
            if (checkBox1.Checked)
            {
                DBConfigControl.InitializationSQLiteDB.Instance.SaveDbconfig(strdir, true);
            }
            else
            {
                DBConfigControl.InitializationSQLiteDB.Instance.SaveDbconfig(strdir);
            }
        }
        
        #region 列表处理
        string strurl = "";
        int ListType = 0;
        private void button1_Click(object sender, EventArgs e)
        {
            comboBox1.Enabled = false;
            button1.Enabled = false;

            InitDBConfig();
            strurl = comboBox1.SelectedItem.ToString();
            ListType=comboBox1.SelectedIndex+1;
            DownloadList(strurl, 1, ListType);
        }
        private void DownloadList(string url, int page,int type) 
        {
            HttpHelper httpmanager = new HttpHelper();
            httpmanager.HttpEndEvent += httpmanager_HttpEndEvent;
            httpmanager.GetServiceDataPost(url, page, type);
        }

        void httpmanager_HttpEndEvent(string _Data,int _page,int _type)
        {
            if (_Data != null && _Data != "")
            {
                EssayClass _class = PageParserManager.Instance.EssayClassParser(_Data);
                _class.ThisListType = _type;
                EssayInfoManager manager = new EssayInfoManager();
                manager.SaveLogs(_class);
                this.UIInvoke(() =>
                {
                    lblTipsInfo.Text = string.Format("第{0}页下载结束！",_page);
                });
                DownloadList(strurl, _page + 1, _type);
            }
            else
            {
                this.UIInvoke(() =>
                {
                    comboBox1.Enabled = true;
                    button1.Enabled = true;
                    lblTipsInfo.Text = "获取列表结束！";
                });
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            strurl = comboBox1.SelectedItem.ToString();
            ListType = comboBox1.SelectedIndex + 1;
        }
        #endregion

        #region 文章详情处理
        private int detailpage = 1;
        private int detailSize =20;
        private void button2_Click(object sender, EventArgs e)
        {
            InitDBConfig();
            comboBox1.Enabled = false;
            button1.Enabled = false;

            DownloadList(detailpage, detailSize);

        }


        List<EssayInfo> list = null;
        EssayInfo _essayitem = null;
        private int _succedcount = 0;
        private void DownloadList(int page,int pagesize)
        {
            _succedcount = 0;
            EssayInfoManager manager = new EssayInfoManager();
            list = manager.GetListNoDetail(page, pagesize);
            if (list != null && list.Count > 0)
            {
                EssayInfoDetailManager detailmanager = null;
                for (var i = 0; i < list.Count; i++)
                {
     
                    detailmanager = new EssayInfoDetailManager(list[i]);
                    detailmanager.DetailLoadEvent += detailmanager_DetailLoadEvent;
                    detailmanager.Load();
                    System.Threading.Thread.Sleep(500);
                }
            }
            else
            {
                this.UIInvoke(() =>
                {
                    comboBox1.Enabled = true;
                    button1.Enabled = true;
                    lblTipsInfo.Text = "获取列表结束！";
                });
            }
        }

        void detailmanager_DetailLoadEvent(EssayInfoDetail _detail)
        {
            if (_detail != null && _detail.EDetail != "")
            {
                EssayInfoManager manager = new EssayInfoManager();
                manager.SaveDetail(_detail);
            }

            _succedcount++;
            this.UIInvoke(() =>
            {
                lblTipsInfo.Text = string.Format("已成功获取并存储{0}个文章详情,当前第{1}页！", ((detailpage - 1) * detailSize + _succedcount), detailpage);
            });
            if (_succedcount == list.Count)
            {
                detailpage++;
                DownloadList(detailpage,detailSize);
            }
            else
            {
            }
        }

     
        #endregion

        private void button3_Click(object sender, EventArgs e)
        {
            comboBox1.Enabled = false;
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;

            ImagesDownloadManager manager = new ImagesDownloadManager();
            manager.UpdateMsgEvent += manager_UpdateMsgEvent;
            manager.UpdateStateChangeEvent += manager_UpdateStateChangeEvent;
            manager.DownloadMiniImages();
        }

        void manager_UpdateStateChangeEvent(bool IsEnd, int ErrorCount)
        {
            this.UIInvoke(() =>
            {
                lblTipsInfo.Text = string.Format("下载缩略图结束，共{0}个错误！", ErrorCount);
                comboBox1.Enabled = false;
                button1.Enabled = false;
                button2.Enabled = false;
                button3.Enabled =true;
                lblTipsInfo.ForeColor = Color.Green;
            });

        }

        void manager_UpdateMsgEvent(int number, bool state)
        {
            this.UIInvoke(() =>
            {
                string strmsg = string.Format("第{0}缩略图", number);
                if (state)
                {
                    strmsg += "下载成功!";
                    lblTipsInfo.ForeColor = Color.Green;
                }
                else
                {
                    strmsg += "下载失败！";
                    lblTipsInfo.ForeColor = Color.Red;
                }
                lblTipsInfo.Text = strmsg;

            });
        }

        #region 详情图片解析处理
        private void button4_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;

            DetailImgsmanager imgparsemanager = new DetailImgsmanager();
            imgparsemanager.StateChangeEvent += imgparsemanager_StateChangeEvent;
            imgparsemanager.StartParse();
        }

        void imgparsemanager_StateChangeEvent(int sumnumber,int errornumber, bool bolsucced, bool isEnd)
        {
            this.UIInvoke(() =>
            {
                if (isEnd)
                {
                    button4.Enabled = true;
                    lblTipsInfo.Text = string.Format("所有文章详情中图片解析成功,共{0}个,错误总计{1}个！", sumnumber, errornumber);
                    lblTipsInfo.ForeColor = Color.Green;
                }
                else
                {
                    lblTipsInfo.Text = string.Format("第{0}个已解析,错误总计{1}个", sumnumber, errornumber);
                    if (bolsucced)
                    {
                        lblTipsInfo.Text += "，已成功!";
                        lblTipsInfo.ForeColor = Color.Green;
                    }
                    else
                    {
                        lblTipsInfo.Text += "，已失败!";
                        lblTipsInfo.ForeColor = Color.Red;
                    }
     
                }

            });
        }
        #endregion

        #region 详情图片下载
        private void button5_Click(object sender, EventArgs e)
        {
            button1.Enabled = false;
            button2.Enabled = false;
            button3.Enabled = false;
            button4.Enabled = false;
            button5.Enabled = false;

            DetailImgsmanager imgparsemanager = new DetailImgsmanager();
            imgparsemanager.StateChangeEvent += imgparsemanager_StateChangeEvent2;
            imgparsemanager.StartDownload();
        }
        void imgparsemanager_StateChangeEvent2(int sumnumber, int errornumber, bool bolsucced, bool isEnd)
        {
            this.UIInvoke(() =>
            {
                if (isEnd)
                {
                    button1.Enabled = false;
                    button2.Enabled = true;
                    button3.Enabled = true;
                    button4.Enabled = true;
                    button5.Enabled = true;
                    lblTipsInfo.Text = string.Format("所有文章详情中图片下载成功,共{0}个,错误{1}个！", sumnumber,errornumber);
                    lblTipsInfo.ForeColor = Color.Green;
                }
                else
                {
                    lblTipsInfo.Text = string.Format("第{0}个已下载,错误总计{1}", sumnumber,errornumber);
                    if (bolsucced)
                    {
                        lblTipsInfo.Text += "，已成功!";
                        lblTipsInfo.ForeColor = Color.Green;
                    }
                    else
                    {
                        lblTipsInfo.Text += "，已失败!";
                        lblTipsInfo.ForeColor = Color.Red;
                    }

                }

            });
        }
        #endregion
    }
 
}
