using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MinimalLibrary.Themes;

namespace MinimalLibrary.Controls
{
    public partial class MStackPanel : TabControl
    {
        public bool UseControlBackground { get; set; }

        /// <summary>
        /// Used theme local variable
        /// </summary>
        private Theme _usedTheme;

        /// <summary>
        /// UsedTheme changed event
        /// </summary>
        [Category("MinimalLibrary")]
        [Description("Fires when UsedTheme is changed.")]
        public event PropertyChangedEventHandler UsedThemeChanged;

        /// <summary>
        /// Used theme property
        /// </summary>
        public Theme UsedTheme
        {
            get { return _usedTheme; }
            set
            {
                // Change used theme
                _usedTheme = value;

                // Fire event
                UsedThemeChanged?.Invoke(this, new PropertyChangedEventArgs("UsedThemeChanged"));

                // Redraw control
                Invalidate(true);
            }
        }

        /// <summary>
        /// Default source theme used for control drawing
        /// </summary>
        private Theme _sourceTheme;

        /// <summary>
        /// Constructor
        /// </summary>
        public MStackPanel()
        {
            InitializeComponent();
            DoubleBuffered = true;
        }

        /// <summary>
        /// Hide tabs
        /// </summary>
        /// <param name="m"></param>
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == 0x1328 && !DesignMode)
            {
                m.Result = (IntPtr)1;
            }
            else
            {
                base.WndProc(ref m);
            }

            if (DesignMode == false)
            {
                // Handles control's source theme
                // Check if control has set own theme
                if (_usedTheme != null)
                {
                    // Set custom theme as source theme
                    _sourceTheme = _usedTheme;
                }
                else
                {
                    // Control don't have its own theme
                    // Try cast control's parent form to MForm
                    MForm form = (MForm)FindForm();
                    _sourceTheme = form.UsedTheme;
                }

                // Repaint tabs
                foreach (TabPage page in TabPages)
                {
                    page.BackColor = (UseControlBackground) ? _sourceTheme.CONTROL_BACKGROUND.Normal.ToColor() : _sourceTheme.FORM_BACKGROUND.Normal.ToColor();
                }
            }
        }        
    }
}
