using System.Collections.Generic;
using System.Text;
using CsGL.OpenGL;
using System.IO;
using System.Windows.Forms;

namespace Gwearable
{
    public class BVH
    {
        public enum ChannelEnum
        {
            X_ROTATION, Y_ROTATION, Z_ROTATION,
            X_POSITION, Y_POSITION, Z_POSITION,
            UNKNOW
        }

        public class Joint
        {
            public string name = "";
            public int index = 0;
            public Joint parent = null;
            public List<Joint> children = new List<Joint>();
            public double[] offset = new double[3] { 0, 0, 0 };//size is 3
            public bool has_site = false;
            public double[] site = new double[] { 0, 0, 0 };//size is 3
            public List<Channel> channelstmp = new List<Channel>();

        }


        public class Channel
        {
            public Joint joint = null;
            public ChannelEnum type = ChannelEnum.UNKNOW;
            public int index = 0;
        }


        //int m_bufferlen = 4096;
        bool m_is_load_success = false;
        string m_file_name = "";
        string m_motion_name = "";
        static int m_num_channel = 0;
        List<Channel> m_channels = new List<Channel>();
        List<Joint> m_joints = new List<Joint>();
        Dictionary<string, Joint> m_joint_index = new Dictionary<string, Joint>();
        static int m_num_frame = 0;
        double m_interval = 0;
        List<double> m_motion = new List<double>();

        static double[] m_m_motion;

        public void SetBoneLen(int boneID,int xyz, float len)//0 表示x轴  y 1   z2
        {
            if (boneID > m_joints.Count) return;
            if (0 == xyz)
            {
                m_joints[boneID].offset[0] += len;    //ysj++
            }
            else if (1 == xyz)
            {
                m_joints[boneID].offset[1] += len;    //ysj++
            }
            else if (2 == xyz)
            {
                m_joints[boneID].offset[2] += len;    //ysj++
            }            
            
        }

        public bool IsLoadSuccess()
        {
            return m_is_load_success;
        }
        public string GetFileName()
        {
            return m_file_name;
        }

        public string GetMotionName()
        {
            return m_motion_name;
        }
        public int GetNumJoint()
        {
            return m_joints.Count;
        }
        public Joint GetJoint(int no)
        {
            return m_joints[no];
        }

        public int GetNumChannel()
        {
            return m_channels.Count;
        }

        public Channel GetChannel(int no)
        {
            return m_channels[no];
        }

        //未重写这个方法
        //  const Joint *   GetJoint( const string & j ) const  {
        //      map< string, Joint * >::const_iterator  i = joint_index.find( j );
        //      return  ( i != joint_index.end() ) ? (*i).second : NULL; }

        public Joint GetJoint(string j)
        {
            if (m_joint_index.ContainsKey(j))
            {
                return m_joint_index[j];
            }

            return null;
            //remove
            //dic.Remove(j);

        }

        //  const Joint *   GetJoint( const char * j ) const  {
        //      map< string, Joint * >::const_iterator  i = joint_index.find( j );
        //      return  ( i != joint_index.end() ) ? (*i).second : NULL; }


        //  int     GetNumFrame() const { return  num_frame; }
        //

        public int GetNumFrame()
        {
            return m_num_frame;
        }

        public double GetInterval()
        {
            return m_interval;
        }

        public double GetMotion(int f, int c)
        {
            return m_motion[f * m_num_channel + c]; //ysj+ bug
        }

        public void SetMotion(int f, int c, double v)
        {
            m_motion[f * m_num_channel + c] = v;//ysj+ bug
        }


        //begin/////////////////
        public BVH()
        {
            //motion = null;
            Clear();
        }
        public BVH(string bvh_file_name)
        {
            //motion = NULL;
            Clear();
            Load(bvh_file_name);
        }
        
        //该函数未用到
        public void Clear()
        {
            int i;
            for (i = 0; i < m_channels.Count; i++)
                //delete  channels[ i ];
                m_channels.RemoveAt(i);
            for (i = 0; i < m_joints.Count; i++)
                //delete  joints[ i ];
                m_joints.RemoveAt(i);

            //if ( motion != NULL )
            //delete  motion;  //如何删除呢  替换是最根本的办法

            m_is_load_success = false;

            m_file_name = "";
            m_motion_name = "";

            m_num_channel = 0;
            m_channels.Clear();
            m_joints.Clear();
            m_joint_index.Clear();

            m_num_frame = 0;
            m_interval = 0.0;
            //motion = NULL;
        }

       // double[] filecontent = new double[102400]; //很大的缓存区，用来存储待显示的帧数据


