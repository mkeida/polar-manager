using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;
using MinimalLibrary.Themes;
using MinimalLibrary.External;
using MinimalLibrary.Internal;

namespace MinimalLibrary.Controls
{
    /// <summary>
    /// Tackbar control
    /// </summary>
    [Designer("MinimalLibrary.Controls.Design.TrackbarDesigner")] 
    public partial class MTrackbar : UserControl
    {
        //
        //  TINT
        //

        /// <summary>
        /// Tint color
        /// </summary>
        private Color _tint;

        /// <summary>
        /// Tint changed event handler
        /// </summary>
        [Category("MinimalLibrary")]
        [Description("Fires when the Tint is changed.")]
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
        //  VALUE
        //

        /// <summary>
        /// Final value
        /// </summary>
        private double _value;

        /// <summary>
        /// Value changed event handler
        /// </summary>
        [Category("MinimalLibrary")]
        [Description("Fires when value is changed.")]
        public event PropertyChangedEventHandler ValueChanged = null;

        /// <summary>
        /// Value property
        /// </summary>s
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Browsable(true)]
        [Category("MinimalLibrary")]
        [Description("Final value of trackbar.")]
        public double Value
        {
            get { return _value; }
            set
            {
                if (value != this._value)
                {
                    this._value = value;
                    HandleTrackButtonPosition();
                    TriggerValueChangedEvent();
                }
            }
        }

        //
        //  MINIMUM
        //

        /// <summary>
        /// Minimum value
        /// </summary>
        private double _minimum;

        /// <summary>
        /// Minimum changed event handler
        /// </summary>
        [Category("MinimalLibrary")]
        [Description("Fires when the Minimum is changed.")]
        public event PropertyChangedEventHandler MinimumChanged;

        /// <summary>
        /// Minimum property
        /// </summary>
        [Category("MinimalLibrary")]
        [Description("Minimum possible value of trackbar.")]
        public double Minimum
        {
            get { return _minimum; }
            set
            {
                if (value != _minimum)
                {
                    _minimum = value;
                    MinimumChanged?.Invoke(this, new PropertyChangedEventArgs("MinimumChanged"));
                    Invalidate();
                    this.Refresh();
                }
            }
        }

        //
        //  MAXIMUM
        //

        /// <summary>
        /// Maximum value
        /// </summary>
        private double _maximum;

        /// <summary>
        /// Maximum changed event handler
        /// </summary>
        [Category("MinimalLibrary")]
        [Description("Fires when the Maximum is changed.")]
        public event PropertyChangedEventHandler MaximumChanged;

