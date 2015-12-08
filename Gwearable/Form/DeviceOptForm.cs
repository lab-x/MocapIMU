using System;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace Gwearable
{
    public delegate void UpdateBtnSort();

    public partial class DeviceOptForm : UserControl
    {
        public UpdateBtnSort m_UpdateBtnSort = null;
        public defineSerialPort m_DefineSerialPort = new defineSerialPort();

        public DeviceOptForm()
        {
            InitializeComponent();
            m_DefineSerialPort = new defineSerialPort();
        }
        public defineSerialPort GetSerialPort()
        {
            if (m_DefineSerialPort!= null)
            {
                return m_DefineSerialPort;
            }
            return null;
            
        }
  
        private void toolStrip1_Paint(object sender, PaintEventArgs e)
        {
            if ((sender as ToolStrip).RenderMode == ToolStripRenderMode.System)
            {
                Rectangle rect = new Rectangle(0, 0, this.toolStrip1.Width, this.toolStrip1.Height - 2);
                e.Graphics.SetClip(rect);
            }
        }
        
        public void Calibrate()
        {            
            Global.isCali = false;
            Global.AposeDone = false;
            Global.TposeDone = false;
            Global.SposeDone = false;
            Global.XposeDone = false;

            for (int i = 0; i < 17;i++ )
            {
                SegmentCollection.arraySegment[i].ADone = false;
                SegmentCollection.arraySegment[i].TDone = false;
                SegmentCollection.arraySegment[i].XDone = false;
                SegmentCollection.arraySegment[i].SDone = false;
                SegmentCollection.arraySegment[i].CaliA = false;
                SegmentCollection.arraySegment[i].CaliT = false;
                SegmentCollection.arraySegment[i].CaliX = false;
                SegmentCollection.arraySegment[i].CaliS = false;
            }
            m_DefineSerialPort.ResetSwitchFlag();//清空第一次使用标志            
        }

        #region 保存对话框
        private void ShowSaveFileDialog()
        {
            //string localFilePath, fileNameExt, newFileName, FilePath; 
            SaveFileDialog sfd = new SaveFileDialog();
            //设置文件类型 
            sfd.Filter = "动作捕捉文件（*.bvh）|*.bvh";

            //设置默认文件类型显示顺序 
            sfd.FilterIndex = 1;

            //保存对话框是否记忆上次打开的目录 
            sfd.RestoreDirectory = true;

            sfd.InitialDirectory = Application.StartupPath + "\\MotionFiles";

            //点了保存按钮进入 
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string localFilePath = sfd.FileName.ToString(); //获得文件路径
                string fileNameExt = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1); //获取文件名，不带路径

                FileStream fs = new FileStream(localFilePath, FileMode.Create); //新建空文件。
                fs.Close();
                m_DefineSerialPort.cf.CreateFile(localFilePath);

                //获取文件路径，不带文件名 
                //FilePath = localFilePath.Substring(0, localFilePath.LastIndexOf("\\"));
                //给文件名前加上时间 
                //newFileName = DateTime.Now.ToString("yyyyMMdd") + fileNameExt;
                //在文件名里加字符 
                //saveFileDialog1.FileName.Insert(1,"dameng");
                //System.IO.FileStream fs = (System.IO.FileStream)sfd.OpenFile();//输出文件
                ////fs输出带文字或图片的文件，就看需求了 
            }
        }

        #endregion

        //已经废弃的函数，暂时保留
        //private void Deviceoptpanel_KeyPress(object sender, KeyPressEventArgs e)
        //{
        //    if (e.KeyChar == 'a')
        //    {
        //        CalibrateBtn_Click(sender, e);
        //    }
        //}

        //
        //protected override bool ProcessCmdKey(ref Message msg, Keys keyData)
        //{
        //    Calibrate();
        //    return base.ProcessCmdKey(ref msg, keyData);
        //}

        private void ZeroBtn_Click(object sender, EventArgs e)
        {
            Global.Fall = false;
            if (Global.EnableAbsPos)
            {
                Global.EnableAbsPos = false;
                Global.ActivateAbsPos = false;
                //Global.HIP.y = 0;
                Global.HIP = new Pos(0, 0, 0);
                SegmentCollection.arraySegment[10].Position = new Pos(0,0,0);
                Console.WriteLine("External Position Disabled");
            }
            else
            {
                Global.EnableAbsPos = true;
                //Global.ActivateAbsPos = true;
                Console.WriteLine("External Position Enabled");
                SegmentCollection.arraySegment[10].Position = Global.HEAD;
            }

            //if (Global.ActivateAbsPos)
            //{
            //    Global.ActivateAbsPos = false;
            //    Global.ArrayAvailable = false;
            //    Console.WriteLine("External Position Disabled");
            //}
            //else
            //{
            //    Global.ActivateAbsPos = true;
            //    Global.ArrayAvailable = true;
            //    // Global.HIP = SegmentCollection.arraySegment[10].Position;
            //    // Global.HIP.z = 0.0;

            //    //开串口读数据，然后
            //    //Global.HEAD = new Pos(X,y,z);/////  meng meng ti gong   //ysj+++bug
                
            //    Console.WriteLine("External Position Enabled");
            //}
        }

        #region 保存配置文件对话框
        private void ShowSaveConfigFileDialog()
        {
            SaveFileDialog sfd = new SaveFileDialog();
            //设置文件类型 
            sfd.Filter = "传感器配置索引（*.txt）|*.txt";
            //设置默认文件类型显示顺序 
            sfd.FilterIndex = 1;
            //保存对话框是否记忆上次打开的目录 
            sfd.RestoreDirectory = true;
            sfd.InitialDirectory = Application.StartupPath + "\\Config";
            //点了保存按钮进入 
            if (sfd.ShowDialog() == DialogResult.OK)
            {
                string localFilePath = sfd.FileName.ToString(); //获得文件路径
                string fileNameExt = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1); //获取文件名，不带路径
                FileStream fs = new FileStream(localFilePath, FileMode.OpenOrCreate); //新建空文件。保证该文件存在

                StreamWriter sw = new StreamWriter(fs, Encoding.ASCII);
                for (int i = 0; i < Global.IDSort.Length; i++)
                {
                    sw.WriteLine(Global.IDSort[i].ToString());
                }
                sw.Close();
                fs.Close();
                //m_DefineSerialPort.CreateFile(localFilePath);

                //将打开的文件内容复制到default.txt文件中，下一次直接加载他
                FileStream fsdefault = new FileStream(Application.StartupPath + "\\Config\\default.txt", FileMode.OpenOrCreate);

                StreamWriter swdefault = new StreamWriter(fsdefault, Encoding.ASCII);
                for (int i = 0; i < Global.IDSort.Length; i++)
                {
                    swdefault.WriteLine(Global.IDSort[i].ToString());
                }

                swdefault.Close();
                fsdefault.Close();



            }
        }

        #endregion

        #region  打开配置文件对话框

        private void ShowOpenConfigFileDialog()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            //设置文件类型 
            ofd.Filter = "传感器配置索引（*.txt）|*.txt";
            //设置默认文件类型显示顺序             
            ofd.FilterIndex = 1;
            //打开对话框是否记忆上次打开的目录 
            ofd.RestoreDirectory = true;
            ofd.InitialDirectory = Application.StartupPath + "\\Config";
            //点了打开按钮进入 
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                string localFilePath = ofd.FileName.ToString();
                string fileNameExt = localFilePath.Substring(localFilePath.LastIndexOf("\\") + 1);
                FileStream fs = new FileStream(localFilePath, FileMode.Open);
                StreamReader sr = new StreamReader(fs);
                int index = 0;
                while (!sr.EndOfStream)
                {
                    Global.IDSort[index] = int.Parse(sr.ReadLine());
                    index++;
                }
                sr.Close();
                fs.Close();

                //将打开的文件内容复制到default.txt文件中，下一次直接加载他
                FileStream fsdefault = new FileStream(Application.StartupPath + "\\Config\\default.txt", FileMode.OpenOrCreate);
                StreamWriter swdefault = new StreamWriter(fsdefault, Encoding.ASCII);
                for (int i = 0; i < Global.IDSort.Length; i++)
                {
                    swdefault.WriteLine(Global.IDSort[i].ToString());
                }

                swdefault.Close();
                fsdefault.Close();


                //立刻刷新图片的信息
                for (int i = 0; i < Global.IDSort.Length; i++)
                {
                    this.m_UpdateBtnSort(); //替换时，全部为未通电时的色彩  //通过定义委托来搞定这事                    
                }
            }
        }

        #endregion
        
        private void ConfigSaveBtn_Click(object sender, EventArgs e)
        {
            ShowSaveConfigFileDialog();
        }

        private void ConfigOpenBtn_Click(object sender, EventArgs e)
        {
            ShowOpenConfigFileDialog();
        }

        private void OpenFileBtn_Click(object sender, EventArgs e)
        {
            openFileDialog1.ShowDialog();
        }

        private void VideoBtn_Click(object sender, EventArgs e)
        {
            if (Global.isWriteable == false)
            {
                if (!m_DefineSerialPort.m_DownPort.IsOpen)
                {
                    MessageBox.Show("没有数据!");
                    return;
                }

                ShowSaveFileDialog();

                //开始录制
                m_DefineSerialPort.cf.StartWriteData();
                //MessageBox.Show("开始录制");
            }
            else
            {
                //录制结束
                m_DefineSerialPort.cf.EndWriteData();
                MessageBox.Show("录制结束");
            }
        }

        private void ConnectBtn_Click(object sender, EventArgs e)
        {
            //首先添加一个给下位机发消息的步骤

            if (!m_DefineSerialPort.m_Port.IsOpen && !m_DefineSerialPort.m_DownPort.IsOpen)
            {
                if (Global.PortName.Trim() == string.Empty && Global.PortName_7.Trim() == string.Empty && Global.GPSPortName.Trim() == string.Empty)
                {
                    MessageBox.Show("两个端口不能都为空");
                    return;
                }

                bool m_portflag = false;
                bool m_portflag_7 = false;
                bool m_portGPSflag = false;

                if (Global.PortName.Trim() != string.Empty)
                {
                    m_DefineSerialPort.InitSerialPort(m_DefineSerialPort.m_Port, Global.PortName);
                    m_portflag = m_DefineSerialPort.OpenPort(m_DefineSerialPort.m_Port);
                }

                if (Global.PortName_7.Trim() != string.Empty)
                {
                    m_DefineSerialPort.InitSerialPort(m_DefineSerialPort.m_DownPort, Global.PortName_7);
                    m_portflag_7 = m_DefineSerialPort.OpenPort(m_DefineSerialPort.m_DownPort);
                }

                if (Global.GPSPortName.Trim() != string.Empty)
                {
                    m_DefineSerialPort.InitSerialPort(m_DefineSerialPort.m_GPSPort, Global.GPSPortName);
                    m_portGPSflag = m_DefineSerialPort.OpenPort(m_DefineSerialPort.m_GPSPort);
                }


                if (m_portflag || m_portflag_7 || m_portGPSflag)  //该串口名称 暂时写死 貌似是这样子的
                {
                    m_DefineSerialPort.StartAllThread();

                    ConnectBtn.Enabled = false;
                    DisconnectBtn.Enabled = true;
                    PoweroffBtn.Enabled = true;
                    CalibrateBtn.Enabled = true;

                    Global.isLive = true;
                }
            }
        }

        private void DisconnectBtn_Click(object sender, EventArgs e)
        {
            if (m_DefineSerialPort.ClosePort())
            {
                ConnectBtn.Enabled = true;
                DisconnectBtn.Enabled = false;
                CalibrateBtn.Enabled = false;
            }
        }

        private void PoweroffBtn_Click(object sender, EventArgs e)
        {
            if(Global.Zero == false)
            Global.Zero = true;
            else
                Global.Zero = false;

           
        }

        private void CalibrateBtn_Click(object sender, EventArgs e)
        {
            //Calibrate();
            //ysj 
            TPoseAPoseForm TPAForm = new TPoseAPoseForm(m_DefineSerialPort);
            if (DialogResult.OK == TPAForm.ShowDialog())
            {
                //MessageBox.Show("校准完毕");
            }
        }

    }
}
