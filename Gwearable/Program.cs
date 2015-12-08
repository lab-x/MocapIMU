using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace Gwearable
{
    //     static class Program
    //     {
    //         /// <summary>
    //         /// 应用程序的主入口点。
    //         /// </summary>
    //         [STAThread]
    //         static void Main()
    //         {
    //             Application.EnableVisualStyles();
    //             Application.SetCompatibleTextRenderingDefault(false);
    //             Application.Run(new Mainform());
    //         }
    //     }

    
    static class Program
    {
        /// 主程序的入口点在此设置，包括一些初始化操作，启动窗体等
        private static ApplicationContext context;
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();                       //样式设置
            Application.SetCompatibleTextRenderingDefault(false);   //样式设置
            SplashForm sp = new SplashForm();                                 //启动窗体
            sp.Show();                                              //显示启动窗体
            context = new ApplicationContext();
            context.Tag = sp;
            Application.Idle += new EventHandler(Application_Idle); //注册程序运行空闲去执行主程序窗体相应初始化代码
            Application.Run(context);
        }

        //初始化等待处理函数
        private static void Application_Idle(object sender, EventArgs e)
        {
            Application.Idle -= new EventHandler(Application_Idle);
            if (context.MainForm == null)
            {
                //Thread.Sleep(2000);
                MainForm mw = new MainForm();
                context.MainForm = mw;
                mw.Init();                                  //主窗体要做的初始化事情在这里，该方法在主窗体里应该申明为public
                Thread.Sleep(2000);
                SplashForm sp = (SplashForm)context.Tag;
                sp.Close();                                 //关闭启动窗体 
                              

                mw.Show();                                  //启动主程序窗体
            }
        }
    }
    
}
