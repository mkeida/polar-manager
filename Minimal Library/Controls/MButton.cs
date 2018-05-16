using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using System.ComponentModel.Design;
using MinimalLibrary.Themes;
using MinimalLibrary.External;
using MinimalLibrary.Internal;

namespace MinimalLibrary.Controls
{
    /// <summary>
    /// Button control
    /// </summary>
    [Designer("MinimalLibrary.Controls.Design.ButtonDesigner")]
    public partial class MButton : Button
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
        [Category("MinimalLibrary")]
        [Description("Fires when the click effect type is changed.")]
        public event PropertyChangedEventHandler ClickEffectChanged;

        /// <summary>
        /// ClickEffect property
        /// </summary>
        [Category("MinimalLibrary")]
        [Description("Click effect of button.")]
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
        // TINT
        //

        /// <summary>
        /// Default tint
        /// </summary>
        private Color _tint = Hex.Blue.ToColor();

        /// <summary>
        /// Tint changed event handler
        /// </summary>
        [Category("MinimalLibrary")]
        [Description("Fires when the Tint is changed")]
        public event PropertyChangedEventHandler TintChanged;

        /// <summary>
        /// Tint property
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
        //  BUTTON TYPE
        //

        /// <summary>
        /// Default button type
        /// </summary>
        private ButtonType _buttonType = ButtonType.Raised;

        /// <summary>
        /// Button type changed event handler
        [Category("MinimalLibrary")]
        [Description("Fires when the button type is changed.")]
        public event PropertyChangedEventHandler TypeChanged;

        /// <summary>
        /// Type property
        /// </summary>
        [Category("MinimalLibrary")]
        [Description("Type of the button.")]
        public ButtonType Type
        {
            get { return _buttonType; }
            set
            {
                if (value != _buttonType)
                {
                    _buttonType = value;
                    TypeChanged?.Invoke(this, new PropertyChangedEventArgs("TypeChanged"));
                    Invalidate();
                }
            }
        }

        //
        //  FULL COLORED
        //

        /// <summary>
        /// Default full colored property
        /// </summary>
        private bool _fullColored = false;

        /// <summary>
        /// FullColored property changed event handler
        /// </summary>
        [Category("MinimalLibrary")]
        [Description("Fires when the button FullColored property is changed.")]
        public event PropertyChangedEventHandler FullColoredChanged;

        /// <summary>
        /// True if button should be fully painted with his tint color
        /// </summary>
        [Category("MinimalLibrary")]
        [Description("Fill button with his tint color")]
        public bool FullColored
        {
            get { return _fullColored; }
            set
            {
                if (value != _fullColored)
                {
                    _fullColored = value;
                    FullColoredChanged?.Invoke(this, new PropertyChangedEventArgs("FullColoredChanged"));
                    Invalidate();
                }
            }
        }

        //
        //  TEXT IN CAPITAL
        //

        /// <summary>
        /// Text in capital
        /// </summary>
        private bool _capitalText = true;

        /// <summary>
        /// Capital text property changed event handler
        /// </summary>
        [Category("MinimalLibrary")]
        [Description("Fires when the button TextInCapital property is changed.")]
        public event PropertyChangedEventHandler CapitalTextChanged;

