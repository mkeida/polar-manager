using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using System.ComponentModel;
using MinimalLibrary.Internal;
using MinimalLibrary.External;
using MinimalLibrary.Scaling;
using MinimalLibrary.Themes;

namespace MinimalLibrary.Controls
{
    /// <summary>
    /// Minimal checkbox
    /// </summary>
    public partial class MCheckBox : CheckBox
    {
        //
        //  TINT
        //

        /// <summary>
        /// Default tint color
        /// </summary>
        private Color _tint = Hex.Blue.ToColor();

        /// <summary>
        /// Tint changed event handler
        /// </summary>
        [Category("MinimalLibrary")]
        [Description("Fires when the Tint is changed")]
        public event PropertyChangedEventHandler TintChanged;

        /// <summary>
        /// Tint color property
        /// </summary>
        [Category("MinimalLibrary")]
        [Description("Tint color of control. Main visible color of the control.")]
        public Color Tint
        {
            get { return _tint; }
            set
            {
                if (value != _tint)
                {
                    _tint = value;
                    TintChanged?.Invoke(this, new PropertyChangedEventArgs("TintChanged"));
                    Invalidate();
                }
            }
        }

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
        /// Timer of the control
        /// </summary>
        private Timer _controlTimer;

        /// <summary>
        /// Default source theme used for control drawing
        /// </summary>
        private Theme _sourceTheme;

        /// <summary>
        /// Current state of the control
        /// </summary>
        private bool _state;

        /// <summary>
        /// Size of the cover square
        /// </summary>
        private int _a;

        /// <summary>
        /// Handles checkmark animation progress
        /// </summary>
        private int _frame;

        /// <summary>
        /// Checkmark point array
        /// </summary>
        private Point[] _checkmark = {};

        /// <summary>
        /// True if the mouse is inside of the control
        /// </summary>
        private bool _hover;

        /// <summary>
        /// Alpha value of the hover effect which is later added to control fill color
        /// </summary>
        private byte _hoverAlpha;

        /// <summary>
        /// Constructor
        /// </summary>
        public MCheckBox()
        {
            InitializeComponent();

            // Default variables
            _controlTimer = new Timer();
            _controlTimer.Enabled = true;
            _controlTimer.Interval = 1;
            _controlTimer.Tick += new EventHandler(Update);
            _controlTimer.Start();
            _state = false;
            _a = 0;
            _frame = 0;
            _hover = false;
            _hoverAlpha = 0;
            DoubleBuffered = true;
            AutoSize = false;
            DIP.GetGraphics(this);
            Height = DIP.Set(15);
            Font = new Font("Segoe UI", 7);
            Size = new Size(130, 36);

            // Redraw
            Invalidate();
        }

        /// <summary>
        /// Update
        /// </summary>
        protected void Update(object sender, EventArgs e)
        {
            // Animarion variables
            if (_state)
            {
                if (_a < 10) { _a++; }
            }
            else
            {
                if (_frame == _checkmark.Length)
                {
                    if (_a > 0) { _a--; }
                }
            }

            // Slow down timer on the checkmark animation
            if (_a == 0 || _a == 10) { _controlTimer.Interval = 12; } else { _controlTimer.Interval = 1; }

            // Frame of the checkmark animation
            if (_frame < _checkmark.Length)
            {
                if (_a == 0 || _a == 10)
                {
                    _frame++;
                }
            }
            else
            {
                _frame = _checkmark.Length;
            }

            // Hover effect
            if (_hover)
            {
                if (_hoverAlpha < 255) { _hoverAlpha += 15; }
            }
            else
            {
                if (_hoverAlpha > 0) { _hoverAlpha -= 15; }
            }

            // Turn off timer
            if (!_hover && _hoverAlpha == 0 && (_a == 0 || _a == 10))
            {
                // _controlTimer.Stop();
            }

            // Redraw
            Invalidate();
        }

        /// <summary>
        /// Paint
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
                // Control dont have its own theme
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
            g.Clear(Parent.BackColor);

            // Scaling
            DIP.GetGraphics(this);

