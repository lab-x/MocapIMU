using System;
using System.IO;
namespace Gwearable
{
    public class V3
    {
        public double x;
        public double y;
        public double z;
        public V3(double a, double b, double c)
        {
            x = a;
            y = b;
            z = c;
        }
        public static V3 cross(V3 I, V3 J)
        {
            double x = I.y * J.z - J.y * I.z;
            double y = -1 * (I.x * J.z - J.x * I.z);
            double z = I.x * J.y - J.x * I.y;
            V3 result = new V3(x, y, z);
            return result;
        }
        public double Norm()
        {
            return x * x+ y*y+ z* z;
        }
        //Quaternion Normalize
        public V3 Normalize()
        {
            double temp = Norm();
            x = this.x / System.Math.Sqrt(temp);
            y = this.y / System.Math.Sqrt(temp);
            z = this.z / System.Math.Sqrt(temp); 
            return this;
        }
        public override string ToString()
        {
            return x.ToString("0.0000") + " " + y.ToString("0.0000") + " " + z.ToString("0.0000");
        }
    }

    public class Pos
    {
        public double x;
        public double y;
        public double z;
        public Pos()
        {
            x = 0.0000;     // 00;
            y = 0.0000;     //1.1300;     // 00;
            z = 0.0000;     // 00;
        }
        public Pos(double a, double b, double c)
        {
            x = a;
            y = b;
            z = c;
        }
        public override string ToString()
        {
            return x.ToString("0.000") + " " + y.ToString("0.000") + " " + z.ToString("0.000");
        }
    }

    public class SensorData
    {
        public CQuaternion Q;
        public Acc acc;
        public Gyro gyro;
        public SensorData()
        {
            Q = new CQuaternion();
            acc = new Acc();
            gyro = new Gyro();
        }
    }

    public class Acc
    {
        public double acc_1;
        public double acc_2;
        public double acc_3;
        public double acc_mag;
        public Acc()
        {
            acc_1 = 0.000;
            acc_2 = 0.000;
            acc_3 = 0.000;
            acc_mag = 0.000;
        }
        public Acc(double a, double b, double c)
        {
            acc_1 = a;
            acc_2 = b;
            acc_3 = c;
            acc_mag = 0.000;
        }
        public override string ToString()
        {
            return acc_1.ToString("0.000") + " " + acc_2.ToString("0.000") + " " + acc_3.ToString("0.000");
        }
    }

    public class Gyro
    {
        public double gyro_1;
        public double gyro_2;
        public double gyro_3;
        public double gyro_mag;
        public Gyro()
        {
            gyro_1 = 0.000;
            gyro_2 = 0.000;
            gyro_3 = 0.000;
            gyro_mag = 0.000;
        }
    }

    public class Position
    {
        /*public static int gaitDetector()   //return State[1]
        {   
            //Globol.State[0] => Previous State   state[1] => Current State
            Segment Left = SegmentCollection.arraySegment[13];
            Segment Right = SegmentCollection.arraySegment[16];
            //double[] b = { 0.1827, 0.1827 };//double[] a = { 1.0, -0.6346 };
            int gL_out = 0;
            int gR_out = 0;
            //double[] Gyr0_mag = { 0, 0, 0, 0 }; double[] Gyr1_mag = { 0, 0, 0, 0 };

            if (Global.LC < 3 || Global.RC < 3)
            {   //P_rankle  = [0,0.18,0]; P_lankle = [0,0.18,0]; P_hip = [0,0.96,0]; 
                Global.State[0] = 0;
                Global.State[1] = 0;
                Left.Position.y = 0.0;  //new Pos(0, 0.03, 0);   //ysj bug  0.18
                Right.Position.y = 0.0;// = new Pos(0, 0.03, 0);
                //Global.gaitCount ++;
            }

            if (Global.LC >= 3 && Global.RC >= 3)
            {
                if (Global.Filted_Left_Gyro[3] >= 70)
                    gL_out = 1;
                else if (Global.Filted_Left_Gyro[3] < 70)
                    gL_out = 0;
                if (Global.Filted_Right_Gyro[3] >= 70)
                    gR_out = 1;
                else if (Global.Filted_Right_Gyro[3] < 70)
                    gR_out = 0;


                if ((gL_out == 0) && (gR_out == 0))
                {

                    Global.State[1] = 0;  //BothRoot
                }

                else if ((gL_out == 1) && (gR_out == 0))
                    Global.State[1] = 2;  //RightRoot              
                else if ((gL_out == 0) && (gR_out == 1))
                    Global.State[1] = 1;  //LeftRoot
                else if ((gL_out == 1) && (gR_out == 1))    //出错的概率？？
                {
                    if (Global.JumpFlag == 1)
                    {
                        Global.State[1] = 0;
                    }
                    else
                    {
                        switch (Global.State[0])
                        {
                            case 1:
                                Global.State[1] = 2;
                                break;
                            case 2:
                                Global.State[1] = 1;
                                break;
                        }
                    }
                }
                else Global.State[1] = 0;

            }
            Global.State[0] = Global.State[1];
            return Global.State[1];
        }*/

