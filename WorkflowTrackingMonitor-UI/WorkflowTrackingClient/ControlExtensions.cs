using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CheckBoxStudio.WinForms;

namespace WorkflowTrackingClient
{
    static public class ControlExtensions
    {
        /// <summary>
        /// Runs code in UI thread synchronously.
        /// </summary>
        /// <param name="code">the code, like "delegate { this.Text = "new text"; }"
        /// </param>
        static public void UIThread(this Control control, MethodInvoker code)
        {
            if (control.InvokeRequired)
            {
                control.Invoke(code);
                return;
            }
            code.Invoke();
        }
    }
}
