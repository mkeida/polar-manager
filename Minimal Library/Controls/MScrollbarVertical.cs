using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MinimalLibrary.Themes;

namespace MinimalLibrary.Controls
{
    public partial class MScrollbarVertical : UserControl
    {
        //
        //  SCROLL
        //

        /// <summary>
        /// Scroll event handler
        /// </summary>
        [Category("MinimalLibrary")]
        public event EventHandler Scroll = null;

        //
        //  VALUE
        //

        /// <summary>
        /// Default value
        /// </summary>
        private int _value = 0;

        /// <summary>
        /// Value changed event handler
        /// </summary>
        [Category("MinimalLibrary")]
        [Description("Fires when value is changed.")]
        public event PropertyChangedEventHandler ValueChanged;

        /// <summary>
        /// Value property
        /// </summary>
        [Category("MinimalLibrary")]
        [Description("Value of scrollbar.")]
        public int Value
        {
            get { return _value; }
            set
            {
                if (value != _value)
                {
                    SetValue(value);
                    ValueChanged?.Invoke(this, new PropertyChangedEventArgs("ValueChanged"));
                }

                // Calls paint method
                _visible = true;
                Invalidate();
            }
        }

        //
        //  MINIMUM
        //

        /// <summary>
        /// Default minimum
        /// </summary>
        private int _minimum = 0;

        /// <summary>
        /// Minimum changed event handler
        /// </summary>
        [Category("MinimalLibrary")]
        [Description("Fires when minimum is changed.")]
        public event PropertyChangedEventHandler MinimumChanged;

        /// <summary>
        /// Minimum property
        /// </summary>
        [Category("MinimalLibrary")]
        [Description("Minimal value of scrollbar.")]
        public int Minimum
        {
            get { return _minimum; }
            set
            {
                if (value != _minimum)
                {
                    _minimum = value;
                    MinimumChanged?.Invoke(this, new PropertyChangedEventArgs("MinimumChanged"));
                }

                // Calls paint method
                Invalidate();
            }
        }

        //
        //  MAXIMUM
        //

        /// <summary>
        /// Default maximum
        /// </summary>
        private int _maximum = 100;

        /// <summary>
        /// Maximum changed event handler
        /// </summary>
        [Category("MinimalLibrary")]
        [Description("Fires when maximum is changed.")]
        public event PropertyChangedEventHandler MaximumChanged;

        /// <summary>
        /// Maximum property
        /// </summary>
        [Category("MinimalLibrary")]
        [Description("Maximal value of scrollbar.")]
        public int Maximum
        {
            get { return _maximum; }
            set
            {
                if (value != _maximum)
                {
                    _maximum = value;
                    MaximumChanged?.Invoke(this, new PropertyChangedEventArgs("MaximumChanged"));
                }

                // Calls paint method
                Invalidate();
            }
        }

        //
        //  CURSOR LENGTH
        //

        /// <summary>
        /// Length of cursor
        /// </summary>
        private int _cursorLength = 50;

        /// <summary>
        /// CursorLength changed event handler
        /// </summary>
        [Category("MinimalLibrary")]
        [Description("Fires when cursor length is changed.")]
        public event PropertyChangedEventHandler CursorLengthChanged;

        /// <summary>
        /// CursorLength property
        /// </summary>
        [Category("MinimalLibrary")]
        [Description("Cursor length.")]
        public int CursorLength
        {
            get { return _cursorLength; }
            set
            {
                if (value != _cursorLength)
                {
                    _cursorLength = value;
                    CursorLengthChanged?.Invoke(this, new PropertyChangedEventArgs("CursorLengthCahnged"));
                }

                // Update value
                // SetValue(_value);

                // Calls paint method
                Invalidate();
            }
        }

        //
        //  AUTOHIDE
        //

        /// <summary>
        /// Autohide
        /// </summary>
        private bool _autoHide;