        /// <summary>
        /// True if button's text should be in capital
        /// </summary>
        [Category("MinimalLibrary")]
        [Description("Fill button with his tint color")]
        public bool CapitalText
        {
            get { return _capitalText; }
            set
            {
                if (value != _capitalText)
                {
                    _capitalText = value;
                    CapitalTextChanged?.Invoke(this, new PropertyChangedEventArgs("FullColoredChanged"));
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
        /// True if the mouse is inside of the control
        /// </summary>
        private bool _hover;

        /// <summary>
        /// Alpha value of the hover effect which is later added to control fill color
        /// </summary>
        private byte _hoverAlpha;

        /// <summary>
        /// Mouse position relative to the control's top left corner
        /// </summary>
        private Point _mouse;

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
        /// Alpha value of the ClickEffect
        /// </summary>
        private byte _alpha;

        /// <summary>
        /// Rotation of the ClickEffect in degrees
        /// </summary>
        private int _rotation;

        /// <summary>
        /// Tint alpha
        /// </summary>
        private byte _tintAlpha;

        /// <summary>
        /// Change service
        /// </summary>
        private IComponentChangeService _changeService;

        /// <summary>
        /// Constructor
        /// </summary>
        public MButton()
        {
            InitializeComponent();

            // Default variables
            _controlTimer = new Timer();
            _controlTimer.Interval = 1;
            _controlTimer.Tick += new EventHandler(Update);
            _controlTimer.Start();
            _hover = false;
            _hoverAlpha = 0;
            _increment = 1;
            _tintAlpha = 0;
            _usedTheme = null;
            Font = new Font("Segoe UI", 8);
            Size = new Size(130, 36);
            DoubleBuffered = true;
            Text = "Ahoj";

            // Redraw
            Invalidate();
        }

        /// <summary>
        /// Update method
        /// </summary>
        protected void Update(object sender, EventArgs e)
        {
            // Hover effect
            if (_hover)
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

            // Alpha
            if (Focused)
            {
                if (_tintAlpha < 255)
                {
                    _tintAlpha += 15;
                }
            }
            else
            {
                if (_tintAlpha > 0)
                {
                    _tintAlpha -= 15;
                }
            }

            // Turn off timer
            if (!_hover && _radius == 0 && _alpha == 0 && _hoverAlpha == 0 && _tintAlpha == 0)
            {
                _controlTimer.Stop();
            }

            // Redraw
            Invalidate();
        }

        /// <summary>
        /// Draw method
        /// </summary>
        /// <param name="e"></param>
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
            g.Clear(Parent.BackColor);

            // Draw raised button
            if (_buttonType == ButtonType.Raised)
            {
                // Fill color
                Color fill = new Color();

                // Button is filled with his Tint color
                if (_fullColored)
                {
                    if (Enabled)
                    {
                        fill = MColor.AddRGB((_sourceTheme.DARK_BASED) ? +(_hoverAlpha / 15) : -(_hoverAlpha / 15), _tint);
                    }
                    else
                    {
                        fill = _sourceTheme.CONTROL_FILL.Disabled.ToColor();
                    }

                    g.FillRectangle(new SolidBrush(fill), ClientRectangle);
                }
                else
                {
                    // Regular raised button
                    fill = MColor.Mix(Color.FromArgb(_hoverAlpha, _sourceTheme.CONTROL_FILL.Hover.ToColor()), _sourceTheme.CONTROL_FILL.Normal.ToColor());

                    // Disabled
                    if (!Enabled)
                    {
                        fill = _sourceTheme.CONTROL_FILL.Disabled.ToColor();
                    }

                    g.FillRectangle(new SolidBrush(fill), ClientRectangle);
                }

                // Click effect
                DrawClick(e);

                // Focus rectangle
                if (Focused || _tintAlpha > 0)
                {
                    Rectangle rc = new Rectangle(Location.X, Location.Y, Width, Height);
                    Color frameColor = MColor.Mix(Color.FromArgb(_tintAlpha, Enabled ? _tint : fill), fill);
                    Pen framePen = new Pen(frameColor);
                    Rectangle frameRectangle = new Rectangle(0, 0, rc.Width - 1, rc.Height - 1);
                    g.DrawRectangle(framePen, frameRectangle);
                }

                // Text
                DrawText(e);
            }

            if (_buttonType == ButtonType.Outline)
            {
                // Fill color
                Color fill = new Color();
                Color border = new Color();

                // Button is filled with his Tint color
                if (_fullColored)
                {
                    // Fullcolored outlined button
                    fill = MColor.Mix(Color.FromArgb(_hoverAlpha, MColor.Lighten(220, _tint, Parent.BackColor)), Parent.BackColor);
                    border = (Enabled) ? _tint : _sourceTheme.CONTROL_FILL.Disabled.ToColor();

                    // Disabled
                    if (!Enabled)
                    {
                        fill = Parent.BackColor;
                    }

                    Rectangle r = ClientRectangle;
                    g.FillRectangle(new SolidBrush(fill), r);
                    g.DrawRectangle(new Pen(border), new Rectangle(r.X, r.Y, r.Width - 1, r.Height - 1));
                }
                else
                {
                    // Fullcolored outlined button
                    fill = MColor.Mix(Color.FromArgb(_hoverAlpha, MColor.Lighten(220, _sourceTheme.CONTROL_BORDER.Normal.ToColor(), Parent.BackColor)), Parent.BackColor);
                    border = (Enabled) ? _sourceTheme.CONTROL_FILL.Normal.ToColor() : _sourceTheme.CONTROL_FILL.Disabled.ToColor();

                    // Disabled
                    if (!Enabled)
                    {
                        fill = Parent.BackColor;
                    }

                    Rectangle r = ClientRectangle;
                    g.FillRectangle(new SolidBrush(fill), r);
                    g.DrawRectangle(new Pen(border), new Rectangle(r.X, r.Y, r.Width - 1, r.Height - 1));
                }

                // Click effect
                DrawClick(e);

                // Text
                DrawText(e);
            }

            if (_buttonType == ButtonType.Flat)
            {
                // Fill color
                Color fill = new Color();

                // Button is filled with his Tint color
                if (_fullColored)
                {
                    // Fullcolored outlined button
                    fill = MColor.Mix(Color.FromArgb(_hoverAlpha, MColor.Lighten(220, _tint, Parent.BackColor)), Parent.BackColor);

                    // Disabled
                    if (!Enabled)
                    {
                        fill = Parent.BackColor;
                    }

                    Rectangle r = ClientRectangle;
                    g.FillRectangle(new SolidBrush(fill), r);
                }
                else
                {
                    // Fullcolored outlined button
                    fill = MColor.Mix(Color.FromArgb(_hoverAlpha, MColor.Lighten(160, _sourceTheme.CONTROL_BORDER.Normal.ToColor(), Parent.BackColor)), Parent.BackColor);

                    // Disabled
                    if (!Enabled)
                    {
                        fill = Parent.BackColor;
                    }

                    Rectangle r = ClientRectangle;
                    g.FillRectangle(new SolidBrush(fill), r);
                }

                // Click effect
                DrawClick(e);

                // Text
                DrawText(e);
            }
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

            // ClickEffect
            if (_clickEffect != ClickEffect.None)
            {
                // Color of ClickEffect
                Color color;
                Color fill = (_fullColored) ? _tint : _sourceTheme.CONTROL_FILL.Normal.ToColor();

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
                // Set up antialiasing
                g.SmoothingMode = SmoothingMode.AntiAlias;

                // Ink
                if (_clickEffect == ClickEffect.Ink)
                {
                    // Ink's brush and grapics path
                    SolidBrush brush = new SolidBrush(color);
                    GraphicsPath ink = Draw.GetEllipsePath(_mouse, (int)_radius);

                    // Draws ink ClickEffect
                    g.FillPath(brush, ink);
                }

                // Square
                if (_clickEffect == ClickEffect.Square || _clickEffect == ClickEffect.SquareRotate)
                {
                    // Square's brush and grapics path
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

                // Remove antialiasing
                g.SmoothingMode = SmoothingMode.Default;
            }
        }

        /// <summary>
        /// Draw text
        /// </summary>
        /// <param name="e"></param>
        private void DrawText(PaintEventArgs e)
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

            // Converts aligment types
            StringFormat format = new StringFormat();
            int lNum = (int)Math.Log((double)TextAlign, 2);
            format.LineAlignment = (StringAlignment)(lNum / 4);
            format.Alignment = (StringAlignment)(lNum % 4);

            // Client Rectangle
            int offset = 10;
            Rectangle c = ClientRectangle;
            Rectangle r = new Rectangle(c.X + offset, c.Y + offset, c.Width - (offset * 2), c.Height - (offset * 2));

            // Color
            Color color = _sourceTheme.CONTROL_FOREGROUND.Normal.ToColor();

            // Font
            Font fontUnderline = new Font(Font, FontStyle.Underline);

            if (!Enabled)
            {
                // Control not enabled
                color = _sourceTheme.CONTROL_FOREGROUND.Disabled.ToColor();
            }
            else
            {
                color = _sourceTheme.CONTROL_FOREGROUND.Normal.ToColor();

                // Full colored control, but not outlined button
                if (_fullColored && _buttonType != ButtonType.Outline)
                {
                    color = ForeColor;
                }

                if (_buttonType == ButtonType.Outline && _fullColored)
                {
                    color = _tint;
                }

                if (_buttonType == ButtonType.Flat && _fullColored)
                {
                    color = _tint;
                }
            }

            // Draw text
            SolidBrush brush = new SolidBrush(color);
            g.DrawString((_capitalText) ? Text.ToUpper() : Text, (Focused && _buttonType != ButtonType.Raised) ? fontUnderline : Font, brush, r, format);
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

        /// <summary>
        /// Overrides site. Rename button at design time.
        /// </summary>
        public override ISite Site
        {
            get
            {
                return base.Site;
            }
            set
            {
                _changeService = (IComponentChangeService)GetService(typeof(IComponentChangeService));

                if (_changeService != null)
                {
                    _changeService.ComponentChanged -= new ComponentChangedEventHandler(OnComponentChanged);
                }

                base.Site = value;
                if (!DesignMode)
                {
                    return;
                }

                _changeService = (IComponentChangeService)GetService(typeof(IComponentChangeService));

                if (_changeService != null)
                {
                    _changeService.ComponentChanged += new ComponentChangedEventHandler(OnComponentChanged);
                }
            }
        }

        /// <summary>
        /// Fires when component is changed
        /// </summary>
        private void OnComponentChanged(object sender, ComponentChangedEventArgs ce)
        {
            MButton aBtn = ce.Component as MButton;
            if (aBtn == null || !aBtn.DesignMode) { return; }
            if (((IComponent)ce.Component).Site == null || ce.Member == null || ce.Member.Name != "Text") { return; }
            if (aBtn.Text == aBtn.Name) { aBtn.Text = aBtn.Name.Replace("mButton", "button"); }
        }
    }
}
