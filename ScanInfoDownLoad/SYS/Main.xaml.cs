using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;

namespace SYS
{
    public  class Main 
    {

        //private int SumCount = 9;
        //private int ThisEndCount = 0;
        //private List<EssayClassStateInfo> ThisAllList = new List<EssayClassStateInfo>();


        ///// <summary>
        /////加载列表
        ///// </summary>
        ///// <param name="_ClasList"></param>
        ///// <param name="IsReload">是否重新加载</param>
        //private void LoadAllList(List<EssayClass> _ClasList,bool IsReload)
        //{
        //    ProBarOne.Visibility = Visibility.Visible;
        //    if (_ClasList != null && _ClasList.Count > 0)
        //    {
        //        string URL = "http://www.fsbus.com/";
        //        HttpHelper httpmanager = null;
        //        foreach (EssayClass _classitem in _ClasList)
        //        {
        //            if (_classitem.ThisListType == 0)
        //            {
        //                LoadLocalFavorites(_classitem);

        //                AddLoadedEssayClassStateInfo(new EssayClassStateInfo(_classitem, true, true));
        //                IsALlLoadEnd();
        //                continue;
        //            }
        //            else
        //            {
        //                if (_classitem.DataCollection != null && _classitem.DataCollection.Count > 0)
        //                {
        //                    //维护集合
        //                    AddLoadedEssayClassStateInfo(new EssayClassStateInfo(_classitem, true, true));
        //                    Dispatcher.BeginInvoke(() =>
        //                    {
        //                        lstDataTips.Visibility = Visibility.Collapsed;
        //                        switch (_classitem.ThisListType)
        //                        {
        //                            #region 已缓存数据，直接加载
        //                            case 1:
        //                                lstData.ItemsSource = _classitem.DataCollection;
        //                                break;
        //                            case 2:
        //                                lstSYJQData.ItemsSource = _classitem.DataCollection;
        //                                break;
        //                            case 3:
        //                                lstSYJCData.ItemsSource = _classitem.DataCollection;
        //                                break;
        //                            case 4:
        //                                lstRXSYData.ItemsSource = _classitem.DataCollection;
        //                                break;
        //                            case 5:
        //                                lstHSSYData.ItemsSource = _classitem.DataCollection;
        //                                break;
        //                            case 6:
        //                                lstETSYData.ItemsSource = _classitem.DataCollection;
        //                                break;
        //                            case 7:
        //                                lstSYQCData.ItemsSource = _classitem.DataCollection;
        //                                break;
        //                            case 8:
        //                                lstSYHQData.ItemsSource = _classitem.DataCollection;
        //                                break;
        //                            case 9:
        //                                lstDFRMData.ItemsSource = _classitem.DataCollection;
        //                                break;
        //                            #endregion
        //                        }
        //                        IsALlLoadEnd();
        //                    });
        //                }
        //                else
        //                {
        //                    switch (_classitem.ThisListType)
        //                    {
        //                        #region 判断类型，新下载
        //                        case 1:
        //                            {
        //                                URL = "http://www.fsbus.com/";
        //                                httpmanager = new HttpHelper();
        //                                httpmanager.HttpEndEvent += httpmanager_HttpEndEvent;
        //                                httpmanager.GetServiceDataPost(URL);
        //                            }
        //                            break;
        //                        case 2:
        //                            URL = "http://www.fsbus.com/sheyingjiqiao/";
        //                            httpmanager = new HttpHelper();
        //                            httpmanager.HttpEndEvent += httpmanager2_HttpEndEvent;
        //                            httpmanager.GetServiceDataPost(URL);
        //                            break;
        //                        case 3:
        //                            URL = "http://www.fsbus.com/sheyingjiaocheng/";
        //                            httpmanager = new HttpHelper();
        //                            httpmanager.HttpEndEvent += httpmanager3_HttpEndEvent;
        //                            httpmanager.GetServiceDataPost(URL);
        //                            break;
        //                        case 4:
        //                            URL = "http://www.fsbus.com/renxiangsheying/";
        //                            httpmanager = new HttpHelper();
        //                            httpmanager.HttpEndEvent += httpmanager4_HttpEndEvent;
        //                            httpmanager.GetServiceDataPost(URL);
        //                            break;
        //                        case 5:
        //                            URL = "http://www.fsbus.com/hunshasheying/";
        //                            httpmanager = new HttpHelper();
        //                            httpmanager.HttpEndEvent += httpmanager5_HttpEndEvent;
        //                            httpmanager.GetServiceDataPost(URL);
        //                            break;
        //                        case 6:
        //                            URL = "http://www.fsbus.com/ertongsheying/";
        //                            httpmanager = new HttpHelper();
        //                            httpmanager.HttpEndEvent += httpmanager6_HttpEndEvent;
        //                            httpmanager.GetServiceDataPost(URL);
        //                            break;
        //                        case 7:
        //                            URL = "http://www.fsbus.com/sheyingqicai/";
        //                            httpmanager = new HttpHelper();
        //                            httpmanager.HttpEndEvent += httpmanager7_HttpEndEvent;
        //                            httpmanager.GetServiceDataPost(URL);
        //                            break;
        //                        case 8:
        //                            URL = "http://www.fsbus.com/sheyinghouqi/";
        //                            httpmanager = new HttpHelper();
        //                            httpmanager.HttpEndEvent += httpmanager8_HttpEndEvent;
        //                            httpmanager.GetServiceDataPost(URL);
        //                            break;
        //                        case 9:
        //                            URL = "http://www.fsbus.com/danfanrumen/";
        //                            httpmanager = new HttpHelper();
        //                            httpmanager.HttpEndEvent += httpmanager9_HttpEndEvent;
        //                            httpmanager.GetServiceDataPost(URL);
        //                            break;
        //                        #endregion
        //                    }
        //                }
        //            }
        //        }
        //    }
        //}

