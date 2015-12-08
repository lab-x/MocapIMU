using System;

namespace Gwearable
{
    public class Segment
    {
        public int ID;
        public int parentID;
        public SensorData raw;

        //Calibration
        public CQuaternion Q_init;
        public CQuaternion Q_initT;
        public bool CaliT;
        public bool TDone;
        public CQuaternion Q_initA;
        public bool CaliA;
        public bool ADone;
        public CQuaternion Q_initS;
        public bool CaliS;
        public bool SDone;
        public CQuaternion Q_initX;
        public bool CaliX;
        public bool XDone;
        public Matrix CaliMat;
        public Matrix invCaliMat;


        public CQuaternion Q_Cali;
        public CQuaternion invQ_Cali;
        //
        public CQuaternion QW;
        public CQuaternion QL;
        public Euler Eul_Out;
        public CQuaternion Qworld;
        public CQuaternion Qlocal;
        public Pos Position;
        public bool isFirstTime;
        //smooth
        public CQuaternion PrevRotQ;
        //Jump 
        public CQuaternion AccCvt;
        public int AccExceed;
        //Timer
        public DateTime TimerStart;
        public DateTime TimerStop;
         
        public Segment(int self_id, int parent_id)
        {
            ID = self_id;
            parentID = parent_id;
            raw = new SensorData();

            Q_init = new CQuaternion();
            Q_initT = new CQuaternion();
            Q_initA = new CQuaternion();
            Q_initS = new CQuaternion();

            Q_Cali = new CQuaternion();
            invQ_Cali = new CQuaternion();
            CaliT = false;
            CaliA = false;
            CaliS = false;
            CaliX = false;
            CaliMat = new Matrix(4, 4);
            invCaliMat = new Matrix(4, 4);
            TDone = false;
            ADone = false;
            SDone = false;
            XDone = false;


            QW = new CQuaternion();
            QL = new CQuaternion();
            Eul_Out = new Euler();
            Qworld = new CQuaternion();
            Qlocal = new CQuaternion();
            Position = new Pos();

            PrevRotQ = new CQuaternion();
            AccCvt = new CQuaternion();
            AccExceed = 0;

            TimerStart = DateTime.Now;
            TimerStop = DateTime.Now;
            isFirstTime = true;

            //TPoseCalibrateTimes = 0;
            //APoseCalibrateTimes = 0;
        }

        public static void Calibration_horizontal(Segment S)    //FOR ARMS 
        {
            //ModelsForm.Calibrate();
            // Calibrate(S);
            TPoseCalibrate(S);
            XPoseCalibrate(S);
            APoseCalibrate(S);
            SPoseCalibrate(S);
            APoseDone();                          // All Nodes' Calibration's Done !
            
            S.PrevRotQ = new CQuaternion(S.raw.Q.m_q0, S.raw.Q.m_q1, S.raw.Q.m_q2, S.raw.Q.m_q3); //T A Pose都会调用
        
        }

       private static void TPoseCalibrate(Segment S)
        {
            #region -T-Pose
            if (!S.TDone && Global.CanTpose)  //如果这个节点的Tpose还没有完成
            {
                // Start Timer and T-Pose Calibrate  
                //第一次进入该节点的T-Pose， 开始计时器，CaliT置位
                if (S.CaliT == false)
                {
                    S.TimerStart = DateTime.Now;
                    S.TimerStop = DateTime.Now;
                    S.CaliT = true;
                    Console.WriteLine(S.ID + "   T ");
                }
                // 正式开始记录T-Pose数据，如果Timer超过5s,完成该节点的T-pose数据读取,S.TDone置位.
                // GlobalTDoneCnt++. 当cnt加到11的时候，给一个Global.APoseStart置位
                if (S.CaliT == true)
                {
                    //CQuaternion tmp = new CQuaternion(S.raw.Q.m_q0, S.raw.Q.m_q1, S.raw.Q.m_q2, S.raw.Q.m_q3);
                    //S.Q_initT = tmp.Conjugate();
                    if (S.ID < 12) //上半身的ID
                        S.Q_init = new CQuaternion(S.raw.Q.m_q0, -S.raw.Q.m_q1, -S.raw.Q.m_q2, -S.raw.Q.m_q3);
                    S.Q_initT = new CQuaternion(S.raw.Q.m_q0, S.raw.Q.m_q1, S.raw.Q.m_q2, S.raw.Q.m_q3);

                    if (double.IsNaN(S.Q_initT.m_q0))
                    {
                        S.Q_initT = new CQuaternion(1, 0, 0, 0);
                    }
                    S.TimerStop = DateTime.Now;
                    TimeSpan duration = S.TimerStop - S.TimerStart;
                    double ds = duration.Ticks / 10000000.0;
                    if (ds > 4)
                    {
                        S.TDone = true;
                        Global.TDoneCnt++;
                        if (Global.TDoneCnt > 12)
                        {
                            Global.XposeAvail = true;
                            Global.TDoneCnt = 0;
                            Global.TposeDone = true;
                        }
                    }
                }
            } 
#endregion
        }

