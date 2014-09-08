using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    /// <summary>
    /// 文章分类
    /// </summary>
    public class EssayClass
    {
        public EssayClass()
        { }

        public EssayClass(int _TolsNumber, int _ThisNumber, ObservableCollection<EssayInfo> _list)
        {
            _TolPages = _TolsNumber;
            _ThisPage = _ThisNumber;
            _EssayList = _list;
        }
        public EssayClass(int _TolsNumber, int _ThisNumber, int _Type,ObservableCollection<EssayInfo> _list)
        {
            _TolPages = _TolsNumber;
            _ThisPage = _ThisNumber;
            _EssayList = _list;
            _ThisListType = _Type;
        }

        private int _ThisListType;
         /// <summary>
         /// 列表类型
         /// </summary>
        public int ThisListType
        {
            get { return _ThisListType; }
            set { _ThisListType = value;}
        }

        private int _TolPages;
        /// <summary>
        /// 总页数
        /// </summary>
         public int TolPages
        {
            get { return _TolPages; }
            set { _TolPages = value;}
        }
        private int _ThisPage;
        /// <summary>
        /// 当前页
        /// </summary>
        public int ThisPage
        {
            get { return _ThisPage; }
            set
            {_ThisPage = value;}
        }
        private ObservableCollection<EssayInfo> _EssayList = new ObservableCollection<EssayInfo>();
        /// <summary>
        /// 当前页文章列表
        /// </summary>
        public ObservableCollection<EssayInfo> DataCollection
        {
            get { return _EssayList; }
            set {_EssayList = value;}
        }
        private int _ThisSuccedLoadCount;
        /// <summary>
        /// 成功加载子项的个数
        /// </summary>
        public int ThisSuccedLoadCount
        {
            get { return _ThisSuccedLoadCount; }
            set { _ThisSuccedLoadCount = value; }
        }
        private bool _IsLoaded = false;
        /// <summary>
        /// 是否加载过
        /// </summary>
        public bool IsLoaded
        {
            get { return _IsLoaded; }
            set { _IsLoaded = value; }
        }
    }

    /// <summary>
    ///文章分类状态信息
    /// </summary>
    public class EssayClassStateInfo
    {
        private EssayClass _Itemobj = null;

        /// <summary>
        ///文章分类信息
        /// </summary>
        public EssayClass Itemobj
        {
            get { return _Itemobj; }
            set { _Itemobj = value; }
        }
        private bool _isLoaded = false;
        private bool _isCache = false;

        /// <summary>
        /// 是否已缓存-只读
        /// </summary>
        public bool IsCache
        {
            get { return _isCache; }
        }
        /// <summary>
        /// 是否已加载--只读
        /// </summary>
        public bool IsLoaded
        {
            get { return _isLoaded; }
        }
        public EssayClassStateInfo()
        { }
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="_Class">类别</param>
        /// <param name="IsLoaded">是否已加载</param>
        /// <param name="IsCache">是否已缓存</param>
        public EssayClassStateInfo(EssayClass _Class, bool IsLoaded,bool IsCache)
        {
            _isLoaded = IsLoaded;
            _Itemobj = _Class;
            _isCache = IsCache;
        }
        /// <summary>
        /// 更新是否已加载状态
        /// </summary>
        /// <param name="_state"></param>
        public void ChangeState(bool _state)
        {
            _isLoaded = _state;
        }
        /// <summary>
        /// 更新是否已缓存
        /// </summary>
        /// <param name="bolCache"></param>
        public void ChangeCache(bool bolCache)
        {
            _isCache = bolCache;
        }
    }
}
