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
    /// Single line item with color preview
    /// </summary>
    public class SingleLineColorItem : MItem
    {
        /// <summary>
        /// Color
        /// </summary>
        private Color _color;

        /// <summary>
        /// Color property
        /// </summary>
        public Color Color
        {
            get { return _color; }
            set { _color = value; }
        }

        /// <summary>
        /// Default source theme used for item drawing
        /// </summary>
        private Theme _sourceTheme;

        /// <summary>
        /// Constructor
        /// </summary>
        public SingleLineColorItem(string primaryText, Color color)
        {
            this.Color = color;
            base.PrimaryText = primaryText;
        }

        /// <summary>
        /// Constructor (Hex)
        /// </summary>
        public SingleLineColorItem(string primaryText, Hex hex)
        {
            this.Color = hex.ToColor();
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
            Rectangle avatar = new Rectangle(new Point(Bounds.X + 10, Bounds.Y + (Bounds.Height / 2) - 8), new Size(15, 15));
            LinearGradientBrush lgb = new LinearGradientBrush(avatar, Color, MColor.AddRGB(55, Color), LinearGradientMode.ForwardDiagonal);
            g.FillRectangle(lgb, avatar);
            g.DrawRectangle(new Pen(_sourceTheme.CONTROL_FOREGROUND.Normal.ToColor(), 1), avatar);

            // Draw primary text
            StringFormat sfPrimary = new StringFormat();
            sfPrimary.LineAlignment = StringAlignment.Center;
            g.DrawString(PrimaryText, new Font("Segoe UI", 9), new SolidBrush(_sourceTheme.CONTROL_FOREGROUND.Normal.ToColor()), new Rectangle(Bounds.X + 32, Bounds.Y + 1, Bounds.Width + 50, Bounds.Height), sfPrimary);
        }
    }
}
