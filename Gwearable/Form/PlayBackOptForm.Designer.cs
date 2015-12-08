namespace Gwearable
{
    partial class PlayBackOptForm
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
            this.CloseBtn = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.panel2 = new System.Windows.Forms.Panel();
            this.LoopPicbox = new System.Windows.Forms.PictureBox();
            this.PlaySpeedCombox = new System.Windows.Forms.ComboBox();
            this.LastFrameBtn = new System.Windows.Forms.Button();
            this.NextFrameBtn = new System.Windows.Forms.Button();
            this.PlayBtn = new System.Windows.Forms.Button();
            this.PlayBackBtn = new System.Windows.Forms.Button();
            this.PreFrameBtn = new System.Windows.Forms.Button();
            this.FirstFrameBtn = new System.Windows.Forms.Button();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.LoopPicbox)).BeginInit();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(58)))), ((int)(((byte)(64)))), ((int)(((byte)(69)))));
            this.panel1.Controls.Add(this.CloseBtn);
            this.panel1.Controls.Add(this.label1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(964, 25);
            this.panel1.TabIndex = 0;
            // 
            // CloseBtn
            // 
            this.CloseBtn.BackgroundImage = global::Gwearable.Properties.Resources.close;
            this.CloseBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.CloseBtn.Dock = System.Windows.Forms.DockStyle.Right;
            this.CloseBtn.FlatAppearance.BorderSize = 0;
            this.CloseBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.CloseBtn.Location = new System.Drawing.Point(939, 0);
            this.CloseBtn.Name = "CloseBtn";
            this.CloseBtn.Size = new System.Drawing.Size(25, 25);
            this.CloseBtn.TabIndex = 2;
            this.CloseBtn.UseVisualStyleBackColor = true;
            this.CloseBtn.Click += new System.EventHandler(this.CloseBtn_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Dock = System.Windows.Forms.DockStyle.Left;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(107, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Play back control";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // panel2
            // 
            this.panel2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(81)))), ((int)(((byte)(90)))));
            this.panel2.Controls.Add(this.LoopPicbox);
            this.panel2.Controls.Add(this.PlaySpeedCombox);
            this.panel2.Controls.Add(this.LastFrameBtn);
            this.panel2.Controls.Add(this.NextFrameBtn);
            this.panel2.Controls.Add(this.PlayBtn);
            this.panel2.Controls.Add(this.PlayBackBtn);
            this.panel2.Controls.Add(this.PreFrameBtn);
            this.panel2.Controls.Add(this.FirstFrameBtn);
            this.panel2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel2.Location = new System.Drawing.Point(0, 25);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(964, 84);
            this.panel2.TabIndex = 1;
            // 
            // LoopPicbox
            // 
            this.LoopPicbox.BackColor = System.Drawing.Color.Transparent;
            this.LoopPicbox.Image = global::Gwearable.Properties.Resources.noloop1;
            this.LoopPicbox.Location = new System.Drawing.Point(470, 28);
            this.LoopPicbox.Name = "LoopPicbox";
            this.LoopPicbox.Size = new System.Drawing.Size(24, 24);
            this.LoopPicbox.TabIndex = 2;
            this.LoopPicbox.TabStop = false;
            this.LoopPicbox.Click += new System.EventHandler(this.pictureBox2_Click);
            // 
            // PlaySpeedCombox
            // 
            this.PlaySpeedCombox.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(81)))), ((int)(((byte)(90)))));
            this.PlaySpeedCombox.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PlaySpeedCombox.ForeColor = System.Drawing.SystemColors.WindowText;
            this.PlaySpeedCombox.FormattingEnabled = true;
            this.PlaySpeedCombox.Items.AddRange(new object[] {
            "1/10",
            "1/5",
            "1/2",
            "1X",
            "2X",
            "5X",
            "10X",
            "20X"});
            this.PlaySpeedCombox.Location = new System.Drawing.Point(520, 28);
            this.PlaySpeedCombox.Name = "PlaySpeedCombox";
            this.PlaySpeedCombox.Size = new System.Drawing.Size(114, 20);
            this.PlaySpeedCombox.TabIndex = 1;
            this.PlaySpeedCombox.Text = "1X";
            this.PlaySpeedCombox.SelectedIndexChanged += new System.EventHandler(this.PlaySpeedCombox_SelectedIndexChanged);
            // 
            // LastFrameBtn
            // 
            this.LastFrameBtn.BackgroundImage = global::Gwearable.Properties.Resources.lastframe;
            this.LastFrameBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.LastFrameBtn.FlatAppearance.BorderSize = 0;
            this.LastFrameBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.LastFrameBtn.Location = new System.Drawing.Point(398, 22);
            this.LastFrameBtn.Name = "LastFrameBtn";
            this.LastFrameBtn.Size = new System.Drawing.Size(50, 38);
            this.LastFrameBtn.TabIndex = 0;
            this.LastFrameBtn.UseVisualStyleBackColor = true;
            this.LastFrameBtn.Click += new System.EventHandler(this.LastFrameBtn_Click);
            // 
            // NextFrameBtn
            // 
            this.NextFrameBtn.BackgroundImage = global::Gwearable.Properties.Resources.nextframe;
            this.NextFrameBtn.FlatAppearance.BorderSize = 0;
            this.NextFrameBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.NextFrameBtn.Location = new System.Drawing.Point(347, 22);
            this.NextFrameBtn.Name = "NextFrameBtn";
            this.NextFrameBtn.Size = new System.Drawing.Size(50, 38);
            this.NextFrameBtn.TabIndex = 0;
            this.NextFrameBtn.UseVisualStyleBackColor = true;
            this.NextFrameBtn.Click += new System.EventHandler(this.NextFrameBtn_Click);
            // 
            // PlayBtn
            // 
            this.PlayBtn.BackgroundImage = global::Gwearable.Properties.Resources.play;
            this.PlayBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PlayBtn.FlatAppearance.BorderSize = 0;
            this.PlayBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PlayBtn.Location = new System.Drawing.Point(270, 22);
            this.PlayBtn.Name = "PlayBtn";
            this.PlayBtn.Size = new System.Drawing.Size(76, 38);
            this.PlayBtn.TabIndex = 0;
            this.PlayBtn.UseVisualStyleBackColor = true;
            this.PlayBtn.Click += new System.EventHandler(this.PlayBtn_Click);
            this.PlayBtn.MouseLeave += new System.EventHandler(this.button4_MouseLeave);
            this.PlayBtn.MouseHover += new System.EventHandler(this.button4_MouseHover);
            // 
            // PlayBackBtn
            // 
            this.PlayBackBtn.BackgroundImage = global::Gwearable.Properties.Resources.playback;
            this.PlayBackBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PlayBackBtn.FlatAppearance.BorderSize = 0;
            this.PlayBackBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PlayBackBtn.Location = new System.Drawing.Point(193, 22);
            this.PlayBackBtn.Name = "PlayBackBtn";
            this.PlayBackBtn.Size = new System.Drawing.Size(76, 38);
            this.PlayBackBtn.TabIndex = 0;
            this.PlayBackBtn.UseVisualStyleBackColor = true;
            this.PlayBackBtn.Click += new System.EventHandler(this.PlayBackBtn_Click);
            // 
            // PreFrameBtn
            // 
            this.PreFrameBtn.BackgroundImage = global::Gwearable.Properties.Resources.preframe;
            this.PreFrameBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.PreFrameBtn.FlatAppearance.BorderSize = 0;
            this.PreFrameBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.PreFrameBtn.Location = new System.Drawing.Point(142, 22);
            this.PreFrameBtn.Name = "PreFrameBtn";
            this.PreFrameBtn.Size = new System.Drawing.Size(50, 38);
            this.PreFrameBtn.TabIndex = 0;
            this.PreFrameBtn.UseVisualStyleBackColor = true;
            this.PreFrameBtn.Click += new System.EventHandler(this.PreFrameBtn_Click);
            // 
            // FirstFrameBtn
            // 
            this.FirstFrameBtn.BackColor = System.Drawing.SystemColors.ControlDarkDark;
            this.FirstFrameBtn.BackgroundImage = global::Gwearable.Properties.Resources.firstframe;
            this.FirstFrameBtn.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.FirstFrameBtn.FlatAppearance.BorderSize = 0;
            this.FirstFrameBtn.FlatAppearance.MouseOverBackColor = System.Drawing.Color.Black;
            this.FirstFrameBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FirstFrameBtn.Location = new System.Drawing.Point(91, 22);
            this.FirstFrameBtn.Name = "FirstFrameBtn";
            this.FirstFrameBtn.Size = new System.Drawing.Size(50, 38);
            this.FirstFrameBtn.TabIndex = 0;
            this.FirstFrameBtn.UseVisualStyleBackColor = false;
            this.FirstFrameBtn.Click += new System.EventHandler(this.FirstFrameBtn_Click);
            // 
            // PlayBackOptForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel2);
            this.Controls.Add(this.panel1);
            this.Name = "PlayBackOptForm";
            this.Size = new System.Drawing.Size(964, 109);
            this.panel1.ResumeLayout(false);
            this.panel1.PerformLayout();
            this.panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.LoopPicbox)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Panel panel2;
        private System.Windows.Forms.ComboBox PlaySpeedCombox;
        private System.Windows.Forms.Button LastFrameBtn;
        private System.Windows.Forms.Button NextFrameBtn;
        private System.Windows.Forms.Button PlayBtn;
        private System.Windows.Forms.Button PlayBackBtn;
        private System.Windows.Forms.Button PreFrameBtn;
        private System.Windows.Forms.Button FirstFrameBtn;
        private System.Windows.Forms.PictureBox LoopPicbox;
        private System.Windows.Forms.Button CloseBtn;
    }
}
