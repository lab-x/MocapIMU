using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Threading;

namespace Gwearable
{
    public partial class PlayBackOptForm : UserControl
    {
        public bool isLoop = false;
        public bool isPlayBacking = false;

        public PlayBackOptForm()
        {
            InitializeComponent();

            //默认1X被选中
            PlaySpeedCombox.SelectedIndex = 3;
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {
            isLoop = !isLoop;

            if (isLoop)
            {
                LoopPicbox.Image = Properties.Resources.loop1;
            }
            else
            {
                LoopPicbox.Image = Properties.Resources.noloop1;
            }
            
        }

        private void button4_MouseHover(object sender, EventArgs e)
        {
            if (isPlayBacking)
            {
                PlayBtn.BackgroundImage = Properties.Resources.pause_hover;
            }
            else
            {
                PlayBtn.BackgroundImage = Properties.Resources.play_hover;
            }
        }
            
        private void button4_Click(object sender, EventArgs e)
        {

        }
                
        private void ReadBVHFile()
        {
            FileStream fs = new FileStream(Application.StartupPath + "\\MotionFiles\\" + Global.PlayBackFileName, FileMode.Open, FileAccess.Read);
            StreamReader m_streamReader = new StreamReader(fs);

            isPlayBacking = true;

            //用StreamReader类来读取文件
            m_streamReader.BaseStream.Seek(0, SeekOrigin.Begin);

            string strLine = m_streamReader.ReadLine();
            while (strLine != null)
            {
                // 这里处理每一行
                if (Global.isRemote)
                {
                    Global.m_strQueue.Enqueue(strLine); //发送到远程回放
                }                
                //m_streamReader.BaseStream.Seek();
                Global.m_strLiveQueue.Enqueue(strLine);//本地回放

                //Thread.Sleep(20);
                strLine = m_streamReader.ReadLine();
            }
            m_streamReader.Close();

            isPlayBacking = false;

            PlayBtn.BackgroundImage = Properties.Resources.play;
        }
        
        private void button4_MouseLeave(object sender, EventArgs e)
        {
            if (isPlayBacking)
            {
                PlayBtn.BackgroundImage = Properties.Resources.pause;
            }
            else
            {
                PlayBtn.BackgroundImage = Properties.Resources.play; 
            }
        }
        
        private void FirstFrameBtn_Click(object sender, EventArgs e)
        {
            Global.isFrontPlay = false;
            Global.isBackPlay = false;

            Global.frame_no = 0;
        }
        
        private void PreFrameBtn_Click(object sender, EventArgs e)
        {
            Global.isFrontPlay = false;
            Global.isBackPlay = false;

            if (Global.frame_no > 0)
            {
                Global.frame_no--;
            }
        }
        
        private void PlayBackBtn_Click(object sender, EventArgs e)
        {
            Global.isBackPlay = !Global.isBackPlay;
            Global.isFrontPlay = false;
            //Global.isBackPlay = false;            
        }

        private void PlayBtn_Click(object sender, EventArgs e)
        {
            //isPlayBacking = !isPlayBacking;
            //Global.isPlayBack = !Global.isPlayBack;
            Global.isBackPlay = false;  
            Global.isFrontPlay = !Global.isFrontPlay;
            if (Global.isFrontPlay)
            {
                PlayBtn.BackgroundImage = Properties.Resources.pause;
            }
            //PlayBtn.BackgroundImage = Properties.Resources.pause;
            //try
            //{

            //    //改为启动一个读文件线程来实现回放。
            //    if (Global.PlayBackFileName.Trim() != string.Empty)
            //    {
            //        Thread PlayBackBVHThread = new Thread(ReadBVHFile);
            //        PlayBackBVHThread.Start();
            //    }
            //    else
            //    {
            //        MessageBox.Show("请选择回放文件名");
            //    }                

            //}
            //catch (Exception ee)
            //{
            //    MessageBox.Show("错误：" + ee.Message, "提示", MessageBoxButtons.OK, MessageBoxIcon.Error);
            //}
        }
        
        private void NextFrameBtn_Click(object sender, EventArgs e)
        {
            Global.isFrontPlay = false;
            Global.isBackPlay = false;

            Global.frame_no++;
        }
        
        private void LastFrameBtn_Click(object sender, EventArgs e)
        {
            Global.isFrontPlay = false;
            Global.isBackPlay = false;

            //首先得有加载的文件，然后求出里面的帧总数，赋值。
            Global.frame_no = Global.frame_totalnum-1;
        }

        private void CloseBtn_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void PlaySpeedCombox_SelectedIndexChanged(object sender, EventArgs e)
        {
            int index = this.PlaySpeedCombox.SelectedIndex;
            if (index == 0)
            {
                Global.frame_speed = Global.frame_originspeed * 10; 
            }
            else if (index == 1)
            {
                Global.frame_speed = Global.frame_originspeed * 5;
            }
            else if (index == 2)
            {
                Global.frame_speed = Global.frame_originspeed * 2;
            }
            else if (index == 3)
            {
                Global.frame_speed = Global.frame_originspeed;
            }
            else if (index == 4)
            {
                Global.frame_speed = Global.frame_originspeed / 2;
            }
            else if (index == 5)
            {
                Global.frame_speed = Global.frame_originspeed / 5;
            }
            else if (index == 6)
            {
                Global.frame_speed = Global.frame_originspeed / 10;
            }
            else if (index == 7)
            {
                Global.frame_speed = Global.frame_originspeed / 20;
            }

        }
    }
}