        /// <summary>
        /// Maximum property
        /// </summary>
        [Category("MinimalLibrary")]
        [Description("Maximum possible value of trackbar.")]
        public double Maximum
        {
            get { return _maximum; }
            set
            {
                if (value != _maximum)
                {
                    _maximum = value;
                    MaximumChanged?.Invoke(this, new PropertyChangedEventArgs("MaximumChanged"));
                    Invalidate();
                    this.Refresh();
                }
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
        [Description("Automaticaly hides slider when true.")]
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
        //  INCREASE VALUE GRADUALLY
        //

        /// <summary>
        /// Increase value gradually
        /// </summary>
        private bool _increaseValueGradually;

        /// <summary>
        /// Increase value gradually event hadler
        /// </summary>
        [Category("MinimalLibrary")]
        [Description("Fires when the increaseValueGradually is changed.")]
        public event PropertyChangedEventHandler IncreaseValueGraduallyChanged;

        /// <summary>
        /// IncreaseValueGradually property
        /// </summary>
        [Category("MinimalLibrary")]
        [Description("Increases value gradually according to the cosinus curve.")]
        public bool IncreaseValueGradually
        {
            get { return _increaseValueGradually; }
            set
            {
                if (value != _increaseValueGradually)
                {
                    _increaseValueGradually = value;
                    IncreaseValueGraduallyChanged?.Invoke(this, new PropertyChangedEventArgs("IncreaseValueGraduallyChanged"));
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

        /// <summary>
        /// Timer of the control
        /// </summary>
        private Timer _controlTimer;

        /// <summary>
        /// Default source theme used for control drawing
        /// </summary>
        private Theme _sourceTheme = Minimal.UsedTheme;

        /// <summary>
        /// Track-button rectangle
        /// </summary>
        private Rectangle _trackButtonRectangle;

        /// <summary>
        /// Current position of track-button
        /// </summary>
        private Point _trackButtonCurrentPosition;

        /// <summary>
        /// Clicked position of track-button
        /// </summary>
        private Point _trackButtonClickPosition;

        /// <summary>
        /// Temporary track-button position
        /// </summary>
        private Point _tempTrackButtonPosition;

        /// <summary>
        /// Current radius of track-button
        /// </summary>
        private int _trackButtonCurrentRadius;

        /// <summary>
        /// Default track-button radius
        /// </summary>
        private int _trackButtonRadius;

        /// <summary>
        /// Radius of track-button on hover
        /// </summary>
        private int _trackButtonHoverRadius;

        /// <summary>
        /// Track-button pen width
        /// </summary>s
        private int _trackButtonPenWidth;

        /// <summary>
        /// Track-path width
        /// </summary>
        private int _trackPathWidth;

        /// <summary>
        /// Inner padding of slider
        /// </summary>
        private int _padding;

        /// <summary>
        /// True if user clicks on track-button
        /// </summary>
        private bool _trackButtonClick;

        /// <summary>
        /// True if user click on track-path
        /// </summary>
        private bool _trackPathClick;

        /// <summary>
        /// True if mouse hovers over control
        /// </summary>
        private bool _mouseControlHover;

        /// <summary>
        /// Start point of track bar
        /// </summary>
        private Point _trackStart;

        /// <summary>
        /// End point of track bar
        /// </summary>
        private Point _trackEnd;

        /// <summary>
        /// Mouse position
        /// </summary>
        private Point _mouse;

        /// <summary>
        /// Track-apth length
        /// </summary>
        private double _trackPathLength;

        /// <summary>
        /// Increment per pixel
        /// </summary>
        private double _increment;

        /// <summary>
        /// Distance between track-path zero point and track-button
        /// </summary>
        private double _trackButtonDistance;

        /// <summary>
        /// Animation of slider move progress in miliseconds
        /// </summary>
        private double _tTrackButton;

        /// <summary>
        /// Constructor
        /// </summary>
        public MTrackbar()
        {
            InitializeComponent();

            // Default variables
            _controlTimer = new Timer();
            _controlTimer.Interval = 1;
            _controlTimer.Tick += new EventHandler(Update);
            _controlTimer.Start();
            _tint = Hex.Blue.ToColor();
            _value = 0;
            _minimum = 0;
            _maximum = 100;
            _autoHide = false;
            _tempTrackButtonPosition = new Point(0, 0);
            _trackButtonCurrentRadius = 9;
            _trackButtonRadius = 9;
            _trackButtonHoverRadius = 8;
            _trackButtonPenWidth = 4;
            _trackPathLength = Geometry.GetDistanceBetweenPoints(_trackStart, _trackEnd);
            _trackPathWidth = 5;
            _padding = _trackButtonRadius + _trackButtonHoverRadius + 1;
            _trackButtonClick = false;
            _trackButtonCurrentPosition.Y = Height / 2;
            _trackPathClick = false;
            _mouseControlHover = false;
            _tTrackButton = 0;
            _usedTheme = null;
            DoubleBuffered = true;
            Size = new Size(200, 35);

            // Autohide
            if (_autoHide) { _trackButtonCurrentRadius = 0; }
        }

        /// <summary>
        /// Update
        /// </summary>
        protected void Update(object sender, EventArgs e)
        {
            // Animation variables
            _tTrackButton += 0.025;

            // Track button
            _trackButtonRectangle = new Rectangle(_trackButtonCurrentPosition.X - _trackButtonCurrentRadius, _trackButtonCurrentPosition.Y - _trackButtonCurrentRadius, _trackButtonCurrentRadius * 2, _trackButtonCurrentRadius * 2);
            _trackButtonClickPosition.Y = Height / 2;
            _trackButtonCurrentPosition.Y = Height / 2;

            // Update important variables for animation
            if (_trackPathClick)
            {
                _trackButtonClickPosition.X = _mouse.X;
                _tempTrackButtonPosition = _trackButtonCurrentPosition;
                _tTrackButton = 0;

                // Value changed event
                TriggerValueChangedEvent();
            }

            if (_trackButtonClick)
            {
                _trackButtonClickPosition.X = _mouse.X;
                _trackButtonCurrentPosition.X = _mouse.X;

                // Value changed event
                TriggerValueChangedEvent();
            }

            // Limit real minimal horizontal position of slider from left, so half
            // of track button don't overlap track bar
            if (_trackButtonClickPosition.X < _trackStart.X)
            {
                _trackButtonClickPosition.X = _trackStart.X;

                // Value changed event
                TriggerValueChangedEvent();
            }

            if (_trackButtonCurrentPosition.X < _trackStart.X)
            {
                _trackButtonCurrentPosition.X = _trackStart.X;

                // Value changed event
                TriggerValueChangedEvent();
            }

            // Limit real minimal horizontal position of slider from right, so half
            // of track button don't overlap track bar
            if (_trackButtonClickPosition.X > _trackEnd.X)
            {
                _trackButtonClickPosition.X = _trackEnd.X;

                // Value changed event
                TriggerValueChangedEvent();
            }

            if (_trackButtonCurrentPosition.X > _trackEnd.X)
            {
                _trackButtonCurrentPosition.X = _trackEnd.X;

                // Value changed event
                TriggerValueChangedEvent();
            }

            // Animation of track button when user clicks on track bar
            if (_trackButtonCurrentPosition.X < _trackButtonClickPosition.X)
            {
                _trackButtonCurrentPosition.X = _tempTrackButtonPosition.X + Animation.CosinusMotion(_tTrackButton, (int)Geometry.GetDistanceBetweenPoints(_tempTrackButtonPosition, _trackButtonClickPosition));

                // Value changed event
                TriggerValueChangedEvent();
            }

            if (_trackButtonCurrentPosition.X > _trackButtonClickPosition.X)
            {
                _trackButtonCurrentPosition.X = _tempTrackButtonPosition.X - Animation.CosinusMotion(_tTrackButton, (int)Geometry.GetDistanceBetweenPoints(_trackButtonClickPosition, _tempTrackButtonPosition));

                // Value changed event
                TriggerValueChangedEvent();
            }

            // Handles auto-hide
            if (_autoHide)
            {
                // Reduce track-button size if mouse is not hovering over it
                if (!_mouseControlHover)
                {
                    if (_trackButtonCurrentRadius > 0)
                    {
                        // Reduce radius
                        _trackButtonCurrentRadius--;
                    }
                }
                else
                {
                    // Increase radius
                    if (_trackButtonCurrentRadius < _trackButtonRadius) {  _trackButtonCurrentRadius++; }
                }
            }

            // If autohide is not active - fix autohide toggle
            if (!_autoHide)
            {
                if (_trackButtonCurrentRadius < _trackButtonRadius)
                {
                    _trackButtonCurrentRadius++;
                }
            }

            // Limit animation variables
            if (_tTrackButton > 1) { _tTrackButton = 1; }

            // Distance between trackbar start point and track button
            _trackButtonDistance = (int)Geometry.GetDistanceBetweenPoints(_trackStart, (_increaseValueGradually) ? _trackButtonCurrentPosition : _trackButtonClickPosition);

            // Length of track bar
            _trackPathLength = (int)Geometry.GetDistanceBetweenPoints(_trackStart, _trackEnd);

            // Increment per pixel
            _increment = (_maximum - _minimum) / _trackPathLength;

            // Value of slider
            _value = _minimum + (int)Math.Round((_trackButtonDistance) * _increment, MidpointRounding.AwayFromZero);

            // Redraw
            Invalidate();
        }

        /// <summary>
        /// Draw method
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            // Base painting
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

            // Set antialiasing
            g.SmoothingMode = SmoothingMode.AntiAlias;

            // Draw track-path
            Color trackColor = _sourceTheme.CONTROL_FILL.Normal.ToColor();
            Pen trackPen = new Pen(trackColor, _trackPathWidth);
            trackPen.StartCap = LineCap.Round;
            trackPen.EndCap = LineCap.Round;
            g.DrawLine(trackPen, _trackStart, _trackEnd);

            // Track_button hover effect
            if (_trackButtonRectangle.Contains(_mouse) && !_trackButtonClick)
            {
                SolidBrush trackButtonBrushActive = new SolidBrush(Color.FromArgb(25, _tint));
                GraphicsPath trackButtonEllipseCenterActive = Draw.GetEllipsePath(_trackButtonCurrentPosition, _trackButtonCurrentRadius + _trackButtonHoverRadius);
                g.FillPath(trackButtonBrushActive, trackButtonEllipseCenterActive);
            }

            // Draw track-button
            Pen trackButtonPen = new Pen(_tint, _trackButtonPenWidth);
            GraphicsPath trackButtonEllipse = Draw.GetEllipsePath(_trackButtonCurrentPosition, _trackButtonCurrentRadius);
            if (_trackButtonCurrentRadius > 0) { g.DrawPath(trackButtonPen, trackButtonEllipse); }

            // Repaint track-path with tint color
            Pen trackTintPen = new Pen(_tint, _trackPathWidth);
            trackTintPen.StartCap = LineCap.Round;
            trackTintPen.EndCap = LineCap.Round;
            g.DrawLine(trackTintPen, _trackStart, new Point(_trackButtonCurrentPosition.X, _trackButtonCurrentPosition.Y));

            // Erase middle of track-button
            Color trackButtonColor = Parent.BackColor;
            SolidBrush trackButtonBrush = new SolidBrush(trackButtonColor);
            GraphicsPath trackButtonEllipseCenter = Draw.GetEllipsePath(_trackButtonCurrentPosition, _trackButtonCurrentRadius - 1);
            if (_trackButtonCurrentRadius > 0) { g.FillPath(trackButtonBrush, trackButtonEllipseCenter); }

            // Track-button click effect
            if (_trackButtonClick)
            {
                SolidBrush trackButtonBrushActive = new SolidBrush(_tint);
                GraphicsPath trackButtonEllipseCenterActive = Draw.GetEllipsePath(_trackButtonCurrentPosition, _trackButtonCurrentRadius - _trackButtonPenWidth + 4);
                g.FillPath(trackButtonBrushActive, trackButtonEllipseCenterActive);
            }

            // Ends antialiasing
            g.SmoothingMode = SmoothingMode.Default; 
        }

        /// <summary>
        /// Mouse click
        /// </summary>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            // If mouse hovers over track-button
            if (_trackButtonRectangle.Contains(e.X, e.Y)) { _trackButtonClick = true; }

            // If mouse hocers over track-path
            if (new Rectangle(0, Height / 2 - 6, Width - _padding, _trackPathWidth + 8).Contains(e.X, e.Y)) { _trackPathClick = true; }
        }

        /// <summary>
        /// Mouse release
        /// </summary>
        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            _trackButtonClick = false;
            _trackPathClick = false;
        }

        /// <summary>
        /// Mouse position update
        /// </summary>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);
            _mouse = new Point(e.X, e.Y);

            if (_trackButtonClick)
            {
                _trackButtonClickPosition = _mouse;
                TriggerValueChangedEvent();
            }

            // Check if mouse is in control area
            if (ClientRectangle.Contains(_mouse))
            {
                _mouseControlHover = true;
            }
        }

        /// <summary>
        /// Mouse leave
        /// </summary>
        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _mouseControlHover = false;
        }

