using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace AssEditor
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            comboBox_fontName_Initialize();
            comboBox_sc2tc_Initialize();

            checkBox_fontName.Checked = true;
            checkBox_fontScale.Checked = true;
            checkBox_wrap.Checked = true;
        }

        private void comboBox_fontName_Initialize()
        {
            comboBox_fontName.Items.Clear();
            foreach (FontFamily font in FontFamily.Families)
                comboBox_fontName.Items.Add(font.Name);

            //ComboBox AutoSize
            int maxSize = 0;
            Graphics g = CreateGraphics();
            for (int i = 0; i < comboBox_fontName.Items.Count; i++)
            {
                SizeF size = g.MeasureString(comboBox_fontName.Items[i].ToString() + "。", comboBox_fontName.Font);
                if (comboBox_fontName.Font.Name == comboBox_fontName.Items[i].ToString())
                    comboBox_fontName.SelectedIndex = i;
                if (maxSize < (int)size.Width)
                    maxSize = (int)size.Width;
            }
            comboBox_fontName.DropDownWidth = maxSize;
        }

        private void comboBox_fontName_SelectedIndexChanged(object sender, EventArgs e)
        {
            var g = CreateGraphics();
            SizeF size = g.MeasureString(comboBox_fontName.SelectedItem.ToString() + "。", comboBox_fontName.Font);
            comboBox_fontName.Width = (int)size.Width;
        }

        private void checkBox_fontName_CheckedChanged(object sender, EventArgs e)
        {
            comboBox_fontName.Enabled = checkBox_fontName.Checked;
        }

        private void checkBox_fontScale_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown_fontScale.Enabled = checkBox_fontScale.Checked;
        }

        private void checkBox_wrap_CheckedChanged(object sender, EventArgs e)
        {
            numericUpDown_wrap.Enabled = checkBox_wrap.Checked;
        }

        private void comboBox_sc2tc_Initialize()
        {
            foreach (var method in Enum.GetValues(typeof(ZhConvert.ZhConverter.Method)))
                comboBox_sc2tc.Items.Add(method);
            comboBox_sc2tc.SelectedIndex = 1;
        }

        private void checkBox_sc2tc_CheckedChanged(object sender, EventArgs e)
        {
            comboBox_sc2tc.Enabled = checkBox_sc2tc.Checked;
        }

        private void checkBox_coverFile_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox_coverFile.Checked == false) return;
            DialogResult result = MessageBox.Show("確認覆蓋原檔案？", "", MessageBoxButtons.YesNo);
            checkBox_coverFile.Checked = result == DialogResult.Yes;
        }

        private void Form1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                string[] FileNames = (string[])e.Data.GetData(DataFormats.FileDrop);
                foreach (string fileName in FileNames)
                {
                    var ext = System.IO.Path.GetExtension(fileName);
                    if (!(ext.Equals(".ass", StringComparison.CurrentCultureIgnoreCase) ||
                        ext.Equals(".ssa", StringComparison.CurrentCultureIgnoreCase)))
                    {
                        e.Effect = DragDropEffects.None;
                        return;
                    }
                }
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void Form1_DragDrop(object sender, DragEventArgs e)
        {
            string[] FileNames = (string[])e.Data.GetData(DataFormats.FileDrop);
            LoadSubtitles(FileNames);
        }

        private void btn_renameForm_Click(object sender, EventArgs e)
        {
            new RenameForm().Show();
        }

        private void btn_openFiles_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.Multiselect = true;
                openFileDialog.Filter = "Ass Files|*.ass;*.ssa";
                openFileDialog.Title = "Select a Ass File";
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                    LoadSubtitles(openFileDialog.FileNames);
            }
        }

        private Queue<Subtitle.Subtitle> subtitles;

        private Subtitle.Subtitle current_subtitle;

        void LoadSubtitles(string[] fileNames)
        {
            if (subtitles == null)
                subtitles = new Queue<Subtitle.Subtitle>();
            subtitles.Clear();

            Array.Sort(fileNames);
            int AllTooLongCount = 0;
            foreach (var file in fileNames)
            {
                var sub = Subtitle.Subtitle.LoadFromFile(file, decimal.ToUInt16(numericUpDown_wrap.Value));
                subtitles.Enqueue(sub);
                AllTooLongCount += sub.DialogueTooLongCount;
            }
            if (checkBox_manualWrap.Checked && checkBox_wrap.Checked)
            {
                var result = MessageBox.Show("總共有 " + AllTooLongCount + " 句需要斷行。\r\n繼續手動斷行？", "", MessageBoxButtons.OKCancel);
                if (result == DialogResult.Cancel)
                {
                    subtitles.Clear();
                    Reset();
                    return;
                }
            }

            EditSubtitlesAsync();
        }

        async void EditSubtitlesAsync()
        {
            if (subtitles.Count == 0)
            {
                //MessageBox.Show("全部完成囉！");
                return;
            }
            if (panel_editor.Controls.Count != 0)
                Reset();
            current_subtitle = subtitles.Dequeue();
            Text = System.IO.Path.GetFileName(current_subtitle.FileName);

            if (checkBox_fontName.Checked)
                current_subtitle.ReplaceFontName(comboBox_fontName.SelectedItem.ToString());
            if (checkBox_fontScale.Checked)
                current_subtitle.ReplaceFontScale(decimal.ToInt32(numericUpDown_fontScale.Value));
            if (checkBox_sc2tc.Checked)
                MessageBox.Show("Not Implement!"); // TODO sc2tc

            // Start Word Wrap
            bool autoWrap = checkBox_wrap.Checked && !checkBox_manualWrap.Checked;
            bool manualWrap = checkBox_manualWrap.Checked && checkBox_wrap.Checked;
            int toLongCount = current_subtitle.DialogueTooLongCount;

            if (autoWrap)
            {
                await current_subtitle.WordWrapAll();
            }
            else if (manualWrap && toLongCount > 0)
            {
                await current_subtitle.WordWrapAll();
                foreach (var d in current_subtitle.Dialogues)
                {
                    if (d.IsTextTooLong)
                    {
                        var textbox = new SubEditTextBox(d);
                        panel_editor.Controls.Add(textbox);
                        panel_editor.Controls.SetChildIndex(textbox, 0);
                        textbox.Dock = DockStyle.Top;
                        textbox.Height += 10;
                    }
                }

                var btn_ok = new Button { Text = "Ok", AutoSize = true };
                btn_ok.Click += SubtitleEdit_OK;
                var btn_cancel = new Button { Text = "Cancel", AutoSize = true };
                btn_cancel.Click += SubtitleEdit_Cancel;
                var btn_stop = new Button { Text = "Stop", AutoSize = true };
                btn_stop.Click += SubtitleEdit_Stop;
                var panel_btns = new FlowLayoutPanel { Dock = DockStyle.Top, AutoSize = true, Padding = new Padding(2) };
                panel_btns.Controls.Add(btn_ok);
                panel_btns.Controls.Add(btn_cancel);
                panel_btns.Controls.Add(btn_stop);

                panel_editor.Controls.Add(panel_btns);
                panel_editor.Controls.SetChildIndex(panel_btns, 0);
                return;
            }

            SaveSubtitle();
            Reset();
            EditSubtitlesAsync();
        }

        void SaveSubtitle()
        {
            string fileName = current_subtitle.FileName;
            if (!checkBox_coverFile.Checked)
            {
                string dir = System.IO.Path.GetDirectoryName(fileName);
                string name = System.IO.Path.GetFileNameWithoutExtension(fileName) + "_fix";
                string ext = System.IO.Path.GetExtension(fileName);

                string new_filename = System.IO.Path.Combine(dir, name) + ext;
                current_subtitle.SaveToFile(new_filename);
            }
            else
                current_subtitle.SaveToFile(fileName);
        }

        private void SubtitleEdit_OK(object sender, EventArgs e)
        {
            for (int i = 0; i < panel_editor.Controls.Count; i++)
                if (panel_editor.Controls[i] is SubEditTextBox box)
                    box.EditDialogue();
            SaveSubtitle();
            Reset();
            EditSubtitlesAsync();
        }

        private void SubtitleEdit_Cancel(object sender, EventArgs e)
        {
            Reset();
            EditSubtitlesAsync();
        }

        private void SubtitleEdit_Stop(object sender, EventArgs e)
        {
            Reset();
        }

        private void Reset()
        {
            Text = ProductName;
            Clear(panel_editor);
            current_subtitle = null;
            GC.Collect();
        }

        private void Clear(Panel panel)
        {
            while (panel.Controls.Count > 0)
                panel.Controls[0].Dispose();
            panel.Controls.Clear();
        }
    }
}
