using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    /// <summary>
    /// 文章列表
    /// </summary>
    public class EssayInfoList:List<EssayInfo>
    {
        public EssayInfoList()
        {
            DataCollection = new ObservableCollection<EssayInfo>();
        }
        public void Add(EssayInfo _esyinfo)
        {
            DataCollection.Add(_esyinfo);
        }

        public ObservableCollection<EssayInfo> DataCollection
        {
            get;
            set;
        }
    }
}
