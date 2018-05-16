using MinimalLibrary;
using MinimalLibrary.Controls;
using MinimalLibrary.Controls.Items;
using MinimalLibrary.External;
using MinimalLibrary.Scaling;
using MinimalLibrary.Themes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Polar.ListItems
{
    class PasswordItem : MItem
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
        /// Secondary text
        /// </summary>
        private string _passwordHash;

        /// <summary>
        /// Secondary text property
        /// </summary>
        public string PasswordHash
        {
            get { return _passwordHash; }
            set { _passwordHash = value; }
        }

        /// <summary>
        /// Default source theme used for item drawing
        /// </summary>
        private Theme _sourceTheme;

        /// <summary>
        /// Constructor
        /// </summary>
        public PasswordItem(string primaryText, string secondaryText, string passwordHash)
        {
            base.PrimaryText = primaryText;
            this.SecondaryText = secondaryText;
            this.PasswordHash = passwordHash;
        }

        /// <summary>
        /// Draw
        /// </summary>
        public override void DrawItem(MListBox owner, Graphics g, Rectangle itemBounds)
        {
            // DIP
            DIP.GetGraphics(g);

            // Basic variables
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
            g.DrawString(PrimaryText, new Font("Segoe UI", 9), new SolidBrush(_sourceTheme.CONTROL_FOREGROUND.Normal.ToColor()), new Rectangle(Bounds.X + 21, Bounds.Y + 6, Bounds.Width, Bounds.Height / 2 + 1), sfPrimary);

            // Draw secondary text
            StringFormat sfSecondary = new StringFormat();
            sfSecondary.LineAlignment = StringAlignment.Center;
            Color textColor = (_sourceTheme.DARK_BASED) ? MColor.AddRGB(-120, _sourceTheme.CONTROL_FOREGROUND.Normal.ToColor()) : MColor.AddRGB(100, _sourceTheme.CONTROL_FOREGROUND.Normal.ToColor());
            g.DrawString(SecondaryText, new Font("Segoe UI", 8), new SolidBrush(textColor), new Rectangle(Bounds.X + 21, Bounds.Y + Bounds.Height / 2 - 6, Bounds.Width, Bounds.Height / 2 + 5), sfSecondary);

            Color bad = new Hex("#F56C6C").ToColor();
            Color medium = new Hex("#E6A23C").ToColor();
            Color good = new Hex("#67C23A").ToColor();

            string strength = "";
            Color infoColor = Color.Beige;

            if (_passwordHash.Length >= 10)
            {
                infoColor = good;
                strength = "Strong";
            }
            else if (_passwordHash.Length >= 6)
            {
                infoColor = medium;
                strength = "Good";
            }
            else
            {
                infoColor = bad;
                strength = "Weak";
            }

            // Draw password hash
            StringFormat sfPassword = new StringFormat();
            sfPassword.LineAlignment = StringAlignment.Center;
            sfPassword.Alignment = StringAlignment.Far;
            
            Font passwordFont = new Font("Segoe UI", 9);
            SizeF size = g.MeasureString(strength, passwordFont);
            Color passwordColor = (_sourceTheme.DARK_BASED) ? MColor.AddRGB(-120, _sourceTheme.CONTROL_FOREGROUND.Normal.ToColor()) : MColor.AddRGB(100, _sourceTheme.CONTROL_FOREGROUND.Normal.ToColor());
            g.DrawString(strength, passwordFont, new SolidBrush(infoColor), new Rectangle(Bounds.X, Bounds.Y, Convert.ToInt32(Bounds.Width - 20), Bounds.Height), sfPassword);
            // new Rectangle(Bounds.X + Bounds.Width - Convert.ToInt32(size.Width) - 21, Bounds.Y + Bounds.Height / 2 - Convert.ToInt32(size.Height/2), Bounds.Width, Bounds.Height / 2 - Convert.ToInt32(size.Height / 2))
        }
    }
}