        /// <summary>
        /// Resize
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            // Start and end points
            _trackStart = new Point(_padding, Height / 2);
            _trackEnd = new Point(Width - _padding, Height / 2);

            // Limit size
            if (Width < 100) { Width = 100; }
            Height = 35;

            // Recalculate track-button position
            HandleTrackButtonPosition();
        }

        /// <summary>
        /// Handle track-button position
        /// </summary>
        private void HandleTrackButtonPosition()
        {
            _trackPathLength = Geometry.GetDistanceBetweenPoints(_trackStart, _trackEnd);
            _increment = _trackPathLength / (_maximum - _minimum);
            _trackButtonClickPosition.X = Convert.ToInt32(_value * _increment) + _padding - (int)(_minimum * _increment);
            _trackButtonCurrentPosition.X = Convert.ToInt32(_value * _increment) + _padding - (int)(_minimum * _increment);

            // Redraw
            Invalidate();
        }

        /// <summary>
        /// Trigger value changed event
        /// </summary>
        private void TriggerValueChangedEvent()
        {
            ValueChanged?.Invoke(this, new PropertyChangedEventArgs("ValueChanged"));
        }

        /// <summary>
        /// Start animation
        /// </summary>
        public void Animate()
        {
            _tTrackButton = 0;
            _trackButtonCurrentPosition = new Point(_trackStart.X, Height / 2);
            _tempTrackButtonPosition = new Point(_trackButtonCurrentPosition.X, _trackButtonCurrentPosition.Y);
            if (_trackButtonCurrentPosition.X < _trackButtonClickPosition.X) { _trackButtonCurrentPosition.X = _tempTrackButtonPosition.X + Animation.CosinusMotion(_tTrackButton, (int)Geometry.GetDistanceBetweenPoints(_tempTrackButtonPosition, _trackButtonClickPosition)); }
            if (_trackButtonCurrentPosition.X > _trackButtonClickPosition.X) { _trackButtonCurrentPosition.X = _tempTrackButtonPosition.X - Animation.CosinusMotion(_tTrackButton, (int)Geometry.GetDistanceBetweenPoints(_trackButtonClickPosition, _tempTrackButtonPosition)); }
        }
    }
}
