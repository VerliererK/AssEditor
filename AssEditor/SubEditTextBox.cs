using System;
using System.Windows.Forms;

namespace AssEditor
{
    public partial class SubEditTextBox : UserControl
    {
        private Subtitle.Dialogue dialogue;

        public SubEditTextBox(Subtitle.Dialogue d)
        {
            InitializeComponent();
            dialogue = d;
            Text = d.Text;
            Disposed += SubEditTextBox_Disposed;
            ColorizedText();

            richTextBox1.MouseWheel += RichTextBox1_MouseWheel;
        }

        private void RichTextBox1_MouseWheel(object sender, MouseEventArgs e)
        {
            var panel = Parent as Panel;
            if (panel != null)
            {
                int delta = SystemInformation.MouseWheelScrollLines * e.Delta;

                if (panel.VerticalScroll.Value - delta < panel.VerticalScroll.Minimum)
                    panel.VerticalScroll.Value = panel.VerticalScroll.Minimum;
                else if (panel.VerticalScroll.Value - delta > panel.VerticalScroll.Maximum)
                    panel.VerticalScroll.Value = panel.VerticalScroll.Maximum;
                else
                    panel.VerticalScroll.Value -= delta;
            }
        }

        private void SubEditTextBox_Disposed(object sender, EventArgs e)
        {
            label_info.Dispose();
            label_info = null;
            richTextBox1.Dispose();
            richTextBox1 = null;
            btn_reset.Dispose();
            btn_reset = null;
        }

        public new string Text
        {
            get { return richTextBox1.Text; }
            set { richTextBox1.Text = value; }
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            Text = dialogue.Text;
            richTextBox1.SelectionStart = Text.Length / 2;
            richTextBox1.Focus();
        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {
            ColorizedText();
        }

        private void ColorizedText()
        {
            int start = richTextBox1.SelectionStart;
            var regex_wrapchar = new System.Text.RegularExpressions.Regex(@" |\\n|\\N");
            foreach (System.Text.RegularExpressions.Match m in regex_wrapchar.Matches(Text))
            {
                richTextBox1.Select(m.Index, m.Length);
                richTextBox1.SelectionBackColor = System.Drawing.Color.LightBlue;
            }
            richTextBox1.Select(start, 0);

            string[] texts = Text.Split(Subtitle.Dialogue.wrapchar, StringSplitOptions.RemoveEmptyEntries);
            int[] count = new int[texts.Length];
            for (int i = 0; i < texts.Length; i++)
                count[i] = texts[i].Length;
            label_info.Text = dialogue.Style + ": " + string.Join(" + ", count) + " 個字 ";
        }

        public void EditDialogue()
        {
            dialogue.Text = Text;
        }
    }
}