        public void Load(string bvh_file_name)
        {
            try
            {
                FileStream fs = new FileStream(bvh_file_name, FileMode.Open);
                StreamReader sr = new StreamReader(fs, Encoding.ASCII);



                char[] line = new char[4096];//
                string token;
                //string separater = " :,\t";

                List<Joint> joint_stack = new List<Joint>();
                Joint joint = null;
                Joint new_joint = new Joint();
                bool is_site = false;
                double x, y, z;
                int i;
                //Clear();// 第一次不用清理
                m_file_name = bvh_file_name;//这一段 是解析文件路径名的。最后计算出的是  text.bvh
                //const char *  mn_first = bvh_file_name;
                //const char *  mn_last = bvh_file_name + strlen( bvh_file_name );
                //if ( strrchr( bvh_file_name, '\\' ) != NULL )
                //    mn_first = strrchr( bvh_file_name, '\\' ) + 1;
                //else if ( strrchr( bvh_file_name, '/' ) != NULL )
                //    mn_first = strrchr( bvh_file_name, '/' ) + 1;
                //if ( strrchr( bvh_file_name, '.' ) != NULL )
                //    mn_last = strrchr( bvh_file_name, '.' );
                //if ( mn_last < mn_first )
                //    mn_last = bvh_file_name + strlen( bvh_file_name );
                //motion_name.assign( mn_first, mn_last );

                //file.open( bvh_file_name, ios::in );
                //byte[] filecontentEXXXX = new byte[102400]; //很大的缓存区，用来存储待显示的帧数据
                //srr.Read(filecontentEXXXX, 0, (int)file.Length);
                if (!fs.CanRead)
                {
                    return;
                }

                while (!sr.EndOfStream)
                {
                    token = sr.ReadLine();

                    Tokens f = new Tokens(token, new char[] { ' ' });


                    if (f.GetFirstNotEmptyString().Equals("{"))
                    {
                        joint_stack.Add(joint);
                        joint = new_joint;
                        continue;
                    }
                    if (f.GetFirstNotEmptyString().Equals("}"))
                    {
                        int lastindex = joint_stack.Count - 1;
                        joint = joint_stack[lastindex];
                        joint_stack.RemoveAt(lastindex);
                        is_site = false;
                        continue;
                    }

                    if ((f.GetFirstNotEmptyString().Equals("ROOT")) || (f.GetFirstNotEmptyString().Equals("JOINT")))
                    {
                        new_joint = new Joint();
                        new_joint.index = m_joints.Count;
                        new_joint.parent = joint;
                        new_joint.has_site = false;

                        new_joint.offset[0] = 0.0;
                        new_joint.offset[1] = 0.0;
                        new_joint.offset[2] = 0.0;

                        new_joint.site[0] = 0.0;
                        new_joint.site[1] = 0.0;
                        new_joint.site[2] = 0.0;


                        m_joints.Add(new_joint);
                        if (joint != null)
                            joint.children.Add(new_joint);

                        f.position++;
                        token = f.elements[f.position];

                        while (token.Equals(string.Empty))
                        {
                            f.position++;
                            token = f.elements[f.position];

                        }
                        if (token != string.Empty)
                        {
                            new_joint.name = token;
                        }
                        m_joint_index[new_joint.name] = new_joint;
                        continue;
                    }

                    if (f.GetFirstNotEmptyString().Equals("End"))
                    {
                        new_joint = joint;
                        is_site = true;
                        continue;
                    }

                    if (f.GetFirstNotEmptyString().Equals("OFFSET"))
                    {
                        f.position++;
                        token = f.elements[f.position++];

                        if (token != string.Empty)
                        {
                            x = double.Parse(token);
                        }
                        else
                        {
                            x = 0.0;
                        }
                        token = f.elements[f.position++];
                        if (token != string.Empty)
                        {
                            y = double.Parse(token);
                        }
                        else
                        {
                            y = 0.0;
                        }
                        token = f.elements[f.position];
                        if (token != string.Empty)
                        {
                            z = double.Parse(token);
                        }
                        else
                        {
                            z = 0.0;
                        }

                        if (is_site)
                        {
                            joint.has_site = true;
                            joint.site[0] = x;
                            joint.site[1] = y;
                            joint.site[2] = z;
                        }
                        else
                        {
                            joint.offset[0] = x;
                            joint.offset[1] = y;
                            joint.offset[2] = z;
                        }
                        continue;
                    }

                    if (f.GetFirstNotEmptyString().Equals("CHANNELS"))
                    {
                        f.position++;
                        token = f.elements[f.position++];

                        int joint_channels_size = int.Parse(token);

                        for (i = 0; i < joint_channels_size; i++)
                        {
                            Channel channel = new Channel();
                            channel.joint = joint;
                            channel.index = m_channels.Count;
                            m_channels.Add(channel);
                            joint.channelstmp.Add(channel);
                            token = f.elements[f.position++];
                            if (token.Equals("Xrotation"))
                                channel.type = ChannelEnum.X_ROTATION;
                            else if (token.Equals("Yrotation"))
                                channel.type = ChannelEnum.Y_ROTATION;
                            else if (token.Equals("Zrotation"))
                                channel.type = ChannelEnum.Z_ROTATION;
                            else if (token.Equals("Xposition"))
                                channel.type = ChannelEnum.X_POSITION;
                            else if (token.Equals("Yposition"))
                                channel.type = ChannelEnum.Y_POSITION;
                            else if (token.Equals("Zposition"))
                                channel.type = ChannelEnum.Z_POSITION;
                        }
                    }

                    if (f.GetFirstNotEmptyString().Equals("MOTION"))
                        break;

                }

                m_is_load_success = true;

                //获取帧数据
                //C# 读取每一帧的数据
                //token = sr.ReadLine();
                //Tokens ff = new Tokens(token, new char[] { ' ' });

                //if (ff.GetFirstNotEmptyString().Equals("Frames"))
                //{
                //    ff.position++;
                //    token = ff.elements[ff.position];
                //    m_num_frame = int.Parse(token);

                //    token = sr.ReadLine();
                //    Tokens fff = new Tokens(token, new char[] { ' ' });

                //    if (fff.GetFirstNotEmptyString().Equals("FrameTime"))
                //    {
                //        fff.position++;
                //        token = fff.elements[fff.position];
                //        m_interval = float.Parse(token);

                //        m_num_channel = m_channels.Count;
                //        //m_motion = new double[m_num_frame*];
                //        //filecontent
                //        m_m_motion = new double[m_num_channel * m_num_frame];

                //        for (i = 0; i < m_num_frame; i++)
                //        {
                //            //file.getline(line, BUFFER_LENGTH);
                //            //token = strtok(line, separater);

                //            //

                //            token = sr.ReadLine();
                //            Tokens fffff = new Tokens(token, new char[] { ' ', '\t', '|' });

                //            for (j = 0; j < m_num_channel; j++)
                //            {
                //                token = fffff.elements[fffff.position];
                //                while (token == string.Empty)
                //                {
                //                    fffff.position++;
                //                    token = fffff.elements[fffff.position];
                //                }

                //                m_m_motion[i * m_num_channel + j] = double.Parse(token);
                //                fffff.position++;
                //            }
                //        }

                //        sr.Close();
                //        fs.Close();
                //        m_is_load_success = true;
                //    }
                //}

            }
            catch (System.IO.FileNotFoundException ex)
            {
                MessageBox.Show(ex.Message);
                MessageBox.Show("逗比,没找到文件！");
                return;
            }
        }