        public static void FeetContact(int GyroFlag)   // flag:1-->LeftRooted flag:2-->RightRooted flag:0-->BothRooted
        {
            #region Calculate Distance from HIP to Ankle
            Segment HIP = SegmentCollection.arraySegment[10];
            Segment LUL = SegmentCollection.arraySegment[11];
            Segment LLL = SegmentCollection.arraySegment[12];
            Segment RUL = SegmentCollection.arraySegment[14];
            Segment RLL = SegmentCollection.arraySegment[15];
            Segment RF = SegmentCollection.arraySegment[16];
            Segment LF = SegmentCollection.arraySegment[13];


            //CQuaternion ToeAnkle = new CQuaternion(0.0, 0.0, 0.0, -1.0); //CQuaternion AnkleToe = new CQuaternion(0.0, 0.0, 0.0, 1.0);
            CQuaternion AnkleKnee = new CQuaternion(0.0, 0.0, 1.0, 0.0);
            CQuaternion KneeAnkle = new CQuaternion(0.0, 0.0, -1.0, 0.0);
            CQuaternion KneeTmp = new CQuaternion(0.0, 0.0, 1.0, 0.0);
            CQuaternion TmpKnee = new CQuaternion(0.0, 0.0, -1.0, 0.0);
            CQuaternion LtmpLhip = new CQuaternion(0.0, -1.0, 0.0, 0.0);
            CQuaternion LhipLtmp = new CQuaternion(0.0, 1.0, 0.0, 0.0);
            CQuaternion RtmpRhip = new CQuaternion(0.0, 1.0, 0.0, 0.0);
            CQuaternion RhipRtmp = new CQuaternion(0.0, -1.0, 0.0, 0.0);

            //////////////Left Toe-->Hip
            // CQuaternion tmp = new CQuaternion(LF.Qworld.m_q0, LF.Qworld.m_q1, LF.Qworld.m_q2, LF.Qworld.m_q3); // CQuaternion vltla = (LF.Qworld * ToeAnkle) * (tmp.Conjugate());
            CQuaternion tmp = new CQuaternion(LLL.Qworld.m_q0, LLL.Qworld.m_q1, LLL.Qworld.m_q2, LLL.Qworld.m_q3);
            CQuaternion vlalk = (LLL.Qworld * AnkleKnee) * (tmp.Conjugate());
            tmp = new CQuaternion(LUL.Qworld.m_q0, LUL.Qworld.m_q1, LUL.Qworld.m_q2, LUL.Qworld.m_q3);
            CQuaternion vlklt = (LUL.Qworld * KneeTmp) * (tmp.Conjugate());
            tmp = new CQuaternion(HIP.Qworld.m_q0, HIP.Qworld.m_q1, HIP.Qworld.m_q2, HIP.Qworld.m_q3);
            CQuaternion vltlh = (HIP.Qworld * LtmpLhip) * (tmp.Conjugate());

            //////////////Right Toe-->Hip
            //tmp = new CQuaternion(RF.Qworld.m_q0, RF.Qworld.m_q1, RF.Qworld.m_q2, RF.Qworld.m_q3); //CQuaternion vrtra = (RF.Qworld * ToeAnkle) * (tmp.Conjugate());
            tmp = new CQuaternion(RLL.Qworld.m_q0, RLL.Qworld.m_q1, RLL.Qworld.m_q2, RLL.Qworld.m_q3);
            CQuaternion vrark = (RLL.Qworld * AnkleKnee) * (tmp.Conjugate());
            tmp = new CQuaternion(RUL.Qworld.m_q0, RUL.Qworld.m_q1, RUL.Qworld.m_q2, RUL.Qworld.m_q3);
            CQuaternion vrkrt = (RUL.Qworld * KneeTmp) * (tmp.Conjugate());
            tmp = new CQuaternion(HIP.Qworld.m_q0, HIP.Qworld.m_q1, HIP.Qworld.m_q2, HIP.Qworld.m_q3);
            CQuaternion vrtrh = (HIP.Qworld * RtmpRhip) * (tmp.Conjugate());

            //Pos DistLa2H = new Pos(0.27 * vltla.m_q1 + 0.55 * vlalk.m_q1 + 0.55 * vlklt.m_q1 + 0.1125 * vltlh.m_q1, 0.27 * vltla.m_q2 + 0.55 * vlalk.m_q2 + 0.55 * vlklt.m_q2 + 0.1125 * vltlh.m_q2, 0.27 * vltla.m_q3 + 0.55 * vlalk.m_q3 + 0.55 * vlklt.m_q3 + 0.1125 * vltlh.m_q3);
            Pos DistLa2H = new Pos(0.55 * vlalk.m_q1 + 0.55 * vlklt.m_q1 + 0.1125 * vltlh.m_q1, 0.55 * vlalk.m_q2 + 0.55 * vlklt.m_q2 + 0.1125 * vltlh.m_q2, 0.55 * vlalk.m_q3 + 0.55 * vlklt.m_q3 + 0.1125 * vltlh.m_q3);
            double Ldiff = DistLa2H.y;//System.Math.Sqrt(DistLa2H.x * DistLa2H.x + DistLa2H.y * DistLa2H.y + DistLa2H.z * DistLa2H.z);
            //Pos DistRa2H = new Pos(0.27 * vrtra.m_q1 + 0.55 * vrark.m_q1 + 0.55 * vrkrt.m_q1 + 0.1125 * vrtrh.m_q1, 0.27 * vrtra.m_q2 + 0.55 * vrark.m_q2 + 0.55 * vrkrt.m_q2 + 0.1125 * vrtrh.m_q2, 0.27 * vrtra.m_q3 + 0.55 * vrark.m_q3 + 0.55 * vrkrt.m_q3 + 0.1125 * vrtrh.m_q3);
            Pos DistRa2H = new Pos(0.55 * vrark.m_q1 + 0.55 * vrkrt.m_q1 + 0.1125 * vrtrh.m_q1, 0.55 * vrark.m_q2 + 0.55 * vrkrt.m_q2 + 0.1125 * vrtrh.m_q2, 0.55 * vrark.m_q3 + 0.55 * vrkrt.m_q3 + 0.1125 * vrtrh.m_q3);
            double Rdiff = DistRa2H.y;//System.Math.Sqrt(DistRa2H.x * DistRa2H.x + DistRa2H.y * DistLa2H.y + DistRa2H.z * DistRa2H.z);

            #endregion

            #region FeetContact
            if (System.Math.Abs(Ldiff - Rdiff) > 0.03)
            {
                if (DistLa2H.y > DistRa2H.y)
                {
                    //Ground Contact: Left Foot
                    if (Global.FCflag == 2)
                    {
                        Global.FCflag = 0;
                    }
                    else
                        Global.FCflag = 1;
                }
                else if (DistLa2H.y < DistRa2H.y)
                {
                    //Ground Contact: Right Foot
                    if (Global.FCflag == 1)
                    {
                        Global.FCflag = 0;
                    }
                    else
                        Global.FCflag = 2;
                }
            }
            else
            {
                //Ground contact: Both Feet
                Global.FCflag = 0;
            }

            if (Global.DEBUG_FeetContact)
            {
                if (Global.FCflag == 0)
                    Console.WriteLine("0    0");//    Jflag:" + Global.JumpFlag.ToString() + " Count:" + Global.JumpCountDown.ToString());
                else if (Global.FCflag == 1)
                    Console.WriteLine("0    1");//    Jflag:" + Global.JumpFlag.ToString() + " Count:" + Global.JumpCountDown.ToString());
                else if (Global.FCflag == 2)
                    Console.WriteLine("1    0");//    Jflag:" + Global.JumpFlag.ToString() + " Count:" + Global.JumpCountDown.ToString());
            }
            #endregion

        }

