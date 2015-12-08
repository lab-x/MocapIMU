using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Threading;
using System.Timers;
using System.Windows.Forms;

namespace Gwearable
{
    //委托定义
    public delegate void UpdateBtnImage();
    public delegate void UpdateInfoImageex(int[] ration);

    public class defineSerialPort
    {
        public CustomFile cf = new CustomFile();
        ArrayData m_ArrayData = new ArrayData();

        ConcurrentQueue<byte> m_RawQueue = new ConcurrentQueue<byte>(); 
        ConcurrentQueue<byte> m_RawQueue_7 = new ConcurrentQueue<byte>();
        ConcurrentQueue<byte> m_GPSRawQueue = new ConcurrentQueue<byte>();


        ConcurrentQueue<byte[]> m_PointCollection = new ConcurrentQueue<byte[]>(); //32个数组的集合
        public static List<Vector3> m_MagCollection = new List<Vector3>();
        public static List<Vector3> m_MagCollectionfordraw = new List<Vector3>();
        public static List<Vector3> m_GPSPointCollection = new List<Vector3>();//for draw Head Sport Line
        public ConcurrentQueue<string> m_WillSendQueue = new ConcurrentQueue<string>();

        public System.Timers.Timer m_UpdateTime = new System.Timers.Timer(1000);
        public System.Timers.Timer m_SendDataTime = new System.Timers.Timer(200);
        
        public UpdateBtnImage UpdateImage = null;
        public UpdateInfoImageex UpdateInfoImage = null;

        public SerialPort m_Port = new SerialPort();
        public SerialPort m_DownPort = new SerialPort();
        public SerialPort m_GPSPort = new SerialPort();//GPS

        private Thread m_ReceiveDataThread;
        private Thread m_ReceiveDataThread_7;
        private Thread m_ProcessDataThread;
        private Thread m_SendDataThread;
        private Thread m_PositionGeneratorThread;

        bool IsReceivingData = false;
        bool IsReceivingData_7 = false;
        bool IsReceivingGPSData = false;
        bool IsProcessingData = false;
        bool IsSendingData = false;

        public defineSerialPort()
        {

        }
                
        public static int[] FirstRunFlagArray = new int[17] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        //判断节点是否开启
        private bool[] node_openflag_arr = new bool[17] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };

        public void ResetSwitchFlag()
        {
            SegmentCollection.arraySegment[10].Position = new Pos(0, 0.00, 0);
            //new Pos(0, 1.13, 0);
            SegmentCollection.arraySegment[13].Position = new Pos(0, 0.00, 0);
            SegmentCollection.arraySegment[16].Position = new Pos(0, 0.00, 0);
            for (int i = 0; i < 17; i++)
            {
                FirstRunFlagArray[i] = 0;
            }
        }

        private void CalcEulerOut(int ID, int ParentID, ref int isfirsttime)
        {
            int indexID = ID - 1;
            int indexParentID = ParentID - 1;
            if (Global.isCali) //APoseDone() can be set true
            {
                isfirsttime = 1;
            }

            if (0 == isfirsttime)
            {
                Segment.Calibration_horizontal(SegmentCollection.arraySegment[indexID]);
                SegmentCollection.arraySegment[indexID].isFirstTime = false;
            }
            else
            {
                Segment.Gen_QW_horizontal(SegmentCollection.arraySegment[indexID]);
                Segment.Gen_QL(SegmentCollection.arraySegment[indexID], SegmentCollection.arraySegment[indexParentID]);
            }
        }

        private void SendData()
        {
            while (IsSendingData)
            {
                if (m_WillSendQueue.Count > 0/*m_SegmentCollection.isChanged*/)
                {
                    if (m_WillSendQueue.Count > 20)
                    {
                        MessageBox.Show("延迟现象很严重了20帧的延迟 受不了");
                    }
                    string outstr = "";
                    m_WillSendQueue.TryDequeue(out outstr);

                    //当且仅当有远程客户端时，插入数据到m_strQueue，否则！
                    if (Global.isRemote)
                    {
                        Global.m_strQueue.Enqueue(outstr);
                        if (Global.m_strQueue.Count > 1000) //如果大于1000帧的数据了，说明延迟了10s了。主动删除
                        {
                            string deletestr = "";
                            for (int i = 0; i < 1000; i++)
                            {
                                Global.m_strQueue.TryDequeue(out deletestr);
                            }
                        }
                    }

                    //存起来用于本地直播
                    Global.m_strLiveQueue.Enqueue(outstr);

                    //存储用于回放
                    if (Global.isWriteable)
                    {
                        cf.WriteFile(outstr);//同时记录保存的帧数
                        Global.TotalFrameNum++;

                    }
                    //m_SegmentCollection.isChanged = false;
                }
            }
            Console.WriteLine("发送线程主动结束了.");
        }
        public string CalcTotalSendString()
        {
            m_SegmentCollection.Packet_resolver(ArrayData.arr[0], 28);
            CalcEulerOut(1, 2, ref FirstRunFlagArray[0]);
            m_SegmentCollection.Packet_resolver(ArrayData.arr[1], 28);
            CalcEulerOut(2, 3, ref FirstRunFlagArray[1]);
            m_SegmentCollection.Packet_resolver(ArrayData.arr[2], 28);
            CalcEulerOut(3, 4, ref FirstRunFlagArray[2]);
            
            m_SegmentCollection.Packet_resolver(ArrayData.arr[3], 28);
            m_SegmentCollection.Packet_resolver(ArrayData.arr[4], 28);
            
            if (Global.ShouldersNChest)
            { 
                m_SegmentCollection.Packet_resolver(ArrayData.arr[9], 28);
                CalcEulerOut(10, 11, ref FirstRunFlagArray[9]);
                CalcEulerOut(4, 10, ref FirstRunFlagArray[3]); 
                CalcEulerOut(5, 10, ref FirstRunFlagArray[4]);
            
            }
            else 
            {
                CalcEulerOut(4, 11, ref FirstRunFlagArray[3]);
                CalcEulerOut(5, 11, ref FirstRunFlagArray[4]);
            } 
            m_SegmentCollection.Packet_resolver(ArrayData.arr[5], 28);
            CalcEulerOut(6, 5, ref FirstRunFlagArray[5]);
            m_SegmentCollection.Packet_resolver(ArrayData.arr[6], 28);
            CalcEulerOut(7, 6, ref FirstRunFlagArray[6]);
            m_SegmentCollection.Packet_resolver(ArrayData.arr[7], 28);
            CalcEulerOut(8, 7, ref FirstRunFlagArray[7]);
            if (Global.EnableAbsPos)
            {
                m_SegmentCollection.Packet_resolver(ArrayData.arr[8], 28);
                CalcEulerOut(9, 10, ref  FirstRunFlagArray[8]);
            }
            //HIP
            m_SegmentCollection.Packet_resolver(ArrayData.arr[10], 28);
            CalcEulerOut(11, 11, ref FirstRunFlagArray[10]);

            m_SegmentCollection.Packet_resolver(ArrayData.arr[11], 28);
            CalcEulerOut(12, 11, ref FirstRunFlagArray[11]);
            m_SegmentCollection.Packet_resolver(ArrayData.arr[12], 28);
            CalcEulerOut(13, 12, ref FirstRunFlagArray[12]);
            //m_SegmentCollection.Packet_resolver(ArrayData.arr[13], 28);
            //CalcEulerOut(14, 13, ref firsttimearr[13]);
            m_SegmentCollection.Packet_resolver(ArrayData.arr[14], 28);
            CalcEulerOut(15, 11, ref FirstRunFlagArray[14]);
            m_SegmentCollection.Packet_resolver(ArrayData.arr[15], 28);
            CalcEulerOut(16, 15, ref FirstRunFlagArray[15]);
            //m_SegmentCollection.Packet_resolver(ArrayData.arr[16], 28);
            //CalcEulerOut(17, 16, ref firsttimearr[16]);

            
            return m_SegmentCollection.GetSendString();
        }
        static int first = 0;
        static int first_7 = 0;
        static int firstGPS = 0;
        private void RecviveData()
        {
            while (IsReceivingData)
            {
                Thread.Sleep(10);
                try
                {
                    if (0 == first)
                    {
                        first = 1;
                        m_Port.DiscardInBuffer();
                        continue;
                    }
                    if (m_Port.IsOpen)
                    {
                        int bytetoread = m_Port.BytesToRead;
                        if (0 == bytetoread)
                        {
                            continue;
                        }
                        byte[] readBuffer = new byte[bytetoread];
                        m_Port.Read(readBuffer, 0, bytetoread);

                        for (int i = 0; i < bytetoread; i++)
                        {
                            m_RawQueue.Enqueue(readBuffer[i]);
                        }

                        ParseArray(m_RawQueue);
                    }
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show("RecviveData" + ex.Message.ToString());
                    //MessageBox.Show(ex.Message.ToString());
                }
            }
            Console.WriteLine("接收线程主动结束了.");
        }
        private void RecviveData_7()
        {
            while (IsReceivingData_7)
            {
                Thread.Sleep(10);
                try
                {
                    if (0 == first_7)
                    {
                        first_7 = 1;
                        m_DownPort.DiscardInBuffer();
                        continue;
                    }

                    if (m_DownPort.IsOpen)
                    {
                        int bytetoread = m_DownPort.BytesToRead;
                        if (0 == bytetoread)
                        {
                            continue;
                        }
                        byte[] readBuffer = new byte[bytetoread];
                        m_DownPort.Read(readBuffer, 0, bytetoread);

                        for (int i = 0; i < bytetoread; i++)
                        {
                            m_RawQueue_7.Enqueue(readBuffer[i]);
                        }
                        ParseArray(m_RawQueue_7);
                    }
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show("RecviveData_7" + ex.Message.ToString());
                    //MessageBox.Show(ex.Message.ToString());
                }
            }
            Console.WriteLine("RecviveData_7接收线程主动结束了.");
        }
        private void RecviveGPSData()
        {
            while (IsReceivingGPSData)
            {
                Thread.Sleep(10);
                try
                {
                    if (0 == firstGPS)
                    {
                        firstGPS = 1;
                        m_GPSPort.DiscardInBuffer();
                        continue;
                    }

                    if (m_GPSPort.IsOpen)
                    {
                        int bytetoread = m_GPSPort.BytesToRead;
                        if (0 == bytetoread)
                        {
                            /////////////////////////////////////////////////////////////////////////////
                            // IF Time Out Flag = 0
                            TimeSpan Elps = DateTime.Now - Global.AbsPosSigStart;
                            double Telps = Elps.Ticks / 10000000.0;
                           if (Telps > 0.03) //0.025)
                            {
                                Global.ActivateAbsPos = false;
                                Console.WriteLine("DongHead:  NO DATA");
                                Global.NoDataFlag = true;
                            } 
                            //////////////////////////////////////////////////////////////////////////////
                            continue;
                        }
                        byte[] readBuffer = new byte[bytetoread];
                        m_GPSPort.Read(readBuffer, 0, bytetoread);

                        for (int i = 0; i < bytetoread; i++)
                        {
                            m_GPSRawQueue.Enqueue(readBuffer[i]);
                        }
                        ParseGPSArray(m_GPSRawQueue);
                    }
                }
                catch (InvalidOperationException ex)
                {
                    MessageBox.Show("RecviveData_7" + ex.Message.ToString());
                    //MessageBox.Show(ex.Message.ToString());
                }
            }
            Console.WriteLine("RecviveData_7接收线程主动结束了.");
        }

