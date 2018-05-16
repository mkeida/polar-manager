using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.Specialized;
using System.Collections.ObjectModel;
using MinimalLibrary.External;
using MinimalLibrary.Internal;
using MinimalLibrary.Controls.Items;
using MinimalLibrary.Themes;

namespace MinimalLibrary.Controls
{
    public partial class MComboBox : MBufferedPanel
    {
        private string _text;

        public string DefaultText
        {
            get { return _text; }
            set
            {
                _text = value;
                _header.Text = value;
            }
        }


        //
        //  ITEMS
        //

        /// <summary>
        /// Items
        /// </summary>
        private ObservableCollection<MItem> _items = new ObservableCollection<MItem>();

        /// <summary>
        /// Items property
        /// </summary>
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public ObservableCollection<MItem> Items
        {
            get { return _items; }
            set { _items = value; }
        }

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
        //  TINT
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
                _header.UsedTheme = value;
                _listBox.UsedTheme = value;

                // Fire event
                UsedThemeChanged?.Invoke(this, new PropertyChangedEventArgs("UsedThemeChanged"));

                // Redraw control
                Invalidate(true);
            }
        }

        //
        //  DISPLAY SEARCH
        //

        /// <summary>
        /// Display search local variable
        /// </summary>
        private bool _displaySearch;

        /// <summary>
        /// DisplaySearch changed event
        /// </summary>
        [Category("MinimalLibrary")]
        [Description("Fires when UsedTheme is changed.")]
        public event PropertyChangedEventHandler DisplaySearchChanged;

        /// <summary>
        /// DisplaySearch property
        /// </summary>
        public bool DisplaySearch
        {
            get { return _displaySearch; }
            set
            {
                // Change used theme
                _displaySearch = value;

                // Fire event
                DisplaySearchChanged?.Invoke(this, new PropertyChangedEventArgs("DisplaySearchChanged"));

                if (_displaySearch)
                {
                    _container.Controls.Add(_search);
                }
                else
                {
                    _container.Controls.Remove(_search);
                }

                // Redraw control
                Invalidate(true);
            }
        }

        //
        //  SELECTED ITEM
        //

        /// <summary>
        /// Selected item
        /// </summary>
        public MItem SelectedItem = null;

        /// <summary>
        /// Selected item changed event hadnler
        /// </summary>
        [Category("MinimalLibrary")]
        [Description("Fires when SelectedItem is changed.")]
        public event PropertyChangedEventHandler SelectedItemChanged;

        /// <summary>
        /// ComboBox head
        /// </summary>
        private MComboBoxHeader _header;

        /// <summary>
        /// ListBox for Items
        /// </summary>
        private MListBox _listBox;

        /// <summary>
        /// Searchbar
        /// </summary>
        private MTextBox _search;

        /// <summary>
        /// Container
        /// </summary>
        private Panel _container;

        /// <summary>
        /// Timer of the control
        /// </summary>
        private Timer _controlTimer;

        /// <summary>
        /// Default source theme used for control drawing
        /// </summary>
        private Theme _sourceTheme;

        /// <summary>
        /// True if comboBox is opened
        /// </summary>
        private bool _opened;

        /// <summary>
        /// Animation time in miliseconds
        /// </summary>
        private double _t;

        /// <summary>
        /// Dropdown length
        /// </summary>
        private int _dropdownLength;

        /// <summary>
        /// True if the mouse is inside of the control
        /// </summary>
        private bool _hover;

        /// <summary>
        /// Alpha value of the hover effect which is later added to control fill color
        /// </summary>
        private byte _hoverAlpha;

        /// <summary>
        /// Tint alpha
        /// </summary>
        private byte _tintAlpha;

        /// <summary>
        /// Constructor
        /// </summary>
        public MComboBox()
        {
            InitializeComponent();

            // Default variables
            _controlTimer = new Timer();
            _controlTimer.Interval = 1;
            _controlTimer.Tick += new EventHandler(Update);
            _controlTimer.Start();
            _header = new MComboBoxHeader();
            _header.Height = 35;
            _header.Click += new EventHandler(HeaderClick);
            _header.Dock = DockStyle.Top;
            _header.Tint = Tint;
            _header.LostFocus += new EventHandler(FocusLost);
            _header.MouseEnter += new EventHandler(HeaderMouseEnter);
            _header.MouseLeave += new EventHandler(HeaderMouseLeave);
            _search = new MTextBox();
            _search.Dock = DockStyle.Top;
            _search.DrawLeftBorder = false;
            _search.DrawRightBorder = false;
            _search.DrawTopBorder = false;
            _search.TextChanged += SearchTextChanged;
            _listBox = new MListBox();
            _listBox.Dock = DockStyle.Fill;
            _listBox.SelectedItemChanged += ListBoxSelectedItemChanged;
            _listBox.Tint = Tint;
            _listBox.LostFocus += new EventHandler(FocusLost);
            _opened = false;
            _t = 1;
            _dropdownLength = 200;
            _usedTheme = null;
            Height = _header.Height;
            Padding = new Padding(1);
            BackColor = Tint;
            Items.CollectionChanged += ItemsChanged;

            _container = new Panel();
            _container.BackColor = Color.Red;
            _container.Dock = DockStyle.Fill;

            Controls.Add(_container);
            Controls.Add(_header);

            _container.Controls.Add(_listBox);
        }

        /// <summary>
        /// Parent changed
        /// </summary>
        /// <param name="e"></param>
        protected override void OnParentChanged(EventArgs e)
        {
            if (Parent != null)
            {
                base.OnParentChanged(e);
                Parent.Click += ParentClick;
            }
        }

        /// <summary>
        /// Parent click
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ParentClick(object sender, EventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Update
        /// </summary>
        protected void Update(object sender, EventArgs e)
        {
            // Animation
            _t += 0.05;
            if (_t > 1) { _t = 1; }

            // Opening and closing
            if (_opened)
            {
                // Opening
                Height = _header.Height + Animation.CosinusMotion(_t, _dropdownLength);
            }
            else
            {
                // Closing
                Height = _dropdownLength + _header.Height + 2 - Animation.CosinusMotion(_t, _dropdownLength);
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

            // Alpha
            if (Focused || _listBox.Focused || _header.Focused || _search.Focused || _search.FakeTextBox.Focused)
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

            // Border color
            Color fill = MColor.Mix(Color.FromArgb(_hoverAlpha, _sourceTheme.CONTROL_FILL.Hover.ToColor()), _sourceTheme.CONTROL_FILL.Normal.ToColor());
            Color frameColor = MColor.Mix(Color.FromArgb(_tintAlpha, _tint), fill);
            BackColor = frameColor;
        }

        /// <summary>
        /// Selected item changed
        /// </summary>
        private void ListBoxSelectedItemChanged(object sender, PropertyChangedEventArgs e)
        {
            SelectedItem = _listBox.SelectedItem;
            SelectedItemChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedItemChanged"));
            _header.Text = _listBox.SelectedItem.PrimaryText;
            _header.Invalidate();
            Close();
        }

        /// <summary>
        /// Items changed
        /// </summary>
        private void ItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            _listBox.Items = new ObservableCollection<MItem>(Items);
        }

        /// <summary>
        /// Searching
        /// </summary>
        private void SearchTextChanged(object sender, PropertyChangedEventArgs e)
        {
            _listBox.Filter = _search.Text;
        }

        /// <summary>
        /// Click event
        /// </summary>
        private void HeaderClick(object sender, EventArgs e)
        {
            _t = 0;
            if (_opened) { Close(); } else { Open(); }
        }

        /// <summary>
        /// Header and listBox focus lost
        /// </summary>
        private void FocusLost(object sender, EventArgs e)
        {
            if (_search.Focused || _search.FakeTextBox.Focused)
            {
                return;
            }

            Close();
        }

        /// <summary>
        /// On lost focus
        /// </summary>
        /// <param name="e"></param>
        protected override void OnLostFocus(EventArgs e)
        {
            base.OnLostFocus(e);

            if (_search.Focused || _search.FakeTextBox.Focused)
            {
                return;
            }

            Close();
        }

        /// <summary>
        /// Resize event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            _header.Width = Width - 2;
        }


        /// <summary>
        /// OnMouseEnter method. Check if mouse is inside of button
        /// </summary>
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _hover = true;
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
        /// Header mouse enter
        /// </summary>
        private void HeaderMouseEnter(object sender, EventArgs e)
        {
            _hover = true;
        }

        /// <summary>
        /// Header mouse leave
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void HeaderMouseLeave(object sender, EventArgs e)
        {
            _hover = false;
        }

        /// <summary>
        /// Opens dropdown
        /// </summary>
        public void Open()
        {
            if (!_opened)
            {
                BringToFront();
                _opened = true;
                _t = 0;
                _search.Text = "";
            }
        }

        /// <summary>
        /// Closes dropdown
        /// </summary>
        public void Close()
        {
            if (_opened)
            {
                _opened = false;
                _t = 0;
            }
        }
    }
}