        public static void ExternalPosition()
        {
            #region Predefined Parameters
            //CQuaternion ToeAnkle = new CQuaternion(0.0, 0.0, 0.0, -1.0); //CQuaternion AnkleToe = new CQuaternion(0.0, 0.0, 0.0, 1.0);
            CQuaternion AnkleKnee = new CQuaternion(0.0, 0.0, 1.0, 0.0);
            CQuaternion KneeAnkle = new CQuaternion(0.0, 0.0, -1.0, 0.0);
            CQuaternion KneeTmp = new CQuaternion(0.0, 0.0, 1.0, 0.0);
            CQuaternion TmpKnee = new CQuaternion(0.0, 0.0, -1.0, 0.0);
            CQuaternion LtmpLhip = new CQuaternion(0.0, -1.0, 0.0, 0.0);
            CQuaternion LhipLtmp = new CQuaternion(0.0, 1.0, 0.0, 0.0);
            CQuaternion RtmpRhip = new CQuaternion(0.0, 1.0, 0.0, 0.0);
            CQuaternion RhipRtmp = new CQuaternion(0.0, -1.0, 0.0, 0.0);
            CQuaternion HeadNeck = new CQuaternion(0.0, 0.0, -1.0, 0.0);   //Qhead
            CQuaternion NeckHip = new CQuaternion(0.0, 0.0, -1.0, 0.0);    //Qhip

            Segment Head = SegmentCollection.arraySegment[8];
            Segment HIP = SegmentCollection.arraySegment[10];

            Segment LUL = SegmentCollection.arraySegment[11];
            Segment LLL = SegmentCollection.arraySegment[12];
            Segment RUL = SegmentCollection.arraySegment[14];
            Segment RLL = SegmentCollection.arraySegment[15];
            Segment RF = SegmentCollection.arraySegment[16];
            Segment LF = SegmentCollection.arraySegment[13];
            #endregion            
            
            #region Save Previous Position State
            Pos Rprevious = new Pos(RF.Position.x, RF.Position.y, RF.Position.z);
            Pos Lprevious = new Pos(LF.Position.x, LF.Position.y, LF.Position.z);
            Pos HipPrev = new Pos(HIP.Position.x, HIP.Position.y, HIP.Position.z);
            #endregion           
            
            CQuaternion tmp = new CQuaternion(Head.Qworld.m_q0, Head.Qworld.m_q1, Head.Qworld.m_q2, Head.Qworld.m_q3);
            CQuaternion vHeadNeck = (Head.Qworld * HeadNeck) * (tmp.Conjugate());
            tmp = new CQuaternion(HIP.Qworld.m_q0, HIP.Qworld.m_q1, HIP.Qworld.m_q2, HIP.Qworld.m_q3);
            CQuaternion vNeckHip = (HIP.Qworld * NeckHip) * (tmp.Conjugate());
            // Adopt Positioning Data
            //Pos DistHead2Hip = new Pos(0.5 * vNeckHip.m_q1 + 0.20 * vHeadNeck.m_q1, 0.5 * vNeckHip.m_q2 + 0.20 * vHeadNeck.m_q2, 0.5 * vNeckHip.m_q3 + 0.20 * vHeadNeck.m_q3);
            Pos DistHead2Hip = new Pos(0.7 * vNeckHip.m_q1, 0.7 * vNeckHip.m_q2, 0.7 * vNeckHip.m_q3);
            Global.HIP = new Pos(Global.HEAD.x + DistHead2Hip.x, Global.HEAD.y + DistHead2Hip.y, Global.HEAD.z + DistHead2Hip.z);

#region Smoothly Shift Calc-->Abs
           //Load Previous Pos State  --- Check Distance

 /*
  *         if (Global.PosState == 0)
            {
                double threshold = 0.05;     //????
                Pos distance = new Pos(HipPrev.x - Global.HIP.x, HipPrev.y - Global.HIP.y, HipPrev.z - Global.HIP.z);
                double D = System.Math.Sqrt(distance.x * distance.x + distance.y * distance.y + distance.z * distance.z);
                if (D > threshold)
                {
                    Global.SmoothFlag = true;
                }

                //update current state
                Global.PosState = 1;     // 0--CalcPosition 1--AbsPosition
                Console.WriteLine("Global.PosState: "+ Global.PosState.ToString());
            }
            if (Global.SmoothFlag)
            {
                double threshold = 0.05;     //????
                Pos distance = new Pos(HipPrev.x - Global.HIP.x, HipPrev.y - Global.HIP.y, HipPrev.z - Global.HIP.z);
                double D = System.Math.Sqrt(distance.x * distance.x + distance.y * distance.y + distance.z * distance.z);
                if (D > threshold)
                {
                    HIP.Position.x = (99 * HipPrev.x + Global.HIP.x) / 100;
                    HIP.Position.y = (99 * HipPrev.y + Global.HIP.y) / 100;
                    HIP.Position.z = (99 * HipPrev.z + Global.HIP.z) / 100;
                }
                else
                {
                    Global.SmoothFlag = false;
                }
                Global.HIP = HIP.Position;
            }*/
#endregion


            #region BackTrackFeetPosition
            tmp = new CQuaternion(HIP.Qworld.m_q0, HIP.Qworld.m_q1, HIP.Qworld.m_q2, HIP.Qworld.m_q3);
            CQuaternion vrhipRtmp = (HIP.Qworld * RhipRtmp) * (tmp.Conjugate());
            tmp = new CQuaternion(RUL.Qworld.m_q0, RUL.Qworld.m_q1, RUL.Qworld.m_q2, RUL.Qworld.m_q3);
            CQuaternion vrtmprknee = (RUL.Qworld * TmpKnee) * (tmp.Conjugate());
            tmp = new CQuaternion(RLL.Qworld.m_q0, RLL.Qworld.m_q1, RLL.Qworld.m_q2, RLL.Qworld.m_q3);
            CQuaternion vrkneerankle = (RLL.Qworld * KneeAnkle) * (tmp.Conjugate());
            //tmp = new CQuaternion(RF.Qworld.m_q0, RF.Qworld.m_q1, RF.Qworld.m_q2, RF.Qworld.m_q3);
            //CQuaternion vranklertoe = (RF.Qworld * AnkleToe) * (tmp.Conjugate());
            tmp = new CQuaternion(HIP.Qworld.m_q0, HIP.Qworld.m_q1, HIP.Qworld.m_q2, HIP.Qworld.m_q3);
            CQuaternion vlhipltmp = (HIP.Qworld * LhipLtmp) * (tmp.Conjugate());
            tmp = new CQuaternion(LUL.Qworld.m_q0, LUL.Qworld.m_q1, LUL.Qworld.m_q2, LUL.Qworld.m_q3);
            CQuaternion vltmplknee = (LUL.Qworld * TmpKnee) * (tmp.Conjugate());
            tmp = new CQuaternion(LLL.Qworld.m_q0, LLL.Qworld.m_q1, LLL.Qworld.m_q2, LLL.Qworld.m_q3);
            CQuaternion vlkneelankle = (LLL.Qworld * KneeAnkle) * (tmp.Conjugate());
            //tmp = new CQuaternion(LF.Qworld.m_q0, LF.Qworld.m_q1, LF.Qworld.m_q2, LF.Qworld.m_q3);
            //CQuaternion vlankleltoe = (LF.Qworld * AnkleToe) * (tmp.Conjugate());
            

            RF.Position.x = HIP.Position.x + 0.1125 * vrhipRtmp.m_q1 + 0.55 * vrtmprknee.m_q1 + 0.55 * vrkneerankle.m_q1; // + 0.27 * vranklertoe.m_q1;
            RF.Position.y = HIP.Position.y + 0.1125 * vrhipRtmp.m_q2 + 0.55 * vrtmprknee.m_q2 + 0.55 * vrkneerankle.m_q2; // + 0.27 * vranklertoe.m_q2;
            RF.Position.z = HIP.Position.z + 0.1125 * vrhipRtmp.m_q3 + 0.55 * vrtmprknee.m_q3 + 0.55 * vrkneerankle.m_q3; // + 0.27 * vranklertoe.m_q3;

            LF.Position.x = HIP.Position.x + 0.1125 * vlhipltmp.m_q1 + 0.55 * vltmplknee.m_q1 + 0.55 * vlkneelankle.m_q1; // + 0.27 * vlankleltoe.m_q1;
            LF.Position.y = HIP.Position.y + 0.1125 * vlhipltmp.m_q2 + 0.55 * vltmplknee.m_q2 + 0.55 * vlkneelankle.m_q2; // + 0.27 * vlankleltoe.m_q2;
            LF.Position.z = HIP.Position.z + 0.1125 * vlhipltmp.m_q3 + 0.55 * vltmplknee.m_q3 + 0.55 * vlkneelankle.m_q3; // + 0.27 * vlankleltoe.m_q3;            
            
            #endregion

            SegmentCollection.arraySegment[10].Position = Global.HIP;

            #region Position De-Quiver
            Pos RPos_post = new Pos(RF.Position.x, RF.Position.y, RF.Position.z);
            Pos LPos_post = new Pos(LF.Position.x, LF.Position.y, LF.Position.z);
            RF.Position.x = (3 * Rprevious.x + RPos_post.x) / 4;
            RF.Position.y = (3 * Rprevious.y + RPos_post.y) / 4;
            RF.Position.z = (3 * Rprevious.z + RPos_post.z) / 4;
            LF.Position.x = (3 * Lprevious.x + LPos_post.x) / 4;
            LF.Position.y = (3 * Lprevious.y + LPos_post.y) / 4;
            LF.Position.z = (3 * Lprevious.z + LPos_post.z) / 4;

            if (System.Math.Abs(HipPrev.x - HIP.Position.x) > 0 && System.Math.Abs(HipPrev.x - HIP.Position.x) < 0.005)
            {
                HIP.Position.x = HipPrev.x;
            }
            if (System.Math.Abs(HipPrev.y - HIP.Position.y) > 0 && System.Math.Abs(HipPrev.y - HIP.Position.y) < 0.005)
            {
                HIP.Position.y = HipPrev.y;
            }
            if (System.Math.Abs(HipPrev.z - HIP.Position.z) > 0 && System.Math.Abs(HipPrev.z - HIP.Position.z) < 0.002)
            {
                HIP.Position.z = HipPrev.z;
            }
            HIP.Position.x = (3 * HipPrev.x + HIP.Position.x) / 4;
            HIP.Position.y = (3 * HipPrev.y + HIP.Position.y) / 4;
            HIP.Position.z = (9 * HipPrev.z + HIP.Position.z) / 10;
            //Console.WriteLine("HIP: " + HIP.Position.ToString()); 
            #endregion
        }

