using System;

//using System;
using System.Drawing;
using System.Windows.Forms;
using CsGL.OpenGL;
//使用运行时Interop


namespace Gwearable.Until
{
    class OpenGLEX : OpenGLControl
    {


        #region variable definitions

        float camera_distance = 5.0f;
        float camera_yaw = 0.0f;
        float camera_pitch = -20.0f;
        float TransX = 0.0f;
        float TransY = 0.5f;
        float TransZ = 0.0f;

        private Timer m_timer = new Timer();
        private Point mouseEndDrag;
        private static int height = 0;
        private static int width = 0;

        private System.Object matrixLock = new System.Object();
        private float[] matrix = new float[16];

        private static bool isLeftDrag = false;
        private static bool isRightDrag = false;

        int last_mouse_x = 0;
        int last_mouse_y = 0;
        public float animation_time = 0.0f;


        private MilkshapeModel m_MS3DMilk = null;

        #endregion

        public OpenGLEX()
        {
            this.KeyDown += new KeyEventHandler(OurView_OnKeyDown);
            GC.Collect();

            //m_timer.Interval = Global.frame_speed; //感觉没起作用？
            m_timer.Interval = 20; //感觉没起作用？
            m_timer.Enabled = true;
            m_timer.Tick += new EventHandler(m_timer_Tick);
        }

