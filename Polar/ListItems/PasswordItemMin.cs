using MinimalLibrary;
using MinimalLibrary.Controls;
using MinimalLibrary.Controls.Items;
using MinimalLibrary.External;
using MinimalLibrary.Scaling;
using MinimalLibrary.Themes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polar.ListItems
{
    /// <summary>
    /// Password item minified
    /// </summary>
    class PasswordItemMin : MItem
    {
        /// <summary>
        /// Secondary text
        /// </summary>
        private string _secondaryText;

        /// <summary>
        /// Secondary text property
        /// </summary>
        public string SecondaryText
        {
            get { return _secondaryText; }
            set { _secondaryText = value; }
        }

        /// <summary>
        /// Default source theme used for item drawing
        /// </summary>
        private Theme _sourceTheme;

        /// <summary>
        /// Constructor
        /// </summary>
        public PasswordItemMin(string primaryText, string secondaryText)
        {
            base.PrimaryText = primaryText;
            this.SecondaryText = secondaryText;
        }

        /// <summary>
        /// Draw
        /// </summary>
        public override void DrawItem(MListBox owner, Graphics g, Rectangle itemBounds)
        {
            // DIP
            DIP.GetGraphics(g);

            // Basic variables
            Height = DIP.Set(90);
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
                // Control don't have its own theme
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
            g.DrawString(PrimaryText, new Font("Segoe UI", 9, FontStyle.Regular), new SolidBrush((owner.SelectedItem == this) ? owner.Tint : _sourceTheme.CONTROL_FOREGROUND.Normal.ToColor()), new Rectangle(Bounds.X + 21, Bounds.Y + 12, Bounds.Width, Bounds.Height / 2 + 1), sfPrimary);

            // Draw secondary text
            StringFormat sfSecondary = new StringFormat();
            sfSecondary.LineAlignment = StringAlignment.Center;
            Color textColor = (_sourceTheme.DARK_BASED) ? MColor.AddRGB(-120, _sourceTheme.CONTROL_FOREGROUND.Normal.ToColor()) : MColor.AddRGB(100, _sourceTheme.CONTROL_FOREGROUND.Normal.ToColor());
            g.DrawString(SecondaryText, new Font("Segoe UI", 8), new SolidBrush(textColor), new Rectangle(Bounds.X + 21, Bounds.Y + Bounds.Height / 2 - 12, Bounds.Width, Bounds.Height / 2 + 5), sfSecondary);
        }
    }
}