        public static void CalcPosition(int GyroFlag)   // flag:1-->LeftRooted flag:2-->RightRooted flag:0-->BothRooted
        {
            #region Calculate Distance from HIP to Ankle
            Segment HIP = SegmentCollection.arraySegment[10];
            Segment LUL = SegmentCollection.arraySegment[11];
            Segment LLL = SegmentCollection.arraySegment[12];
            Segment RUL = SegmentCollection.arraySegment[14];
            Segment RLL = SegmentCollection.arraySegment[15];
            Segment RF = SegmentCollection.arraySegment[16];
            Segment LF = SegmentCollection.arraySegment[13];


            //CQuaternion ToeAnkle = new CQuaternion(0.0, 0.0, 0.0, -1.0); //CQuaternion AnkleToe = new CQuaternion(0.0, 0.0, 0.0, 1.0);
            CQuaternion AnkleKnee = new CQuaternion(0.0, 0.0, 1.0, 0.0);
            CQuaternion KneeAnkle = new CQuaternion(0.0, 0.0, -1.0, 0.0);
            CQuaternion KneeTmp = new CQuaternion(0.0, 0.0, 1.0, 0.0);
            CQuaternion TmpKnee = new CQuaternion(0.0, 0.0, -1.0, 0.0);
            CQuaternion LtmpLhip = new CQuaternion(0.0, -1.0, 0.0, 0.0);
            CQuaternion LhipLtmp = new CQuaternion(0.0, 1.0, 0.0, 0.0);
            CQuaternion RtmpRhip = new CQuaternion(0.0, 1.0, 0.0, 0.0);
            CQuaternion RhipRtmp = new CQuaternion(0.0, -1.0, 0.0, 0.0);

            //////////////Left Toe-->Hip
            // CQuaternion tmp = new CQuaternion(LF.Qworld.m_q0, LF.Qworld.m_q1, LF.Qworld.m_q2, LF.Qworld.m_q3); // CQuaternion vltla = (LF.Qworld * ToeAnkle) * (tmp.Conjugate());
            CQuaternion tmp = new CQuaternion(LLL.Qworld.m_q0, LLL.Qworld.m_q1, LLL.Qworld.m_q2, LLL.Qworld.m_q3);
            CQuaternion vlalk = (LLL.Qworld * AnkleKnee) * (tmp.Conjugate());
            tmp = new CQuaternion(LUL.Qworld.m_q0, LUL.Qworld.m_q1, LUL.Qworld.m_q2, LUL.Qworld.m_q3);
            CQuaternion vlklt = (LUL.Qworld * KneeTmp) * (tmp.Conjugate());
            tmp = new CQuaternion(HIP.Qworld.m_q0, HIP.Qworld.m_q1, HIP.Qworld.m_q2, HIP.Qworld.m_q3);
            CQuaternion vltlh = (HIP.Qworld * LtmpLhip) * (tmp.Conjugate());

            //////////////Right Toe-->Hip
            //tmp = new CQuaternion(RF.Qworld.m_q0, RF.Qworld.m_q1, RF.Qworld.m_q2, RF.Qworld.m_q3); //CQuaternion vrtra = (RF.Qworld * ToeAnkle) * (tmp.Conjugate());
            tmp = new CQuaternion(RLL.Qworld.m_q0, RLL.Qworld.m_q1, RLL.Qworld.m_q2, RLL.Qworld.m_q3);
            CQuaternion vrark = (RLL.Qworld * AnkleKnee) * (tmp.Conjugate());
            tmp = new CQuaternion(RUL.Qworld.m_q0, RUL.Qworld.m_q1, RUL.Qworld.m_q2, RUL.Qworld.m_q3);
            CQuaternion vrkrt = (RUL.Qworld * KneeTmp) * (tmp.Conjugate());
            tmp = new CQuaternion(HIP.Qworld.m_q0, HIP.Qworld.m_q1, HIP.Qworld.m_q2, HIP.Qworld.m_q3);
            CQuaternion vrtrh = (HIP.Qworld * RtmpRhip) * (tmp.Conjugate());

            //Pos DistLa2H = new Pos(0.27 * vltla.m_q1 + 0.55 * vlalk.m_q1 + 0.55 * vlklt.m_q1 + 0.1125 * vltlh.m_q1, 0.27 * vltla.m_q2 + 0.55 * vlalk.m_q2 + 0.55 * vlklt.m_q2 + 0.1125 * vltlh.m_q2, 0.27 * vltla.m_q3 + 0.55 * vlalk.m_q3 + 0.55 * vlklt.m_q3 + 0.1125 * vltlh.m_q3);
            Pos DistLa2H = new Pos(0.55 * vlalk.m_q1 + 0.55 * vlklt.m_q1 + 0.1125 * vltlh.m_q1, 0.55 * vlalk.m_q2 + 0.55 * vlklt.m_q2 + 0.1125 * vltlh.m_q2, 0.55 * vlalk.m_q3 + 0.55 * vlklt.m_q3 + 0.1125 * vltlh.m_q3);
            double Ldiff = DistLa2H.y;//System.Math.Sqrt(DistLa2H.x * DistLa2H.x + DistLa2H.y * DistLa2H.y + DistLa2H.z * DistLa2H.z);
            //Pos DistRa2H = new Pos(0.27 * vrtra.m_q1 + 0.55 * vrark.m_q1 + 0.55 * vrkrt.m_q1 + 0.1125 * vrtrh.m_q1, 0.27 * vrtra.m_q2 + 0.55 * vrark.m_q2 + 0.55 * vrkrt.m_q2 + 0.1125 * vrtrh.m_q2, 0.27 * vrtra.m_q3 + 0.55 * vrark.m_q3 + 0.55 * vrkrt.m_q3 + 0.1125 * vrtrh.m_q3);
            Pos DistRa2H = new Pos(0.55 * vrark.m_q1 + 0.55 * vrkrt.m_q1 + 0.1125 * vrtrh.m_q1, 0.55 * vrark.m_q2 + 0.55 * vrkrt.m_q2 + 0.1125 * vrtrh.m_q2, 0.55 * vrark.m_q3 + 0.55 * vrkrt.m_q3 + 0.1125 * vrtrh.m_q3);
            double Rdiff = DistRa2H.y;//System.Math.Sqrt(DistRa2H.x * DistRa2H.x + DistRa2H.y * DistLa2H.y + DistRa2H.z * DistRa2H.z);

            #endregion

            #region FeetContact
            if (System.Math.Abs(Ldiff - Rdiff) > 0.03)
            {
                if (DistLa2H.y > DistRa2H.y)
                {
                    //Ground Contact: Left Foot
                    if (Global.FCflag == 2)
                    {
                        Global.FCflag = 0;
                    }
                    else
                        Global.FCflag = 1;
                }
                else if (DistLa2H.y < DistRa2H.y)
                {
                    //Ground Contact: Right Foot
                    if (Global.FCflag == 1)
                    {
                        Global.FCflag = 0;
                    }
                    else
                        Global.FCflag = 2;
                }
            }
            else
            {
                //Ground contact: Both Feet
                Global.FCflag = 0;
            }

            if (Global.DEBUG_FeetContact)
            {
                if (Global.FCflag == 0)
                    Console.WriteLine("0    0");//    Jflag:" + Global.JumpFlag.ToString() + " Count:" + Global.JumpCountDown.ToString());
                else if (Global.FCflag == 1)
                    Console.WriteLine("0    1");//    Jflag:" + Global.JumpFlag.ToString() + " Count:" + Global.JumpCountDown.ToString());
                else if (Global.FCflag == 2)
                    Console.WriteLine("1    0");//    Jflag:" + Global.JumpFlag.ToString() + " Count:" + Global.JumpCountDown.ToString());
            }
            #endregion

            #region Jump Case Template CountDown
            if (Global.JumpFlag == 1)
            {
                // Console.WriteLine("Hip:" + Global.HipAcc.ToString() + " LUL:" + Global.LULAcc.ToString() + " LLL:" + Global.LLLAcc.ToString() + " RUL:" + Global.RULAcc.ToString() + " RLL:" + Global.RLLAcc.ToString());
                Global.FCflag = 0;
                Global.JumpCountDown--;    //start from 7
                HIP.Position.y = Global.JumpHeight[Global.JumpCountDown];
                Global.JumpFlag = 1;

                if (Global.JumpCountDown == 0)
                {
                    Console.WriteLine("JUMP FINISHED!!!!!");
                    Global.JumpFlag = 0;
                    Global.JumpCountDown = 22;
                    Global.NoJumpStart = DateTime.Now;
                }
            }

            #endregion


            #region Save Previous Position State
            Pos Rprevious = new Pos(RF.Position.x, RF.Position.y, RF.Position.z);
            Pos Lprevious = new Pos(LF.Position.x, LF.Position.y, LF.Position.z);
            Pos HipPrev = new Pos(HIP.Position.x, HIP.Position.y, HIP.Position.z);
            #endregion

            #region Calculate Hip Position
            //BothRooted
            if (Global.FCflag == 0)
            {
                //if (LF.Position.y < 0.0) LF.Position.y = 0;  if (RF.Position.y < 0.0) RF.Position.y = 0; //大于0时，也应该赋值为0？
                double Lx = LF.Position.x + DistLa2H.x;
                double Ly = LF.Position.y + DistLa2H.y;
                double Lz = LF.Position.z + DistLa2H.z;
                double Rx = RF.Position.x + DistRa2H.x;
                double Ry = RF.Position.y + DistRa2H.y;
                double Rz = RF.Position.z + DistRa2H.z;

                if (Global.JumpFlag == 0)
                {
                    if (DistRa2H.y < 1 && DistLa2H.y < 1)
                        HIP.Position.y = (Ry + Ly) / 2.0;
                    else
                        HIP.Position.y = 1.13;
                }
                HIP.Position.x = (Rx + Lx) / 2.0;
                HIP.Position.z = (Rz + Lz) / 2.0;
            }
            //LEFT Rooted
            else if (Global.FCflag == 1)
            {
                HIP.Position.x = LF.Position.x + DistLa2H.x;
                if (DistLa2H.y < 1)
                    HIP.Position.y = LF.Position.y + DistLa2H.y;
                else
                    HIP.Position.y = 1.13;
                HIP.Position.z = LF.Position.z + DistLa2H.z;
            }
            else if (Global.FCflag == 2)
            {
                HIP.Position.x = RF.Position.x + DistRa2H.x;
                if (DistRa2H.y < 1)
                    HIP.Position.y = RF.Position.y + DistRa2H.y;
                else
                    HIP.Position.y = 1.13;
                HIP.Position.z = RF.Position.z + DistRa2H.z;
            }
            if (Global.Zero)
            {
                HIP.Position = new Pos(0.0, 1.0, 0.0);
            }
            #endregion

            /************************************************************************/
            #region Blank Interface for Absolute Position Aiding System
            if (Global.ActivateAbsPos)
            {
                HIP.Position.z = Global.HIP.z;
            }
            #endregion


            #region BackTrack Feet Position
            tmp = new CQuaternion(HIP.Qworld.m_q0, HIP.Qworld.m_q1, HIP.Qworld.m_q2, HIP.Qworld.m_q3);
            CQuaternion vrhipRtmp = (HIP.Qworld * RhipRtmp) * (tmp.Conjugate());
            tmp = new CQuaternion(RUL.Qworld.m_q0, RUL.Qworld.m_q1, RUL.Qworld.m_q2, RUL.Qworld.m_q3);
            CQuaternion vrtmprknee = (RUL.Qworld * TmpKnee) * (tmp.Conjugate());
            tmp = new CQuaternion(RLL.Qworld.m_q0, RLL.Qworld.m_q1, RLL.Qworld.m_q2, RLL.Qworld.m_q3);
            CQuaternion vrkneerankle = (RLL.Qworld * KneeAnkle) * (tmp.Conjugate());
            //tmp = new CQuaternion(RF.Qworld.m_q0, RF.Qworld.m_q1, RF.Qworld.m_q2, RF.Qworld.m_q3);
            //CQuaternion vranklertoe = (RF.Qworld * AnkleToe) * (tmp.Conjugate());
            tmp = new CQuaternion(HIP.Qworld.m_q0, HIP.Qworld.m_q1, HIP.Qworld.m_q2, HIP.Qworld.m_q3);
            CQuaternion vlhipltmp = (HIP.Qworld * LhipLtmp) * (tmp.Conjugate());
            tmp = new CQuaternion(LUL.Qworld.m_q0, LUL.Qworld.m_q1, LUL.Qworld.m_q2, LUL.Qworld.m_q3);
            CQuaternion vltmplknee = (LUL.Qworld * TmpKnee) * (tmp.Conjugate());
            tmp = new CQuaternion(LLL.Qworld.m_q0, LLL.Qworld.m_q1, LLL.Qworld.m_q2, LLL.Qworld.m_q3);
            CQuaternion vlkneelankle = (LLL.Qworld * KneeAnkle) * (tmp.Conjugate());
            //tmp = new CQuaternion(LF.Qworld.m_q0, LF.Qworld.m_q1, LF.Qworld.m_q2, LF.Qworld.m_q3);
            //CQuaternion vlankleltoe = (LF.Qworld * AnkleToe) * (tmp.Conjugate());

            RF.Position.x = HIP.Position.x + 0.1125 * vrhipRtmp.m_q1 + 0.55 * vrtmprknee.m_q1 + 0.55 * vrkneerankle.m_q1; // + 0.27 * vranklertoe.m_q1;
            RF.Position.y = HIP.Position.y + 0.1125 * vrhipRtmp.m_q2 + 0.55 * vrtmprknee.m_q2 + 0.55 * vrkneerankle.m_q2; // + 0.27 * vranklertoe.m_q2;
            if (RF.Position.y < 0)
                RF.Position.y = 0;
            RF.Position.z = HIP.Position.z + 0.1125 * vrhipRtmp.m_q3 + 0.55 * vrtmprknee.m_q3 + 0.55 * vrkneerankle.m_q3; // + 0.27 * vranklertoe.m_q3;

            LF.Position.x = HIP.Position.x + 0.1125 * vlhipltmp.m_q1 + 0.55 * vltmplknee.m_q1 + 0.55 * vlkneelankle.m_q1; // + 0.27 * vlankleltoe.m_q1;
            LF.Position.y = HIP.Position.y + 0.1125 * vlhipltmp.m_q2 + 0.55 * vltmplknee.m_q2 + 0.55 * vlkneelankle.m_q2; // + 0.27 * vlankleltoe.m_q2;
            if (RF.Position.y < 0)
                RF.Position.y = 0;
            LF.Position.z = HIP.Position.z + 0.1125 * vlhipltmp.m_q3 + 0.55 * vltmplknee.m_q3 + 0.55 * vlkneelankle.m_q3; // + 0.27 * vlankleltoe.m_q3;            
            #endregion

            #region Position De-Quiver
            Pos RPos_post = new Pos(RF.Position.x, RF.Position.y, RF.Position.z);
            Pos LPos_post = new Pos(LF.Position.x, LF.Position.y, LF.Position.z);
            RF.Position.x = (3 * Rprevious.x + RPos_post.x) / 4;
            RF.Position.y = (3 * Rprevious.y + RPos_post.y) / 4;
            RF.Position.z = (3 * Rprevious.z + RPos_post.z) / 4;
            LF.Position.x = (3 * Lprevious.x + LPos_post.x) / 4;
            LF.Position.y = (3 * Lprevious.y + LPos_post.y) / 4;
            LF.Position.z = (3 * Lprevious.z + LPos_post.z) / 4;

            if (System.Math.Abs(HipPrev.x - HIP.Position.x) > 0 && System.Math.Abs(HipPrev.x - HIP.Position.x) < 0.002)
            {
                HIP.Position.x = HipPrev.x;
            }
            if (System.Math.Abs(HipPrev.y - HIP.Position.y) > 0 && System.Math.Abs(HipPrev.y - HIP.Position.y) < 0.002)
            {
                HIP.Position.y = HipPrev.y;
            }
            if (System.Math.Abs(HipPrev.z - HIP.Position.z) > 0 && System.Math.Abs(HipPrev.z - HIP.Position.z) < 0.002)
            {
                HIP.Position.z = HipPrev.z;
            }
            HIP.Position.x = (3 * HipPrev.x + HIP.Position.x) / 4;
            HIP.Position.y = (3 * HipPrev.y + HIP.Position.y) / 4;
            HIP.Position.z = (3 * HipPrev.z + HIP.Position.z) / 4;
            //Console.WriteLine("HIP: " + HIP.Position.ToString()); 
            #endregion
        }
    