        private static void XPoseCalibrate(Segment S)
       {
           #region -X-Pose
           if (Global.XposeAvail && Global.CanXpose)
           {
               if (!S.XDone)
               {
                   if (S.CaliX == false)
                   {
                       S.TimerStart = DateTime.Now;
                       S.TimerStop = DateTime.Now;
                       S.CaliX = true;
                       Console.WriteLine(S.ID + "   X ");
                   }
                   if (S.CaliX == true)
                   {
                       S.Q_initX = new CQuaternion(S.raw.Q.m_q0, S.raw.Q.m_q1, S.raw.Q.m_q2, S.raw.Q.m_q3);
                       if (double.IsNaN(S.Q_initX.m_q0))
                       {
                           S.Q_initX = new CQuaternion(1, 0, 0, 0);
                       }
                       S.TimerStop = DateTime.Now;
                       TimeSpan duration = S.TimerStop - S.TimerStart;
                       if (duration.Seconds > 4)
                       {
                           S.XDone = true;
                           Global.XDoneCnt++;
                           if (Global.XDoneCnt > 12)
                           {
                               Global.AposeAvail = true;
                               Global.XDoneCnt = 0;
                               Global.XposeDone = true;
                           }
                       }
                   }
               }
           }
           #endregion
       }

        private static void APoseCalibrate(Segment S)
        {
            #region -A-Pose
            if (Global.AposeAvail && Global.CanApose)
            {
                if (!S.ADone)  //如果这个节点的Tpose还没有完成
                {
                    // Start Timer and A-Pose Calibrate  
                    //第一次进入该节点的A-Pose， 开始计时器，CaliA置位
                    if (S.CaliA == false)
                    {
                        S.TimerStart = DateTime.Now;
                        S.TimerStop = DateTime.Now;
                        S.CaliA = true;
                        Console.WriteLine(S.ID + "   A ");
                    }
                    // 正式开始记录A-Pose数据，如果Timer超过5s,完成该节点的A-pose数据读取,S.ADone置位.
                    // GlobalADoneCnt++. 当cnt加到11的时候，给一个Global.APoseStart置位
                    if (S.CaliA == true)
                    {
                        if (S.ID > 11)
                            S.Q_init = new CQuaternion(S.raw.Q.m_q0, -S.raw.Q.m_q1, -S.raw.Q.m_q2, -S.raw.Q.m_q3);
                        S.Q_initA = new CQuaternion(S.raw.Q.m_q0, S.raw.Q.m_q1, S.raw.Q.m_q2, S.raw.Q.m_q3);
                        if (double.IsNaN(S.Q_initA.m_q0))
                        {
                            S.Q_initA = new CQuaternion(1, 0, 0, 0);
                        }

                        S.TimerStop = DateTime.Now;
                        TimeSpan duration = S.TimerStop - S.TimerStart;
                        double ds = duration.Ticks / 10000000.0;

                        if (ds > 4)
                        {
                            S.ADone = true;
                            Global.ADoneCnt++;
                            if (Global.ADoneCnt > 12)
                            {
                                Global.SposeAvail = true;
                                Global.ADoneCnt = 0;
                                Global.AposeDone = true;
                            }

                        }
                    }
                }
            } 
#endregion
        }

