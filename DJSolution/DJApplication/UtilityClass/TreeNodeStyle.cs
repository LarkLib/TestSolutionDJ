using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace DJ.LMS.WinForms
{
    public class TreeNodeStyle : TreeNode
    {
        public TreeNodeStyle(string caption, string relativeForm)
        {
            base.Text = caption;
            RelativeForm = relativeForm;
        }

        public string RelativeForm
        {
            get;
            set;
        }
    }
}