        /*
        public static void CalcPosition(int GyroFlag)   // flag:1-->LeftRooted flag:2-->RightRooted flag:0-->BothRooted
        {
            #region Calculate Distance from HIP to Ankle
            Segment HIP = SegmentCollection.arraySegment[10];
            Segment LUL = SegmentCollection.arraySegment[11];
            Segment LLL = SegmentCollection.arraySegment[12];
            Segment RUL = SegmentCollection.arraySegment[14];
            Segment RLL = SegmentCollection.arraySegment[15];
            Segment RF = SegmentCollection.arraySegment[16];
            Segment LF = SegmentCollection.arraySegment[13];


            //CQuaternion ToeAnkle = new CQuaternion(0.0, 0.0, 0.0, -1.0); //CQuaternion AnkleToe = new CQuaternion(0.0, 0.0, 0.0, 1.0);
            CQuaternion AnkleKnee = new CQuaternion(0.0, 0.0, 1.0, 0.0);
            CQuaternion KneeAnkle = new CQuaternion(0.0, 0.0, -1.0, 0.0);
            CQuaternion KneeTmp = new CQuaternion(0.0, 0.0, 1.0, 0.0);
            CQuaternion TmpKnee = new CQuaternion(0.0, 0.0, -1.0, 0.0);
            CQuaternion LtmpLhip = new CQuaternion(0.0, -1.0, 0.0, 0.0);
            CQuaternion LhipLtmp = new CQuaternion(0.0, 1.0, 0.0, 0.0);
            CQuaternion RtmpRhip = new CQuaternion(0.0, 1.0, 0.0, 0.0);
            CQuaternion RhipRtmp = new CQuaternion(0.0, -1.0, 0.0, 0.0);

            //////////////Left Toe-->Hip
            // CQuaternion tmp = new CQuaternion(LF.Qworld.m_q0, LF.Qworld.m_q1, LF.Qworld.m_q2, LF.Qworld.m_q3); // CQuaternion vltla = (LF.Qworld * ToeAnkle) * (tmp.Conjugate());
            CQuaternion tmp = new CQuaternion(LLL.Qworld.m_q0, LLL.Qworld.m_q1, LLL.Qworld.m_q2, LLL.Qworld.m_q3);
            CQuaternion vlalk = (LLL.Qworld * AnkleKnee) * (tmp.Conjugate());
            tmp = new CQuaternion(LUL.Qworld.m_q0, LUL.Qworld.m_q1, LUL.Qworld.m_q2, LUL.Qworld.m_q3);
            CQuaternion vlklt = (LUL.Qworld * KneeTmp) * (tmp.Conjugate());
            tmp = new CQuaternion(HIP.Qworld.m_q0, HIP.Qworld.m_q1, HIP.Qworld.m_q2, HIP.Qworld.m_q3);
            CQuaternion vltlh = (HIP.Qworld * LtmpLhip) * (tmp.Conjugate());

            //////////////Right Toe-->Hip
            //tmp = new CQuaternion(RF.Qworld.m_q0, RF.Qworld.m_q1, RF.Qworld.m_q2, RF.Qworld.m_q3); //CQuaternion vrtra = (RF.Qworld * ToeAnkle) * (tmp.Conjugate());
            tmp = new CQuaternion(RLL.Qworld.m_q0, RLL.Qworld.m_q1, RLL.Qworld.m_q2, RLL.Qworld.m_q3);
            CQuaternion vrark = (RLL.Qworld * AnkleKnee) * (tmp.Conjugate());
            tmp = new CQuaternion(RUL.Qworld.m_q0, RUL.Qworld.m_q1, RUL.Qworld.m_q2, RUL.Qworld.m_q3);
            CQuaternion vrkrt = (RUL.Qworld * KneeTmp) * (tmp.Conjugate());
            tmp = new CQuaternion(HIP.Qworld.m_q0, HIP.Qworld.m_q1, HIP.Qworld.m_q2, HIP.Qworld.m_q3);
            CQuaternion vrtrh = (HIP.Qworld * RtmpRhip) * (tmp.Conjugate());

            //Pos DistLa2H = new Pos(0.27 * vltla.m_q1 + 0.55 * vlalk.m_q1 + 0.55 * vlklt.m_q1 + 0.1125 * vltlh.m_q1, 0.27 * vltla.m_q2 + 0.55 * vlalk.m_q2 + 0.55 * vlklt.m_q2 + 0.1125 * vltlh.m_q2, 0.27 * vltla.m_q3 + 0.55 * vlalk.m_q3 + 0.55 * vlklt.m_q3 + 0.1125 * vltlh.m_q3);
            Pos DistLa2H = new Pos(0.55 * vlalk.m_q1 + 0.55 * vlklt.m_q1 + 0.1125 * vltlh.m_q1, 0.55 * vlalk.m_q2 + 0.55 * vlklt.m_q2 + 0.1125 * vltlh.m_q2, 0.55 * vlalk.m_q3 + 0.55 * vlklt.m_q3 + 0.1125 * vltlh.m_q3);
            //Pos DistLa2H = new Pos(0.55 * vlalk.m_q1 + 0.55 * vlklt.m_q1, 0.55 * vlalk.m_q2 + 0.55 * vlklt.m_q2, 0.55 * vlalk.m_q3 + 0.55 * vlklt.m_q3);
            
            //Pos DistRa2H = new Pos(0.27 * vrtra.m_q1 + 0.55 * vrark.m_q1 + 0.55 * vrkrt.m_q1 + 0.1125 * vrtrh.m_q1, 0.27 * vrtra.m_q2 + 0.55 * vrark.m_q2 + 0.55 * vrkrt.m_q2 + 0.1125 * vrtrh.m_q2, 0.27 * vrtra.m_q3 + 0.55 * vrark.m_q3 + 0.55 * vrkrt.m_q3 + 0.1125 * vrtrh.m_q3);
            Pos DistRa2H = new Pos(0.55 * vrark.m_q1 + 0.55 * vrkrt.m_q1 + 0.1125 * vrtrh.m_q1, 0.55 * vrark.m_q2 + 0.55 * vrkrt.m_q2 + 0.1125 * vrtrh.m_q2, 0.55 * vrark.m_q3 + 0.55 * vrkrt.m_q3 + 0.1125 * vrtrh.m_q3);
            //Pos DistRa2H = new Pos(0.55 * vrark.m_q1 + 0.55 * vrkrt.m_q1, 0.55 * vrark.m_q2 + 0.55 * vrkrt.m_q2, 0.55 * vrark.m_q3 + 0.55 * vrkrt.m_q3);
            
            double Rdiff = DistRa2H.y;//System.Math.Sqrt(DistRa2H.x * DistRa2H.x + DistRa2H.y * DistLa2H.y + DistRa2H.z * DistRa2H.z);
            double Ldiff = DistLa2H.y;//System.Math.Sqrt(DistLa2H.x * DistLa2H.x + DistLa2H.y * DistLa2H.y + DistLa2H.z * DistLa2H.z);
            
            #endregion

            #region FeetContact
            if (System.Math.Abs(Ldiff - Rdiff) > 0.03)
            {
                if (DistLa2H.y > DistRa2H.y)
                {
                    //Ground Contact: Left Foot
                    if (Global.FCflag == 2)
                    {
                        Global.FCflag = 0;
                    }
                    else
                        Global.FCflag = 1;
                }
                else if (DistLa2H.y < DistRa2H.y)
                {
                    //Ground Contact: Right Foot
                    if (Global.FCflag == 1)
                    {
                        Global.FCflag = 0;
                    }
                    else
                        Global.FCflag = 2;
                }
            }
            else
            {
                //Ground contact: Both Feet
                Global.FCflag = 0;
            }

            if (Global.DEBUG_FeetContact)
            {
                if (Global.FCflag == 0)
                    Console.WriteLine("0    0");//    Jflag:" + Global.JumpFlag.ToString() + " Count:" + Global.JumpCountDown.ToString());
                else if (Global.FCflag == 1)
                    Console.WriteLine("0    1");//    Jflag:" + Global.JumpFlag.ToString() + " Count:" + Global.JumpCountDown.ToString());
                else if (Global.FCflag == 2)
                    Console.WriteLine("1    0");//    Jflag:" + Global.JumpFlag.ToString() + " Count:" + Global.JumpCountDown.ToString());
            }
            #endregion

            #region Jump Case Template CountDown
        //    if (Global.JumpFlag == 1)
        //  {
        //      // Console.WriteLine("Hip:" + Global.HipAcc.ToString() + " LUL:" + Global.LULAcc.ToString() + " LLL:" + Global.LLLAcc.ToString() + " RUL:" + Global.RULAcc.ToString() + " RLL:" + Global.RLLAcc.ToString());
        //      Global.FCflag = 0;
        //      Global.JumpCountDown--;    //start from 7
        //      HIP.Position.y = Global.JumpHeight[Global.JumpCountDown];
        //      Global.JumpFlag = 1;
        //
        //      if (Global.JumpCountDown == 0)
        //      {
        //          Console.WriteLine("JUMP FINISHED!!!!!");
        //          Global.JumpFlag = 0;
        //          Global.JumpCountDown = 22;
        //          Global.NoJumpStart = DateTime.Now;
        //      }
        //  }
        //
            #endregion

            #region Save Previous Position State
            Pos Rprevious = new Pos(RF.Position.x, RF.Position.y, RF.Position.z);
            Pos Lprevious = new Pos(LF.Position.x, LF.Position.y, LF.Position.z);
            Pos HipPrev = new Pos(HIP.Position.x, HIP.Position.y, HIP.Position.z);
            #endregion

            #region Calculate Hip Position
            //BothRooted
            if (Global.FCflag == 0)
            {
                //if (LF.Position.y < 0.0) LF.Position.y = 0;  if (RF.Position.y < 0.0) RF.Position.y = 0; //大于0时，也应该赋值为0？
                double Lx = LF.Position.x + DistLa2H.x;
                double Ly = LF.Position.y + DistLa2H.y;
                double Lz = LF.Position.z + DistLa2H.z;
                double Rx = RF.Position.x + DistRa2H.x;
                double Ry = RF.Position.y + DistRa2H.y;
                double Rz = RF.Position.z + DistRa2H.z;

                if (Global.JumpFlag == 0)
                {
                    if (DistRa2H.y < 1.08 && DistLa2H.y < 1.08)
                    {
                        if (DistLa2H.y < DistRa2H.y)
                        {
                            HIP.Position = new Pos(Lx, Ly, Lz);
                        }
                        else
                        {
                            HIP.Position = new Pos(Rx, Ry, Rz);
                        }
                    }
                    else
                        HIP.Position = HipPrev;                       
                } 
            }
            //LEFT Rooted
            else if (Global.FCflag == 1)
            {
                HIP.Position.x = LF.Position.x + DistLa2H.x;
                if (DistLa2H.y < 1)
                    HIP.Position.y = LF.Position.y + DistLa2H.y;
                else
                    //HIP.Position.y = 1.13;
                    //HIP.Position.y = 0.00;
                    HIP.Position.y = HipPrev.y;
                    HIP.Position.z = LF.Position.z + DistLa2H.z;
            }
            else if (Global.FCflag == 2)
            {
                HIP.Position.x = RF.Position.x + DistRa2H.x;
                if (DistRa2H.y < 1)
                    HIP.Position.y = RF.Position.y + DistRa2H.y;
                else
                    HIP.Position.y = HipPrev.y;
                    //HIP.Position.y = 1.13;
                    //HIP.Position.y = 0.00;
                HIP.Position.z = RF.Position.z + DistRa2H.z;
            }
            if (Global.Zero)
            {
                HIP.Position = new Pos(0.0, 0.0, 0.0);
                //Global.HIP = new Pos(0.0, 0.0, 0.0);
            }
            #endregion
          //  /*********************************************************************** 
#region Smoothly Shift  Abs-->Calc
            //Load Previous Pos State  --- Check Distance
//          
//          if (Global.PosState == 1)
//          {
//              double threshold = 0.05;     //????
//              //Pos distance = new Pos(HipPrev.x - HIP.Position.x, HipPrev.y - HIP.Position.y, HipPrev.z - HIP.Position.z);
//              Pos distance = new Pos(HipPrev.x - Global.HIP.x, HipPrev.y -Global.HIP.y, HipPrev.z - Global.HIP.z);
//              
//              double D = System.Math.Sqrt(distance.x * distance.x + distance.y * distance.y + distance.z * distance.z);
//              if (D > threshold)
//              {
//                  Global.SmoothFlag = true; 
//              } 
//              //update current state
//              Global.PosState = 0;     // 0--CalcPosition 1--AbsPosition
//              //Console.WriteLine("Global.PosState: " + Global.PosState.ToString());
//          }
//          if (Global.SmoothFlag)
//          {
//              double threshold = 0.05;     //????
//              Pos distance = new Pos(HipPrev.x - HIP.Position.x, HipPrev.y - HIP.Position.y, HipPrev.z - HIP.Position.z);
//              double D = System.Math.Sqrt(distance.x * distance.x + distance.y * distance.y + distance.z * distance.z);
//              if (D > threshold)
//              {
//                  Global.SmoothFlag = true;
//                  HIP.Position.x = (99 * HipPrev.x + HIP.Position.x) / 100;
//                  HIP.Position.y = (99 * HipPrev.y + HIP.Position.y) / 100;
//                  HIP.Position.z = (99 * HipPrev.z + HIP.Position.z) / 100;
//              }
//              else
//              {
//                  Global.SmoothFlag = false;
//              }
//              Global.HIP = HIP.Position;
//          }
//          
#endregion

            #region position de-quiver
            if (System.Math.Abs(HipPrev.x - HIP.Position.x) > 0 && System.Math.Abs(HipPrev.x - HIP.Position.x) < 0.002)
            {
                HIP.Position.x = HipPrev.x;
            }
            if (System.Math.Abs(HipPrev.y - HIP.Position.y) > 0 && System.Math.Abs(HipPrev.y - HIP.Position.y) < 0.002)
            {
                HIP.Position.y = HipPrev.y;
            }
            if (System.Math.Abs(HipPrev.z - HIP.Position.z) > 0 && System.Math.Abs(HipPrev.z - HIP.Position.z) < 0.002)
            {
                HIP.Position.z = HipPrev.z;
            }
            HIP.Position.x = (3 * HipPrev.x + HIP.Position.x) / 4;
            HIP.Position.y = (3 * HipPrev.y + HIP.Position.y) / 4;
            HIP.Position.z = (3 * HipPrev.z + HIP.Position.z) / 4; 
            #endregion


            #region BackTrack Feet Position
            tmp = new CQuaternion(HIP.Qworld.m_q0, HIP.Qworld.m_q1, HIP.Qworld.m_q2, HIP.Qworld.m_q3);
            CQuaternion vrhipRtmp = (HIP.Qworld * RhipRtmp) * (tmp.Conjugate());
            tmp = new CQuaternion(RUL.Qworld.m_q0, RUL.Qworld.m_q1, RUL.Qworld.m_q2, RUL.Qworld.m_q3);
            CQuaternion vrtmprknee = (RUL.Qworld * TmpKnee) * (tmp.Conjugate());
            tmp = new CQuaternion(RLL.Qworld.m_q0, RLL.Qworld.m_q1, RLL.Qworld.m_q2, RLL.Qworld.m_q3);
            CQuaternion vrkneerankle = (RLL.Qworld * KneeAnkle) * (tmp.Conjugate());
            //tmp = new CQuaternion(RF.Qworld.m_q0, RF.Qworld.m_q1, RF.Qworld.m_q2, RF.Qworld.m_q3);
            //CQuaternion vranklertoe = (RF.Qworld * AnkleToe) * (tmp.Conjugate());
            tmp = new CQuaternion(HIP.Qworld.m_q0, HIP.Qworld.m_q1, HIP.Qworld.m_q2, HIP.Qworld.m_q3);
            CQuaternion vlhipltmp = (HIP.Qworld * LhipLtmp) * (tmp.Conjugate());
            tmp = new CQuaternion(LUL.Qworld.m_q0, LUL.Qworld.m_q1, LUL.Qworld.m_q2, LUL.Qworld.m_q3);
            CQuaternion vltmplknee = (LUL.Qworld * TmpKnee) * (tmp.Conjugate());
            tmp = new CQuaternion(LLL.Qworld.m_q0, LLL.Qworld.m_q1, LLL.Qworld.m_q2, LLL.Qworld.m_q3);
            CQuaternion vlkneelankle = (LLL.Qworld * KneeAnkle) * (tmp.Conjugate());
            //tmp = new CQuaternion(LF.Qworld.m_q0, LF.Qworld.m_q1, LF.Qworld.m_q2, LF.Qworld.m_q3);
            //CQuaternion vlankleltoe = (LF.Qworld * AnkleToe) * (tmp.Conjugate());

            RF.Position.x = HIP.Position.x + 0.1125 * vrhipRtmp.m_q1 + 0.55 * vrtmprknee.m_q1 + 0.55 * vrkneerankle.m_q1; // + 0.27 * vranklertoe.m_q1;
            RF.Position.y = HIP.Position.y + 0.1125 * vrhipRtmp.m_q2 + 0.55 * vrtmprknee.m_q2 + 0.55 * vrkneerankle.m_q2; // + 0.27 * vranklertoe.m_q2;
            if (RF.Position.y < -1.1)
                RF.Position.y = -1.1;
            RF.Position.z = HIP.Position.z + 0.1125 * vrhipRtmp.m_q3 + 0.55 * vrtmprknee.m_q3 + 0.55 * vrkneerankle.m_q3; // + 0.27 * vranklertoe.m_q3;

            LF.Position.x = HIP.Position.x + 0.1125 * vlhipltmp.m_q1 + 0.55 * vltmplknee.m_q1 + 0.55 * vlkneelankle.m_q1; // + 0.27 * vlankleltoe.m_q1;
            LF.Position.y = HIP.Position.y + 0.1125 * vlhipltmp.m_q2 + 0.55 * vltmplknee.m_q2 + 0.55 * vlkneelankle.m_q2; // + 0.27 * vlankleltoe.m_q2;
            if (RF.Position.y < -1.1)
                RF.Position.y = -1.1; ;
            LF.Position.z = HIP.Position.z + 0.1125 * vlhipltmp.m_q3 + 0.55 * vltmplknee.m_q3 + 0.55 * vlkneelankle.m_q3; // + 0.27 * vlankleltoe.m_q3;            
            #endregion 
            #endregion
            Global.HIP = HIP.Position;
        }
        */
        public static void AccIntg(Acc acc)
        {
            if (acc.acc_1 > 1)
            {
                acc.acc_1 = 1;

            }
            if (acc.acc_2 > 2)
            {
                acc.acc_2 = 2;

            }

            if (acc.acc_3 > 1)
            {
                acc.acc_3 = 1;

            }

            V3 Velo_dT = new V3(acc.acc_1 * 0.098, acc.acc_2 * 0.098, acc.acc_3 * 0.098);

            Global.velo = new V3(Global.velo.x + Velo_dT.x, Global.velo.y + Velo_dT.y, Global.velo.z + Velo_dT.z);

            /*   if (Global.velo.x > 0.5) 
            { 
                Global.velo.x = 0.5;
            }
            if (Global.velo.x > 0.5)
            {
                Global.velo.x = 0.5;
            }
            if (Global.velo.x > 2.5)
            {
                Global.velo.x = 2.5;

            }*/
            //Console.WriteLine("V: " + Global.velo.ToString());
            V3 Pos_dT = new V3(Global.velo.x * 0.098, Global.velo.y * 0.098, Global.velo.z * 0.098);
            Global.S = new V3(Global.S.x + Pos_dT.x, Global.S.y + Pos_dT.y, Global.S.z + Pos_dT.z);
        }


