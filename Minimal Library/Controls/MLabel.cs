using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using MinimalLibrary.Internal;
using MinimalLibrary.External;
using MinimalLibrary.Themes;

namespace MinimalLibrary.Controls
{
    /// <summary>
    /// Simple label supporting theming
    /// </summary>
    [Designer("MinimalLibrary.Controls.Design.LabelDesigner")]
    public partial class MLabel : Label
    {
        //
        //  Tint
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
        //  Type
        //

        /// <summary>
        /// Default type
        /// </summary>
        private LabelType _type;

        /// <summary>
        /// Type changed event
        /// </summary>
        [Category("Property Changed")]
        [Description("Fires when the type is changed.")]
        public event PropertyChangedEventHandler TypeChanged;

        /// <summary>
        /// Type property
        /// </summary>
        [Category("MinimalLibrary")]
        [Description("Type of label.")]
        public LabelType Type
        {
            get { return _type; }
            set
            {
                if (value != _type)
                {
                    _type = value;
                    TypeChanged?.Invoke(this, new PropertyChangedEventArgs("AlternateTextChanged"));
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
        /// Constructor
        /// </summary>
        public MLabel()
        {
            InitializeComponent();

            // Default variables
            _controlTimer = new Timer();
            _controlTimer.Interval = 1;
            _controlTimer.Tick += new EventHandler(Update);
            _controlTimer.Start();
            Font = new Font("Segoe UI", 9);
        }

        /// <summary>
        /// Update method
        /// </summary>
        protected void Update(object sender, EventArgs e)
        {
            // Calls draw
            Invalidate();

            // Default variables
            _usedTheme = null;
        }

        /// <summary>
        /// Draw label
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
                // Control dont have it's own theme
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

            // Normal and alternate color
            Color standardTextColor = _sourceTheme.CONTROL_FOREGROUND.Normal.ToColor();
            Color alternateTextColor = (_sourceTheme.DARK_BASED) ? MColor.AddRGB(-120, _sourceTheme.CONTROL_FOREGROUND.Normal.ToColor()) : MColor.AddRGB(100, _sourceTheme.CONTROL_FOREGROUND.Normal.ToColor());
            Color tintTextColor = _tint;

            // Set foreground color
            switch(_type)
            {
                case LabelType.Standard:
                    ForeColor = standardTextColor;
                    break;
                case LabelType.Alternate:
                    ForeColor = alternateTextColor;
                    break;
                case LabelType.Tint:
                    ForeColor = tintTextColor;
                    break;
            }
        }
    }
}
