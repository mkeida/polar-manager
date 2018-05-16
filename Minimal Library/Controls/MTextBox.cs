using System;
using System.Drawing;
using System.ComponentModel;
using System.Windows.Forms;
using MinimalLibrary.Themes;
using MinimalLibrary.External;
using MinimalLibrary.Internal;

namespace MinimalLibrary.Controls
{
    /// <summary>
    /// Minimal text-box
    /// </summary>
    public partial class MTextBox : UserControl
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
        //  TEXT
        //

        /// <summary>
        /// Text changed event handler
        /// </summary>
        [Category("Property Changed")]
        [Browsable(true)]
        [Description("Fires when the text is changed.")]
        public new event PropertyChangedEventHandler TextChanged;

        /// <summary>
        /// Text property
        /// </summary>
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Bindable(true)]
        [Category("Appearance")]
        public override string Text
        {
            get { return FakeTextBox.Text; }
            set
            {
                FakeTextBox.Text = value;
                TextChanged?.Invoke(this, new PropertyChangedEventArgs("TextChanged"));

                // Redraw
                Invalidate();
            }
        }

        //
        //  PLACEHOLDER
        //

        /// <summary>
        /// Placeholder
        /// </summary>
        private string _placeholder = "";

        /// <summary>
        /// Placeholder changed event handler
        /// </summary>
        [Category("Property Changed")]
        [Browsable(true)]
        [Description("Fires when the Placeholder is changed.")]
        public new event PropertyChangedEventHandler PlaceholderChanged;

        /// <summary>
        /// Placeholder property
        /// </summary>
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Bindable(true)]
        [Category("Appearance")]
        public string Placeholder
        {
            get { return _placeholder; }
            set
            {
                _placeholder = value;
                PlaceholderChanged?.Invoke(this, new PropertyChangedEventArgs("PlaceholderChanged"));

                // Redraw
                Invalidate();
            }
        }

        //
        //  PASSWORD CHAR
        //

        /// <summary>
        /// PasswordChar property
        /// </summary>
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Bindable(true)]
        [Category("Behavior")]
        public char PasswordChar
        {
            get { return FakeTextBox.PasswordChar; }
            set
            {
                FakeTextBox.PasswordChar = value;

                // Redraw
                Invalidate();
            }
        }

        //
        // MULTILINE
        //

        /// <summary>
        /// Multiline event handler
        /// </summary>
        [Category("Property Changed")]
        [Browsable(true)]
        [Description("Fires when the Multiline is changed.")]
        public event PropertyChangedEventHandler MultilineChanged;

        /// <summary>
        /// Multiline property
        /// </summary>
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Bindable(true)]
        [Category("Behavior")]
        public bool Multiline
        {
            get { return FakeTextBox.Multiline; }
            set
            {
                FakeTextBox.Multiline = value;

                if (MultilineChanged != null)
                {
                    MultilineChanged(this, new PropertyChangedEventArgs("Multiline"));
                }

                // Redraw
                Invalidate();
            }
        }

        //
        //  MAX LENGTH
        //

        /// <summary>
        /// MaxLength property
        /// </summary>
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Bindable(true)]
        [Category("Behavior")]
        public int MaxLength
        {
            get { return FakeTextBox.MaxLength; }
            set
            {
                FakeTextBox.MaxLength = value;

                // Redraw
                Invalidate();
            }
        }

        //
        //  ACCEPTS RETURN
        //

        /// <summary>
        /// AcceptsReturn property
        /// </summary>
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Bindable(true)]
        [Category("Behavior")]
        public bool AcceptsReturn
        {
            get { return FakeTextBox.AcceptsReturn; }
            set
            {
                FakeTextBox.AcceptsReturn = value;

                // Redraw
                Invalidate();
            }
        }

        //
        // ACCEPTS TAB
        //

        /// <summary>
        /// AcceptsTab property
        /// </summary>
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Bindable(true)]
        [Category("Behavior")]
        public bool AcceptsTab
        {
            get { return FakeTextBox.AcceptsTab; }
            set
            {
                FakeTextBox.AcceptsTab = value;

                // Redraw
                Invalidate();
            }
        }

        //
        //  CHARACTER CASING
        //

        /// <summary>
        /// CharacterCasing property
        /// </summary>
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Bindable(true)]
        [Category("Behavior")]
        public CharacterCasing CharacterCasing
        {
            get { return FakeTextBox.CharacterCasing; }
            set
            {
                FakeTextBox.CharacterCasing = value;

                // Redraw
                Invalidate();
            }
        }

        //
        //  HIDE SELECTION
        //

        /// <summary>
        /// HideSelection event handler
        /// </summary>
        [Category("Property Changed")]
        [Browsable(true)]
        [Description("Fires when the HideSelection is changed.")]
        public event PropertyChangedEventHandler HideSelectionChanged;

        /// <summary>
        /// HideSelection property
        /// </summary>
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Bindable(true)]
        [Category("Behavior")]
        public bool HideSelection
        {
            get { return FakeTextBox.HideSelection; }
            set
            {
                FakeTextBox.HideSelection = value;

                if (HideSelectionChanged != null)
                {
                    HideSelectionChanged(this, new PropertyChangedEventArgs("HideSelection"));
                }

                // Redraw
                Invalidate();
            }
        }

        //
        //  READ ONLY
        //

        /// <summary>
        /// ReadOnly event handler
        /// </summary>
        [Category("Property Changed")]
        [Browsable(true)]
        [Description("Fires when the ReadOnly is changed.")]
        public event PropertyChangedEventHandler ReadOnlyChanged;

        /// <summary>
        /// ReadOnly property
        /// </summary>
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Bindable(true)]
        [Category("Behavior")]
        public bool ReadOnly
        {
            get { return FakeTextBox.ReadOnly; }
            set
            {
                FakeTextBox.ReadOnly = value;

                if (ReadOnlyChanged != null)
                {
                    ReadOnlyChanged(this, new PropertyChangedEventArgs("ReadOnly"));
                }

                // Redraw
                Invalidate();
            }
        }

        //
        //  SHORTCUTS ENABLED
        //

        /// <summary>
        /// ShortcutsEnabled property
        /// </summary>
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Bindable(true)]
        [Category("Behavior")]
        public bool ShortcutsEnabled
        {
            get { return FakeTextBox.ShortcutsEnabled; }
            set
            {
                FakeTextBox.ShortcutsEnabled = value;

                // Redraw
                Invalidate();
            }
        }

        //
        //  USE SYSTEM PASSWORD CHAR
        //

        /// <summary>
        /// UseSystemPasswordChar property
        /// </summary>
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Bindable(true)]
        [Category("Behavior")]
        public bool UseSystemPasswordChar
        {
            get { return FakeTextBox.UseSystemPasswordChar; }
            set
            {
                FakeTextBox.UseSystemPasswordChar = value;

                // Redraw
                Invalidate();
            }
        }

        //
        //  WORD WRAP
        //

        /// <summary>
        /// WordWrap property
        /// </summary>
        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [EditorBrowsable(EditorBrowsableState.Always)]
        [Bindable(true)]
        [Category("Behavior")]
        public bool WordWrap
        {
            get { return FakeTextBox.WordWrap; }
            set
            {
                FakeTextBox.WordWrap = value;

                // WordWrap
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
        /// Inner fake TextBox
        /// </summary>
        internal TextBox FakeTextBox;

        /// <summary>
        /// True if TextBox should have left border
        /// </summary>
        internal bool DrawLeftBorder = true;

        /// <summary>
        /// True if TextBox should have right border
        /// </summary>
        internal bool DrawRightBorder = true;

        /// <summary>
        /// True if TextBox should have right border
        /// </summary>
        internal bool DrawTopBorder = true;

        /// <summary>
        /// Timer of the button
        /// </summary>
        private Timer _controlTimer;

        /// <summary>
        /// Default source theme used for control drawing
        /// </summary>
        private Theme _sourceTheme;

        /// <summary>
        /// Control padding
        /// </summary>
        private int _padding;

        /// <summary>
        /// Tint of the text
        /// </summary>
        private Color _textTint;

        /// <summary>
        /// Tint alpha
        /// </summary>
        private byte _tintAlpha;

        /// <summary>
        /// Mouse position
        /// </summary>
        private Point _mouse;

        /// <summary>
        /// Constructor
        /// </summary>
        public MTextBox()
        {
            InitializeComponent();

            // Default variables
            _controlTimer = new Timer();
            _controlTimer.Enabled = true;
            _controlTimer.Interval = 1;
            _controlTimer.Tick += new EventHandler(Update);
            _controlTimer.Start();
            _padding = 10;
            _textTint = Hex.Blue.ToColor();
            _tintAlpha = 0;
            _usedTheme = null;
            DoubleBuffered = true;

            // Initialize textBox
            FakeTextBox = new TextBox();
            FakeTextBox.Location = new Point(_padding, _padding);
            FakeTextBox.BorderStyle = BorderStyle.None;
            FakeTextBox.Multiline = false;
            FakeTextBox.TextAlign = HorizontalAlignment.Left;
            FakeTextBox.BackColor = Color.White;
            FakeTextBox.TextChanged += new EventHandler(OnTextChanged);
            FakeTextBox.Width = Width - (_padding * 2);
            FakeTextBox.Font = new Font("Segoe UI", 9);

            // Other
            Height = FakeTextBox.Height + (_padding * 2);
            Controls.Add(FakeTextBox);
            Text = "";

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
        }

        /// <summary>
        /// Update
        /// </summary>
        protected void Update(object sender, EventArgs e)
        {
            // Color of text
            _textTint = MColor.Mix(Color.FromArgb(_tintAlpha, _tint), _sourceTheme.CONTROL_FOREGROUND.Normal.ToColor());
            FakeTextBox.ForeColor = _textTint;

            // Alpha
            if (FakeTextBox.Focused)
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

            // Redraw
            Invalidate();
        }

        /// <summary>
        /// Draw
        /// </summary>
        protected override void OnPaint(PaintEventArgs e)
        {
            // Base painting
            base.OnPaint(e);
            Graphics g = e.Graphics;
            Rectangle rc = ClientRectangle;

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

            // Fill color
            Color fill = Enabled ? _sourceTheme.CONTROL_BACKGROUND.Normal.ToColor() : _sourceTheme.CONTROL_BACKGROUND.Disabled.ToColor();

            // TextBox background color
            FakeTextBox.BackColor = fill;

            // Control outline colors
            Color backgroundColor = fill;
            SolidBrush backroundBrush = new SolidBrush(backgroundColor);
            g.FillRectangle(backroundBrush, rc);

            // Draw control outline
            Color frameColor = MColor.Mix(Color.FromArgb(_tintAlpha, Enabled ? _tint : _sourceTheme.CONTROL_BORDER.Disabled.ToColor()), Enabled ? _sourceTheme.CONTROL_BORDER.Normal.ToColor() : _sourceTheme.CONTROL_BORDER.Disabled.ToColor());
            Pen framePen = new Pen(frameColor);

            // Top border
            if (DrawTopBorder) { g.DrawLine(framePen, new Point(0, 0), new Point(Width, 0)); }

            // Bottom border
            g.DrawLine(framePen, new Point(0, Height - 1), new Point(Width, Height - 1));

            // Left border
            if (DrawLeftBorder) { g.DrawLine(framePen, new Point(0, 0), new Point(0, Height - 1)); }

            // Right border
            if (DrawRightBorder) { g.DrawLine(framePen, new Point(Width - 1, 0), new Point(Width - 1, Height - 1)); }
        }

        /// <summary>
        /// Fake _textBox text change
        /// </summary>
        protected void OnTextChanged(object sender, EventArgs e)
        {
            TextChanged?.Invoke(this, new PropertyChangedEventArgs("TextChanged"));
        }

        /// <summary>
        /// TextBox resize
        /// </summary>
        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            // Size of textbox and control
            FakeTextBox.Location = new Point(_padding, _padding);
            FakeTextBox.Width = Width - (_padding * 2);

            if (!FakeTextBox.Multiline)
            {
                Height = FakeTextBox.Height + (_padding * 2);
            }
            else
            {
                FakeTextBox.Height = Height - (_padding * 2);
            }
        }

        /// <summary>
        /// Pass focus to textBox
        /// </summary>
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            FakeTextBox.Focus();
            FakeTextBox.SelectionLength = 0;
            _controlTimer.Start();
        }

        /// <summary>
        /// After click pass focus to textBox
        /// </summary>
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            FakeTextBox.Focus();

            // Mouse position update
            _mouse = new Point(e.X, e.Y);
        }

        /// <summary>
        /// Set cursor icon
        /// </summary>
        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            // Check if mouse is in position of the control
            if (ClientRectangle.Contains(new Point(_mouse.X, _mouse.Y)))
            {
                // Set cursor
                // Cursor.Current = Cursors.IBeam;
            }
        }

        /// <summary>
        /// Font changed
        /// </summary>
        /// <param name="e">Event arguments</param>
        protected override void OnFontChanged(EventArgs e)
        {
            base.OnFontChanged(e);

            // Size of textbox and control
            FakeTextBox.Location = new Point(_padding, _padding);
            FakeTextBox.Width = Width - (_padding * 2);

            if (!FakeTextBox.Multiline)
            {
                Height = FakeTextBox.Height + (_padding * 2);
            }
            else
            {
                FakeTextBox.Height = Height - (_padding * 2);
            }

            Invalidate();
        }
    }
}
