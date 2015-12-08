namespace Gwearable
{
    partial class TPoseAPoseForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.CalibrateProcessBar = new System.Windows.Forms.ProgressBar();
            this.StartCalibrateBtn = new System.Windows.Forms.Button();
            this.FinishedCaliBtn = new System.Windows.Forms.Button();
            this.APoseProgressBar = new System.Windows.Forms.ProgressBar();
            this.Xpose = new System.Windows.Forms.Button();
            this.Apose = new System.Windows.Forms.Button();
            this.Spose = new System.Windows.Forms.Button();
            this.StandardGesture = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.StandardGesture)).BeginInit();
            this.SuspendLayout();
            // 
            // CalibrateProcessBar
            // 
            this.CalibrateProcessBar.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(35)))), ((int)(((byte)(39)))));
            this.CalibrateProcessBar.Location = new System.Drawing.Point(80, 332);
            this.CalibrateProcessBar.Name = "CalibrateProcessBar";
            this.CalibrateProcessBar.Size = new System.Drawing.Size(397, 23);
            this.CalibrateProcessBar.TabIndex = 1;
            this.CalibrateProcessBar.Visible = false;
            // 
            // StartCalibrateBtn
            // 
            this.StartCalibrateBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.StartCalibrateBtn.ForeColor = System.Drawing.Color.White;
            this.StartCalibrateBtn.Location = new System.Drawing.Point(80, 406);
            this.StartCalibrateBtn.Name = "StartCalibrateBtn";
            this.StartCalibrateBtn.Size = new System.Drawing.Size(75, 23);
            this.StartCalibrateBtn.TabIndex = 2;
            this.StartCalibrateBtn.Text = "TPose";
            this.StartCalibrateBtn.UseVisualStyleBackColor = true;
            this.StartCalibrateBtn.Click += new System.EventHandler(this.StartCalibrateBtn_Click);
            // 
            // FinishedCaliBtn
            // 
            this.FinishedCaliBtn.Enabled = false;
            this.FinishedCaliBtn.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.FinishedCaliBtn.ForeColor = System.Drawing.Color.White;
            this.FinishedCaliBtn.Location = new System.Drawing.Point(402, 406);
            this.FinishedCaliBtn.Name = "FinishedCaliBtn";
            this.FinishedCaliBtn.Size = new System.Drawing.Size(75, 23);
            this.FinishedCaliBtn.TabIndex = 2;
            this.FinishedCaliBtn.Text = "Finished";
            this.FinishedCaliBtn.UseVisualStyleBackColor = true;
            this.FinishedCaliBtn.Click += new System.EventHandler(this.FinishedCaliBtn_Click);
            // 
            // APoseProgressBar
            // 
            this.APoseProgressBar.Location = new System.Drawing.Point(80, 361);
            this.APoseProgressBar.Name = "APoseProgressBar";
            this.APoseProgressBar.Size = new System.Drawing.Size(397, 23);
            this.APoseProgressBar.TabIndex = 4;
            this.APoseProgressBar.Visible = false;
            // 
            // Xpose
            // 
            this.Xpose.Enabled = false;
            this.Xpose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Xpose.ForeColor = System.Drawing.Color.White;
            this.Xpose.Location = new System.Drawing.Point(161, 406);
            this.Xpose.Name = "Xpose";
            this.Xpose.Size = new System.Drawing.Size(75, 23);
            this.Xpose.TabIndex = 2;
            this.Xpose.Text = "XPose";
            this.Xpose.UseVisualStyleBackColor = true;
            this.Xpose.Click += new System.EventHandler(this.Xpose_Click);
            // 
            // Apose
            // 
            this.Apose.Enabled = false;
            this.Apose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Apose.ForeColor = System.Drawing.Color.White;
            this.Apose.Location = new System.Drawing.Point(242, 406);
            this.Apose.Name = "Apose";
            this.Apose.Size = new System.Drawing.Size(75, 23);
            this.Apose.TabIndex = 2;
            this.Apose.Text = "APose";
            this.Apose.UseVisualStyleBackColor = true;
            this.Apose.Click += new System.EventHandler(this.Apose_Click);
            // 
            // Spose
            // 
            this.Spose.Enabled = false;
            this.Spose.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Spose.ForeColor = System.Drawing.Color.White;
            this.Spose.Location = new System.Drawing.Point(321, 406);
            this.Spose.Name = "Spose";
            this.Spose.Size = new System.Drawing.Size(75, 23);
            this.Spose.TabIndex = 2;
            this.Spose.Text = "SPose";
            this.Spose.UseVisualStyleBackColor = true;
            this.Spose.Click += new System.EventHandler(this.Spose_Click);
            // 
            // StandardGesture
            // 
            this.StandardGesture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.StandardGesture.Image = global::Gwearable.Properties.Resources.Tpose;
            this.StandardGesture.Location = new System.Drawing.Point(80, 12);
            this.StandardGesture.Name = "StandardGesture";
            this.StandardGesture.Size = new System.Drawing.Size(397, 297);
            this.StandardGesture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.StandardGesture.TabIndex = 0;
            this.StandardGesture.TabStop = false;
            // 
            // TPoseAPoseForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(33)))), ((int)(((byte)(35)))), ((int)(((byte)(39)))));
            this.ClientSize = new System.Drawing.Size(552, 468);
            this.Controls.Add(this.APoseProgressBar);
            this.Controls.Add(this.FinishedCaliBtn);
            this.Controls.Add(this.Spose);
            this.Controls.Add(this.Apose);
            this.Controls.Add(this.Xpose);
            this.Controls.Add(this.StartCalibrateBtn);
            this.Controls.Add(this.CalibrateProcessBar);
            this.Controls.Add(this.StandardGesture);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "TPoseAPoseForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "TPoseAPoseForm";
            ((System.ComponentModel.ISupportInitialize)(this.StandardGesture)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox StandardGesture;
        private System.Windows.Forms.ProgressBar CalibrateProcessBar;
        private System.Windows.Forms.Button StartCalibrateBtn;
        private System.Windows.Forms.Button FinishedCaliBtn;
        private System.Windows.Forms.ProgressBar APoseProgressBar;
        private System.Windows.Forms.Button Xpose;
        private System.Windows.Forms.Button Apose;
        private System.Windows.Forms.Button Spose;
    }
}