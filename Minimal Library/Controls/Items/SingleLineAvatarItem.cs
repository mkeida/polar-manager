using System.Drawing;
using MinimalLibrary.Scaling;
using MinimalLibrary.Themes;

namespace MinimalLibrary.Controls.Items
{
    /// <summary>
    /// Single line item with letter avatar
    /// </summary>
    public class SingleLineAvatarItem : MItem
    {
        /// <summary>
        /// Default source theme used for control drawing
        /// </summary>
        private Theme _sourceTheme;

        /// <summary>
        /// Constructor
        /// </summary>
        public SingleLineAvatarItem(string primaryText)
        {
            base.PrimaryText = primaryText;
        }

        /// <summary>
        /// Draw
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
            if (Bounds.Contains(owner.MousePosition))
            {
                // Partially transparent tint layer
                g.FillRectangle(new SolidBrush(Color.FromArgb(25, owner.Tint)), Bounds);
            }

            // Avatar
            g.FillRectangle(new SolidBrush(owner.Tint), new Rectangle(new Point(Bounds.X + 10, Bounds.Y + (Bounds.Height / 2) - 16), new Size(32, 32)));
            StringFormat sfAvatar = new StringFormat();
            sfAvatar.LineAlignment = StringAlignment.Center;
            sfAvatar.Alignment = StringAlignment.Center;
            g.DrawString(PrimaryText[0].ToString(), new Font("Segoe UI Light", 12), new SolidBrush(Color.White), new Rectangle(new Point(Bounds.X + 10, Bounds.Y + (Bounds.Height / 2) - 16), new Size(32, 32)), sfAvatar);

            // Draw primary text
            StringFormat sfPrimary = new StringFormat();
            sfPrimary.LineAlignment = StringAlignment.Center;
            g.DrawString(PrimaryText, new Font("Segoe UI", 9), new SolidBrush(_sourceTheme.CONTROL_FOREGROUND.Normal.ToColor()), new Rectangle(Bounds.X + 48, Bounds.Y + 1, Bounds.Width + 50, Bounds.Height), sfPrimary);
        }
    }
}
