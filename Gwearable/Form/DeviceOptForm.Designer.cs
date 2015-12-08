namespace Gwearable
{
    partial class DeviceOptForm
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
            this.toolStrip1 = new System.Windows.Forms.ToolStrip();
            this.DisconnectBtn = new System.Windows.Forms.ToolStripButton();
            this.ConnectBtn = new System.Windows.Forms.ToolStripButton();
            this.CalibrateBtn = new System.Windows.Forms.ToolStripButton();
            this.ZeroBtn = new System.Windows.Forms.ToolStripButton();
            this.PoweroffBtn = new System.Windows.Forms.ToolStripButton();
            this.OpenFileBtn = new System.Windows.Forms.ToolStripButton();
            this.VideoBtn = new System.Windows.Forms.ToolStripButton();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.ConfigSaveBtn = new System.Windows.Forms.ToolStripButton();
            this.ConfigOpenBtn = new System.Windows.Forms.ToolStripButton();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.toolStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // toolStrip1
            // 
            this.toolStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(75)))), ((int)(((byte)(81)))), ((int)(((byte)(90)))));
            this.toolStrip1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.toolStrip1.GripMargin = new System.Windows.Forms.Padding(0);
            this.toolStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.toolStrip1.ImageScalingSize = new System.Drawing.Size(30, 30);
            this.toolStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.DisconnectBtn,
            this.ConnectBtn,
            this.CalibrateBtn,
            this.ZeroBtn,
            this.PoweroffBtn,
            this.OpenFileBtn,
            this.VideoBtn,
            this.toolStripSeparator1,
            this.toolStripSeparator2,
            this.ConfigSaveBtn,
            this.ConfigOpenBtn});
            this.toolStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.VerticalStackWithOverflow;
            this.toolStrip1.Location = new System.Drawing.Point(0, 0);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.RenderMode = System.Windows.Forms.ToolStripRenderMode.System;
            this.toolStrip1.Size = new System.Drawing.Size(40, 352);
            this.toolStrip1.TabIndex = 0;
            this.toolStrip1.Text = "toolStrip1";
            this.toolStrip1.Paint += new System.Windows.Forms.PaintEventHandler(this.toolStrip1_Paint);
            // 
            // DisconnectBtn
            // 
            this.DisconnectBtn.AutoSize = false;
            this.DisconnectBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.DisconnectBtn.Enabled = false;
            this.DisconnectBtn.Image = global::Gwearable.Properties.Resources.guanbi;
            this.DisconnectBtn.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.DisconnectBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.DisconnectBtn.Name = "DisconnectBtn";
            this.DisconnectBtn.Size = new System.Drawing.Size(32, 32);
            this.DisconnectBtn.Text = "toolStripButton4";
            this.DisconnectBtn.ToolTipText = "Disconnect";
            this.DisconnectBtn.Click += new System.EventHandler(this.DisconnectBtn_Click);
            // 
            // ConnectBtn
            // 
            this.ConnectBtn.AutoSize = false;
            this.ConnectBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ConnectBtn.Image = global::Gwearable.Properties.Resources.lianjie;
            this.ConnectBtn.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ConnectBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ConnectBtn.Name = "ConnectBtn";
            this.ConnectBtn.Size = new System.Drawing.Size(32, 32);
            this.ConnectBtn.Text = "toolStripButton3";
            this.ConnectBtn.ToolTipText = "Connect";
            this.ConnectBtn.Click += new System.EventHandler(this.ConnectBtn_Click);
            // 
            // CalibrateBtn
            // 
            this.CalibrateBtn.AutoSize = false;
            this.CalibrateBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.CalibrateBtn.Enabled = false;
            this.CalibrateBtn.Image = global::Gwearable.Properties.Resources.jiaozhun;
            this.CalibrateBtn.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.CalibrateBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.CalibrateBtn.Name = "CalibrateBtn";
            this.CalibrateBtn.Size = new System.Drawing.Size(32, 32);
            this.CalibrateBtn.Text = "Calibration(&A)";
            this.CalibrateBtn.ToolTipText = "Calibration";
            this.CalibrateBtn.Click += new System.EventHandler(this.CalibrateBtn_Click);
            // 
            // ZeroBtn
            // 
            this.ZeroBtn.AutoSize = false;
            this.ZeroBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ZeroBtn.ForeColor = System.Drawing.Color.Snow;
            this.ZeroBtn.Image = global::Gwearable.Properties.Resources.zero;
            this.ZeroBtn.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.ZeroBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ZeroBtn.Name = "ZeroBtn";
            this.ZeroBtn.Size = new System.Drawing.Size(32, 32);
            this.ZeroBtn.Text = "toolStripButton7";
            this.ZeroBtn.ToolTipText = "Zero out";
            this.ZeroBtn.Click += new System.EventHandler(this.ZeroBtn_Click);
            // 
            // PoweroffBtn
            // 
            this.PoweroffBtn.AutoSize = false;
            this.PoweroffBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.PoweroffBtn.Enabled = false;
            this.PoweroffBtn.Image = global::Gwearable.Properties.Resources.zeroout;
            this.PoweroffBtn.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.PoweroffBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.PoweroffBtn.Name = "PoweroffBtn";
            this.PoweroffBtn.Size = new System.Drawing.Size(32, 32);
            this.PoweroffBtn.Text = "toolStripButton5";
            this.PoweroffBtn.ToolTipText = "Power off";
            this.PoweroffBtn.Click += new System.EventHandler(this.PoweroffBtn_Click);
            // 
            // OpenFileBtn
            // 
            this.OpenFileBtn.AutoSize = false;
            this.OpenFileBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.OpenFileBtn.Image = global::Gwearable.Properties.Resources.wenjian;
            this.OpenFileBtn.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.OpenFileBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.OpenFileBtn.Name = "OpenFileBtn";
            this.OpenFileBtn.Size = new System.Drawing.Size(32, 32);
            this.OpenFileBtn.Text = "toolStripButton1";
            this.OpenFileBtn.ToolTipText = "Open file";
            this.OpenFileBtn.Click += new System.EventHandler(this.OpenFileBtn_Click);
            // 
            // VideoBtn
            // 
            this.VideoBtn.AutoSize = false;
            this.VideoBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.VideoBtn.Image = global::Gwearable.Properties.Resources.luzhi;
            this.VideoBtn.ImageScaling = System.Windows.Forms.ToolStripItemImageScaling.None;
            this.VideoBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.VideoBtn.Name = "VideoBtn";
            this.VideoBtn.Size = new System.Drawing.Size(32, 32);
            this.VideoBtn.Text = "toolStripButton2";
            this.VideoBtn.ToolTipText = "Record";
            this.VideoBtn.Click += new System.EventHandler(this.VideoBtn_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(38, 6);
            this.toolStripSeparator1.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical90;
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            this.toolStripSeparator2.Size = new System.Drawing.Size(38, 6);
            this.toolStripSeparator2.TextDirection = System.Windows.Forms.ToolStripTextDirection.Vertical90;
            // 
            // ConfigSaveBtn
            // 
            this.ConfigSaveBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ConfigSaveBtn.Image = global::Gwearable.Properties.Resources.marker;
            this.ConfigSaveBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ConfigSaveBtn.Name = "ConfigSaveBtn";
            this.ConfigSaveBtn.Size = new System.Drawing.Size(38, 34);
            this.ConfigSaveBtn.ToolTipText = "SaveConfig";
            this.ConfigSaveBtn.Click += new System.EventHandler(this.ConfigSaveBtn_Click);
            // 
            // ConfigOpenBtn
            // 
            this.ConfigOpenBtn.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image;
            this.ConfigOpenBtn.Image = global::Gwearable.Properties.Resources.record;
            this.ConfigOpenBtn.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.ConfigOpenBtn.Name = "ConfigOpenBtn";
            this.ConfigOpenBtn.Size = new System.Drawing.Size(38, 34);
            this.ConfigOpenBtn.ToolTipText = "OpenConfig";
            this.ConfigOpenBtn.Click += new System.EventHandler(this.ConfigOpenBtn_Click);
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            this.openFileDialog1.Filter = "RAW Files|*.raw";
            // 
            // saveFileDialog1
            // 
            this.saveFileDialog1.CheckFileExists = true;
            this.saveFileDialog1.Filter = "BVH Files|*.BVH";
            this.saveFileDialog1.Title = "录制文件";
            // 
            // DeviceOptForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Controls.Add(this.toolStrip1);
            this.Name = "DeviceOptForm";
            this.Size = new System.Drawing.Size(40, 352);
            this.toolStrip1.ResumeLayout(false);
            this.toolStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ToolStrip toolStrip1;
        private System.Windows.Forms.ToolStripButton OpenFileBtn;
        private System.Windows.Forms.ToolStripButton VideoBtn;
        private System.Windows.Forms.ToolStripButton ConnectBtn;
        private System.Windows.Forms.ToolStripButton DisconnectBtn;
        private System.Windows.Forms.ToolStripButton PoweroffBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripButton CalibrateBtn;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripButton ZeroBtn;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.Windows.Forms.ToolStripButton ConfigSaveBtn;
        private System.Windows.Forms.ToolStripButton ConfigOpenBtn;
    }
}
