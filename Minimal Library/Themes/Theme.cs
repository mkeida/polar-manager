namespace MinimalLibrary.Themes
{
    public class Theme
    {
        /// <summary>
        /// Control background color
        /// </summary>
        public ThemeColor CONTROL_BACKGROUND { get; set; }

        /// <summary>
        /// Control foreground color (text color)
        /// </summary>
        public ThemeColor CONTROL_FOREGROUND { get; set; }

        /// <summary>
        /// Fill color of control
        /// </summary>
        public ThemeColor CONTROL_FILL { get; set; }

        /// <summary>
        /// Control border color
        /// </summary>
        public ThemeColor CONTROL_BORDER { get; set; }

        /// <summary>
        /// Control highlight color
        /// </summary>
        public ThemeColor CONTROL_HIGHLIGHT { get; set; }

        /// <summary>
        /// Background color of form
        /// </summary>
        public ThemeColor FORM_BACKGROUND { get; set; }

        /// <summary>
        /// Captionbar color
        /// </summary>
        public ThemeColor WINDOW_CAPTIONBAR { get; set; }

        /// <summary>
        /// Close, minimize and maximize form buttons
        /// </summary>
        public ThemeColor WINDOW_CLOSEBUTTON { get; set; }

        /// <summary>
        /// Close, minimize and maximize form buttons
        /// </summary>
        public ThemeColor WINDOW_FOREGROUND { get; set; }

        /// <summary>
        /// False for light based theme, true for dark based theme
        /// </summary>
        public bool DARK_BASED { get; set; }
    }
}