        /// <summary>
        /// AutoHide event hadler
        /// </summary>
        [Category("MinimalLibrary")]
        [Description("Fires when the AutoHide is changed.")]
        public event PropertyChangedEventHandler AutoHideChanged;

        /// <summary>
        /// AutoHide property
        /// </summary>
        [Category("MinimalLibrary")]
        [Description("Automaticaly hides scrollbar when true.")]
        public bool AutoHide
        {
            get { return _autoHide; }
            set
            {
                if (value != _autoHide)
                {
                    _autoHide = value;
                    AutoHideChanged?.Invoke(this, new PropertyChangedEventArgs("AutoHideChanged"));
                }

                // Redraw
                Invalidate();
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

        //
        //  SCROLLBAR MINIFIED
        //

        /// <summary>
        /// Minified local variable
        /// </summary>
        private bool _scrollbarMinified;

        /// <summary>
        /// Minified changed event
        /// </summary>
        [Category("MinimalLibrary")]
        [Description("Fires when Minified is changed.")]
        public event PropertyChangedEventHandler ScrollbarMinifiedChanged;

        /// <summary>
        /// Minified property
        /// </summary>
        public bool ScrollbarMinified
        {
            get { return _scrollbarMinified; }
            set
            {
                // Change used theme
                _scrollbarMinified = value;

                // Fire event
                ScrollbarMinifiedChanged?.Invoke(this, new PropertyChangedEventArgs("MinifiedChanged"));

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
        /// Mouse click position
        /// </summary>
        private Point _mouseClickPosition;

        /// <summary>
        /// Mouse increment
        /// </summary>
        private int _mouseIncrement;

        /// <summary>
        /// Trackbar
        /// </summary>
        private Rectangle _cursor;

        /// <summary>
        /// Position of cursor
        /// </summary>
        private Point _cursorPosition;

        /// <summary>
        /// Previous cursor position
        /// </summary>
        private Point _cursorPositionPrevious;

        /// <summary>
        /// True if mouse cursor hovers over scrollbar's cursor
        /// </summary>
        private bool _cursorHover;

        /// <summary>
        /// True if mouse hovers over control
        /// </summary>
        private bool _hover;

        /// <summary>
        /// Timer controls how long can scrollbar stay visible
        /// </summary>
        private int _timer;

        /// <summary>
        /// true if user clicks on scrollbar's cursor
        /// </summary>
        private bool _cursorClick;

        /// <summary>
        /// Alpha of scrollbar
        /// </summary>
        private int _alpha;

        /// <summary>
        /// True if scrollbar is hidden
        /// </summary>
        private bool _visible;

        /// <summary>
        /// Constructor
        /// </summary>
        public MScrollbarVertical()
        {
            InitializeComponent();

            // Default variables
            _controlTimer = new Timer();
            _controlTimer.Interval = 1;
            _controlTimer.Tick += new EventHandler(Update);
            _controlTimer.Start();
            _cursor = new Rectangle();
            _cursorPosition = new Point(0, 0);
            _cursorHover = false;
            _cursorClick = false;
            _visible = false;
            _autoHide = true;
            _alpha = (LicenseManager.UsageMode == LicenseUsageMode.Designtime) ? 255 : 0;
            _timer = 0;
            _usedTheme = null;
            Width = 5;
            DoubleBuffered = true;
        }

        /// <summary>
        /// Update method
        /// </summary>
        protected void Update(object sender, EventArgs e)
        {
            // Update cursor
            _cursor = new Rectangle(_cursorPosition, new Size(Width, _cursorLength));

            // Update cursor position
            if (_cursorClick)
            {
                _cursorPosition = new Point(0, _cursorPositionPrevious.Y + _mouseIncrement);

                // Run events
                Scroll?.Invoke(this, new EventArgs());
                ValueChanged?.Invoke(this, new PropertyChangedEventArgs("ValueChanged"));
                _visible = true;
            }

            // Limit cursor position
            // Minimal
            if (_cursorPosition.Y < 0) { _cursorPosition.Y = 0; }

            // Maximal
            if (_cursorPosition.Y + _cursorLength > Height) { _cursorPosition.Y = Height - _cursorLength; }

            // Update value
            _value = GetValue();

            // Call mouse leave method
            try { if (!ClientRectangle.Contains(PointToClient(MousePosition))) { base.OnMouseLeave(e); } } catch { }

            // Timer
            if (_visible && !_hover)
            {
                _timer++;
                if ((_timer % 200) == 0)
                {
                    _timer = 0;
                    _visible = false;
                }
            }

            // Update alpha
            if (AutoHide)
            {
                if (_visible)
                {
                    if (_alpha < 255) { _alpha += 15; }
                    if (Width < (_scrollbarMinified ? 4 : 20)) { Width++; }
                }
                else
                {
                    if (_alpha > 0) { _alpha -= 15; }
                    if (Width > 5) { Width--; }
                }
            }

            // Redraw scrollbar
            Invalidate();
        }

        /// <summary>
        /// Draw method
        /// </summary>
        /// <param name="e"></param>
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
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

            // Clear control
            g.Clear(Parent.BackColor);

            // Draw scrollbar path
            if (!_scrollbarMinified)
            {
                Color path = Color.FromArgb(_alpha, _sourceTheme.CONTROL_FILL.Normal.ToColor());
                g.FillRectangle(new SolidBrush(path), ClientRectangle);
            }

            // Draw cursor
            Color normal = Color.FromArgb(_alpha, _sourceTheme.CONTROL_HIGHLIGHT.Normal.ToColor());
            Color hover = Color.FromArgb(_alpha, _sourceTheme.CONTROL_HIGHLIGHT.Hover.ToColor());

            if (_cursor.Height != Height)
            {
                g.FillRectangle((_cursorClick) ? new SolidBrush(hover) : new SolidBrush(normal), _cursor);
            }
        }

        /// <summary>
        /// Resize event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            // Update cursor
            SetValue(_value);
        }

        /// <summary>
        /// Mouse move
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            // Check if mouse hover over scrollbar's cursor
            if (_cursor.Contains(e.Location)) { _cursorHover = true; } else { _cursorHover = false; }

            // Mouse increment
            Point mouse = PointToClient(MousePosition);
            _mouseIncrement = mouse.Y - _mouseClickPosition.Y;
        }

        /// <summary>
        /// Return Value based on cursor position
        /// </summary>
        private int GetValue()
        {
            decimal path = Height - _cursorLength;
           
            // Catch divide by zero exception
            if (path > 0 && (_maximum - _minimum > 0))
            {
                decimal increment = (_maximum - _minimum) / path;
                return _minimum + Convert.ToInt32(increment * _cursorPosition.Y);
            }

            return 0;
        }

        /// <summary>
        /// Set Value and updates cursor position
        /// </summary>
        private void SetValue(int value)
        {
            // Update value
            _value = value;

            // Update cursor
            decimal path = Height - _cursorLength;

            // Catch divide by zero exception
            if (path > 0 && (_maximum - _minimum > 0))
            {
                decimal increment = path / (_maximum - _minimum);
                _cursorPosition.Y = Convert.ToInt32(_value * increment) + (int)(_minimum * increment);
            }

            // Reset timer
            _timer = 0;

            // Redraw
            Invalidate();
        }

        /// <summary>
        /// Mouse click
        /// </summary>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseClick(e);

            // Check if user clicks on scrollbar's cursor
            if (_cursorHover) { _cursorClick = true; }

            // Mouse click position
            _mouseClickPosition = e.Location;

            // Store current cursor position to previous cursor position
            _cursorPositionPrevious = _cursorPosition;
        }

        /// <summary>
        /// Mouse release
        /// </summary>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            // Reset cursor click
            _cursorClick = false;
        }

        /// <summary>
        /// Mouse enter
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _hover = true;
            _visible = true;
        }

        /// <summary>
        /// Mouse leave
        /// </summary>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _cursorHover = false;
            _hover = false;
        }
    }
}
