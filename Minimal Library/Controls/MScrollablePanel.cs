using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MinimalLibrary.Themes;

namespace MinimalLibrary.Controls
{
    /// <summary>
    /// Double buffered panel with custom scrollbar
    /// </summary>
    [Designer("MinimalLibrary.Controls.Design.ScrollablePanelDesigner")]
    public partial class MScrollablePanel : UserControl
    {
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
        //  PANEL
        //

        /// <summary>
        /// Double buffered panel
        /// </summary>
        MBufferedPanel _panel;

        /// <summary>
        /// Panel
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
        public MBufferedPanel Panel
        {
            get { return _panel; }
            set { _panel = value; }
        }

        //
        //  VERTICAL SCROLLBAR
        //

        /// <summary>
        /// Vertical scrollbar
        /// </summary>
        MScrollbarVertical _verticalScrollbar;

        /// <summary>
        /// Scrollbar
        /// </summary>
        public MScrollbarVertical Scrollbar
        {
            get { return _verticalScrollbar; }
            set { _verticalScrollbar = value; }
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
        /// Scrollbar visible
        /// </summary>
        private bool _scrollbarVisible = true;

        /// <summary>
        /// Scrollbar visible changed event handler
        /// </summary>
        [Category("MinimalLibrary")]
        [Description("Fires when the ScrollbarVisible is changed.")]
        public event PropertyChangedEventHandler ScrollbarVisibleChanged;

        /// <summary>
        /// Scrollbar visible property
        /// </summary>
        public bool ScrollbarVisible
        {
            get { return _scrollbarVisible; }
            set
            {
                _scrollbarVisible = value;
                ScrollbarVisibleChanged?.Invoke(this, new PropertyChangedEventArgs("ScrollbarVisibleChanged"));

                if (_scrollbarVisible)
                {
                    Controls.Add(_verticalScrollbar);
                }
                else
                {
                    Controls.Remove(_verticalScrollbar);
                }
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public MScrollablePanel()
        {
            InitializeComponent();

            // Default variables
            _controlTimer = new Timer();
            _controlTimer.Interval = 1;
            _controlTimer.Tick += new EventHandler(Update);
            _controlTimer.Start();
            _panel = new MBufferedPanel();
            _panel.Dock = DockStyle.Fill;
            _panel.AutoScroll = true;
            _panel.Scroll += new ScrollEventHandler(PanelScrollbarScroll);
            _panel.MouseWheel += new MouseEventHandler(PanelWheelScroll);
            _panel.AutoScrollPosition = new Point(0, 0);
            _verticalScrollbar = new MScrollbarVertical();
            _verticalScrollbar.Dock = DockStyle.Right;
            _verticalScrollbar.Scroll += new EventHandler(ScrollbarScroll);
            _usedTheme = null;

            // Adds panel and scrollbar
            Controls.Add(_panel);
            Controls.Add(_verticalScrollbar);

            // Redraw control
            Invalidate(true);
        }

        /// <summary>
        /// On handle created
        /// </summary>
        /// <param name="e"></param>
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            _verticalScrollbar.Minimum = 0;
            _verticalScrollbar.Maximum = _panel.DisplayRectangle.Height - _panel.Size.Height;

            // Calculate cursor length
            CalculateCursorLength();

            // Call resize
            this.OnResize(EventArgs.Empty);

            // Redraw control
            Invalidate(true);
        }

        /// <summary>
        /// Update method
        /// </summary>
        protected void Update(object sender, EventArgs e)
        {
            // Redraw control and its childs
            Invalidate(true);

            // Turn off timer
            _controlTimer.Stop();
        }

        /// <summary>
        /// Draw method
        /// </summary>
        /// <param name="e"></param>
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

            Panel.BackColor = _sourceTheme.CONTROL_BACKGROUND.Normal.ToColor();
            BackColor = _sourceTheme.CONTROL_BACKGROUND.Normal.ToColor();
        }

        /// <summary>
        /// Panel mouse wheel scroll
        /// </summary>
        private void PanelWheelScroll(object sender, MouseEventArgs e)
        {
            _verticalScrollbar.Value = Math.Abs(_panel.AutoScrollPosition.Y);
        }

        /// <summary>
        /// Panel default scrollbar scroll
        /// </summary>
        private void PanelScrollbarScroll(object sender, EventArgs e)
        {
            _verticalScrollbar.Value = Math.Abs(_panel.AutoScrollPosition.Y);
        }

        /// <summary>
        /// Scrollbar scroll
        /// </summary>
        private void ScrollbarScroll(object sender, EventArgs e)
        {
            _panel.AutoScrollPosition = new Point(0, _verticalScrollbar.Value);
        }

        /// <summary>
        /// Resize control
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            _verticalScrollbar.Minimum = 0;
            _verticalScrollbar.Maximum = _panel.DisplayRectangle.Height - _panel.Size.Height;

            // Calculate cursor length
            CalculateCursorLength();
        }

        /// <summary>
        /// Calculate cursor length
        /// </summary>
        private void CalculateCursorLength()
        {
            // Calculate cursor length
            if (_panel.DisplayRectangle.Height > 0)
            {
                decimal viewableRatio = Convert.ToDecimal(_panel.Size.Height) / Convert.ToDecimal(_panel.DisplayRectangle.Height);
                decimal scrollBarArea = _verticalScrollbar.Height;
                decimal thumbHeight = scrollBarArea * viewableRatio;
                _verticalScrollbar.CursorLength = Convert.ToInt32(thumbHeight);
            }
        }
    }
}
