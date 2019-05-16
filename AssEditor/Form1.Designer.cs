namespace AssEditor
{
    partial class Form1
    {
        /// <summary>
        /// 設計工具所需的變數。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清除任何使用中的資源。
        /// </summary>
        /// <param name="disposing">如果應該處置受控資源則為 true，否則為 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 設計工具產生的程式碼

        /// <summary>
        /// 此為設計工具支援所需的方法 - 請勿使用程式碼編輯器修改
        /// 這個方法的內容。
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.numericUpDown_wrap = new System.Windows.Forms.NumericUpDown();
            this.checkBox_coverFile = new System.Windows.Forms.CheckBox();
            this.checkBox_fontName = new System.Windows.Forms.CheckBox();
            this.checkBox_sc2tc = new System.Windows.Forms.CheckBox();
            this.checkBox_manualWrap = new System.Windows.Forms.CheckBox();
            this.checkBox_wrap = new System.Windows.Forms.CheckBox();
            this.checkBox_fontScale = new System.Windows.Forms.CheckBox();
            this.comboBox_fontName = new System.Windows.Forms.ComboBox();
            this.comboBox_sc2tc = new System.Windows.Forms.ComboBox();
            this.numericUpDown_fontScale = new System.Windows.Forms.NumericUpDown();
            this.panel_editor = new System.Windows.Forms.Panel();
            this.btn_renameForm = new System.Windows.Forms.Button();
            this.btn_openFiles = new System.Windows.Forms.Button();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.checkBox_ignoreStyle = new System.Windows.Forms.CheckBox();
            this.richTextBox_ignoreStyle = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_wrap)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_fontScale)).BeginInit();
            this.SuspendLayout();
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.AutoSize = true;
            this.tableLayoutPanel1.ColumnCount = 2;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle());
            this.tableLayoutPanel1.Controls.Add(this.checkBox_ignoreStyle, 0, 5);
            this.tableLayoutPanel1.Controls.Add(this.numericUpDown_wrap, 1, 2);
            this.tableLayoutPanel1.Controls.Add(this.checkBox_coverFile, 0, 4);
            this.tableLayoutPanel1.Controls.Add(this.checkBox_fontName, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.checkBox_sc2tc, 0, 3);
            this.tableLayoutPanel1.Controls.Add(this.checkBox_manualWrap, 1, 4);
            this.tableLayoutPanel1.Controls.Add(this.checkBox_wrap, 0, 2);
            this.tableLayoutPanel1.Controls.Add(this.checkBox_fontScale, 0, 1);
            this.tableLayoutPanel1.Controls.Add(this.comboBox_fontName, 1, 0);
            this.tableLayoutPanel1.Controls.Add(this.comboBox_sc2tc, 1, 3);
            this.tableLayoutPanel1.Controls.Add(this.numericUpDown_fontScale, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.richTextBox_ignoreStyle, 1, 5);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(141, 13);
            this.tableLayoutPanel1.Margin = new System.Windows.Forms.Padding(4);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 6;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle());
            this.tableLayoutPanel1.Size = new System.Drawing.Size(345, 287);
            this.tableLayoutPanel1.TabIndex = 2;
            // 
            // numericUpDown_wrap
            // 
            this.numericUpDown_wrap.Enabled = false;
            this.numericUpDown_wrap.Location = new System.Drawing.Point(162, 102);
            this.numericUpDown_wrap.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_wrap.Name = "numericUpDown_wrap";
            this.numericUpDown_wrap.Size = new System.Drawing.Size(180, 38);
            this.numericUpDown_wrap.TabIndex = 9;
            this.numericUpDown_wrap.Value = new decimal(new int[] {
            22,
            0,
            0,
            0});
            // 
            // checkBox_coverFile
            // 
            this.checkBox_coverFile.AutoSize = true;
            this.checkBox_coverFile.Location = new System.Drawing.Point(4, 197);
            this.checkBox_coverFile.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox_coverFile.Name = "checkBox_coverFile";
            this.checkBox_coverFile.Padding = new System.Windows.Forms.Padding(10, 0, 10, 5);
            this.checkBox_coverFile.Size = new System.Drawing.Size(151, 39);
            this.checkBox_coverFile.TabIndex = 4;
            this.checkBox_coverFile.Text = "覆蓋原檔";
            this.checkBox_coverFile.UseVisualStyleBackColor = true;
            this.checkBox_coverFile.CheckedChanged += new System.EventHandler(this.checkBox_coverFile_CheckedChanged);
            // 
            // checkBox_fontName
            // 
            this.checkBox_fontName.AutoSize = true;
            this.checkBox_fontName.Location = new System.Drawing.Point(4, 4);
            this.checkBox_fontName.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox_fontName.Name = "checkBox_fontName";
            this.checkBox_fontName.Padding = new System.Windows.Forms.Padding(10, 5, 10, 5);
            this.checkBox_fontName.Size = new System.Drawing.Size(151, 44);
            this.checkBox_fontName.TabIndex = 0;
            this.checkBox_fontName.Text = "字型替換";
            this.checkBox_fontName.UseVisualStyleBackColor = true;
            this.checkBox_fontName.CheckedChanged += new System.EventHandler(this.checkBox_fontName_CheckedChanged);
            // 
            // checkBox_sc2tc
            // 
            this.checkBox_sc2tc.AutoSize = true;
            this.checkBox_sc2tc.Location = new System.Drawing.Point(4, 150);
            this.checkBox_sc2tc.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox_sc2tc.Name = "checkBox_sc2tc";
            this.checkBox_sc2tc.Padding = new System.Windows.Forms.Padding(10, 0, 10, 5);
            this.checkBox_sc2tc.Size = new System.Drawing.Size(127, 39);
            this.checkBox_sc2tc.TabIndex = 3;
            this.checkBox_sc2tc.Text = "簡轉繁";
            this.checkBox_sc2tc.UseVisualStyleBackColor = true;
            this.checkBox_sc2tc.CheckedChanged += new System.EventHandler(this.checkBox_sc2tc_CheckedChanged);
            // 
            // checkBox_manualWrap
            // 
            this.checkBox_manualWrap.AutoSize = true;
            this.checkBox_manualWrap.Location = new System.Drawing.Point(163, 197);
            this.checkBox_manualWrap.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox_manualWrap.Name = "checkBox_manualWrap";
            this.checkBox_manualWrap.Padding = new System.Windows.Forms.Padding(10, 0, 10, 0);
            this.checkBox_manualWrap.Size = new System.Drawing.Size(151, 34);
            this.checkBox_manualWrap.TabIndex = 5;
            this.checkBox_manualWrap.Text = "手動斷句";
            this.checkBox_manualWrap.UseVisualStyleBackColor = true;
            // 
            // checkBox_wrap
            // 
            this.checkBox_wrap.AutoSize = true;
            this.checkBox_wrap.Location = new System.Drawing.Point(4, 103);
            this.checkBox_wrap.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox_wrap.Name = "checkBox_wrap";
            this.checkBox_wrap.Padding = new System.Windows.Forms.Padding(10, 0, 10, 5);
            this.checkBox_wrap.Size = new System.Drawing.Size(151, 39);
            this.checkBox_wrap.TabIndex = 2;
            this.checkBox_wrap.Text = "自動斷句";
            this.checkBox_wrap.UseVisualStyleBackColor = true;
            this.checkBox_wrap.CheckedChanged += new System.EventHandler(this.checkBox_wrap_CheckedChanged);
            // 
            // checkBox_fontScale
            // 
            this.checkBox_fontScale.AutoSize = true;
            this.checkBox_fontScale.Location = new System.Drawing.Point(4, 56);
            this.checkBox_fontScale.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox_fontScale.Name = "checkBox_fontScale";
            this.checkBox_fontScale.Padding = new System.Windows.Forms.Padding(10, 0, 10, 5);
            this.checkBox_fontScale.Size = new System.Drawing.Size(151, 39);
            this.checkBox_fontScale.TabIndex = 1;
            this.checkBox_fontScale.Text = "字型縮放";
            this.checkBox_fontScale.UseVisualStyleBackColor = true;
            this.checkBox_fontScale.CheckedChanged += new System.EventHandler(this.checkBox_fontScale_CheckedChanged);
            // 
            // comboBox_fontName
            // 
            this.comboBox_fontName.Enabled = false;
            this.comboBox_fontName.FormattingEnabled = true;
            this.comboBox_fontName.Location = new System.Drawing.Point(162, 3);
            this.comboBox_fontName.MinimumSize = new System.Drawing.Size(180, 0);
            this.comboBox_fontName.Name = "comboBox_fontName";
            this.comboBox_fontName.Size = new System.Drawing.Size(180, 37);
            this.comboBox_fontName.TabIndex = 6;
            this.comboBox_fontName.SelectedIndexChanged += new System.EventHandler(this.comboBox_fontName_SelectedIndexChanged);
            // 
            // comboBox_sc2tc
            // 
            this.comboBox_sc2tc.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_sc2tc.Enabled = false;
            this.comboBox_sc2tc.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.comboBox_sc2tc.FormattingEnabled = true;
            this.comboBox_sc2tc.Location = new System.Drawing.Point(162, 149);
            this.comboBox_sc2tc.Name = "comboBox_sc2tc";
            this.comboBox_sc2tc.Size = new System.Drawing.Size(180, 37);
            this.comboBox_sc2tc.TabIndex = 7;
            // 
            // numericUpDown_fontScale
            // 
            this.numericUpDown_fontScale.Enabled = false;
            this.numericUpDown_fontScale.Increment = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_fontScale.Location = new System.Drawing.Point(162, 55);
            this.numericUpDown_fontScale.Maximum = new decimal(new int[] {
            200,
            0,
            0,
            0});
            this.numericUpDown_fontScale.Name = "numericUpDown_fontScale";
            this.numericUpDown_fontScale.Size = new System.Drawing.Size(180, 38);
            this.numericUpDown_fontScale.TabIndex = 8;
            this.numericUpDown_fontScale.Value = new decimal(new int[] {
            150,
            0,
            0,
            0});
            // 
            // panel_editor
            // 
            this.panel_editor.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.panel_editor.AutoScroll = true;
            this.panel_editor.BackColor = System.Drawing.SystemColors.Control;
            this.panel_editor.Location = new System.Drawing.Point(0, 307);
            this.panel_editor.Name = "panel_editor";
            this.panel_editor.Padding = new System.Windows.Forms.Padding(10);
            this.panel_editor.Size = new System.Drawing.Size(782, 351);
            this.panel_editor.TabIndex = 4;
            // 
            // btn_renameForm
            // 
            this.btn_renameForm.BackColor = System.Drawing.Color.Transparent;
            this.btn_renameForm.BackgroundImage = global::AssEditor.Properties.Resources.icons8_rename_100;
            this.btn_renameForm.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_renameForm.FlatAppearance.BorderSize = 0;
            this.btn_renameForm.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_renameForm.Location = new System.Drawing.Point(13, 206);
            this.btn_renameForm.Name = "btn_renameForm";
            this.btn_renameForm.Size = new System.Drawing.Size(40, 40);
            this.btn_renameForm.TabIndex = 5;
            this.btn_renameForm.UseVisualStyleBackColor = false;
            this.btn_renameForm.Click += new System.EventHandler(this.btn_renameForm_Click);
            // 
            // btn_openFiles
            // 
            this.btn_openFiles.BackColor = System.Drawing.Color.Transparent;
            this.btn_openFiles.BackgroundImage = global::AssEditor.Properties.Resources.openfile;
            this.btn_openFiles.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Zoom;
            this.btn_openFiles.FlatAppearance.BorderSize = 0;
            this.btn_openFiles.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.btn_openFiles.ForeColor = System.Drawing.Color.Transparent;
            this.btn_openFiles.Location = new System.Drawing.Point(13, 24);
            this.btn_openFiles.Margin = new System.Windows.Forms.Padding(4);
            this.btn_openFiles.Name = "btn_openFiles";
            this.btn_openFiles.Size = new System.Drawing.Size(120, 120);
            this.btn_openFiles.TabIndex = 3;
            this.btn_openFiles.UseVisualStyleBackColor = false;
            this.btn_openFiles.Click += new System.EventHandler(this.btn_openFiles_Click);
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(141, 400);
            this.progressBar1.MarqueeAnimationSpeed = 10;
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(345, 45);
            this.progressBar1.TabIndex = 0;
            this.progressBar1.Visible = false;
            // 
            // checkBox_ignoreStyle
            // 
            this.checkBox_ignoreStyle.AutoSize = true;
            this.checkBox_ignoreStyle.Location = new System.Drawing.Point(4, 244);
            this.checkBox_ignoreStyle.Margin = new System.Windows.Forms.Padding(4);
            this.checkBox_ignoreStyle.Name = "checkBox_ignoreStyle";
            this.checkBox_ignoreStyle.Padding = new System.Windows.Forms.Padding(10, 0, 10, 5);
            this.checkBox_ignoreStyle.Size = new System.Drawing.Size(151, 39);
            this.checkBox_ignoreStyle.TabIndex = 10;
            this.checkBox_ignoreStyle.Text = "忽略樣式";
            this.checkBox_ignoreStyle.UseVisualStyleBackColor = true;
            this.checkBox_ignoreStyle.CheckedChanged += new System.EventHandler(this.CheckBox_ignoreStyle_CheckedChanged);
            // 
            // richTextBox_ignoreStyle
            // 
            this.richTextBox_ignoreStyle.Location = new System.Drawing.Point(162, 243);
            this.richTextBox_ignoreStyle.Multiline = false;
            this.richTextBox_ignoreStyle.Name = "richTextBox_ignoreStyle";
            this.richTextBox_ignoreStyle.Size = new System.Drawing.Size(180, 40);
            this.richTextBox_ignoreStyle.TabIndex = 11;
            this.richTextBox_ignoreStyle.Text = "";
            // 
            // Form1
            // 
            this.AllowDrop = true;
            this.AutoScaleDimensions = new System.Drawing.SizeF(14F, 29F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(782, 670);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.btn_renameForm);
            this.Controls.Add(this.panel_editor);
            this.Controls.Add(this.btn_openFiles);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Font = new System.Drawing.Font("微軟正黑體", 13.8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(136)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(5);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "Form1";
            this.Text = "AssEditor";
            this.DragDrop += new System.Windows.Forms.DragEventHandler(this.Form1_DragDrop);
            this.DragEnter += new System.Windows.Forms.DragEventHandler(this.Form1_DragEnter);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_wrap)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_fontScale)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.Button btn_openFiles;
        private System.Windows.Forms.CheckBox checkBox_fontName;
        private System.Windows.Forms.CheckBox checkBox_coverFile;
        private System.Windows.Forms.CheckBox checkBox_wrap;
        private System.Windows.Forms.CheckBox checkBox_fontScale;
        private System.Windows.Forms.CheckBox checkBox_sc2tc;
        private System.Windows.Forms.CheckBox checkBox_manualWrap;
        private System.Windows.Forms.NumericUpDown numericUpDown_wrap;
        private System.Windows.Forms.ComboBox comboBox_fontName;
        private System.Windows.Forms.ComboBox comboBox_sc2tc;
        private System.Windows.Forms.NumericUpDown numericUpDown_fontScale;
        private System.Windows.Forms.Panel panel_editor;
        private System.Windows.Forms.Button btn_renameForm;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.CheckBox checkBox_ignoreStyle;
        private System.Windows.Forms.RichTextBox richTextBox_ignoreStyle;
    }
}

