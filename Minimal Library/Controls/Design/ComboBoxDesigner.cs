using System.Collections;
using System.Windows.Forms.Design;


namespace MinimalLibrary.Controls.Design
{
    /// <summary>
    /// ComboBox designer class
    /// </summary>
    partial class ComboBoxDesigner : ControlDesigner
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
            properties.Remove("RightToLeft");

            // Push new properties
            base.PreFilterProperties(properties);
        }
    }
}
