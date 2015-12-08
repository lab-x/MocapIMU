using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net.Sockets;
using System.Net;
using System.Threading;
using System.IO;

namespace Gwearable
{
    public delegate void UpdateLabelText();

    public delegate void UpdateGPSText();

    public partial class ModelsForm : UserControl
    {
        public UpdateLabelText m_UpdateLabelText = null;
        public UpdateGPSText m_UpdateGPSText = null;

        private static byte[] result = new byte[1024];
        private static byte[] recvresult = new byte[10];
        private static int myProt = 7001;
        static Socket serverSocket = null;

        public string GetPortName()
        {
            return tbPortname.Text;
        }

        private defineSerialPort m_port = null;

        public ModelsForm(defineSerialPort port)
        {
            InitializeComponent();
            m_port = port; //deviceoptform类内的port成员变量
            //StartSendThread();
            panel8.Width -= 20;
            customScrollbar1.Height = panel8.Height;

            Point pt;
            pt = new Point(this.panel8.AutoScrollPosition.X, this.panel8.AutoScrollPosition.Y);
            this.customScrollbar1.Minimum = 0;
            this.customScrollbar1.Maximum = this.panel8.DisplayRectangle.Height;//panel的最大高度
            this.customScrollbar1.LargeChange = customScrollbar1.Maximum / customScrollbar1.Height + this.panel8.Height;
            this.customScrollbar1.SmallChange = 5;
            this.customScrollbar1.Value = Math.Abs(this.panel8.AutoScrollPosition.Y);


            //初始化下位机按钮状态
            InitMode();


            //初始化opengl球体
            //this.glWindow.Parent = this.DisplayOpengl;
            //this.glWindow.Dock = DockStyle.Fill;


            InitCollectTimer();
            InitUpdateTimer();
            this.m_UpdateLabelText = new UpdateLabelText(ChangeLabelText);
            InitUpdateGPSTimer();
            this.m_UpdateGPSText = new UpdateGPSText(ChangePGSText);

            //开启陀螺校准的状态计时器
            InitAndStartCaliTimer();
            InitAndStartQueryTimer();
            InitAndStartExitTimer();
        }

        private void ChangeLabelText()
        {
            OptStatus.Text = Global.CollectDataStatus;
        }

        private void ChangePGSText()
        {
            if (Global.NoDataFlag == true)
            {
                GPSX.Text = "No Valid Data";
                GPSY.Text = "No Valid Data";
                GPSZ.Text = "No Valid Data";
            }
            else
            {
                if (Global.Station0)
                {
                    GPSX.Text = " 0";
                    GPSY.Text = Global.HEADRAW.ToString();
                    if (Global.TransMatAvailable == true)
                    {
                        GPSZ.Text = Global.HEAD.ToString();
                    }
                    else
                    {
                        GPSZ.Text = "Not Transformed";
                    }
                }
                else if (Global.Station1)
                {
                    GPSX.Text = " 1"; 
                    GPSY.Text = Global.HEADRAW.ToString();
                    if (Global.TransMatAvailable == true)
                    {
                        GPSZ.Text = Global.HEAD.ToString();
                    }
                    else
                    {
                        GPSZ.Text = "Not Transformed";
                    }
                }
                else
                {
                    GPSX.Text = "***********";
                } 
               // GPSX.Text = Global.HEAD.x.ToString();
               // GPSY.Text = Global.HEAD.y.ToString();
               // GPSZ.Text = Global.HEAD.z.ToString();
            }
        }
        private void InitMode()
        {
            SensorID.Enabled = false;
            GeomagnetsimCalibrate.Enabled = false;
            GyroCalibrate.Enabled = false;
            StopCalibrate.Enabled = false;
        }

        private void ChangeLargeBarLength()
        {
            this.customScrollbar1.Minimum = 0;
            this.customScrollbar1.Maximum = this.panel8.DisplayRectangle.Height;//panel的最大高度
            this.customScrollbar1.LargeChange = customScrollbar1.Maximum / customScrollbar1.Height + this.panel8.Height;
            this.customScrollbar1.SmallChange = 1;
        }