        void m_timer_Tick(object sender, EventArgs e)
        {
            try
            {
                drawScene();
                //Console.WriteLine("Global.frame_speed---" + Global.frame_speed.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        //初始化人物的T-Pose
        //string lastStr = "0.00 1.150 0.0 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 ||0.10 1.130 0.0 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 ||"; //目的：记录最后一次的数据，防止队列中无数据时，人物消失，当无数据时，一直使用最后一次的数据？暂定ok。。

        string lastStr = "-0.666667 -1.417522 0.000000 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.03 0.02 0.01 1.00 2.00 3.00 ||";
        //目的：记录最后一次的数据，防止队列中无数据时，人物消失，当无数据时，一直使用最后一次的数据？暂定ok。。

        //static double tpos = 0;
        //string str1 = "-0.141556 -1.428533 121.557121 69.38 4.23 15.73 -36.25 -36.81 -1.39 -14.88 34.18 -20.39 -18.73 1.20 9.42 3.42 -8.43 -6.42 -31.44 -3.32 1.08 -9.03 -1.01 -11.21 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 -45.28 0.96 18.03 0.00 0.00 0.00 -16.98 -23.23 -19.41 9.25 4.35 6.01 23.26 11.48 12.97 16.36 27.15 -17.39 1.36 -10.28 -43.68 10.46 11.26 -2.42 -43.27 7.40 -77.28 -26.08 7.96 5.50 -14.63 2.38 6.95||";
        //string str2 = "-0.141556 -1.428533 121.557121 61.10 3.54 13.53 -34.99 -35.62 -1.38 -9.70 30.26 -16.00 -14.80 6.26 7.10 4.37 -8.91 -3.95 -36.72 19.81 -4.74 -10.99 -10.49 -3.26 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 -44.93 2.54 15.12 0.00 0.00 0.00 -10.78 -19.44 -18.33 1.67 17.67 12.36 8.37 3.26 25.27 26.95 24.19 -27.42 17.75 -2.94 -45.28 8.82 9.82 -3.18 -40.38 8.20 -74.48 -25.09 9.70 5.91 -14.87 0.12 5.67||";
        //string str3 = "-0.141556 -1.428533 121.557121 36.17 0.41 -2.62 -37.04 -16.15 4.35 -7.88 9.76 -5.93 -10.18 13.23 3.30 4.67 -1.66 -1.65 -29.38 16.16 -2.73 -6.00 -10.38 6.53 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 -45.64 9.79 4.41 0.00 0.00 0.00 10.20 -7.69 -3.66 -4.58 11.82 11.50 4.63 0.62 27.39 18.61 10.03 -7.98 16.38 15.78 -2.09 11.76 8.88 -3.76 -25.19 7.56 -56.56 -19.40 11.13 2.84 -17.06 2.22 4.39||";


        private void drawScene()
        {
            GL.glClear(GL.GL_COLOR_BUFFER_BIT | GL.GL_DEPTH_BUFFER_BIT | GL.GL_STENCIL_BUFFER_BIT);

            GL.glMatrixMode(GL.GL_MODELVIEW);
            GL.glLoadIdentity();
            GL.glTranslatef(0.0f, 0.0f, -camera_distance);

            GL.glRotatef(-camera_pitch, 1.0f, 0.0f, 0.0f);//垂直旋转。//
            GL.glRotatef(-camera_yaw, 0.0f, 1.0f, 0.0f);//水平旋转效果//
            GL.glTranslatef(TransX, -TransY, TransZ);

            float[] light0_position = new float[4] { 10.0f, 10.0f, 10.0f, 1.0f };
            GL.glLightfv(GL.GL_LIGHT0, GL.GL_POSITION, light0_position);

            if (Global.isFloorVisible)
            {
                DrawGround();
            }

            if (Global.isCrossLineVisible)
            {
                DrawCoordinateLine();
            }

            //                 if (m_bvh != null)
            //                 {
            //                     //这是回放
            //                     if (Global.isPlayBack)
            //                     {
            //                         m_bvh.RenderFigure(Global.frame_no, 0.03f);            
            //                     }
            //                
            //                 }
            GL.glColor3f(0.9f, 0.9f, 0.0f);

            
            if (m_MS3DMilk != null)
            {
                
//                 if (runtime == 0)
//                 {
//                     delta+=0.01f;
// 
//                     m_MS3DMilk.TestRenderEx(0.01f);            
//                 }
//                 else if (runtime == 1)
//                 {
//                     m_MS3DMilk.advanceAnimation(lastStr,);  
//                     m_MS3DMilk.TestRenderEx(0.01f);             
//                 }


                //if (runtime == 0)
                //{
                //    m_MS3DMilk.advanceAnimation(str1);
                //}
                //else if (runtime == 1)
                //{
                //    m_MS3DMilk.advanceAnimation(str2);
                //}
                //else if (runtime == 2)
                //{
                //    m_MS3DMilk.advanceAnimation(str3);
                //}
                //else
                //{
                //    m_MS3DMilk.advanceAnimation(lastStr);
                //}


                //m_MS3DMilk.TestRenderEx(0.01f);   

                //runtime++;
                //runtime %= 4;

                m_MS3DMilk.advanceAnimation(lastStr);
                m_MS3DMilk.TestRenderEx(0.01f);

                if (Global.isLive)
                {
                    string str = "";
                    bool issuccess = Global.m_strLiveQueue.TryDequeue(out str);
                    string tmpstr = "";
                    bool issuccess00 = Global.m_strLiveQueue.TryDequeue(out tmpstr);
                    bool issuccess01 = Global.m_strLiveQueue.TryDequeue(out tmpstr);//1/3的帧显示 ysj+++
                    if (issuccess)
                    {
                        lastStr = str;
                        //m_bvh.LiveRenderFigure(str, 0.03f);
                        m_MS3DMilk.advanceAnimation(str);
                    }
                    else
                    {
                        //使用上一次的数据
                        if (!string.IsNullOrEmpty(lastStr))
                        {
                            //m_bvh.LiveRenderFigure(lastStr, 0.03f);
                            m_MS3DMilk.advanceAnimation(lastStr);

                        }
                    }


                    m_MS3DMilk.TestRenderEx(0.01f);
                }




                
            }

            this.SwapBuffer();
        }
        /// <summary>
        /// 画方格地板
        /// </summary>
        /// <returns></returns>
        private void DrawGround()
        {
            float size = 1.5f;
            int num_x = 40, num_z = 40;
            float ox, oz;

            //
            GL.glBegin(GL.GL_QUADS);
            GL.glNormal3f(0.0f, 1.0f, 0.0f);
            ox = -(num_x * size) / 2;
            for (int x = 0; x < num_x; x++, ox += size)
            {
                oz = -(num_z * size) / 2;
                for (int z = 0; z < num_z; z++, oz += size)
                {
                    if (((x + z) % 2) == 0)
                        GL.glColor3f(0.16f, 0.16f, 0.16f);
                    else
                        GL.glColor3f(0.2f, 0.2f, 0.2f);


                    GL.glVertex3f(ox, 0.0f, oz);
                    GL.glVertex3f(ox, 0.0f, oz + size);
                    GL.glVertex3f(ox + size, 0.0f, oz + size);
                    GL.glVertex3f(ox + size, 0.0f, oz);
                }
            }

            GL.glEnd();
        }

        /// <summary>
        /// 绘制坐标系
        /// </summary>
        /// <returns></returns>
        private void DrawCoordinateLine()
        {

            GL.glBegin(GL.GL_LINES);
            // light red x axis arrow
            GL.glColor3f(1.0f, 0.5f, .5f);
            GL.glVertex3f(0.0f, 0.0f, 0.0f);
            GL.glVertex3f(1.0f, 0.0f, 0.0f);
            // light green y axis arrow
            GL.glColor3f(.5f, 1.0f, 0.5f);
            GL.glVertex3f(0.0f, 0.0f, 0.0f);
            GL.glVertex3f(0.0f, 1.0f, 0.0f);
            // light blue z axis arrow
            GL.glColor3f(.5f, .5f, 1.0f);
            GL.glVertex3f(0.0f, 0.0f, 0.0f);
            GL.glVertex3f(0.0f, 0.0f, 1.0f);
            GL.glEnd();
            GL.glBegin(GL.GL_LINES);
            // x letter & arrowhead
            GL.glColor3f(1.0f, 0.5f, .5f);
            GL.glVertex3f(1.1f, 0.1f, 0.0f);
            GL.glVertex3f(1.3f, -0.1f, 0.0f);
            GL.glVertex3f(1.3f, 0.1f, 0.0f);
            GL.glVertex3f(1.1f, -0.1f, 0.0f);
            GL.glVertex3f(1.0f, 0.0f, 0.0f);
            GL.glVertex3f(0.9f, 0.1f, 0.0f);
            GL.glVertex3f(1.0f, 0.0f, 0.0f);
            GL.glVertex3f(0.9f, -0.1f, 0.0f);
            // y letter & arrowhead
            GL.glColor3f(.5f, 1.0f, 0.5f);
            GL.glVertex3f(-0.1f, 1.3f, 0.0f);
            GL.glVertex3f(0.0f, 1.2f, 0.0f);
            GL.glVertex3f(0.1f, 1.3f, 0.0f);
            GL.glVertex3f(0.0f, 1.2f, 0.0f);
            GL.glVertex3f(0.0f, 1.2f, 0.0f);
            GL.glVertex3f(0.0f, 1.1f, 0.0f);
            GL.glVertex3f(0.0f, 1.0f, 0.0f);
            GL.glVertex3f(0.1f, 0.9f, 0.0f);
            GL.glVertex3f(0.0f, 1.0f, 0.0f);
            GL.glVertex3f(-0.1f, 0.9f, 0.0f);
            // z letter & arrowhead
            GL.glColor3f(.5f, .5f, 1.0f);
            GL.glVertex3f(0.0f, -0.1f, 1.3f);
            GL.glVertex3f(0.0f, 0.1f, 1.3f);
            GL.glVertex3f(0.0f, 0.1f, 1.3f);
            GL.glVertex3f(0.0f, -0.1f, 1.1f);
            GL.glVertex3f(0.0f, -0.1f, 1.1f);
            GL.glVertex3f(0.0f, 0.1f, 1.1f);
            GL.glVertex3f(0.0f, 0.0f, 1.0f);
            GL.glVertex3f(0.0f, 0.1f, 0.9f);
            GL.glVertex3f(0.0f, 0.0f, 1.0f);
            GL.glVertex3f(0.0f, -0.1f, 0.9f);
            GL.glEnd();
        }

        private void init()
        {
            float[] light0_position = new float[] { 10.0f, 10.0f, 10.0f, 1.0f };
            float[] light0_diffuse = new float[] { 0.8f, 0.8f, 0.8f, 1.0f };
            float[] light0_specular = new float[] { 1.0f, 1.0f, 1.0f, 1.0f };
            float[] light0_ambient = new float[] { 0.1f, 0.1f, 0.1f, 1.0f };
            GL.glLightfv(GL.GL_LIGHT0, GL.GL_POSITION, light0_position);
            GL.glLightfv(GL.GL_LIGHT0, GL.GL_DIFFUSE, light0_diffuse);
            GL.glLightfv(GL.GL_LIGHT0, GL.GL_SPECULAR, light0_specular);
            GL.glLightfv(GL.GL_LIGHT0, GL.GL_AMBIENT, light0_ambient);
            GL.glEnable(GL.GL_LIGHT0);
            GL.glEnable(GL.GL_LIGHTING);
            GL.glEnable(GL.GL_COLOR_MATERIAL);
            GL.glEnable(GL.GL_DEPTH_TEST);
            GL.glCullFace(GL.GL_BACK);
            GL.glEnable(GL.GL_CULL_FACE);
            GL.glClearColor(0.1f, 0.1f, 0.1f, 1.0f);
        }

        protected override void InitGLContext()
        {
            init();
            drawScene();
            LoadModal();

            MouseControl mouseControl = new MouseControl(this);
            mouseControl.AddControl(this);
            mouseControl.LeftMouseDown += new MouseEventHandler(glOnLeftMouseDown);
            mouseControl.LeftMouseUp += new MouseEventHandler(glOnLeftMouseUp);
            mouseControl.RightMouseDown += new MouseEventHandler(glOnRightMouseDown);
            mouseControl.RightMouseUp += new MouseEventHandler(glOnRightMouseUp);
            mouseControl.LRMouseMove += new MouseEventHandler(glOnLRMouseMove);
            mouseControl.LRMouseWheel += new MouseEventHandler(glOnLRMouseWheel);

            //just for test ysj++
            //mouseControl.LeftClick += new EventHandler(mouseControl_LeftClick);

        }

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            Size s = Size;
            height = s.Height;
            width = s.Width;

            GL.glViewport(0, 0, width, height);
            //GL.glPushMatrix();
            GL.glMatrixMode(GL.GL_PROJECTION);
            GL.glLoadIdentity();
            GL.gluPerspective(45.0f, (double)width / (double)height, 1.0, 500.0);
            //GL.glTranslatef(0.0f, 0.0f, -4.0f);
            GL.glMatrixMode(GL.GL_MODELVIEW);
            //GL.glPopMatrix();

            GL.glClearDepth(1.0f);
            GL.glEnable(GL.GL_DEPTH_TEST);
        }



        public void LoadModal()
        {
            //m_MS3D = new MS3D();
            //bool returnvalue = false;
            //returnvalue = m_MS3D.LoadMS3DFile(Application.StartupPath + "\\MotionFiles\\dongzuo.smd", 0.01f);

            //if (!returnvalue)
            //{
            //    MessageBox.Show("读取失败");
            //}
            //{
            //    //读取成功了
            //    m_MS3D.SetupJoints();
            //}

            m_MS3DMilk = new MilkshapeModel(Application.StartupPath + "\\MotionFiles\\dongzuo.ms3d");


            if (m_MS3DMilk != null)
            {
                //MessageBox.Show("读取成功");
            }
            {
                //读取成功了
                //m_MS3D.SetupJoints();
            }


        }

        #region Keyboard Control

        protected void OurView_OnKeyDown(object Sender, KeyEventArgs kea)
        {
            if (kea.KeyCode == Keys.Escape)
            {
                this.Parent.Dispose();
            }

            if (kea.KeyCode == Keys.R)
            {
                //this.reset();
                //keyboardLEX();
            }
        }

        #endregion Keyboard Control

        #region Mouse Control

        private void startDrag(Point MousePt)
        {

        }

        private void drag(Point MousePt)
        {

        }

        public void glOnMouseMove(object sender, MouseEventArgs e)
        {
            Point tempAux = new Point(e.X, e.Y);
            mouseEndDrag = tempAux;


        }

        public void glOnLeftMouseDown(object sender, MouseEventArgs e)
        {
            isLeftDrag = true;
            last_mouse_x = e.X;
            last_mouse_y = e.Y;
        }

        public void glOnLeftMouseUp(object sender, MouseEventArgs e)
        {
            isLeftDrag = false;
        }

        private void glOnRightMouseDown(object sender, MouseEventArgs e)
        {
            last_mouse_x = e.X;
            last_mouse_y = e.Y;
            isRightDrag = true;
        }

        private void glOnRightMouseUp(object sender, MouseEventArgs e)
        {
            isRightDrag = false;
        }

        private void glOnLRMouseWheel(object sender, MouseEventArgs e)
        {
            if (e.Delta > 0)
            {
                camera_distance += 1;
            }
            else
            {
                camera_distance -= 1;
            }
            if (camera_distance < 2.0f)
                camera_distance = 2.0f;
        }

        //static int open = 0;
        // 
        //         void mouseControl_LeftClick(object sender, EventArgs e)
        //         {
        //             Console.WriteLine("鼠标点击启动---start");
        //             if (open == 0)
        //             {
        //                 //keyboardLEX();///ysj bug+++ //上传的代码 防止别人误点 先屏蔽掉赛
        //                 open = 1;
        //             }
        //             
        //             Console.WriteLine("鼠标点击启动---end");
        // 
        //         }


        private void glOnLRMouseMove(object sender, MouseEventArgs e)
        {
            //鼠标滑动时，做的动作

            if (isLeftDrag)
            {
                //水平旋转
                camera_yaw -= (e.X - last_mouse_x) * 0.1f;
                if (camera_yaw < 0.0)
                    camera_yaw += 360.0f;
                else if (camera_yaw > 360.0)
                    camera_yaw -= 360.0f;

                //垂直旋转
                camera_pitch -= (e.Y - last_mouse_y) * 1.0f;
                if (camera_pitch < -89.0f)
                    camera_pitch = -89.0f;
                else if (camera_pitch > 91.0f)
                    camera_pitch = 91.0f;
                //OutputDebugStringA("11111111111111111111\n");
            }
            //右键缩放
            if (isRightDrag)
            {
                camera_distance += (e.Y - last_mouse_y) * 0.2f;
                if (camera_distance < 1.0f)
                    camera_distance = 1.0f;
            }

            //鼠标中间按键
            if (isRightDrag && isLeftDrag)
            {
                TransX += (e.X - last_mouse_x) * 0.01f;
                TransY += (e.Y - last_mouse_y) * 0.01f;
            }

            last_mouse_x = e.X;
            last_mouse_y = e.Y;

            //glutPostRedisplay();
            //DrawFunc();
            drawScene();

        }
        #endregion

    }
}
