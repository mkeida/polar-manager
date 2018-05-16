using System.Drawing;
using MinimalLibrary.External;
using MinimalLibrary.Internal;
using MinimalLibrary.Scaling;
using MinimalLibrary.Themes;

namespace MinimalLibrary.Controls.Items
{
    /// <summary>
    /// Two line item
    /// </summary>
    public class TwoLineItem : MItem
    {
        /// <summary>
        /// Secondary text
        /// </summary>
        private string _secondaryText;

        /// <summary>
        /// Default source theme used for item drawing
        /// </summary>
        private Theme _sourceTheme;

        /// <summary>
        /// Secondary text property
        /// </summary>
        public string SecondaryText
        {
            get { return _secondaryText; }
            set { _secondaryText = value; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public TwoLineItem(string primaryText, string secondaryText)
        {
            // Primary text, seocondary text, item height
            base.PrimaryText = primaryText;
            this.SecondaryText = secondaryText;
        }

        /// <summary>
        /// Draw item
        /// </summary>
        public override void DrawItem(MListBox owner, Graphics g, Rectangle itemBounds)
        {
            // DIP
            DIP.GetGraphics(g);

            // Basic varibles
            Height = DIP.Set(70);
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

            // Draw primary text
            StringFormat sfPrimary = new StringFormat();
            sfPrimary.LineAlignment = StringAlignment.Center;
            g.DrawString(PrimaryText, new Font("Segoe UI", 9), new SolidBrush((Bounds.Contains(owner.MousePosition) ? owner.Tint : _sourceTheme.CONTROL_FOREGROUND.Normal.ToColor())), new Rectangle(Bounds.X + 10, Bounds.Y + 5, Bounds.Width, Bounds.Height / 2 + 1), sfPrimary);

            // Draw secondary text
            StringFormat sfSecondary = new StringFormat();
            sfSecondary.LineAlignment = StringAlignment.Center;
            Color textColor = (_sourceTheme.DARK_BASED) ? MColor.AddRGB(-120, _sourceTheme.CONTROL_FOREGROUND.Normal.ToColor()) : MColor.AddRGB(100, _sourceTheme.CONTROL_FOREGROUND.Normal.ToColor());
            g.DrawString(SecondaryText, new Font("Segoe UI", 8), new SolidBrush(textColor), new Rectangle(Bounds.X + 10, Bounds.Y + Bounds.Height / 2 - 9, Bounds.Width, Bounds.Height / 2 + 5), sfSecondary);
        }
    }
}
