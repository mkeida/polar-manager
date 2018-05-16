using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalLibrary.External
{
    public class Hex
    {
        /// <summary>
        /// Value of hex
        /// </summary>
        private string value;

        /// <summary>
        /// Value property
        /// </summary>
        public string Value
        {
            get { return value; }
            set { value = this.value; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="hex">HEX color value</param>
        public Hex(string hex)
        {
            this.value = hex;
        }

        public Hex(Color color)
        {
            this.value = "#" + color.R.ToString("X2") + color.G.ToString("X2") + color.B.ToString("X2");
        }

        /// <summary>
        /// Converts given HEX color to System.Drawing.Color
        /// </summary>
        /// <returns>Converted HEX color</returns>
        public Color ToColor()
        {
            return ColorTranslator.FromHtml(value);
        }

        /// <summary>
        /// Converts Hex to string HEX value
        /// </summary>
        /// <returns></returns>
        public string ToHexString()
        {
            return Value;
        }

        // Defined colors
        public static Hex Red = new Hex("#E53935");
        public static Hex Pink = new Hex("#D81B60");
        public static Hex Purple = new Hex("#8E24AA");
        public static Hex DeepPurple = new Hex("#5E35B1");
        public static Hex Indigo = new Hex("#3949AB");
        public static Hex Blue = new Hex("#1E88E5");
        public static Hex SeaBlue = new Hex("#304FFE");
        public static Hex LightBlue = new Hex("#039BE5");
        public static Hex Cyan = new Hex("#00ACC1");
        public static Hex Teal = new Hex("#00897B");
        public static Hex Green = new Hex("#43A047");
        public static Hex LightGreen = new Hex("#7CB342");
        public static Hex Lime = new Hex("#C0CA33");
        public static Hex Yellow = new Hex("#FDD835");
        public static Hex Amber = new Hex("#FFB300");
        public static Hex Orange = new Hex("#FB8C00");
        public static Hex DeepOrange = new Hex("#F4511E");
        public static Hex Brown = new Hex("#6D4C41");
        public static Hex Grey = new Hex("#757575");
        public static Hex BlueGray = new Hex("#546E7A");
        public static Hex Black = new Hex("#000000");
        public static Hex White = new Hex("#FFFFFF");
    }
}
