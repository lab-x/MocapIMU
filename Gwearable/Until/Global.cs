using System;
using System.Collections.Concurrent;

namespace Gwearable
{
    class Global
    {
        public static bool ShouldersNChest = false;//true;
        public static double LegLength = 0;
        public static double LeftRight = 0;
        public static double FwdBwd = 0;

        public static double BodyUnit = 1.8 / 6.5;
        /************************************************************************/
        /* DEBUG FLAGS                                                          */
        /************************************************************************/
        public static bool DEBUG_AZIMUTH = false;
        public static bool DEBUG_Position = true;
        public static bool DEBUG_TPose = false;
        public static bool DEBUG_JumpDetect = false;
        public static bool DEBUG_FeetContact = false;
        public static bool DEBUG_DWWong = false;

        //DeQuiver 

        public static ConcurrentQueue<string> m_strQueue = new ConcurrentQueue<string>();
        public static ConcurrentQueue<string> m_strLiveQueue = new ConcurrentQueue<string>();
        //public static Queue<int> CurrentState = new Queue<int>();
        public static int NodeCount = 0;
        public static bool isCali = false;
        //测试开关
        public static bool testSwitch = false;
        //写文档标志
        public static bool isWriteable = false;
        //记录写文档的总帧数
        public static int TotalFrameNum = 0;
        //portname 用来在不同模块之间共享一些数据
        public static string PortName = "";
        public static string PortName_7 = "";
        public static string GPSPortName = "";
        //准备播放的文件的名称
        public static string PlayBackFileName = "";

        /************************************************************************/
        /* HIP POSITION and LEVERAGE PEDO    +    ABSOLUTE POSITION AIDING      */
        /************************************************************************/
        public static byte Info;
        public static bool Smooth = false;
        public static bool EnableSmooth = false;
        public static float Smooth_PrevParam = 3.0f;
        public static float Smooth_ThisParam = 1.0f;

        public static float height=1.5f;
        public static DateTime SaveDataStart = DateTime.Now;
        public static TimeSpan SaveDataElps = DateTime.Now - SaveDataStart;

        public static bool Save1 = false; 
        public static bool Save2 = false; 
        public static bool NoDataFlag = false;
        public static Matrix TransMat1 = new Matrix(3, 4);  
        public static Matrix TransMat2 = new Matrix(3, 4);
        public static Matrix TM1_33 = new Matrix(3, 3);
        public static Matrix TM2_33 = new Matrix(3, 3);


        public static Matrix TransMat2to1 = new Matrix(3, 4);
        
        public static bool TransMatAvailable = false;
        public static bool Station0 = false; 
        public static bool Station1 = false;
        
        public static int PosState = 1; // 0--CalcPosition 1--AbsPosition
        public static bool SmoothFlag = false; //

        public static bool Zero = false;
        public static bool ArrayAvailable = false; 
        public static bool ActivateAbsPos = false;
        public static bool EnableAbsPos = true;
        public static Pos HIP = new Pos(0, 0, 0);
        public static Pos HEAD = new Pos(0, 0, 0);
        public static CQuaternion QHEAD = new CQuaternion(1, 0, 0, 0); 
        public static Pos HEADRAW = new Pos(0, 0, 0);
        public static CQuaternion QHEADRAW = new CQuaternion(1, 0, 0, 0);
        public static CQuaternion QHEADRAWT1 = new CQuaternion(1, 0, 0, 0);
        public static CQuaternion QHEADRAWT2 = new CQuaternion(1, 0, 0, 0);
        public static CQuaternion invQHEADRAWT2 = new CQuaternion(1, 0, 0, 0);

        public static int FCflag = 0;

        /*   Gait Detector
        public static double[] Butterworth_b = { 0.1827, 0.1827 };
        public static double[] Butterworth_a = { 1.0, -0.6346 };
        public static double[] Left_Gyro = { 0, 0, 0, 0 }; 
        public static double[] Filted_Left_Gyro = { 0, 0, 0, 0 };
        public static int LC = 0;  
        
        public static double[] Right_Gyro = { 0, 0, 0, 0 };
        public static double[] Filted_Right_Gyro = { 0, 0, 0, 0 };
        public static int RC = 0;
        
        public static int[] State = { 0, 0 }; 
        public static int gaitCount = 0;  // previous and current
        */
        /************************************************************************/
        /* JUMP PARAMETERS                                                      */
        /************************************************************************/
        public static CQuaternion HipAcc = new CQuaternion();
        //public static CQuaternion QAZIMUTH;

        public static bool Fall = false;
        public static int JumpFlag = 0;
        public static double[] JumpHeight = { 1.13, 1.13, 1.35, 1.35, 1.5, 1.5, 1.5, 1.57, 1.57, 1.57, 1.60, 1.60, 1.60, 1.60, 1.57, 1.57, 1.57, 1.5, 1.5, 1.5, 1.35, 1.35 };  //7 elements
        public static int JumpCountDown = 22;

