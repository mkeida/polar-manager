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
using MinimalLibrary.External;

namespace MinimalLibrary.Controls
{
    public partial class MColorBox : MBufferedPanel
    {
        //
        //  COLOR
        //

        /// <summary>
        /// Color
        /// </summary>
        private Color _color = Hex.Blue.ToColor();

        /// <summary>
        /// Color property
        /// </summary>
        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }

        //
        //  TITLE
        //

        /// <summary>
        /// Title
        /// </summary>
        private string _title;

        /// <summary>
        /// Title property
        /// </summary>
        public string Title
        {
            get { return _title; }
            set { _title = value; }
        }

        //
        //  DRAW BORDER
        //

        /// <summary>
        /// True if color box should have border
        /// </summary>
        public bool DrawBorder { get; set; }

        //
        //  USED THEME
        //

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
        public MColorBox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Draw
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            // Base painting
            base.OnPaint(e);

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
                try
                {
                    MForm form = (MForm)FindForm();
                    _sourceTheme = form.UsedTheme;
                }
                catch
                {
                    // Control's parent form is not MForm type
                    // Set application wide theme
                    _sourceTheme = Minimal.UsedTheme;
                }
            }

            // Graphics
            Graphics g = e.Graphics;

            // Clear control
            g.Clear(Color);

            // Draw border
            if (DrawBorder)
            {
                g.DrawRectangle(new Pen(new SolidBrush(_sourceTheme.CONTROL_FOREGROUND.Normal.ToColor())), new Rectangle(new Point(0, 0), new Size(ClientRectangle.Width - 1, ClientRectangle.Height - 1)));
            }

            // String format
            StringFormat format = new StringFormat();
            format.LineAlignment = StringAlignment.Center;
            format.Alignment = StringAlignment.Near;

            // Draw title
            g.DrawString(_title, Font, new SolidBrush(ForeColor), new Rectangle(new Point(10, 0), new Size(ClientRectangle.Width, ClientRectangle.Height)), format);
        }
    }
}
