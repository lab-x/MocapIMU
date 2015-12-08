
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
    public partial class BodyInforForm : UserControl
    {
        ToolTip m_ToolTip = new ToolTip();
        

        public LinkInfoForm m_Linkinfoform;
        public BodyInforForm()
        {
            InitializeComponent();

            m_Linkinfoform = new LinkInfoForm();
            m_Linkinfoform.Visible = true;
            m_Linkinfoform.Dock = DockStyle.Fill;
            this.splitContainer1.Panel2.Controls.Add(m_Linkinfoform);

            m_ToolTip.BackColor = Color.Blue;
            m_ToolTip.OwnerDraw = true;
            m_ToolTip.Draw += new DrawToolTipEventHandler(m_ToolTip_Draw);

        }

        void m_ToolTip_Draw(object sender, DrawToolTipEventArgs e)
        {            
            Graphics g = e.Graphics;
            g.FillRectangle(new SolidBrush(this.m_ToolTip.BackColor), g.ClipBounds);
            //e.Graphics.DrawLines(SystemPens.ControlLightLight, new Point[] { new Point(0, e.Bounds.Height - 1), new Point(0, 0), new Point(e.Bounds.Width - 1, 0) });
            //e.Graphics.DrawLines(SystemPens.ControlDarkDark, new Point[] { new Point(0, e.Bounds.Height - 1), new Point(e.Bounds.Width - 1, e.Bounds.Height - 1), new Point(e.Bounds.Width - 1, 0) });
            TextFormatFlags sf = TextFormatFlags.VerticalCenter | TextFormatFlags.NoFullWidthCharacterBreak;
            e.DrawText(sf);

        }
            
        private void defineBodyinfoform_Paint(object sender, PaintEventArgs e)
        {
            //from msdn ysj++
            //System.Drawing.Drawing2D.GraphicsPath buttonPath =
            //new System.Drawing.Drawing2D.GraphicsPath();

            //// Set a new rectangle to the same size as the button's 
            //// ClientRectangle property.
            //System.Drawing.Rectangle newRectangle = button2.ClientRectangle;

            //// Decrease the size of the rectangle.
            //newRectangle.Inflate(-5, -5);

            //// Draw the button's border.
            //e.Graphics.DrawEllipse(System.Drawing.Pens.Black, newRectangle);

            //// Increase the size of the rectangle to include the border.
            //newRectangle.Inflate(10, 10);

            //// Create a circle within the new rectangle.
            //buttonPath.AddEllipse(newRectangle);

            //// Set the button's Region property to the newly created 
            //// circle region.
            //button2.Region = new System.Drawing.Region(buttonPath);

            //button2.Image = Properties.Resources.red;
        }
        
        public void RefreshButtonImage()
        {
            for (int i = 0; i < 17; i++)
            {
                if (0 <= ArrayData.ration[i] && ArrayData.ration[i] < 20)
                {
                    ChangeButtonImage(i, 0);
                }
                else if (20 <= ArrayData.ration[i] && ArrayData.ration[i] < 70)
                {
                    ChangeButtonImage(i, 1);
                }
                else if (70 <= ArrayData.ration[i] && ArrayData.ration[i] < 90)
                {
                    ChangeButtonImage(i, 2);
                }
                else if (90 <= ArrayData.ration[i])
                {
                    ChangeButtonImage(i, 3);
                }
            }
        }

        public void ChangeButtonImage(int id, int status)
        {
            //此处的i==0时，可能对应的是第16个节点（右脚）,所以对应关系需要在这里找一下
            //defineSerialPort.FindIndexForOther();
            int imageindex = Global.IDSort[id];//拖动后，ID对应的几点关系。   i是15，就是btn16 对应的索引，index是 实际图片上的id-1. status 是状态
 

            if (0 == id)
            {
                checkimage(button1,imageindex, status);                                
            }else if (1 == id)
            {
                checkimage(button2, imageindex, status);
            }
            else if (2 == id)
            {
                checkimage(button3, imageindex, status);
            }
            else if (3 == id)
            {
                checkimage(button4, imageindex, status);
            }
            else if (4 == id)
            {
                checkimage(button5, imageindex, status);
            }
            else if (5 == id)
            {
                checkimage(button6, imageindex, status);
            }
            else if (6 == id)
            {
                checkimage(button7, imageindex, status);
            }
            else if (7 == id)
            {
                checkimage(button8, imageindex, status);
            }
            else if (8 == id)
            {
                checkimage(button9, imageindex, status);
            }
            else if (9 == id)
            {
                checkimage(button10, imageindex, status);
            }
            else if (10 == id)
            {
                checkimage(button11, imageindex, status);
            }
            else if (11 == id)
            {
                checkimage(button12, imageindex, status);
            }
            else if (12 == id)
            {
                checkimage(button13, imageindex, status);
            }
            else if (13 == id)
            {
                checkimage(button14, imageindex, status);
            }
            else if (14 == id)
            {
                checkimage(button15, imageindex, status);
            }
            else if (15 == id)
            {
                checkimage(button16, imageindex, status);
            }
            else if (16 == id)
            {
                checkimage(button17, imageindex, status);
            }
        }

        private void checkimage(object btn,int imageindex, int status)
        {
            if (1 == imageindex)
            {
                if (1 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.red01;
                }
                else if (2 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.yellow01;
                }
                else if (3 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.green01;
                }
                else if (0 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.blue01;
                }
            }
            else if (2 == imageindex)
            {
                if (1 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.red02;
                }
                else if (2 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.yellow02;
                }
                else if (3 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.green02;
                }
                else if (0 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.blue02;
                }
            }
            else if (3 == imageindex)
            {
                if (1 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.red03;
                }
                else if (2 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.yellow03;
                }
                else if (3 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.green03;
                }
                else if (0 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.blue03;
                }
            }
            else if (4 == imageindex)
            {
                if (1 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.red04;
                }
                else if (2 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.yellow04;
                }
                else if (3 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.green04;
                }
                else if (0 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.blue04;
                }
            }
            else if (5 == imageindex)
            {
                if (1 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.red05;
                }
                else if (2 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.yellow05;
                }
                else if (3 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.green05;
                }
                else if (0 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.blue05;
                }
            }
            else if (6 == imageindex)
            {
                if (1 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.red06;
                }
                else if (2 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.yellow06;
                }
                else if (3 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.green06;
                }
                else if (0 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.blue06;
                }
            }
            else if (7 == imageindex)
            {
                if (1 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.red07;
                }
                else if (2 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.yellow07;
                }
                else if (3 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.green07;
                }
                else if (0 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.blue07;
                }
            }
            else if (8 == imageindex)
            {
                if (1 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.red08;
                }
                else if (2 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.yellow08;
                }
                else if (3 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.green08;
                }
                else if (0 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.blue08;
                }
            }
            else if (9 == imageindex)
            {
                if (1 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.red09;
                }
                else if (2 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.yellow09;
                }
                else if (3 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.green09;
                }
                else if (0 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.blue09;
                }
            }
            else if (10 == imageindex)
            {
                if (1 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.red10;
                }
                else if (2 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.yellow10;
                }
                else if (3 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.green10;
                }
                else if (0 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.blue10;
                }
            }
            else if (11 == imageindex)
            {
                if (1 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.red11;
                }
                else if (2 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.yellow11;
                }
                else if (3 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.green11;
                }
                else if (0 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.blue11;
                }
            }
            else if (12 == imageindex)
            {
                if (1 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.red12;
                }
                else if (2 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.yellow12;
                }
                else if (3 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.green12;
                }
                else if (0 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.blue12;
                }
            }
            else if (13 == imageindex)
            {
                if (1 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.red13;
                }
                else if (2 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.yellow13;
                }
                else if (3 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.green13;
                }
                else if (0 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.blue13;
                }
            }
            else if (14 == imageindex)
            {
                if (1 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.red14;
                }
                else if (2 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.yellow14;
                }
                else if (3 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.green14;
                }
                else if (0 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.blue14;
                }
            }
            else if (15 == imageindex)
            {
                if (1 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.red15;
                }
                else if (2 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.yellow15;
                }
                else if (3 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.green15;
                }
                else if (0 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.blue15;
                }
            }
            else if (16 == imageindex)
            {
                if (1 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.red16;
                }
                else if (2 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.yellow16;
                }
                else if (3 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.green16;
                }
                else if (0 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.blue16;
                }
            }
            else if (17 == imageindex)
            {
                if (1 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.red17;
                }
                else if (2 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.yellow17;
                }
                else if (3 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.green17;
                }
                else if (0 == status)
                {
                    (btn as Button).BackgroundImage = Properties.Resources.blue17;
                }
            }
        }
#region Mouse Hover event

        private void button1_MouseHover(object sender, EventArgs e)
        {
            int realIDIndex = defineSerialPort.FindIndex(1);
            
            string tiptext = "ID:" + (realIDIndex+1).ToString() + "\r\n" + "-------------\r\nRatio:" + (ArrayData.ration[realIDIndex]).ToString() + "%";
            m_ToolTip.SetToolTip((Button)sender, tiptext);
        }

        private void button2_MouseHover(object sender, EventArgs e)
        {
            int realIDIndex = defineSerialPort.FindIndex(2);

            string tiptext = "ID:" + (realIDIndex + 1).ToString() + "\r\n" + "-------------\r\nRatio:" + (ArrayData.ration[realIDIndex]).ToString() + "%";
            m_ToolTip.SetToolTip((Button)sender, tiptext);
        }

        private void button3_MouseHover(object sender, EventArgs e)
        {
            int realIDIndex = defineSerialPort.FindIndex(3);

            string tiptext = "ID:" + (realIDIndex + 1).ToString() + "\r\n" + "-------------\r\nRatio:" + (ArrayData.ration[realIDIndex]).ToString() + "%";
            m_ToolTip.SetToolTip((Button)sender, tiptext);
        }

        private void button4_MouseHover(object sender, EventArgs e)
        {
            int realIDIndex = defineSerialPort.FindIndex(4);
            string tiptext = "ID:" + (realIDIndex + 1).ToString() + "\r\n" + "-------------\r\nRatio:" + (ArrayData.ration[realIDIndex]).ToString() + "%";
            m_ToolTip.SetToolTip((Button)sender, tiptext);
        }

        private void button5_MouseHover(object sender, EventArgs e)
        {
            int realIDIndex = defineSerialPort.FindIndex(5);
            string tiptext = "ID:" + (realIDIndex + 1).ToString() + "\r\n" + "-------------\r\nRatio:" + (ArrayData.ration[realIDIndex]).ToString() + "%";
            m_ToolTip.SetToolTip((Button)sender, tiptext);
        }

        private void button6_MouseHover(object sender, EventArgs e)
        {
            int realIDIndex = defineSerialPort.FindIndex(6);
            string tiptext = "ID:" + (realIDIndex + 1).ToString() + "\r\n" + "-------------\r\nRatio:" + (ArrayData.ration[realIDIndex]).ToString() + "%";
            m_ToolTip.SetToolTip((Button)sender, tiptext);
        }

        private void button7_MouseHover(object sender, EventArgs e)
        {
            int realIDIndex = defineSerialPort.FindIndex(7);
            string tiptext = "ID:" + (realIDIndex + 1).ToString() + "\r\n" + "-------------\r\nRatio:" + (ArrayData.ration[realIDIndex]).ToString() + "%";
            m_ToolTip.SetToolTip((Button)sender, tiptext);
        }

        private void button8_MouseHover(object sender, EventArgs e)
        {
            int realIDIndex = defineSerialPort.FindIndex(8);
            string tiptext = "ID:" + (realIDIndex + 1).ToString() + "\r\n" + "-------------\r\nRatio:" + (ArrayData.ration[realIDIndex]).ToString() + "%";
            m_ToolTip.SetToolTip((Button)sender, tiptext);
        }

        private void button9_MouseHover(object sender, EventArgs e)
        {
            int realIDIndex = defineSerialPort.FindIndex(9);
            string tiptext = "ID:" + (realIDIndex + 1).ToString() + "\r\n" + "-------------\r\nRatio:" + (ArrayData.ration[realIDIndex]).ToString() + "%";
            m_ToolTip.SetToolTip((Button)sender, tiptext);
        }

        private void button10_MouseHover(object sender, EventArgs e)
        {
            int realIDIndex = defineSerialPort.FindIndex(10);
            string tiptext = "ID:" + (realIDIndex + 1).ToString() + "\r\n" + "-------------\r\nRatio:" + (ArrayData.ration[realIDIndex]).ToString() + "%";
            m_ToolTip.SetToolTip((Button)sender, tiptext);
        }

        private void button11_MouseHover(object sender, EventArgs e)
        {
            int realIDIndex = defineSerialPort.FindIndex(11);
            string tiptext = "ID:" + (realIDIndex + 1).ToString() + "\r\n" + "-------------\r\nRatio:" + (ArrayData.ration[realIDIndex]).ToString() + "%";
            m_ToolTip.SetToolTip((Button)sender, tiptext);
        }

        private void button12_MouseHover(object sender, EventArgs e)
        {
            int realIDIndex = defineSerialPort.FindIndex(12);
            string tiptext = "ID:" + (realIDIndex + 1).ToString() + "\r\n" + "-------------\r\nRatio:" + (ArrayData.ration[realIDIndex]).ToString() + "%";
            m_ToolTip.SetToolTip((Button)sender, tiptext);
        }

        private void button13_MouseHover(object sender, EventArgs e)
        {
            int realIDIndex = defineSerialPort.FindIndex(13);
            string tiptext = "ID:" + (realIDIndex + 1).ToString() + "\r\n" + "-------------\r\nRatio:" + (ArrayData.ration[realIDIndex]).ToString() + "%";
            m_ToolTip.SetToolTip((Button)sender, tiptext);
        }

        private void button14_MouseHover(object sender, EventArgs e)
        {
            int realIDIndex = defineSerialPort.FindIndex(14);
            string tiptext = "ID:" + (realIDIndex + 1).ToString() + "\r\n" + "-------------\r\nRatio:" + (ArrayData.ration[realIDIndex]).ToString() + "%";
            m_ToolTip.SetToolTip((Button)sender, tiptext);
        }

        private void button15_MouseHover(object sender, EventArgs e)
        {
            int realIDIndex = defineSerialPort.FindIndex(15);
            string tiptext = "ID:" + (realIDIndex + 1).ToString() + "\r\n" + "-------------\r\nRatio:" + (ArrayData.ration[realIDIndex]).ToString() + "%";
            m_ToolTip.SetToolTip((Button)sender, tiptext);
        }

        private void button16_MouseHover(object sender, EventArgs e)
        {
            int realIDIndex = defineSerialPort.FindIndex(16);
            string tiptext = "ID:" + (realIDIndex + 1).ToString() + "\r\n" + "-------------\r\nRatio:" + (ArrayData.ration[realIDIndex]).ToString() + "%";
            m_ToolTip.SetToolTip((Button)sender, tiptext);
        }

        private void button17_MouseHover(object sender, EventArgs e)
        {
            int realIDIndex = defineSerialPort.FindIndex(17);
            string tiptext = "ID:" + (realIDIndex + 1).ToString() + "\r\n" + "-------------\r\nRatio:" + (ArrayData.ration[realIDIndex]).ToString() + "%";
            m_ToolTip.SetToolTip((Button)sender, tiptext);
        }
#endregion

        private void panel3_SizeChanged_1(object sender, EventArgs e)
        {
            #region  get x y 的较小者，

            int xlen = this.panel3.Width;
            int ylen = this.panel3.Height;

            int xpos = this.Location.X;
            int ypos = this.Location.Y;

            int minlen = xlen < ylen ? xlen : ylen;

            int backxpos = (this.panel3.Width - minlen) / 2;
            int backypos = (this.panel3.Height - minlen) / 2;

            //button2
            button1.Height = (int)(0.04 * minlen);
            button1.Width = (int)(0.08 * minlen);

            Point _pt2 = new Point();
            _pt2.X = (int)((this.panel3.Width - minlen) / 2 + 0.09 * minlen);
            _pt2.Y = (int)((this.panel3.Height - minlen) / 2 + 0.21 * minlen);
            button1.Location = _pt2;

            //button3
            button2.Height = (int)(0.04 * minlen);
            button2.Width = (int)(0.08 * minlen);
            Point _pt3 = new Point();
            _pt3.X = (int)((this.panel3.Width - minlen) / 2 + 0.19 * minlen);
            _pt3.Y = (int)((this.panel3.Height - minlen) / 2 + 0.21 * minlen);
            button2.Location = _pt3;

            //button4
            button3.Height = (int)(0.04 * minlen);
            button3.Width = (int)(0.08 * minlen);
            Point _pt4 = new Point();
            _pt4.X = (int)((this.panel3.Width - minlen) / 2 + 0.29 * minlen);
            _pt4.Y = (int)((this.panel3.Height - minlen) / 2 + 0.21 * minlen);
            button3.Location = _pt4;

            //button5
            button4.Height = (int)(0.04 * minlen);
            button4.Width = (int)(0.08 * minlen);
            Point _pt5 = new Point();
            _pt5.X = (int)((this.panel3.Width - minlen) / 2 + 0.39 * minlen);
            _pt5.Y = (int)((this.panel3.Height - minlen) / 2 + 0.23 * minlen);
            button4.Location = _pt5;

            //button6
            button5.Height = (int)(0.04 * minlen);
            button5.Width = (int)(0.08 * minlen);
            Point _pt6 = new Point();
            _pt6.X = (int)((this.panel3.Width - minlen) / 2 + 0.53 * minlen);
            _pt6.Y = (int)((this.panel3.Height - minlen) / 2 + 0.23 * minlen);
            button5.Location = _pt6;

            //button7
            button6.Height = (int)(0.04 * minlen);
            button6.Width = (int)(0.08 * minlen);
            Point _pt7 = new Point();

            _pt7.X = (int)((this.panel3.Width - minlen) / 2 + 0.63 * minlen);
            _pt7.Y = (int)((this.panel3.Height - minlen) / 2 + 0.21 * minlen);
            button6.Location = _pt7;

            //button8
            button7.Height = (int)(0.04 * minlen);
            button7.Width = (int)(0.08 * minlen);
            Point _pt8 = new Point();
            _pt8.X = (int)((this.panel3.Width - minlen) / 2 + 0.73 * minlen);
            _pt8.Y = (int)((this.panel3.Height - minlen) / 2 + 0.21 * minlen);
            button7.Location = _pt8;

            //button9
            button8.Height = (int)(0.04 * minlen);
            button8.Width = (int)(0.08 * minlen);
            Point _pt9 = new Point();
            _pt9.X = (int)((this.panel3.Width - minlen) / 2 + 0.83 * minlen);
            _pt9.Y = (int)((this.panel3.Height - minlen) / 2 + 0.21 * minlen);
            button8.Location = _pt9;

            //button10
            button9.Height = (int)(0.04 * minlen);
            button9.Width = (int)(0.08 * minlen);
            Point _pt10 = new Point();
            _pt10.X = (int)((this.panel3.Width - minlen) / 2 + 0.45 * minlen);
            _pt10.Y = (int)((this.panel3.Height - minlen) / 2 + 0.14 * minlen);
            button9.Location = _pt10;

            //button11
            button10.Height = (int)(0.04 * minlen);
            button10.Width = (int)(0.08 * minlen);
            Point _pt11 = new Point();
            _pt11.X = (int)((this.panel3.Width - minlen) / 2 + 0.46 * minlen);
            _pt11.Y = (int)((this.panel3.Height - minlen) / 2 + 0.3 * minlen);
            button10.Location = _pt11;

            //button12
            button11.Height = (int)(0.04 * minlen);
            button11.Width = (int)(0.08 * minlen);
            Point _pt12 = new Point();
            _pt12.X = (int)((this.panel3.Width - minlen) / 2 + 0.46 * minlen);
            _pt12.Y = (int)((this.panel3.Height - minlen) / 2 + 0.45 * minlen);
            button11.Location = _pt12;

            //button13
            button12.Height = (int)(0.04 * minlen);
            button12.Width = (int)(0.08 * minlen);
            Point _pt13 = new Point();
            _pt13.X = (int)((this.panel3.Width - minlen) / 2 + 0.41 * minlen);
            _pt13.Y = (int)((this.panel3.Height - minlen) / 2 + 0.57 * minlen);
            button12.Location = _pt13;
            /***/
            //button16
            button15.Height = (int)(0.04 * minlen);
            button15.Width = (int)(0.08 * minlen);
            Point _pt14 = new Point();
            _pt14.X = (int)((this.panel3.Width - minlen) / 2 + 0.50 * minlen);
            _pt14.Y = (int)((this.panel3.Height - minlen) / 2 + 0.57 * minlen);
            button15.Location = _pt14;

            //button14
            button13.Height = (int)(0.04 * minlen);
            button13.Width = (int)(0.08 * minlen);
            Point _pt15 = new Point();
            _pt15.X = (int)((this.panel3.Width - minlen) / 2 + 0.41 * minlen);
            _pt15.Y = (int)((this.panel3.Height - minlen) / 2 + 0.75 * minlen);
            button13.Location = _pt15;

            //button17
            button16.Height = (int)(0.04 * minlen);
            button16.Width = (int)(0.08 * minlen);
            Point _pt16 = new Point();
            _pt16.X = (int)((this.panel3.Width - minlen) / 2 + 0.50 * minlen);
            _pt16.Y = (int)((this.panel3.Height - minlen) / 2 + 0.75 * minlen);
            button16.Location = _pt16;

            //button15
            button14.Height = (int)(0.04 * minlen);
            button14.Width = (int)(0.08 * minlen);
            Point _pt17 = new Point();
            _pt17.X = (int)((this.panel3.Width - minlen) / 2 + 0.40 * minlen);
            _pt17.Y = (int)((this.panel3.Height - minlen) / 2 + 0.89 * minlen);
            button14.Location = _pt17;

            //button18
            button17.Height = (int)(0.04 * minlen);
            button17.Width = (int)(0.08 * minlen);
            Point _pt18 = new Point();
            _pt18.X = (int)((this.panel3.Width - minlen) / 2 + 0.51 * minlen);
            _pt18.Y = (int)((this.panel3.Height - minlen) / 2 + 0.89 * minlen);
            button17.Location = _pt18;
            #endregion
        }
          
        private bool[] btnDragingFlagArr = new bool[] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };

        private void button1_DragDrop(object sender, DragEventArgs e)
        {            
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                SwapBtnBackGround(button1); //改变了与buttong1 交互的按钮的  背景

                (sender as Button).BackgroundImage = e.Data.GetData(DataFormats.Bitmap) as Bitmap;//改变了button1的背景

                //背景交换完成后，更改数组的排序
                ReSortID(0);                
                
            }           

            ResetFlag();
        }

        private void ReSortID(int DescIDIndex)
        {
            for (int i=0;i<17;i++)
                {
                    if (btnDragingFlagArr[i])
                    {
                        //说明i是被拖动的按钮的索引值
                        int tmp = Global.IDSort[i];
                        Global.IDSort[i] = Global.IDSort[DescIDIndex];
                        Global.IDSort[DescIDIndex] = tmp;
                    }
                }
        }

        private void button1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            { e.Effect = DragDropEffects.All; }
        }

        private void button1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
