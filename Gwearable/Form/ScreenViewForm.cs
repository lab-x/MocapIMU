using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CsGL;
using MyOpenGL;
using System.Runtime.InteropServices;
using Gwearable.Until;
namespace Gwearable
{
    public partial class ScreenViewForm : UserControl
    {
        private OpenGL glWindow = new OpenGL();
        //private OpenGLEX glWindowEX = new OpenGLEX();

        public ScreenViewForm()
        {
            InitializeComponent();
            //OpenGL
            this.glWindow.Parent = this;
            this.glWindow.Dock = DockStyle.Fill; 

            //OpenGLEX
            //this.glWindowEX.Parent = this;
            //this.glWindowEX.Dock = DockStyle.Fill;

            //this.glWindow.MouseMove += new System.Windows.Forms.MouseEventHandler(this.glWindow.glOnMouseMove);
            //this.glWindow.MouseWheel += new System.Windows.Forms.MouseEventHandler(this.glWindow.glOnMiddleMouseonWheel);
            //this.glWindow.MouseHover += new EventHandler(this.glWindow.glOnMouseHover);
            //OpenGLView.Refresh();
        }

        private void panel1_SizeChanged(object sender, EventArgs e)
        {
            //this.glWindow.PlotGL();
        }

        private void defineScreenView_Load(object sender, EventArgs e)
        {
            //this.glWindow.BackgroundImage = Properties.Resources.back;
        }

        private void defineScreenView_SizeChanged(object sender, EventArgs e)
        {
            //this.glWindow.PlotGL();
        }

        public void SetBoneLen(int boneId,int xyz,float len)
        {
      //      glWindow.SetBoneLen(boneId,xyz,len);
        }

        public OpenGL GetOpenGLWindow()
        {
//             if (glWindow!= null)
//             {
//                 return glWindow;
//             }
            return null;
        }
    }
}
