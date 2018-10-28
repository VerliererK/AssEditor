using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace AssEditor
{
    public partial class RenameForm : Form
    {
        readonly string[] videoExtension = new string[] { ".mp4", ".mkv" };
        readonly string[] subtitleExtension = new string[] { ".srt", ".ass", ".ssa" };
        List<string> videoFiles = new List<string>();
        List<string> subtitleFiles = new List<string>();

        public RenameForm()
        {
            InitializeComponent();
        }

        private void RenameForm_Resize(object sender, EventArgs e)
        {
            for (int i = 0; i < listView1.Columns.Count - 1; i++)
                listView1.Columns[i].Width = listView1.Width / 3 - 1;
            listView1.Columns[listView1.Columns.Count - 1].Width = -2;
        }

        private void UpdateListView()
        {
            listView1.Items.Clear();
            int maxCount = Math.Max(videoFiles.Count, subtitleFiles.Count);
            for (int i = 0; i < maxCount; i++)
            {
                if (i < videoFiles.Count && i < subtitleFiles.Count)
                    listView1.Items.Add(new ListViewItem(new string[] {
                        Path.GetFileName(videoFiles[i]),
                        Path.GetFileName(subtitleFiles[i]),
                        Path.GetFileNameWithoutExtension(videoFiles[i]) + Path.GetExtension(subtitleFiles[i])
                    }));
                else if (i < videoFiles.Count)
                    listView1.Items.Add(new ListViewItem(new string[] { Path.GetFileName(videoFiles[i]), "" }));
                else if (i < subtitleFiles.Count)
                    listView1.Items.Add(new ListViewItem(new string[] { "", Path.GetFileName(subtitleFiles[i]) }));
            }
        }

        private void listView1_DragDrop(object sender, DragEventArgs e)
        {
            string[] FileNames = (string[])e.Data.GetData(DataFormats.FileDrop);
            Array.Sort(FileNames);
            var vFiles = new List<string>();
            var sFiles = new List<string>();
            foreach (var f in FileNames)
            {
                if (Array.FindIndex(videoExtension, ext => ext == Path.GetExtension(f)) >= 0)
                    vFiles.Add(f);
                else if (Array.FindIndex(subtitleExtension, ext => ext == Path.GetExtension(f)) >= 0)
                    sFiles.Add(f);
            }
            if (vFiles.Count > 0)
            {
                videoFiles.Clear();
                videoFiles.AddRange(vFiles);
            }
            if (sFiles.Count > 0)
            {
                subtitleFiles.Clear();
                subtitleFiles.AddRange(sFiles);
            }

            UpdateListView();
        }

        private void listView1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Link;
            else
                e.Effect = DragDropEffects.None;
        }

        private void renameToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int minCount = Math.Min(videoFiles.Count, subtitleFiles.Count);
            for (int i = 0; i < minCount; i++)
            {
                var oldsub = subtitleFiles[i];
                var newsub = Path.Combine(Path.GetDirectoryName(oldsub), Path.GetFileNameWithoutExtension(videoFiles[i]) + Path.GetExtension(oldsub));
                if (File.Exists(newsub))
                {
                    MessageBox.Show("File is already exist: " + newsub);
                    continue;
                }
                if (oldsub != newsub)
                    File.Move(oldsub, newsub);
            }
            subtitleFiles.Clear();
            UpdateListView();
        }

        private void clearToolStripMenuItem_Click(object sender, EventArgs e)
        {
            videoFiles.Clear();
            subtitleFiles.Clear();
            UpdateListView();
        }
    }
}