        public static void LoadRawFile(string raw_file_name)
        {
            //获取帧数据
            //C# 读取每一帧的数据
            FileStream fs = new FileStream(Application.StartupPath + "\\MotionFiles\\" + raw_file_name, FileMode.Open);
            StreamReader sr = new StreamReader(fs, Encoding.ASCII);

            string token = null;

            token = sr.ReadLine();
            Tokens ff = new Tokens(token, new char[] { ' ' });

            if (ff.GetFirstNotEmptyString().Equals("Frames"))
            {
                ff.position++;
                token = ff.elements[ff.position];
                m_num_frame = int.Parse(token);

                Global.frame_totalnum = m_num_frame;

                token = sr.ReadLine();
                Tokens fff = new Tokens(token, new char[] { ' ' });

                if (fff.GetFirstNotEmptyString().Equals("FrameTime"))
                {
                    //不要时间间隔这个值


                    //fff.position++;
                    //token = fff.elements[fff.position];
                    //m_interval = float.Parse(token);

                    m_num_channel = 66;//  m_channels.Count;  //ysj 暂定66了
                    m_m_motion = new double[m_num_channel * m_num_frame]; // m_num_frame这个值 需要从文件中读取出来，每次记录文件的时候，都需要在最后添加这样一个标记

                    for (int i = 0; i < m_num_frame; i++)
                    {
                        //file.getline(line, BUFFER_LENGTH);
                        //token = strtok(line, separater);

                        token = sr.ReadLine();
                        Tokens fffff = new Tokens(token, new char[] { ' ', '\t', '|' });

                        for (int j = 0; j < m_num_channel; j++)
                        {
                            token = fffff.elements[fffff.position];
                            while (token == string.Empty)
                            {
                                fffff.position++;
                                token = fffff.elements[fffff.position];
                            }

                            m_m_motion[i * m_num_channel + j] = double.Parse(token);
                            fffff.position++;
                        }
                    }

                    sr.Close();
                    fs.Close();
                    //m_is_load_success = true;
                }
            }
        }

        public void RenderFigure(int frame_no, float scale)
        {
            double[] tmpData = new double[66];

            for (int i = 0; i < 66; i++) //每次复制66个数据（一帧的数据用来计算）
            {
                tmpData[i] = m_m_motion[frame_no * m_num_channel + i];
            }

            RenderFigure(m_joints[0], tmpData, scale);
        }

        public void LiveRenderFigure(string data, float scale)
        {
            //首先解析一帧数据
            if (data == null)
            {
                return;
            }
            string token = null;
            Tokens tmpff = new Tokens(data, new char[] { ' ', '\t', '|' });

            double[] tmpData = new double[66];
            for (int i = 0; i < 66; i++)
            {
                token = tmpff.elements[tmpff.position];
                while (token == string.Empty)
                {
                    tmpff.position++;
                    token = tmpff.elements[tmpff.position];
                }
                tmpData[i] = double.Parse(token);
                tmpff.position++;
            }

            RenderFigure(m_joints[0], tmpData, scale);

        }

        const int bodyradius = 6;

