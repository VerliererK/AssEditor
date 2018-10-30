using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AssEditor.Subtitle
{
    class Subtitle
    {
        private Encoding encoding = Encoding.UTF8;
        public string FileName { get; private set; }
        public List<Style> Styles { get; private set; }
        public List<Dialogue> Dialogues { get; private set; }
        private List<object> AllLines;
        public int DialogueTooLongCount { get { return Dialogues.Where(d => d.IsTextTooLong).Count(); } }

        public static async Task<Subtitle> LoadFromFile(string fileName, ushort wrapLimit = 20, bool ToTraditional = false, ZhConvert.ZhConverter.Method method = ZhConvert.ZhConverter.Method.Fanhuaji)
        {
            if (!File.Exists(fileName)) return null;
            Subtitle subtitle = new Subtitle();
            subtitle.FileName = fileName;
            subtitle.encoding = EncodingHelper.GetEncoding(fileName);
            string allText = (!ToTraditional) ? File.ReadAllText(fileName, subtitle.encoding) :
                await ZhConvert.ZhConverter.ToTraditional(File.ReadAllText(fileName), method);
            string line;
            using (var sr = new StringReader(allText))
                while ((line = sr.ReadLine()) != null)
                {
                    var d = Dialogue.Parse(line, wrapLimit);
                    var s = Style.Parse(line);

                    if (d != null)
                    {
                        subtitle.AllLines.Add(d);
                        subtitle.Dialogues.Add(d);
                    }
                    else if (s != null)
                    {
                        subtitle.AllLines.Add(s);
                        subtitle.Styles.Add(s);
                    }
                    else
                        subtitle.AllLines.Add(line);
                }
            return subtitle;
        }

        private Subtitle()
        {
            AllLines = new List<object>();
            Styles = new List<Style>();
            Dialogues = new List<Dialogue>();
        }

        ~Subtitle()
        {
            AllLines.Clear();
            AllLines = null;
            Styles.Clear();
            Styles = null;
            Dialogues.Clear();
            Dialogues = null;
        }

        public void SaveToFile(string fileName)
        {
            using (var fs = new StreamWriter(fileName, false, encoding))
                foreach (var line in AllLines)
                    fs.WriteLine(line.ToString());
        }

        internal void ReplaceFontName(string fontName)
        {
            foreach (var s in Styles)
                s.SetFontName(fontName);
            foreach (var d in Dialogues)
                d.ReplaceFontName(fontName);
        }

        internal void ReplaceFontScale(int scale)
        {
            foreach (var s in Styles)
                s.SetScale(scale);
            foreach (var d in Dialogues)
                d.ReplaceFontScale(scale);
        }

        internal async System.Threading.Tasks.Task WordWrapAll()
        {
            await System.Threading.Tasks.Task.WhenAll(Dialogues.Select(d =>
             {
                 if (d.IsTextTooLong)
                     return d.WordWrapAsync(true);
                 else
                     return System.Threading.Tasks.Task.Delay(0);
             }).ToArray());
        }

        /*
        public async System.Threading.Tasks.Task ToTraditional()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var d in Dialogues) sb.AppendLine(d.Text);
            var allText = await ZhConvert.ZhConverter.ToTraditional(sb.ToString(), ZhConvert.ZhConverter.Method.Fanhuaji);
            var newText = allText.Split(System.Environment.NewLine.ToCharArray(), System.StringSplitOptions.RemoveEmptyEntries);
            //if (dialogues.Count != newText.Length) throw new System.Exception("ZhConverter Error: Length not same.");
            for (int i = 0; i < newText.Length; i++)
                Dialogues[i].Text = newText[i];
        }
        */
    }
}
