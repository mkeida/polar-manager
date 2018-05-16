using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MinimalLibrary.Themes;
using MinimalLibrary.Internal;
using MinimalLibrary.External;
using MinimalLibrary.Scaling;

namespace MinimalLibrary.Controls
{
    public partial class MLabeledTextBox : UserControl
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
                    _textBox.Tint = value;
                    TintChanged?.Invoke(this, new PropertyChangedEventArgs("TintChanged"));
                    Invalidate();
                }
            }
        }

        /// <summary>
        /// Prefix 
        /// </summary>
        private string _prefix;

        /// <summary>
        /// Prefix changed event handler
        /// </summary>
        [Category("Property Changed")]
        [Description("Fires when the prefix is changed.")]
        public event PropertyChangedEventHandler PrefixChanged;

        /// <summary>
        /// Prefix property
        /// </summary>
        [Category("Appearance")]
        [Description("Prefix of control group")]
        public string Prefix
        {
            get { return _prefix; }
            set
            {
                if (value != _prefix)
                {
                    _prefix = value;
                    PrefixChanged?.Invoke(this, new PropertyChangedEventArgs("PrefixChangedArgs"));
                    Invalidate();
                }

                // Calls paint method
                Invalidate();
            }
        }

        /// <summary>
        /// Postfix 
        /// </summary>
        private string _postfix;

        /// <summary>
        /// Postfix changed event handler
        /// </summary>
        [Category("Property Changed")]
        [Description("Fires when the postfix is changed.")]
        public event PropertyChangedEventHandler PostfixChanged;

        /// <summary>
        /// Prefix property
        /// </summary>
        [Category("Appearance")]
        [Description("Postfix of control group")]
        public string Postfix
        {
            get { return _postfix; }
            set
            {
                if (value != _postfix)
                {
                    _postfix = value;
                    PostfixChanged?.Invoke(this, new PropertyChangedEventArgs("PostfixChangedArgs"));
                    Invalidate();
                }

                // Calls paint method
                Invalidate();
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
            get { return _textBox.Text; }
            set
            {
                _textBox.Text = value;
                TextChanged?.Invoke(this, new PropertyChangedEventArgs("TextChanged"));

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
            get { return _textBox.PasswordChar; }
            set
            {
                _textBox.PasswordChar = value;

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
            get { return _textBox.Multiline; }
            set
            {
                _textBox.Multiline = value;

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
            get { return _textBox.MaxLength; }
            set
            {
                _textBox.MaxLength = value;

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
            get { return _textBox.AcceptsReturn; }
            set
            {
                _textBox.AcceptsReturn = value;

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
            get { return _textBox.AcceptsTab; }
            set
            {
                _textBox.AcceptsTab = value;

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
            get { return _textBox.CharacterCasing; }
            set
            {
                _textBox.CharacterCasing = value;

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
            get { return _textBox.HideSelection; }
            set
            {
                _textBox.HideSelection = value;

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
            get { return _textBox.ReadOnly; }
            set
            {
                _textBox.ReadOnly = value;

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
            get { return _textBox.ShortcutsEnabled; }
            set
            {
                _textBox.ShortcutsEnabled = value;

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
            get { return _textBox.UseSystemPasswordChar; }
            set
            {
                _textBox.UseSystemPasswordChar = value;

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
            get { return _textBox.WordWrap; }
            set
            {
                _textBox.WordWrap = value;

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
        /// Timer of the button
        /// </summary>
        private Timer _controlTimer;

        /// <summary>
        /// Default source theme used for control drawing
        /// </summary>
        private Theme _sourceTheme;

        /// <summary>
        /// TextBox
        /// </summary>
        private MTextBox _textBox = new MTextBox();

        /// <summary>
        /// Prefix size in pixels
        /// </summary>
        private Size _prefixSize;

        /// <summary>
        /// Postfix size in pixels
        /// </summary>
        private Size _postfixSize;

        /// <summary>
        /// Prefix area
        /// </summary>
        private Rectangle _prefixRectangle;

        /// <summary>
        /// Postfix area
        /// </summary>
        private Rectangle _postfixRectangle;

        /// <summary>
        /// Tint alpha
        /// </summary>
        private byte _tintAlpha;


        /// <summary>
        /// Construcotor
        /// </summary>
        public MLabeledTextBox()
        {
            InitializeComponent();
            DIP.GetGraphics(this);

            // Default variables
            _controlTimer = new Timer();
            _controlTimer.Enabled = true;
            _controlTimer.Interval = 1;
            _controlTimer.Tick += new EventHandler(Update);
            _controlTimer.Start();
            _usedTheme = null;
            _tintAlpha = 0;
            DoubleBuffered = true;
            _textBox.TextChanged += new PropertyChangedEventHandler(OnTextChanged);
            PrefixChanged += OnPrefixChange;
            PostfixChanged += OnPostfixChanged;      
            _textBox.Font = new Font("Segoe UI", 9);
            Controls.Add(_textBox);
            Height = _textBox.Height;
            Prefix = "";
            Postfix = "";
            UpdateLayout();
        }

        /// <summary>
        /// Update method
        /// </summary>
        protected void Update(object sender, EventArgs e)
        {
            // Hover effect
            if (_textBox.FakeTextBox.Focused)
            {
                if (_tintAlpha < 255) { _tintAlpha += 15; }
            }
            else
            {
                if (_tintAlpha > 0) { _tintAlpha -= 15; }
            }

            // Redraw
            Invalidate();
        }

        /// <summary>
        /// Draw method
        /// </summary>
        /// <param name="e">Paint event arguments</param>
        protected override void OnPaint(PaintEventArgs e)
        {
            // Base painting
            base.OnPaint(e);

            // Graphics
            Graphics g = e.Graphics;

            // Clear control
            g.Clear(Parent.BackColor);

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

            // Colors
            Color fill = new Color();
            Color border = new Color();
            Color fore = new Color();

            if (Enabled)
            {
                fill = _sourceTheme.CONTROL_FILL.Normal.ToColor();
                border = MColor.Mix(Color.FromArgb(_tintAlpha, _tint), _sourceTheme.CONTROL_BORDER.Normal.ToColor());
                fore = _sourceTheme.CONTROL_FOREGROUND.Normal.ToColor();
            }
            else
            {
                fill = _sourceTheme.CONTROL_FILL.Disabled.ToColor();
                border = _sourceTheme.CONTROL_BORDER.Disabled.ToColor();
                fore = _sourceTheme.CONTROL_FOREGROUND.Disabled.ToColor();
            }

            // Fill control
            g.FillRectangle(new SolidBrush(fill), ClientRectangle);

            // Handles prefix
            if (_prefix != "")
            {
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                g.DrawString(_prefix, this.Font, new SolidBrush(fore), _prefixRectangle, sf);
            }

            // Handles postfix
            if (_postfix != "")
            {
                StringFormat sf = new StringFormat();
                sf.Alignment = StringAlignment.Center;
                sf.LineAlignment = StringAlignment.Center;
                g.DrawString(_postfix, this.Font, new SolidBrush(fore), _postfixRectangle, sf);
            }

            // Draw borders
            g.DrawRectangle(new Pen(border), new Rectangle(new Point(0, 0), new Size(ClientRectangle.Width - 1, ClientRectangle.Height - 1)));
        }

        /// <summary>
        /// Updates prefix, postfix and textBox positions
        /// </summary>
        private void UpdateLayout()
        {
            try
            {
                // Size of postfix and prefix in pixels
                _prefixSize = TextRenderer.MeasureText(_prefix, this.Font);
                _postfixSize = TextRenderer.MeasureText(_postfix, this.Font);

                // Prefix
                // Prefix is not empty string
                if (_prefix != "")
                {
                    // Prefix area rectangle
                    _prefixRectangle = new Rectangle(new Point(0, 0), new Size(DIP.ToInt(10) + _prefixSize.Width, this.Height));
                    _textBox.DrawLeftBorder = false;
                }
                else
                {
                    // Empty area
                    _prefixRectangle = new Rectangle(new Point(0, 0), new Size(0, 0));
                    _textBox.DrawLeftBorder = true;
                }

                // Adjust textBox size
                _textBox.Size = new Size(this.Width - _prefixRectangle.Width - _postfixRectangle.Width, this.Height);
                _textBox.Location = new Point(_prefixRectangle.X + _prefixRectangle.Width, 0);

                // Postfix
                // Postfix is not empty string
                if (_postfix != "")
                {
                    // Postfix area rectangle
                    _postfixRectangle = new Rectangle(new Point(_prefixRectangle.X + _prefixRectangle.Width + _textBox.Width, 0), new Size(DIP.ToInt(10) + _postfixSize.Width, this.Height));
                    _textBox.DrawRightBorder = false;
                }
                else
                {
                    // Empty area
                    _postfixRectangle = new Rectangle(new Point(0, 0), new Size(0, 0));
                    _textBox.DrawRightBorder = true;
                }

                // Adjust textBox size
                _textBox.Size = new Size(this.Width - _prefixRectangle.Width - _postfixRectangle.Width, this.Height);
                _textBox.Location = new Point(_prefixRectangle.X + _prefixRectangle.Width, 0);

                // Redraw
                Invalidate();
            }
            catch
            {

            }
        }

        /// <summary>
        /// Prefix changed
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e">Event arguments</param>
        private void OnPrefixChange(object sender, PropertyChangedEventArgs e)
        {
            UpdateLayout();
            Invalidate();
        }

        /// <summary>
        /// Postfix changed
        /// </summary>
        /// <param name="sender">Event sender</param>
        /// <param name="e"><Event arguments/param>
        private void OnPostfixChanged(object sender, PropertyChangedEventArgs e)
        {
            UpdateLayout();
            Invalidate();
        }

        /// <summary>
        /// Size changed event
        /// </summary>
        /// <param name="e"></param>
        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            this.Height = _textBox.Height;
            _prefixSize = TextRenderer.MeasureText(_prefix, this.Font);
            _postfixSize = TextRenderer.MeasureText(_postfix, this.Font);

            // Prefix is not empty string
            UpdateLayout();
        }

        /// <summary>
        /// Fake _textBox text change
        /// </summary>
        protected void OnTextChanged(object sender, EventArgs e)
        {
            TextChanged?.Invoke(this, new PropertyChangedEventArgs("TextChanged"));
        }

        /// <summary>
        /// On got focus
        /// </summary>
        /// <param name="e"></param>
        protected override void OnGotFocus(EventArgs e)
        {
            base.OnGotFocus(e);
            _textBox.Focus();
        }
    }
}
