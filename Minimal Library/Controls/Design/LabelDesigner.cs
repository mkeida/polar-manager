using System.Collections;
using System.Windows.Forms.Design;

namespace MinimalLibrary.Controls.Design
{
    /// <summary>
    /// Label designer class
    /// </summary>
    partial class LabelDesigner : ControlDesigner
    {
        /// <summary>
        /// Filter properties
        /// </summary>
        /// <param name="properties"></param>
        protected override void PreFilterProperties(IDictionary properties)
        {
            // Removes unnecessary inherited Label class properties
            properties.Remove("BackColor");
            properties.Remove("FlatStyle");
            properties.Remove("ForeColor");
            properties.Remove("Image");
            properties.Remove("ImageAlign");
            properties.Remove("ImageIndex");
            properties.Remove("ImageKey");
            properties.Remove("ImageList");
            properties.Remove("TextImageRelation");
            properties.Remove("UseVisualStyleBackColor");
            properties.Remove("RightToLeft");

            // Push new properties
            base.PreFilterProperties(properties);
        }
    }
}
