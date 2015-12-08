using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;

namespace Gwearable
{
    public delegate void UpdateText();

    public delegate void UpdateProgressBar();

    public partial class TPoseAPoseForm : Form
    {
        public UpdateText m_UpdateText = null;

        public UpdateProgressBar m_UpdateProgressBar = null;

        public System.Timers.Timer m_CountDownTime = new System.Timers.Timer(1000);

        private defineSerialPort m_SerialPort = null;

        //int TposeWaittime = 7;

        public TPoseAPoseForm(defineSerialPort serialport)
        {
            InitializeComponent();
            m_SerialPort = serialport;

            m_CountDownTime.Enabled = false;
            m_CountDownTime.AutoReset = true;
            m_CountDownTime.Elapsed += new System.Timers.ElapsedEventHandler(m_CountDownTime_Elapsed);

            this.m_UpdateText = new UpdateText(ChangePoseImage);
            //this.m_UpdateProgressBar = new UpdateProgressBar(TPoseProgressBarIncrease);
        }

        void m_CountDownTime_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            //StepRun(); 这个函数 没什么用处，多余了。
            //this.Invoke(m_UpdateProgressBar); 
            this.Invoke(m_UpdateText);
            //this.Invoke(m_UpdateText);
        }

        private void StepRun()
        {    
            m_SerialPort.m_WillSendQueue.Enqueue(m_SerialPort.CalcTotalSendString());            
        }

        private void TPoseProgressBarIncrease()
        {
            if (!Global.TposeDone && !Global.AposeDone)
            {
                if (CalibrateProcessBar.Value < 100)
                {
                    CalibrateProcessBar.Value += 20;
                    if (CalibrateProcessBar.Value == 100)
                    {
                        //CalibrateProcessBar.Value = 100;
                        //TposeWaittime = 8;
                    }
                }
            }

            if (Global.TposeDone && !Global.AposeDone)
            {
                StandardGesture.Image = Properties.Resources.APose;

                CalibrateProcessBar.Visible = false;
                APoseProgressBar.Visible = true;

                if (APoseProgressBar.Value < 100)
                {
                    APoseProgressBar.Value += 20;
                }
            }

            if (Global.TposeDone && Global.AposeDone)
            {
                FinishedCaliBtn.Enabled = true;

                //Global.isCalibrating = false;
                m_CountDownTime.Enabled = false;
            }
        }

        private void ChangeText()
        {
            //CountDownNum.Text = TposeWaittime.ToString();
            //if (TposeWaittime > 0)
            //{
            //    TposeWaittime--;
            //}
        }

        private void ChangePoseImage()
        {
            if (Global.SposeDone)
            {
                StandardGesture.Image = Properties.Resources.Tpose;
            }
            else if (Global.AposeDone)
            {
                StandardGesture.Image = Properties.Resources.SPose;
            }
            else if (Global.XposeDone)
            {
                StandardGesture.Image = Properties.Resources.APose;
            }
            else if (Global.TposeDone)
            {
                StandardGesture.Image = Properties.Resources.Xpose;
            }

        }

        private void StartCalibrateBtn_Click(object sender, EventArgs e)
        {
            #region Old 代码，不要删除，maybe 需要恢复

            /***/
            //m_CountDownTime.Enabled = true;
            ////Global.isCalibrating = true;
            //StartCalibrateBtn.Enabled = false;
            //FinishedCaliBtn.Enabled = false;

            //Global.CanTpose = true;
            //Global.CanXpose = true;
            //Global.CanApose = true;
            //Global.CanSpose = true;            
            //ModelsForm.Calibrate(); //ysj bug++
            #endregion
            Global.CanTpose = true;
            Global.CanXpose = false;
            Global.CanApose = false;
            Global.CanSpose = false;
            StartCalibrateBtn.Enabled = false;
            Xpose.Enabled = true;
            Apose.Enabled = false;
            Spose.Enabled = false;
            FinishedCaliBtn.Enabled = false;

            m_CountDownTime.Enabled = true;
            ModelsForm.Calibrate();
        }

        private void Xpose_Click(object sender, EventArgs e)
        {
            Global.CanTpose = false;
            Global.CanXpose = true;
            Global.CanApose = false;
            Global.CanSpose = false;

            StartCalibrateBtn.Enabled = false;
            Xpose.Enabled = false;
            Apose.Enabled = true;
            Spose.Enabled = false;
            FinishedCaliBtn.Enabled = false;
        }

        private void Apose_Click(object sender, EventArgs e)
        {
            Global.CanTpose = false;
            Global.CanXpose = false;
            Global.CanApose = true;
            Global.CanSpose = false;

            StartCalibrateBtn.Enabled = false;
            Xpose.Enabled = false;
            Apose.Enabled = false;
            Spose.Enabled = true;
            FinishedCaliBtn.Enabled = false;
            Global.height = (float)Global.HEAD.y;
        }

        private void Spose_Click(object sender, EventArgs e)
        {
            Global.CanTpose = false;
            Global.CanXpose = false;
            Global.CanApose = false;
            Global.CanSpose = true;

            StartCalibrateBtn.Enabled = false;
            Xpose.Enabled = false;
            Apose.Enabled = false;
            Spose.Enabled = false;
            FinishedCaliBtn.Enabled = true;

        }

        private void FinishedCaliBtn_Click(object sender, EventArgs e)
        {
            m_CountDownTime.Enabled = false;
            m_CountDownTime.Stop();

            this.DialogResult = DialogResult.OK;
            this.Close();
        }
    }
}
