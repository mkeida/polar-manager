using System.Drawing;
using MinimalLibrary.External;
using MinimalLibrary.Internal;
using MinimalLibrary.Scaling;
using MinimalLibrary.Themes;

namespace MinimalLibrary.Controls.Items
{
    public class TextDividerItem : MDividerItem
    {
        /// <summary>
        /// Default source theme used for control drawing
        /// </summary>
        private Theme _sourceTheme;

        public TextDividerItem(string primaryText)
        {
            base.PrimaryText = primaryText;
        }

        public override void DrawItem(MListBox owner, Graphics g, Rectangle itemBounds)
        {
            // DIP
            DIP.GetGraphics(g);

            // Basic varibles
            Height = DIP.Set(30);
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
            g.FillRectangle(new SolidBrush((_sourceTheme.DARK_BASED) ? MColor.AddRGB(5, _sourceTheme.CONTROL_BACKGROUND.Normal.ToColor()) : MColor.AddRGB(-5, _sourceTheme.CONTROL_BACKGROUND.Normal.ToColor())), Bounds);

            // Draw primary text
            Color color = _sourceTheme.CONTROL_FOREGROUND.Normal.ToColor();
            StringFormat sfPrimary = new StringFormat();
            sfPrimary.LineAlignment = StringAlignment.Center;
            g.DrawString(PrimaryText, new Font("Segoe UI Semibold", 8), new SolidBrush(color), new Rectangle(Bounds.X + 10, Bounds.Y + 5, Bounds.Width, Bounds.Height / 2 + 5), sfPrimary);

            // Draw divider lines
            Color lineColor = (_sourceTheme.DARK_BASED) ? MColor.AddRGB(-150, _sourceTheme.CONTROL_FOREGROUND.Normal.ToColor()) : MColor.AddRGB(150, _sourceTheme.CONTROL_FOREGROUND.Normal.ToColor());
            g.DrawLine(new Pen(lineColor), new Point(Bounds.X, Bounds.Y), new Point(Bounds.X + Bounds.Width, Bounds.Y));
            g.DrawLine(new Pen(lineColor), new Point(Bounds.X, Bounds.Y + Bounds.Height - 1), new Point(Bounds.X + Bounds.Width, Bounds.Y + Bounds.Height - 1));
        }
    }
}
