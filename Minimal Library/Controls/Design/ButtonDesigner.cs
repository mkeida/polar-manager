using System.Collections;
using System.Windows.Forms.Design;

namespace MinimalLibrary.Controls.Design
{
    /// <summary>
    /// Button designer class
    /// </summary>
    partial class ButtonDesigner : ControlDesigner
    {
        /// <summary>
        /// Filter properties
        /// </summary>
        /// <param name="properties"></param>
        protected override void PreFilterProperties(IDictionary properties)
        {
            // Removes unnecessary inherited Button class properties
            properties.Remove("BackColor");
            properties.Remove("BackgroundImage");
            properties.Remove("BackgroundImageLayout");
            properties.Remove("FlatAppearance");
            properties.Remove("FlatStyle");
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
