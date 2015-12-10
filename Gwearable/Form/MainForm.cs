using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Gwearable
{
    public partial class MainForm : Form
    {        
        OpenGLOptForm m_OpenGLform;
        DeviceOptForm m_Deviceoptform;
        WorkDirForm m_DefineWorkDirform;
        ModelsForm m_Definemodelform;
        BodyInforForm m_DefineBodyinfoform;
        PlayBackOptForm m_Playbackform;
        ScreenViewForm m_ScreenViewform;
        
        private bool locked = false;
        private bool workdirflag = false;
        private bool modelflag = false;

        public MainForm()
        {
            InitializeComponent();                        
        }

        public void Init()
        {
            m_OpenGLform = new OpenGLOptForm();
            m_OpenGLform.Dock = DockStyle.Fill;
            m_OpenGLform.Visible = true;
            this.openGLsplitContainer.Panel1.Controls.Add(m_OpenGLform);

            m_Deviceoptform = new DeviceOptForm();
            m_Deviceoptform.Visible = true;
            m_Deviceoptform.Dock = DockStyle.Fill;
            this.deviceoptsplitContainer.Panel1.Controls.Add(m_Deviceoptform);

            m_DefineWorkDirform = new WorkDirForm();
            m_DefineWorkDirform.Visible = true;
            //m_DefineWorkDirform.Visible = false;
            m_DefineWorkDirform.Dock = DockStyle.Fill;
            this.leftbottomsplitContainer.Panel1.Controls.Add(m_DefineWorkDirform);

            if (m_Deviceoptform.GetSerialPort()!=null)
            {
                m_Definemodelform = new ModelsForm(m_Deviceoptform.GetSerialPort());
                m_Definemodelform.Visible = true;
                m_Definemodelform.Dock = DockStyle.Fill;
                this.leftbottomsplitContainer.Panel2.Controls.Add(m_Definemodelform);

            }
            
            m_DefineBodyinfoform = new BodyInforForm();
            m_DefineBodyinfoform.Visible = true;
            m_DefineBodyinfoform.Dock = DockStyle.Fill;
            this.mainSplit.Panel1.Controls.Add(m_DefineBodyinfoform);

            //playback form
            m_Playbackform = new PlayBackOptForm();
            m_Playbackform.Visible = true;
            m_Playbackform.Dock = DockStyle.Fill;
            this.leftTopSplitContainer.Panel2.Controls.Add(m_Playbackform);

            //
            m_ScreenViewform = new ScreenViewForm();
            m_ScreenViewform.Visible = true;
            m_ScreenViewform.Dock = DockStyle.Fill;
            this.deviceoptsplitContainer.Panel2.Controls.Add(m_ScreenViewform);

            m_Definemodelform.SetScreenViewForm(m_ScreenViewform); //通过modelform 控制骨骼长度 传参数

            //注册workdir form 关闭事件
            m_DefineWorkDirform.VisibleChanged += new EventHandler(m_DefineWorkDirform_VisibleChanged);
            m_Definemodelform.VisibleChanged += new EventHandler(m_Definemodelform_VisibleChanged);
            m_DefineBodyinfoform.VisibleChanged += new EventHandler(m_DefineBodyinfoform_VisibleChanged);
            m_Playbackform.VisibleChanged += new EventHandler(m_Playbackform_VisibleChanged);

            //this.m_Deviceoptform.m_DefineSerialPort.UpdateImage = this.m_DefineBodyinfoform.RefreshButton;
            //this.m_Deviceoptform.m_DefineSerialPort.UpdateInfoImage = this.m_DefineBodyinfoform.m_Linkinfoform.RefreshImage;

            //m_DefineBodyinfoform.REFRESHBUTTONIMAGE+=new defineBodyinfoform.RefreshButtonImage(m_DefineBodyinfoform_REFRESHBUTTONIMAGE);

            this.m_Deviceoptform.m_DefineSerialPort.UpdateImage = this.RefreshButton;
            this.m_Deviceoptform.m_DefineSerialPort.UpdateInfoImage = this.RefreshImage;
            //m_DefineBodyinfoform.REFRESHBUTTONIMAGE+=new defineBodyinfoform.RefreshButtonImage(m_DefineBodyinfoform_REFRESHBUTTONIMAGE);

            workDirectoryBarToolStripMenuItem.Checked = false;
            this.m_Deviceoptform.m_UpdateBtnSort = this.RefreshBtnSort;

            //阅读配置文档default.txt ，得到上一次的配置文件，然后调用RefreshBtnSort刷新图片            
                FileStream fs = new FileStream(Application.StartupPath + "\\Config\\default.txt", FileMode.Open); //读取默认配置文件

                StreamReader sr = new StreamReader(fs);
                int index = 0;
                while (!sr.EndOfStream)
                {
                    Global.IDSort[index] = int.Parse(sr.ReadLine());
                    index++;
                    index %= Global.IDSort.Length;
                }
                sr.Close();
                fs.Close();
                RefreshBtnSort();            
            
        }

        private void RefreshBtnSort()
        {
            for (int i= 0; i < Global.IDSort.Length; i++)
            {
                m_DefineBodyinfoform.ChangeButtonImage(i, 0);
            }
            
        }

        private void RefreshButton()
        {
            if (this.InvokeRequired)
            {
//                 if (this.m_DefineBodyinfoform != null&& this!=null )//防止关闭时，mainform已经关闭，但是here依然调用，但是找不到mainform的问题
//                 {
//                     this.Invoke(new UpdateImageex(this.m_DefineBodyinfoform.RefreshButton));                    
//                 }   
             
                try
                {
                    this.Invoke(new UpdateBtnImage(this.m_DefineBodyinfoform.RefreshButtonImage));                    
                }
                catch (System.Exception ex)
                {
                    MessageBox.Show("RefreshButton");
                    MessageBox.Show(ex.Message.ToString());//打印出错误 .一般情况下提示mainform被释放。无法访问被释放的资源
                }
            }
            else
            {
                this.m_DefineBodyinfoform.RefreshButtonImage();
            }
        }

        private void RefreshImage(int[] ration)
        {
            if (this.InvokeRequired)
            {
//                 if (this.m_DefineBodyinfoform != null)
//                 {
//                     this.Invoke(new UpdateInfoImageex(this.m_DefineBodyinfoform.m_Linkinfoform.RefreshImage), ration);
//                 }   

                try
                {
                    this.Invoke(new UpdateInfoImageex(this.m_DefineBodyinfoform.m_Linkinfoform.RefreshImage), ration);
                }
                catch (System.Exception ex)
                {
                    //throw ex;
                    MessageBox.Show("RefreshImage");
                    MessageBox.Show(ex.Message.ToString());
                }
            }
            else
            {
                this.m_DefineBodyinfoform.m_Linkinfoform.RefreshImage(ration);
            }
        }
        
        void m_Playbackform_VisibleChanged(object sender, EventArgs e)
        {
            bool bVisible = (sender as Control).Visible;
            this.leftTopSplitContainer.Panel2Collapsed = !bVisible;
            locked = true;
            transportBarToolStripMenuItem.Checked = bVisible;
            locked = false;

        }

        void m_Definemodelform_VisibleChanged(object sender, EventArgs e)
        {
            if (workdirflag)
            {
                return;
            }
            bool bVisible = (sender as Control).Visible;
            this.leftbottomsplitContainer.Panel2Collapsed = !bVisible;

            if (!m_DefineWorkDirform.Visible && !m_Definemodelform.Visible)
            {
                this.leftmainsplitContainer.Panel2Collapsed = true;
            }

            locked = true;
            editBarToolStripMenuItem.Checked = bVisible;
            locked = false;


        }

        void m_DefineWorkDirform_VisibleChanged(object sender, EventArgs e)
        {
            if (modelflag)
            {
                return;
            }
            bool bVisible = (sender as Control).Visible;
            this.leftbottomsplitContainer.Panel1Collapsed = !bVisible;

            if (!m_DefineWorkDirform.Visible && !m_Definemodelform.Visible)
            {
                this.leftmainsplitContainer.Panel2Collapsed = true;
            }

            locked = true;
            workDirectoryBarToolStripMenuItem.Checked = bVisible;
            locked = false;



        }

        void m_DefineBodyinfoform_VisibleChanged(object sender, EventArgs e)
        {
            bool bVisible=(sender as Control).Visible;
            this.mainSplit.Panel1Collapsed = !bVisible; 
            locked= true;
            sensorBarToolStripMenuItem.Checked = bVisible;
            locked = false;

            
        }      

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PreTOCloseMainForm();
            this.Close();
        }

        private void PreTOCloseMainForm()
        {
            m_Deviceoptform.m_DefineSerialPort.ClosePort();
            m_Deviceoptform.m_DefineSerialPort.KillTimer();
        }

        private void sensorControlsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sensorControlsToolStripMenuItem.Checked = !sensorControlsToolStripMenuItem.Checked;
        }

        private void sensorControlsToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            if (sensorControlsToolStripMenuItem.Checked)
             {
                 m_Deviceoptform.Visible = true;
                 this.deviceoptsplitContainer.Panel1Collapsed = false;
             }
            else
            {
                m_Deviceoptform.Visible = false;
                this.deviceoptsplitContainer.Panel1Collapsed =true;

            }
        }

        private void sensorBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            sensorBarToolStripMenuItem.Checked = !sensorBarToolStripMenuItem.Checked;
        }

        private void sensorBarToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            if (locked)
                    return;

            if (sensorBarToolStripMenuItem.Checked)
            {
                m_DefineBodyinfoform.Visible = true;
                this.mainSplit.Panel1Collapsed = false;
            }
            else
            {
                m_DefineBodyinfoform.Visible = false;
                this.mainSplit.Panel1Collapsed = true;
            }

        }

        private void openGLControlsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            openGLControlsToolStripMenuItem.Checked = !openGLControlsToolStripMenuItem.Checked;
        }

        private void openGLControlsToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            if (openGLControlsToolStripMenuItem.Checked)
            {
                m_OpenGLform.Visible = true;
                this.openGLsplitContainer.Panel1Collapsed = false;
            }
            else
            {
                m_OpenGLform.Visible = false;
                this.openGLsplitContainer.Panel1Collapsed = true;
            }
        }

        private void transportBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            transportBarToolStripMenuItem.Checked = !transportBarToolStripMenuItem.Checked;                   
        }

        private void transportBarToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            if (locked)
            {
                return;
            }

            if (transportBarToolStripMenuItem.Checked)
            {
                m_Playbackform.Visible = true;
                this.leftTopSplitContainer.Panel2Collapsed = false;
            }
            else
            {
                m_Playbackform.Visible = false;
                this.leftTopSplitContainer.Panel2Collapsed = true;
            }
        }

        private void workDirectoryBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            workDirectoryBarToolStripMenuItem.Checked = !workDirectoryBarToolStripMenuItem.Checked;
        }

        private void workDirectoryBarToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            if (locked) return;
            if (workDirectoryBarToolStripMenuItem.Checked)
            {
                //m_DefineWorkDirform.Visible = true;

                if(m_Definemodelform.Visible)
                {
                    m_DefineWorkDirform.Visible = true;
                    this.leftbottomsplitContainer.Panel1Collapsed = false;
                }
                else
                {
                    workdirflag = true;
                    //this.leftmainsplitContainer.Panel2Collapsed = false;//这里显示导致子控件的visible发生了变化//
                    m_DefineWorkDirform.Visible = true;
                    m_Definemodelform.Visible = false;
                    this.leftbottomsplitContainer.Panel1Collapsed = false;
                    this.leftbottomsplitContainer.Panel2Collapsed = true;
                    this.leftmainsplitContainer.Panel2Collapsed = false;//这里显示导致子控件的visible发生了变化//
                    workdirflag = false;
                }
                
            }
            else
            {
                if (!m_Definemodelform.Visible)
                {
                    //全部为空
                    //this.leftbottomsplitContainer.Panel1Collapsed = true;
                    this.leftmainsplitContainer.Panel2Collapsed = true;
                    //isleftmainsplittercollspe = true;
                }
                else
                {
                    this.leftbottomsplitContainer.Panel1Collapsed = true;
                    this.leftmainsplitContainer.Panel2Collapsed = false;
                }
            }
        }

        private void editBarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            editBarToolStripMenuItem.Checked = !editBarToolStripMenuItem.Checked;
        }

        private void editBarToolStripMenuItem_CheckStateChanged(object sender, EventArgs e)
        {
            if (locked) return;
            if (editBarToolStripMenuItem.Checked)
            {
                //m_Definemodelform.Visible = true;
                if (m_DefineWorkDirform.Visible)
                {
                    m_Definemodelform.Visible = true;
                    this.leftbottomsplitContainer.Panel2Collapsed = false;
                }
                else
                {
                    modelflag = true;                    
                    //this.leftmainsplitContainer.Panel2Collapsed = false;//这里显示导致子控件的visible发生了变化//
                    m_Definemodelform.Visible = true;
                    m_DefineWorkDirform.Visible = false;
                    this.leftbottomsplitContainer.Panel2Collapsed = false;
                    this.leftbottomsplitContainer.Panel1Collapsed = true;
                    this.leftmainsplitContainer.Panel2Collapsed = false;//这里显示导致子控件的visible发生了变化//
                    modelflag = false;
                }                
            }
            else
            {
                if (!m_DefineWorkDirform.Visible)
                {
                    //全部为空
                    //this.leftbottomsplitContainer.Panel2Collapsed = true;
                    this.leftmainsplitContainer.Panel2Collapsed = true;
                    //isleftmainsplittercollspe = true;
                }
                else
                {
                    this.leftbottomsplitContainer.Panel2Collapsed = true;
                    this.leftmainsplitContainer.Panel2Collapsed = false;
                }
            }
        }

        private void Mainform_Load(object sender, EventArgs e)
        {
            //Form2 splashform = new Form2();
            //splashform.ShowDialog();
        }

        private void Mainform_Resize(object sender, EventArgs e)
        {
            //this.Width - 76;
            this.Text = "";
            int tablecount = (this.Width - 76 - 100)/8;

            for (int i = 0; i < tablecount; i++)
            {
                this.Text += " ";
            }
            this.Text += "G-wearables";

        }

        private void Mainform_MouseMove(object sender, MouseEventArgs e)
        {
            //Activate(); //do nothing            
        }

        private void Mainform_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 'r' || e.KeyChar == 'R')
            {
                m_Deviceoptform.Calibrate();
            }
        }
    }
}


/*
 *          测试算法用临时代码
 *          CQuaternion q1 = new CQuaternion( 0.5,0.5,0.5,0.5);
	        CQuaternion q2 = new CQuaternion( 0.707,0,0,0.707);

	        Euler result;
	        result = CQuaternion.Quat2angle(   -0.5905, -0.4077, 0.2256, 0.6589, 6);
	
	        int[] A = { 36(start),2(ID),0,255,164,4,236,239,65,216,128,0,18,246,209,194,58,219,0,0,0,  9 *0     13,10 (1310=0d0a)};
            CQuaternion result2 =  CQuaternion.getRawQuat(A,23);


            double _a = result2.m_q0;
            double _a1 = result2.m_q1;
            double _a2 = result2.m_q2;
            double _a3 = result2.m_q3;
 * 
 */