        public void RenderFigure(Joint joint, double[] data, float scale)
        {
            GL.glPushMatrix();
            if (joint.parent == null)
            {
                GL.glTranslated(data[0], data[1], data[2]);
            }
            else
            {
                GL.glTranslated(joint.offset[0] * scale, joint.offset[1] * scale, joint.offset[2] * scale);
            }

            int i;
            for (i = 0; i < joint.channelstmp.Count; i++)
            {
                Channel channel = joint.channelstmp[i];
                if (channel.type == ChannelEnum.X_ROTATION)
                    GL.glRotated(data[channel.index], 1.0, 0.0, 0.0);
                else if (channel.type == ChannelEnum.Y_ROTATION)
                    GL.glRotated(data[channel.index], 0.0, 1.0, 0.0);
                else if (channel.type == ChannelEnum.Z_ROTATION)
                    GL.glRotated(data[channel.index], 0.0, 0.0, 1.0);
            }

            if (joint.children.Count == 0)
            {
                if (joint.name == "Head")
                {
                    GL.glColor3d(0.0, 1.0, 0.5);
                    drawSphere(joint.site[0] * scale / 2, joint.site[1] * scale / 2, joint.site[2] * scale / 2, 0.13f, 10.0f, 10.0f);//绘制头部
                    
                    //drawSphere(joint.site[0] * scale / 2, joint.site[1] * scale / 2, joint.site[2] * scale / 2, 0.05f, 10.0f, 10.0f);//绘制眼睛
                    //drawSphere(joint.site[0] * scale / 2, joint.site[1] * scale / 2, joint.site[2] * scale / 2, 0.05f, 10.0f, 10.0f);//绘制眼睛
                    GL.glColor3d(0.0, 0.0, 0.0);//黑色的眼睛
                    drawSphere(-4*scale / 2, joint.site[1] * scale / 2, 5.0 * scale / 2, 0.05f, 10.0f, 10.0f);//绘制眼睛
                    drawSphere(4* scale / 2, joint.site[1] * scale / 2, 5.0 * scale / 2, 0.05f, 10.0f, 10.0f);//绘制眼睛

                    //drawHead(0.15);
                }
                else if (joint.name == "LeftAnkle" || joint.name == "RightAnkle")
                {
                    //GL.glColor3d(0.0, 0.0, 1.0);
                    //drawSphere(joint.site[0] * scale / 2, joint.site[1] * scale / 2, joint.site[2] * scale / 2, 0.1f, 10.0f, 10.0f);//绘制左右脚
                    if (joint.name == "LeftAnkle")
                    {
                        //GL.glColor3d(0.0, 1.0, 1.0);
                        drawLeftFoot(0.0, 0.0, joint.site[0] * scale / 2, joint.site[1] * scale / 2, joint.site[2] * scale / 2);
                    }
                    else
                    {
                        drawRightFoot(0.0, 0.0, joint.site[0] * scale / 2, joint.site[1] * scale / 2, joint.site[2] * scale / 2);
                    }
                }
                else if (joint.name == "RightWrist" || joint.name == "LeftWrist")
                {
                    //GL.glColor3d(0.0, 1.0, 1.0);
                    //drawSphere(joint.site[0] * scale / 2, joint.site[1] * scale / 2, joint.site[2] * scale / 2, 0.1f, 10.0f, 10.0f);//绘制左右手
                   // RenderBone(0.0f, 0.0f, 0.0f, joint.site[0] * scale / 2, joint.site[1] * scale / 2, joint.site[2] * scale / 2, 0.01, 0.01, 4, 20); //
                    //GL.glColor3d(0.0, 1.0, 1.0);
                    //RenderBone(0.0f, 0.0f, 0.0f, joint.site[0] * scale / 2, -joint.site[1] * scale / 2, -joint.site[2] * scale , 0.01, 0.01, 4, 20);
                    //drawFinger(0.0, 0.0, joint.site[0] * scale / 5, joint.site[1] * scale / 2, joint.site[2] * scale / 2);

                    //right -  left +
                    //将right的- 改为 正
                    if (joint.name == "RightWrist")
                    {
                        //GL.glColor3d(0.0, 1.0, 1.0);
                        drawRightFinger(0.0, 0.0, joint.site[0] * scale / 5, joint.site[1] * scale / 2, joint.site[2] * scale / 2);
                    }
                    else
                    {
                        //GL.glColor3d(0.0, 1.0, 1.0);
                        drawLeftFinger(0.0, 0.0, joint.site[0] * scale / 5, joint.site[1] * scale / 2, joint.site[2] * scale / 2);
                    }



                   // GL.glColor3d(0.0, 0.0, 1.0);
                    //RenderBone(0.0f, 0.0f, 0.0f, joint.site[0] * scale / 2, joint.site[1] * scale / 2, joint.site[2] * scale / 3, 0.01, 0.01, 4, 20);
                }
                //GL.glColor3d(1.0, 0.0, 0.0);//head hands foots 5 个点  已经验证
                //RenderBone(0.0f, 0.0f, 0.0f, joint.site[0] * scale, joint.site[1] * scale, joint.site[2] * scale);
                //drawSphere(joint.site[0] * scale / 2, joint.site[1] * scale / 2, joint.site[2] * scale / 2, 0.1f, 10.0f, 10.0f);

            }
            if (joint.children.Count == 1)
            {
                GL.glColor3d(1.0, 1.0, 0.0);//貌似是有一个孩子的  4个腿 和 4个 胳膊 部分都是绿色   暂定上腿的0.1---0.5 下推 0.5--0.3  上胳膊0.05-0.03 下胳膊0.03-0.02
                Joint child = joint.children[0];
                if (child.name == "RightKnee" || child.name == "LeftKnee")
                {
                    RenderBone(0.0f, 0.0f, 0.0f, child.offset[0] * scale, child.offset[1] * scale, child.offset[2] * scale, 0.08, 0.05, 4, 20);
                }
                else if (child.name == "RightAnkle" || child.name == "LeftAnkle")
                {
                    RenderBone(0.0f, 0.0f, 0.0f, child.offset[0] * scale, child.offset[1] * scale, child.offset[2] * scale, 0.05, 0.03, 4, 20);
                }
                else if (child.name == "RightShoulder" || child.name == "LeftShoulder")//RightShoulder
                {
                    RenderBone(0.0f, 0.0f, 0.0f, child.offset[0] * scale, child.offset[1] * scale, child.offset[2] * scale, 0.05, 0.05, 4, 20);
                }
                else if (child.name == "RightElbow" || child.name == "LeftElbow")
                {
                    RenderBone(0.0f, 0.0f, 0.0f, child.offset[0] * scale, child.offset[1] * scale, child.offset[2] * scale, 0.05, 0.04, 4, 20);
                }
                else if (child.name == "RightWrist" || child.name == "LeftWrist")//
                {
                    RenderBone(0.0f, 0.0f, 0.0f, child.offset[0] * scale, child.offset[1] * scale, child.offset[2] * scale, 0.04, 0.03, 4, 20);
                    //GL.glutSolidCube(0.5f);
                    //RenderBone(child.offset[0] * scale * 0.3, child.offset[1] * scale * 0.3, child.offset[2] * scale * 0.3, child.offset[0] * scale, child.offset[1] * scale, child.offset[2] * scale, 0.01, 0.01, 3, 20);//画手指头
                }
                else if (child.name == "Chest1")
                {
                    //GL.glColor3d(1.0, 0.0, 0.0); //红色
                    RenderBone(0.0f, 0.0f, 0.0f, child.offset[0] * scale, child.offset[1] * scale, child.offset[2] * scale, 0.11, 0.13, bodyradius, 20);
                }
                else if (child.name == "Chest2")
                {
                    //GL.glColor3d(1.0, 0.0, 1.0);//粉色
                    RenderBone(0.0f, 0.0f, 0.0f, child.offset[0] * scale, child.offset[1] * scale, child.offset[2] * scale, 0.13, 0.15, bodyradius, 20);
                }
                else if (child.name == "Chest3")
                {
                    //GL.glColor3d(1.0, 0.0, 1.0);//粉色
                    RenderBone(0.0f, 0.0f, 0.0f, child.offset[0] * scale, child.offset[1] * scale, child.offset[2] * scale, 0.15, 0.15, bodyradius, 20);
                }
                else if (child.name == "Head")
                {
                    //GL.glColor3d(1.0, 1.0, 1.0);
                    RenderBone(0.0f, 0.0f, 0.0f, child.offset[0] * scale, child.offset[1] * scale, child.offset[2] * scale, 0.05, 0.05, 3, 3);
                }
                else
                {
                    //GL.glColor3d(1.0, 1.0, 1.0);
                    RenderBone(0.0f, 0.0f, 0.0f, child.offset[0] * scale, child.offset[1] * scale, child.offset[2] * scale);
                }

            }
            if (joint.children.Count > 1)
            {
                double[] center = new double[3] { 0.0, 0.0, 0.0 };
                for (i = 0; i < joint.children.Count; i++)
                {
                    Joint child = joint.children[i];
                    center[0] += child.offset[0];
                    center[1] += child.offset[1];
                    center[2] += child.offset[2];
                }
                center[0] /= joint.children.Count + 1;
                center[1] /= joint.children.Count + 1;
                center[2] /= joint.children.Count + 1;
                
                if (joint.name == "Hips")
                {
                    //GL.glColor3d(0.0, 0.0, 1.0);// 蓝色 好像hip 和胸 是蓝色 其余不是
                    RenderBone(0.0f, 0.0f, 0.0f, center[0] * scale, center[1] * scale, center[2] * scale, 0.05, 0.15, 3, 3);//HIP附近的盖子
                }
                else if(joint.name == "Chest3")
                {
                    //chest3 走这里
                    //RenderBone(0.0f, 0.0f, 0.0f, center[0] * scale, center[1] * scale, center[2] * scale);//不走这里
                    RenderBone(0.0f, 0.0f, 0.0f, center[0] * scale, center[1] * scale, center[2] * scale, 0.15, 0.05, bodyradius, 3);//chest3附近的盖子
                }
                //RenderBone(0.0f, 0.0f, 0.0f, center[0] * scale, center[1] * scale, center[2] * scale);

                for (i = 0; i < joint.children.Count; i++)
                {
                    GL.glColor3d(1.0, 1.0, 0.0);//粉色  hip  和 neck 呢
                    Joint child = joint.children[i];
                    if (joint.name == "Hips")
                    {
                        if (child.name == "Chest")//根身躯结合的地方，上大 下小
                        {
                            RenderBone(center[0] * scale, center[1] * scale, center[2] * scale, child.offset[0] * scale, child.offset[1] * scale, child.offset[2] * scale, 0.02, 0.11, bodyradius, 3);
                        }
                        else
                        {
                            //lefthip  righthip 在这里绘
                            RenderBone(center[0] * scale, center[1] * scale, center[2] * scale, child.offset[0] * scale, child.offset[1] * scale, child.offset[2] * scale, 0.05, 0.05, 8, 3);
                            //drawDisk(child.offset[0] * scale, child.offset[1] * scale, child.offset[2] * scale, 0.0, 0.7, 23, 13);
                        }
                    }
                    else if (joint.name == "Chest3")
                    {
                        //GL.glColor3d(1.0, 1.0, 1.0);
                        RenderBone(center[0] * scale, center[1] * scale, center[2] * scale, child.offset[0] * scale, child.offset[1] * scale, child.offset[2] * scale, 0.05, 0.05, 3, 3);
                    }
                    else
                    {
                        //leftshoulder  rightshoulder在这里绘制
                        RenderBone(center[0] * scale, center[1] * scale, center[2] * scale, child.offset[0] * scale, child.offset[1] * scale, child.offset[2] * scale, 0.05, 0.1, 3, 3);
                    }

                    //RenderBone(center[0] * scale, center[1] * scale, center[2] * scale, child.offset[0] * scale, child.offset[1] * scale, child.offset[2] * scale);
                }
            }

            for (i = 0; i < joint.children.Count; i++)
            {
                RenderFigure(joint.children[i], data, scale);
            }

            GL.glPopMatrix();
        }


