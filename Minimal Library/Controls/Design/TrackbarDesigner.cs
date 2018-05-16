using System.Collections;
using System.Windows.Forms.Design;

namespace MinimalLibrary.Controls.Design
{
    /// <summary>
    /// Trackbar designer class
    /// </summary>
    partial class TrackbarDesigner : ControlDesigner
    {
        /// <summary>
        /// Filter properties
        /// </summary>
        /// <param name="properties"></param>
        protected override void PreFilterProperties(IDictionary properties)
        {
            // Removes unnecessary inherited Control class properties
            properties.Remove("BackColor");
            properties.Remove("BackgroundImage");
            properties.Remove("BackgroundImageLayout");
            properties.Remove("ForeColor");
            properties.Remove("RightToLeft");
            properties.Remove("Font");

            // Push new properties
            base.PreFilterProperties(properties);
        }
    }
}
