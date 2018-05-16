using MinimalLibrary.Internal;
using MinimalLibrary.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalLibrary.External
{
    public class MIcon
    {
        /// <summary>
        /// Image 8
        /// </summary>
        public Image Icon = null;

        /// <summary>
        /// True if icon is darkbased
        /// </summary>
        private bool _darkBased = false;

        /// <summary>
        /// Dark based changed event handler
        /// </summary>
        public event PropertyChangedEventHandler DarkBasedChanged;

        /// <summary>
        /// DarkBased property
        /// </summary>
        public bool DarkBased
        {
            get { return _darkBased; }
            set
            {
                if (value != _darkBased)
                {
                    _darkBased = value;

                    if (_darkBased)
                    {
                        try { Icon = MColor.SetImageColor(Icon, Color.White); } catch { }
                    }
                    else
                    {
                        try { Icon = MColor.SetImageColor(Icon, Color.Black); } catch { }
                    }

                    DarkBasedChanged?.Invoke(this, new PropertyChangedEventArgs("DarkBasedChanged"));
                }
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public MIcon(Image image)
        {
            Icon = image;
        }

        public static MIcon Menu = new MIcon(Resources.menu32);
        public static MIcon Home = new MIcon(Resources.home32);
        public static MIcon Marker = new MIcon(Resources.marker32);
        public static MIcon TreasureMap = new MIcon(Resources.treasureMap32);
        public static MIcon Slider = new MIcon(Resources.slider32);
        public static MIcon Key = new MIcon(Resources.key32);
        public static MIcon Bear = new MIcon(Resources.bear32);
        public static MIcon Footprint = new MIcon(Resources.footprint32);
        public static MIcon Koala = new MIcon(Resources.koala32);
        public static MIcon Panda = new MIcon(Resources.panda32);
        public static MIcon Search = new MIcon(Resources.search32);
    }
}
