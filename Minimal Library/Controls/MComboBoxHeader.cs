using MinimalLibrary.External;
using MinimalLibrary.Internal;
using MinimalLibrary.Themes;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace MinimalLibrary.Controls
{
    /// <summary>
    /// ComboBox
    /// </summary>
    [ToolboxItem(false)]
    public partial class MComboBoxHeader : UserControl
    {
        //
        //  CLICK EFFECT
        //

        /// <summary>
        /// Default click effect
        /// </summary>
        private ClickEffect _clickEffect = ClickEffect.Ink;

        /// <summary>
        /// Button ClickEffect changed event handler
        /// </summary>
        public event PropertyChangedEventHandler ClickEffectChanged;

        /// <summary>
        /// ClickEffect property
        /// </summary>
        public ClickEffect ClickEffect
        {
            get { return _clickEffect; }
            set
            {
                if (value != _clickEffect)
                {
                    _clickEffect = value;
                    ClickEffectChanged?.Invoke(this, new PropertyChangedEventArgs("ClickEffectChanged"));
                }

                // Calls paint method
                Invalidate();
            }
        }

        //
        //  TINT
        //

        /// <summary>
        /// Default tint
        /// </summary>
        private Color _tint = Hex.Blue.ToColor();

        /// <summary>
        /// Tint changed event handler
        /// </summary>
        public event PropertyChangedEventHandler TintChanged;

        /// <summary>
        /// Tint property
        /// </summary>
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
        /// True if the mouse is inside of the control
        /// </summary>
        internal bool Hover;

        /// <summary>
        /// Alpha value of the hover effect which is later added to control fill color
        /// </summary>
        private byte _hoverAlpha;

        /// <summary>
        /// Mouse position relative to the control's top left corner
        /// </summary>
        private Point _mouse;

        /// <summary>
        /// Alpha value of the ClickEffect
        /// </summary>
        private byte _alpha;

        /// <summary>
        /// Length of the button diagonal
        /// </summary>
        private int _diagonal;

        /// <summary>
        /// Radius of the ClickEffect
        /// </summary>
        private double _radius;

        /// <summary>
        /// Radius increment of the ClickEffect
        /// </summary>
        private double _increment;

        /// <summary>
        /// Rotation of the ClickEffect in degrees
        /// </summary>
        private int _rotation;

        /// <summary>
        /// Constructor
        /// </summary>
        public MComboBoxHeader()
        {
            InitializeComponent();

            // Default variables
            _controlTimer = new Timer();
            _controlTimer.Interval = 1;
            _controlTimer.Tick += new EventHandler(Update);
            _controlTimer.Start();
            _increment = 1;
            _usedTheme = null;
            DoubleBuffered = true;
            Text = "ComboBox";
        }

        /// <summary>
        /// Update method
        /// </summary>
        protected void Update(object sender, EventArgs e)
        {
            // Hover effect
            if (Hover)
            {
                if (_hoverAlpha < 255) { _hoverAlpha += 15; }
            }
            else
            {
                if (_hoverAlpha > 0) { _hoverAlpha -= 15; }
            }

            // ClickEffect
            // If radius is smaller than half of diagonal length, then increase radius by ink inkIncrement
            if (_radius < _diagonal / 2) { _radius += _increment; }

            // Decrease alpha if it's not zero else reset animation variables
            if (_alpha > 0) { _alpha -= 1; } else { _radius = 0; _alpha = 0; }

            // Rotation
            if (_rotation < 360) { _rotation++; } else { _rotation = 0; }

            // Turn off timer
            if (!Hover && _radius == 0 && _alpha == 0 && _hoverAlpha == 0)
            {
                _controlTimer.Stop();
            }

            // Redraw
            Invalidate();
        }

        /// <summary>
        /// Draw method
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
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
            g.Clear(Parent.BackColor);

            // Regular raised button
            Color fill = MColor.Mix(Color.FromArgb(_hoverAlpha, _sourceTheme.CONTROL_FILL.Hover.ToColor()), _sourceTheme.CONTROL_FILL.Normal.ToColor());
            Color text = _sourceTheme.CONTROL_FOREGROUND.Normal.ToColor();

            // Disabled
            if (!Enabled)
            {
                fill = _sourceTheme.CONTROL_FILL.Disabled.ToColor();
                text = _sourceTheme.CONTROL_FOREGROUND.Disabled.ToColor();
            }

            // Fill control
            g.FillRectangle(new SolidBrush(fill), ClientRectangle);

            // Click effect
            DrawClick(e);

            // Draw text
            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            g.DrawString(Text, new Font("Segoe UI", 9), new SolidBrush(text), new Rectangle(ClientRectangle.X + 10, ClientRectangle.Y, ClientRectangle.Width, ClientRectangle.Height), sf);
        }

        /// <summary>
        /// Draw click effect
        /// </summary>
        /// <param name="e"></param>
        private void DrawClick(PaintEventArgs e)
        {
            // Control's graphics object
            Graphics g = e.Graphics;

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

            // ClickEffect
            if (_clickEffect != ClickEffect.None)
            {
                // Color of ClickEffect
                Color color;
                Color fill = _sourceTheme.CONTROL_FILL.Normal.ToColor();

                if (_sourceTheme.DARK_BASED == true)
                {
                    // Dark based themes
                    color = Color.FromArgb(_alpha, MColor.AddRGB(150, fill));
                }
                else
                {
                    // Light based themes
                    color = Color.FromArgb(_alpha, MColor.AddRGB(-150, fill));
                }

                // Draws ClickEffect
                // Set up anti-aliasing
                g.SmoothingMode = SmoothingMode.AntiAlias;

                // Ink
                if (_clickEffect == ClickEffect.Ink)
                {
                    // Ink's brush and graphics path
                    SolidBrush brush = new SolidBrush(color);
                    GraphicsPath ink = Draw.GetEllipsePath(_mouse, (int)_radius);

                    // Draws ink ClickEffect
                    g.FillPath(brush, ink);
                }

                // Square
                if (_clickEffect == ClickEffect.Square || _clickEffect == ClickEffect.SquareRotate)
                {
                    // Square's brush and graphics path
                    SolidBrush brush = new SolidBrush(color);
                    GraphicsPath square = Draw.GetSquarePath(_mouse, (int)_radius);

                    // Rotates square
                    if (_clickEffect == ClickEffect.SquareRotate)
                    {
                        Matrix matrix = new Matrix();
                        matrix.RotateAt(_rotation, _mouse);
                        square.Transform(matrix);
                    }

                    // Draws square ClickEffect
                    g.FillPath(brush, square);
                }

                // Remove anti-aliasing
                g.SmoothingMode = SmoothingMode.Default;
            }
        }

        /// <summary>
        /// OnMouseEnter method. Check if mouse is inside of button
        /// </summary>
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            Hover = true;

            // Turn on timer
            _controlTimer.Start();
        }

        /// <summary>
        /// OnMouseLeave method. Check if mouse is outside of button
        /// </summary>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            Hover = false;
        }

        /// <summary>
        /// Control resize event
        /// </summary>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            _diagonal = (int)(2 * Math.Sqrt(Math.Pow(Width, 2) + Math.Pow(Height, 2)));
        }

        /// <summary>
        /// Control click event
        /// </summary>
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            // Set up ClickEffect variables
            _mouse = PointToClient(MousePosition);

            // ClickEffect
            // Ink
            if (_clickEffect == ClickEffect.Ink) { _radius = Width / 5; }

            // Square
            if (_clickEffect == ClickEffect.Square || _clickEffect == ClickEffect.SquareRotate) { _radius = Width / 8; }

            // Resets alpha
            _alpha = 25;
        }

        /// <summary>
        /// On got focus
        /// </summary>
        /// <param name="e"></param>
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            _controlTimer.Start();
        }
    }
}