        /////// <summary>
        /////// 页面加载结束后
        /////// </summary>
        /////// <param name="sender"></param>
        /////// <param name="e"></param>
        ////private void PhoneApplicationPage_Loaded(object sender, RoutedEventArgs e)
        ////{
        ////    //缓存列表
        ////    List<EssayClass> CacheList = null;
        ////    try
        ////    {
        ////        if (ThisAllList != null && ThisAllList.Count == 10)
        ////        {
        ////            ThisEndCount = SumCount;
        ////            ProBarOne.Visibility = Visibility.Collapsed;
        ////        }
        ////        else
        ////        {
        ////            ThisEndCount = 0;
        ////            CacheList = LocalStorageManager.Instance.EssayClassGetAll();
        ////            LoadAllList(CacheList, false);
        ////        }
        ////    }
        ////    catch (Exception ex)
        ////    {
        ////    }

        ////    CommonMSGManager.Instance.UpdateFavoriteEvent += Instance_UpdateFavoriteEvent;
        ////}
 
        //#region 数据绑定处理

        ///// <summary>
        ///// 加载本地收藏
        ///// </summary>
        ///// <param name="_classinfo"></param>
        //private void LoadLocalFavorites( EssayClass _classinfo)
        //{
        //    if (_classinfo.DataCollection != null && _classinfo.DataCollection.Count > 0)
        //    {
        //        lstFactData.ItemsSource = _classinfo.DataCollection;
        //        LocalFavoriteTips.Visibility = Visibility.Collapsed;
        //    }
        //    else 
        //    {
        //        lstFactData.ItemsSource = _classinfo.DataCollection;
        //        LocalFavoriteTips.Text = "您还未添加任何收藏记录!";
        //        LocalFavoriteTips.Visibility = Visibility.Visible;
        //    }
        //}

        //void httpmanager_HttpEndEvent(string _Data)
        //{
        //    if (!string.IsNullOrEmpty(_Data))
        //    {

