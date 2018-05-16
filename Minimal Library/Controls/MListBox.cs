using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using MinimalLibrary.Controls.Items;
using MinimalLibrary.External;
using MinimalLibrary.Themes;

namespace MinimalLibrary.Controls
{
    /// <summary>
    /// ListBox
    /// </summary>
    [Designer("MinimalLibrary.Controls.Design.ListBoxDesigner")]
    public partial class MListBox : MScrollablePanel
    {
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
            set
            {
                _items = value;
                FilterItems();
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
        /// Selected item changed event handler
        /// </summary>
        [Category("MinimalLibrary")]
        [Description("Fires when SelectedItem is changed.")]
        public event PropertyChangedEventHandler SelectedItemChanged;

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
        [Description("Click effect of listBox.")]
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
                this.Scrollbar.ScrollbarMinified = value;

                // Fire event
                ScrollbarMinifiedChanged?.Invoke(this, new PropertyChangedEventArgs("MinifiedChanged"));

                // Redraw control
                Invalidate(true);
            }
        }

        /// <summary>
        /// Filtered items
        /// </summary>
        public List<MItem> DisplayedItems = new List<MItem>();

        /// <summary>
        /// Filter
        /// </summary>
        private string _filter = "";

        /// <summary>
        /// Filter property
        /// </summary>
        public string Filter
        {
            get { return _filter; }
            set
            {
                _filter = value;
                FilterItems();
            }
        }

        /// <summary>
        /// Mouse position
        /// </summary>
        public Point MousePosition;

        /// <summary>
        /// Last item Y position
        /// </summary>
        int _lastItemY;

        /// <summary>
        /// Default source theme used for control drawing
        /// </summary>
        private Theme _sourceTheme;

        /// <summary>
        /// Constructor
        /// </summary>
        public MListBox()
        {
            InitializeComponent();

            // Basic variables
            MousePosition = new Point(Cursor.Position.X - Panel.AutoScrollPosition.X, Cursor.Position.Y - Panel.AutoScrollPosition.Y);
            _lastItemY = 0;
            _usedTheme = null;
            Panel.Scroll += new ScrollEventHandler(OnPanelScroll);
            Panel.Paint += new PaintEventHandler(OnPanelPaint);
            Panel.MouseMove += new MouseEventHandler(OnPanelMouseMove);
            Panel.MouseLeave += new EventHandler(OnPanelMouseLeave);
            Panel.Click += new EventHandler(OnPanelClick);
            Items.CollectionChanged += ItemsChanged;

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

            // Redraw
            Panel.Invalidate();
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            Invalidate(true);
            this.OnResize(EventArgs.Empty);
        }

        /// <summary>
        /// Items changed
        /// </summary>
        private void ItemsChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            FilterItems();

            Invalidate(true);
            this.OnResize(EventArgs.Empty);
            Refresh();
        }

        /// <summary>
        /// Panel paint
        /// </summary>
        private void OnPanelPaint(object sender, PaintEventArgs e)
        {
            // Transform panel
            e.Graphics.TranslateTransform(Panel.AutoScrollPosition.X, Panel.AutoScrollPosition.Y);

            // Draw items
            DrawItems(e);
        }

        /// <summary>
        /// Draw items
        /// </summary>
        private void DrawItems(PaintEventArgs e)
        {
            // Y position if item
            int y = 0;

            // Draw items
            if (Items.Count > 0)
            {
                foreach (MItem item in DisplayedItems)
                {
                    Rectangle itemBounds = new Rectangle(0, y, Width, item.Height);
                    item.DrawItem(this, e.Graphics, itemBounds);
                    y += item.Height;
                }
            }

            _lastItemY = y;
            Panel.AutoScrollMinSize = new Size(0, _lastItemY);
            this.OnResize(EventArgs.Empty);
        }


        /// <summary>
        /// Panel scroll
        /// </summary>
        private void OnPanelScroll(object sender, ScrollEventArgs e)
        {
            Panel.Invalidate();
        }

        /// <summary>
        /// Mouse move
        /// </summary>
        private void OnPanelMouseMove(object sender, MouseEventArgs e)
        {
            MousePosition = new Point(e.Location.X - Panel.AutoScrollPosition.X, e.Location.Y - Panel.AutoScrollPosition.Y);
            Panel.Invalidate();
        }

        /// <summary>
        /// Panel click
        /// </summary>
        private void OnPanelClick(object sender, EventArgs e)
        {
            if (Items.Count > 0)
            {
                foreach (MItem item in DisplayedItems)
                {
                    if (item.Bounds.Contains(MousePosition))
                    {
                        // Ignore dividers
                        if (item is MDividerItem)
                        {
                            continue;
                        }

                        SelectedItem = item;
                        SelectedItemChanged?.Invoke(this, new PropertyChangedEventArgs("SelectedItemChanged"));
                    }
                }
            }
        }

        /// <summary>
        /// Panel mouse leave
        /// </summary>
        private void OnPanelMouseLeave(object sender, EventArgs e)
        {
            MousePosition = new Point(-1, -1);

            Refresh();
        }

        /// <summary>
        /// Filter items
        /// </summary>
        private void FilterItems()
        {
            DisplayedItems.Clear();

            if (Items.Count > 0)
            {
                foreach (MItem item in Items)
                {
                    if (item.PrimaryText.ToLower().Contains(_filter.ToLower()))
                    {
                        DisplayedItems.Add(item);
                    }
                }
            }

            Refresh();
        }

        /// <summary>
        /// Scrolls to item
        /// </summary>
        public bool ScrollToItem(MItem item)
        {
            if (Items.Count > 0)
            {
                foreach (MItem searchedItem in DisplayedItems)
                {
                    if (searchedItem == item)
                    {
                        Panel.AutoScrollPosition = new Point(0, searchedItem.Bounds.Y);
                        Scrollbar.Value = searchedItem.Bounds.Y;

                        // Success
                        return true;
                    }
                }
            }

            // Fail
            return false;
        }

        public void ItemsClear()
        {
            SelectedItem = null;
            Items.Clear();
            DisplayedItems.Clear();

            //Panel.AutoScrollMinSize = new Size(0, 0);
            //Panel.AutoScrollPosition = new Point(0, 0);

            //this.OnResize(EventArgs.Empty);
            Invalidate(true);
            //Panel.Invalidate(true);
            Refresh();
        }
    }
}