        public static void CoordinateTransform(int i)    //标定
        {
            float h = Global.height;
          
            #region generate transformation matrix
            if (i == 1)
            { 
                Pos Araw0 = new Pos(Global.firstpos0.x, Global.firstpos0.y, Global.firstpos0.z);        //new Pos(-28.545 / 1000, 1181.472 / 1000, 3093.082 / 1000);// Global.firstpos;
                Pos Oraw0 = new Pos(Global.secondpos0.x, Global.secondpos0.y, Global.secondpos0.z);     //new Pos(834.922 / 1000, 691.672 / 1000, 2520.504 / 1000);//Global.secondpos;
                Pos Braw0 = new Pos(Global.thirdpos0.x, Global.thirdpos0.y, Global.thirdpos0.z);        // new Pos(67.020 / 1000, 217.296 / 1000, 1741.619 / 1000);//Global.thirdpos; //手动输三点的position
                V3 AO0 = new V3(Oraw0.x - Araw0.x, Oraw0.y - Araw0.y, Oraw0.z - Araw0.z);
                V3 OB0 = new V3(Braw0.x - Oraw0.x, Braw0.y - Oraw0.y, Braw0.z - Oraw0.z);

                V3 y0 = V3.cross(AO0, OB0);
                y0.Normalize();
                V3 x0 = V3.cross(y0, OB0);
                x0.Normalize(); 
                V3 z0 = V3.cross(x0, y0); 
                Matrix Rot0 = new Matrix(3, 3);
                Rot0[0, 0] = x0.x;
                Rot0[0, 1] = x0.y;
                Rot0[0, 2] = x0.z;

                Rot0[1, 0] = y0.x;
                Rot0[1, 1] = y0.y;
                Rot0[1, 2] = y0.z;

                Rot0[2, 0] = z0.x;
                Rot0[2, 1] = z0.y;
                Rot0[2, 2] = z0.z;
         
                Matrix T0 = new Matrix(3, 1);
                T0[0, 0] = -Oraw0.x + h * y0.x;
                T0[1, 0] = -Oraw0.y + h * y0.y;
                T0[2, 0] = -Oraw0.z + h * y0.z;
                Matrix Trans0 = Rot0 * T0;

                Global.TransMat1[0, 0] = x0.x;
                Global.TransMat1[0, 1] = x0.y;
                Global.TransMat1[0, 2] = x0.z;
                Global.TransMat1[0, 3] = Trans0[0, 0];
                Global.TransMat1[1, 0] = y0.x;
                Global.TransMat1[1, 1] = y0.y;
                Global.TransMat1[1, 2] = y0.z;
                Global.TransMat1[1, 3] = Trans0[1, 0];
                Global.TransMat1[2, 0] = z0.x;
                Global.TransMat1[2, 1] = z0.y;
                Global.TransMat1[2, 2] = z0.z;
                Global.TransMat1[2, 3] = Trans0[2, 0];
                Console.WriteLine("TransMat1:" + Global.TransMat1.ToString());
                
                
                FileStream fs1 = new FileStream("basestation_1.txt", FileMode.OpenOrCreate);
                StreamWriter sw1 = new StreamWriter(fs1);
                sw1.Write(Global.TransMat1.ToString());
                sw1.Close();
                fs1.Close();
            }
            else if (i == 2)
            {
                Pos Araw1 = new Pos(Global.firstpos1.x, Global.firstpos1.y, Global.firstpos1.z);
                Pos Oraw1 = new Pos(Global.secondpos1.x, Global.secondpos1.y, Global.secondpos1.z);
                Pos Braw1 = new Pos(Global.thirdpos1.x, Global.thirdpos1.y, Global.thirdpos1.z);
                V3 AO1 = new V3(Oraw1.x - Araw1.x, Oraw1.y - Araw1.y, Oraw1.z - Araw1.z);
                V3 OB1 = new V3(Braw1.x - Oraw1.x, Braw1.y - Oraw1.y, Braw1.z - Oraw1.z);
                V3 y1 = V3.cross(AO1, OB1);
                y1.Normalize();
                V3 x1 = V3.cross(y1, OB1);
                x1.Normalize();
                V3 z1 = V3.cross(x1, y1);
                Matrix Rot1 = new Matrix(3, 3);
                Rot1[0, 0] = x1.x;
                Rot1[0, 1] = x1.y;
                Rot1[0, 2] = x1.z;

                Rot1[1, 0] = y1.x;
                Rot1[1, 1] = y1.y;
                Rot1[1, 2] = y1.z;

                Rot1[2, 0] = z1.x;
                Rot1[2, 1] = z1.y;
                Rot1[2, 2] = z1.z;
                Matrix T1 = new Matrix(3, 1);
                T1[0, 0] = -Oraw1.x + h * y1.x;
                T1[1, 0] = -Oraw1.y + h * y1.y;
                T1[2, 0] = -Oraw1.z + h * y1.z;
                Matrix Trans1 = Rot1 * T1;
                Global.TransMat2[0, 0] = x1.x;
                Global.TransMat2[0, 1] = x1.y;
                Global.TransMat2[0, 2] = x1.z;
                Global.TransMat2[0, 3] = Trans1[0, 0];
                Global.TransMat2[1, 0] = y1.x;
                Global.TransMat2[1, 1] = y1.y;
                Global.TransMat2[1, 2] = y1.z;
                Global.TransMat2[1, 3] = Trans1[1, 0];
                Global.TransMat2[2, 0] = z1.x;
                Global.TransMat2[2, 1] = z1.y;
                Global.TransMat2[2, 2] = z1.z;
                Global.TransMat2[2, 3] = Trans1[2, 0];
                Console.WriteLine("TransMat2:" + Global.TransMat2.ToString());
                
                FileStream fs2 = new FileStream("basestation_2.txt", FileMode.OpenOrCreate);
                StreamWriter sw2 = new StreamWriter(fs2);
                sw2.Write(Global.TransMat2.ToString());
                sw2.Close();
                fs2.Close();
            }
            Global.TransMatAvailable = true;

            #endregion 

            #region 赋值 
/*          
//3 --ROOM at home
//-0.6549286 -0.3343168 -0.6777173 2.3624073 
//0.0702397 0.8660053 -0.4950769 2.3792035 
//0.7524194 -0.3718427 -0.5436894 2.6200383 

            Global.TransMat1[0, 0]= -0.6549286 ;
            Global.TransMat1[0, 1]  = -0.3343168 ;
            Global.TransMat1[0, 2]  = -0.6777173 ;
            Global.TransMat1[0, 3]  = 2.3624073 ;
                                      
            Global.TransMat1[1, 0]  = 0.0702397 ;
            Global.TransMat1[1, 1]  = 0.8660053 ;
            Global.TransMat1[1, 2]  = -0.4950769;
            Global.TransMat1[1, 3]  = 2.3792035;
                                      
            Global.TransMat1[2, 0]  = 0.7524194 ;
            Global.TransMat1[2, 1]  = -0.3718427 ;
            Global.TransMat1[2, 2]  = -0.5436894 ;
            Global.TransMat1[2, 3]  = 2.6200383 ;    
     
//Dong.Head: 2222             
//0.6893654 0.3499155 0.6342984 -1.0466751 
//0.0475712 0.8518456 -0.5216282 2.3486419 
//-0.7228502 0.3897667 0.5705870 -0.7723936 
          
            Global.TransMat2[0, 0] = 0.6893654 ;
            Global.TransMat2[0, 1] = 0.3499155 ;
            Global.TransMat2[0, 2] = 0.6342984;
            Global.TransMat2[0, 3] = -1.0466751;
                          
            Global.TransMat2[1, 0] = 0.0475712  ;
            Global.TransMat2[1, 1] = 0.8518456;
            Global.TransMat2[1, 2] = -0.5216282 ;
            Global.TransMat2[1, 3] = 2.3486419;
                           
            Global.TransMat2[2, 0] = -0.7228502 ;
            Global.TransMat2[2, 1] =  0.3897667 ;
            Global.TransMat2[2, 2] = 0.5705870 ;
            Global.TransMat2[2, 3] = -0.7723936;
            Global.TransMatAvailable = true;  
  */          
            #endregion
        }
    }
    