        private static void SPoseCalibrate(Segment S)
        {
            #region -S-Pose
            if (Global.SposeAvail && Global.CanSpose)
            {
                if (!S.SDone)  //如果这个节点的Tpose还没有完成
                {
                    // Start Timer and A-Pose Calibrate  
                    //第一次进入该节点的A-Pose， 开始计时器，CaliA置位
                    if (S.CaliS == false)
                    {
                        S.TimerStart = DateTime.Now;
                        S.TimerStop = DateTime.Now;
                        S.CaliS = true;
                        Console.WriteLine(S.ID + "   S ");
                    }
                    // 正式开始记录A-Pose数据，如果Timer超过5s,完成该节点的A-pose数据读取,S.ADone置位.
                    // GlobalADoneCnt++. 当cnt加到11的时候，给一个Global.APoseStart置位
                    if (S.CaliS == true)
                    {
                        S.Q_initS = new CQuaternion(S.raw.Q.m_q0, S.raw.Q.m_q1, S.raw.Q.m_q2, S.raw.Q.m_q3);
                        if (double.IsNaN(S.Q_initS.m_q0))
                        {
                            S.Q_initS = new CQuaternion(1, 0, 0, 0);
                        }
                        S.TimerStop = DateTime.Now;
                        TimeSpan duration = S.TimerStop - S.TimerStart;
                        double ds = duration.Ticks / 10000000.0;
                        if (ds > 5)
                        {
                            S.SDone = true;
                            Global.SDoneCnt++;
                            if (Global.SDoneCnt > 12)
                            {
                                Global.SposeDone = true;
                                //Global.SDoneCnt = 0;//why??
                            }

                        }
                    }
                }
            }
            #endregion
        }
        private static void APoseDone()
        {
            //Console.WriteLine("Calibration Done!");
            if (Global.SDoneCnt == 13) //此处应为所有活动的传感器最大的个数，保证所有的版本都能使用
            {
                Console.WriteLine("Calibration Done!");
                Global.isCali = true;
                Global.AposeAvail = false;

                for (int i = 0; i < SegmentCollection.arraySegment.Length; i++)
                {
                    Segment S = SegmentCollection.arraySegment[i];
                    S.ADone = false;
                    S.TDone = false;
                    S.SDone = false;
                    S.XDone = false;
                    S.CaliA = false;
                    S.CaliT = false;
                    S.CaliS = false;
                    S.CaliX = false;
                    S.PrevRotQ = new CQuaternion(S.raw.Q.m_q0, S.raw.Q.m_q1, S.raw.Q.m_q2, S.raw.Q.m_q3);
                    #region HIP (shoulders + chest)
                    if (S.ID == 11 || S.ID == 9 ) //S.ID == 10||
                    {
                        CQuaternion Zref = new CQuaternion(0, 0, 0, 1);
                        CQuaternion ZW0 = Zref * S.Q_init;//T;
                        CQuaternion ZW = S.raw.Q * ZW0;
                        CQuaternion YW = new CQuaternion(0, ZW.m_q1, ZW.m_q2, 0);
                        double sqrtYW = 1 / System.Math.Sqrt(ZW.m_q1 * ZW.m_q1 + ZW.m_q2 * ZW.m_q2);
                        CQuaternion YWnorm = new CQuaternion(0, YW.m_q1 * sqrtYW, YW.m_q2 * sqrtYW, 0);
                        V3 SS = new V3(YWnorm.m_q1 * 0.5, (YWnorm.m_q2 + 1) * 0.5, YWnorm.m_q3 * 0.5);
                        double w = System.Math.Sqrt(SS.x * SS.x + SS.y * SS.y + SS.z * SS.z);
                        V3 normSS = new V3(SS.x / w, SS.y / w, SS.z / w);
                        V3 Yref = new V3(0, 1, 0);
                        V3 z = V3.cross(Yref, normSS);
                        if (S.ID == 11)
                        {
                            Global.Qtc = new CQuaternion(w, z.x, z.y, z.z);
                            Global.invQtc = new CQuaternion(Global.Qtc.m_q0, -Global.Qtc.m_q1, -Global.Qtc.m_q2, -Global.Qtc.m_q3);
                        }
                        else 
                        {
                            S.Q_Cali = new CQuaternion(w, z.x, z.y, z.z);
                            S.invQ_Cali = new CQuaternion(Global.Qtc.m_q0, -Global.Qtc.m_q1, -Global.Qtc.m_q2, -Global.Qtc.m_q3);
                        }
                        if (double.IsNaN(Global.Qtc.m_q0))
                        {
                            Global.Qtc = new CQuaternion(1, 0, 0, 0);
                            Global.invQtc = new CQuaternion(1, 0, 0, 0);
                        }
                        if (Global.DEBUG_TPose)
                        {
                            Console.WriteLine("Qtc = " + Global.Qtc.ToString());
                            Console.WriteLine("Calibration Done!");
                        }
                    } 
                    #endregion

                    #region Left Leg
                    if (S.ID == 12 || S.ID == 13)
                    {
                        CQuaternion dQ = SegmentCollection.arraySegment[i].Q_initT * SegmentCollection.arraySegment[i].Q_initA.Conjugate();

                        CQuaternion YW = new CQuaternion(0, dQ.m_q1, dQ.m_q2, 0);//r = [ dQ.m_q1, dQ.m_q2, 0 ]


                        double sqrtYW = 1 / System.Math.Sqrt(dQ.m_q1 * dQ.m_q1 + dQ.m_q2 * dQ.m_q2);
                        CQuaternion YWnorm = new CQuaternion(0, YW.m_q1 * sqrtYW, YW.m_q2 * sqrtYW, 0); //Normalize R

                        V3 SS = new V3(YWnorm.m_q1 * 0.5, (YWnorm.m_q2 + 1) * 0.5, YWnorm.m_q3 * 0.5); // (R+Y)/2

                        double w = System.Math.Sqrt(SS.x * SS.x + SS.y * SS.y + SS.z * SS.z);

                        V3 normSS = new V3(SS.x / w, SS.y / w, SS.z / w);
                        V3 Yref = new V3(0, 1, 0);
                        V3 z = V3.cross(Yref, normSS);
                        SegmentCollection.arraySegment[i].Q_Cali = new CQuaternion(w, z.x, z.y, z.z);
                        SegmentCollection.arraySegment[i].invQ_Cali = new CQuaternion(w, -z.x, -z.y, -z.z);
                        if (double.IsNaN(SegmentCollection.arraySegment[i].Q_Cali.m_q0))
                        {
                            SegmentCollection.arraySegment[i].Q_Cali = new CQuaternion(1, 0, 0, 0);
                            SegmentCollection.arraySegment[i].invQ_Cali = new CQuaternion(1, 0, 0, 0);
                        }
                    } 
                    #endregion

                    #region Right Leg
                    if (S.ID == 15 || S.ID == 16)
                    {
                        CQuaternion dQ = SegmentCollection.arraySegment[i].Q_initA * SegmentCollection.arraySegment[i].Q_initT.Conjugate();
                        CQuaternion YW = new CQuaternion(0, dQ.m_q1, dQ.m_q2, 0);//r = [ dQ.m_q1, dQ.m_q2, 0 ]
                        double sqrtYW = 1 / System.Math.Sqrt(dQ.m_q1 * dQ.m_q1 + dQ.m_q2 * dQ.m_q2);
                        CQuaternion YWnorm = new CQuaternion(0, YW.m_q1 * sqrtYW, YW.m_q2 * sqrtYW, 0); //Normalize R

                        V3 SS = new V3(YWnorm.m_q1 * 0.5, (YWnorm.m_q2 + 1) * 0.5, YWnorm.m_q3 * 0.5); // (R+Y)/2

                        double w = System.Math.Sqrt(SS.x * SS.x + SS.y * SS.y + SS.z * SS.z);

                        V3 normSS = new V3(SS.x / w, SS.y / w, SS.z / w);
                        V3 Yref = new V3(0, 1, 0);
                        V3 z = V3.cross(Yref, normSS);
                        SegmentCollection.arraySegment[i].Q_Cali = new CQuaternion(w, z.x, z.y, z.z);
                        SegmentCollection.arraySegment[i].invQ_Cali = new CQuaternion(w, -z.x, -z.y, -z.z);
                        if (double.IsNaN(SegmentCollection.arraySegment[i].Q_Cali.m_q0))
                        {
                            SegmentCollection.arraySegment[i].Q_Cali = new CQuaternion(1, 0, 0, 0);
                            SegmentCollection.arraySegment[i].invQ_Cali = new CQuaternion(1, 0, 0, 0);
                        }
                    } 
                    #endregion
                     //OLD
                    #region ARMS
                   
                   if (S.ID < 9) //== 1 || S.ID == 2 || S.ID == 3 || S.ID == 6 || S.ID == 7 || S.ID == 8)     // || i == 11 || i == 12) 
                    {
                        
                        //CQuaternion invQ_initT  = new CQuaternion(S.Q_initT.m_q0, -S.Q_initT.m_q1, -S.Q_initT.m_q2, -S.Q_initT.m_q3);

                        #region inner product
                        double dotTX = S.Q_initT.m_q0 * S.Q_initX.m_q0 + S.Q_initT.m_q1 * S.Q_initX.m_q1 + S.Q_initT.m_q2 * S.Q_initX.m_q2 + S.Q_initT.m_q3 * S.Q_initX.m_q3;
                        double NegdotTX = S.Q_initT.m_q0 * -S.Q_initX.m_q0 + S.Q_initT.m_q1 * -S.Q_initX.m_q1 + S.Q_initT.m_q2 * -S.Q_initX.m_q2 + S.Q_initT.m_q3 * -S.Q_initX.m_q3;
                        if (System.Math.Acos(dotTX) > System.Math.Acos(NegdotTX))
                            S.Q_initX = new CQuaternion(-S.Q_initX.m_q0, -S.Q_initX.m_q1, -S.Q_initX.m_q2, -S.Q_initX.m_q3);

                        double dotTA = S.Q_initT.m_q0 * S.Q_initA.m_q0 + S.Q_initT.m_q1 * S.Q_initA.m_q1 + S.Q_initT.m_q2 * S.Q_initA.m_q2 + S.Q_initT.m_q3 * S.Q_initA.m_q3;
                        double NegdotTA = S.Q_initT.m_q0 * -S.Q_initA.m_q0 + S.Q_initT.m_q1 * -S.Q_initA.m_q1 + S.Q_initT.m_q2 * -S.Q_initA.m_q2 + S.Q_initT.m_q3 * -S.Q_initA.m_q3;
                        if (System.Math.Acos(dotTA) > System.Math.Acos(NegdotTA))
                            S.Q_initX = new CQuaternion(-S.Q_initA.m_q0, -S.Q_initA.m_q1, -S.Q_initA.m_q2, -S.Q_initA.m_q3);

                        double dotTS = S.Q_initT.m_q0 * S.Q_initS.m_q0 + S.Q_initT.m_q1 * S.Q_initS.m_q1 + S.Q_initT.m_q2 * S.Q_initS.m_q2 + S.Q_initT.m_q3 * S.Q_initS.m_q3;
                        double NegdotTS = S.Q_initT.m_q0 * -S.Q_initS.m_q0 + S.Q_initT.m_q1 * -S.Q_initS.m_q1 + S.Q_initT.m_q2 * -S.Q_initS.m_q2 + S.Q_initT.m_q3 * -S.Q_initS.m_q3;
                        if (System.Math.Acos(dotTS) > System.Math.Acos(NegdotTS))
                            S.Q_initS = new CQuaternion(-S.Q_initS.m_q0, -S.Q_initS.m_q1, -S.Q_initS.m_q2, -S.Q_initS.m_q3); 
                        #endregion
                       
                       CQuaternion T = S.Q_initT * S.Q_init;
                       CQuaternion X = S.Q_initX * S.Q_init;
                       CQuaternion AA = S.Q_initA * S.Q_init;
                       CQuaternion SS = S.Q_initS * S.Q_init;

                       S.CaliMat = new Matrix(4, 4);   // CaliMat ==> Jacobian Matrix
                       Matrix J0 = new Matrix(4, 1);
                       Matrix J1 = new Matrix(4, 1);
                       Matrix J2 = new Matrix(4, 1);
                        Matrix J3 = new Matrix(4, 1);
                        Matrix R0 = new Matrix(4, 1);
                        Matrix R1 = new Matrix(4, 1);
                        Matrix R2 = new Matrix(4, 1);
                        Matrix R3 = new Matrix(4, 1);

                        string MatStrL0 = "1 \r\n  0.707  \r\n  0.707 \r\n  0.707  \r\n";
                        string MatStrL1 = "0 \r\n -0.707  \r\n   0    \r\n  0      \r\n";
                        string MatStrL2 = "0 \r\n  0      \r\n  0     \r\n  -0.707 \r\n";
                        string MatStrL3 = "0 \r\n  0      \r\n -0.707 \r\n  0      \r\n";
                        string MatStrR0 = "1 \r\n  0.707  \r\n  0.707 \r\n  0.707  \r\n";
                        string MatStrR1 = "0 \r\n -0.707  \r\n  0     \r\n  0      \r\n";
                        string MatStrR2 = "0 \r\n  0      \r\n  0     \r\n  0.707  \r\n";
                        string MatStrR3 = "0 \r\n  0      \r\n  0.707 \r\n  0      \r\n"; 
                       
                        //Calculate Upper Arms' Rotation Angle
                        double cosTX = (S.Q_initX.m_q0 * S.Q_initT.m_q0) + (S.Q_initX.m_q1 * S.Q_initT.m_q1) + (S.Q_initX.m_q2 * S.Q_initT.m_q2) + (S.Q_initX.m_q3 * S.Q_initT.m_q3);
                        double Ang = System.Math.Acos(cosTX);
                        CQuaternion QUAX = new CQuaternion(System.Math.Cos(Ang), System.Math.Sin(Ang), 0, 0);
 
                       if (S.ID < 5)  //LH(0) LLA(1) LUA(2)
                        {
                            R0 = Matrix.Parse(MatStrL0);
                            R1 = Matrix.Parse(MatStrL1);
                           if (System.Math.Cos(Ang) > 0)
                               R0[1, 0] = System.Math.Cos(Ang);  //Matrix.Parse(MatStrU0);
                           else
                               R0[1, 0] = -System.Math.Cos(Ang);
                           if (System.Math.Cos(Ang) > 0) 
                               R1[1, 0] = -System.Math.Sin(Ang); //Matrix.Parse(MatStrU1);
                                else
                               R1[1, 0] = System.Math.Sin(Ang);
                            R2 = Matrix.Parse(MatStrL2);
                            R3 = Matrix.Parse(MatStrL3);
                        }
                        if (S.ID > 4)   //RH(7) RLA(6) RUA(5)
                        {
                            R0 = Matrix.Parse(MatStrR0);
                            R1 = Matrix.Parse(MatStrR1);
                            R0[1, 0] = System.Math.Cos(Ang); //Matrix.Parse(MatStrU0);
                            R1[1, 0] = -System.Math.Sin(Ang);//Matrix.Parse(MatStrU1);
                            R2 = Matrix.Parse(MatStrR2);
                            R3 = Matrix.Parse(MatStrR3);
                        }

                        Matrix A = new Matrix(4, 4);
                        // T Pose Read
                        A[0, 0] = T.m_q0;
                        A[0, 1] = T.m_q1;
                        A[0, 2] = T.m_q2;
                        A[0, 3] = T.m_q3;
                        // X Pose Read
                        A[1, 0] = X.m_q0;
                        A[1, 1] = X.m_q1;
                        A[1, 2] = X.m_q2;
                        A[1, 3] = X.m_q3;
                        // A Pose Read
                        A[2, 0] = AA.m_q0;
                        A[2, 1] = AA.m_q1;
                        A[2, 2] = AA.m_q2;
                        A[2, 3] = AA.m_q3;
                        // S Pose Read
                        A[3, 0] = SS.m_q0; //q.m_q0;
                        A[3, 1] = SS.m_q1; //q.m_q1;
                        A[3, 2] = SS.m_q2; //q.m_q2;
                        A[3, 3] = SS.m_q3; //q.m_q3;

                        if (A[3, 0] + A[2, 0] + A[1, 0] + A[0, 0] == 4)
                        {
                            S.CaliMat = Matrix.IdentityMatrix(4, 4);
                        }
                        else
                        {
                            J0 = A.SolveWith(R0);
                            J1 = A.SolveWith(R1);
                            J2 = A.SolveWith(R2);
                            J3 = A.SolveWith(R3);

                            for (int k = 0; k < 4; k++)     // cols
                            {
                                S.CaliMat[0, k] = J0[k, 0];
                            }
                            for (int k = 0; k < 4; k++)     // cols
                            {
                                S.CaliMat[1, k] = J1[k, 0];
                            }
                            for (int k = 0; k < 4; k++)     // cols
                            {
                                S.CaliMat[2, k] = J2[k, 0];
                            }
                            for (int k = 0; k < 4; k++)     // cols
                            {
                                S.CaliMat[3, k] = J3[k, 0];
                            }
                        }
                    } 
                    #endregion 

                 //   Console.WriteLine(SegmentCollection.arraySegment[i].ID + ": Q_Cali = " + SegmentCollection.arraySegment[i].Q_Cali.ToString());
                    Console.WriteLine("S:" + S.ID);
                    System.Threading.Thread.Sleep(3);
                    if (i == 16)
                    {
                        Global.AposeAvail = false;
                        Global.SposeAvail = false;
                        Global.XposeAvail = false;
                        Global.ADoneCnt = 0;
                        Global.TDoneCnt = 0;
                        Global.SDoneCnt = 0;
                        Global.XDoneCnt = 0;

                        Global.CanTpose = false;
                        Global.CanXpose = false;
                        Global.CanApose = false;
                        Global.CanSpose = false;


                        Global.isCali = true;
                        Global.HIP = new Pos();
                    }
                }
            }
        } 