        //        EssayClass _class = PageParserManager.Instance.NewEssayParser(_Data);
        //        _class.ThisListType = 1;
        //        AddLoadedEssayClassStateInfo(new EssayClassStateInfo(_class, true, false));
 
        //        Dispatcher.BeginInvoke(() =>
        //        {
        //            lstDataTips.Visibility = Visibility.Collapsed;
        //            lstData.ItemsSource = _class.DataCollection;
        //            IsALlLoadEnd();
        //        });

        //    }
        //    else
        //    {
        //        Dispatcher.BeginInvoke(() =>
        //       {
        //           lstDataTips.Visibility = Visibility.Visible;
        //           lstDataTips.Text = "列表加载失败!";
        //           IsALlLoadEnd();
        //       });
        //    }
        //}
        //void httpmanager2_HttpEndEvent(string _Data)
        //{
        //    if (!string.IsNullOrEmpty(_Data))
        //    {

        //        EssayClass _class = PageParserManager.Instance.EssayClassParser(_Data);
        //        _class.ThisListType = 2;
        //        AddLoadedEssayClassStateInfo(new EssayClassStateInfo(_class, true, false));

        //        Dispatcher.BeginInvoke(() =>
        //        {
        //            lstSYJQData.ItemsSource = _class.DataCollection;
        //            lstSYJQDataTips.Visibility = Visibility.Collapsed;
        //            IsALlLoadEnd();
        //        });
        //    }
        //    else
        //    {
        //        Dispatcher.BeginInvoke(() =>
        //        {
        //            lstSYJQDataTips.Visibility = Visibility.Visible;
        //            lstSYJQDataTips.Text = "列表加载失败!";
        //            IsALlLoadEnd();
        //        });
        //    }
        //}
        //void httpmanager3_HttpEndEvent(string _Data)
        //{
        //    if (!string.IsNullOrEmpty(_Data))
        //    {

        //        EssayClass _class = PageParserManager.Instance.EssayClassParser(_Data);
        //        _class.ThisListType = 3;
        //        AddLoadedEssayClassStateInfo(new EssayClassStateInfo(_class, true, false));

        //        Dispatcher.BeginInvoke(() =>
        //        {
        //            lstSYJCData.ItemsSource = _class.DataCollection;
        //            lstSYJCDataTips.Visibility = Visibility.Collapsed;
        //            IsALlLoadEnd();
        //        });
        //    }
        //    else
        //    {
        //            Dispatcher.BeginInvoke(() =>
        //        {
        //            lstSYJCDataTips.Visibility = Visibility.Visible;
        //            lstSYJCDataTips.Text = "列表加载失败!";
        //            IsALlLoadEnd();
        //        });
        //    }
        //}
        //void httpmanager4_HttpEndEvent(string _Data)
        //{
        //    if (!string.IsNullOrEmpty(_Data))
        //    {

        //        EssayClass _class = PageParserManager.Instance.EssayClassParser(_Data);
        //        _class.ThisListType = 4;
        //        AddLoadedEssayClassStateInfo(new EssayClassStateInfo(_class, true, false));

        //        Dispatcher.BeginInvoke(() =>
        //        {
        //            lstRXSYData.ItemsSource = _class.DataCollection;
        //            lstRXSYDataTips.Visibility = Visibility.Collapsed;
        //            IsALlLoadEnd();
        //        });
        //    }
        //    else
        //    {
        //            Dispatcher.BeginInvoke(() =>
        //        {
        //            lstRXSYDataTips.Visibility = Visibility.Visible;
        //            lstRXSYDataTips.Text = "列表加载失败!";
        //            IsALlLoadEnd();
        //        });
        //    }
        //}
        //void httpmanager5_HttpEndEvent(string _Data)
        //{
        //    if (!string.IsNullOrEmpty(_Data))
        //    {

