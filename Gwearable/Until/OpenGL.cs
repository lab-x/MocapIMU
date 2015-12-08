using System;
using System.Drawing;
using System.Windows.Forms;
using CsGL.OpenGL;
using Gwearable;
using System.Collections.Generic;   //ʹ������ʱInterop

//using arcball;

namespace MyOpenGL
{

    public class OpenGL : OpenGLControl
    {
        private void DrawPoint() // for gyro calibrate
        {
            GL.glPointSize(4.0f);
            GL.glBegin(GL.GL_POINTS);
            GL.glColor3f(1.0f, 1.0f, 1.0f);

            for (int i = 0; i < defineSerialPort.m_MagCollectionfordraw.Count; i++)
            {
                GL.glVertex3f(defineSerialPort.m_MagCollectionfordraw[i].X, defineSerialPort.m_MagCollectionfordraw[i].Y, defineSerialPort.m_MagCollectionfordraw[i].Z);
            }
            GL.glEnd();
            GL.glFlush();
        }

        private void DrawHeadPoint()
        {
            GL.glPointSize(4.0f);
            GL.glBegin(GL.GL_POINTS);
            GL.glColor3f(0.0f, 1.0f, 0.0f);

            for (int i = 0; i < defineSerialPort.m_GPSPointCollection.Count; i++)
            {
                GL.glVertex3f(defineSerialPort.m_GPSPointCollection[i].X, defineSerialPort.m_GPSPointCollection[i].Y, defineSerialPort.m_GPSPointCollection[i].Z);
            }
            GL.glEnd();
            GL.glFlush();
        }
        #region ��������

        float camera_distance= 5.0f;
	    float camera_yaw = 0.0f;
	    float camera_pitch = -20.0f;
        float TransX = 0.0f;
        float TransY = 0.5f;
        float TransZ = 0.0f;

        private Timer m_timer = new Timer();
        //private Point mouseEndDrag;
        private static int height = 0;
        private static int width = 0;

        //private System.Object matrixLock = new System.Object();
        //private float[] matrix = new float[16];

        private static bool isLeftDrag = false;
        private static bool isRightDrag = false;

        int last_mouse_x = 0;
        int last_mouse_y = 0;
        //public float animation_time = 0.0f;
        private BVH m_bvh = null;// = new BVH();

        public void SetBoneLen(int boneId,int xyz,float len)
        {
            if (m_bvh == null) return;
            
            m_bvh.SetBoneLen(boneId, xyz, len);
        }

        #endregion

        protected override void OnDoubleClick(EventArgs e)
        {
            base.OnDoubleClick(e);
            //base.Focus();
            bool hhaha = base.Focused;
        }

        public BVH GetBVH()
        {
            if (m_bvh!=null)
            {
                return m_bvh;
            }
            return null;
        }
        
        public OpenGL()
        {
            this.KeyDown += new KeyEventHandler(OurView_OnKeyDown);
            GC.Collect();

            //��ʼ��ʱ��ؼ�
            m_timer.Interval = Global.frame_speed; //�о�û�����ã�
            m_timer.Enabled = true;
            m_timer.Tick += new EventHandler(m_timer_Tick);

        }