        //int Poly = 0x8408;  == 33800  // CRC冗余校验算式
        public byte[] GetCRC16ByPoly(byte[] Cmd)
        {
            ushort Poly = 0x8408;
            byte[] CRC = new byte[2];
            ushort CRCValue = 0xFFFF;
            for (int i = 0; i < Cmd.Length; i++)
            {
                CRCValue = (ushort)(CRCValue ^ Cmd[i]);
                for (int j = 0; j < 8; j++)
                {
                    if ((CRCValue & 0x0001) != 0)
                    {
                        CRCValue = (ushort)((CRCValue >> 1) ^ Poly);
                    }
                    else
                    {
                        CRCValue = (ushort)(CRCValue >> 1);
                    }
                }
            }
            return BitConverter.GetBytes(CRCValue);
        }

        private SegmentCollection m_SegmentCollection = new SegmentCollection();
        private void InitUpdateTime()
        {
            m_UpdateTime.Enabled = true;
            m_UpdateTime.AutoReset = true;
            m_UpdateTime.Elapsed += new ElapsedEventHandler(m_UpdateTime_Elapsed);
        }
        
        void m_UpdateTime_Elapsed(object sender, ElapsedEventArgs e)
        {
            for (int i = 0; i < 17; i++)
            {
                if (i < 10)
                {
                    ArrayData.ration[i] = ArrayData.num[i] * 100 / 90;
                }
                else
                {
                    ArrayData.ration[i] = ArrayData.num[i] * 100 / 90;
                }

            }

            int workingnode = 0;
            for (int i = 0; i < 17; i++)
            {
                if (0 < ArrayData.ration[i])
                {
                    workingnode++;
                }
            }
            Global.NodeCount = workingnode;

            ArrayData.ResetTimes();
            this.UpdateImage();
            this.UpdateInfoImage(ArrayData.ration);
        }
        private void InitSendDataTime()
        {
            m_SendDataTime.Enabled = true;
            m_SendDataTime.AutoReset = true;
            m_SendDataTime.Elapsed += new ElapsedEventHandler(m_SendDataTime_Elapsed);            
        }

        void m_SendDataTime_Elapsed(object sender, ElapsedEventArgs e)
        {
            for (int i = 0; i < 17; i++)
            {
                if (node_openflag_arr[i])
                {
                    Global.NodeCount++;
                }
            }        
        }

        static int RunTimesFlag = 0;
        private void ProcessData()
        {
            while (IsProcessingData)
            {
                Thread.Sleep(15);//间隔10ms，基本上每秒发送100帧的数据 ..这里有问题，这里每15ms处理一次，很可能丢掉了收到的数据，而处理能力并未饱和。caosw//
                if (0 == RunTimesFlag)
                {
                    RunTimesFlag = 1;
                    InitUpdateTime();
                }
                if (true/*!Global.isCalibrating*/)
                {
                    //m_WillSendQueue.Enqueue(CalcTotalSendString());
                }
                m_WillSendQueue.Enqueue(CalcTotalSendString());
            }
            Console.WriteLine("处理线程主动结束了ProcessData.");
        }
        
