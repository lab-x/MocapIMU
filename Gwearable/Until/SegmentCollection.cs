using System;

namespace Gwearable
{
    public class SegmentCollection
    {
        public bool isChanged = false;

        public static Segment[] arraySegment = new Segment[17] { 
                                                                new Segment(1,2),new Segment(2,3), new Segment(3,4), new Segment(4,10),new Segment(5,10),new Segment(6,5), 
                                                                new Segment(7,6), new Segment(8,7), new Segment(9,10), new Segment(10,11), new Segment(11,11), new Segment(12,11), 
                                                                new Segment(13,12), new Segment(14,13), new Segment(15,11), new Segment(16,15), new Segment(17,16) };

        public CQuaternion Quat_packet(int[] data, int count)
        {
            double q0 = TwoBytes(data[2], data[3]) / 10000;
            double q1 = TwoBytes(data[4], data[5]) / 10000;
            double q2 = TwoBytes(data[6], data[7]) / 10000;
            double q3 = TwoBytes(data[8], data[9]) / 10000;

            double mag = System.Math.Sqrt(q0 * q0 + q1 * q1 + q2 * q2 + q3 * q3);
            if (0 == mag)
            {
                mag = 1;
            }
            CQuaternion QQ = new CQuaternion(q0 / mag, q1 / mag, q2 / mag, q3 / mag);
            return QQ;
        }

        public Acc Acc_packet(int[] data, int count, int indexID)
        {
            Acc acc = new Acc();
            acc.acc_1 = TwoBytes(data[10], data[11]) / 2048;//20480; //16384;
            acc.acc_2 = TwoBytes(data[12], data[13]) / 2048;//1024;//20480; //16384;
            acc.acc_3 = TwoBytes(data[14], data[15]) / 2048; //1024;//20480; //16384;


            return acc;
        }

        public Gyro Gyro_packet(int[] data, int count)
        {
            Gyro gyro = new Gyro();
            gyro.gyro_1 = TwoBytes(data[16], data[17]) / 16384;
            gyro.gyro_2 = TwoBytes(data[18], data[19]) / 16384;
            gyro.gyro_3 = TwoBytes(data[20], data[21]) / 16384;
            gyro.gyro_mag = System.Math.Sqrt(gyro.gyro_1 * gyro.gyro_1 + gyro.gyro_2 * gyro.gyro_2 + gyro.gyro_3 * gyro.gyro_3);
            return gyro;
        }