        void RenderBone(double x0, double y0, double z0, double x1, double y1, double z1)
        {
            double dir_x = x1 - x0;
            double dir_y = y1 - y0;
            double dir_z = z1 - z0;
            double bone_length = System.Math.Sqrt(dir_x * dir_x + dir_y * dir_y + dir_z * dir_z);

            GLUquadric quad_obj = new GLUquadric();

            //static GLUquadricObj *  quad_obj = NULL;
            if (quad_obj.data.ToInt32() == 0)
                quad_obj = GL.gluNewQuadric();

            //GL.gluQuadricDrawStyle(quad_obj, GL.GLU_FILL);
            //GL.gluQuadricNormals(quad_obj, GL.GLU_SMOOTH);
            GL.gluQuadricDrawStyle(quad_obj, GL.GLU_FILL); //GLU_SILHOUETTE  //GLU_POINT // GLU_FILL //GLU_LINE
            GL.gluQuadricNormals(quad_obj, GL.GLU_SMOOTH);

            GL.glPushMatrix();

            GL.glTranslated(x0, y0, z0);

            double length;
            length = System.Math.Sqrt(dir_x * dir_x + dir_y * dir_y + dir_z * dir_z);
            if (length < 0.0001)
            {
                dir_x = 0.0; dir_y = 0.0; dir_z = 1.0; length = 1.0;
            }
            dir_x /= length; dir_y /= length; dir_z /= length;

            double up_x, up_y, up_z;
            up_x = 0.0;
            up_y = 1.0;
            up_z = 0.0;

            double side_x, side_y, side_z;
            side_x = up_y * dir_z - up_z * dir_y;
            side_y = up_z * dir_x - up_x * dir_z;
            side_z = up_x * dir_y - up_y * dir_x;

            length = System.Math.Sqrt(side_x * side_x + side_y * side_y + side_z * side_z);
            if (length < 0.0001)
            {
                side_x = 1.0; side_y = 0.0; side_z = 0.0; length = 1.0;
            }
            side_x /= length; side_y /= length; side_z /= length;

            up_x = dir_y * side_z - dir_z * side_y;
            up_y = dir_z * side_x - dir_x * side_z;
            up_z = dir_x * side_y - dir_y * side_x;

            double[] m = new double[16]{ side_x, side_y, side_z, 0.0,
	                            up_x,   up_y,   up_z,   0.0,
	                            dir_x,  dir_y,  dir_z,  0.0,
	                            0.0,    0.0,    0.0,    1.0 };
            GL.glMultMatrixd(m);

            double radius = 0.1;
            int slices = 8;
            int stack = 3;

            GL.gluCylinder(quad_obj, radius, radius, bone_length, slices, stack);
            GL.glPopMatrix();
        }

        
        /************************************************************************/
        /* 带有圆柱体的上下面的半径大小的RenderBone函数                         */
        /************************************************************************/
        void RenderBone(double x0, double y0, double z0, double x1, double y1, double z1, double topradius, double bottomradius, int VLineNum, int HLineNum)
        {
            double dir_x = x1 - x0;
            double dir_y = y1 - y0;
            double dir_z = z1 - z0;
            double bone_length = System.Math.Sqrt(dir_x * dir_x + dir_y * dir_y + dir_z * dir_z);

            GLUquadric quad_obj = new GLUquadric();

            if (quad_obj.data.ToInt32() == 0)
                quad_obj = GL.gluNewQuadric();

            GL.gluQuadricDrawStyle(quad_obj, GL.GLU_FILL); //GLU_SILHOUETTE  //GLU_POINT // GLU_FILL //GLU_LINE
            GL.gluQuadricNormals(quad_obj, GL.GLU_SMOOTH);

            GL.glPushMatrix();

            GL.glTranslated(x0, y0, z0);

            double length;
            length = System.Math.Sqrt(dir_x * dir_x + dir_y * dir_y + dir_z * dir_z);
            if (length < 0.0001)
            {
                dir_x = 0.0; dir_y = 0.0; dir_z = 1.0; length = 1.0;
            }
            dir_x /= length; dir_y /= length; dir_z /= length;

            double up_x, up_y, up_z;
            up_x = 0.0;
            up_y = 1.0;
            up_z = 0.0;

            double side_x, side_y, side_z;
            side_x = up_y * dir_z - up_z * dir_y;
            side_y = up_z * dir_x - up_x * dir_z;
            side_z = up_x * dir_y - up_y * dir_x;

            length = System.Math.Sqrt(side_x * side_x + side_y * side_y + side_z * side_z);
            if (length < 0.0001)
            {
                side_x = 1.0; side_y = 0.0; side_z = 0.0; length = 1.0;
            }
            side_x /= length; side_y /= length; side_z /= length;

            up_x = dir_y * side_z - dir_z * side_y;
            up_y = dir_z * side_x - dir_x * side_z;
            up_z = dir_x * side_y - dir_y * side_x;

            double[] m = new double[16]{ side_x, side_y, side_z, 0.0,
	                            up_x,   up_y,   up_z,   0.0,
	                            dir_x,  dir_y,  dir_z,  0.0,
	                            0.0,    0.0,    0.0,    1.0 };
            GL.glMultMatrixd(m);

            //double radius = 0.1;//这个值，暂时不用了
            //int slices = 3;
            //int stack = 50;

            GL.gluCylinder(quad_obj, topradius, bottomradius, bone_length, VLineNum, HLineNum);
            //GL.gluDisk(quad_obj, bottomradius, bottomradius, VLineNum, HLineNum);
            GL.glPopMatrix();
        }