        private void ParseArray(ConcurrentQueue<byte> rawDataQueue)
        {
            #region  正常模式的时候

            if (!Global.isDevelopMode)
            {
                while (30 <= rawDataQueue.Count)
                {
                    byte tmp;
                    rawDataQueue.TryPeek(out tmp);//先查找不删除，偏于判断后面的0D0A

                    if ('$' == tmp)
                    {
                        byte _p28 = rawDataQueue.ElementAt(28);
                        byte _p29 = rawDataQueue.ElementAt(29);
                        int id = (int)rawDataQueue.ElementAt(1);

                        if (id == 97)  //节点的开启时，初始化值ID设为a = 97 ，所以这里这样设置，若需要修改联系谢。
                        {
                            rawDataQueue.TryDequeue(out tmp);
                        }
                        Global.CollectDataStatus = "进入正常模式";
                        if (_p28 == 13 && _p29 == 10)
                        {
                            #region
                            switch (id)
                            {
                                case 1:
                                    FillSubDataArray(FindIndex(1), rawDataQueue);
                                    break;
                                case 2:
                                    FillSubDataArray(FindIndex(2), rawDataQueue);
                                    break;
                                case 3:
                                    FillSubDataArray(FindIndex(3), rawDataQueue);
                                    break;
                                case 4:
                                    FillSubDataArray(FindIndex(4), rawDataQueue);
                                    break;
                                case 5:
                                    FillSubDataArray(FindIndex(5), rawDataQueue);
                                    break;
                                case 6:
                                    FillSubDataArray(FindIndex(6), rawDataQueue);
                                    break;
                                case 7:
                                    FillSubDataArray(FindIndex(7), rawDataQueue);
                                    break;
                                case 8:
                                    FillSubDataArray(FindIndex(8), rawDataQueue);
                                    break;
                                case 9:
                                    FillSubDataArray(FindIndex(9), rawDataQueue);
                                    break;
                                case 10:
                                    FillSubDataArray(FindIndex(10), rawDataQueue);
                                    break;
                                case 11:
                                    FillSubDataArray(FindIndex(11), rawDataQueue);
                                    break;
                                case 12:
                                    FillSubDataArray(FindIndex(12), rawDataQueue);
                                    break;
                                case 13:
                                    FillSubDataArray(FindIndex(13), rawDataQueue);
                                    break;
                                case 14:
                                    FillSubDataArray(FindIndex(14), rawDataQueue);
                                    break;
                                case 15:
                                    FillSubDataArray(FindIndex(15), rawDataQueue);
                                    break;
                                case 16:
                                    FillSubDataArray(FindIndex(16), rawDataQueue);
                                    break;
                                case 17:
                                    FillSubDataArray(FindIndex(17), rawDataQueue);
                                    break;
                                default:
                                    rawDataQueue.TryDequeue(out tmp);//前后都对了，但是ID超过了17，是垃圾数据，清除
                                    break;
                            }
                            #endregion
                        }
                        else
                        {
                            rawDataQueue.TryDequeue(out tmp);
                        }
                    }
                    else
                    {
                        rawDataQueue.TryDequeue(out tmp);
                    }

                }
            }

            #endregion
            else
            {
                #region 开发者模式
                if (Global.isDevelopMode && !Global.isClearMode && !Global.isUpdateMode && !Global.isCollectMode && !Global.isStartCaligyroMode && 
                    !Global.isCaliinggyroMode && !Global.isCaligyroFinishedMode)
                {
                    while (rawDataQueue.Count >= 17)
                    {
                        byte tmp;
                        rawDataQueue.TryPeek(out tmp);//立刻删除
                        if ('r' == tmp)
                        {
                            //if it's back value from down sensor
                            byte _v1 = rawDataQueue.ElementAt(1);
                            byte _v2 = rawDataQueue.ElementAt(2);
                            byte _v3 = rawDataQueue.ElementAt(3);
                            byte _v4 = rawDataQueue.ElementAt(4);
                            if (_v1 == 'e' && _v2 == 'p' && _v3 == 'l' && _v4 == 'y')
                            {
                                Console.WriteLine("成功收到了 开发者模式 reply  line 565 字符串");

                                byte _v14 = rawDataQueue.ElementAt(14);

                                if (170 == _v14)
                                {
                                    byte _v15 = rawDataQueue.ElementAt(16);
                                    if (_v15 == 0)
                                    {
                                        Console.WriteLine("收到正常模式0xaa返回消息");
                                        Global.CollectDataStatus = "进入正常模式";

                                        //循环删除17个长度的byte数据
                                        for (int i = 0; i < 17; i++)
                                        {
                                            byte tmpex;
                                            rawDataQueue.TryDequeue(out tmpex);

                                        }

                                    }
                                    else if (_v15 == 1)
                                    {
                                        Console.WriteLine("收到开发者模式0xaa返回消息");
                                        Global.CollectDataStatus = "进入开发者模式";

                                        //循环删除17个长度的byte数据
                                        for (int i = 0; i < 17; i++)
                                        {
                                            byte tmpex;
                                            rawDataQueue.TryDequeue(out tmpex);

                                        }

                                    }
                                    else
                                    {
                                        Console.WriteLine("模式0xaa返回消息错误，需要重发");
                                        Global.CollectDataStatus = "模式0xaa返回消息错误，需要重发";
                                        byte tmpex;
                                        rawDataQueue.TryDequeue(out tmpex);
                                    }

                                }
                                else
                                {
                                    byte tmpex;
                                    rawDataQueue.TryDequeue(out tmpex);
                                }
                            }
                            else
                            {
                                byte tmpex;
                                rawDataQueue.TryDequeue(out tmpex);
                            }
                        }
                        else
                        {
                            byte tmpex;
                            rawDataQueue.TryDequeue(out tmpex);
                        }
                    }
                }
                #endregion
                #region  开发者模式时的复位模式

                if (Global.isClearMode)
                {
                    while (rawDataQueue.Count >= 17)
                    {
                        byte tmp;
                        rawDataQueue.TryPeek(out tmp);//立刻删除
                        if ('r' == tmp)
                        {
                            //if it's back value from down sensor
                            byte _v1 = rawDataQueue.ElementAt(1);
                            byte _v2 = rawDataQueue.ElementAt(2);
                            byte _v3 = rawDataQueue.ElementAt(3);
                            byte _v4 = rawDataQueue.ElementAt(4);
                            if (_v1 == 'e' && _v2 == 'p' && _v3 == 'l' && _v4 == 'y')
                            {
                                Console.WriteLine("成功收到了reply字符串");

                                byte _v14 = rawDataQueue.ElementAt(14);
                                if (7 == _v14)//
                                {
                                    Console.WriteLine("clear 返回消息");
                                    byte _v16 = rawDataQueue.ElementAt(16);
                                    if (Global.SelectedSensorId == _v16)
                                    {
                                        Console.WriteLine("复位返回数据成功****");
                                        Global.CollectDataStatus = "复位返回数据成功";

                                        //循环删除17个长度的byte数据
                                        for (int i = 0; i < 17; i++)
                                        {
                                            byte tmpex;
                                            rawDataQueue.TryDequeue(out tmpex);
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("复位返回数据失败****");
                                        Global.CollectDataStatus = "复位返回数据失败";

                                        rawDataQueue.TryDequeue(out tmp);
                                    }
                                }

                            }
                            else
                            {
                                rawDataQueue.TryDequeue(out tmp); //删除第一个就会引发连锁反应
                            }
                        }
                        else
                        {
                            rawDataQueue.TryDequeue(out tmp);
                        }


                    }
                }

                #endregion
                #region  开发者模式时的更新模式

                if (Global.isUpdateMode)
                {
                    ////

                    while (rawDataQueue.Count >= 18)
                    {
                        byte tmp;
                        rawDataQueue.TryPeek(out tmp);//立刻删除
                        if ('r' == tmp)
                        {
                            //if it's back value from down sensor
                            byte _v1 = rawDataQueue.ElementAt(1);
                            byte _v2 = rawDataQueue.ElementAt(2);
                            byte _v3 = rawDataQueue.ElementAt(3);
                            byte _v4 = rawDataQueue.ElementAt(4);
                            if (_v1 == 'e' && _v2 == 'p' && _v3 == 'l' && _v4 == 'y')
                            {
                                Console.WriteLine("成功收到了reply字符串");

                                byte _v14 = rawDataQueue.ElementAt(14);


                                if (8 == _v14)  //
                                {
                                    Console.WriteLine("update 返回消息");
                                    byte _v16 = rawDataQueue.ElementAt(16);
                                    byte _v17 = rawDataQueue.ElementAt(17);
                                    if (Global.SelectedSensorId == _v16 && _v17 == 0) ///no error
                                    {
                                        Console.WriteLine("update 更新数据成功");
                                        Global.CollectDataStatus = "更新数据成功";
                                        //更新成功之后，很多变量都得赋回原值，方便下次使用

                                        Global.isClearMode = false;  //ysj
                                        Global.isCollectMode = false;
                                        Global.isUpdateMode = false;

                                        //循环删除17个长度的byte数据
                                        for (int i = 0; i < 18; i++)
                                        {
                                            byte tmpex;
                                            rawDataQueue.TryDequeue(out tmpex);

                                        }

                                    }
                                    else
                                    {
                                        Console.WriteLine("update 更新数据失败");
                                        Global.CollectDataStatus = "更新数据失败";

                                        byte tmpex;
                                        rawDataQueue.TryDequeue(out tmpex);

                                    }
                                }

                            }
                            else
                            {
                                rawDataQueue.TryDequeue(out tmp); //删除第一个就会引发连锁反应
                            }
                        }
                        else
                        {
                            rawDataQueue.TryDequeue(out tmp);
                        }


                    }




                    ////
                }

                #endregion
                #region 开发者模式时的采集模式

                if (Global.isCollectMode)
                {
                    //////////////////////////////////////////////////////////////////////////

                    while (rawDataQueue.Count >= 49)
                    {
                        byte tmp;
                        rawDataQueue.TryPeek(out tmp);//立刻删除
                        if ('r' == tmp)
                        {
                            //if it's back value from down sensor
                            byte _v1 = rawDataQueue.ElementAt(1);
                            byte _v2 = rawDataQueue.ElementAt(2);
                            byte _v3 = rawDataQueue.ElementAt(3);
                            byte _v4 = rawDataQueue.ElementAt(4);
                            if (_v1 == 'e' && _v2 == 'p' && _v3 == 'l' && _v4 == 'y')
                            {
                                Console.WriteLine("成功收到了reply字符串");

                                byte _v14 = rawDataQueue.ElementAt(14);
                                if (2 == _v14) 
                                {
                                    byte _v48 = rawDataQueue.ElementAt(48);  //下标从16到47 共计32个byte数据  需要存储，留给萌萌计算，数量不一定 大概几千个左右

                                    if (Global.SelectedSensorId == _v48)
                                    {
                                        Console.WriteLine("正在采集数据.............");
                                        Global.CollectDataStatus = "正在采集数据...";
                                        Vector3 points = new Vector3();
//                                         byte Xfront = rawDataQueue.ElementAt(22);
//                                         byte Xback = rawDataQueue.ElementAt(23);
//                                         byte Yfront = rawDataQueue.ElementAt(24);
//                                         byte Yback = rawDataQueue.ElementAt(25);
//                                         byte Zfront = rawDataQueue.ElementAt(26);
//                                         byte Zback = rawDataQueue.ElementAt(27);
                                        byte Xfront = rawDataQueue.ElementAt(38);
                                        byte Xback = rawDataQueue.ElementAt(39);
                                        byte Yfront = rawDataQueue.ElementAt(40);
                                        byte Yback = rawDataQueue.ElementAt(41);
                                        byte Zfront = rawDataQueue.ElementAt(42);
                                        byte Zback = rawDataQueue.ElementAt(43);

                                        points.X = (Xfront << 8) + Xback;
                                        if (points.X > 32767)
                                        {
                                            points.X -= 65535;
                                            
                                        }
                                        

                                        points.Y = (Yfront << 8) + Yback;
                                        if (points.Y > 32767)
                                        {
                                            points.Y -= 65535;
                                            
                                        }
                                        

                                        points.Z = (Zfront << 8) + Zback;
                                        if (points.Z > 32767)
                                        {
                                            points.Z -= 65535;
                                            
                                        }


                                        m_MagCollection.Add(points);
                                        Vector3 smalldata = new Vector3();
                                        smalldata.X = points.X / 1000;
                                        smalldata.Y = points.Y / 1000;
                                        smalldata.Z = points.Z / 1000;
                                        //points.X /= 1000;
                                        //points.Y /= 1000;
                                        //points.Z /= 1000;                                        
                                        m_MagCollectionfordraw.Add(smalldata); //for si jie hua dian yong 
                                        //Console.WriteLine(points.X.ToString()+"****"+points.Y.ToString()+"****"+points.Z.ToString());

                                        //循环删除17个长度的byte数据
                                        for (int i = 0; i < 49; i++)
                                        {
                                            byte tmpex;
                                            rawDataQueue.TryDequeue(out tmpex);

                                        }


                                    }
                                    else
                                    {
                                        Console.WriteLine("采集数据失败");
                                        Global.CollectDataStatus = "采集数据失败";
                                        byte tmpex;

                                        rawDataQueue.TryDequeue(out tmpex);
                                    }
                                }
                                else
                                {
                                    rawDataQueue.TryDequeue(out tmp); //删除第一个就会引发连锁反应
                                }
                            }
                            else
                            {
                                rawDataQueue.TryDequeue(out tmp); //删除第一个就会引发连锁反应
                            }
                        }
                        else
                        {
                            rawDataQueue.TryDequeue(out tmp);
                        }


                    }





                    ////////
                }
                #endregion
                #region 开发者模式时的开始校验陀螺模式

                if (Global.isStartCaligyroMode)
                {                    
                    while (rawDataQueue.Count >= 18)
                    {
                        byte tmp;
                        rawDataQueue.TryPeek(out tmp);
                        if ('r' == tmp)
                        {
                            byte _v1 = rawDataQueue.ElementAt(1);
                            byte _v2 = rawDataQueue.ElementAt(2);
                            byte _v3 = rawDataQueue.ElementAt(3);
                            byte _v4 = rawDataQueue.ElementAt(4);
                            if (_v1 == 'e' && _v2 == 'p' && _v3 == 'l' && _v4 == 'y')
                            {
                                //Console.WriteLine("成功收到了 开始校验陀螺 模式 reply字符串");
                                byte _v16SensorID = rawDataQueue.ElementAt(16);//Sensor ID
                                #region 设置校验标志数组值                                
                                
                                if (1 == _v16SensorID) //ID 1
                                {
                                    Global.CollectDataStatus = "正在校验陀螺1...";
                                    Global.isGyroCaliingFlagArray[0] = true;
                                    Console.WriteLine("正在校验陀螺1...");
                                    //循环删除18个长度的byte数据
                                    for (int i = 0; i < 18; i++)
                                    {
                                        byte tmpex;
                                        rawDataQueue.TryDequeue(out tmpex);
                                    }
                                } 
                                else if (2 == _v16SensorID)
                                {
                                    Global.CollectDataStatus = "正在校验陀螺2...";
                                    Global.isGyroCaliingFlagArray[1] = true;
                                    Console.WriteLine("正在校验陀螺2...");
                                    //循环删除17个长度的byte数据
                                    for (int i = 0; i < 18; i++)
                                    {
                                        byte tmpex;
                                        rawDataQueue.TryDequeue(out tmpex);
                                    }
                                }
                                else if (3 == _v16SensorID)
                                {
                                    Global.CollectDataStatus = "正在校验陀螺3...";
                                    Global.isGyroCaliingFlagArray[2] = true;
                                    Console.WriteLine("正在校验陀螺3...");
                                    //循环删除17个长度的byte数据
                                    for (int i = 0; i < 18; i++)
                                    {
                                        byte tmpex;
                                        rawDataQueue.TryDequeue(out tmpex);
                                    }
                                }
                                else if (4 == _v16SensorID)
                                {
                                    Global.CollectDataStatus = "正在校验陀螺4...";
                                    Global.isGyroCaliingFlagArray[3] = true;
                                    Console.WriteLine("正在校验陀螺4...");
                                    //循环删除17个长度的byte数据
                                    for (int i = 0; i < 18; i++)
                                    {
                                        byte tmpex;
                                        rawDataQueue.TryDequeue(out tmpex);
                                    }
                                }
                                else if (5 == _v16SensorID)
                                {
                                    Global.CollectDataStatus = "正在校验陀螺5...";
                                    Global.isGyroCaliingFlagArray[4] = true;
                                    Console.WriteLine("正在校验陀螺5...");
                                    //循环删除17个长度的byte数据
                                    for (int i = 0; i < 18; i++)
                                    {
                                        byte tmpex;
                                        rawDataQueue.TryDequeue(out tmpex);
                                    }
                                }
                                else if (6 == _v16SensorID)
                                {
                                    Global.CollectDataStatus = "正在校验陀螺6...";
                                    Global.isGyroCaliingFlagArray[5] = true;
                                    Console.WriteLine("正在校验陀螺6...");
                                    //循环删除17个长度的byte数据
                                    for (int i = 0; i < 18; i++)
                                    {
                                        byte tmpex;
                                        rawDataQueue.TryDequeue(out tmpex);
                                    }
                                }
                                else if (7 == _v16SensorID)
                                {
                                    Global.CollectDataStatus = "正在校验陀螺7...";
                                    Global.isGyroCaliingFlagArray[6] = true;
                                    Console.WriteLine("正在校验陀螺7...");
                                    //循环删除17个长度的byte数据
                                    for (int i = 0; i < 18; i++)
                                    {
                                        byte tmpex;
                                        rawDataQueue.TryDequeue(out tmpex);
                                    }
                                }
                                else if (8 == _v16SensorID)
                                {
                                    Global.CollectDataStatus = "正在校验陀螺8...";
                                    Global.isGyroCaliingFlagArray[7] = true;
                                    Console.WriteLine("正在校验陀螺8...");
                                    //循环删除17个长度的byte数据
                                    for (int i = 0; i < 18; i++)
                                    {
                                        byte tmpex;
                                        rawDataQueue.TryDequeue(out tmpex);
                                    }
                                }
                                else if (9 == _v16SensorID)
                                {
                                    Global.CollectDataStatus = "正在校验陀螺9...";
                                    Global.isGyroCaliingFlagArray[8] = true;
                                    Console.WriteLine("正在校验陀螺9...");
                                    //循环删除17个长度的byte数据
                                    for (int i = 0; i < 18; i++)
                                    {
                                        byte tmpex;
                                        rawDataQueue.TryDequeue(out tmpex);
                                    }
                                }
                                else if (10 == _v16SensorID)
                                {
                                    Global.CollectDataStatus = "正在校验陀螺10...";
                                    Global.isGyroCaliingFlagArray[9] = true;
                                    Console.WriteLine("正在校验陀螺10...");
                                    //循环删除17个长度的byte数据
                                    for (int i = 0; i < 18; i++)
                                    {
                                        byte tmpex;
                                        rawDataQueue.TryDequeue(out tmpex);
                                    }
                                }
                                else if (11 == _v16SensorID)
                                {
                                    Global.CollectDataStatus = "正在校验陀螺11...";
                                    Global.isGyroCaliingFlagArray[10] = true;
                                    Console.WriteLine("正在校验陀螺11...");
                                    //循环删除17个长度的byte数据
                                    for (int i = 0; i < 18; i++)
                                    {
                                        byte tmpex;
                                        rawDataQueue.TryDequeue(out tmpex);
                                    }
                                }
                                else if (12 == _v16SensorID)
                                {
                                    Global.CollectDataStatus = "正在校验陀螺12...";
                                    Global.isGyroCaliingFlagArray[11] = true;
                                    Console.WriteLine("正在校验陀螺12...");
                                    //循环删除17个长度的byte数据
                                    for (int i = 0; i < 18; i++)
                                    {
                                        byte tmpex;
                                        rawDataQueue.TryDequeue(out tmpex);
                                    }
                                }
                                else if (13 == _v16SensorID)
                                {
                                    Global.CollectDataStatus = "正在校验陀螺13...";
                                    Global.isGyroCaliingFlagArray[12] = true;
                                    //循环删除17个长度的byte数据
                                    for (int i = 0; i < 18; i++)
                                    {
                                        byte tmpex;
                                        rawDataQueue.TryDequeue(out tmpex);
                                    }
                                }
                                else if (14 == _v16SensorID)
                                {
                                    Global.CollectDataStatus = "正在校验陀螺14...";
                                    Global.isGyroCaliingFlagArray[13] = true;
                                    //循环删除17个长度的byte数据
                                    for (int i = 0; i < 18; i++)
                                    {
                                        byte tmpex;
                                        rawDataQueue.TryDequeue(out tmpex);
                                    }
                                }
                                else if (15 == _v16SensorID)
                                {
                                    Global.CollectDataStatus = "正在校验陀螺15...";
                                    Global.isGyroCaliingFlagArray[14] = true;
                                    //循环删除17个长度的byte数据
                                    for (int i = 0; i < 18; i++)
                                    {
                                        byte tmpex;
                                        rawDataQueue.TryDequeue(out tmpex);
                                    }
                                }
                                else if (16 == _v16SensorID)
                                {
                                    Global.CollectDataStatus = "正在校验陀螺16...";
                                    Global.isGyroCaliingFlagArray[15] = true;
                                    //循环删除17个长度的byte数据
                                    for (int i = 0; i < 18; i++)
                                    {
                                        byte tmpex;
                                        rawDataQueue.TryDequeue(out tmpex);
                                    }
                                }
                                else if (17 == _v16SensorID)
                                {
                                    Global.CollectDataStatus = "正在校验陀螺17...";
                                    Global.isGyroCaliingFlagArray[16] = true;
                                    //循环删除17个长度的byte数据
                                    for (int i = 0; i < 18; i++)
                                    {
                                        byte tmpex;
                                        rawDataQueue.TryDequeue(out tmpex);
                                    }
                                }
                                else
                                {
                                    rawDataQueue.TryDequeue(out tmp); //删除第一个就会引发连锁反应
                                }
                                #endregion
                            }
                            else
                            {
                                rawDataQueue.TryDequeue(out tmp); //删除第一个就会引发连锁反应
                            }
                        }
                        else
                        {
                            rawDataQueue.TryDequeue(out tmp);
                        }
                    }
                }
                #endregion
                #region 开发者模式时的正在校验陀螺模式

                if (Global.isCaliinggyroMode)
                {
                    while (rawDataQueue.Count >= 19)
                    {
                        byte tmp;
                        rawDataQueue.TryPeek(out tmp);
                        if ('r' == tmp)
                        {
                            byte _v1 = rawDataQueue.ElementAt(1);
                            byte _v2 = rawDataQueue.ElementAt(2);
                            byte _v3 = rawDataQueue.ElementAt(3);
                            byte _v4 = rawDataQueue.ElementAt(4);
                            //byte _v19Status = rawDataQueue.ElementAt(19);
                            if (_v1 == 'e' && _v2 == 'p' && _v3 == 'l' && _v4 == 'y')
                            {
                                //Console.WriteLine("成功收到了正在校验 完毕 reply字符串");
                                byte _v16SensorID = rawDataQueue.ElementAt(16);//Sensor ID
                                byte _v18Status = rawDataQueue.ElementAt(18);  //status value

                                #region 设置校验是否完毕标志数组值

                                if (1 == _v16SensorID) //ID 1
                                {
                                    Global.CollectDataStatus = "校验陀螺1完毕!";
                                    
                                    if (_v18Status!=0) //校验完毕,设置完毕标志
                                    {
                                        Global.isGyroCaliFinishedFlagArray[0] = true;
                                        Console.WriteLine("校验陀螺1完毕!");
                                    }
                                    //循环删除19个长度的byte数据
                                    for (int i = 0; i < 19; i++)
                                    {
                                        byte tmpex;
                                        rawDataQueue.TryDequeue(out tmpex);
                                    }
                                }
                                else if (2 == _v16SensorID)
                                {
                                    Global.CollectDataStatus = "校验陀螺完毕!2...";
                                    
                                    if (_v18Status != 0) //校验完毕,设置完毕标志
                                    {
                                        Global.isGyroCaliFinishedFlagArray[1] = true;
                                        Console.WriteLine("校验陀螺2完毕!");
                                    }
                                    //循环删除19个长度的byte数据
                                    for (int i = 0; i < 19; i++)
                                    {
                                        byte tmpex;
                                        rawDataQueue.TryDequeue(out tmpex);
                                    }
                                }
                                else if (3 == _v16SensorID)
                                {
                                    Global.CollectDataStatus = "校验陀螺完毕!3...";
                                    if (_v18Status != 0) //校验完毕,设置完毕标志
                                    {
                                        Global.isGyroCaliFinishedFlagArray[2] = true;
                                        Console.WriteLine("校验陀螺3完毕!");
                                    }
                                    //循环删除19个长度的byte数据
                                    for (int i = 0; i < 19; i++)
                                    {
                                        byte tmpex;
                                        rawDataQueue.TryDequeue(out tmpex);
                                    }
                                }
                                else if (4 == _v16SensorID)
                                {
                                    Global.CollectDataStatus = "校验陀螺完毕!4...";
                                    if (_v18Status != 0) //校验完毕,设置完毕标志
                                    {
                                        Global.isGyroCaliFinishedFlagArray[3] = true;
                                        Console.WriteLine("校验陀螺4完毕!");
                                    }
                                    //循环删除19个长度的byte数据
                                    for (int i = 0; i < 19; i++)
                                    {
                                        byte tmpex;
                                        rawDataQueue.TryDequeue(out tmpex);
                                    }
                                }
                                else if (5 == _v16SensorID)
                                {
                                    Global.CollectDataStatus = "校验陀螺完毕!5...";
                                    if (_v18Status != 0) //校验完毕,设置完毕标志
                                    {
                                        Global.isGyroCaliFinishedFlagArray[4] = true;
                                        Console.WriteLine("校验陀螺5完毕!");
                                    }
                                    //循环删除19个长度的byte数据
                                    for (int i = 0; i < 19; i++)
                                    {
                                        byte tmpex;
                                        rawDataQueue.TryDequeue(out tmpex);
                                    }
                                }
                                else if (6 == _v16SensorID)
                                {
                                    Global.CollectDataStatus = "校验陀螺完毕!6...";
                                    if (_v18Status != 0) //校验完毕,设置完毕标志
                                    {
                                        Global.isGyroCaliFinishedFlagArray[5] = true;
                                        Console.WriteLine("校验陀螺6完毕!");
                                    }
                                    //循环删除19个长度的byte数据
                                    for (int i = 0; i < 19; i++)
                                    {
                                        byte tmpex;
                                        rawDataQueue.TryDequeue(out tmpex);
                                    }
                                }
                                else if (7 == _v16SensorID)
                                {
                                    Global.CollectDataStatus = "校验陀螺完毕!7...";
                                    if (_v18Status != 0) //校验完毕,设置完毕标志
                                    {
                                        Global.isGyroCaliFinishedFlagArray[6] = true;
                                        Console.WriteLine("校验陀螺7完毕!");
                                    }
                                    //循环删除19个长度的byte数据
                                    for (int i = 0; i < 19; i++)
                                    {
                                        byte tmpex;
                                        rawDataQueue.TryDequeue(out tmpex);
                                    }
                                }
                                else if (8 == _v16SensorID)
                                {
                                    Global.CollectDataStatus = "校验陀螺完毕!8...";
                                    if (_v18Status != 0) //校验完毕,设置完毕标志
                                    {
                                        Global.isGyroCaliFinishedFlagArray[7] = true;
                                        Console.WriteLine("校验陀螺8完毕!");
                                    }
                                    //循环删除19个长度的byte数据
                                    for (int i = 0; i < 19; i++)
                                    {
                                        byte tmpex;
                                        rawDataQueue.TryDequeue(out tmpex);
                                    }
                                }
                                else if (9 == _v16SensorID)
                                {
                                    Global.CollectDataStatus = "校验陀螺完毕!9...";
                                    if (_v18Status != 0) //校验完毕,设置完毕标志
                                    {
                                        Global.isGyroCaliFinishedFlagArray[8] = true;
                                        Console.WriteLine("校验陀螺9完毕!");
                                    }
                                    //循环删除19个长度的byte数据
                                    for (int i = 0; i < 19; i++)
                                    {
                                        byte tmpex;
                                        rawDataQueue.TryDequeue(out tmpex);
                                    }
                                }
                                else if (10 == _v16SensorID)
                                {
                                    Global.CollectDataStatus = "校验陀螺完毕!10...";
                                    if (_v18Status != 0) //校验完毕,设置完毕标志
                                    {
                                        Global.isGyroCaliFinishedFlagArray[9] = true;
                                        Console.WriteLine("校验陀螺10完毕!");
                                    }
                                    //循环删除19个长度的byte数据
                                    for (int i = 0; i < 19; i++)
                                    {
                                        byte tmpex;
                                        rawDataQueue.TryDequeue(out tmpex);
                                    }
                                }
                                else if (11 == _v16SensorID)
                                {
                                    Global.CollectDataStatus = "校验陀螺完毕!11...";
                                    if (_v18Status != 0) //校验完毕,设置完毕标志
                                    {
                                        Global.isGyroCaliFinishedFlagArray[10] = true;
                                        Console.WriteLine("校验陀螺11完毕!");
                                    }
                                    //循环删除19个长度的byte数据
                                    for (int i = 0; i < 19; i++)
                                    {
                                        byte tmpex;
                                        rawDataQueue.TryDequeue(out tmpex);
                                    }
                                }
                                else if (12 == _v16SensorID)
                                {
                                    Global.CollectDataStatus = "校验陀螺完毕!12...";
                                    if (_v18Status != 0) //校验完毕,设置完毕标志
                                    {
                                        Global.isGyroCaliFinishedFlagArray[11] = true;
                                        Console.WriteLine("校验陀螺12完毕!");
                                    }
                                    //循环删除19个长度的byte数据
                                    for (int i = 0; i < 19; i++)
                                    {
                                        byte tmpex;
                                        rawDataQueue.TryDequeue(out tmpex);
                                    }
                                }
                                else if (13 == _v16SensorID)
                                {
                                    Global.CollectDataStatus = "校验陀螺完毕!13...";
                                    if (_v18Status != 0) //校验完毕,设置完毕标志
                                    {
                                        Global.isGyroCaliFinishedFlagArray[12] = true;
                                    }
                                    //循环删除19个长度的byte数据
                                    for (int i = 0; i < 19; i++)
                                    {
                                        byte tmpex;
                                        rawDataQueue.TryDequeue(out tmpex);
                                    }
                                }
                                else if (14 == _v16SensorID)
                                {
                                    Global.CollectDataStatus = "校验陀螺完毕!14...";
                                    if (_v18Status != 0) //校验完毕,设置完毕标志
                                    {
                                        Global.isGyroCaliFinishedFlagArray[13] = true;
                                    }
                                    //循环删除19个长度的byte数据
                                    for (int i = 0; i < 19; i++)
                                    {
                                        byte tmpex;
                                        rawDataQueue.TryDequeue(out tmpex);
                                    }
                                }
                                else if (15 == _v16SensorID)
                                {
                                    Global.CollectDataStatus = "校验陀螺完毕!15...";
                                    if (_v18Status != 0) //校验完毕,设置完毕标志
                                    {
                                        Global.isGyroCaliFinishedFlagArray[14] = true;
                                    }
                                    //循环删除19个长度的byte数据
                                    for (int i = 0; i < 19; i++)
                                    {
                                        byte tmpex;
                                        rawDataQueue.TryDequeue(out tmpex);
                                    }
                                }
                                else if (16 == _v16SensorID)
                                {
                                    Global.CollectDataStatus = "校验陀螺完毕!16...";
                                    if (_v18Status != 0) //校验完毕,设置完毕标志
                                    {
                                        Global.isGyroCaliFinishedFlagArray[15] = true;
                                    }
                                    //循环删除19个长度的byte数据
                                    for (int i = 0; i < 19; i++)
                                    {
                                        byte tmpex;
                                        rawDataQueue.TryDequeue(out tmpex);
                                    }
                                }
                                else if (17 == _v16SensorID)
                                {
                                    Global.CollectDataStatus = "校验陀螺完毕!17...";
                                    if (_v18Status != 0) //校验完毕,设置完毕标志
                                    {
                                        Global.isGyroCaliFinishedFlagArray[16] = true;
                                    }
                                    //循环删除19个长度的byte数据
                                    for (int i = 0; i < 19; i++)
                                    {
                                        byte tmpex;
                                        rawDataQueue.TryDequeue(out tmpex);
                                    }
                                }
                                else
                                {
                                    rawDataQueue.TryDequeue(out tmp); //删除第一个就会引发连锁反应
                                }
                                #endregion
                            }
                            else
                            {
                                rawDataQueue.TryDequeue(out tmp); //删除第一个就会引发连锁反应
                            }
                        }
                        else
                        {
                            rawDataQueue.TryDequeue(out tmp);
                        }
                    }
                }
                #endregion                               
                #region 开发者模式时的退出校验陀螺模式

                if (Global.isCaligyroFinishedMode)
                {
                    while (rawDataQueue.Count >= 19)
                    {
                        byte tmp;
                        rawDataQueue.TryPeek(out tmp);
                        if ('r' == tmp)
                        {
                            byte _v1 = rawDataQueue.ElementAt(1);
                            byte _v2 = rawDataQueue.ElementAt(2);
                            byte _v3 = rawDataQueue.ElementAt(3);
                            byte _v4 = rawDataQueue.ElementAt(4);
                            //byte _v19Status = rawDataQueue.ElementAt(19);
                            if (_v1 == 'e' && _v2 == 'p' && _v3 == 'l' && _v4 == 'y')
                            {
                                //Console.WriteLine("成功收到了校验完毕 reply字符串");
                                byte _v16SensorID = rawDataQueue.ElementAt(16);//Sensor ID
                                byte _v18Status = rawDataQueue.ElementAt(18);  //status value

                                #region 设置校验是否完毕标志数组值

                                if (1 == _v16SensorID) //ID 1
                                {
                                    Global.CollectDataStatus = "退出陀螺1完毕!";
                                    if (_v18Status != 0) //校验完毕,设置完毕标志
                                    {
                                        Global.isGyroCaliFinishedAndExitFlagArray[0] = true;
                                        Console.WriteLine("退出陀螺1完毕!");
                                    }
                                    //循环删除19个长度的byte数据
                                    for (int i = 0; i < 19; i++)
                                    {
                                        byte tmpex;
                                        rawDataQueue.TryDequeue(out tmpex);
                                    }
                                }
                                else if (2 == _v16SensorID)
                                {
                                    Global.CollectDataStatus = "退出校验陀螺2...";
                                    if (_v18Status != 0) //校验完毕,设置完毕标志
                                    {
                                        Global.isGyroCaliFinishedAndExitFlagArray[1] = true;
                                        Console.WriteLine("退出陀螺2完毕!");
                                    }
                                    //循环删除19个长度的byte数据
                                    for (int i = 0; i < 19; i++)
                                    {
                                        byte tmpex;
                                        rawDataQueue.TryDequeue(out tmpex);
                                    }
                                }
                                else if (3 == _v16SensorID)
                                {
                                    Global.CollectDataStatus = "退出校验陀螺3...";
                                    if (_v18Status != 0) //校验完毕,设置完毕标志
                                    {
                                        Global.isGyroCaliFinishedAndExitFlagArray[2] = true;
                                        Console.WriteLine("退出陀螺3完毕!");
                                    }
                                    //循环删除19个长度的byte数据
                                    for (int i = 0; i < 19; i++)
                                    {
                                        byte tmpex;
                                        rawDataQueue.TryDequeue(out tmpex);
                                    }
                                }
                                else if (4 == _v16SensorID)
                                {
                                    Global.CollectDataStatus = "退出校验陀螺4...";
                                    if (_v18Status != 0) //校验完毕,设置完毕标志
                                    {
                                        Global.isGyroCaliFinishedAndExitFlagArray[3] = true;
                                        Console.WriteLine("退出陀螺4完毕!");
                                    }
                                    //循环删除19个长度的byte数据
                                    for (int i = 0; i < 19; i++)
                                    {
                                        byte tmpex;
                                        rawDataQueue.TryDequeue(out tmpex);
                                    }
                                }
                                else if (5 == _v16SensorID)
                                {
                                    Global.CollectDataStatus = "退出校验陀螺5...";
                                    if (_v18Status != 0) //校验完毕,设置完毕标志
                                    {
                                        Global.isGyroCaliFinishedAndExitFlagArray[4] = true;
                                        Console.WriteLine("退出陀螺5完毕!");
                                    }
                                    //循环删除19个长度的byte数据
                                    for (int i = 0; i < 19; i++)
                                    {
                                        byte tmpex;
                                        rawDataQueue.TryDequeue(out tmpex);
                                    }
                                }
                                else if (6 == _v16SensorID)
                                {
                                    Global.CollectDataStatus = "退出校验陀螺6...";
                                    if (_v18Status != 0) //校验完毕,设置完毕标志
                                    {
                                        Global.isGyroCaliFinishedAndExitFlagArray[5] = true;
                                        Console.WriteLine("退出陀螺6完毕!");
                                    }
                                    //循环删除19个长度的byte数据
                                    for (int i = 0; i < 19; i++)
                                    {
                                        byte tmpex;
                                        rawDataQueue.TryDequeue(out tmpex);
                                    }
                                }
                                else if (7 == _v16SensorID)
                                {
                                    Global.CollectDataStatus = "退出校验陀螺7...";
                                    if (_v18Status != 0) //校验完毕,设置完毕标志
                                    {
                                        Global.isGyroCaliFinishedAndExitFlagArray[6] = true;
                                        Console.WriteLine("退出陀螺7完毕!");
                                    }
                                    //循环删除19个长度的byte数据
                                    for (int i = 0; i < 19; i++)
                                    {
                                        byte tmpex;
                                        rawDataQueue.TryDequeue(out tmpex);
                                    }
                                }
                                else if (8 == _v16SensorID)
                                {
                                    Global.CollectDataStatus = "退出校验陀螺8...";
                                    if (_v18Status != 0) //校验完毕,设置完毕标志
                                    {
                                        Global.isGyroCaliFinishedAndExitFlagArray[7] = true;
                                        Console.WriteLine("退出陀螺8完毕!");
                                    }
                                    //循环删除19个长度的byte数据
                                    for (int i = 0; i < 19; i++)
                                    {
                                        byte tmpex;
                                        rawDataQueue.TryDequeue(out tmpex);
                                    }
                                }
                                else if (9 == _v16SensorID)
                                {
                                    Global.CollectDataStatus = "退出校验陀螺9...";
                                    if (_v18Status != 0) //校验完毕,设置完毕标志
                                    {
                                        Global.isGyroCaliFinishedAndExitFlagArray[8] = true;
                                        Console.WriteLine("退出陀螺9完毕!");
                                    }
                                    //循环删除19个长度的byte数据
                                    for (int i = 0; i < 19; i++)
                                    {
                                        byte tmpex;
                                        rawDataQueue.TryDequeue(out tmpex);
                                    }
                                }
                                else if (10 == _v16SensorID)
                                {
                                    Global.CollectDataStatus = "退出校验陀螺10...";
                                    if (_v18Status != 0) //校验完毕,设置完毕标志
                                    {
                                        Global.isGyroCaliFinishedAndExitFlagArray[9] = true;
                                        Console.WriteLine("退出陀螺10完毕!");
                                    }
                                    //循环删除19个长度的byte数据
                                    for (int i = 0; i < 19; i++)
                                    {
                                        byte tmpex;
                                        rawDataQueue.TryDequeue(out tmpex);
                                    }
                                }
                                else if (11 == _v16SensorID)
                                {
                                    Global.CollectDataStatus = "退出校验陀螺11...";
                                    if (_v18Status != 0) //校验完毕,设置完毕标志
                                    {
                                        Global.isGyroCaliFinishedAndExitFlagArray[10] = true;
                                        Console.WriteLine("退出陀螺11完毕!");
                                    }
                                    //循环删除19个长度的byte数据
                                    for (int i = 0; i < 19; i++)
                                    {
                                        byte tmpex;
                                        rawDataQueue.TryDequeue(out tmpex);
                                    }
                                }
                                else if (12 == _v16SensorID)
                                {
                                    Global.CollectDataStatus = "退出校验陀螺12...";
                                    if (_v18Status != 0) //校验完毕,设置完毕标志
                                    {
                                        Global.isGyroCaliFinishedAndExitFlagArray[11] = true;
                                    }
                                    //循环删除19个长度的byte数据
                                    for (int i = 0; i < 19; i++)
                                    {
                                        byte tmpex;
                                        rawDataQueue.TryDequeue(out tmpex);
                                    }
                                }
                                else if (13 == _v16SensorID)
                                {
                                    Global.CollectDataStatus = "退出校验陀螺13...";
                                    if (_v18Status != 0) //校验完毕,设置完毕标志
                                    {
                                        Global.isGyroCaliFinishedAndExitFlagArray[12] = true;
                                    }
                                    //循环删除19个长度的byte数据
                                    for (int i = 0; i < 19; i++)
                                    {
                                        byte tmpex;
                                        rawDataQueue.TryDequeue(out tmpex);
                                    }
                                }
                                else if (14 == _v16SensorID)
                                {
                                    Global.CollectDataStatus = "退出校验陀螺14...";
                                    if (_v18Status != 0) //校验完毕,设置完毕标志
                                    {
                                        Global.isGyroCaliFinishedAndExitFlagArray[13] = true;
                                    }
                                    //循环删除19个长度的byte数据
                                    for (int i = 0; i < 19; i++)
                                    {
                                        byte tmpex;
                                        rawDataQueue.TryDequeue(out tmpex);
                                    }
                                }
                                else if (15 == _v16SensorID)
                                {
                                    Global.CollectDataStatus = "退出校验陀螺15...";
                                    if (_v18Status != 0) //校验完毕,设置完毕标志
                                    {
                                        Global.isGyroCaliFinishedAndExitFlagArray[14] = true;
                                    }
                                    //循环删除19个长度的byte数据
                                    for (int i = 0; i < 19; i++)
                                    {
                                        byte tmpex;
                                        rawDataQueue.TryDequeue(out tmpex);
                                    }
                                }
                                else if (16 == _v16SensorID)
                                {
                                    Global.CollectDataStatus = "退出校验陀螺16...";
                                    if (_v18Status != 0) //校验完毕,设置完毕标志
                                    {
                                        Global.isGyroCaliFinishedAndExitFlagArray[15] = true;
                                    }
                                    //循环删除19个长度的byte数据
                                    for (int i = 0; i < 19; i++)
                                    {
                                        byte tmpex;
                                        rawDataQueue.TryDequeue(out tmpex);
                                    }
                                }
                                else if (17 == _v16SensorID)
                                {
                                    Global.CollectDataStatus = "退出校验陀螺17...";
                                    if (_v18Status != 0) //校验完毕,设置完毕标志
                                    {
                                        Global.isGyroCaliFinishedAndExitFlagArray[16] = true;
                                    }
                                    //循环删除19个长度的byte数据
                                    for (int i = 0; i < 19; i++)
                                    {
                                        byte tmpex;
                                        rawDataQueue.TryDequeue(out tmpex);
                                    }
                                }
                                else
                                {
                                    rawDataQueue.TryDequeue(out tmp); //删除第一个就会引发连锁反应
                                }
                                #endregion
                            }
                            else
                            {
                                rawDataQueue.TryDequeue(out tmp); //删除第一个就会引发连锁反应
                            }
                        }
                        else
                        {
                            rawDataQueue.TryDequeue(out tmp);
                        }
                    }
                }
                #endregion
            }
        }

        private void ParseGPSArray(ConcurrentQueue<byte> rawGPSDataQueue)
        {
            while (32 <= rawGPSDataQueue.Count)
            {
                byte tmp;
                rawGPSDataQueue.TryPeek(out tmp);

                if ('P' == tmp) 
                {
                    byte _va1 = rawGPSDataQueue.ElementAt(1); 

                    if (_va1 == 'T')
                    {
                        Pos PreHEAD = new Pos(Global.HEAD.x,Global.HEAD.y,Global.HEAD.z);

                        byte[] GPSRawData = new byte[32];
                        for (int i = 0; i < 32;i++ )
                        {
                            GPSRawData[i] = rawGPSDataQueue.ElementAt(i); //校验和暂时不加了，后续再添加吧
                        }
                        byte b1 = 0x40;
                        byte b2 = GPSRawData[2];
                        byte b3 = (byte)(b1 & b2);
                        byte b4 = 0x10;
                        byte b5 = (byte)(b4 & b2);
                        Int32 Xpos = 0;
                        Int32 Ypos = 0;
                        Int32 Zpos = 0;
                        int refID = 0;
                        if ((b3 == b1) && (b5 == 0))
                        {
                            refID = 1; 
                            Global.Station1 = false;
                            Global.Station0 = true;
                            Global.NoDataFlag = false;
                            Xpos = BitConverter.ToInt32(GPSRawData, 3);
                            Ypos = BitConverter.ToInt32(GPSRawData, 7);
                            Zpos = BitConverter.ToInt32(GPSRawData, 11);
                            Global.HEADRAW = new Pos(Xpos / 10000.0f, Ypos / 10000.0f, Zpos / 10000.0f);
                            
                            //Console.WriteLine("0：  " + Global.HEADRAW.ToString());
                        } 
                        else if ((b3 == 0x00) && (b5 == 0))
                        {
                            refID = 2;
                            Global.Station1 = true;
                            Global.Station0 = false;
                            Global.NoDataFlag = false;
                            Xpos = BitConverter.ToInt32(GPSRawData, 3);
                            Ypos = BitConverter.ToInt32(GPSRawData, 7);
                            Zpos = BitConverter.ToInt32(GPSRawData, 11);
                            Global.HEADRAW = new Pos(Xpos / 10000.0f, Ypos / 10000.0f, Zpos / 10000.0f);
                            //Console.WriteLine("1：  " + Global.HEADRAW.ToString());
                        }
                        else
                        { 
                            //Console.WriteLine("????" );
                            //Global.NoDataFlag = true;
                            //Global.Station1 = false;
                            //Global.Station0 = false;
                            //return;
                        }


#region TransMat Available
                        if (Global.TransMatAvailable)
                        {
                            Matrix raw = new Matrix(4, 1);
                            raw[0, 0] = Xpos / 10000.0f;
                            raw[1, 0] = Ypos / 10000.0f;
                            raw[2, 0] = Zpos / 10000.0f;
                            raw[3, 0] = 1;
                            if (refID == 1)  //&& Global.Station1 == false)  // 塔号
                            {
                                Global.Station0 = true;
                                Global.Station1 = false;
                                Matrix head = Global.TransMat1 * raw;
                                Global.HEAD = new Pos(-head[2, 0], head[1, 0], head[0, 0]);
                                //Global.HEAD = new Pos(-head[0, 0], head[1, 0], -head[2, 0]);
                                Console.WriteLine("Dong." + "MAPPED:  " + Global.HEAD.ToString());
                            }
                            else if (refID == 2)  //&& Global.Station0 == false) // 塔号
                            {
                                Global.Station1 = true;
                                Global.Station0 = false;
                                Matrix head = Global.TransMat2 * raw;
                                Global.HEAD = new Pos(-head[2, 0], head[1, 0], head[0, 0]);
                                //Global.HEAD = new Pos(-head[0, 0], head[1, 0], -head[2, 0]);
                                Console.WriteLine("Dong." + "MAPPED:  " + Global.HEAD.ToString());
                            }
                        } 
#endregion

#region TransMat UnAvailable
                        else if(refID != 0)
                        {
                            Global.HEAD = Global.HEADRAW;
                            Console.WriteLine("LightHouse-" + refID + ":    " + Global.HEADRAW.ToString());
                        } 
#endregion

                        
                        //NOTE:此处得到的xyz pos值是0.1毫秒级别，四元数是扩大10000倍之后的整数。使用的时候需要作相应的除法。
                       
                      #region HEAD Position De-Quiver
/*                          if (System.Math.Abs(PreHEAD.x - Global.HEAD.x) > 0 && System.Math.Abs(PreHEAD.x - Global.HEAD.x) < 0.01)
                        {
                            Global.HEAD.x = PreHEAD.x;
                        }
                        if (System.Math.Abs(PreHEAD.y - Global.HEAD.y) > 0 && System.Math.Abs(PreHEAD.y - Global.HEAD.y) < 0.01)
                        {
                            Global.HEAD.y = PreHEAD.y;
                        }
                        if (System.Math.Abs(PreHEAD.z - Global.HEAD.z) > 0 && System.Math.Abs(PreHEAD.z - Global.HEAD.z) < 0.01)
                        {
                            Global.HEAD.z = PreHEAD.z;
                        }
                        Global.HEAD.x = (3 * PreHEAD.x + Global.HEAD.x) / 4;
                        Global.HEAD.y = (3 * PreHEAD.y + Global.HEAD.y) / 4;
                        Global.HEAD.z = (3 * PreHEAD.z + Global.HEAD.z) / 4;
  */                     
                      #endregion
                        
                        m_GPSPointCollection.Add(new Vector3(Xpos/10000.0f, Ypos/10000.0f, Zpos/10000.0f)); //米 ---级别的精度值


                        Int32 XQuat = BitConverter.ToInt32(GPSRawData, 15);
                        Int32 YQuat = BitConverter.ToInt32(GPSRawData, 19);
                        Int32 ZQuat = BitConverter.ToInt32(GPSRawData, 23);
                        Int32 WQuat = BitConverter.ToInt32(GPSRawData, 27);

                        //四元数赋值
                        SegmentCollection.arraySegment[8].raw.Q.m_q0 = -XQuat / 10000.0f;
                        SegmentCollection.arraySegment[8].raw.Q.m_q1 = YQuat / 10000.0f;
                        SegmentCollection.arraySegment[8].raw.Q.m_q3 = ZQuat / 10000.0f;
                        SegmentCollection.arraySegment[8].raw.Q.m_q2 = WQuat / 10000.0f;
                        Global.AbsPosSigStart = DateTime.Now;
                        Global.ActivateAbsPos = true;
                        //Console.WriteLine("Dong.Head: "+ Global.HEAD.ToString());
                        for (int i = 0; i < 32;i++ )
                        {
                            rawGPSDataQueue.TryDequeue(out tmp);
                        }
                    }
                    else
                    {
                        rawGPSDataQueue.TryDequeue(out tmp);
                    }
                }
                else
                {
                    rawGPSDataQueue.TryDequeue(out tmp);
                }
            }
        }
        
        //针对长度为32的解析函数
        private void ParseArray32(ConcurrentQueue<byte> rawDataQueue)
        {
            while (32 <= rawDataQueue.Count)
            {
                byte tmp;
                rawDataQueue.TryPeek(out tmp);//先查找不删除，偏于判断后面的0D0A

                if ('$' == tmp)
                {
                    byte _p30 = rawDataQueue.ElementAt(30);
                    byte _p31 = rawDataQueue.ElementAt(31);
                    int id = (int)rawDataQueue.ElementAt(1);

                    if (id == 97)  //节点的开启时，初始化值ID设为a = 97 ，所以这里这样设置，若需要修改联系谢。
                    {
                        rawDataQueue.TryDequeue(out tmp);
                    }

                    if (_p30 == 13 && _p31 == 10)
                    {
                        #region
                        switch (id)
                        {
                            case 1:
                                FillSubDataArray(FindIndex(1), rawDataQueue);
                                break;
                            case 2:
                                FillSubDataArray(FindIndex(2), rawDataQueue);
                                break;
                            case 3:
                                FillSubDataArray(FindIndex(3), rawDataQueue);
                                break;
                            case 4:
                                FillSubDataArray(FindIndex(4), rawDataQueue);
                                break;
                            case 5:
                                FillSubDataArray(FindIndex(5), rawDataQueue);
                                break;
                            case 6:
                                FillSubDataArray(FindIndex(6), rawDataQueue);
                                break;
                            case 7:
                                FillSubDataArray(FindIndex(7), rawDataQueue);
                                break;
                            case 8:
                                FillSubDataArray(FindIndex(8), rawDataQueue);
                                break;
                            case 9:
                                FillSubDataArray(FindIndex(9), rawDataQueue);
                                break;
                            case 10:
                                FillSubDataArray(FindIndex(10), rawDataQueue);
                                break;
                            case 11:
                                FillSubDataArray(FindIndex(11), rawDataQueue);
                                break;
                            case 12:
                                FillSubDataArray(FindIndex(12), rawDataQueue);
                                break;
                            case 13:
                                FillSubDataArray(FindIndex(13), rawDataQueue);
                                break;
                            case 14:
                                FillSubDataArray(FindIndex(14), rawDataQueue);
                                break;
                            case 15:
                                FillSubDataArray(FindIndex(15), rawDataQueue);
                                break;
                            case 16:
                                FillSubDataArray(FindIndex(16), rawDataQueue);
                                break;
                            case 17:
                                FillSubDataArray(FindIndex(17), rawDataQueue);
                                break;
                            default:
                                rawDataQueue.TryDequeue(out tmp);//前后都对了，但是ID超过了17，是垃圾数据，清除
                                break;
                        }
                        #endregion
                    }
                    else
                    {
                        rawDataQueue.TryDequeue(out tmp);
                    }
                }
                else
                {
                    rawDataQueue.TryDequeue(out tmp);
                }

            }
        }
        
        private void FillSubDataArray(int index, ConcurrentQueue<byte> rawDataQueue)
        {
            if (-1 == index)
            {
                return;
            }
            byte subtmp;
            for (int j = 0; j < 28; j++)
            {
                if (rawDataQueue.TryDequeue(out subtmp))
                {
                    ArrayData.arr[index][j] = subtmp;
                }
            }
            ArrayData.num[index]++;
            rawDataQueue.TryDequeue(out subtmp);
            rawDataQueue.TryDequeue(out subtmp);
        }
        
        public static int FindIndex(int ID)
        {
            for (int i = 0; i < 17; i++)
            {
                if (ID == Global.IDSort[i])
                {
                    return i;
                }
            }
            return -1;//未寻找到ID，则默认是-1；不处理  
        }
        
        public void InitSerialPort(SerialPort port, string portname)
        {
            port.PortName = portname;
            port.BaudRate = 115200;       //波特率
            port.Parity = Parity.None;    //无奇偶校验位
            port.StopBits = StopBits.One; //1个停止位
            port.DataBits = 8;            //数据位
        }
        
        public bool OpenPort(SerialPort port)
        {
            try
            {
                port.Open();
                return true;
            }
            catch (SystemException ex)
            {
                MessageBox.Show(port.PortName+"端口，未能成功打开");
                //MessageBox.Show(ex.Message);
            }
            return false;
        }

        //废弃的函数
        public bool OpenPort()
        {
            try
            {
                m_DownPort.Open();
                m_Port.Open();
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.Message);
            }

            if (m_Port.IsOpen && m_DownPort.IsOpen)
            {
                Console.WriteLine("the port and port_7 is opened");
                StartAllThread();
                return true;
            }
            else
            {
                Console.WriteLine("failure to open the port");
            }

            return false;
        }