        public static void Gen_QW_horizontal(Segment S_self)    //FOR ARMS
        {
            Segment S1 = S_self;
            CQuaternion q_current = S1.raw.Q;
            CQuaternion tmp0 = q_current * S1.Q_init; // S1.Q_initT;   T pose quaternion's inverse

            if (S1.ID == 11)
            {
                CQuaternion tmp1 = tmp0 * Global.Qtc;
                CQuaternion tmp2 = Global.invQtc * tmp1;
                S1.QW = new CQuaternion(tmp2.m_q0, -tmp2.m_q1, tmp2.m_q3, tmp2.m_q2);
            }

            #region DisableShoulderJoints
            else if (S1.ID > 10)    //   ||S1.ID == 1)
            {
                CQuaternion tmp1 = tmp0 * S1.Q_Cali;
                CQuaternion tmp2 = S1.invQ_Cali * tmp1;
                S1.QW = new CQuaternion(tmp2.m_q0, -tmp2.m_q1, tmp2.m_q3, tmp2.m_q2);
                //S1.QW = new CQuaternion(SegmentCollection.arraySegment[10].QW.m_q0, SegmentCollection.arraySegment[10].QW.m_q1, SegmentCollection.arraySegment[10].QW.m_q2, SegmentCollection.arraySegment[10].QW.m_q3);
            }
            #endregion

            else if(S1.ID < 9)  // S.ID == 1 2 3 6 7 8
            {
                Matrix tmpMat1 = new Matrix(4, 1);
                tmpMat1[0, 0] = tmp0.m_q0;  
                tmpMat1[1, 0] = tmp0.m_q1;  
                tmpMat1[2, 0] = tmp0.m_q2; 
                tmpMat1[3, 0] = tmp0.m_q3;  
                Matrix result = S1.CaliMat * tmpMat1;
                S1.QW = new CQuaternion(result[0, 0], result[1, 0], result[2, 0], result[3, 0]);
                S1.QW.Normalize();
                //if (Global.ShouldersNChest == false)
                //{
                    if (S1.ID == 4 || S1.ID == 5)
                    {
                        S1.QW = new CQuaternion(SegmentCollection.arraySegment[10].QW.m_q0, SegmentCollection.arraySegment[10].QW.m_q1, SegmentCollection.arraySegment[10].QW.m_q2, SegmentCollection.arraySegment[10].QW.m_q3);
                    }
                //}
            }
        }

