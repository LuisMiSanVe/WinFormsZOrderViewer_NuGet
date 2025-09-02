using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace WinFormsZOrderViewer
{
    [ProvideProperty("ZOrder", typeof(Control))]
    public class ZOrderView : Component, IExtenderProvider
    {
        public ZOrderView() { }

        public bool CanExtend(object extendee)
        {
            return extendee is Control;
        }

        [Category("Z-Order")]
        [Description("Gets or sets the Z-Order index of the control within its parent.")]
        public int GetZOrder(Control control)
        {
            if (control?.Parent == null)
                return -1;

            return control.Parent.Controls.GetChildIndex(control);
        }

        public void SetZOrder(Control control, int value)
        {
            if (control?.Parent == null)
                return;

            var parent = control.Parent;
           
            value = Math.Max(0, Math.Min(value, parent.Controls.Count - 1));

            parent.Controls.SetChildIndex(control, value);
            parent.Invalidate();
        }
    }
}