        /************************************************************************/
        /* INTERFACE TO ABSOLUTE POSITION AIDING SYSTEM    */
        /************************************************************************/


        public void StartAllThread()
        {
            IsReceivingData = true;
            IsReceivingData_7 = true;
            IsProcessingData = true;
            IsSendingData = true;

            IsReceivingGPSData = true;

            if (m_Port.IsOpen)
            {
                m_ReceiveDataThread = new Thread(RecviveData);
                m_ReceiveDataThread.IsBackground = true;
                m_ReceiveDataThread.Start();
            }

            if (m_DownPort.IsOpen)
            {
                m_ReceiveDataThread_7 = new Thread(RecviveData_7);
                m_ReceiveDataThread_7.IsBackground = true;
                m_ReceiveDataThread_7.Start();
            }

            if (m_GPSPort.IsOpen)
            {
                m_PositionGeneratorThread = new Thread(RecviveGPSData);
                m_PositionGeneratorThread.IsBackground = true;
                m_PositionGeneratorThread.Start();
            }

            m_ProcessDataThread = new Thread(ProcessData);
            m_ProcessDataThread.IsBackground = true;
            m_ProcessDataThread.Start();
            m_SendDataThread = new Thread(SendData);
            m_SendDataThread.IsBackground = true;
            m_SendDataThread.Start();

        }
        public bool ClosePort()
        {
            if (Global.isWriteable)
            {
                //意外断开连接的情况，那么我结束文件操作
                cf.CloseFile();
                Global.isWriteable = false;
            }

            IsProcessingData = false;
            IsSendingData = false;
            IsReceivingData = false;
            IsReceivingData_7 = false;
            IsReceivingGPSData = false;

            try
            {
                //假如try catch 防止板子断电时，关闭异常
                if (m_Port != null)
                {
                    m_Port.Close();
                }
                if (m_DownPort != null)
                {
                    m_DownPort.Close();
                }

                if (m_GPSPort!= null)
                {
                    m_GPSPort.Close();
                }

                return true;
            }
            catch (SystemException ex)
            {
                Console.WriteLine("the port is closed error!");
                MessageBox.Show(ex.Message);
            }

            if (!m_Port.IsOpen)
            {
                Console.WriteLine("the port is already closed");
                return true;
            }

            return false;
        }
        public void KillTimer()
        {
            m_UpdateTime.Stop();
            m_SendDataTime.Stop();
            m_SendDataTime.Close();
            m_UpdateTime.Close();
        }
    }
}
