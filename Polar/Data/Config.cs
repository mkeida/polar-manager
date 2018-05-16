using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinimalLibrary.Themes;

namespace Polar.Data
{
    public class Config
    {
        /// <summary>
        /// Application theme
        /// </summary>
        public Theme AppTheme { get; set; }

        /// <summary>
        /// Drawer theme
        /// </summary>
        public Theme DrawerTheme { get; set; }

        /// <summary>
        /// Used tint
        /// </summary>
        public Color Tint { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Config(Theme appTheme, Theme drawerTheme, Color tint)
        {
            AppTheme = appTheme;
            DrawerTheme = drawerTheme;
            Tint = tint;
        }
    }
}
