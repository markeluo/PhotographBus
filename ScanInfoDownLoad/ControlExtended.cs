using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace ScanInfoDownLoad
{
    //扩展项目下所有Control类，把线程操作Invoke提出来。不然像2.0哪样个个线程方法都得if else
    public static class ControlExtended
    {
        public delegate void InvokeHandler();
        /// <summary>
        /// 扩展项目下所有Control类，线程操作Invoke共用处理
        /// </summary>
        /// <param name="control"></param>
        /// <param name="handler"></param>
        public static void UIInvoke(this Control control, InvokeHandler handler)
        {
            if (control.InvokeRequired)//如果跨线程则使用委托调用方法
            {
                control.Invoke(handler);
            }
            else//否则直接调用方法
            {
                handler();
            }
        }
    }
}
