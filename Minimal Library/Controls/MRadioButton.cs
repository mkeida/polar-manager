using System;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.ComponentModel;
using MinimalLibrary.Themes;
using MinimalLibrary.External;
using MinimalLibrary.Internal;
using MinimalLibrary.Scaling;

namespace MinimalLibrary.Controls
{
    public partial class MRadioButton : RadioButton
    {
        /// <summary>
        /// Default tint color
        /// </summary>
        private Color _tint = Hex.Blue.ToColor();

        /// <summary>
        /// Tint changed event handler
        /// </summary>
        [Category("Property Changed")]
        [Description("Fires when the Tint is changed")]
        public event PropertyChangedEventHandler TintChanged;

        /// <summary>
        /// Tint color property
        /// </summary>
        [Category("Appearance")]
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
        /// Tint alpha
        /// </summary>
        private byte _tintAlpha;

        /// <summary>
        /// Mouse position
        /// </summary>
        private Point _mouse;

        /// <summary>
        /// Radius of inner circle
        /// </summary>
        private double _radius;

        /// <summary>
        /// Hover alpha value
        /// </summary>
        private int _hoverAlpha;

        /// <summary>
        /// True if the mouse is inside a control
        /// </summary>
        private bool _hover;

        /// <summary>
        /// Constructor
        /// </summary>
        public MRadioButton()
        {
            InitializeComponent();

            // Scaling
            DIP.GetGraphics(this);

            // Default variables
            _controlTimer = new Timer();
            _controlTimer.Enabled = true;
            _controlTimer.Interval = 1;
            _controlTimer.Tick += new EventHandler(Update);
            _controlTimer.Start();
            _state = false;
            _tintAlpha = 0;
            _radius = 0;
            _hoverAlpha = 0;
            _hover = false;
            _usedTheme = null;
            DoubleBuffered = true;
            AutoSize = false;
            Height = DIP.Set(20);
            Font = new Font("Segoe UI", 7);
            Size = new Size(130, 36);

            // Pass graphics to DIP class
            DIP.GetGraphics(this);

            // Redraw
            Invalidate();
        }

        /// <summary>
        /// Update
        /// </summary>
        protected void Update(object sender, EventArgs e)
        {
            if (_state)
            {
                // Checked
                // Radius
                if (_radius < 5) { _radius += 0.35; }

                // Alpha
                if (_tintAlpha < 255)
                {
                    _tintAlpha += 15;
                }
            }
            else
            {
                // Unchecked
                // Radius
                if (_radius > 0) { _radius -= 0.35; }

                // Alpha
                if (_tintAlpha > 0)
                {
                    _tintAlpha -= 15;
                }
            }

            // Hover
            if (_hover)
            {
                if (_hoverAlpha < 255) { _hoverAlpha += 15; }
            }
            else
            {
                if (_hoverAlpha > 0) { _hoverAlpha -= 15; }
            }

            // Turn off timer
            if (!_hover && _hoverAlpha == 0 && _tintAlpha == 0)
            {
                _controlTimer.Stop();
            }

            // Call paint method
            Invalidate();
        }

        /// <summary>
        /// Draw
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            // Base painting
            base.OnPaint(e);

            // Scaling
            DIP.GetGraphics(this);

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

            // Fill color, text and border
            Color fill = new Color();
            Color border = new Color();
            Color hover = new Color();
            Color foreground = new Color();

            if (Enabled)
            {
                // Enabled
                fill = _sourceTheme.CONTROL_BACKGROUND.Normal.ToColor();
                border = _sourceTheme.CONTROL_BORDER.Normal.ToColor();
                foreground = _sourceTheme.CONTROL_FOREGROUND.Normal.ToColor();
                hover = _sourceTheme.CONTROL_FILL.Hover.ToColor();
            }
            else
            {
                // Disabled
                fill = _sourceTheme.CONTROL_BACKGROUND.Disabled.ToColor();
                border = _sourceTheme.CONTROL_BORDER.Disabled.ToColor();
                foreground = _sourceTheme.CONTROL_FOREGROUND.Disabled.ToColor();
                hover = _sourceTheme.CONTROL_FILL.Disabled.ToColor();
            }

            // Draw background
            g.FillPath(new SolidBrush(fill), Draw.GetEllipsePath(new Point(DIP.Set(10), DIP.Set(10)), DIP.Set((10))));

            // Antialiasing
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Draw border
            if (!_state)
            {
                g.DrawPath(new Pen(!Checked ? _sourceTheme.CONTROL_BORDER.Normal.ToColor() : _tint, 1), Draw.GetEllipsePath(new Point(DIP.Set(10), DIP.Set(10)), DIP.Set(10)));
            }

            // Mouse hover
            g.FillPath(new SolidBrush(Color.FromArgb(_hoverAlpha, hover)), Draw.GetEllipsePath(new Point(DIP.Set(10), DIP.Set(10)), DIP.Set(3)));

            // Animation - inner circle
            g.FillPath(new SolidBrush(Enabled ? _tint : _sourceTheme.CONTROL_FILL.Disabled.ToColor()), Draw.GetEllipsePath(new Point(DIP.Set(10), DIP.Set(10)), Convert.ToInt32(_radius)));

            // Animation - border tint
            g.DrawEllipse(new Pen(MColor.Mix(Color.FromArgb(_tintAlpha, Enabled ? _tint : _sourceTheme.CONTROL_BORDER.Disabled.ToColor()), border)), new Rectangle(0, 0, DIP.Set(20), DIP.Set(20)));

            // Antialiasing
            g.SmoothingMode = SmoothingMode.Default;

            // Draw text
            g.DrawString(this.Text, this.Font, new SolidBrush(foreground), new Point(25 + DIP.Set(8), 2));
        }

        /// <summary>
        /// Checked changed event
        /// </summary>
        protected override void OnCheckedChanged(EventArgs e)
        {
            base.OnCheckedChanged(e);
            _state = Checked;
        }

        /// <summary>
        /// Mouse move
        /// </summary>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            _mouse = new Point(e.X, e.Y);
        }

        /// <summary>
        /// OnMouseEnter method. Check if mouse is inside of control
        /// </summary>
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _hover = true;
            _controlTimer.Start();
        }

        /// <summary>
        /// OnMouseLeave method. Check if mouse is outside of control
        /// </summary>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _hover = false;
        }

        public override bool AutoSize
        {
            get { return base.AutoSize; }
            set { base.AutoSize = false; }
        }
    }
}
