namespace AssEditor.Subtitle
{
    internal class Style
    {
        public string Name, FontName;
        public float Fontsize;

        public string PrimaryColour, SecondaryColour, OutlineColour, BackColour;

        public float Bold, Italic, Underline, StrikeOut,
            ScaleX, ScaleY, Spacing, Angle,
            BorderStyle, Outline,
            Shadow, Alignment,
            MarginL, MarginR, MarginV,
            Encoding;

        public static Style Parse(string line)
        {
            if (line.StartsWith("Style: "))
            {
                string[] values = line.Substring("Style: ".Length).Split(',');
                if (values.Length != 23)
                    return null;
                Style style = new Style();
                style.Name = values[0];
                style.FontName = values[1];
                style.Fontsize = float.Parse(values[2]);
                style.PrimaryColour = values[3];
                style.SecondaryColour = values[4];
                style.OutlineColour = values[5];
                style.BackColour = values[6];
                style.Bold = float.Parse(values[7]);
                style.Italic = float.Parse(values[8]);
                style.Underline = float.Parse(values[9]);
                style.StrikeOut = float.Parse(values[10]);
                style.ScaleX = float.Parse(values[11]);
                style.ScaleY = float.Parse(values[12]);
                style.Spacing = float.Parse(values[13]);
                style.Angle = float.Parse(values[14]);
                style.BorderStyle = float.Parse(values[15]);
                style.Outline = float.Parse(values[16]);
                style.Shadow = float.Parse(values[17]);
                style.Alignment = float.Parse(values[18]);
                style.MarginL = float.Parse(values[19]);
                style.MarginR = float.Parse(values[20]);
                style.MarginV = float.Parse(values[21]);
                //Encoding 字體的編碼： 英文編碼 1、日文編碼128、簡體中文編碼134、繁體中文編碼136
                //Encoding 強制轉 1
                //fontFormat.Encoding = float.Parse(style[22]);
                style.Encoding = 1;
                return style;
            }
            return null;
        }

        public void SetFontName(string fontName)
        {
            if (FontName.Contains("@"))
                fontName = "@" + fontName;
            FontName = fontName;
        }

        public void SetScale(float scale)
        {
            ScaleX += (scale - 100);
            ScaleY += (scale - 100);
        }

        public override string ToString()
        {
            return "Style: " + string.Join(",", Name, FontName, Fontsize, PrimaryColour, SecondaryColour, OutlineColour, BackColour, Bold, Italic, Underline, StrikeOut, ScaleX, ScaleY, Spacing, Angle, BorderStyle, Outline, Shadow, Alignment, MarginL, MarginR, MarginV, Encoding);
        }
    }
}