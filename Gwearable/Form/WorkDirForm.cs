using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;
using System.IO;

namespace Gwearable
{
    public partial class WorkDirForm : UserControl
    {
        public WorkDirForm()
        {
            InitializeComponent();
            LoadFiles();
        }
        public void LoadFiles()
        {
            DirectoryInfo dir = new DirectoryInfo(Application.StartupPath+"\\MotionFiles");

            ArrayList Flst = GetAll(dir);
            string str = Application.StartupPath;
            for (int i = 0; i < Flst.Count;i++ )
            {
                RecordFileListBox.Items.Add(Flst[i].ToString());
            }
        }

        ArrayList GetAll(DirectoryInfo dir)//搜索文件夹中的文件
        {
            ArrayList FileList = new ArrayList();

            FileInfo[] allFile = dir.GetFiles();
            foreach (FileInfo fi in allFile)
            {
                FileList.Add(fi.Name);
            }

            DirectoryInfo[] allDir = dir.GetDirectories();
            foreach (DirectoryInfo d in allDir)
            {
                GetAll(d);
            }
            return FileList;
        }





        private void button1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        
        //private void listView1_DrawItem(object sender, DrawListViewItemEventArgs e)
        //{
        //    SolidBrush dark = new SolidBrush(Color.DarkGray);
        //    SolidBrush black = new SolidBrush(Color.Black);
        //    StringFormat stringFormat = new StringFormat();

        //    stringFormat.Alignment = StringAlignment.Center;

        //    Rectangle rec1 = listView1.GetItemRect(0);
        //    e.Graphics.FillRectangle(dark, rec1);

        //    Rectangle rec2 = listView1.GetItemRect(1);
        //    e.Graphics.FillRectangle(dark, rec2);



        //    for (int i = 0; i < listView1.Columns.Count; i++)
        //    {
        //        Rectangle rec = listView1.GetItemRect(i);
        //        e.Graphics.DrawString(listView1.Columns[i].Text, new Font("宋体", 9), black, rec, stringFormat);
        //    }
        //}

        private void toolStripButton1_Click(object sender, EventArgs e)
        {
            //openFileDialog1.ShowDialog();
            ShowOpenFileDialog();
        }

        #region 打开对话框
        private void ShowOpenFileDialog()
        {
            //string localFilePath, fileNameExt, newFileName, FilePath; 
            OpenFileDialog ofd = new OpenFileDialog();
            //设置文件类型 
            ofd.Filter = "动作捕捉文件（*.bvh）|*.bvh";

            //设置默认文件类型显示顺序 
            ofd.FilterIndex = 1;

            //保存对话框是否记忆上次打开的目录
            ofd.RestoreDirectory = true;

            ofd.InitialDirectory = Application.StartupPath+"\\MotionFiles";

            //点了保存按钮进入 
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string resultFile = ofd.FileName.ToString();
                string fileNameExt = resultFile.Substring(resultFile.LastIndexOf("\\") + 1); //获取文件名，不带路径
                RecordFileListBox.Items.Add(fileNameExt);
            }
        }
        #endregion
                
        private void toolStripButton2_Click(object sender, EventArgs e)
        {
            //openFileDialog2.ShowDialog();
            //folderBrowserDialog1.ShowDialog();
        }

        private void toolStrip1_Paint(object sender, PaintEventArgs e)
        {
            if ((sender as ToolStrip).RenderMode == ToolStripRenderMode.System)
            {
                Rectangle rect = new Rectangle(0, 0, this.toolStrip1.Width-2, this.toolStrip1.Height-2 );
                e.Graphics.SetClip(rect);
            }
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            //button1.BackgroundImage = Properties.Resources.close_back_hit;
        }

        private void button1_MouseEnter(object sender, EventArgs e)
        {
            //button1.BackgroundImage = Properties.Resources.close_back_hover;
        }

        private void button1_MouseLeave(object sender, EventArgs e)
        {
            //button1.BackgroundImage = Properties.Resources.close_back_normal;
        }

        private void RecordFileListBox_DoubleClick(object sender, EventArgs e)
        {
            int selectfileIndex = RecordFileListBox.SelectedIndex;
            Global.PlayBackFileName = RecordFileListBox.Items[selectfileIndex].ToString();

            //FileStream fs = new FileStream(Application.StartupPath + "\\MotionFiles\\" + Global.PlayBackFileName, FileMode.Open, FileAccess.Read);

            BVH.LoadRawFile(Global.PlayBackFileName); //在此处加载文件 计算帧数等等预备工作,然后playbtn的主要工作就是控制帧数 跳跃。。

            Global.isPlayBack = true;
            Global.isLive = false;
        }

        private void RecordFileListBox_DrawItem(object sender, DrawItemEventArgs e)
        {
            e.DrawBackground();
            e.DrawFocusRectangle();

            //让文字位于Item的中间
            float difH = (e.Bounds.Height - e.Font.Height) / 2;
            RectangleF rf = new RectangleF(e.Bounds.X, e.Bounds.Y + difH, e.Bounds.Width, e.Font.Height);

            e.Graphics.DrawString(RecordFileListBox.Items[e.Index].ToString(), e.Font, new SolidBrush(e.ForeColor), rf);
        }

        private void RecordFileListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int selectfileIndex = RecordFileListBox.SelectedIndex;
            Global.PlayBackFileName = RecordFileListBox.Items[selectfileIndex].ToString();
        }
    }
}