        //        EssayClass _class = PageParserManager.Instance.EssayClassParser(_Data);
        //        _class.ThisListType = 5;
        //        AddLoadedEssayClassStateInfo(new EssayClassStateInfo(_class, true, false));

        //        Dispatcher.BeginInvoke(() =>
        //        {
        //            lstHSSYData.ItemsSource = _class.DataCollection;
        //            lstHSSYDataTips.Visibility = Visibility.Collapsed;
        //            IsALlLoadEnd();
        //        });
        //    }
        //    else
        //    {
        //            Dispatcher.BeginInvoke(() =>
        //        {
        //            lstHSSYDataTips.Visibility = Visibility.Visible;
        //            lstHSSYDataTips.Text = "列表加载失败!";
        //            IsALlLoadEnd();
        //        });
        //    }
        //}
        //void httpmanager6_HttpEndEvent(string _Data)
        //{
        //    if (!string.IsNullOrEmpty(_Data))
        //    {

        //        EssayClass _class = PageParserManager.Instance.EssayClassParser(_Data);
        //        _class.ThisListType = 6;
        //        AddLoadedEssayClassStateInfo(new EssayClassStateInfo(_class, true, false));
        //        Dispatcher.BeginInvoke(() =>
        //        {
        //            lstETSYData.ItemsSource = _class.DataCollection;
        //            lstETSYDataTips.Visibility = Visibility.Collapsed;
        //            IsALlLoadEnd();
        //        });
        //    }
        //    else
        //    {
        //            Dispatcher.BeginInvoke(() =>
        //        {
        //            lstETSYDataTips.Visibility = Visibility.Visible;
        //            lstETSYDataTips.Text = "列表加载失败!";
        //            IsALlLoadEnd();
        //        });
        //    }
        //}
        //void httpmanager7_HttpEndEvent(string _Data)
        //{
        //    if (!string.IsNullOrEmpty(_Data))
        //    {

        //        EssayClass _class = PageParserManager.Instance.EssayClassParser(_Data);
        //        _class.ThisListType = 7;
        //        AddLoadedEssayClassStateInfo(new EssayClassStateInfo(_class, true, false));

        //        Dispatcher.BeginInvoke(() =>
        //        {
        //            lstSYQCData.ItemsSource = _class.DataCollection;
        //            lstSYQCDataTips.Visibility = Visibility.Collapsed;
        //            IsALlLoadEnd();
        //        });
        //    }
        //    else
        //    {
        //            Dispatcher.BeginInvoke(() =>
        //        {
        //            lstSYQCDataTips.Visibility = Visibility.Visible;
        //            lstSYQCDataTips.Text = "列表加载失败!";
        //            IsALlLoadEnd();
        //        });
        //    }
        //}
        //void httpmanager8_HttpEndEvent(string _Data)
        //{
        //    if (!string.IsNullOrEmpty(_Data))
        //    {

        //        EssayClass _class = PageParserManager.Instance.EssayClassParser(_Data);
        //        _class.ThisListType = 8;
        //        AddLoadedEssayClassStateInfo(new EssayClassStateInfo(_class, true, false));

        //        Dispatcher.BeginInvoke(() =>
        //        {
        //            lstSYHQData.ItemsSource = _class.DataCollection;
        //            lstSYHQDataTips.Visibility = Visibility.Collapsed;
        //            IsALlLoadEnd();
        //        });
        //    }
        //    else
        //    {
        //            Dispatcher.BeginInvoke(() =>
        //        {
        //            lstSYHQDataTips.Visibility = Visibility.Visible;
        //            lstSYHQDataTips.Text = "列表加载失败!";
        //            IsALlLoadEnd();
        //        });
        //    }
        //}
        //void httpmanager9_HttpEndEvent(string _Data)
        //{
        //    if (!string.IsNullOrEmpty(_Data))
        //    {

