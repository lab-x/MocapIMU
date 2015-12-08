using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Gwearable
{
    public partial class OpenGLOptForm : UserControl
    {
        ToolStrip m_ToolStrip = new ToolStrip();

        public OpenGLOptForm()
        {
            InitializeComponent();
        }
          

        private void toolStrip1_Paint(object sender, PaintEventArgs e)
        {
            if ((sender as ToolStrip).RenderMode == ToolStripRenderMode.System)
            {
                Rectangle rect = new Rectangle(0, 0, this.toolStrip1.Width , this.toolStrip1.Height-2);
                e.Graphics.SetClip(rect);
            }   
        }

        private void toolStripButton1_MouseEnter(object sender, EventArgs e)
        {
            //toolStripButton1.BackgroundImage = Properties.Resources.ground_back_hover;

        }

        private void toolStripButton1_MouseDown(object sender, MouseEventArgs e)
        {
            //toolStripButton1.BackgroundImage = Properties.Resources.ground_back_hit;
        }

        private void toolStripButton1_MouseLeave(object sender, EventArgs e)
        {
            //toolStripButton1.BackgroundImage = Properties.Resources.ground;
        }

        private void toolStripButton5_MouseHover(object sender, EventArgs e)
        {
            //do nothingk
        }

        private void toolStripButton5_MouseLeave(object sender, EventArgs e)
        {
            //toolStripButton5.Image = Properties.Resources.ground_back_normal;
        }

        private void toolStripButton5_MouseEnter(object sender, EventArgs e)
        {
            //toolStripButton5.Image = Properties.Resources.ground_back_hover;
        }

        private void toolStripButton5_MouseDown(object sender, MouseEventArgs e)
        {
            //toolStripButton5.Image = Properties.Resources.ground_back_hit;
        }

        private void BackGroundBtn_Click(object sender, EventArgs e)
        {
            Global.isFloorVisible = !Global.isFloorVisible;
            if (Global.isFloorVisible)
            {
                BackGroundBtn.Checked = true;
            }
            else
            {
                BackGroundBtn.Checked = false;
            }
        }

        private void CrossLineBtn_Click(object sender, EventArgs e)
        {
            Global.isCrossLineVisible = !Global.isCrossLineVisible;
            if (Global.isCrossLineVisible)
            {
                CrossLineBtn.Checked = true;
            }
            else
            {
                CrossLineBtn.Checked = false;
            }
        }

    }
}
