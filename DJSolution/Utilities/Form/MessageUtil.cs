using System;
using System.Windows.Forms;

namespace DJ.LMS.Utilities
{
    /// <summary>
    /// 用来方便实现标准统一的消息提示对话框。
    /// </summary>
    public class MessageUtil
    {
        /// <summary>
        /// 显示一般的提示信息
        /// </summary>
        /// <param name="message">提示信息</param>
        public static DialogResult ShowTips(string message)
        {
            return MessageBox.Show(message, "提示信息", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }
        /// <summary>
        /// 显示警告信息
        /// </summary>
        /// <param name="message">警告信息</param>
        public static DialogResult ShowWarning(string message)
        {
            return MessageBox.Show(message, "警告信息", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }
        /// <summary>
        /// 显示错误信息
        /// </summary>
        /// <param name="message">错误信息</param>
        public static DialogResult ShowError(string message)
        {
            return MessageBox.Show(message, "错误信息", MessageBoxButtons.OK, MessageBoxIcon.Hand);
        }
        /// <summary>
        /// 显示询问用户信息，并显示错误标志
        /// </summary>
        /// <param name="message">错误信息</param>
        public static DialogResult ShowYesNoAndError(string message)
        {
            return MessageBox.Show(message, "错误信息", MessageBoxButtons.YesNo, MessageBoxIcon.Hand);
        }
        /// <summary>
        /// 显示询问用户信息，并显示提示标志
        /// </summary>
        /// <param name="message">错误信息</param>
        public static DialogResult ShowYesNoAndTips(string message)
        {
            return MessageBox.Show(message, "提示信息", MessageBoxButtons.YesNo, MessageBoxIcon.Asterisk);
        }
        /// <summary>
        /// 显示询问用户信息，并显示警告标志
        /// </summary>
        /// <param name="message">警告信息</param>
        public static DialogResult ShowYesNoAndWarning(string message)
        {
            return MessageBox.Show(message, "警告信息", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation);
        }
        /// <summary>
        /// 显示询问用户信息，并显示提示标志
        /// </summary>
        /// <param name="message">错误信息</param>
        public static DialogResult ShowYesNoCancelAndTips(string message)
        {
            return MessageBox.Show(message, "提示信息", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Asterisk);
        }
        /// <summary>
        /// 显示询问用户信息，并显示提示标志
        /// </summary>
        /// <param name="prompt">提示信息</param>
        /// <returns></returns>
        public static bool ConfirmYesNo(string prompt)
        {
            return MessageBox.Show(prompt, "确认", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes;
        }
        /// <summary>
        /// 显示询问用户信息，并显示提示标志
        /// </summary>
        /// <param name="prompt">提示信息</param>
        /// <returns></returns>
        public static DialogResult ConfirmYesNoCancel(string prompt)
        {
            return MessageBox.Show(prompt, "确认", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
        }
    }
}
