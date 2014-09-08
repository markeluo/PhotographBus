using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;

namespace SYS
{
    public partial class ListPage 
    {
        //private string LinkURL = "";
        //private string TagName = "";
        //protected  void OnNavigatedTo()
        //{
        //    LoadEssayLIst(LinkURL);
        //}
        //private void LoadEssayLIst(string LinkURL)
        //{

        //    //httphelper httpmanager = new httphelper();
        //    //httpmanager.httpendevent += httpmanager_httpendevent;
        //    //httpmanager.getservicedatapost(linkurl,1);
        //}

        //void httpmanager_HttpEndEvent(string _Data,int _page)
        //{
        //    if (!string.IsNullOrEmpty(_Data))
        //    {
        //        EssayClass _class = PageParserManager.Instance.EssayClassParser(_Data);

        //        //Dispatcher.BeginInvoke(() =>
        //        //{
        //        //    lstData.ItemsSource = _class.DataCollection;
        //        //    ProBarOne.Visibility = Visibility.Collapsed;
        //        //});
        //    }
        //}

        ///// <summary>
        ///// 选中项发生更改
        ///// </summary>
        ///// <param name="sender"></param>
        ///// <param name="e"></param>
        //private void lstData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        //{
        //    if (e.AddedItems.Count>0 && e.AddedItems[0] is EssayInfo)
        //    {
        //        EssayInfo _SelEssay = (EssayInfo)e.AddedItems[0];
        //        this.NavigationService.Navigate(new Uri("/DetailInfo.xaml?URL=" + _SelEssay.Address, UriKind.Relative));
        //    }
        //}
    }
}