        public static DateTime AbsPosSigStart = DateTime.Now;
        public static TimeSpan AbsPosElps = DateTime.Now - AbsPosSigStart;
        
        
        public static DateTime JumpTimeStart = DateTime.Now;
        public static DateTime JumpTimeEnd = DateTime.Now;
        public static DateTime NoJumpStart = DateTime.Now;
        public static DateTime NoJumpEnd = DateTime.Now;
        public static int JStart = 0;


        public static int ii;   //Integral Index
        public static V3 velo = new V3(0, 0, 0);
        public static V3 S = new V3(0, 0, 0);
        public static bool TimerEnabled = true;
        public static bool IntgEnabled = false;
        public static DateTime YdropSig_Start;
        public static DateTime YpeakSig_Stop;

        public static DateTime Jump_Start;
        public static DateTime Jump_Stop;

        public static bool isJump = false;
        public static bool JumpFinished = false;
        public static bool Deploit = true;
        /************************************************************************/
        /* Calibration and Data Checking                                        */
        /************************************************************************/
        public static int FSR_Acc = 16;
        public static int TDoneCnt = 0;
        public static int XDoneCnt = 0;
        public static int ADoneCnt = 0;
        public static int SDoneCnt = 0;
        
        public static bool TposeDone = false;
        public static bool XposeDone = false;
        public static bool AposeDone = false;
        public static bool SposeDone = false;

        public static bool CanTpose = false;
        public static bool CanXpose = false;
        public static bool CanApose = false;
        public static bool CanSpose = false;

        public static bool XposeAvail = false;
        public static bool AposeAvail = false;
        public static bool SposeAvail = false;
        public static CQuaternion Qtc = new CQuaternion(1,0,0,0);
        public static CQuaternion invQtc = new CQuaternion();
        public static int CalibrateFlag = 0;

       
        public static DateTime CaliStart = DateTime.Now;
        public static DateTime CaliEnd = DateTime.Now;
        



        public static int[] IDSort = new int[17] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 };
        /************************************************************************/
        /* 首次打开port按钮的情形，以及控制开关                                 */
        /************************************************************************/
        public static bool isLive = false;
        public static bool isPlayBack = false;
        public static bool isRemote = false;

        /************************************************************************/
        /* 播放操作标示                                                         */
        /************************************************************************/
        public static bool isFrontPlay = false;
        public static bool isBackPlay = false;
        public static bool isFrontPause = false;
        public static bool isBackPause = false;
        public static bool isPreFrame = false;
        public static bool isNextFrame = false;
        public static bool isFirstFrame = false;
        public static bool isLastFrame = false;

        public static int frame_no = 0;   //设定为全局，方便了 我在别的窗口控制opengl窗口的操作，但是弊端也有，暂时这样
        public static int frame_totalnum = 0;


        public static int frame_speed = 10;//默认是100帧的数据
        public static int frame_originspeed = 10;//默认是100帧的数据

        /************************************************************************/
        /* 是否显示地板等系列按钮操作                                           */
        /************************************************************************/
        public static bool isFloorVisible = true;
        public static bool isCrossLineVisible = true;
        public static bool isShadowVisible = false;
        public static bool isCameraVisible = false;
        public static bool isGravityVisible = false;


        /************************************************************************/
        /* 下位机模式                                                           */
        /************************************************************************/
        public static string CmdMode = "normalmode";  //or developmode


        /************************************************************************/
        /* 校准状态标记                                                         */
        /************************************************************************/
        //public static bool isCalibrating = false;



        /************************************************************************/
        /* 校准磁场的标示                                                       */
        /************************************************************************/
        public static bool StartShowPoint = false;

        public static bool StartShowHeadPoint = false; //是否显示GPS数据，头部的运动轨迹

        /************************************************************************/
        /* 采集数据时Label显示的内容                                            */
        /************************************************************************/
        public static string CollectDataStatus = "......";


        public static int SelectedSensorId = -1;

        /************************************************************************/
        /* 下位机模式《正常or开发者》                                           */
        /************************************************************************/
        public static bool isDevelopMode = false;

        public static bool isClearMode = false;

        public static bool isUpdateMode = false;

        public static bool isCollectMode = false;

        //校验陀螺模式
        public static bool isStartCaligyroMode = false;
        public static bool isCaliinggyroMode = false;
        public static bool isCaligyroFinishedMode = false;

        public static int[] CaliCountArray = new int[17] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0}; 
        public static bool[] isGyroCaliingFlagArray = new bool[17] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false};
        public static bool[] isGyroCaliFinishedFlagArray = new bool[17] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };
        //是否成功退出标志
        public static bool[] isGyroCaliFinishedAndExitFlagArray = new bool[17] { false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false, false };



        public static Pos firstpos0 = new Pos();
        public static Pos secondpos0 = new Pos();
        public static Pos thirdpos0 = new Pos();

        public static Pos firstpos1 = new Pos();
        public static Pos secondpos1 = new Pos();
        public static Pos thirdpos1 = new Pos();
    }
}
