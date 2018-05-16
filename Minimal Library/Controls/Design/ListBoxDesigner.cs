using System.Collections;
using System.Windows.Forms.Design;

namespace MinimalLibrary.Controls.Design
{
    /// <summary>
    /// ListBox designer class
    /// </summary>
    partial class ListBoxDesigner : ControlDesigner
    {
        /// <summary>
        /// Filter properties
        /// </summary>
        /// <param name="properties"></param>
        protected override void PreFilterProperties(IDictionary properties)
        {
            // Removes unnecessary inherited class properties
            properties.Remove("BackColor");

            // Push new properties
            base.PreFilterProperties(properties);
        }
    }
}