        void m_timer_Tick(object sender, EventArgs e)
        {
            try
            {
                drawScene();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }
        //static int runtimes = 0;

        //��ʼ�������T-Pose
        string lastStr = "0.00 1.150 0.0 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 ||0.10 1.130 0.0 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 0.00 ||"; //Ŀ�ģ���¼���һ�ε����ݣ���ֹ������������ʱ��������ʧ����������ʱ��һֱʹ�����һ�ε����ݣ��ݶ�ok����

        //static double tpos = 0;

        private void drawScene()
        {
            //�������ʱ������һ��boolֵ�����true����ô��ʱ���������λ�ã����������λ�ã�Ȼ������gllookat��ͨ�������ļ���ʵ�����ʼ�ո��������������   ysjbug

//             tpos += 0.1;
//             if (tpos > 10)
//             {
//                 tpos = 0;
//             }

            GL.glClear(GL.GL_COLOR_BUFFER_BIT | GL.GL_DEPTH_BUFFER_BIT | GL.GL_STENCIL_BUFFER_BIT);

            GL.glMatrixMode(GL.GL_MODELVIEW);
            GL.glLoadIdentity();
            GL.glTranslatef(0.0f, 0.0f, -camera_distance);

            //GL.gluLookAt(tpos, 5, camera_distance, tpos, 0, 0, 0, 1, 0); //������֪��������ƶ�������������ˡ�������������������������꣬�Լ������������������

            GL.glRotatef(-camera_pitch, 1.0f, 0.0f, 0.0f);//��ֱ��ת��//
            GL.glRotatef(-camera_yaw, 0.0f, 1.0f, 0.0f);//ˮƽ��תЧ��//
            //GL.glTranslatef(0.0f, -0.5f, 0.0f);
            GL.glTranslatef(TransX,-TransY,TransZ);

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

            if (Global.StartShowHeadPoint)
            {
                DrawHeadPoint();
            }

            GL.glColor3f(0.90f, 0.90f, 0.90f);


            if (Global.StartShowPoint)
            {
                DrawPoint();
            }
            else
            {
                if (m_bvh != null)
                {
                   // m_bvh.LiveRenderFigure(lastStr, 0.03f); //ysj++ ������Ϊ�˲��ԣ�û�ж˿�ʱ��Ҳ�ܿ���ģ��.�ǵ�ɾ��

                    //���ǻط�
                    if (Global.isPlayBack)
                    {
                        m_bvh.RenderFigure(Global.frame_no, 0.03f);
                        if (Global.isFrontPlay)
                        {
                            Global.frame_no++;
                        }

                        if (Global.isBackPlay)
                        {
                            if (Global.frame_no > 0)
                            {
                                Global.frame_no--;
                            }
                        }

                        //int framecount = m_bvh.GetNumFrame();  //����˵�ǳ������㡣��ysj bug
                        //frame_no = frame_no % m_bvh.GetNumFrame();  //ѭ�������õ�
                        Global.frame_no = Global.frame_no % Global.frame_totalnum;  //ѭ�������õ�
                        //runtimes++;
                    }
                    //����ֱ��
                    if (Global.isLive)
                    {
                        string str = "";                        
                        bool issuccess = Global.m_strLiveQueue.TryDequeue(out str);
                        string tmpstr = "";
                        bool issuccess00 = Global.m_strLiveQueue.TryDequeue(out tmpstr);
                        bool issuccess01 = Global.m_strLiveQueue.TryDequeue(out tmpstr);
                        bool issuccess02 = Global.m_strLiveQueue.TryDequeue(out tmpstr);//1/4��֡��ʾ ysj+++
                        if (issuccess)
                        {
                            lastStr = str;
                            m_bvh.LiveRenderFigure(str, 0.03f);
                        }
                        else
                        {
                            //ʹ����һ�ε�����
                            if (!string.IsNullOrEmpty(lastStr))
                            {
                                m_bvh.LiveRenderFigure(lastStr, 0.03f);
                            }
                        }
                    }
                }
            }

            this.SwapBuffer();
        }
                
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
            float[]  light0_position = new float[]{ 10.0f, 10.0f, 10.0f, 1.0f };
            float[]  light0_diffuse = new float[]{ 0.8f, 0.8f, 0.8f, 1.0f };
            float[]  light0_specular=new float[] { 1.0f, 1.0f, 1.0f, 1.0f };
            float[]  light0_ambient=new float[]{ 0.1f, 0.1f, 0.1f, 1.0f };
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
            mouseControl.LRMouseMove +=new MouseEventHandler(glOnLRMouseMove);
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
            string filepath = Application.StartupPath + "\\MotionFiles\\Robot.bvh";
            m_bvh = new BVH(filepath);
            if (!m_bvh.IsLoadSuccess())
            {
                m_bvh = null;
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
                      
        private void glOnLRMouseMove(object sender, MouseEventArgs e)
        {
            //��껬��ʱ�����Ķ���

            if (isLeftDrag)
            {
                //ˮƽ��ת
                camera_yaw -= (e.X - last_mouse_x) * 0.1f;
                if (camera_yaw < 0.0)
                    camera_yaw += 360.0f;
                else if (camera_yaw > 360.0)
                    camera_yaw -= 360.0f;

                //��ֱ��ת
                camera_pitch -= (e.Y - last_mouse_y) * 1.0f;
                if (camera_pitch < -89.0f)
                    camera_pitch = -89.0f;
                else if (camera_pitch > 91.0f)
                    camera_pitch = 91.0f;
                //OutputDebugStringA("11111111111111111111\n");
            }
            //�Ҽ�����
            if (isRightDrag)
            {
                camera_distance += (e.Y - last_mouse_y) * 0.2f;
                if (camera_distance < 1.0f)
                    camera_distance = 1.0f;
            }

            //����м䰴��
            if (isRightDrag&&isLeftDrag)
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