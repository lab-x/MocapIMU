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
    public partial class LinkInfoForm : UserControl
    {
        static int flag = 0;

        int[] m_Data = new int[17] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };


        public LinkInfoForm()
        {
            InitializeComponent();

            customScrollbar1.Visible = false;

        }
        
        public void RefreshImage(int[] ration)
        {
            //for (int i = 0; i < 17; i++)
            //{
            //    if (0 <= ArrayData.ration[i] && ArrayData.ration[i] < 20)
            //    {
            //        ChangePBoxImage(i, 1);
            //    }
            //    else if (20 <= ArrayData.ration[i] && ArrayData.ration[i] < 70)
            //    {
            //        ChangePBoxImage(i, 2);
            //    }
            //    else if (70 <= ArrayData.ration[i] && ArrayData.ration[i] < 90)
            //    {
            //        ChangePBoxImage(i, 3);
            //    }
            //    else if (90 <= ArrayData.ration[i])
            //    {
            //        ChangePBoxImage(i, 0);
            //    }

            //}

            UpdateText(ration);

            
        }

        //更新链接信号状态以及信号丢失率
        private void UpdateText(int[] data)
        {

//             label7.Text = ArrayData.ration[0].ToString() + "%";
//             label11.Text = ArrayData.ration[1].ToString() + "%";
//             label15.Text = ArrayData.ration[2].ToString() + "%";
//             label19.Text = ArrayData.ration[3].ToString() + "%";
//             label45.Text = ArrayData.ration[4].ToString() + "%";
//             label46.Text = ArrayData.ration[5].ToString() + "%";
//             label47.Text = ArrayData.ration[6].ToString() + "%";
//             label48.Text = ArrayData.ration[7].ToString() + "%";
//             label49.Text = ArrayData.ration[8].ToString() + "%";
//             label50.Text = ArrayData.ration[9].ToString() + "%";
//             label51.Text = ArrayData.ration[10].ToString() + "%";
//             label52.Text = ArrayData.ration[11].ToString() + "%";
//             label53.Text = ArrayData.ration[12].ToString() + "%";
//             label54.Text = ArrayData.ration[13].ToString() + "%";
//             label55.Text = ArrayData.ration[14].ToString() + "%";
//             label56.Text = ArrayData.ration[15].ToString() + "%";
//             label71.Text = ArrayData.ration[16].ToString() + "%";

            for (int i= 0;i<17;i++)
            {
                
                if (data[i]>=100)
                {
                    glacialList1.Items[i].SubItems[2].Text = "100%";
                }
                else
                {
                    glacialList1.Items[i].SubItems[2].Text = data[i].ToString() + "%";
                }
                
                if (0 <= data[i] && data[i] < 20)
                {
                    glacialList1.Items[i].SubItems[2].ImageIndex = 0;
                }
                else if (20 <= data[i] && data[i] < 70)
                {
                    glacialList1.Items[i].SubItems[2].ImageIndex = 1;
                }
                else if (70 <= data[i] && data[i] < 90)
                {
                    glacialList1.Items[i].SubItems[2].ImageIndex = 2;
                }
                else
                {
                    glacialList1.Items[i].SubItems[2].ImageIndex = 3;
                }
            }
            

        }

        //private void ChangePBoxImage(int i, int status)
        //{
        //    if (0 == i)
        //    {
        //        if (1 == status)
        //        {
        //            pictureBox7.BackgroundImage = Properties.Resources.point;
        //        }
        //        else if (2 == status)
        //        {
        //            pictureBox7.BackgroundImage = Properties.Resources.linkerror;
        //        }
        //        else if (3 == status)
        //        {
        //            pictureBox7.BackgroundImage = Properties.Resources.linksemisucc;
        //        }
        //        else if (0 == status)
        //        {
        //            pictureBox7.BackgroundImage = Properties.Resources.linksucc;
        //        }

        //    }
        //    else if (1 == i)
        //    {
        //        if (1 == status)
        //        {
        //            pictureBox8.BackgroundImage = Properties.Resources.point;
        //        }
        //        else if (2 == status)
        //        {
        //            pictureBox8.BackgroundImage = Properties.Resources.linkerror;
        //        }
        //        else if (3 == status)
        //        {
        //            pictureBox8.BackgroundImage = Properties.Resources.linksemisucc;
        //        }
        //        else if (0 == status)
        //        {
        //            pictureBox8.BackgroundImage = Properties.Resources.linksucc;
        //        }
        //    }
        //    else if (2 == i)
        //    {
        //        if (1 == status)
        //        {
        //            pictureBox14.BackgroundImage = Properties.Resources.point;
        //        }
        //        else if (2 == status)
        //        {
        //            pictureBox14.BackgroundImage = Properties.Resources.linkerror;
        //        }
        //        else if (3 == status)
        //        {
        //            pictureBox14.BackgroundImage = Properties.Resources.linksemisucc;
        //        }
        //        else if (0 == status)
        //        {
        //            pictureBox14.BackgroundImage = Properties.Resources.linksucc;
        //        }
        //    }
        //    else if (3 == i)
        //    {
        //        if (1 == status)
        //        {
        //            pictureBox15.BackgroundImage = Properties.Resources.point;
        //        }
        //        else if (2 == status)
        //        {
        //            pictureBox15.BackgroundImage = Properties.Resources.linkerror;
        //        }
        //        else if (3 == status)
        //        {
        //            pictureBox15.BackgroundImage = Properties.Resources.linksemisucc;
        //        }
        //        else if (0 == status)
        //        {
        //            pictureBox15.BackgroundImage = Properties.Resources.linksucc;
        //        }
        //    }
        //    else if (4 == i)
        //    {
        //        if (1 == status)
        //        {
        //            pictureBox16.BackgroundImage = Properties.Resources.point;
        //        }
        //        else if (2 == status)
        //        {
        //            pictureBox16.BackgroundImage = Properties.Resources.linkerror;
        //        }
        //        else if (3 == status)
        //        {
        //            pictureBox16.BackgroundImage = Properties.Resources.linksemisucc;
        //        }
        //        else if (0 == status)
        //        {
        //            pictureBox16.BackgroundImage = Properties.Resources.linksucc;
        //        }
        //    }
        //    else if (5 == i)
        //    {
        //        if (1 == status)
        //        {
        //            pictureBox17.BackgroundImage = Properties.Resources.point;
        //        }
        //        else if (2 == status)
        //        {
        //            pictureBox17.BackgroundImage = Properties.Resources.linkerror;
        //        }
        //        else if (3 == status)
        //        {
        //            pictureBox17.BackgroundImage = Properties.Resources.linksemisucc;
        //        }
        //        else if (0 == status)
        //        {
        //            pictureBox17.BackgroundImage = Properties.Resources.linksucc;
        //        }
        //    }
        //    else if (6 == i)
        //    {
        //        if (1 == status)
        //        {
        //            pictureBox18.BackgroundImage = Properties.Resources.point;
        //        }
        //        else if (2 == status)
        //        {
        //            pictureBox18.BackgroundImage = Properties.Resources.linkerror;
        //        }
        //        else if (3 == status)
        //        {
        //            pictureBox18.BackgroundImage = Properties.Resources.linksemisucc;
        //        }
        //        else if (0 == status)
        //        {
        //            pictureBox18.BackgroundImage = Properties.Resources.linksucc;
        //        }
        //    }
        //    else if (7 == i)
        //    {
        //        if (1 == status)
        //        {
        //            pictureBox19.BackgroundImage = Properties.Resources.point;
        //        }
        //        else if (2 == status)
        //        {
        //            pictureBox19.BackgroundImage = Properties.Resources.linkerror;
        //        }
        //        else if (3 == status)
        //        {
        //            pictureBox19.BackgroundImage = Properties.Resources.linksemisucc;
        //        }
        //        else if (0 == status)
        //        {
        //            pictureBox19.BackgroundImage = Properties.Resources.linksucc;
        //        }
        //    }
        //    else if (8 == i)
        //    {
        //        if (1 == status)
        //        {
        //            pictureBox20.BackgroundImage = Properties.Resources.point;
        //        }
        //        else if (2 == status)
        //        {
        //            pictureBox20.BackgroundImage = Properties.Resources.linkerror;
        //        }
        //        else if (3 == status)
        //        {
        //            pictureBox20.BackgroundImage = Properties.Resources.linksemisucc;
        //        }
        //        else if (0 == status)
        //        {
        //            pictureBox20.BackgroundImage = Properties.Resources.linksucc;
        //        }
        //    }
        //    else if (9 == i)
        //    {
        //        if (1 == status)
        //        {
        //            pictureBox21.BackgroundImage = Properties.Resources.point;
        //        }
        //        else if (2 == status)
        //        {
        //            pictureBox21.BackgroundImage = Properties.Resources.linkerror;
        //        }
        //        else if (3 == status)
        //        {
        //            pictureBox21.BackgroundImage = Properties.Resources.linksemisucc;
        //        }
        //        else if (0 == status)
        //        {
        //            pictureBox21.BackgroundImage = Properties.Resources.linksucc;
        //        }
        //    }
        //    else if (10 == i)
        //    {
        //        if (1 == status)
        //        {
        //            pictureBox22.BackgroundImage = Properties.Resources.point;
        //        }
        //        else if (2 == status)
        //        {
        //            pictureBox22.BackgroundImage = Properties.Resources.linkerror;
        //        }
        //        else if (3 == status)
        //        {
        //            pictureBox22.BackgroundImage = Properties.Resources.linksemisucc;
        //        }
        //        else if (0 == status)
        //        {
        //            pictureBox22.BackgroundImage = Properties.Resources.linksucc;
        //        }
        //    }
        //    else if (11 == i)
        //    {
        //        if (1 == status)
        //        {
        //            pictureBox23.BackgroundImage = Properties.Resources.point;
        //        }
        //        else if (2 == status)
        //        {
        //            pictureBox23.BackgroundImage = Properties.Resources.linkerror;
        //        }
        //        else if (3 == status)
        //        {
        //            pictureBox23.BackgroundImage = Properties.Resources.linksemisucc;
        //        }
        //        else if (0 == status)
        //        {
        //            pictureBox23.BackgroundImage = Properties.Resources.linksucc;
        //        }
        //    }
        //    else if (12 == i)
        //    {
        //        if (1 == status)
        //        {
        //            pictureBox24.BackgroundImage = Properties.Resources.point;
        //        }
        //        else if (2 == status)
        //        {
        //            pictureBox24.BackgroundImage = Properties.Resources.linkerror;
        //        }
        //        else if (3 == status)
        //        {
        //            pictureBox24.BackgroundImage = Properties.Resources.linksemisucc;
        //        }
        //        else if (0 == status)
        //        {
        //            pictureBox24.BackgroundImage = Properties.Resources.linksucc;
        //        }
        //    }
        //    else if (13 == i)
        //    {
        //        if (1 == status)
        //        {
        //            pictureBox25.BackgroundImage = Properties.Resources.point;
        //        }
        //        else if (2 == status)
        //        {
        //            pictureBox25.BackgroundImage = Properties.Resources.linkerror;
        //        }
        //        else if (3 == status)
        //        {
        //            pictureBox25.BackgroundImage = Properties.Resources.linksemisucc;
        //        }
        //        else if (0 == status)
        //        {
        //            pictureBox25.BackgroundImage = Properties.Resources.linksucc;
        //        }
        //    }
        //    else if (14 == i)
        //    {
        //        if (1 == status)
        //        {
        //            pictureBox26.BackgroundImage = Properties.Resources.point;
        //        }
        //        else if (2 == status)
        //        {
        //            pictureBox26.BackgroundImage = Properties.Resources.linkerror;
        //        }
        //        else if (3 == status)
        //        {
        //            pictureBox26.BackgroundImage = Properties.Resources.linksemisucc;
        //        }
        //        else if (0 == status)
        //        {
        //            pictureBox26.BackgroundImage = Properties.Resources.linksucc;
        //        }
        //    }
        //    else if (15 == i)
        //    {
        //        if (1 == status)
        //        {
        //            pictureBox27.BackgroundImage = Properties.Resources.point;
        //        }
        //        else if (2 == status)
        //        {
        //            pictureBox27.BackgroundImage = Properties.Resources.linkerror;
        //        }
        //        else if (3 == status)
        //        {
        //            pictureBox27.BackgroundImage = Properties.Resources.linksemisucc;
        //        }
        //        else if (0 == status)
        //        {
        //            pictureBox27.BackgroundImage = Properties.Resources.linksucc;
        //        }
        //    }
        //    else if (16 == i)
        //    {
        //        if (1 == status)
        //        {
        //            pictureBox40.BackgroundImage = Properties.Resources.point;
        //        }
        //        else if (2 == status)
        //        {
        //            pictureBox40.BackgroundImage = Properties.Resources.linkerror;
        //        }
        //        else if (3 == status)
        //        {
        //            pictureBox40.BackgroundImage = Properties.Resources.linksemisucc;
        //        }
        //        else if (0 == status)
        //        {
        //            pictureBox40.BackgroundImage = Properties.Resources.linksucc;
        //        }
        //    }
        //}

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            //change image of point 
            if (0 == flag)
            {
               // pictureBox6.BackgroundImage = Image.FromFile("../../image/linkerror.png");
                //pictureBox7.BackgroundImage = Image.FromFile("../../image/linkerror.png");
                //pictureBox8.BackgroundImage = Image.FromFile("../../image/linkerror.png");
                //pictureBox14.BackgroundImage = Image.FromFile("../../image/linkerror.png");
                //pictureBox15.BackgroundImage = Image.FromFile("../../image/linkerror.png");
                flag++;
            }
            else
            {
                //pictureBox6.BackgroundImage = Image.FromFile("../../image/linksucc.png");
                //pictureBox7.BackgroundImage = Image.FromFile("../../image/linksucc.png");
                //pictureBox8.BackgroundImage = Image.FromFile("../../image/linksucc.png");
                //pictureBox14.BackgroundImage = Image.FromFile("../../image/linksucc.png");
                //pictureBox15.BackgroundImage = Image.FromFile("../../image/linksucc.png");
                flag = 0;
            }

        }

        private void tabControl1_DrawItem(object sender, DrawItemEventArgs e)
        {
            SolidBrush yellow = new SolidBrush(Color.Yellow);

            //Rectangle rect[4];
            //Rectangle rect1 = tabControl1.GetTabRect(0);
            e.Graphics.FillRectangle(yellow, tabControl1.GetTabRect(0));
            e.Graphics.FillRectangle(yellow, tabControl1.GetTabRect(1));
            e.Graphics.FillRectangle(yellow, tabControl1.GetTabRect(2));
            e.Graphics.FillRectangle(yellow, tabControl1.GetTabRect(3));
        }

        private void listView1_DrawColumnHeader(object sender, DrawListViewColumnHeaderEventArgs e)
        {
            //int tColumnCount;
            //Rectangle tRect = new Rectangle();
            //Point tPoint = new Point();
            //Font tFont = new Font("宋体", 9, FontStyle.Regular);
            //SolidBrush tBackBrush = new SolidBrush(System.Drawing.Color.FromArgb(((int)(((byte)(56)))), ((int)(((byte)(56)))), ((int)(((byte)(56))))));
            //SolidBrush tFtontBrush;
            //tFtontBrush = new SolidBrush(System.Drawing.SystemColors.GradientActiveCaption);

            //if (listView1.Columns.Count == 0)
            //    return;

            //tColumnCount = listView1.Columns.Count;
            //tRect.Y = 0;
            //tRect.Height = e.Bounds.Height - 1;
            //tPoint.Y = 3;
            //for (int i = 0; i < tColumnCount; i++)
            //{
            //    if (i == 0)
            //    {
            //        tRect.X = 0;
            //        tRect.Width = listView1.Columns[i].Width;
            //    }
            //    else
            //    {
            //        tRect.X += tRect.Width;
            //        tRect.X += 1;
            //        tRect.Width = listView1.Columns[i].Width - 1;
            //    }
            //    e.Graphics.FillRectangle(tBackBrush, tRect);
            //    tPoint.X = tRect.X + 3;
            //    e.Graphics.DrawString(listView1.Columns[i].Text, tFont, tFtontBrush, tPoint);
            //}

        }


        private void SetScrollBarPos()
        {
            //panel8.Width -= 20;
            customScrollbar1.Location = new Point(glacialList1.ClientSize.Width-15,25);

            customScrollbar1.Height = glacialList1.ClientSize.Height;

            Point pt;
            pt = new Point(this.glacialList1.Location.X, this.glacialList1.Location.Y);
            this.customScrollbar1.Minimum = 0;
            this.customScrollbar1.Maximum = 340;////this.glacialList1.ClientSize.Height/20;//panel的最大高度
            this.customScrollbar1.LargeChange = customScrollbar1.Maximum / customScrollbar1.Height + this.glacialList1.ClientSize.Height/20;
            this.customScrollbar1.SmallChange = 1;
            this.customScrollbar1.Height -= 20;            
            this.customScrollbar1.Value = Math.Abs(this.glacialList1.Location.Y);

        }
        private void glacialList1_Resize(object sender, EventArgs e)
        {
       
        }

        private void panel2_Resize(object sender, EventArgs e)
        {
            if (glacialList1.ClientSize.Height <= 362)   //17*20 + 22 + 
            {
                customScrollbar1.Visible = false;
                SetScrollBarPos();  //scrollbar 的样式
            }
            else
            {
                customScrollbar1.Visible = false;
            }
        }

        private void customScrollbar1_Load(object sender, EventArgs e)
        {

        }

        private void customScrollbar1_Scroll(object sender, EventArgs e)
        {
            //panel8.AutoScrollPosition = new Point(0, customScrollbar1.Value);
            //panel8.VerticalScroll.Value = customScrollbar1.Value;
            //this.glacialList1.ClientRectangle.Location = new Point(0, customScrollbar1.Value);
//             if (this.glacialList1.Columns.Width > this.glacialList1.RowsInnerClientRect.Width)
//             {
//                 //水平滚动条出现
// 
//             }
// 
//             if (this.glacialList1.ItemHeight*17 > this.glacialList1.RowsInnerClientRect.Height)
//             {
//                 MessageBox.Show("垂直滚动条出现了");
//             }

            
            //customScrollbar1.Value+=1;
            //glacialList1.Location.Y += customScrollbar1.Value*20;

            //this.customScrollbar1.VerticalScroll.Value = customScrollbar1.Value;
            //glacialList1.IsItemVisible();
//             for (int i=0;i<17;i++)
//             {
//                 if (glacialList1.IsItemVisible(glacialList1.Items[i]))
//                 {
//                     MessageBox.Show(i.ToString() + "看不到");
//                 }
//             }
            MessageBox.Show(customScrollbar1.Value.ToString());
            customScrollbar1.Invalidate();
            Application.DoEvents();
        }

        //private void listView1_DrawItem(object sender, DrawListViewItemEventArgs e)
        //{
        //    //e.Graphics.
        //}

        //private void listView1_DrawSubItem(object sender, DrawListViewSubItemEventArgs e)
        //{

        //}

        //private void listView1_DrawColumnHeader_1(object sender, DrawListViewColumnHeaderEventArgs e)
        //{
        //    int tColumnCount;
        //    Rectangle tRect = new Rectangle();
        //    Point tPoint = new Point();
        //    Font tFont = new Font("宋体", 9, FontStyle.Regular);
        //    SolidBrush tBackBrush = new SolidBrush(System.Drawing.Color.FromArgb(((int)(((byte)(156)))), ((int)(((byte)(56)))), ((int)(((byte)(56))))));
        //    SolidBrush tFtontBrush;
        //    tFtontBrush = new SolidBrush(System.Drawing.SystemColors.GradientActiveCaption);

        //    if (listView1.Columns.Count == 0)
        //        return;

        //    tColumnCount = listView1.Columns.Count;
        //    tRect.Y = 0;
        //    tRect.Height = e.Bounds.Height - 1;
        //    tPoint.Y = 3;

        //    for (int i = 0; i < tColumnCount; i++)
        //    {
        //        //if (i == 0)
        //        //{
        //        //    tRect.X = 0;
        //        //    tRect.Width = listView1.Columns[i].Width;
        //        //}
        //        //else
        //        //{
        //        //    tRect.X += tRect.Width;
        //        //    tRect.X += 1;
        //        //    tRect.Width = listView1.Columns[i].Width - 1;

 
        //        //}
        //        //e.Graphics.FillRectangle(tBackBrush, tRect);


        //        if (true)
        //        {
        //            // tRect.X += tRect.Width;
        //            tRect.X += 1;
        //            tRect.Width = 500;// listView1.Size.Width + 100;

        //            e.Graphics.FillRectangle(tBackBrush, tRect);
        //        }

        //        tPoint.X = tRect.X + 3;
        //        e.Graphics.DrawString(listView1.Columns[i].Text, tFont, tFtontBrush, tPoint);
        //    }

        //    //最后一列的后面部分

        //    //tRect.X += tRect.Width;

        //    //listView1.Width
                



        //}

        //private void listView1_DrawItem_1(object sender, DrawListViewItemEventArgs e)
        //{
        //    e.DrawText();
        //}
    }
}
