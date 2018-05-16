using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MinimalLibrary.Scaling
{
    /// <summary>
    /// Device Independent Pixel class helps to achieve better viewing experience
    /// across all possible Windows DPI types. 
    /// </summary>
    public class DIP
    {
        /// <summary>
        /// Graphics object
        /// </summary>
        private static Graphics g;

        /// <summary>
        /// Gets Graphics object of passed control. Must be called BEFORE all other methods. Assign value
        /// to static variable g.
        /// </summary>
        /// <param name="control">Control we want to get Graphics object from</param>
        public static void GetGraphics(Control control)
        {
            g = control.CreateGraphics();
        }

        /// <summary>
        /// Gets Graphics object of passed control. Must be called BEFORE all other methods. Assign value
        /// to static variable g.
        /// </summary>
        /// <param name="control">Graphics object</param>
        public static void GetGraphics(Graphics graphics)
        {
            g = graphics;
        }

        /// <summary>
        /// Set DIP
        /// </summary>
        public static int Set(int i)
        {
            // Gets system DPI
            float dpi = g.DpiX;

            // For every 24 DPI there is 0.25 pixel increase. Thats cca 0.01 pixel for 1 DPI. 
            return Convert.ToInt32(i * (dpi) * 0.0104166666666667 * 0.8);
        }

        /// <summary>
        /// Converts given value from pixels to device independent pixels.
        /// </summary>
        /// <param name="i">DIP value to convert</param>
        /// <returns>Int value in normal pixels</returns>
        public static int ToInt(double i)
        {
            // Gets system DPI
            float dpi = g.DpiX;

            // For every 24 DPI there is 0.25 pixel increase. Thats cca 0.01 pixel for 1 DPI. 
            return Convert.ToInt32(i * (dpi) * 0.0104166666666667);
        }

        /// <summary>
        /// Converts given value from pixels to device independent pixels.
        /// </summary>
        /// <param name="i">DIP value to convert</param>
        /// <returns>Double value in normal pixels</returns>
        public static double ToDouble(double i)
        {
            // Gets system DPI
            float dpi = g.DpiX;

            // For every 24 DPI there is 0.25 pixel increase. Thats cca 0.01 pixel for 1 DPI. 
            return i * (dpi) * 0.0104166666666667;
        }
    }
}
