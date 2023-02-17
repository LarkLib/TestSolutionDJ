using System;
using System.Windows.Forms;

namespace DJ.LMS.Utilities
{
    /// <summary>
    /// 用来方便实现在多线程中处理中，对数据的绑定和赋值。
    /// 
    /// </summary>
    public class CallCtrlWithThreadSafety
    {
        private delegate void Delegate1(Control objCtrl, string text, Form winform);
        private delegate void Delegate2(ToolStripStatusLabel objCtrl, string text, Form winform);
        private delegate void Delegate3(Control objCtrl, bool enable, Form winform);
        private delegate void Delegate4(Control objCtrl, Form winform);
        private delegate void Delegate5(CheckBox objCtrl, bool isCheck, Form winform);
        /// <summary>    
        /// 设置控件的文本属性    
        /// </summary>    
        /// <typeparam name="TObject">控件对象类型</typeparam>    
        /// <param name="objCtrl">控件对象</param>    
        /// <param name="text">文本信息</param>    
        /// <param name="winform">所在窗体</param>
        public static void SetText<TObject>(TObject objCtrl, string text, Form winform) where TObject : Control
        {
            if (objCtrl.InvokeRequired)
            {
                CallCtrlWithThreadSafety.Delegate1 method = new CallCtrlWithThreadSafety.Delegate1(CallCtrlWithThreadSafety.SetText<Control>);
                if (!winform.IsDisposed)
                    winform.Invoke(method, new object[] { objCtrl, text, winform });
            }
            else
            {
                objCtrl.Text = text;
            }
        }
        /// <summary>    
        /// 设置控件的可用状态    
        /// </summary>    
        /// <typeparam name="TObject">控件对象类型</typeparam>    
        /// <param name="objCtrl">控件对象</param>    
        /// <param name="enable">控件是否可用</param>    
        /// <param name="winf">所在窗体</param> 
        public static void SetEnable<TObject>(TObject objCtrl, bool enable, Form winform) where TObject : Control
        {
            if (objCtrl.InvokeRequired)
            {
                CallCtrlWithThreadSafety.Delegate3 method = new CallCtrlWithThreadSafety.Delegate3(CallCtrlWithThreadSafety.SetEnable<Control>);
                if (!winform.IsDisposed)
                    winform.Invoke(method, new object[] { objCtrl, enable, winform });
            }
            else
            {
                objCtrl.Enabled = enable;
            }
        }
        /// <summary>    
        /// 设置控件的焦点定位    
        /// </summary>    
        /// <typeparam name="TObject">控件对象类型</typeparam>    
        /// <param name="objCtrl">控件对象</param>    
        /// <param name="winf">所在窗体</param>
        public static void SetFocus<TObject>(TObject objCtrl, Form winform) where TObject : Control
        {
            if (objCtrl.InvokeRequired)
            {
                CallCtrlWithThreadSafety.Delegate4 method = new CallCtrlWithThreadSafety.Delegate4(CallCtrlWithThreadSafety.SetFocus<Control>);
                if (!winform.IsDisposed)
                    winform.Invoke(method, new object[] { objCtrl, winform });
            }
            else
            {
                objCtrl.Focus();
            }
        }
        /// <summary>    
        /// 设置控件的选择状态    
        /// </summary>    
        /// <typeparam name="TObject">控件对象类型</typeparam>    
        /// <param name="objCtrl">控件对象</param>    
        /// <param name="isChecked">是否选择</param>    
        /// <param name="winf">所在窗体</param> 
        public static void SetChecked<TObject>(TObject objCtrl, bool isChecked, Form winform) where TObject : CheckBox
        {
            if (objCtrl.InvokeRequired)
            {
                CallCtrlWithThreadSafety.Delegate5 method = new CallCtrlWithThreadSafety.Delegate5(CallCtrlWithThreadSafety.SetChecked<CheckBox>);
                if (!winform.IsDisposed)
                    winform.Invoke(method, new object[] { objCtrl, isChecked, winform });
            }
            else
            {
                objCtrl.Checked = isChecked;
            }
        }
        /// <summary>    
        /// 设置控件的可见状态    
        /// </summary>    
        /// <typeparam name="TObject">控件对象类型</typeparam>    
        /// <param name="objCtrl">控件对象</param>    
        /// <param name="isVisible">是否可见</param>    
        /// <param name="winf">所在窗体</param> 
        public static void SetVisible<TObject>(TObject objCtrl, bool isVisible, Form winform) where TObject : Control
        {
            if (objCtrl.InvokeRequired)
            {
                CallCtrlWithThreadSafety.Delegate5 method = new CallCtrlWithThreadSafety.Delegate5(CallCtrlWithThreadSafety.SetChecked<CheckBox>);
                if (!winform.IsDisposed)
                    winform.Invoke(method, new object[] { objCtrl, isVisible, winform });
            }
            else
            {
                objCtrl.Visible = isVisible;
            }
        }
        /// <summary>    
        /// 设置工具状态条的文本内容    
        /// </summary>    
        /// <typeparam name="TObject">控件对象类型</typeparam>    
        /// <param name="objCtrl">控件对象</param>    
        /// <param name="text">文本信息</param>    
        /// <param name="winf">所在窗体</param>
        public static void SetText2<TObject>(TObject objCtrl, string text, Form winform) where TObject : ToolStripStatusLabel
        {
            if (objCtrl.Owner.InvokeRequired)
            {
                CallCtrlWithThreadSafety.Delegate2 method = new CallCtrlWithThreadSafety.Delegate2(CallCtrlWithThreadSafety.SetText2<ToolStripStatusLabel>);
                if (!winform.IsDisposed)
                    winform.Invoke(method, new object[] { objCtrl, text, winform });
            }
            else
            {
                objCtrl.Text = text;
            }
        }
    }
}
