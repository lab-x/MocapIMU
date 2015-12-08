namespace Gwearable
{
    partial class WorkDirForm
    {
        /// <summary> 
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region 组件设计器生成的代码

        /// <summary> 
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.panel1 = new System.Windows.Forms.Panel();
            this.button1 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.FileTypeFilter = new System.Windows.Forms.ToolStripSplitButton();
            this.toolStripMenuItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem2 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem3 = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripMenuItem4 = new System.Windows.Forms.ToolStripMenuItem();
            this.AddRecordFile = new System.Windows.Forms.ToolStripButton();
            this.SearchRecordFileFolder = new System.Windows.Forms.ToolStripButton();
            this.panel3 = new System.Windows.Forms.Panel();
            this.RecordFileListBox = new System.Windows.Forms.ListBox();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog2 = new System.Windows.Forms.OpenFileDialog();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.toolStrip1.SuspendLayout();
            this.panel3.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(64)))), ((int)(((byte)(69)))));
            this.panel1.Controls.Add(this.button1);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(631, 25);
            this.panel1.TabIndex = 0;
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(64)))), ((int)(((byte)(69)))));
            this.button1.BackgroundImage = global::Gwearable.Properties.Resources.close;
            this.button1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.button1.Dock = System.Windows.Forms.DockStyle.Right;
            this.button1.FlatAppearance.BorderSize = 0;
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(606, 0);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(25, 25);
            this.button1.TabIndex = 1;
            this.button1.UseVisualStyleBackColor = false;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            this.button1.MouseDown += new System.Windows.Forms.MouseEventHandler(this.button1_MouseDown);
            this.button1.MouseEnter += new System.EventHandler(this.button1_MouseEnter);
            this.button1.MouseLeave += new System.EventHandler(this.button1_MouseLeave);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(3, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(89, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Work directory";
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.Black;
            this.panel2.Controls.Add(this.toolStrip1);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel2.Location = new System.Drawing.Point(0, 25);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(631, 26);
            this.panel2.TabIndex = 1;
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(50)))), ((int)(((byte)(54)))), ((int)(((byte)(58)))));
            this.toolStrip1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.FileTypeFilter,
            this.AddRecordFile,
            this.SearchRecordFileFolder});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(631, 26);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Paint += new System.Windows.Forms.PaintEventHandler(this.toolStrip1_Paint);
            // 
            // FileTypeFilter
            // 
            this.FileTypeFilter.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.FileTypeFilter.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.toolStripMenuItem1,
            this.toolStripMenuItem2,
            this.toolStripMenuItem3,
            this.toolStripMenuItem4});
            this.FileTypeFilter.Image = global::Gwearable.Properties.Resources.listfile1;
            this.FileTypeFilter.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.FileTypeFilter.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.FileTypeFilter.Name = "FileTypeFilter";
            this.FileTypeFilter.Size = new System.Drawing.Size(26, 23);
            this.FileTypeFilter.Text = "toolStripSplitButton1";
            // 
            // toolStripMenuItem1
            // 
            this.toolStripMenuItem1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(52)))), ((int)(((byte)(57)))), ((int)(((byte)(63)))));
            this.toolStripMenuItem1.BackgroundImage = global::Gwearable.Properties.Resources.backgrounds;
            this.toolStripMenuItem1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStripMenuItem1.Checked = true;
            this.toolStripMenuItem1.CheckOnClick = true;
            this.toolStripMenuItem1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripMenuItem1.Name = "toolStripMenuItem1";
            this.toolStripMenuItem1.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem1.Text = "RAW";
            // 
            // toolStripMenuItem2
            // 
            this.toolStripMenuItem2.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.toolStripMenuItem2.BackgroundImage = global::Gwearable.Properties.Resources.backgrounds;
            this.toolStripMenuItem2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStripMenuItem2.Checked = true;
            this.toolStripMenuItem2.CheckOnClick = true;
            this.toolStripMenuItem2.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripMenuItem2.Name = "toolStripMenuItem2";
            this.toolStripMenuItem2.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem2.Text = "BVH";
            // 
            // toolStripMenuItem3
            // 
            this.toolStripMenuItem3.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.toolStripMenuItem3.BackgroundImage = global::Gwearable.Properties.Resources.backgrounds;
            this.toolStripMenuItem3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStripMenuItem3.Checked = true;
            this.toolStripMenuItem3.CheckOnClick = true;
            this.toolStripMenuItem3.CheckState = System.Windows.Forms.CheckState.Checked;
            this.toolStripMenuItem3.Name = "toolStripMenuItem3";
            this.toolStripMenuItem3.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem3.Text = "FBX";
            // 
            // toolStripMenuItem4
            // 
            this.toolStripMenuItem4.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.toolStripMenuItem4.BackgroundImage = global::Gwearable.Properties.Resources.backgrounds;
            this.toolStripMenuItem4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.toolStripMenuItem4.CheckOnClick = true;
            this.toolStripMenuItem4.Name = "toolStripMenuItem4";
            this.toolStripMenuItem4.Size = new System.Drawing.Size(152, 22);
            this.toolStripMenuItem4.Text = "FCD";
            // 
            // AddRecordFile
            // 
            this.AddRecordFile.AutoSize = false;
            this.AddRecordFile.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.AddRecordFile.Image = global::Gwearable.Properties.Resources.addfile1;
            this.AddRecordFile.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.AddRecordFile.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.AddRecordFile.Name = "AddRecordFile";
            this.AddRecordFile.Size = new System.Drawing.Size(23, 23);
            this.AddRecordFile.Text = "toolStripButton1";
            this.AddRecordFile.ToolTipText = "Add file";
            this.AddRecordFile.Click += new System.EventHandler(this.toolStripButton1_Click);
            // 
            // SearchRecordFileFolder
            // 
            this.SearchRecordFileFolder.AutoSize = false;
            this.SearchRecordFileFolder.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.SearchRecordFileFolder.Image = global::Gwearable.Properties.Resources.openfloder1;
            this.SearchRecordFileFolder.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.SearchRecordFileFolder.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.SearchRecordFileFolder.Name = "SearchRecordFileFolder";
            this.SearchRecordFileFolder.Size = new System.Drawing.Size(23, 23);
            this.SearchRecordFileFolder.Text = "toolStripButton2";
            this.SearchRecordFileFolder.Click += new System.EventHandler(this.toolStripButton2_Click);
            // 
            // panel3
            // 
            this.panel3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(35)))), ((int)(((byte)(39)))));
            this.panel3.Controls.Add(this.RecordFileListBox);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 51);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(631, 369);
            this.panel3.TabIndex = 2;
            // 
            // RecordFileListBox
            // 
            this.RecordFileListBox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(81)))), ((int)(((byte)(90)))));
            this.RecordFileListBox.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.RecordFileListBox.ColumnWidth = 100;
            this.RecordFileListBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.RecordFileListBox.DrawMode = System.Windows.Forms.DrawMode.OwnerDrawFixed;
            this.RecordFileListBox.FormattingEnabled = true;
            this.RecordFileListBox.ItemHeight = 20;
            this.RecordFileListBox.Location = new System.Drawing.Point(0, 0);
            this.RecordFileListBox.Name = "RecordFileListBox";
            this.RecordFileListBox.Size = new System.Drawing.Size(631, 369);
            this.RecordFileListBox.Sorted = true;
            this.RecordFileListBox.TabIndex = 0;
            this.RecordFileListBox.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.RecordFileListBox_DrawItem);
            this.RecordFileListBox.SelectedIndexChanged += new System.EventHandler(this.RecordFileListBox_SelectedIndexChanged);
            this.RecordFileListBox.DoubleClick += new System.EventHandler(this.RecordFileListBox_DoubleClick);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "RAW Files|*.raw|BVH Files|*.bvh|FBX Files|*.fbx|FCD Files|*.fcd|All Files|*.*";
            // 
            // openFileDialog2
            // 
            this.openFileDialog2.FileName = "openFileDialog2";
            this.openFileDialog2.Title = "1111111111";
            // 
            // WorkDirForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel3);
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "WorkDirForm";
            this.Size = new System.Drawing.Size(631, 420);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            this.panel2.PerformLayout();
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.panel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripSplitButton FileTypeFilter;
        private System.Windows.Forms.ToolStripButton AddRecordFile;
        private System.Windows.Forms.ToolStripButton SearchRecordFileFolder;
        private System.Windows.Forms.Panel panel3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem1;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem2;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem3;
        private System.Windows.Forms.ToolStripMenuItem toolStripMenuItem4;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.OpenFileDialog openFileDialog2;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.ListBox RecordFileListBox;
    }
}