        public static void Gen_QL(Segment S_self, Segment S_parent)     //FOR ALL SEGMENTS
        {

            Segment Sc = S_self;
            Segment Sp = S_parent;
            if (Global.isCali == false)
            {
                Sc.PrevRotQ = new CQuaternion(1, 0, 0, 0);
            }
            ///Quat_DeQuiver//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
            if (Sc.isFirstTime)
            {
                Sc.PrevRotQ = new CQuaternion();
                Sc.isFirstTime = false;
            }
            else
            {
                Sc.PrevRotQ = new CQuaternion(Sc.QL.m_q0, Sc.QL.m_q1, Sc.QL.m_q2, Sc.QL.m_q3);
            }
            ///Quat_DeQuiver//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

            if (Sc.ID == Sp.ID)//ID=11
            {
                Sc.QL = Sc.QW;
                //DEBUG***************************                
                //Console.WriteLine("HIP QW: " + Sc.QW.ToString());
            }
            else
            {
                CQuaternion tmp = new CQuaternion(Sp.QW.m_q0, Sp.QW.m_q1, Sp.QW.m_q2, Sp.QW.m_q3);
                tmp.Conjugate();
                Sc.QL = tmp * Sc.QW;
            }
          
            Euler ang = CQuaternion.Quat2angle(Sc.QL.m_q0, Sc.QL.m_q1, Sc.QL.m_q2, Sc.QL.m_q3, 3);
            double y = ang.Eul_1;
            double x = ang.Eul_2;
            double z = ang.Eul_3;
            
            if (double.IsNaN(x) || double.IsNaN(y) || double.IsNaN(z))
            {
                Sc.PrevRotQ = new CQuaternion(1, 0, 0, 0);
                //Console.WriteLine("S:" + Sc.ID + " is NaN");
                x = 0;
                y = 0;
                z = 0; 
            }

            Sc.Eul_Out.Eul_1 = y * 57.3;
            Sc.Eul_Out.Eul_2 = x * 57.3;
            Sc.Eul_Out.Eul_3 = z * 57.3;
            
            # region Left Side
            if (Sc.ID < 4)      //LEFT SIDE
            {
                if (Sc.ID == 2)
                {
                    if (Sc.Eul_Out.Eul_1 < -145 || Sc.Eul_Out.Eul_1 > 0)
                    {
                        //Console.WriteLine("RIGHT FORE ARM exceed!");
                        Sc.QL = Sc.PrevRotQ;
                        ang = CQuaternion.Quat2angle(Sc.QL.m_q0, Sc.QL.m_q1, Sc.QL.m_q2, Sc.QL.m_q3, 3);
                        y = ang.Eul_1;
                        x = ang.Eul_2;
                        z = ang.Eul_3;
                        Sc.Eul_Out.Eul_1 = y * 57.3;
                        Sc.Eul_Out.Eul_2 = x * 57.3;
                        Sc.Eul_Out.Eul_3 = z * 57.3; 
                    }
                }
                if (Sc.ID == 1)
                {
                    if (Sc.Eul_Out.Eul_1 < -80 || Sc.Eul_Out.Eul_1 > 80)
                    {
                        //Console.WriteLine("RIGHT HAND exceed!");
                        Sc.QL = Sc.PrevRotQ;
                        ang = CQuaternion.Quat2angle(Sc.QL.m_q0, Sc.QL.m_q1, Sc.QL.m_q2, Sc.QL.m_q3, 3);
                        y = ang.Eul_1;
                        x = ang.Eul_2;
                        z = ang.Eul_3;
                        Sc.Eul_Out.Eul_1 = y * 57.3;
                        Sc.Eul_Out.Eul_2 = x * 57.3;
                        Sc.Eul_Out.Eul_3 = z * 57.3;
                    }
                }
            }
            #endregion
            #region Right Arm
            else if (Sc.ID < 9)     //RIGHT SIDE
            {
                if (Sc.ID == 7)
                {
                    if (Sc.Eul_Out.Eul_1 > 145 || Sc.Eul_Out.Eul_1 < 0)
                    {
                        //Console.WriteLine("RIGHT FORE ARM exceed!");
                        Sc.QL = Sc.PrevRotQ;
                        ang = CQuaternion.Quat2angle(Sc.QL.m_q0, Sc.QL.m_q1, Sc.QL.m_q2, Sc.QL.m_q3, 3);
                        y = ang.Eul_1;
                        x = ang.Eul_2;
                        z = ang.Eul_3;
                        Sc.Eul_Out.Eul_1 = y * 57.3;
                        Sc.Eul_Out.Eul_2 = x * 57.3;
                        Sc.Eul_Out.Eul_3 = z * 57.3;

                    }
                }
                 if (Sc.ID == 8)
                {
                    if (Sc.Eul_Out.Eul_1 > 80 || Sc.Eul_Out.Eul_1 < -80)
                    {
                        //Console.WriteLine("RIGHT HAND exceed!");
                        Sc.QL = Sc.PrevRotQ;
                        ang = CQuaternion.Quat2angle(Sc.QL.m_q0, Sc.QL.m_q1, Sc.QL.m_q2, Sc.QL.m_q3, 3);
                        y = ang.Eul_1;
                        x = ang.Eul_2;
                        z = ang.Eul_3;
                        Sc.Eul_Out.Eul_1 = y * 57.3;
                        Sc.Eul_Out.Eul_2 = x * 57.3;
                        Sc.Eul_Out.Eul_3 = z * 57.3;
                    }
                }
               
            }
            #endregion
            
            #region Quat_Dequiver
            
            CQuaternion Start = new CQuaternion(Sc.PrevRotQ.m_q0, Sc.PrevRotQ.m_q1, Sc.PrevRotQ.m_q2, Sc.PrevRotQ.m_q3);
            CQuaternion Stop = new CQuaternion(Sc.QL.m_q0, Sc.QL.m_q1, Sc.QL.m_q2, Sc.QL.m_q3);
            CQuaternion q1 = new CQuaternion(1, 0, 0, 0);
            double dot = Start.m_q0 * Stop.m_q0 + Start.m_q1 * Stop.m_q1 + Start.m_q2 * Stop.m_q2 + Start.m_q3 * Stop.m_q3;

            if (dot < 0)
            {
                Start = new CQuaternion(-Sc.PrevRotQ.m_q0, -Sc.PrevRotQ.m_q1, -Sc.PrevRotQ.m_q2, -Sc.PrevRotQ.m_q3);
                dot = Start.m_q0 * Stop.m_q0 + Start.m_q1 * Stop.m_q1 + Start.m_q2 * Stop.m_q2 + Start.m_q3 * Stop.m_q3;

            }
            else if (dot > 1)
            {
                dot = 1.00;
            }
            double Scaler1 = System.Math.Asin(0.55 * dot) / System.Math.Asin(dot);
            double Scaler2 = System.Math.Asin(0.45 * dot) / System.Math.Asin(dot);

            q1 = new CQuaternion(Scaler1 * Start.m_q0 + Scaler2 * Stop.m_q0, Scaler1 * Start.m_q1 + Scaler2 * Stop.m_q1, Scaler1 * Start.m_q2 + Scaler2 * Stop.m_q2, Scaler1 * Start.m_q3 + Scaler2 * Stop.m_q3);
            double mag = System.Math.Sqrt(q1.m_q0 * q1.m_q0 + q1.m_q1 * q1.m_q1 + q1.m_q2 * q1.m_q2 + q1.m_q3 * q1.m_q3);
            q1 = new CQuaternion(q1.m_q0 / mag, q1.m_q1 / mag, q1.m_q2 / mag, q1.m_q3 / mag);

            Sc.QL = new CQuaternion(q1.m_q0, q1.m_q1, q1.m_q2, q1.m_q3);
         
            #endregion
            
            ang = CQuaternion.Quat2angle(Sc.QL.m_q0, Sc.QL.m_q1, Sc.QL.m_q2, Sc.QL.m_q3, 3);
            y = ang.Eul_1;
            x = ang.Eul_2;
            z = ang.Eul_3;

            if (double.IsNaN(x) || double.IsNaN(y) || double.IsNaN(z))
            {
                Sc.PrevRotQ = new CQuaternion(1, 0, 0, 0);
               // Console.WriteLine("S:" + Sc.ID + " is NaN");
                x = 0;
                y = 0;
                z = 0;
             }

            Sc.Eul_Out.Eul_1 = y * 57.3;
            Sc.Eul_Out.Eul_2 = x * 57.3;
            Sc.Eul_Out.Eul_3 = z * 57.3;

            #region  ReCalculate Qworld
            if (Sc.ID == Sp.ID)
            {
                Sc.Qlocal = CQuaternion.angle2Quat(y, x, z, 3);
                Sc.Qworld = Sc.Qlocal;
            }
            else
            {
                Sc.Qlocal = CQuaternion.angle2Quat(y, x, z, 3);
                Sc.Qworld = Sp.Qworld * Sc.Qlocal;
            } 
            #endregion
        }

    }

    
}
