using MinimalLibrary;
using MinimalLibrary.Controls;
using MinimalLibrary.Controls.Items;
using MinimalLibrary.External;
using MinimalLibrary.Scaling;
using MinimalLibrary.Themes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polar.ListItems
{
    class GroupDivider : MDividerItem
    {
        /// <summary>
        /// Default source theme used for control drawing
        /// </summary>
        private Theme _sourceTheme;

        public GroupDivider(string primaryText)
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
            g.FillRectangle(new SolidBrush(_sourceTheme.CONTROL_BACKGROUND.Normal.ToColor()), itemBounds);

            // Draw primary text
            Color color = _sourceTheme.CONTROL_FOREGROUND.Normal.ToColor();
            StringFormat sfPrimary = new StringFormat();
            sfPrimary.LineAlignment = StringAlignment.Center;
            g.DrawString(PrimaryText, new Font("Segoe UI Semibold", 10), new SolidBrush(color), new Rectangle(Bounds.X + 21, Bounds.Y + 5, Bounds.Width, Bounds.Height / 2 + 5), sfPrimary);

            // Draw divider lines
            Color lineColor = (_sourceTheme.DARK_BASED) ? MColor.AddRGB(-150, _sourceTheme.CONTROL_FOREGROUND.Normal.ToColor()) : MColor.AddRGB(150, _sourceTheme.CONTROL_FOREGROUND.Normal.ToColor());
            g.DrawLine(new Pen(lineColor), new Point(Bounds.X + 21, Bounds.Y + Bounds.Height - 1), new Point(Bounds.X + Bounds.Width - 21, Bounds.Y + Bounds.Height - 1));
        }
    }
}
