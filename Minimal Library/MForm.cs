using MinimalLibrary.Internal;
using MinimalLibrary.Themes;
using System.ComponentModel;
using System.Windows.Forms;

namespace MinimalLibrary
{
    /// <summary>
    /// Minimal Form
    /// </summary>
    public partial class MForm : Form
    {
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
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public MForm()
        {
            InitializeComponent();
            UsedTheme = Minimal.Light;
        }
    }
}
