using System;
using System.ComponentModel;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace MinimalLibrary.Controls
{
    /// <summary>
    /// Double buffered panel
    /// </summary>
    public partial class MBufferedPanel : Panel
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public MBufferedPanel()
        {
            this.DoubleBuffered = true;
            Invalidate();
        }

        /// <summary>
        /// Draw method
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
        }

        // Hide default panel's scroll-bars
        /// <summary>
        /// User32 .dll
        /// </summary>
        /// <param name="hWnd">Window handle</param>
        /// <param name="wBar">Scrollbar</param>
        /// <param name="bShow">Show</param>
        /// <returns></returns>
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool ShowScrollBar(IntPtr hWnd, int wBar, bool bShow);

        /// <summary>
        /// Scroll bar directions
        /// </summary>
        private enum ScrollBarDirection
        {
            SB_HORZ = 0,
            SB_VERT = 1,
            SB_CTL = 2,
            SB_BOTH = 3
        }

        /// <summary>
        /// Process call back
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref System.Windows.Forms.Message m)
        {
            if (!DesignMode)
            {
                ShowScrollBar(this.Handle, (int)ScrollBarDirection.SB_BOTH, false);
            }

            base.WndProc(ref m);
        }
    }
}
