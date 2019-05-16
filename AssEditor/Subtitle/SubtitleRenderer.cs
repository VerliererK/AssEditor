using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace AssEditor.Subtitle
{
    class SubtitleRenderer
    {
        //150 pixels / 96 dpi = 1.56 (my guess)
        private const float RenderScale = 1.56f;

        //style.Alignment
        // 7 8 9
        // 4 5 6
        // 1 2 3
        public static RectangleF MeasureDialogue(Subtitle subtitle, Dialogue dialogue)
        {
            var style = subtitle.Styles.Find(s => s.Name == dialogue.Style);

            var hA = (HorizontalAlignment)(int)(style.Alignment % 3);
            var vA = 2 - (VerticalAlignment)(int)(style.Alignment / 3);

            using (var f = new Font(style.FontName, style.Fontsize))
            using (var image = new Bitmap(1, 1))
            using (var g = Graphics.FromImage(image))
            {
                g.PageUnit = GraphicsUnit.Point;
                var size = g.MeasureString(dialogue.Text, f);
                size.Width *= style.ScaleX / 100f / RenderScale;
                size.Height *= style.ScaleY / 100f / RenderScale;
                RectangleF rect = new RectangleF(0, 0, size.Width, size.Height);

                if (hA == HorizontalAlignment.Left) rect.X = style.MarginL;
                else if (hA == HorizontalAlignment.Center) rect.X = style.MarginL - style.MarginR + (subtitle.PlayResX - size.Width) / 2;
                else if (hA == HorizontalAlignment.Right) rect.X = subtitle.PlayResX - style.MarginR - size.Width / 2;

                if (vA == VerticalAlignment.Top) rect.Y = style.MarginV;
                else if (vA == VerticalAlignment.Center) rect.Y = subtitle.PlayResY / 2;
                else if (vA == VerticalAlignment.Bottom) rect.Y = subtitle.PlayResY - style.MarginV - size.Height;

                return rect;
            }
        }
    }
}