        void drawDisk(double x, double y, double z, double innerradius, double outerradius, int VLineNum, int HLineNum)
        {
            GLUquadric quad_obj = new GLUquadric();

            if (quad_obj.data.ToInt32() == 0)
                quad_obj = GL.gluNewQuadric();

            GL.gluQuadricDrawStyle(quad_obj, GL.GLU_FILL); //GLU_SILHOUETTE  //GLU_POINT // GLU_FILL //GLU_LINE
            GL.gluQuadricNormals(quad_obj, GL.GLU_SMOOTH);
            GL.glColor3d(1.0, 0, 0);
            //GL.glTranslated(x, y, z);
            GL.glRotated(0, 0, 0, 0);
            GL.gluDisk(quad_obj, innerradius, outerradius, VLineNum, HLineNum);
        }
        //球心坐标为（x，y，z），球的半径为radius，M，N分别表示球体的横纵向被分成多少份
        const float PI = 3.1415926f;
        void drawSphere(double xx, double yy, double zz, double radius, double M, double N)
        {
            double step_z = PI / M;
            double step_xy = 2 * PI / N;
            double[] x = new double[4];
            double[] y = new double[4];
            double[] z = new double[4];

            double angle_z = 0.0f;
            double angle_xy = 0.0f;
            int i = 0, j = 0;
            GL.glBegin(GL.GL_QUADS);
            for (i = 0; i < M; i++)
            {
                angle_z = i * step_z;

                for (j = 0; j < N; j++)
                {
                    angle_xy = j * step_xy;

                    x[0] = radius * System.Math.Sin(angle_z) * System.Math.Cos(angle_xy);
                    y[0] = radius * System.Math.Sin(angle_z) * System.Math.Sin(angle_xy);
                    z[0] = radius * System.Math.Cos(angle_z);

                    x[1] = radius * System.Math.Sin(angle_z + step_z) * System.Math.Cos(angle_xy);
                    y[1] = radius * System.Math.Sin(angle_z + step_z) * System.Math.Sin(angle_xy);
                    z[1] = radius * System.Math.Cos(angle_z + step_z);

                    x[2] = radius * System.Math.Sin(angle_z + step_z) * System.Math.Cos(angle_xy + step_xy);
                    y[2] = radius * System.Math.Sin(angle_z + step_z) * System.Math.Sin(angle_xy + step_xy);
                    z[2] = radius * System.Math.Cos(angle_z + step_z);

                    x[3] = radius * System.Math.Sin(angle_z) * System.Math.Cos(angle_xy + step_xy);
                    y[3] = radius * System.Math.Sin(angle_z) * System.Math.Sin(angle_xy + step_xy);
                    z[3] = radius * System.Math.Cos(angle_z);

                    for (int k = 0; k < 4; k++)
                    {
                        GL.glVertex3d(xx + x[k], yy + y[k], zz + z[k]);
                    }
                }
            }
            GL.glEnd();
        }