        public void Packet_resolver(int[] data, int count)
        {
            if (0 == data[1]) return;

            int IDindex = defineSerialPort.FindIndex(data[1]);

            Acc acc = Acc_packet(data, count, IDindex);
            if (acc.acc_1 > Global.FSR_Acc || acc.acc_2 > Global.FSR_Acc || acc.acc_3 > Global.FSR_Acc)
            {
                arraySegment[IDindex].raw = arraySegment[IDindex].raw;
            }
            else
            {
                arraySegment[IDindex].raw.Q = Quat_packet(data, count);
                arraySegment[IDindex].raw.acc = acc;   // Acc_packet(data, count, IDindex);
                arraySegment[IDindex].raw.gyro = Gyro_packet(data, count);
            }

            // JUMP 
            #region AccConvert_Sensor2NED2Avatar (OLD VERSION)
            /*CQuaternion Qacc = new CQuaternion(0.0, arraySegment[IDindex].raw.acc.acc_1, arraySegment[IDindex].raw.acc.acc_2, arraySegment[IDindex].raw.acc.acc_3);
            CQuaternion tmp = new CQuaternion(arraySegment[IDindex].raw.Q.m_q0, -arraySegment[IDindex].raw.Q.m_q1, -arraySegment[IDindex].raw.Q.m_q2, -arraySegment[IDindex].raw.Q.m_q3);
            CQuaternion tmp1 = Qacc * tmp;
            CQuaternion tmp2 = arraySegment[IDindex].raw.Q * tmp1;
            arraySegment[IDindex].AccCvt = new CQuaternion(tmp2.m_q0, tmp2.m_q1, tmp2.m_q2, tmp2.m_q3);

            // Set Time Window for Acc value, when it exceeds 1.7g.
            if (arraySegment[IDindex].AccCvt.m_q3 > 1.7)
            {
                arraySegment[IDindex].TimerStart = DateTime.Now;
                arraySegment[IDindex].AccExceed = 1;
            }

            arraySegment[IDindex].TimerStop = DateTime.Now;
            TimeSpan dt = arraySegment[IDindex].TimerStop - arraySegment[IDindex].TimerStart;
            double dts = dt.Ticks / 10000000.0;
            if (dts > 0.05)
            {
                arraySegment[IDindex].AccExceed = 0;
            }

            /*if (arraySegment[IDindex].ID == 11)      //HIP
            {   //NED
                CQuaternion HipAcc = new CQuaternion(0.0, arraySegment[IDindex].raw.acc.acc_1, arraySegment[IDindex].raw.acc.acc_2, arraySegment[IDindex].raw.acc.acc_3);
                CQuaternion Hiptmp0 = new CQuaternion(arraySegment[IDindex].raw.Q.m_q0, -arraySegment[IDindex].raw.Q.m_q1, -arraySegment[IDindex].raw.Q.m_q2, -arraySegment[IDindex].raw.Q.m_q3);
                CQuaternion Hiptmp1 = HipAcc * Hiptmp0;// arraySegment[IDindex].raw.Q;
                Global.HipAcc = arraySegment[IDindex].raw.Q * Hiptmp1;
                // Console.WriteLine("HIP:" + Global.HipAcc.ToString()); 
            }*/           
            #endregion

            #region AccConvert_Sensor2NED2Avatar   (LATEST)
           
            Segment S = arraySegment[IDindex];
            CQuaternion Qnow = S.raw.Q;

            CQuaternion Qacc = new CQuaternion(0.0, S.raw.acc.acc_1, S.raw.acc.acc_2, S.raw.acc.acc_3);
            CQuaternion tmp = new CQuaternion(Qnow.m_q0, -Qnow.m_q1, -Qnow.m_q2, -Qnow.m_q3);
            CQuaternion tmp1 = Qacc * tmp;
            CQuaternion tmp2 = Qnow * tmp1;

            // CQuaternion tmp3 = tmp2 * S.Q_init; // S1.Q_initT;   T pose quaternion's inverse

            if (S.ID == 11)
            {
                CQuaternion tmp4 = tmp2 * Global.Qtc;
                CQuaternion tmp5 = Global.invQtc * tmp4;
                S.AccCvt = new CQuaternion(tmp5.m_q0, -tmp5.m_q1, tmp5.m_q3, tmp5.m_q2);
            }
            else
            {
                CQuaternion tmp4 = tmp2 * S.Q_Cali;
                CQuaternion tmp5 = S.invQ_Cali * tmp4;
                S.AccCvt = new CQuaternion(tmp5.m_q0, -tmp5.m_q1, tmp5.m_q3, tmp5.m_q2);
            }
            if (((SegmentCollection.arraySegment[15].AccCvt.m_q2 > 2)||(SegmentCollection.arraySegment[12].AccCvt.m_q2 > 2)) && SegmentCollection.arraySegment[10].AccCvt.m_q2 > 1.2)
            {
                //Console.WriteLine("ID:" + S.ID.ToString() + "acc" + S.AccCvt.ToString());
                Console.WriteLine("  hip:  " + SegmentCollection.arraySegment[10].AccCvt.m_q2.ToString());

                Console.WriteLine(" Left:  " + SegmentCollection.arraySegment[12].AccCvt.m_q2.ToString());
                Console.WriteLine("Right:  " + SegmentCollection.arraySegment[15].AccCvt.m_q2.ToString());
                Global.Fall = true;
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////
            ////////////////////////////////////////////////////////////////////////////////////////////////////////
            #region 12thAug______JUMP
            ////////////////////////////////////////////////////////////////////////////////////////////////////////

           /* if (S.ID == 11)
            {
                // Console.WriteLine("HIP_Acc:  " + S.AccCvt.ToString());

                Acc acc_dT = new Acc(S.AccCvt.m_q1, S.AccCvt.m_q2, S.AccCvt.m_q3);

                if (Global.Deploit)
                {
                    if (Global.IntgEnabled)
                    {
                        Global.ii++;
                    }
                    if (S.AccCvt.m_q2 < 0.8)
                    {
                        if (Global.TimerEnabled && Global.IntgEnabled == false) //可以使用Timer而且没有在某一次积分过程中
                        {
                            //StartTimer;
                            Global.YdropSig_Start = DateTime.Now;
                            Global.IntgEnabled = true;
                            Global.ii++;
                            Gwearable.Position.AccIntg(acc_dT); //计算积分速度与距离
                        }
                    }
                    if (Global.IntgEnabled)                              //正在积分
                    {
                        Gwearable.Position.AccIntg(acc_dT);             //计算积分速度与距离

                        if (Global.TimerEnabled)                        //没有Y方向上的跳跃波峰出现
                        {
                            Global.YpeakSig_Stop = DateTime.Now;
                            TimeSpan dT = Global.YpeakSig_Stop - Global.YdropSig_Start;
                            double dts = dT.Ticks / 10000000.0;
                            if (dts > 0.8)
                            {
                                Global.IntgEnabled = false;             //之前无效信号，积分结果无效
                                Global.isJump = false;
                                Global.S = new V3(0, 0, 0);
                                Global.velo = new V3(0, 0, 0);
                            }
                        }
                        if (S.AccCvt.m_q2 > 2 && Global.isJump == false)                         //Peak Detected!
                        {
                            Console.WriteLine("V: " + Global.velo.ToString());
                            Console.WriteLine("S: " + Global.S.ToString());
                            Global.isJump = true;
                            Global.TimerEnabled = false;
                        }
                        if (Global.isJump && (S.AccCvt.m_q2 < 1.2 || S.AccCvt.m_q1 == 0 || S.AccCvt.m_q3 == 0))
                        {
                            Segment LF = SegmentCollection.arraySegment[13];
                            Segment RF = SegmentCollection.arraySegment[16];
                            //积分结束  计算位移
                            S.Position.x = S.Position.x + Global.S.x;
                            S.Position.y = S.Position.y + Global.S.y / 10;
                            S.Position.z = S.Position.z + Global.S.z;
                            LF.Position.x = LF.Position.x + Global.S.x;
                            LF.Position.y = LF.Position.y + Global.S.y / 10;
                            LF.Position.z = LF.Position.z + Global.S.z;
                            RF.Position.x = RF.Position.x + Global.S.x;
                            RF.Position.y = RF.Position.y + Global.S.y / 10;
                            RF.Position.z = RF.Position.z + Global.S.z;
                            //Console.WriteLine("S: " + Global.S.x + "  " + Global.S.z);

                            Global.IntgEnabled = false;
                            Global.isJump = false;
                            Global.S = new V3(0, 0, 0);
                            Global.velo = new V3(0, 0, 0);
                            Global.TimerEnabled = true;
                            Global.Deploit = false;
                            Global.Jump_Start = DateTime.Now;
                        }
                    }
                }
                if (Global.Deploit == false)
                {
                    if (S.Position.y > 1.13)
                    {
                        S.Position.y = S.Position.y - 0.02;
                    }
                    else
                    {
                        Global.Jump_Stop = DateTime.Now;
                        TimeSpan dt = Global.Jump_Stop - Global.Jump_Start;
                        double t = dt.Ticks / 10000000.0;
                        if (t > 1 || S.AccCvt.m_q2 > 2)
                        {
                            Global.Deploit = true;

                        }
                    }
                }
            }
            ////////////////////////////////////////////////////////////////////////////////////////////////////////
            */
            #endregion
       
            /*if (S.ID == 11) //   Console.WriteLine("HIP_Acc:  " + S.AccCvt.ToString());
            //arraySegment[IDindex].AccCvt = new CQuaternion(tmp2.m_q0, tmp2.m_q1, tmp2.m_q2, tmp2.m_q3);
            // Set Time Window for Acc value, when it exceeds 1.7g.
                if (arraySegment[IDindex].AccCvt.m_q3 > 2)
                {
                    arraySegment[IDindex].TimerStart = DateTime.Now;
                    arraySegment[IDindex].AccExceed = 1;
                }
            arraySegment[IDindex].TimerStop = DateTime.Now;
            TimeSpan DltT = arraySegment[IDindex].TimerStop - arraySegment[IDindex].TimerStart;
            double tt = DltT.Ticks / 10000000.0;
            if (tt > 0.05)
            {
                arraySegment[IDindex].AccExceed = 0;
            }   
            int AccAve = arraySegment[10].AccExceed + arraySegment[11].AccExceed + arraySegment[12].AccExceed + arraySegment[14].AccExceed + arraySegment[15].AccExceed;
      */
           
            #endregion

            /* if ((AccAve > 3) && (Global.JumpFlag == 0))
            {

                Global.NoJumpEnd = DateTime.Now;
                TimeSpan ts = Global.NoJumpEnd - Global.NoJumpStart;
                double stable = ts.Ticks / 10000000.0;
                if (stable > 1)
                {
                    Global.JumpFlag = 1;
                    //     Global.JumpTimeStart = DateTime.Now;
                    //     Global.JStart = 1;
                }

                Global.JumpFlag = 1;
            }*/
            #region JUMP CASE
            /*
            if (Global.isJump)
            {

                Global.NoJumpEnd = DateTime.Now;
                TimeSpan ts = Global.NoJumpEnd - Global.NoJumpStart;
                double stable = ts.Ticks / 10000000.0;
                if (stable > 1)
                {
                    Global.JumpFlag = 1;
                    //     Global.JumpTimeStart = DateTime.Now;
                    //     Global.JStart = 1;
                }

                Global.JumpFlag = 1;
            }


            if (Global.DEBUG_JumpDetect)
            {
                Console.WriteLine("JStart=" + Global.JStart + "   JumpFlag=" + Global.JumpFlag + "   JumpCountDown" + Global.JumpCountDown);
            }*/

            
            #endregion

        }

        public double TwoBytes(int a1, int a2)
        {
            double a;
            a = (a1 << 8) + (a2);
            if (a > 32767)
            {
                a -= 65536;
            }
            return a;
        }

        public string GetSendString()
        {
           // Console.WriteLine("EnableSmooth:"+ Global.EnableSmooth+"Smooth:"+Global.Smooth + "    ParaPrev:"+Global.Smooth_PrevParam + " ParaThis"+Global.Smooth_ThisParam);
            string str;
            
            #region Switch between Absolute and Dead Reckoning
            
            if (Global.EnableAbsPos)// Global.ActivateAbsPos && Global.ActivateAbsPos)
            {
                /*Position.FeetContact(1);
                if (Global.FCflag != 0)
                {
                    Position.ExternalPosition();  
                    SegmentCollection.arraySegment[10].Position = Global.HEAD;
                }*/
                Pos absPos = new Pos(Global.HEAD.x, Global.HEAD.y, Global.HEAD.z);
                SegmentCollection.arraySegment[10].Position = new Pos(absPos.x, absPos.y, absPos.z);
                str = absPos.ToString();
            }
            else
            {
                Position.CalcPosition(1);                                   
                //SegmentCollection.arraySegment[10].Position.y = 0;
                //str = SegmentCollection.arraySegment[10].Position.ToString();

                Pos absPos = new Pos(SegmentCollection.arraySegment[10].Position.x, SegmentCollection.arraySegment[10].Position.y + Global.LegLength, SegmentCollection.arraySegment[10].Position.z);
                //Pos absPos = new Pos(SegmentCollection.arraySegment[10].Position.x, SegmentCollection.arraySegment[10].Position.y, SegmentCollection.arraySegment[10].Position.z);
                //absPos.y = 0.0;
                //SegmentCollection.arraySegment[10].Position = new Pos(1, 0, 1);
                str = absPos.ToString();
            }
         
           /* if (Global.Fall)
            {
                SegmentCollection.arraySegment[10].Position.y = SegmentCollection.arraySegment[10].Position.y - 0.5;
                if (SegmentCollection.arraySegment[10].Position.y<-5)
                    Global.Fall = false;
                Pos absPos = new Pos(SegmentCollection.arraySegment[10].Position.x, SegmentCollection.arraySegment[10].Position.y - Global.LegLength , SegmentCollection.arraySegment[10].Position.z);

                str = absPos.ToString();
            }
            */ 
            #endregion 

            #region Old Version---Switch between Absolute and Dead Reckoning
            /*if (Global.ActivateAbsPos)
                Position.ExternalPosition();//Console.WriteLine(SegmentCollection.arraySegment[10].Position.ToString());
            else
                Position.CalcPosition(1);
            */ 
            #endregion  
            
            /*str = Global.HEAD.ToString();
            if(Global.isCali){
                absPos.y = Global.HEAD.y * 0.556 ;
            }
            SegmentCollection.arraySegment[10].Position = absPos;
            str = absPos.ToString();
            */ 

            //Console.WriteLine("**H:" + SegmentCollection.arraySegment[10].Eul_Out.ToString() + "**State:" + Global.PosState.ToString() + "**Smooth:" + Global.SmoothFlag.ToString());
            //str = arraySegment[10].Position.ToString();
            str += " ";
            str += arraySegment[10].Eul_Out.ToString();     //　ＨＩＰ
            str += " ";
            str += arraySegment[14].Eul_Out.ToString();     //　ＲＩＧＨＴ　ＵＰＰＥＲ　ＬＥＧ
            str += " ";
            str += arraySegment[15].Eul_Out.ToString();     //　ＲＩＧＨＴ　ＬＯＷＥＲ　ＬＥＧ
            str += " ";
            str += "0.00 0.00 0.00";                        //　ＲＩＧＨＴ ＦＥＥＴ
            //arraySegment[16].Eul_Out.ToString(); S.ID = 17;   (NODE Pkt_ID = 15)  
            str += " ";
            str += arraySegment[11].Eul_Out.ToString();     //　ＬＥＦＴ　ＵＰＰＥＲ　ＬＥＧ
            str += " ";                                     
            str += arraySegment[12].Eul_Out.ToString();     //  ＬＥＦＴ　ＬＯＷＥＲ　ＬＥＧ
            str += " ";
            str += "0.00 0.00 0.00";                        //  ＬＥＦＴ　ＦＥＥＴ
            //arraySegment[13].Eul_Out.ToString(); S.ID = 14;   (NODE Pkt_ID = 14)  
            str += " ";
            str += "0.00 0.00 0.00";                        //  Ｓ０
            str += " ";
            str += "0.00 0.00 0.00";                        //  Ｓ１
            str += " ";
            str += arraySegment[9].Eul_Out.ToString();                         //  Ｓ２
            str += " ";
            str += "0.00 0.00 0.00";//arraySegment[9].Eul_Out.ToString();      //  Ｓ３　  ------ＣＨＥＳＴ　ＳＥＧＭＥＮＴ　//"0.00 0.00 0.00";　
            //arraySegment[9].Eul_Out.ToString(); S.ID = 10;   (NODE Pkt_ID = 16)  
            str += " ";
            str += "0.00 0.00 0.00";                        //  ＮＥＣＫ
            str += " ";
            str += arraySegment[8].Eul_Out.ToString();      //  ＨＥＡＤ
            str += " ";
            str += arraySegment[4].Eul_Out.ToString();      //  ＲＩＧＨＴ　ＳＨＯＵＬＤＥＲ
            //arraySegment[4].Eul_Out.ToString(); S.ID = 5;   (NODE Pkt_ID = 13)  
            str += " ";
            str += arraySegment[5].Eul_Out.ToString();      //  ＲＩＧＨＴ　ＵＰＰＥＲ　ＡＲＭ
            str += " ";                                    
            str += arraySegment[6].Eul_Out.ToString();      //  ＲＩＧＨＴ　ＦＯＲＥ　ＡＲＭ
            str += " ";                                    
            str += arraySegment[7].Eul_Out.ToString();      //  ＲＩＧＨＴ　ＨＡＮＤ
           // Console.WriteLine("right hand: " + arraySegment[7].Eul_Out.ToString());
            str += " ";
            str += arraySegment[3].Eul_Out.ToString();      //  ＬＥＦＴ　ＳＨＯＵＬＤＥＲ
            //arraySegment[3].Eul_Out.ToString(); S.ID = 4;   (NODE Pkt_ID = 12)  
            str += " ";                                      
            str += arraySegment[2].Eul_Out.ToString();      //  ＬＥＦＴ　ＵＰＰＥＲ　ＡＲＭ
            str += " ";                                     
            str += arraySegment[1].Eul_Out.ToString();      //  ＬＥＦＴ　ＦＯＲＥ　ＡＲＭ
            str += " ";                                     
            str += arraySegment[0].Eul_Out.ToString();      //  ＬＥＦＴ　ＨＡＮＤ
            //Console.WriteLine("left hand: " + arraySegment[0].Eul_Out.ToString());
            str += "||";
         //   str = "0.00 0.00 0.00 0.00 0.00 -0.00 -0.00 -0.00 0 -0.00 -0.00 -0.00 0.00 0.00 0.00 -0.00 -0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 -0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 90.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 -90.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00||";
          //  Console.WriteLine(str);
            
            return str;
        }
    }
}
