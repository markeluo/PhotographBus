<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="webservice._Default" %>

<asp:Content runat="server" ID="FeaturedContent" ContentPlaceHolderID="FeaturedContent">
    <section class="featured">
        <div class="content-wrapper">
            <hgroup class="title">
                <h1><%: Title %>.</h1>
                <h2>Modify this template to jump-start your ASP.NET application.</h2>
            </hgroup>
            <p>
                To learn more about ASP.NET, visit <a href="http://asp.net" title="ASP.NET Website">http://asp.net</a>.
                The page features <mark>videos, tutorials, and samples</mark> to help you get the most from ASP.NET.
                If you have any questions about ASP.NET visit
                <a href="http://forums.asp.net/18.aspx" title="ASP.NET Forum">our forums</a>.
            </p>
        </div>
    </section>
</asp:Content>
<asp:Content runat="server" ID="BodyContent" ContentPlaceHolderID="MainContent">
    <h3>HOT TAGS:</h3>
    <div id="taglist" class="round">
         
    </div>
     <!-- Load JavaScript Libraries -->
<script type="text/javascript" src="Content/metrolui/js/docs.js"></script>
<script type="text/javascript" src="Content/metrolui/js/datamanager.js"></script>
<script type="text/javascript">
    window.onload = function () {
        LoadTagClassData();//热门标签分类加载

    }
    //加载热门分类标签数据
    function LoadTagClassData() {
        //var strcontent = "<div class='tile double live' data-role='live-tile' data-effect=''>" +
        //"<div class='tile-content image'><img src='miniimg/00cefe6b156e4452a398d30d9eb8e5bd.jpg'></div>" +
        //"<div class='tile-content image'><img src='miniimg/0a3ea8927dc44377b88f857264ab5570.jpg'></div>" +
        //"<div class='tile-content image'><img src='miniimg/0ab2e1cf5fad49a8b984aab7e0d483b9.jpg'></div>" +
        //"<div class='tile-status bg-dark opacity'><span class='label'>摄影入门: 35</span></div>" +
        //"</div>";
        //$("#taglist").append(strcontent);
        var postpars = {
            MethodName: "GetHotTaglist_JSON",
            Paras: JSON.stringify({"topnumber":5}),
            CallBackFunction: function (result) {
                if (result.result == "true") {
                    $("#taglist").html("");
                    var strtagitem = "";
                    for (var i = 0; i < result.data.length; i++) {
                        strtagitem = "<div class='tile double live' data-role='live-tile' data-effect='' style='width:200px;' onclick='tagpanelclick(this)' data-value='" + result.data[i].Etag + "'>";
                        for (var j = 0; j < result.data[i].Images.length; j++) {
                            strtagitem += "<div class='tile-content image'><img key='" + result.data[i].Images[j].Key + "' src='" + result.data[i].Images[j].Path + "'></div>"
                        }
                        strtagitem += "<div class='tile-status bg-dark opacity'><span class='label fg-white'>" + result.data[i].Etag + "</span><span class='badge bg-cyan' style='right:0px;'>" + result.data[i].Sumnumber + "</span></div>"
                        $("#taglist").append(strtagitem);
                    }
                }
            }
        }
        datamanager.ReadData(postpars);
    }
    function tagpanelclick(obj) {
        $(obj).parent().find("div[data-role='live-tile']").removeClass("selected");
        $(obj).addClass("selected");

    }
</script>
</asp:Content>