        void drawLeftFinger(double angle0, double angle1, double x, double y, double z)
        {
            GL.glTranslated(x, y, z);
            GL.glPushMatrix();
            GL.glScaled(0.1f, 0.01f, 0.05f);
            GL.glutSolidCube(1.0f);
            GL.glPopMatrix();

            GL.glTranslated(0.1, 0.0f, 0.0f);
            GL.glPushMatrix();
            GL.glScalef(0.1f, 0.01f, 0.01f);
            GL.glutSolidCube(1.0f);
            GL.glPopMatrix();

            GL.glColor3d(1.0, 0.0, 0.0);
            GL.glTranslated(0.0, 0.0f, 0.015f);
            GL.glPushMatrix();
            GL.glScalef(0.1f, 0.01f, 0.01f);
            GL.glutSolidCube(1.0f);
            GL.glPopMatrix();

            GL.glColor3d(1.0, 1.0, 0.0);
            GL.glRotated(angle1, 0.0f, 0.0f, 1.0f);
            GL.glTranslated(0.0, 0.0f, -0.03f);
            GL.glPushMatrix();
            GL.glScalef(0.05f, 0.01f, 0.01f);
            GL.glutSolidCube(1.0f);
            GL.glPopMatrix();
        }

        void drawRightFinger(double angle0, double angle1, double x, double y, double z)
        {
            GL.glTranslated(x, y, z);
            GL.glRotated(180, 0.0f, 1.0f, 0.0f);
            GL.glRotated(180, 1.0f, 0.0f, 0.0f);
            GL.glPushMatrix();
            GL.glScaled(0.1f, 0.01f, 0.05f);
            GL.glutSolidCube(1.0f);
            GL.glPopMatrix();

            GL.glTranslated(0.1, 0.0f, 0.0f);
            GL.glPushMatrix();
            GL.glScalef(0.1f, 0.01f, 0.01f);
            GL.glutSolidCube(1.0f);
            GL.glPopMatrix();

            GL.glColor3d(1.0, 0.0, 0.0);
            GL.glTranslated(0.0, 0.0f, 0.015f);
            GL.glPushMatrix();
            GL.glScalef(0.05f, 0.01f, 0.01f);
            GL.glutSolidCube(1.0f);
            GL.glPopMatrix();
            
            GL.glColor3d(1.0, 1.0, 0.0);
            GL.glRotated(angle1, 0.0f, 0.0f, 1.0f);
            GL.glTranslated(0.0, 0.0f, -0.03f);
            GL.glPushMatrix();
            GL.glScalef(0.05f, 0.01f, 0.01f);
            GL.glutSolidCube(1.0f);
            GL.glPopMatrix();
        }

