using System.Drawing;
using MinimalLibrary.Scaling;
using MinimalLibrary.Themes;

namespace MinimalLibrary.Controls.Items
{
    /// <summary>
    /// Single line item
    /// </summary>
    public class SingleLineItem : MItem
    {
        /// <summary>
        /// Default source theme used for item drawing
        /// </summary>
        private Theme _sourceTheme;

        /// <summary>
        ///  Constructor
        /// </summary>
        public SingleLineItem(string primaryText)
        {

            // Primary text
            base.PrimaryText = primaryText;
        }

        /// <summary>
        /// Draw item
        /// </summary>
        public override void DrawItem(MListBox owner, Graphics g, Rectangle itemBounds)
        {
            // DIP
            DIP.GetGraphics(g);

            // Basic varibles
            Height = DIP.Set(50);
            Bounds = itemBounds;
            Graphics = g;

            // Handles control's source theme
            // Check if control has set own theme
            if (owner.UsedTheme != null)
            {
                // Set custom theme as source theme
                _sourceTheme = owner.UsedTheme;
            }
            else
            {
                // Control dont have its own theme
                // Try cast control's parent form to MForm
                try
                {
                    MForm form = (MForm)owner.FindForm();
                    _sourceTheme = form.UsedTheme;
                }
                catch
                {
                    // Control's parent form is not MForm type
                    // Set application wide theme
                    _sourceTheme = Minimal.UsedTheme;
                }
            }

            // Background
            g.FillRectangle(new SolidBrush(_sourceTheme.CONTROL_BACKGROUND.Normal.ToColor()), Bounds);

            // Item have focus || mouse hover
            if (itemBounds.Contains(owner.MousePosition))
            {
                // Partially transparent tint layer
                g.FillRectangle(new SolidBrush(Color.FromArgb(25, owner.Tint)), Bounds);
            }

            // Draw text
            StringFormat sf = new StringFormat();
            sf.LineAlignment = StringAlignment.Center;
            g.DrawString(PrimaryText, new Font("Segoe UI", 9), new SolidBrush(_sourceTheme.CONTROL_FOREGROUND.Normal.ToColor()), new Rectangle(Bounds.X + 10, Bounds.Y, Bounds.Width, Bounds.Height), sf);
        }
    }
}
