using System.Drawing;

namespace MinimalLibrary.Controls.Items
{
    /// <summary>
    /// Minimal Item
    /// </summary>
    abstract public class MItem
    {
        /// <summary>
        /// Graphics
        /// </summary>
        public Graphics Graphics { get; set; }

        /// <summary>
        /// Primary text
        /// </summary>
        public string PrimaryText { get; set; }

        /// <summary>
        /// Height
        /// </summary>
        public int Height { get; set; }

        /// <summary>
        /// Bounds
        /// </summary>
        public Rectangle Bounds { get; set; }


        /// <summary>
        /// Draw item method
        /// </summary>
        abstract public void DrawItem(MListBox owner, Graphics g, Rectangle itemBounds);
    }
}