        void drawLeftFoot(double angle0, double angle1, double x, double y, double z)
        {
            //y = 0.01;//脚与小腿之间的位移距离
            GL.glTranslated(x, y, z);
            GL.glRotated(-90, 0.0f, 1.0f, 0.0f);
            GL.glPushMatrix();
            GL.glScaled(0.1f, 0.01f, 0.05f);
            GL.glutSolidCube(1.0f);
            GL.glPopMatrix();

            GL.glTranslated(0.07, 0.0f, 0.0f);
            GL.glPushMatrix();
            GL.glutSolidCube(0.01f);
            GL.glPopMatrix();

            GL.glColor3d(1.0, 0.0, 0.0);
            GL.glTranslated(0.0, 0.0f, 0.015f);
            GL.glPushMatrix();
            GL.glutSolidCube(0.01f);
            GL.glPopMatrix();

            GL.glColor3d(1.0, 1.0, 0.0);
            GL.glRotated(angle1, 0.0f, 0.0f, 1.0f);
            GL.glTranslated(0.0, 0.0f, -0.03f);
            GL.glPushMatrix();
            GL.glutSolidCube(0.01f);
            GL.glPopMatrix();
        }

        void drawRightFoot(double angle0, double angle1, double x, double y, double z)
        {
            GL.glTranslated(x, y, z);
            GL.glRotated(-90, 0.0f, 1.0f, 0.0f);
            GL.glRotated(180, 1.0f, 0.0f, 0.0f);
            GL.glPushMatrix();
            GL.glScaled(0.1f, 0.01f, 0.05f);
            GL.glutSolidCube(1.0f);
            GL.glPopMatrix();

            GL.glTranslated(0.07, 0.0f, 0.0f);
            GL.glPushMatrix();
            GL.glutSolidCube(0.01f);
            GL.glPopMatrix();

            GL.glColor3d(1.0, 0.0, 0.0);
            GL.glTranslated(0.0, 0.0f, 0.015f);
            GL.glPushMatrix();
            GL.glutSolidCube(0.01f);
            GL.glPopMatrix();

            GL.glColor3d(1.0, 1.0, 0.0);
            GL.glRotated(angle1, 0.0f, 0.0f, 1.0f);
            GL.glTranslated(0.0, 0.0f, -0.03f);
            GL.glPushMatrix();
            GL.glutSolidCube(0.01f);
            GL.glPopMatrix();
        }

        void drawHead(double size)
        {
            GL.glutSolidCube(size);
        }

    }
}
