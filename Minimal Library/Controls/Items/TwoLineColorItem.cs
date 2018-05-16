using MinimalLibrary.External;
using MinimalLibrary.Internal;
using MinimalLibrary.Scaling;
using MinimalLibrary.Themes;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalLibrary.Controls.Items
{
    /// <summary>
    /// Item with color preview
    /// </summary>
    public class TwoLineColorItem : MItem
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
        /// Color
        /// </summary>
        private Color color;

        /// <summary>
        /// Color property
        /// </summary>
        public Color Color
        {
            get { return color; }
            set { color = value; }
        }

        /// <summary>
        /// Default source theme used for item drawing
        /// </summary>
        private Theme _sourceTheme;

        /// <summary>
        /// Constructor
        /// </summary>
        public TwoLineColorItem(string primaryText, string secondaryText, Color color)
        {
            this.Color = color;
            this.SecondaryText = secondaryText;
            base.PrimaryText = primaryText;
        }

        /// <summary>
        /// Constructor (Hex)
        /// </summary>
        public TwoLineColorItem(string primaryText, string secondaryText, Hex hex)
        {
            this.Color = hex.ToColor();
            this.SecondaryText = secondaryText;
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

            // Avatar
            Rectangle avatar = new Rectangle(new Point(Bounds.X + 10, Bounds.Y + (Bounds.Height / 2) - 16 - 1), new Size(32, 32));
            LinearGradientBrush lgb = new LinearGradientBrush(avatar, Color, MColor.AddRGB(70, Color), LinearGradientMode.ForwardDiagonal);
            g.FillRectangle(lgb, avatar);
            g.DrawRectangle(new Pen(_sourceTheme.CONTROL_FOREGROUND.Normal.ToColor(), 1), avatar);

            // Draw primary text
            StringFormat sfPrimary = new StringFormat();
            sfPrimary.LineAlignment = StringAlignment.Center;
            g.DrawString(PrimaryText, new Font("Segoe UI", 9), new SolidBrush(_sourceTheme.CONTROL_FOREGROUND.Normal.ToColor()), new Rectangle(Bounds.X + 48, Bounds.Y + 6, Bounds.Width, Bounds.Height / 2 + 1), sfPrimary);

            // Draw secondary text
            StringFormat sfSecondary = new StringFormat();
            sfSecondary.LineAlignment = StringAlignment.Center;
            Color textColor = (_sourceTheme.DARK_BASED) ? MColor.AddRGB(-120, _sourceTheme.CONTROL_FOREGROUND.Normal.ToColor()) : MColor.AddRGB(100, _sourceTheme.CONTROL_FOREGROUND.Normal.ToColor());
            g.DrawString(SecondaryText, new Font("Segoe UI", 8), new SolidBrush(textColor), new Rectangle(Bounds.X + 48, Bounds.Y + Bounds.Height / 2 - 8, Bounds.Width, Bounds.Height / 2 + 5), sfSecondary);
        }
    }
}