    public class Tools
    {
        public static double[] timereverse(double[] data)
        {
            double swap;
            double[] returnValue = new double[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                swap = data[i];
                returnValue[i] = data[data.Length - 1 - i];
            }
            return returnValue;
        }
        public static double[] filtfilt(double[] b, double[] a, double[] data)
        {
            int len = 4;   //data.Length; //na = 2 a.Length; int nb = 2; b.Length; int nfilt = 2;System.Math.Max(na, nb);
            int nfact = 3;          //3 * (nfilt - 1);
            if (len <= nfact)
            {
                Console.WriteLine("input data too short!");
            }
            double zi = 0.8173;
            double[] d = new double[10];
            for (int i = 0; i < 3; i++)
                d[i] = 2 * data[0] - data[3 - i];

            for (int i = 3; i < 7; i++)
                d[i] = data[i - 3];

            for (int i = 7; i < 10; i++)
            {
                d[i] = 2 * data[3] - data[9 - i];
            }
            double[] x = filt(b, a, d, zi * d[0]);
            x = timereverse(x);
            double[] z = filt(b, a, x, zi * x[0]);
            z = timereverse(z);
            double[] rst = { z[3], z[4], z[5], z[6] };
            return rst;
        }
        public static double[] filt(double[] b, double[] a, double[] data, double zi)
        {
            double[] y = new double[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                if (i == 0)
                {
                    y[i] = zi;
                }
                else
                {
                    y[i] = 0;
                }
            }
            for (int n = 0; n < b.Length; n++)
            {
                int max = n;
                //CZ: zpetnovazebni cast 
                for (int k = 0; k <= max; k++)
                    y[n] += b[k] * data[n - k];
                for (int k = 1; k <= max; k++)
                    y[n] -= a[k] * y[n - k];
            }
            for (int n = b.Length; n < data.Length; n++)
            {
                //dopredna cast 
                for (int k = 1; k < a.Length; k++)
                    y[n] -= a[k] * y[n - k];
                //zpetnovazebni cast 
                for (int k = 0; k < b.Length; k++)
                    y[n] += b[k] * data[n - k];
            }
            return y;
        }
    }


}