//                oripoint = e.Location;
                ResetFlag();
                //isbtn1Draging = true;
                btnDragingFlagArr[0] = true;
                button1.DoDragDrop(button1.BackgroundImage, DragDropEffects.Move);                
            }
        }
#region 废弃的代码 暂时保留 画矩形

        //Rectangle rect = new Rectangle();
        //private void button1_MouseMove(object sender, MouseEventArgs e)
        //{
        //    Graphics g = panel3.CreateGraphics();
        //    //rect = new Rectangle(e.Location.X, e.Location.Y, 50, 50);
        //    rect.Location = e.Location;
        //    rect.Size = new System.Drawing.Size(50, 50);
        //    SolidBrush b1 = new SolidBrush(Color.Red);//定义单色画刷          
        //    g.FillRectangle(b1, rect);//填充这个矩形

        //}
#endregion

        private void SwapBtnBackGround(object sender)
        {
            if (btnDragingFlagArr[0])
            {
                button1.BackgroundImage = (sender as Button).BackgroundImage;
            }
            if (btnDragingFlagArr[1])
            {
                button2.BackgroundImage = (sender as Button).BackgroundImage;
            }
            if (btnDragingFlagArr[2])
            {
                button3.BackgroundImage = (sender as Button).BackgroundImage;
            }
            if (btnDragingFlagArr[3])
            {
                button4.BackgroundImage = (sender as Button).BackgroundImage;
            }
            if (btnDragingFlagArr[4])
            {
                button5.BackgroundImage = (sender as Button).BackgroundImage;
            }
            if (btnDragingFlagArr[5])
            {
                button6.BackgroundImage = (sender as Button).BackgroundImage;
            }
            if (btnDragingFlagArr[6])
            {
                button7.BackgroundImage = (sender as Button).BackgroundImage;
            }
            if (btnDragingFlagArr[7])
            {
                button8.BackgroundImage = (sender as Button).BackgroundImage;
            }
            if (btnDragingFlagArr[8])
            {
                button9.BackgroundImage = (sender as Button).BackgroundImage;
            }
            if (btnDragingFlagArr[9])
            {
                button10.BackgroundImage = (sender as Button).BackgroundImage;
            }
            if (btnDragingFlagArr[10])
            {
                button11.BackgroundImage = (sender as Button).BackgroundImage;
            }
            if (btnDragingFlagArr[11])
            {
                button12.BackgroundImage = (sender as Button).BackgroundImage;
            }
            if (btnDragingFlagArr[12])
            {
                button13.BackgroundImage = (sender as Button).BackgroundImage;
            }
            if (btnDragingFlagArr[13])
            {
                button14.BackgroundImage = (sender as Button).BackgroundImage;
            }
            if (btnDragingFlagArr[14])
            {
                button15.BackgroundImage = (sender as Button).BackgroundImage;
            }
            if (btnDragingFlagArr[15])
            {
                button16.BackgroundImage = (sender as Button).BackgroundImage;
            }
            if (btnDragingFlagArr[16])
            {
                button17.BackgroundImage = (sender as Button).BackgroundImage;
            }
   
        }

        private void button2_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                //1.首先替换被拖动的btn的背景，然后才替换目标btn的背景

                SwapBtnBackGround(button2);

                //2.更换目的地btn图片
                (sender as Button).BackgroundImage = e.Data.GetData(DataFormats.Bitmap) as Bitmap;

                ReSortID(1);
            }
            ResetFlag();
        }

        private void button2_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                e.Effect = DragDropEffects.All;
            }
        }

        private void button2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ResetFlag();
                btnDragingFlagArr[1] = true;
                button2.DoDragDrop(button2.BackgroundImage, DragDropEffects.Move);
            }
        }

        private void closebtn_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void panel3_MouseUp(object sender, MouseEventArgs e)
        {
            ResetFlag();
        }

        private void button3_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ResetFlag();
                btnDragingFlagArr[2] = true;
                button3.DoDragDrop(button3.BackgroundImage, DragDropEffects.Move);
            }
        }

        private void button3_DragDrop(object sender, DragEventArgs e)
        {           

            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                SwapBtnBackGround(button3);
                (sender as Button).BackgroundImage = e.Data.GetData(DataFormats.Bitmap) as Bitmap;
                ReSortID(2);
            }
            ResetFlag();
        }

        private void button3_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                e.Effect = DragDropEffects.All;
            }
        }

        private void ResetFlag()
        {
            for (int i = 0; i < 17;i++ )
            {
                btnDragingFlagArr[i] = false;
            }
        }

        private void button4_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ResetFlag();
                btnDragingFlagArr[3] = true;
                button4.DoDragDrop(button4.BackgroundImage, DragDropEffects.Move);
            }
        }

        private void button4_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                e.Effect = DragDropEffects.All;
            }
        }

        private void button4_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                SwapBtnBackGround(button4);
                (sender as Button).BackgroundImage = e.Data.GetData(DataFormats.Bitmap) as Bitmap;
                ReSortID(3);
            }
            ResetFlag();
        }

        private void button5_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ResetFlag();
                btnDragingFlagArr[4] = true;
                button5.DoDragDrop(button5.BackgroundImage, DragDropEffects.Move);
            }
        }

        private void button5_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                SwapBtnBackGround(button5);
                (sender as Button).BackgroundImage = e.Data.GetData(DataFormats.Bitmap) as Bitmap;
                ReSortID(4);
            }
            ResetFlag();
        }

        private void button5_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                e.Effect = DragDropEffects.All;
            }
        }

        private void button6_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                SwapBtnBackGround(button6);
                (sender as Button).BackgroundImage = e.Data.GetData(DataFormats.Bitmap) as Bitmap;
                ReSortID(5);
            }
            ResetFlag();
        }

        private void button6_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                e.Effect = DragDropEffects.All;
            }
        }

        private void button6_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ResetFlag();
                btnDragingFlagArr[5] = true;
                button6.DoDragDrop(button6.BackgroundImage, DragDropEffects.Move);
            }
        }

        private void button7_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                SwapBtnBackGround(button7);
                (sender as Button).BackgroundImage = e.Data.GetData(DataFormats.Bitmap) as Bitmap;
                ReSortID(6);
            }
            ResetFlag();
        }

        private void button7_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                e.Effect = DragDropEffects.All;
            }
        }

        private void button7_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ResetFlag();
                btnDragingFlagArr[6] = true;
                button7.DoDragDrop(button7.BackgroundImage, DragDropEffects.Move);
            }
        }

        private void button8_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                SwapBtnBackGround(button8);
                (sender as Button).BackgroundImage = e.Data.GetData(DataFormats.Bitmap) as Bitmap;
                ReSortID(7);
            }
            ResetFlag();
        }

        private void button8_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                e.Effect = DragDropEffects.All;
            }
        }

        private void button8_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ResetFlag();
                btnDragingFlagArr[7] = true;
                button8.DoDragDrop(button8.BackgroundImage, DragDropEffects.Move);
            }
        }

        private void button9_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                SwapBtnBackGround(button9);
                (sender as Button).BackgroundImage = e.Data.GetData(DataFormats.Bitmap) as Bitmap;
                ReSortID(8);
            }
            ResetFlag();
        }

        private void button9_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                e.Effect = DragDropEffects.All;
            }
        }

        private void button9_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ResetFlag();
                btnDragingFlagArr[8] = true;
                button9.DoDragDrop(button9.BackgroundImage, DragDropEffects.Move);
            }
        }

        private void button10_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                SwapBtnBackGround(button10);
                (sender as Button).BackgroundImage = e.Data.GetData(DataFormats.Bitmap) as Bitmap;
                ReSortID(9);
            }
            ResetFlag();
        }

        private void button10_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                e.Effect = DragDropEffects.All;
            }
        }

        private void button10_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ResetFlag();
                btnDragingFlagArr[9] = true;
                button10.DoDragDrop(button10.BackgroundImage, DragDropEffects.Move);
            }
        }

        private void button11_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                SwapBtnBackGround(button11);
                (sender as Button).BackgroundImage = e.Data.GetData(DataFormats.Bitmap) as Bitmap;
                ReSortID(10);
            }
            ResetFlag();
        }

        private void button11_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                e.Effect = DragDropEffects.All;
            }
        }

        private void button11_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ResetFlag();
                btnDragingFlagArr[10] = true;
                button11.DoDragDrop(button11.BackgroundImage, DragDropEffects.Move);
            }
        }

        private void button12_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                SwapBtnBackGround(button12);
                (sender as Button).BackgroundImage = e.Data.GetData(DataFormats.Bitmap) as Bitmap;
                ReSortID(11);
            }
            ResetFlag();
        }

        private void button12_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                e.Effect = DragDropEffects.All;
            }
        }

        private void button12_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ResetFlag();
                btnDragingFlagArr[11] = true;
                button12.DoDragDrop(button12.BackgroundImage, DragDropEffects.Move);
            }
        }

        private void button13_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                SwapBtnBackGround(button13);
                (sender as Button).BackgroundImage = e.Data.GetData(DataFormats.Bitmap) as Bitmap;
                ReSortID(12);
            }
            ResetFlag();
        }

        private void button13_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                e.Effect = DragDropEffects.All;
            }
        }

        private void button13_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ResetFlag();
                btnDragingFlagArr[12] = true;
                button13.DoDragDrop(button13.BackgroundImage, DragDropEffects.Move);
            }
        }

        private void button14_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                SwapBtnBackGround(button14);
                (sender as Button).BackgroundImage = e.Data.GetData(DataFormats.Bitmap) as Bitmap;
                ReSortID(13);
            }
            ResetFlag();
        }

        private void button14_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                e.Effect = DragDropEffects.All;
            }
        }

        private void button14_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ResetFlag();
                btnDragingFlagArr[13] = true;
                button14.DoDragDrop(button14.BackgroundImage, DragDropEffects.Move);
            }
        }

        private void button15_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                SwapBtnBackGround(button15);
                (sender as Button).BackgroundImage = e.Data.GetData(DataFormats.Bitmap) as Bitmap;
                ReSortID(14);
            }
            ResetFlag();
        }

        private void button15_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                e.Effect = DragDropEffects.All;
            }
        }

        private void button15_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ResetFlag();
                btnDragingFlagArr[14] = true;
                button15.DoDragDrop(button15.BackgroundImage, DragDropEffects.Move);
            }
        }

        private void button16_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                SwapBtnBackGround(button16);
                (sender as Button).BackgroundImage = e.Data.GetData(DataFormats.Bitmap) as Bitmap;
                ReSortID(15);
            }
            ResetFlag();
        }

        private void button16_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                e.Effect = DragDropEffects.All;
            }
        }

        private void button16_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ResetFlag();
                btnDragingFlagArr[15] = true;
                button16.DoDragDrop(button16.BackgroundImage, DragDropEffects.Move);
            }
        }

        private void button17_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                SwapBtnBackGround(button17);
                (sender as Button).BackgroundImage = e.Data.GetData(DataFormats.Bitmap) as Bitmap;
                ReSortID(16);
            }
            ResetFlag();
        }

        private void button17_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.Bitmap))
            {
                e.Effect = DragDropEffects.All;
            }
        }

        private void button17_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ResetFlag();
                btnDragingFlagArr[16] = true;
                button17.DoDragDrop(button17.BackgroundImage, DragDropEffects.Move);
            }
        }

        private void panel3_MouseMove(object sender, MouseEventArgs e)
        {
            //
        }



    }
}