        //        EssayClass _class = PageParserManager.Instance.EssayClassParser(_Data);
        //        _class.ThisListType = 9;
        //        AddLoadedEssayClassStateInfo(new EssayClassStateInfo(_class, true, false));

        //        Dispatcher.BeginInvoke(() =>
        //        {
        //            lstDFRMData.ItemsSource = _class.DataCollection;
        //            lstDFRMDataTips.Visibility = Visibility.Collapsed;
        //            IsALlLoadEnd();
        //        });
        //    }
        //    else
        //    {
        //            Dispatcher.BeginInvoke(() =>
        //        {
        //            lstDFRMDataTips.Visibility = Visibility.Visible;
        //            lstDFRMDataTips.Text = "列表加载失败!";
        //            IsALlLoadEnd();
        //        });
        //    }
        //}
        //#endregion

        //private void IsALlLoadEnd()
        //{
        //    ThisEndCount++;
        //    if (ThisEndCount >= SumCount)
        //    {
        //        ProBarOne.Visibility = Visibility.Collapsed;

        //        //更新缓存
        //        CacheUpdateManager manager = new CacheUpdateManager(ThisAllList);
        //        manager.CacheUpdateEndEvent += manager_CacheUpdateEndEvent;
        //        manager.StartUpdate();
        //    }
        //}

        //void manager_CacheUpdateEndEvent(bool bolstate, string strmsg)
        //{
            
        //}


        ///// <summary>
        ///// 选中项发生更改
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void lstData_SelectionChanged(object sender, object e)
        //{
        //    //if (e.AddedItems.Count > 0)
        //    //{
        //    //    EssayInfo _SelEssay = (EssayInfo)e.AddedItems[0];
        //    //    this.NavigationService.Navigate(new Uri(string.Format("/DetailInfo.xaml?URL={0}&TL={1}&SM={2}", _SelEssay.Address, _SelEssay.Title,_SelEssay.Summary), UriKind.Relative));
        //    //}
        //}

        //#region 底部菜单栏处理
        ///// <summary>
        ///// 清空收藏
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void ApplicationBarIconButton_Click_1(object sender, EventArgs e)
        //{
        //     MessageBoxResult msgRst = MessageBox.Show("您确定要清除所有收藏记录吗？", "提示",MessageBoxButton.OKCancel);
        //     if (msgRst == MessageBoxResult.OK)
        //     {
        //         bool _bolsucced = LocalStorageManager.Instance.ClearALL();
        //         if (_bolsucced)
        //         {
        //             CommonMSGManager.Instance.RefreshFavoritesShow();
        //             MessageBox.Show("清除成功!");
        //         }
        //         else
        //         {
        //             MessageBox.Show("清除失败!");
        //         }
        //     } 
        //}

        /////// <summary>
        /////// 点击事件
        /////// </summary>
        /////// <param name="sender"></param>
        /////// <param name="e"></param>
        ////private void lstFactData_Tap(object sender, System.Windows.Input.GestureEventArgs e)
        ////{
        ////    if (e.OriginalSource is System.Windows.Controls.Border)
        ////    {
        ////        Object obj = ((System.Windows.Controls.Border)e.OriginalSource).DataContext;
        ////        if (obj is SYS.EssayInfo)
        ////        {
        ////            EssayInfo _SelEssay = (EssayInfo)obj;
        ////            this.NavigationService.Navigate(new Uri(string.Format("/DetailInfo.xaml?URL={0}&TL={1}&SM={2}", _SelEssay.Address, _SelEssay.Title, _SelEssay.Summary), UriKind.Relative));
        ////        }
        ////    }
        ////    else
        ////    {
        ////        if (e.OriginalSource is System.Windows.Controls.Image) 
        ////        {
        ////            if (((System.Windows.Controls.Image)e.OriginalSource).DataContext is SYS.EssayInfo)
        ////            {
        ////                EssayInfo _SelEssay = (EssayInfo)((System.Windows.Controls.Image)e.OriginalSource).DataContext;
        ////                this.NavigationService.Navigate(new Uri(string.Format("/DetailInfo.xaml?URL={0}&TL={1}&SM={2}", _SelEssay.Address, _SelEssay.Title, _SelEssay.Summary), UriKind.Relative));
        ////            }
        ////        }
        ////        if (e.OriginalSource is System.Windows.Controls.TextBlock)
        ////        {
        ////            if (((System.Windows.Controls.TextBlock)e.OriginalSource).DataContext is SYS.EssayInfo)
        ////            {
        ////                EssayInfo _SelEssay = (EssayInfo)((System.Windows.Controls.TextBlock)e.OriginalSource).DataContext;
        ////                this.NavigationService.Navigate(new Uri(string.Format("/DetailInfo.xaml?URL={0}&TL={1}&SM={2}", _SelEssay.Address, _SelEssay.Title, _SelEssay.Summary), UriKind.Relative));
        ////            }
        ////        }
        ////    }
        ////}

