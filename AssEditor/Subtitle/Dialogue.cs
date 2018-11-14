using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AssEditor.Subtitle
{
    public class Dialogue
    {
        class TextEffect
        {
            public TextEffect(int index, string value)
            {
                this.index = index;
                this.value = value;
            }
            public int index;
            public string value;
        }
        private List<TextEffect> TextEffects = new List<TextEffect>();
        public string Text;
        public readonly string OriginalText;
        public string Layer, Start, End, Style, Name, MarginL, MarginR, MarginV, Effect;
        private readonly ushort WrapLimit;
        public bool IsTextTooLong { get; private set; }

        public static readonly Regex regex_fad = new Regex(@"\\fad\(\d*,\d*\)");
        public static readonly Regex regex_fn = new Regex(@"\\fn[^\\^}]*");
        public static readonly Regex regex_fscx = new Regex(@"\\fscx([0-9]*)");
        public static readonly Regex regex_fscy = new Regex(@"\\fscy([0-9]*)");
        public static readonly Regex regex_effect = new Regex(@"{[^{]*\}");
        public static readonly string[] wrapchar = new string[] { " ", "\\n", "\\N" };

        public static Dialogue Parse(string line, ushort wrapLimit = 20, bool splitTextEffect = true)
        {
            if (!line.StartsWith("Dialogue: ")) return null;
            string[] Format = line.Substring("Dialogue: ".Length).Split(new char[] { ',' }, 10);
            if (Format.Length != 10) return null;

            Dialogue dialogue = new Dialogue(
                text: Format[9],
                layer: Format[0],
                start: Format[1],
                end: Format[2],
                style: Format[3],
                name: Format[4],
                marginL: Format[5], marginR: Format[6], marginV: Format[7],
                effect: Format[8],
                wrapLimit: wrapLimit);

            return dialogue;
        }

        public static Dictionary<int, string> GetTooLongString(string text, ushort wrapLimit)
        {
            var split = text.Split(wrapchar, System.StringSplitOptions.RemoveEmptyEntries);
            Dictionary<int, string> matches = new Dictionary<int, string>();
            int startIndex = 0;
            foreach (var s in split)
                if (s.Length >= wrapLimit)
                {
                    matches.Add(text.IndexOf(s, startIndex), s);
                    startIndex += s.Length;
                }
            return matches;
        }

        private static bool IsTooLong(string text, ushort wrapLimit)
        {
            return GetTooLongString(text, wrapLimit).Count > 0;
        }

        private static bool IsLetterOrDigit(char c)  // only english letter and 0-9
        {
            if ('a' <= c && c <= 'z')
                return true;
            if ('A' <= c && c <= 'Z')
                return true;
            if ('0' <= c && c <= '9')
                return true;
            return false;
        }

        private static async System.Threading.Tasks.Task<int> FindWrapIndex(string text)
        {
            int midIndex = text.Length / 2;
            int index = 0;

            int padding = 3;
            char c;

            for (int i = padding; i < text.Length - padding; i++)
            {
                c = text[i];
                if (char.IsPunctuation(c) || IsLetterOrDigit(c) || char.IsWhiteSpace(c))
                    if (System.Math.Abs(i - midIndex) < System.Math.Abs(index - midIndex))
                        index = i;
            }

            if (index == 0)
            {
                List<Segment> seg = await Segmentor.SegmentTextAsync(text);
                int segIndex = 0;
                if (seg != null)
                    foreach (var s in seg)
                    {
                        segIndex += s.Word.Length;
                        if (Segmentor.IsNoun(s.POS) && System.Math.Abs(segIndex - midIndex) < System.Math.Abs(index - midIndex))
                            index = segIndex - 1;
                    }
            }

            // fine tune
            if (index > 0)
            {
                c = text[index];
                if (c == '(' || c == '（' || c == '｛' || c == '「' || c == '『' || c == '〈' || c == '《' || c == '〔' || c == '【')
                    index--;
                else if (char.IsLetterOrDigit(c))  // find end or front of digits or letters
                {
                    while (index < text.Length - 1 && IsLetterOrDigit(text[index + 1]))
                        index++;
                    if (index > text.Length - padding)
                    {
                        while (index > 0 && IsLetterOrDigit(text[index - 1]))
                            index--;
                        index--;  //insert front
                    }
                }
            }
            // not found
            if (index <= 0 || index >= text.Length - 1) index = midIndex - 1;

            return index;
        }

        Dialogue(string text, string layer, string start, string end, string style, string name, string marginL, string marginR, string marginV, string effect, ushort wrapLimit)
        {
            TextEffects.Clear();
            int prevEffectLength = 0;
            foreach (Match m in regex_effect.Matches(text))
            {
                TextEffects.Add(new TextEffect(m.Index - prevEffectLength, m.Value));
                prevEffectLength += m.Value.Length;
            }

            OriginalText = regex_effect.Replace(text, "");
            Text = OriginalText;
            Layer = layer;
            Start = start;
            End = end;
            Style = style;
            Name = name;
            MarginL = marginL;
            MarginR = marginR;
            MarginV = marginV;
            Effect = effect;
            WrapLimit = wrapLimit;
            IsTextTooLong = IsTooLong(OriginalText, wrapLimit);
        }

        ~Dialogue()
        {
            TextEffects.Clear();
            TextEffects = null;
        }

        public async System.Threading.Tasks.Task WordWrapAsync(bool auto = true)
        {
            var toLong = GetTooLongString(Text, WrapLimit);
            int insertIndex = 0;
            foreach (var s in toLong)
            {
                int index = await FindWrapIndex(s.Value);
                if (auto)
                    Text = Text.Insert(s.Key + index + insertIndex++ + 1, " ");
            }
        }

        public void ReplaceFontName(string fontName)
        {
            foreach (var effect in TextEffects)
                effect.value = regex_fn.Replace(effect.value, (match) =>
                {
                    return match.Value.Contains("@") ? "\\fn@" + fontName : "\\fn" + fontName;
                });
        }

        public void ReplaceFontScale(int scale)
        {
            foreach (var effect in TextEffects)
            {
                effect.value = regex_fscx.Replace(effect.value, (match) =>
                {
                    bool success = int.TryParse(match.Groups[1].Value, out int s);
                    return success ? "\\fscx" + (s + (scale - 100)) : "\\fscx";
                });
                effect.value = regex_fscy.Replace(effect.value, (match) =>
                  {
                      bool success = int.TryParse(match.Groups[1].Value, out int s);
                      return success ? "\\fscy" + (s + (scale - 100)) : "\\fscy";
                  });
            }
        }

        private string GetTextWithEffect()
        {
            var dmp = new DiffMatchPatch.diff_match_patch();
            var diff = dmp.diff_main(OriginalText, Text);

            int index = 0;
            foreach (var d in diff)
            {
                var length = d.text.Length;
                if (d.operation == DiffMatchPatch.Operation.DELETE)
                    foreach (var effect in TextEffects)
                        effect.index = (effect.index >= index) ? effect.index - length : effect.index;
                else if (d.operation == DiffMatchPatch.Operation.INSERT)
                    foreach (var effect in TextEffects)
                        effect.index = (effect.index >= index) ? effect.index + length : effect.index;
                index += length;
            }

            string text_effect = Text;
            int prevEffectLength = 0;
            foreach (var effect in TextEffects)
            {
                int insertIndex = effect.index + prevEffectLength;
                insertIndex = System.Math.Min(insertIndex, text_effect.Length);
                insertIndex = System.Math.Max(insertIndex, 0);
                text_effect = text_effect.Insert(insertIndex, effect.value);
                prevEffectLength += effect.value.Length;
            }
            return text_effect;
        }

        public override string ToString()
        {
            string text_effect = GetTextWithEffect();
            return "Dialogue: " + string.Join(",", Layer, Start, End, Style, Name, MarginL, MarginR, MarginV, Effect, text_effect);
        }
    }
}