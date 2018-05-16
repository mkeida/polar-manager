using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalLibrary.Internal
{
    internal class Draw
    {
        /// <summary>
        /// Returns ellipse. Zero position is center instead of top left corner.
        /// </summary>
        public static GraphicsPath GetEllipsePath(Point center, int radius)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddEllipse(new Rectangle(center.X - radius, center.Y - radius, radius * 2, radius * 2));
            return path;
        }

        /// <summary>
        /// Returns square. Zero position is center instead of top left corner.
        /// </summary>
        public static GraphicsPath GetSquarePath(Point center, int a)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(new Rectangle(center.X - a, center.Y - a, a * 2, a * 2));
            return path;
        }

        /// <summary>
        /// Returns rectangle. Zero position is center instead of top left corner.
        /// </summary>
        public static GraphicsPath GetRectanglePath(Point center, int a, int b)
        {
            //Find the x-coordinate of the upper-left corner of the rectangle to draw.
            int x = center.X - a / 2;

            //Find y-coordinate of the upper-left corner of the rectangle to draw. 
            int y = center.Y - b / 2;

            GraphicsPath path = new GraphicsPath();
            path.AddRectangle(new Rectangle(x, y, a, b));
            return path;
        }

        // Use a series of pens with decreasing widths and
        // increasing opacity to draw a GraphicsPath.
        public static void FuzzyPath(GraphicsPath path, Graphics g, Color baseColor, int maxOpacity, int width, int opaqueWidth)
        {
            // Number of pens we will uses
            int stepsNum = width - opaqueWidth + 1;

            // Change in alpha between pens
            float delta = (float)maxOpacity / stepsNum / stepsNum;

            // Initial alpha.
            float alpha = delta;

            for (int thickness = width; thickness >= opaqueWidth; thickness--)
            {
                Color color = Color.FromArgb((int)alpha, baseColor.R, baseColor.G, baseColor.B);

                using (Pen pen = new Pen(color, thickness))
                {
                    pen.EndCap = LineCap.Round;
                    pen.StartCap = LineCap.Round;
                    g.DrawPath(pen, path);
                }

                alpha += delta;
            }
        }
    }
}
