using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Collections.ObjectModel;
using MinimalLibrary.External;
using MinimalLibrary.Internal;
using MinimalLibrary.Themes;
using MinimalLibrary.Scaling;

namespace MinimalLibrary.Controls
{
    /// <summary>
    /// Side menu
    /// </summary>
    [Designer("MinimalLibrary.Controls.Design.DrawerDesigner")]
    public partial class MDrawer : MBufferedPanel
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

                    // Refresh items tint
                    for (int i = 0; i < Items.Count; i++)
                    {
                        DrawerItem item = Items[i];
                        item.Tint = this.Tint;
                        item.Invalidate();
                    }

                    Invalidate();
                }
            }
        }

        //
        //  SELECTED ITEM
        //

        /// <summary>
        /// Default selected item
        /// </summary>
        private DrawerItem _selectedItem = new DrawerItem("");

        /// <summary>
        /// Selected item changed event
        /// </summary>
        [Category("MinimalLibrary")]
        [Description("Fires when the SelectedItem is changed")]
        public event PropertyChangedEventHandler SelectedItemChanged;

        /// <summary>
        /// Selected item property
        /// </summary>
        [Category("MinimalLibrary")]
        [Description("Selected Item of SideMenu")]
        public DrawerItem SelectedItem
        {
            get { return _selectedItem; }
            set
            {
                if (value != _selectedItem)
                {
                    _selectedItem = value;
                    SelectedItemChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedItemChanged"));

                    _clickedItemPoint.Y = _selectedItem.Location.Y;
                    _tempSliderPosition.Y = _selectedItem.Location.Y;
                    _sliderPosition.Y = _selectedItem.Location.Y;

                    Invalidate();
                }
            }
        }

        //
        //  COLLAPSED
        //

        /// <summary>
        /// Default hidden boolean
        /// </summary>
        private bool _collapsed = true;

        /// <summary>
        /// Hidden changed event
        /// </summary>
        [Category("MinimalLibrary")]
        [Description("Fires when the Collapsed is changed")]
        public event PropertyChangedEventHandler CollapsedChanged;

        /// <summary>
        /// Hidden item property
        /// </summary>
        [Category("MinimalLibrary")]
        [Description("Collapsed property. True if menu should be hidden.")]
        public bool Collapsed
        {
            get { return _collapsed; }
            set
            {
                if (value != _collapsed)
                {
                    _collapsed = value;
                    CollapsedChanged?.Invoke(this, new PropertyChangedEventArgs("CollapsedChanged"));
                    Invalidate();
                }
            }
        }

        //
        //  ABOVE CONTENT
        //

        /// <summary>
        /// Default above content prop
        /// </summary>
        private bool _aboveContent = false;

        /// <summary>
        /// AboveContent changed event
        /// </summary>
        [Category("MinimalLibrary")]
        [Description("Fires when the AboveContent is changed")]
        public event PropertyChangedEventHandler AboveContentChanged;

        /// <summary>
        /// AboveContent property
        /// </summary>
        [Category("MinimalLibrary")]
        [Description("True if menu should be above content.")]
        public bool AboveContent
        {
            get { return _aboveContent; }
            set
            {
                if (value != _aboveContent)
                {
                    _aboveContent = value;
                    AboveContentChanged?.Invoke(this, new PropertyChangedEventArgs("AboveContent"));
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

        //
        //  ITEMS   
        //

        /// <summary>
        /// Items
        /// </summary>
        public ObservableCollection<DrawerItem> Items;

        /// <summary>
        /// Timer of the control
        /// </summary>
        private Timer _controlTimer;

        /// <summary>
        /// Default source theme used for control drawing
        /// </summary>
        private Theme _sourceTheme;

        /// <summary>
        /// Side panel used for animation
        /// </summary>
        MBufferedPanel _sidePanel;

        /// <summary>
        /// Slider position
        /// </summary>
        private Point _sliderPosition;

        /// <summary>
        /// Temporary slider position
        /// </summary>
        private Point _tempSliderPosition;

        /// <summary>
        /// Clicked item position
        /// </summary>
        private Point _clickedItemPoint;

        /// <summary>
        /// Animation of slider move progress in miliseconds
        /// </summary>
        private double _tSlider;

        /// <summary>
        /// Animation of drawer menu in miliseconds
        /// </summary>
        private double _tDrawerMenu;

        /// <summary>
        /// Menu item
        /// </summary>
        public DrawerItem MenuItem;

        /// <summary>
        /// Constructor
        /// </summary>
        public MDrawer()
        {
            InitializeComponent();

            // Side panel
            _sidePanel = new MBufferedPanel();
            _sidePanel.Width = 5;
            _sidePanel.Location = new Point(this.Location.X, this.Location.Y);
            _sidePanel.Paint += SidePanelPaint;
            this.Controls.Add(_sidePanel);

            // Default variables
            _controlTimer = new Timer();
            _controlTimer.Interval = 1;
            _controlTimer.Tick += new EventHandler(Update);
            _controlTimer.Start();
            _sliderPosition = new Point();
            _collapsed = true;
            _tempSliderPosition = new Point(0, 0);
            _clickedItemPoint = new Point(0, 0);
            _tSlider = 0;
            _tDrawerMenu = 1;
            Items = new ObservableCollection<DrawerItem>();
            Items.CollectionChanged += new System.Collections.Specialized.NotifyCollectionChangedEventHandler(ItemsChanged);
            Width = 65 + 5;
            Dock = DockStyle.Left;

            // Refresh items
            RefreshItems();
        }

        /// <summary>
        /// On handle created
        /// </summary>
        /// <param name="e"></param>
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            // Menu item
            MenuItem = new DrawerItem("");
            MenuItem.FullColored = true;
            MIcon icon = MIcon.Menu;
            icon.DarkBased = true;
            MenuItem.Icon = icon;
            MenuItem.Click += MenuClick;
            Items.Insert(0, MenuItem);
            MenuItem.TriggerResize();
            this.OnResize(EventArgs.Empty);
            RefreshItems();
            Invalidate(true);

            _clickedItemPoint.Y = _selectedItem.Location.Y;
            _tempSliderPosition.Y = _selectedItem.Location.Y;
            _sliderPosition.Y = _selectedItem.Location.Y;
        }

        /// <summary>
        /// Menu click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void MenuClick(object sender, EventArgs e)
        {
            if (_collapsed) { Open(); } else { Close(); }
        }

        /// <summary>
        /// Items changed 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ItemsChanged(object sender, EventArgs e)
        {
            // Refresh all items
            RefreshItems();
        }

        /// <summary>
        /// Update
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Update(object sender, EventArgs e)
        {
            // Animation timer - cursor
            _tSlider += 0.05;
            if (_tSlider > 1) { _tSlider = 1; }

            // Animation timer - drawer menu
            _tDrawerMenu += 0.05;
            if (_tDrawerMenu > 1) { _tDrawerMenu = 1; }

            // Update slider position
            if (_clickedItemPoint.Y > _tempSliderPosition.Y)
            {
                _sliderPosition.Y = _tempSliderPosition.Y + Animation.CosinusMotion(_tSlider, (int)Geometry.GetDistanceBetweenPoints(_clickedItemPoint, _tempSliderPosition));
            }
            else
            {
                _sliderPosition.Y = _tempSliderPosition.Y - Animation.CosinusMotion(_tSlider, (int)Geometry.GetDistanceBetweenPoints(_clickedItemPoint, _tempSliderPosition));
            }

            // Handles menu resizing (hidden property)
            if (_collapsed)
            {
                Width = 300 - Animation.CosinusMotion(_tDrawerMenu, 300 - (65 + _sidePanel.Width));
            }
            else
            {
                Width = 65 + Animation.CosinusMotion(_tDrawerMenu, 300 - (65 + _sidePanel.Width));
            }

            // Redraw
            _sidePanel.Invalidate();
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

            // Background
            g.Clear(_sourceTheme.WINDOW_CAPTIONBAR.Normal.ToColor());

            // Tint
            MenuItem.Tint = Tint;

            // Side panel
            _sidePanel.BackColor = _sourceTheme.CONTROL_BACKGROUND.Normal.ToColor();
        }

        /// <summary>
        /// Side panel draw method
        /// </summary>
        private void SidePanelPaint(object sender, PaintEventArgs e)
        {
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

            g.Clear(_sourceTheme.WINDOW_CAPTIONBAR.Normal.ToColor());

            if (Items.Count > 0)
            {
                g.FillRectangle(new SolidBrush(_tint), new Rectangle(_sliderPosition.X, _sliderPosition.Y, _sidePanel.Width, this.SelectedItem.Height));
            }

            // Draw menu item side
            g.FillRectangle(new SolidBrush(_tint), new Rectangle(0, 0, _sidePanel.Width, 65));
        }

        /// <summary>
        /// Refresh items
        /// </summary>
        private void RefreshItems()
        {
            Controls.Clear();
            Controls.Add(_sidePanel);

            if (Items.Count > 0)
            {
                Items[0].Selected = true;
            }

            // Adds menu items
            for (int i = 0; i < Items.Count; i++)
            {
                DrawerItem item = Items[i];
                item.Owner = this;
                item.Location = new Point(_sidePanel.Width, i * 65);
                item.Tint = this.Tint;
                Controls.Add(item);
            }
        }

        /// <summary>
        /// Resize
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            _sidePanel.Height = Height;
        }

        /// <summary>
        /// Click on item
        /// </summary>
        public void ItemClick(DrawerItem clickedItem)
        {
            if (clickedItem != MenuItem)
            {
                // Refresh items click
                if (clickedItem != _selectedItem)
                {
                    for (int i = 0; i < Items.Count; i++)
                    {
                        DrawerItem item = Items[i];
                        item.Selected = false;
                        item.Invalidate();
                    }

                    _tSlider = 0;
                    _tempSliderPosition = _sliderPosition;
                    _clickedItemPoint = clickedItem.Location;
                    _selectedItem = clickedItem;
                }
            }
        }

        /// <summary>
        /// Opens menu
        /// </summary>
        public void Open()
        {
            if (_aboveContent) { BringToFront(); }
            Collapsed = false;
            _tDrawerMenu = 0;
        }
        
        /// <summary>
        /// Closes menu
        /// </summary>
        public void Close()
        {
            Collapsed = true;
            _tDrawerMenu = 0;
        }
    }

    /// <summary>
    /// Side menu item
    /// </summary>
    [ToolboxItem(false)]
    public class DrawerItem : UserControl
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
        //  CLICK EFFECT
        //

        /// <summary>
        /// Default click effect
        /// </summary>
        private ClickEffect _clickEffect = ClickEffect.Ink;

        /// <summary>
        /// Control ClickEffect changed event handler
        /// </summary>
        [Category("Property Changed")]
        [Description("Fires when the click effect type is changed.")]
        public event PropertyChangedEventHandler ClickEffectChanged;

        /// <summary>
        /// ClickEffect property
        /// </summary>
        [Category("MinimalLibrary")]
        [Description("ClickEffect of control")]
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
        //  ICON
        //

        /// <summary>
        /// Default icon
        /// </summary>
        private MIcon _icon = null;

        /// <summary>
        /// Icon changed event handler
        /// </summary>
        [Category("Property Changed")]
        [Description("Fires when the Icon is changed.")]
        public event PropertyChangedEventHandler IconChanged;

        /// <summary>
        /// ClickEffect property
        /// </summary>
        [Category("MinimalLibrary")]
        [Description("Icon of control.")]
        public External.MIcon Icon
        {
            get { return _icon; }
            set
            {
                if (value != _icon)
                {
                    _icon = value;
                    IconChanged?.Invoke(this, new PropertyChangedEventArgs("IconChanged"));
                }

                // Calls paint method
                Invalidate();
            }
        }

        //
        //  FULLCOLORED
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
        /// True if control should be fully painted with his Tint color
        /// </summary>
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
        //  OWNER
        //

        /// <summary>
        /// Owner
        /// </summary>
        private MDrawer _owner;

        /// <summary>
        /// Property owner
        /// </summary>
        internal MDrawer Owner
        {
            get { return _owner; }
            set { _owner = value; }
        }

        /// <summary>
        /// Timer of the control
        /// </summary>
        private Timer _controlTimer;

        /// <summary>
        /// True if item is selected
        /// </summary>
        public bool Selected;

        /// <summary>
        /// True if the mouse is inside of the control
        /// </summary>
        private bool _hover;

        /// <summary>
        /// Alpha value of the hover effect which is later added to control fill color
        /// </summary>
        private byte _hoverAlpha;

        /// <summary>
        /// Mouse possition relative to the control's top left corner
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
        /// Default source theme used for control drawing
        /// </summary>
        private Theme _sourceTheme;

        /// <summary>
        /// Constructor
        /// </summary>
        public DrawerItem(string text)
        {
            Text = text;

            // Default variables
            _controlTimer = new Timer();
            _controlTimer.Interval = 1;
            _controlTimer.Tick += new EventHandler(Update);
            _controlTimer.Start();
            _increment = 1;
            _hoverAlpha = 0;
            _hover = false;
            Selected = false;

            // Optimize
            DoubleBuffered = true;
            Size = new Size(300, 65);
            Font = new Font("Segoe UI", 10, FontStyle.Regular);
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

            // Call draw method
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
            Graphics g = e.Graphics;
            DIP.GetGraphics(g);

            // Handles control's source theme
            // Check if control has set own theme
            if (Owner.UsedTheme != null)
            {
                // Set custom theme as source theme
                _sourceTheme = Owner.UsedTheme;
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

            // Draw item background
            Color fill = (Owner.MenuItem == this) ? _sourceTheme.WINDOW_CAPTIONBAR.Normal.ToColor() : MColor.Mix(Color.FromArgb(_hoverAlpha, MColor.Lighten(230, _tint, _sourceTheme.WINDOW_CAPTIONBAR.Normal.ToColor())), _sourceTheme.WINDOW_CAPTIONBAR.Normal.ToColor());
            g.FillRectangle(new SolidBrush(fill), new Rectangle(0, 0, Width, Height));

            if (FullColored)
            {
                g.FillRectangle(new SolidBrush(MColor.Mix(Color.FromArgb(_hoverAlpha, MColor.AddRGB((_sourceTheme.DARK_BASED) ? +(_hoverAlpha / 15) : -(_hoverAlpha / 15), _tint)), _tint)), new Rectangle(0, 0, Owner.MenuItem.Height, Owner.MenuItem.Height));
            }

            // Click effect
            DrawClick(e);

            // Icon mode
            if (_icon != null && !_fullColored)
            {
                if (_sourceTheme.DARK_BASED)
                {
                    _icon.DarkBased = true;
                }
                else
                {
                    _icon.DarkBased = false;
                }
            }

            // Draw Icon
            if (_icon != null)
            {
                
                g.InterpolationMode = InterpolationMode.NearestNeighbor;

                if (Owner.SelectedItem == this)
                {
                    Image img = MColor.SetImageColor(_icon.Icon, _tint);
                    g.DrawImage(MColor.SetImageOpacity(img, 1f), 16, 16, 32, 32);
                }
                else
                {
                    g.DrawImage(MColor.SetImageOpacity(_icon.Icon, 1f), 16, 16, 32, 32);
                }
            }
            else
            {
                // Icon rect
                g.DrawRectangle(new Pen(_sourceTheme.CONTROL_FOREGROUND.Normal.ToColor()), new Rectangle(20, 20, 20, 20));
            }

            // Draw text
            SolidBrush brush = (Owner.SelectedItem == this) ? new SolidBrush(_tint) : (_fullColored) ? new SolidBrush(ForeColor) : new SolidBrush(_sourceTheme.CONTROL_FOREGROUND.Normal.ToColor());
            g.DrawString(Text, Font, brush, new Rectangle(65, (Height / 2) - (Font.Height / 2), Width, Height));
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
            if (Owner.UsedTheme != null)
            {
                // Set custom theme as source theme
                _sourceTheme = Owner.UsedTheme;
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
                Color fill = _tint;

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
        /// Click event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);
            Owner.ItemClick(this);
            Selected = true;
            Invalidate();

            // Set up ClickEffect variables
            _mouse = PointToClient(MousePosition);

            // ClickEffect
            // Ink
            if (_clickEffect == ClickEffect.Ink) { _radius = ((Owner.Collapsed) ? 65 : Width) / 5; }

            // Square
            if (_clickEffect == ClickEffect.Square || _clickEffect == ClickEffect.SquareRotate) { _radius = ((Owner.Collapsed) ? 65 : Width / 8); }

            // Resets alpha
            _alpha = 25;
        }

        /// <summary>
        /// Mouse hower
        /// </summary>
        /// <param name="e"></param>
        protected override void OnMouseHover(EventArgs e)
        {
            base.OnMouseHover(e);
        }

        /// <summary>
        /// OnMouseEnter method. Check if mouse is inside of item
        /// </summary>
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _hover = true;

            // Turn on timer
            _controlTimer.Start();
        }

        /// <summary>
        /// OnMouseLeave method. Check if mouse is outside of item
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
        /// Trigger resize event
        /// </summary>
        internal void TriggerResize()
        {
            this.OnResize(EventArgs.Empty);
        }
    }
}