            // Fill color, text and border
            Color fill = new Color();
            Color border = new Color();
            Color foreground = new Color();

            if (Enabled)
            {
                // Enabled
                fill = MColor.Mix(Color.FromArgb(_hoverAlpha, _sourceTheme.CONTROL_BACKGROUND.Hover.ToColor()), _sourceTheme.CONTROL_BACKGROUND.Normal.ToColor());
                border = _sourceTheme.CONTROL_BORDER.Normal.ToColor();
                foreground = _sourceTheme.CONTROL_FOREGROUND.Normal.ToColor();
            }
            else
            {
                // Disabled
                fill = _sourceTheme.CONTROL_BACKGROUND.Disabled.ToColor();
                border = _sourceTheme.CONTROL_BORDER.Disabled.ToColor();
                foreground = _sourceTheme.CONTROL_FOREGROUND.Disabled.ToColor();
            }

            // Draw unchecked checkbox
            int height = 19;
            g.FillRectangle(new SolidBrush(fill), new Rectangle(0, Height / 2 - height / 2, height, height));
            g.DrawRectangle(new Pen(border), new Rectangle(0, Height / 2 - height / 2, height, height));

            // Draws cover
            GraphicsPath square = Draw.GetSquarePath(new Point(10, Height / 2 - height / 2 +10), _a);
            g.FillPath(new SolidBrush(Enabled ? _tint : _sourceTheme.CONTROL_FILL.Disabled.ToColor()), square);

            int b = Height / 2 - height / 2;
            _checkmark = new Point[]
            {
                new Point(4, b + 9),
                new Point(5, b + 10),
                new Point(6, b + 11),
                new Point(7, b + 12),
                new Point(8, b + 13),
                new Point(9, b + 12),
                new Point(10, b + 11),
                new Point(11, b + 10),
                new Point(12, b + 9),
                new Point(13, b + 8),
                new Point(14, b + 7),
                new Point(15, b + 6),
            };

            // Draws checkmark
            if (_a == 10 && _state == true)
            {
                DrawCheckmarkIn(e);
            }

            if (_a == 10 && _state == false)
            {
                DrawCheckmarkOut(e);
            }

            // Draw text
            g.DrawString(this.Text, this.Font, new SolidBrush(foreground), new Point(25, b));
        }

        /// <summary>
        /// OnMouseEnter method. Check if mouse is inside of button
        /// </summary>
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _hover = true;

            // Turn on timer
            _controlTimer.Start();
        }

        /// <summary>
        /// OnMouseLeave method. Check if mouse is outside of button
        /// </summary>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _hover = false;
        }

        /// <summary>
        /// Checked changed event
        /// </summary>
        protected override void OnCheckedChanged(EventArgs e)
        {
            base.OnCheckedChanged(e);

            // State
            _frame = 0;
            _state = Checked;

            // Calls OnPaint method
            Invalidate();
        }

        /// <summary>
        /// Draws checkmark
        /// </summary>
        private void DrawCheckmarkIn(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Pen
            Pen pen = new Pen(Parent.BackColor, 2);

            // Draws checkmark
            for (int i = 0; i < _checkmark.Length; i++)
            {
                if (i + 1 < _checkmark.Length)
                {
                    if (i < _frame)
                    {
                        g.DrawLine(pen, _checkmark[i], _checkmark[i + 1]);
                    }
                }
            }
        }

        /// <summary>
        /// Draws checkmark backwards
        /// </summary>
        private void DrawCheckmarkOut(PaintEventArgs e)
        {
            Graphics g = e.Graphics;

            // Pen
            Pen pen = new Pen(Parent.BackColor, 2);

            // Draws checkmark
            for (int i = _checkmark.Length - 1; i > 0; i--)
            {
                if (i < (_checkmark.Length - _frame))
                {
                    g.DrawLine(pen, _checkmark[i], _checkmark[i - 1]);
                }
            }
        }

        /// <summary>
        /// Autosize. Always false. Allow to set custom height of check box.
        /// </summary>
        public override bool AutoSize
        {
            set { base.AutoSize = false; }
            get { return base.AutoSize; }
        }
    }
}