        ///// <summary>
        ///// 关于
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void ApplicationBarIconButton_Click_2(object sender, EventArgs e)
        //{
        //    this.NavigationService.Navigate(new Uri("/About.xaml", UriKind.Relative));
        //}
        ///// <summary>
        ///// 离线下载
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void ApplicationBarIconButton_Click(object sender, EventArgs e)
        //{
        //    this.NavigationService.Navigate(new Uri("/CacheDownload.xaml", UriKind.Relative));
        //}

        ///// <summary>
        ///// 数据更新
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void ApplicationBarIconButton_Click3(object sender, EventArgs e)
        //{
        //    ProBarOne.Visibility = Visibility.Visible;
        //    ThisEndCount = ThisEndCount-1;

        //    List<EssayClass> _ClasList = new List<EssayClass>();
        //    EssayClass _newitem = new EssayClass();
        //    _newitem.ThisListType = _pivotimeindex;
        //    _ClasList.Add(_newitem);
        //    LoadAllList(_ClasList,true);
        //}
        //#endregion

        //#region 相关控制方法
        ///// <summary>
        ///// 添加相关类别状态详情
        ///// </summary>
        ///// <param name="_stateinfo"></param>
        //public void AddLoadedEssayClassStateInfo(EssayClassStateInfo _stateinfo)
        //{ 
        //    if(ThisAllList==null)
        //    {
        //        ThisAllList = new List<EssayClassStateInfo>();
        //    }
        //    if (ThisAllList.Count > 0)
        //    {
        //        bool bolIsExt = false;
        //        foreach (EssayClassStateInfo item in ThisAllList)
        //        {
        //            if (item.Itemobj.ThisListType == _stateinfo.Itemobj.ThisListType)
        //            {
        //                item.Itemobj.DataCollection = _stateinfo.Itemobj.DataCollection;
        //                item.ChangeState(_stateinfo.IsLoaded);
        //                item.ChangeCache(_stateinfo.IsCache);
        //                bolIsExt = true;
        //                break;
        //            }
        //        }
        //        if (!bolIsExt)
        //        {
        //            ThisAllList.Add(_stateinfo);
        //        }
        //    }
        //    else
        //    {
        //        ThisAllList.Add(_stateinfo);
        //    }
        //}

        ///// <summary>
        ///// 收藏夹内容更新处理
        ///// </summary>
        ///// <param name="_obj"></param>
        //void Instance_UpdateFavoriteEvent(object _obj)
        //{
        //    EssayClass _FavtEssayClass = LocalStorageManager.Instance.EssayClassGetByType(0);
        //    LoadLocalFavorites(_FavtEssayClass);
        //}
        //#endregion

        //private int _pivotimeindex = 0;
        ///// <summary>
        ///// 文章类型切换
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void PivotItems_SelectionChanged(object sender, object e)
        //{
        //    _pivotimeindex=PivotItems.SelectedIndex;
        //}
    }
}