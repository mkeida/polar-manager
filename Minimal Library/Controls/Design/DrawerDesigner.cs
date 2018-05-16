using System.Collections;
using System.Windows.Forms.Design;

namespace MinimalLibrary.Controls.Design
{
    /// <summary>
    /// Drawer designer class
    /// </summary>
    partial class DrawerDesigner : ControlDesigner
    {
        /// <summary>
        /// Filter properties
        /// </summary>
        /// <param name="properties"></param>
        protected override void PreFilterProperties(IDictionary properties)
        {
            // Removes unnecessary inherited class properties
            properties.Remove("BackColor");
            properties.Remove("BackgroundImage");
            properties.Remove("BackgroundImageLayout");
            properties.Remove("BorderStyle");
            properties.Remove("RightToLeft");
            properties.Remove("BackColor");

            // Push new properties
            base.PreFilterProperties(properties);
        }
    }
}