        private void checkBox19_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox19.Checked)
            {
                textBoxport.Enabled = true;
                comboBoxformat.Enabled = true;
                checkBoxdisplacement.Enabled = true;
                checkBoxreference.Enabled = true;
                textBoxIP.Enabled = true;
            }
            else
            {
                textBoxport.Enabled = false;
                comboBoxformat.Enabled = false;
                checkBoxdisplacement.Enabled = false;
                checkBoxreference.Enabled = false;
                textBoxIP.Enabled = false;
            }
        }

        private void closebtn_Click(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private void startserver_Click(object sender, EventArgs e)
        {
            StartSendThread();
        }

        private void StartSendThread()
        {
            if (serverSocket != null)
            {
                return;
            }
            if (textBoxport.Text.Trim() == string.Empty)
            {
                MessageBox.Show("端口号不能为空!");
                return;
            }
            if (textBoxIP.Text.Trim() == string.Empty)
            {
                MessageBox.Show("IP地址不能为空!");
                return;
            }
            //IPAddress ip = IPAddress.Parse("192.168.1.122");
            IPAddress ip = IPAddress.Parse(textBoxIP.Text);
            serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            myProt = System.Int32.Parse(textBoxport.Text);

            //IPHostEntry myIP = Dns.GetHostEntry(Dns.GetHostName());
            //string ipfromhostname = myIP.AddressList.GetValue(1).ToString();
            //IPAddress trueip = IPAddress.Parse(ipfromhostname);

            serverSocket.Bind(new IPEndPoint(ip, myProt)); /////port多次使用 这里会报错的 ysj bug
            serverSocket.Listen(5);
            //通过Clientsoket发送数据  
            Thread myThread = new Thread(ListenClientConnect);
            myThread.IsBackground = true;
            myThread.Start();
        }

        private static void ListenClientConnect()
        {
            while (true)
            {
                Socket clientSocket = serverSocket.Accept();
                Thread sendThread = new Thread(sendMessage);
                sendThread.Start(clientSocket);
                Thread recvThread = new Thread(ReceiveMessage);
                recvThread.Start(clientSocket);
                Global.isRemote = true; //有远程客户端连接
            }
        }

        public static void Calibrate()
        {
            //直接跨类调用不方便，我直接在这里调用了校准的函数内容。正好是静态方便使用，若需要修改，则此处需一并修改            
            Global.isCali = false;
            Global.AposeDone = false;
            Global.TposeDone = false;
            Global.SposeDone = false;
            Global.XposeDone = false;



            for (int i = 0; i < 17; i++)
            {
                SegmentCollection.arraySegment[i].ADone = false;
                SegmentCollection.arraySegment[i].TDone = false;
                SegmentCollection.arraySegment[i].SDone = false;
                SegmentCollection.arraySegment[i].XDone = false;
                SegmentCollection.arraySegment[i].CaliA = false;
                SegmentCollection.arraySegment[i].CaliT = false;
                SegmentCollection.arraySegment[i].CaliX = false;
                SegmentCollection.arraySegment[i].CaliS = false;
            }

            SegmentCollection.arraySegment[10].Position = new Pos(0, 0.00, 0);//new Pos(0, 1.13, 0);
            SegmentCollection.arraySegment[13].Position = new Pos(0, 0.00, 0);
            SegmentCollection.arraySegment[16].Position = new Pos(0, 0.00, 0);
            for (int i = 0; i < defineSerialPort.FirstRunFlagArray.Length; i++)
            {
                defineSerialPort.FirstRunFlagArray[i] = 0;
            }

        }

        private static void sendMessage(object clientSocket)
        {
            Socket myClientSocket = (Socket)clientSocket;
            //myClientSocket.Blocking = false;
            while (true)
            {
                try
                {
                    if (Global.m_strQueue.Count > 0)
                    {
                        string str = "";
                        bool issuccess = Global.m_strQueue.TryDequeue(out str);
                        myClientSocket.Send(Encoding.ASCII.GetBytes(str));
                    }


                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    if (myClientSocket != null && myClientSocket.Connected)
                    {
                        myClientSocket.Shutdown(SocketShutdown.Both);
                        myClientSocket.Close();
                    }
                    break;
                }
            }
        }

        private static void ReceiveMessage(object clientSocket)
        {
            Socket myClientSocket = (Socket)clientSocket;
            while (true)
            {
                try
                {
                    //int receiveNumber = myClientSocket.Receive(result);
                    // Console.WriteLine("接收客户端{0}消息{1}", myClientSocket.RemoteEndPoint.ToString(), Encoding.ASCII.GetString(result, 0, receiveNumber));

                    int receiveNumber = myClientSocket.Receive(recvresult);//这里会发生阻塞

                    if (receiveNumber != 0)
                    {
                        string recvstr = Encoding.ASCII.GetString(recvresult, 0, receiveNumber);
                        if (recvstr == "TPose")
                        {
                            Global.CanTpose = true;
                            Calibrate(); //只在TPOSE时，被调用一次
                            try
                            {
                                myClientSocket.Send(Encoding.ASCII.GetBytes("TOK"));
                                Console.WriteLine("send TOK to client*******");
                            }
                            catch(Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                        else if (recvstr == "XPose")
                        {
                            try
                            {
                                Global.CanXpose = true;
                                myClientSocket.Send(Encoding.ASCII.GetBytes("XOK"));
                                Console.WriteLine("send XOK to client*********");                                
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                        else if (recvstr == "APose")
                        {
                            try
                            {
                                Global.CanApose = true;
                                myClientSocket.Send(Encoding.ASCII.GetBytes("AOK"));
                                Console.WriteLine("send AOK to client***********");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                        else if (recvstr == "SPose")
                        {
                            try
                            {
                                Global.CanSpose = true;
                                myClientSocket.Send(Encoding.ASCII.GetBytes("SOK"));
                                Console.WriteLine("send SOK to client*******");
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    if (myClientSocket != null && myClientSocket.Connected)
                    {
                        myClientSocket.Shutdown(SocketShutdown.Both);
                        myClientSocket.Close();
                    }
                    break;
                }
            }
        }

        private void groupBox9_Enter(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void tbPortname_TextChanged(object sender, EventArgs e)
        {
            //Global.PortName = (sender as TextBox).Text;
        }

        private void groupBox1_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(groupBox1.BackColor);
            e.Graphics.DrawString(groupBox1.Text, groupBox1.Font, Brushes.Black, 10, 1);
            e.Graphics.DrawLine(Pens.Black, 1, 7, 8, 7);
            e.Graphics.DrawLine(Pens.Black, 102, 7, groupBox1.Width - 2, 7);
            e.Graphics.DrawLine(Pens.Black, 1, 7, 1, groupBox1.Height - 2);
            e.Graphics.DrawLine(Pens.Black, 1, groupBox1.Height - 2, groupBox1.Width - 2, groupBox1.Height - 2);
            e.Graphics.DrawLine(Pens.Black, groupBox1.Width - 2, 7, groupBox1.Width - 2, groupBox1.Height - 2);
        }

        private void groupBox2_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(groupBox2.BackColor);
            e.Graphics.DrawString(groupBox2.Text, groupBox2.Font, Brushes.Black, 10, 1);
            e.Graphics.DrawLine(Pens.Black, 1, 7, 8, 7);
            e.Graphics.DrawLine(Pens.Black, 92, 7, groupBox2.Width - 2, 7);
            e.Graphics.DrawLine(Pens.Black, 1, 7, 1, groupBox2.Height - 2);
            e.Graphics.DrawLine(Pens.Black, 1, groupBox2.Height - 2, groupBox2.Width - 2, groupBox2.Height - 2);
            e.Graphics.DrawLine(Pens.Black, groupBox2.Width - 2, 7, groupBox2.Width - 2, groupBox2.Height - 2);
        }

        private void groupBox3_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(groupBox3.BackColor);
            e.Graphics.DrawString(groupBox3.Text, groupBox3.Font, Brushes.Black, 10, 1);
            e.Graphics.DrawLine(Pens.Black, 1, 7, 8, 7);
            e.Graphics.DrawLine(Pens.Black, 58, 7, groupBox3.Width - 2, 7);
            e.Graphics.DrawLine(Pens.Black, 1, 7, 1, groupBox3.Height - 2);
            e.Graphics.DrawLine(Pens.Black, 1, groupBox3.Height - 2, groupBox3.Width - 2, groupBox3.Height - 2);
            e.Graphics.DrawLine(Pens.Black, groupBox3.Width - 2, 7, groupBox3.Width - 2, groupBox3.Height - 2);
        }

        private void groupBox4_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(groupBox4.BackColor);
            e.Graphics.DrawString(groupBox4.Text, groupBox4.Font, Brushes.Black, 10, 1);
            e.Graphics.DrawLine(Pens.Black, 1, 7, 8, 7);
            e.Graphics.DrawLine(Pens.Black, 146, 7, groupBox4.Width - 2, 7);
            e.Graphics.DrawLine(Pens.Black, 1, 7, 1, groupBox4.Height - 2);
            e.Graphics.DrawLine(Pens.Black, 1, groupBox4.Height - 2, groupBox4.Width - 2, groupBox4.Height - 2);
            e.Graphics.DrawLine(Pens.Black, groupBox4.Width - 2, 7, groupBox4.Width - 2, groupBox4.Height - 2);
        }

        private void groupBox5_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(groupBox5.BackColor);
            e.Graphics.DrawString(groupBox5.Text, groupBox5.Font, Brushes.Black, 10, 1);
            e.Graphics.DrawLine(Pens.Black, 1, 7, 8, 7);
            e.Graphics.DrawLine(Pens.Black, 136, 7, groupBox5.Width - 2, 7);
            e.Graphics.DrawLine(Pens.Black, 1, 7, 1, groupBox5.Height - 2);
            e.Graphics.DrawLine(Pens.Black, 1, groupBox5.Height - 2, groupBox5.Width - 2, groupBox5.Height - 2);
            e.Graphics.DrawLine(Pens.Black, groupBox5.Width - 2, 7, groupBox5.Width - 2, groupBox5.Height - 2);
        }

        private void groupBox6_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(groupBox6.BackColor);
            e.Graphics.DrawString(groupBox6.Text, groupBox6.Font, Brushes.Black, 10, 1);
            e.Graphics.DrawLine(Pens.Black, 1, 7, 8, 7);
            e.Graphics.DrawLine(Pens.Black, 94, 7, groupBox6.Width - 2, 7);
            e.Graphics.DrawLine(Pens.Black, 1, 7, 1, groupBox6.Height - 2);
            e.Graphics.DrawLine(Pens.Black, 1, groupBox6.Height - 2, groupBox6.Width - 2, groupBox6.Height - 2);
            e.Graphics.DrawLine(Pens.Black, groupBox6.Width - 2, 7, groupBox6.Width - 2, groupBox6.Height - 2);
        }

        private void groupBox7_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(groupBox7.BackColor);
            e.Graphics.DrawString(groupBox7.Text, groupBox7.Font, Brushes.Black, 10, 1);
            e.Graphics.DrawLine(Pens.Black, 1, 7, 8, 7);
            e.Graphics.DrawLine(Pens.Black, 63, 7, groupBox7.Width - 2, 7);
            e.Graphics.DrawLine(Pens.Black, 1, 7, 1, groupBox7.Height - 2);
            e.Graphics.DrawLine(Pens.Black, 1, groupBox7.Height - 2, groupBox7.Width - 2, groupBox7.Height - 2);
            e.Graphics.DrawLine(Pens.Black, groupBox7.Width - 2, 7, groupBox7.Width - 2, groupBox7.Height - 2);
        }

        private void groupBox8_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(groupBox8.BackColor);
            e.Graphics.DrawString(groupBox8.Text, groupBox8.Font, Brushes.Black, 10, 1);
            e.Graphics.DrawLine(Pens.Black, 1, 7, 8, 7);
            e.Graphics.DrawLine(Pens.Black, 130, 7, groupBox8.Width - 2, 7);
            e.Graphics.DrawLine(Pens.Black, 1, 7, 1, groupBox8.Height - 2);
            e.Graphics.DrawLine(Pens.Black, 1, groupBox8.Height - 2, groupBox8.Width - 2, groupBox8.Height - 2);
            e.Graphics.DrawLine(Pens.Black, groupBox8.Width - 2, 7, groupBox8.Width - 2, groupBox8.Height - 2);
        }

        private void groupBox9_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(groupBox9.BackColor);
            e.Graphics.DrawString(groupBox9.Text, groupBox9.Font, Brushes.Black, 10, 1);
            e.Graphics.DrawLine(Pens.Black, 1, 7, 8, 7);
            e.Graphics.DrawLine(Pens.Black, 64, 7, groupBox9.Width - 2, 7);
            e.Graphics.DrawLine(Pens.Black, 1, 7, 1, groupBox9.Height - 2);
            e.Graphics.DrawLine(Pens.Black, 1, groupBox9.Height - 2, groupBox9.Width - 2, groupBox9.Height - 2);
            e.Graphics.DrawLine(Pens.Black, groupBox9.Width - 2, 7, groupBox9.Width - 2, groupBox9.Height - 2);
        }

        private void groupBox10_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(groupBox10.BackColor);
            e.Graphics.DrawString(groupBox10.Text, groupBox10.Font, Brushes.Black, 10, 1);
            e.Graphics.DrawLine(Pens.Black, 1, 7, 8, 7);
            e.Graphics.DrawLine(Pens.Black, 40, 7, groupBox10.Width - 2, 7);
            e.Graphics.DrawLine(Pens.Black, 1, 7, 1, groupBox10.Height - 2);
            e.Graphics.DrawLine(Pens.Black, 1, groupBox10.Height - 2, groupBox10.Width - 2, groupBox10.Height - 2);
            e.Graphics.DrawLine(Pens.Black, groupBox10.Width - 2, 7, groupBox10.Width - 2, groupBox10.Height - 2);
        }

        private void groupBox11_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(groupBox11.BackColor);
            e.Graphics.DrawString(groupBox11.Text, groupBox11.Font, Brushes.Black, 10, 1);
            e.Graphics.DrawLine(Pens.Black, 1, 7, 8, 7);
            e.Graphics.DrawLine(Pens.Black, 74, 7, groupBox11.Width - 2, 7);
            e.Graphics.DrawLine(Pens.Black, 1, 7, 1, groupBox11.Height - 2);
            e.Graphics.DrawLine(Pens.Black, 1, groupBox11.Height - 2, groupBox11.Width - 2, groupBox11.Height - 2);
            e.Graphics.DrawLine(Pens.Black, groupBox11.Width - 2, 7, groupBox11.Width - 2, groupBox11.Height - 2);
        }

        private void groupBox12_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(groupBox12.BackColor);
            e.Graphics.DrawString(groupBox12.Text, groupBox12.Font, Brushes.Black, 10, 1);
            e.Graphics.DrawLine(Pens.Black, 1, 7, 8, 7);
            e.Graphics.DrawLine(Pens.Black, 64, 7, groupBox12.Width - 2, 7);
            e.Graphics.DrawLine(Pens.Black, 1, 7, 1, groupBox12.Height - 2);
            e.Graphics.DrawLine(Pens.Black, 1, groupBox12.Height - 2, groupBox12.Width - 2, groupBox12.Height - 2);
            e.Graphics.DrawLine(Pens.Black, groupBox12.Width - 2, 7, groupBox12.Width - 2, groupBox12.Height - 2);
        }

        private void groupBox13_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.Clear(groupBox13.BackColor);
            e.Graphics.DrawString(groupBox13.Text, groupBox13.Font, Brushes.Black, 10, 1);
            e.Graphics.DrawLine(Pens.Black, 1, 7, 8, 7);
            e.Graphics.DrawLine(Pens.Black, 64, 7, groupBox13.Width - 2, 7);
            e.Graphics.DrawLine(Pens.Black, 1, 7, 1, groupBox13.Height - 2);
            e.Graphics.DrawLine(Pens.Black, 1, groupBox13.Height - 2, groupBox13.Width - 2, groupBox13.Height - 2);
            e.Graphics.DrawLine(Pens.Black, groupBox13.Width - 2, 7, groupBox13.Width - 2, groupBox13.Height - 2);
        }

        private void panel8_Resize(object sender, EventArgs e)
        {
            if (panel8.Height > 614)
            {
                customScrollbar1.Visible = false;
                panel8.Width = panel10.Width;
            }
            else
            {
                customScrollbar1.Visible = true;
                panel8.Width = panel10.Width - 2;
            }

            customScrollbar1.Height = panel8.Height;
            ChangeLargeBarLength();
        }

        private void customScrollbar1_Scroll(object sender, EventArgs e)
        {
            panel8.AutoScrollPosition = new Point(0, customScrollbar1.Value);
            panel8.VerticalScroll.Value = customScrollbar1.Value;
            customScrollbar1.Invalidate();
            Application.DoEvents();
            //Debug.WriteLine("vscroll: " + vScrollBar1.Value.ToString() + "  custom: " + customScrollbar1.Value.ToString());
        }

        private void button13_Click(object sender, EventArgs e)
        {
            string hostName = Dns.GetHostName();//本机名   
            //System.Net.IPAddress[] addressList = Dns.GetHostByName(hostName).AddressList;//会警告GetHostByName()已过期，我运行时且只返回了一个IPv4的地址   
            System.Net.IPAddress[] addressList = Dns.GetHostAddresses(hostName);//会返回所有地址，包括IPv4和IPv6   
            foreach (IPAddress ip in addressList)
            {
                string ipstr = ip.ToString();
                if (ipstr.Contains('.'))
                {
                    textBoxIP.Text = ip.ToString();
                }
            }
        }

        private void GetPortNameBtn_Click(object sender, EventArgs e)
        {
            Global.PortName = EnumPort.GetComName()[0]; //对应的上半身
            Global.PortName_7 = EnumPort.GetComName()[1]; //对应下半身
            tbPortname.Text = EnumPort.GetComName()[0] + EnumPort.GetComName()[1];
        }

        private void DevelopMode_CheckedChanged(object sender, EventArgs e)
        {
            if (DevelopMode.Checked)
            {
                SensorID.Enabled = true;
                GeomagnetsimCalibrate.Enabled = true;
                GyroCalibrate.Enabled = true;
                StopCalibrate.Enabled = true;

                Global.CmdMode = "developmode";
            }
        }

        private void NormalMode_CheckedChanged(object sender, EventArgs e)
        {
            if (NormalMode.Checked)
            {
                SensorID.Enabled = false;
                GeomagnetsimCalibrate.Enabled = false;
                GyroCalibrate.Enabled = false;
                StopCalibrate.Enabled = false;
                Global.CmdMode = "normalmode";
            }
        }

        public void SendCmdToSensor(string cmdtype)
        {
            #region  normalmode

            if (cmdtype == "normalmode")
            {
                byte[] cmdmsg = new byte[17];
                cmdmsg[0] = (byte)('c');
                cmdmsg[1] = (byte)('o');
                cmdmsg[2] = (byte)('m');
                cmdmsg[3] = (byte)('m');
                cmdmsg[4] = (byte)('a');
                cmdmsg[5] = (byte)('n');
                cmdmsg[6] = (byte)('d');
                cmdmsg[7] = 0;

                cmdmsg[12] = 0x11; //len
                cmdmsg[13] = 0x11; //len

                cmdmsg[14] = 0xaa; //cmd
                cmdmsg[15] = 0x00; //cmd

                cmdmsg[16] = 0x00; //para  0是正常模式，1是开发者模式 另外还有begin query stop 三种命令
                int len = 0;
                for (int i = 12; i < 17; i++)
                {
                    len += cmdmsg[i];
                }

                byte[] tmp = System.BitConverter.GetBytes(len);
                cmdmsg[8] = tmp[0];
                cmdmsg[9] = tmp[1];
                cmdmsg[10] = tmp[2];
                cmdmsg[11] = tmp[3];
                if (m_port.m_DownPort.IsOpen)
                {
                    m_port.m_DownPort.Write(cmdmsg, 0, 17);
                }
                else
                {
                    MessageBox.Show("normalmode 端口未打开，无法发送消息");
                }

            }
            #endregion
            #region developmode

            else if (cmdtype == "developmode")
            {
                byte[] cmdmsg = new byte[17];
                cmdmsg[0] = (byte)('c');
                cmdmsg[1] = (byte)('o');
                cmdmsg[2] = (byte)('m');
                cmdmsg[3] = (byte)('m');
                cmdmsg[4] = (byte)('a');
                cmdmsg[5] = (byte)('n');
                cmdmsg[6] = (byte)('d');
                cmdmsg[7] = 0;


                cmdmsg[12] = 0x11; //len
                cmdmsg[13] = 0x11; //len

                cmdmsg[14] = 0xaa; //cmd
                cmdmsg[15] = 0x00; //cmd

                cmdmsg[16] = 0x01; //para  0是正常模式，1是开发者模式 
                int len = 0;
                for (int i = 12; i < 17; i++)
                {
                    len += cmdmsg[i];
                }

                byte[] tmp = System.BitConverter.GetBytes(len);
                cmdmsg[8] = tmp[0];
                cmdmsg[9] = tmp[1];
                cmdmsg[10] = tmp[2];
                cmdmsg[11] = tmp[3];

                if (m_port.m_DownPort.IsOpen)
                {
                    m_port.m_DownPort.Write(cmdmsg, 0, 17);
                }
                else
                {
                    MessageBox.Show("developmode 端口未打开，无法接收消息");
                }
            }
            #endregion
            #region clear

            else if (cmdtype == "clear")
            {
                byte[] cmdmsg = new byte[18];
                cmdmsg[0] = (byte)('c');
                cmdmsg[1] = (byte)('o');
                cmdmsg[2] = (byte)('m');
                cmdmsg[3] = (byte)('m');
                cmdmsg[4] = (byte)('a');
                cmdmsg[5] = (byte)('n');
                cmdmsg[6] = (byte)('d');
                cmdmsg[7] = 0;


                cmdmsg[12] = 0x12; //len
                cmdmsg[13] = 0x12; //len

                cmdmsg[14] = 0x07; //cmd
                cmdmsg[15] = 0x00; //cmd

                cmdmsg[16] = (byte)SelectedSensorId;// 0x01;
                cmdmsg[17] = caliitem;   //0x01;  改命令长度为18
                int len = 0;
                for (int i = 12; i < 18; i++)
                {
                    len += cmdmsg[i];
                }

                byte[] tmp = System.BitConverter.GetBytes(len);
                cmdmsg[8] = tmp[0];
                cmdmsg[9] = tmp[1];
                cmdmsg[10] = tmp[2];
                cmdmsg[11] = tmp[3];

                if (m_port.m_DownPort.IsOpen)
                {
                    m_port.m_DownPort.Write(cmdmsg, 0, 18);
                }
                else
                {
                    MessageBox.Show("clear 端口未打开，无法接收消息");
                }

            }
            #endregion
            #region update
            else if (cmdtype == "update")
            {
                byte[] cmdmsg = new byte[52];
                cmdmsg[0] = (byte)('c');
                cmdmsg[1] = (byte)('o');
                cmdmsg[2] = (byte)('m');
                cmdmsg[3] = (byte)('m');
                cmdmsg[4] = (byte)('a');
                cmdmsg[5] = (byte)('n');
                cmdmsg[6] = (byte)('d');
                cmdmsg[7] = 0;


                cmdmsg[12] = 0x34; //len
                cmdmsg[13] = 0x34; //len

                cmdmsg[14] = 0x08; //cmd
                cmdmsg[15] = 0x00; //cmd

                cmdmsg[16] = (byte)SelectedSensorId;// 0x01;
                cmdmsg[17] = caliitem;   //0x01;  改命令长度为18  比 以前的打一个

                cmdmsg[18] = 0;
                cmdmsg[19] = 0;//  uint8_t reserve[2] ; // set to zero

                /***19-51之间的数据都是从采集到的数据计算得到的，得从外部得到才是**ysj+++++++++++**/
                for (int i = 20; i < 52; i++)
                {
                    //CalcCollectData();   //ysj++++
                    cmdmsg[i] = newdata[i - 20];
                }

                int len = 0;
                for (int i = 12; i < 52; i++)
                {
                    len += cmdmsg[i];
                }

                byte[] tmp = System.BitConverter.GetBytes(len);
                cmdmsg[8] = tmp[0];
                cmdmsg[9] = tmp[1];
                cmdmsg[10] = tmp[2];
                cmdmsg[11] = tmp[3];

                if (m_port.m_DownPort.IsOpen)
                {
                    m_port.m_DownPort.Write(cmdmsg, 0, 52);
                }
                else
                {
                    MessageBox.Show("端口未打开64564，无法接收消息");
                }
            }
            #endregion
            #region  collectdata
            if (cmdtype == "collectdata")
            {
                byte[] cmdmsg = new byte[17];
                cmdmsg[0] = (byte)('c');
                cmdmsg[1] = (byte)('o');
                cmdmsg[2] = (byte)('m');
                cmdmsg[3] = (byte)('m');
                cmdmsg[4] = (byte)('a');
                cmdmsg[5] = (byte)('n');
                cmdmsg[6] = (byte)('d');
                cmdmsg[7] = 0;


                cmdmsg[12] = 0x11; //len
                cmdmsg[13] = 0x11; //len

                cmdmsg[14] = 0x02; //cmd
                cmdmsg[15] = 0x00; //cmd

                cmdmsg[16] = (byte)SelectedSensorId;

                int len = 0;
                for (int i = 12; i < 17; i++)
                {
                    len += cmdmsg[i];
                }

                byte[] tmp = System.BitConverter.GetBytes(len);
                cmdmsg[8] = tmp[0];
                cmdmsg[9] = tmp[1];
                cmdmsg[10] = tmp[2];
                cmdmsg[11] = tmp[3];

                if (m_port.m_DownPort.IsOpen)
                {
                    m_port.m_DownPort.Write(cmdmsg, 0, 17);
                }
                else
                {
                    MessageBox.Show("collectdata 端口未打开，无法接收消息");
                }
            }
            #endregion

        }

        private void SendGyroCmd(string cmdtype, int id)
        {
            #region  校验陀螺命令 caligyro
            if (cmdtype == "caligyro")
            {
                byte[] cmdmsg = new byte[18];
                cmdmsg[0] = (byte)('c');
                cmdmsg[1] = (byte)('o');
                cmdmsg[2] = (byte)('m');
                cmdmsg[3] = (byte)('m');
                cmdmsg[4] = (byte)('a');
                cmdmsg[5] = (byte)('n');
                cmdmsg[6] = (byte)('d');
                cmdmsg[7] = 0;


                cmdmsg[12] = 0x12; //len
                cmdmsg[13] = 0x12; //len

                cmdmsg[14] = 0x04; //cmd
                cmdmsg[15] = 0x00; //cmd

                cmdmsg[16] = (byte)id;

                cmdmsg[17] = 0x04;//陀螺的标志

                int len = 0;
                for (int i = 12; i < 18; i++)
                {
                    len += cmdmsg[i];
                }

                byte[] tmp = System.BitConverter.GetBytes(len);
                cmdmsg[8] = tmp[0];
                cmdmsg[9] = tmp[1];
                cmdmsg[10] = tmp[2];
                cmdmsg[11] = tmp[3];

                if (m_port.m_DownPort.IsOpen)
                {
                    m_port.m_DownPort.Write(cmdmsg, 0, 18);
                }
                else
                {
                    MessageBox.Show("caligyro 端口未打开，无法接收消息");
                }
            }
            #endregion
            #region  查询陀螺命令 querygyro
            if (cmdtype == "querygyro")
            {
                byte[] cmdmsg = new byte[17];
                cmdmsg[0] = (byte)('c');
                cmdmsg[1] = (byte)('o');
                cmdmsg[2] = (byte)('m');
                cmdmsg[3] = (byte)('m');
                cmdmsg[4] = (byte)('a');
                cmdmsg[5] = (byte)('n');
                cmdmsg[6] = (byte)('d');
                cmdmsg[7] = 0;


                cmdmsg[12] = 0x11; //len
                cmdmsg[13] = 0x11; //len

                cmdmsg[14] = 0x05; //cmd
                cmdmsg[15] = 0x00; //cmd

                cmdmsg[16] = (byte)id;


                int len = 0;
                for (int i = 12; i < 17; i++)
                {
                    len += cmdmsg[i];
                }

                byte[] tmp = System.BitConverter.GetBytes(len);
                cmdmsg[8] = tmp[0];
                cmdmsg[9] = tmp[1];
                cmdmsg[10] = tmp[2];
                cmdmsg[11] = tmp[3];

                if (m_port.m_DownPort.IsOpen)
                {
                    m_port.m_DownPort.Write(cmdmsg, 0, 17);
                }
                else
                {
                    MessageBox.Show("querygyro 端口未打开，无法接收消息");
                }
            }
            #endregion
            #region  停止陀螺命令 stopgyro
            if (cmdtype == "stopgyro")
            {
                byte[] cmdmsg = new byte[17];
                cmdmsg[0] = (byte)('c');
                cmdmsg[1] = (byte)('o');
                cmdmsg[2] = (byte)('m');
                cmdmsg[3] = (byte)('m');
                cmdmsg[4] = (byte)('a');
                cmdmsg[5] = (byte)('n');
                cmdmsg[6] = (byte)('d');
                cmdmsg[7] = 0;


                cmdmsg[12] = 0x11; //len
                cmdmsg[13] = 0x11; //len

                cmdmsg[14] = 0x06; //cmd
                cmdmsg[15] = 0x00; //cmd

                cmdmsg[16] = (byte)id;


                int len = 0;
                for (int i = 12; i < 17; i++)
                {
                    len += cmdmsg[i];
                }

                byte[] tmp = System.BitConverter.GetBytes(len);
                cmdmsg[8] = tmp[0];
                cmdmsg[9] = tmp[1];
                cmdmsg[10] = tmp[2];
                cmdmsg[11] = tmp[3];

                if (m_port.m_DownPort.IsOpen)
                {
                    m_port.m_DownPort.Write(cmdmsg, 0, 17);
                }
                else
                {
                    MessageBox.Show("stopgyro 端口未打开，无法接收消息");
                }
            }
            #endregion
        }

        private void ClearCalibPara_Click(object sender, EventArgs e)
        {
            if (SensorID.Text.Trim() == string.Empty)
            {
                MessageBox.Show("请选择传感器ID");
                return;
            }
            Global.isCollectMode = false;
            Global.isClearMode = true;
            Global.isUpdateMode = false;

            SendCmdToSensor("clear");
            Global.CollectDataStatus = "正在进入复位模式...";
        }

        private void UpdateCalibPara_Click(object sender, EventArgs e)
        {
            Global.isCollectMode = false;
            Global.isClearMode = false;
            Global.isUpdateMode = true;

            SendCmdToSensor("update");
            Global.CollectDataStatus = "正在进入更新模式...";

            //defineSerialPort.m_MagCollection //需要更新的是？采集的数组， 32位的那个byte 数组暂时不清空，防止更新失败，可以再一次更新。
            defineSerialPort.m_MagCollection.Clear();
            defineSerialPort.m_MagCollectionfordraw.Clear();

        }

        private System.Timers.Timer CollectDataTimer = new System.Timers.Timer(100); //100 hao miao 发送一次取数据的命令
        private System.Timers.Timer UpdateLabelTextTimer = new System.Timers.Timer(100); //100 hao miao 发送一次取数据的命令
        private System.Timers.Timer UpdateGPSTextTimer = new System.Timers.Timer(100);

        private void InitCollectTimer()
        {
            CollectDataTimer.AutoReset = true;
            CollectDataTimer.Enabled = false;
            CollectDataTimer.Elapsed += new System.Timers.ElapsedEventHandler(CollectDataTimer_Elapsed);
        }

        private void InitUpdateTimer()
        {
            UpdateLabelTextTimer.AutoReset = true;
            UpdateLabelTextTimer.Enabled = false;
            UpdateLabelTextTimer.Elapsed += new System.Timers.ElapsedEventHandler(UpdateLabelTextTimer_Elapsed);
        }


        void UpdateLabelTextTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.Invoke(m_UpdateLabelText); //ysj 这里在关闭的时候，有时会报错，原因是程序退出，但是该时间片依然被触发，并执行，结果程序已经退出找不到label。所以会error
        }

        private void InitUpdateGPSTimer()
        {
            UpdateGPSTextTimer.AutoReset = true;
            UpdateGPSTextTimer.Enabled = false;
            UpdateGPSTextTimer.Elapsed += new System.Timers.ElapsedEventHandler(UpdateGPSTextTimer_Elapsed);
        }

        void UpdateGPSTextTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //throw new NotImplementedException();
            this.Invoke(m_UpdateGPSText);

        }

        void CollectDataTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            SendCmdToSensor("collectdata");
        }

        private void StartCollect_Click(object sender, EventArgs e)
        {

            Global.isCollectMode = true;
            Global.isClearMode = false;
            Global.isUpdateMode = false;


            CollectDataTimer.Enabled = true;
            Global.CollectDataStatus = "开始采集数据...";
            Global.StartShowPoint = true;
            Global.isFloorVisible = false;

            

        }

        private void StopCollect_Click(object sender, EventArgs e)
        {
            CollectDataTimer.Enabled = false;
            Global.CollectDataStatus = "停止采集数据并进行计算...";
            Global.StartShowPoint = false;
            Global.isFloorVisible = true;

            if (MagCheckBox.Checked || AccCheckBox.Checked || GyroCheckbox.Checked)
            {
                CalcCollectData(defineSerialPort.m_MagCollection);
            }
            else
            {
                defineSerialPort.m_MagCollection.Clear();
                defineSerialPort.m_MagCollectionfordraw.Clear();
            }

        }

        private byte[] newdata = new byte[32];

        private void CalcCollectData(List<Vector3> lv)
        {
            int n = lv.Count;
            if (n == 0)
            {
                return;
            }

            float[] X = { 0, 0, 0, 0, 0, 0 };
            float[] Y = { 0, 0, 0, 0, 0, 0 };
            float[] Z = { 0, 0, 0, 0, 0, 0 };


            Matrix magX = new Matrix(n, 1);
            Matrix magY = new Matrix(n, 1);
            Matrix magZ = new Matrix(n, 1);
            Matrix Xcen = new Matrix(n, 1);
            Matrix Ycen = new Matrix(n, 1);
            Matrix Zcen = new Matrix(n, 1);

            for (int i = 0; i < n; i++)
            {
                magX[i, 0] = lv[i].X + 100;
                magY[i, 0] = lv[i].Y + 100;
                magZ[i, 0] = lv[i].Z + 100;
            }


            Matrix A = new Matrix(n, 6);
            Matrix R = new Matrix(n, 1);

            for (int i = 0; i < n; i++)
            {
                A[i, 0] = magX[i, 0] * magX[i, 0];
                A[i, 1] = (-2) * magX[i, 0];
                A[i, 2] = magY[i, 0] * magY[i, 0];
                A[i, 3] = (-2) * magY[i, 0];
                A[i, 4] = magZ[i, 0] * magZ[i, 0];
                A[i, 5] = (-2) * magZ[i, 0];
                R[i, 0] = 1;
            }
            Matrix ATrans = Matrix.Transpose(A);
            Matrix tmp1 = ATrans * A;
            Matrix tmp2 = ATrans * R;
            Matrix result = tmp1.SolveWith(tmp2);


            double bX = result[1, 0] / result[0, 0];
            double bY = result[3, 0] / result[2, 0];
            double bZ = result[5, 0] / result[4, 0];

            for (int i = 0; i < n; i++)
            {
                Xcen[i, 0] = (float)(magX[i, 0] - (float)bX);
                Ycen[i, 0] = (float)(magY[i, 0] - (float)bY);
                Zcen[i, 0] = (float)(magZ[i, 0] - (float)bZ);
            }
            Matrix AA = new Matrix(n, 3);
            Matrix RR = new Matrix(n, 1);

            for (int i = 0; i < n; i++)
            {
                AA[i, 0] = Xcen[i, 0] * Xcen[i, 0];
                AA[i, 1] = Ycen[i, 0] * Ycen[i, 0];
                AA[i, 2] = Zcen[i, 0] * Zcen[i, 0];
                RR[i, 0] = 62500;
            }
            Matrix AATrans = Matrix.Transpose(AA);
            Matrix tmp11 = AATrans * AA;
            Matrix tmp22 = AATrans * RR;
            Matrix rst = tmp11.Invert() * tmp22;

            double a = System.Math.Sqrt(rst[0, 0]);
            double b = System.Math.Sqrt(rst[1, 0]);
            double c = System.Math.Sqrt(rst[2, 0]);


            Int16 Xmax = (Int16)((250 / a) + bX - 100);
            Int16 Xmin = (Int16)((-250 / a) + bX - 100);
            Int16 Ymax = (Int16)((250 / b) + bY - 100);
            Int16 Ymin = (Int16)((-250 / b) + bY - 100);
            Int16 Zmax = (Int16)((250 / c) + bZ - 100);
            Int16 Zmin = (Int16)((-250 / c) + bZ - 100);

            Console.WriteLine("X987: " + Xmax + "/" + Xmin + " Y: " + Ymax + "/" + Ymin + " Z: " + Zmax + "/" + Zmin);

            byte[] XmaxArr = BitConverter.GetBytes(Xmax);
            byte[] YmaxArr = BitConverter.GetBytes(Ymax);
            byte[] ZmaxArr = BitConverter.GetBytes(Zmax);
            byte[] XminArr = BitConverter.GetBytes(Xmin);
            byte[] YminArr = BitConverter.GetBytes(Ymin);
            byte[] ZminArr = BitConverter.GetBytes(Zmin);

            newdata[0] = XmaxArr[0];
            newdata[1] = XmaxArr[1];
            newdata[2] = YmaxArr[0];
            newdata[3] = YmaxArr[1];
            newdata[4] = ZmaxArr[0];
            newdata[5] = ZmaxArr[1];
            newdata[6] = XminArr[0];
            newdata[7] = XminArr[1];
            newdata[8] = YminArr[0];
            newdata[9] = YminArr[1];
            newdata[10] = ZminArr[0];
            newdata[11] = ZminArr[1];


            for (int i = 12; i < 32; i++)
            {
                newdata[i] = 0;
            }

            Global.CollectDataStatus = "计算完毕!";

        }

        private byte caliitem = 0x00;

        private void MagCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (MagCheckBox.Checked)
            {
                caliitem = (byte)(caliitem | ((byte)0x01));
            }
            else
            {
                caliitem = (byte)(caliitem & (~(byte)(0x01)));
            }
        }

        private void AccCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            if (AccCheckBox.Checked)
            {
                caliitem = (byte)(caliitem | ((byte)0x02));
            }
            else
            {
                caliitem = (byte)(caliitem & (~(byte)(0x02)));
            }
        }

        private void GyroCheckbox_CheckedChanged(object sender, EventArgs e)
        {
            if (GyroCheckbox.Checked)
            {
                caliitem = (byte)(caliitem | ((byte)0x04));
            }
            else
            {
                caliitem = (byte)(caliitem & (~(byte)(0x04)));
            }
        }

        private int SelectedSensorId = -1;

        private void SensorID_SelectedIndexChanged(object sender, EventArgs e)
        {
            SelectedSensorId = SensorID.SelectedIndex + 1;
            Global.SelectedSensorId = SelectedSensorId;

            Global.isClearMode = false;
            Global.isCollectMode = false;
            Global.isUpdateMode = false;
        }

        private void SendCmdBtn_Click_1(object sender, EventArgs e)
        {
            Global.isClearMode = false;
            Global.isCollectMode = false;
            Global.isUpdateMode = false;

            if (NormalMode.Checked)
            {
                Global.isDevelopMode = false;
                SendCmdToSensor("normalmode");
                Global.CollectDataStatus = "正在进入正常模式...";

            }
            else if (DevelopMode.Checked)
            {
                Global.isDevelopMode = true;
                SendCmdToSensor("developmode");
                Global.CollectDataStatus = "正在进入开发者模式...";
            }
        }

        private void DevelopMode_CheckedChanged_1(object sender, EventArgs e)
        {
            if (DevelopMode.Checked)
            {
                GeomagnetsimCalibrate.Enabled = true;
                GyroCalibrate.Enabled = true;
                StopCalibrate.Enabled = true;

                Global.CmdMode = "developmode";

                //按钮可用
                MagCheckBox.Enabled = true;
                AccCheckBox.Enabled = true;
                GyroCheckbox.Enabled = true;
                ClearCalibPara.Enabled = true;
                UpdateCalibPara.Enabled = true;
                StartCollect.Enabled = true;
                StopCollect.Enabled = true;
                SensorID.Enabled = true;
                UpdateLabelTextTimer.Enabled = true;

            }
        }

        private void NormalMode_CheckedChanged_1(object sender, EventArgs e)
        {
            if (NormalMode.Checked)
            {
                GeomagnetsimCalibrate.Enabled = false;
                GyroCalibrate.Enabled = false;
                StopCalibrate.Enabled = false;
                Global.CmdMode = "normalmode";

                //按钮不可用
                MagCheckBox.Enabled = false;
                AccCheckBox.Enabled = false;
                GyroCheckbox.Enabled = false;
                ClearCalibPara.Enabled = false;
                UpdateCalibPara.Enabled = false;
                StartCollect.Enabled = false;
                StopCollect.Enabled = false;
                SensorID.Enabled = false;
                //UpdateLabelTextTimer.Enabled = false;

            }
        }

        private void tbGPSPort_TextChanged(object sender, EventArgs e)
        {
            Global.GPSPortName = tbGPSPort.Text;
        }

        /************************************************************************/
        /* 以下为临时增加骨骼长度的测试代码，后续maybe会删除                    */
        /************************************************************************/

        private ScreenViewForm localScreenViewForm = null;

        public void SetScreenViewForm(ScreenViewForm opgform)
        {
            localScreenViewForm = opgform;
        }

        private void AddBoneLen_Click(object sender, EventArgs e)
        {
            if (BoneID.SelectedIndex < 0) return;

            localScreenViewForm.SetBoneLen(BoneID.SelectedIndex, Axis, 1.0f);
        }

        private void SubtractBoneLen_Click(object sender, EventArgs e)
        {
            if (BoneID.SelectedIndex < 0) return;

            localScreenViewForm.SetBoneLen(BoneID.SelectedIndex, Axis, -1.0f);
        }

        private int Axis = 0;//默认是X轴

        private void radioButton28_CheckedChanged(object sender, EventArgs e)
        {
            Axis = 0;
        }

        private void radioButton29_CheckedChanged(object sender, EventArgs e)
        {
            Axis = 1;
        }

        private void radioButton30_CheckedChanged(object sender, EventArgs e)
        {
            Axis = 2;
        }

        private void BoneID_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        DateTime StartTime,EndTime;
        private void GyroCalibrate_Click(object sender, EventArgs e)
        {
            ResetCaliStatusFlagArray();
            Global.isStartCaligyroMode = true;

            CaliTimer.Enabled = true;
            QueryTimer.Enabled = true;
            ExitTimer.Enabled = true;

            

        }

        private void ResetCaliStatusFlagArray()
        {
            Global.isStartCaligyroMode = false;
            Global.isCaliinggyroMode = false;
            Global.isCaligyroFinishedMode = false;


            for (int i = 0; i < Global.isGyroCaliingFlagArray.Length; i++)
            {
                Global.isGyroCaliingFlagArray[i] = false;
            }
            for (int i = 0; i < Global.isGyroCaliFinishedFlagArray.Length; i++)
            {
                Global.isGyroCaliFinishedFlagArray[i] = false;
            }
            for (int i = 0; i < Global.isGyroCaliFinishedAndExitFlagArray.Length; i++)
            {
                Global.isGyroCaliFinishedAndExitFlagArray[i] = false;
            }
            for (int i = 0; i < Global.CaliCountArray.Length;i++ )
            {
                Global.CaliCountArray[i] = 0;
            }

        }

        private System.Timers.Timer CaliTimer = new System.Timers.Timer(30); //间隔30毫秒，发送一次开始命令

        private void InitAndStartCaliTimer()
        {
            CaliTimer.AutoReset = true;
            CaliTimer.Enabled = false;
            CaliTimer.Elapsed += new System.Timers.ElapsedEventHandler(CaliTimer_Elapsed);
        }

        void CaliTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            if (Global.isStartCaligyroMode)
            {
                bool isEnd = false;
                for (int i = 0; i < 11; i++)
                {
                    if (!Global.isGyroCaliingFlagArray[i] && Global.CaliCountArray[i] < 6) //5次机会
                    {
                        Global.CaliCountArray[i] += 1;
                        SendGyroCmd("caligyro", i + 1);
                        break;
                    }
                    if (i==10)
                    {
                        isEnd = true; //只有当i == 10 时，才可以说目前是到达最后了。
                    }
                    
                }

                if (isEnd)
                {
                    for (int i = 0; i < Global.CaliCountArray.Length; i++)
                    {
                        Global.CaliCountArray[i] = 0;//清空数据，留给查询timer计数使用
                    }
                    Global.isStartCaligyroMode = false;
                    Global.isCaliinggyroMode = true;
                    StartTime = DateTime.Now;

                }  

            }

        }

        private System.Timers.Timer QueryTimer = new System.Timers.Timer(30); //间隔100毫秒，发送一次查询命令

        private void InitAndStartQueryTimer()
        {
            QueryTimer.AutoReset = true;
            QueryTimer.Enabled = false;
            QueryTimer.Elapsed += new System.Timers.ElapsedEventHandler(QueryTimer_Elapsed);
        }

        void QueryTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            EndTime = DateTime.Now;
            TimeSpan sp = EndTime - StartTime;
            if (sp.Seconds < 8)
            {
                return;
            }

            if (Global.isCaliinggyroMode)
            {
                bool isEnd = false;
                for (int i = 0; i < 11; i++)
                {
                    if (Global.isGyroCaliingFlagArray[i] && !Global.isGyroCaliFinishedFlagArray[i] && Global.CaliCountArray[i] < 10) //10次查询
                    {
                        Global.CaliCountArray[i] += 1;
                        SendGyroCmd("querygyro", i + 1);
                        break;
                    }
                    if (i== 10)
                    {
                        isEnd = true;
                    }
                    
                }


                if (isEnd)
                {

                    Global.isCaliinggyroMode = false;
                    Global.isCaligyroFinishedMode = true;

                    for (int i = 0; i < Global.CaliCountArray.Length; i++)
                    {
                        Global.CaliCountArray[i] = 0;//清空数据，留给exit timer计数使用
                    }
                }

            }

        }
        
        private System.Timers.Timer ExitTimer = new System.Timers.Timer(30); //间隔30毫秒，发送一次退出命令
        
        private void InitAndStartExitTimer()
        {
            ExitTimer.AutoReset = true;
            ExitTimer.Enabled = false;
            ExitTimer.Elapsed += new System.Timers.ElapsedEventHandler(ExitTimer_Elapsed);
        }

        void ExitTimer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            

            if (Global.isCaligyroFinishedMode)
            {
                bool isEnd = false;
                for (int i = 0; i < 11; i++)
                {
                    if (/*!Global.isGyroCaliFinishedAndExitFlagArray[i] &&*/ Global.CaliCountArray[i] < 10)//10次退出消息，如果依然未收到，说明有问题
                    {
                        Global.CaliCountArray[i] += 1;
                        SendGyroCmd("stopgyro", i + 1);
                        break;
                    }
                    if (i == 10)
                    {
                        isEnd = true;
                    }
                }

                //如果所有节点均校准完毕。那么开始发送完成命令
                if (isEnd)
                {
                    Global.isCaligyroFinishedMode = false;
                    Global.CollectDataStatus = "校准陀螺成功";

                    CaliTimer.Enabled = false;
                    QueryTimer.Enabled = false;
                    ExitTimer.Enabled = false;
                }                

            }
        }

        private void SportOrbit_CheckedChanged(object sender, EventArgs e)
        {
            if (SportOrbit.Checked)
            {
                Global.StartShowHeadPoint = true;
            }
            else
            {
                Global.StartShowHeadPoint = false;
            }

            defineSerialPort.m_GPSPointCollection.Clear(); //每次都清空以前的轨迹点的数据
        }

        private void WirelessBtn_Click(object sender, EventArgs e)
        {
            Global.PortName = EnumPort.GetComName()[1]; //对应的上半身
            Global.PortName_7 = EnumPort.GetComName()[0]; //对应下半身
            tbPortname.Text = EnumPort.GetComName()[0] + EnumPort.GetComName()[1];
        } 
        private void textBoxIP_TextChanged(object sender, EventArgs e)
        {

        }

        private void firstbtn_Click(object sender, EventArgs e)
        {
            Global.SaveDataStart = DateTime.Now;
            while(true)
            {
                Global.SaveDataElps = DateTime.Now - Global.SaveDataStart;
                double Telps = Global.SaveDataElps.Ticks / 10000000.0;
                if (Telps > 1) //0.025)
                {
                    return;
                } 

                //if (Global.Save1 && Global.Station0)
                if (radioButton31.Checked && Global.Station0)
                    {
                    firstlab.Text = "0: X(" + Global.HEADRAW.x.ToString() + ") Y(" + Global.HEADRAW.y.ToString() + ") Z(" + Global.HEADRAW.z.ToString()+")";
                    Global.firstpos0 = Global.HEADRAW;
                    return;
                }
                else if (radioButton32.Checked && Global.Station1)
                {
                    firstlab.Text = "1: X("+ Global.HEADRAW.x.ToString() + ") Y(" + Global.HEADRAW.y.ToString() + ") Z(" + Global.HEADRAW.z.ToString() + ")";
                    Global.firstpos1 = Global.HEADRAW;
                    return;
                }
                
                
            }
        }

        private void secondbtn_Click(object sender, EventArgs e)
        {
            Global.SaveDataStart = DateTime.Now;
            while (true)
            {
                Global.SaveDataElps = DateTime.Now - Global.SaveDataStart;
                double Telps = Global.SaveDataElps.Ticks / 10000000.0;
                if (Telps > 1) //0.025)
                {
                    break;
                } 
                if (radioButton31.Checked && Global.Station0)
                {
                    secondlab.Text = "0: X(" + Global.HEADRAW.x.ToString() + ") Y(" + Global.HEADRAW.y.ToString() + ") Z(" + Global.HEADRAW.z.ToString() + ")"; 
                    Global.secondpos0 = Global.HEADRAW;
                    return;
                }
                else if (Global.Save2 && Global.Station1)
                {
                    secondlab.Text = "1: X(" + Global.HEADRAW.x.ToString() + ") Y(" + Global.HEADRAW.y.ToString() + ") Z(" + Global.HEADRAW.z.ToString() + ")";
                    Global.secondpos1 = Global.HEADRAW;
                    return;
                }
            }
        }

        private void thirdbtn_Click(object sender, EventArgs e)
        {
            Global.SaveDataStart = DateTime.Now;
            while (true)
            {
                Global.SaveDataElps = DateTime.Now - Global.SaveDataStart;
                double Telps = Global.SaveDataElps.Ticks / 10000000.0;
                if (Telps > 1) //0.025)
                {
                    break;
                } 
                if (radioButton31.Checked && Global.Station0)
                {
                    thirdlab.Text = "0: X(" + Global.HEADRAW.x.ToString() + ") Y(" + Global.HEADRAW.y.ToString() + ") Z(" + Global.HEADRAW.z.ToString() + ")";
                    Global.thirdpos0 = Global.HEADRAW;
                    return;
                }
                else if (radioButton32.Checked && Global.Station1)
                {
                    thirdlab.Text = "1: X(" + Global.HEADRAW.x.ToString() + ") Y(" + Global.HEADRAW.y.ToString() + ") Z(" + Global.HEADRAW.z.ToString() + ")";
                    Global.thirdpos1 = Global.HEADRAW;
                    return;
                }
            } 
        }

        private void Calibtn_Click(object sender, EventArgs e)
        {
            if (radioButton31.Checked)
            {
                Position.CoordinateTransform(1);
            }
            else if (radioButton32.Checked)
            {
                Position.CoordinateTransform(2);
            }
         //   Position.CoordinateTransform();
        }

        private void button15_Click(object sender, EventArgs e)
        {

        }

        private void label58_Click(object sender, EventArgs e)
        {

        }

        private void groupBox13_Enter(object sender, EventArgs e)
        {

        }
        // X + 1
        private void button16_Click(object sender, EventArgs e)
        {
            Segment HIP = SegmentCollection.arraySegment[10];
            CQuaternion x_left = new CQuaternion(0, 1, 0, 0);
            CQuaternion tmp = new CQuaternion(HIP.Qworld.m_q0, HIP.Qworld.m_q1, HIP.Qworld.m_q2, HIP.Qworld.m_q3);
            CQuaternion Adjust = (HIP.Qworld * x_left) * (tmp.Conjugate());


            SegmentCollection.arraySegment[10].Position.x = SegmentCollection.arraySegment[10].Position.x + 0.01 * Adjust.m_q1;
            SegmentCollection.arraySegment[10].Position.y = SegmentCollection.arraySegment[10].Position.y + 0.01 * Adjust.m_q2;
            SegmentCollection.arraySegment[10].Position.z = SegmentCollection.arraySegment[10].Position.z + 0.01 * Adjust.m_q3;


            SegmentCollection.arraySegment[16].Position.x = SegmentCollection.arraySegment[16].Position.x + 0.01 * Adjust.m_q1;
            SegmentCollection.arraySegment[16].Position.y = SegmentCollection.arraySegment[16].Position.y + 0.01 * Adjust.m_q2;
            SegmentCollection.arraySegment[16].Position.z = SegmentCollection.arraySegment[16].Position.z + 0.01 * Adjust.m_q3;

            SegmentCollection.arraySegment[13].Position.x = SegmentCollection.arraySegment[13].Position.x + 0.01 * Adjust.m_q1;
            SegmentCollection.arraySegment[13].Position.y = SegmentCollection.arraySegment[13].Position.y + 0.01 * Adjust.m_q2;
            SegmentCollection.arraySegment[13].Position.z = SegmentCollection.arraySegment[13].Position.z + 0.01 * Adjust.m_q3;

        }
        // X - 1
        private void button1_Click(object sender, EventArgs e)
        {
            Segment HIP = SegmentCollection.arraySegment[10];
            CQuaternion x_left = new CQuaternion(0, 1, 0, 0);
            CQuaternion tmp = new CQuaternion(HIP.Qworld.m_q0, HIP.Qworld.m_q1, HIP.Qworld.m_q2, HIP.Qworld.m_q3);
            CQuaternion Adjust = (HIP.Qworld * x_left) * (tmp.Conjugate());


            SegmentCollection.arraySegment[10].Position.x = SegmentCollection.arraySegment[10].Position.x - 0.01 * Adjust.m_q1;
            SegmentCollection.arraySegment[10].Position.y = SegmentCollection.arraySegment[10].Position.y - 0.01 * Adjust.m_q2;
            SegmentCollection.arraySegment[10].Position.z = SegmentCollection.arraySegment[10].Position.z - 0.01 * Adjust.m_q3;


            SegmentCollection.arraySegment[16].Position.x = SegmentCollection.arraySegment[16].Position.x - 0.01 * Adjust.m_q1;
            SegmentCollection.arraySegment[16].Position.y = SegmentCollection.arraySegment[16].Position.y - 0.01 * Adjust.m_q2;
            SegmentCollection.arraySegment[16].Position.z = SegmentCollection.arraySegment[16].Position.z - 0.01 * Adjust.m_q3;

            SegmentCollection.arraySegment[13].Position.x = SegmentCollection.arraySegment[13].Position.x - 0.01 * Adjust.m_q1;
            SegmentCollection.arraySegment[13].Position.y = SegmentCollection.arraySegment[13].Position.y - 0.01 * Adjust.m_q2;
            SegmentCollection.arraySegment[13].Position.z = SegmentCollection.arraySegment[13].Position.z - 0.01 * Adjust.m_q3;

        }
        // Y - 1
        private void button14_Click(object sender, EventArgs e)
        {
            Global.LegLength = Global.LegLength - 0.1;
            /*Segment HIP = SegmentCollection.arraySegment[10];
            CQuaternion x_left = new CQuaternion(0, 0, 1, 0);
            CQuaternion tmp = new CQuaternion(HIP.Qworld.m_q0, HIP.Qworld.m_q1, HIP.Qworld.m_q2, HIP.Qworld.m_q3);
            CQuaternion Adjust = (HIP.Qworld * x_left) * (tmp.Conjugate());


            SegmentCollection.arraySegment[10].Position.x = SegmentCollection.arraySegment[10].Position.x - 0.01 * Adjust.m_q1;
            //SegmentCollection.arraySegment[10].Position.y = SegmentCollection.arraySegment[10].Position.y - 0.1;
            SegmentCollection.arraySegment[10].Position.z = SegmentCollection.arraySegment[10].Position.z - 0.01 * Adjust.m_q3;


            SegmentCollection.arraySegment[16].Position.x = SegmentCollection.arraySegment[16].Position.x - 0.01 * Adjust.m_q1;
            SegmentCollection.arraySegment[16].Position.y = SegmentCollection.arraySegment[16].Position.y - 0.1;
            SegmentCollection.arraySegment[16].Position.z = SegmentCollection.arraySegment[16].Position.z - 0.01 * Adjust.m_q3;

            SegmentCollection.arraySegment[13].Position.x = SegmentCollection.arraySegment[13].Position.x - 0.01 * Adjust.m_q1;
            SegmentCollection.arraySegment[13].Position.y = SegmentCollection.arraySegment[13].Position.y - 0.1;
            SegmentCollection.arraySegment[13].Position.z = SegmentCollection.arraySegment[13].Position.z - 0.01 * Adjust.m_q3;
        */
        }
        // Y + 1
        private void button17_Click(object sender, EventArgs e)
        {
            Global.LegLength = Global.LegLength + 0.1;
           /* Segment HIP = SegmentCollection.arraySegment[10];
            CQuaternion x_left = new CQuaternion(0, 0, 1, 0);
            CQuaternion tmp = new CQuaternion(HIP.Qworld.m_q0, HIP.Qworld.m_q1, HIP.Qworld.m_q2, HIP.Qworld.m_q3);
            CQuaternion Adjust = (HIP.Qworld * x_left) * (tmp.Conjugate());


            SegmentCollection.arraySegment[10].Position.x = SegmentCollection.arraySegment[10].Position.x + 0.01 * Adjust.m_q1;
            SegmentCollection.arraySegment[10].Position.y = SegmentCollection.arraySegment[10].Position.y + 0.01;
            SegmentCollection.arraySegment[10].Position.z = SegmentCollection.arraySegment[10].Position.z + 0.01 * Adjust.m_q3;


            SegmentCollection.arraySegment[16].Position.x = SegmentCollection.arraySegment[16].Position.x + 0.01 * Adjust.m_q1;
            SegmentCollection.arraySegment[16].Position.y = SegmentCollection.arraySegment[16].Position.y + 0.01;
            SegmentCollection.arraySegment[16].Position.z = SegmentCollection.arraySegment[16].Position.z + 0.01 * Adjust.m_q3;

            SegmentCollection.arraySegment[13].Position.x = SegmentCollection.arraySegment[13].Position.x + 0.01 * Adjust.m_q1;
            SegmentCollection.arraySegment[13].Position.y = SegmentCollection.arraySegment[13].Position.y + 0.01;
            SegmentCollection.arraySegment[13].Position.z = SegmentCollection.arraySegment[13].Position.z + 0.01 * Adjust.m_q3;*/
        }
        // Z - 1
        private void button15_Click_1(object sender, EventArgs e)
        {
            Segment HIP = SegmentCollection.arraySegment[10];
            CQuaternion x_left = new CQuaternion(0, 0, 0, 1);
            CQuaternion tmp = new CQuaternion(HIP.Qworld.m_q0, HIP.Qworld.m_q1, HIP.Qworld.m_q2, HIP.Qworld.m_q3);
            CQuaternion Adjust = (HIP.Qworld * x_left) * (tmp.Conjugate());


            SegmentCollection.arraySegment[10].Position.x = SegmentCollection.arraySegment[10].Position.x - 0.01 * Adjust.m_q1;
            SegmentCollection.arraySegment[10].Position.y = SegmentCollection.arraySegment[10].Position.y - 0.01 * Adjust.m_q2;
            SegmentCollection.arraySegment[10].Position.z = SegmentCollection.arraySegment[10].Position.z - 0.01 * Adjust.m_q3;


            SegmentCollection.arraySegment[16].Position.x = SegmentCollection.arraySegment[16].Position.x - 0.01 * Adjust.m_q1;
            SegmentCollection.arraySegment[16].Position.y = SegmentCollection.arraySegment[16].Position.y - 0.01 * Adjust.m_q2;
            SegmentCollection.arraySegment[16].Position.z = SegmentCollection.arraySegment[16].Position.z - 0.01 * Adjust.m_q3;

            SegmentCollection.arraySegment[13].Position.x = SegmentCollection.arraySegment[13].Position.x - 0.01 * Adjust.m_q1;
            SegmentCollection.arraySegment[13].Position.y = SegmentCollection.arraySegment[13].Position.y - 0.01 * Adjust.m_q2;
            SegmentCollection.arraySegment[13].Position.z = SegmentCollection.arraySegment[13].Position.z - 0.01 * Adjust.m_q3;
        }
        // Z + 1
        private void button18_Click(object sender, EventArgs e)
        {
            Segment HIP = SegmentCollection.arraySegment[10];
            CQuaternion x_left = new CQuaternion(0, 0, 0, 1);
            CQuaternion tmp = new CQuaternion(HIP.Qworld.m_q0, HIP.Qworld.m_q1, HIP.Qworld.m_q2, HIP.Qworld.m_q3);
            CQuaternion Adjust = (HIP.Qworld * x_left) * (tmp.Conjugate()); 

            SegmentCollection.arraySegment[10].Position.x = SegmentCollection.arraySegment[10].Position.x + 0.01 * Adjust.m_q1;
            SegmentCollection.arraySegment[10].Position.y = SegmentCollection.arraySegment[10].Position.y + 0.01 * Adjust.m_q2;
            SegmentCollection.arraySegment[10].Position.z = SegmentCollection.arraySegment[10].Position.z + 0.01 * Adjust.m_q3;
            
            SegmentCollection.arraySegment[16].Position.x = SegmentCollection.arraySegment[16].Position.x + 0.01 * Adjust.m_q1;
            SegmentCollection.arraySegment[16].Position.y = SegmentCollection.arraySegment[16].Position.y + 0.01 * Adjust.m_q2;
            SegmentCollection.arraySegment[16].Position.z = SegmentCollection.arraySegment[16].Position.z + 0.01 * Adjust.m_q3;

            SegmentCollection.arraySegment[13].Position.x = SegmentCollection.arraySegment[13].Position.x + 0.01 * Adjust.m_q1;
            SegmentCollection.arraySegment[13].Position.y = SegmentCollection.arraySegment[13].Position.y + 0.01 * Adjust.m_q2;
            SegmentCollection.arraySegment[13].Position.z = SegmentCollection.arraySegment[13].Position.z + 0.01 * Adjust.m_q3;
        }

        private void GPSData_CheckedChanged(object sender, EventArgs e)
        {
            if (GPSData.Checked)
            {
                UpdateGPSTextTimer.Enabled = true;
            }
            else
            {
                UpdateGPSTextTimer.Enabled = false;
            }
        }

    /*  private void button19_Click(object sender, EventArgs e)
        {
            Global.Save1 = true;
            Global.Save2 = false;
        }

        private void button20_Click(object sender, EventArgs e)
        {
            Global.Save1 = false;
            Global.Save2 = true;
   
        }
*/
        private void radioButton31_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void textBox16_TextChanged(object sender, EventArgs e)
        {
            Global.height = float.Parse(textBox16.Text);
        }

        private void label64_Click(object sender, EventArgs e)
        {
            if (Global.TransMatAvailable)
            {
                label64.Text = "Coordinate Transformed!";
            }
            else
            {
                label64.Text = "Untransformed coordinate...";
            }
        }

        private void button20_Click(object sender, EventArgs e)
        {
            //read file .
            FileStream fs = new FileStream("basestation_1.txt", FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            for (int line = 0; line < 3; line++)
            {
                string linedata = sr.ReadLine(); 
                Tokens ff = new Tokens(linedata, new char[] { ' ' }); 
                for (int col = 0; col < 4; col++)
                {
                    Global.TransMat1[line, col] = double.Parse(ff.elements[col]);
                } 
            }
            Global.TransMatAvailable = true;
            sr.Close();
            fs.Close();
        }

        private void button21_Click(object sender, EventArgs e)
        {
            FileStream fs = new FileStream("basestation_2.txt", FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            for (int line = 0; line < 3; line++)
            {
                string linedata = sr.ReadLine(); 
                Tokens ff = new Tokens(linedata, new char[] { ' ' }); 
                for (int col = 0; col < 4; col++)
                {
                    Global.TransMat2[line, col] = double.Parse(ff.elements[col]);
                }
                Global.TransMatAvailable = true;
            } 
            sr.Close();
            fs.Close();
        } 
    }
}
