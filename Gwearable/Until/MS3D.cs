using System;
using System.Collections.Generic;
using System.IO;
using CsGL.OpenGL;

namespace Gwearable.Until
{
    public class JointEX
    {
        public int Id;
        public string Name;
        public int ParentId;

        public Vector3fEX location;
        public Vector3fEX rotation;

        public Vector3fEX initlocation;
        public Vector3fEX initrotation;

        public MilkshapeMatrix3x4 m_StaLocalMartic;
        public MilkshapeMatrix3x4 m_StaGlobaclMartic;
        public MilkshapeMatrix3x4 m_CurLocalMartic;
        public MilkshapeMatrix3x4 m_CurGlobMartic;

        public JointEX(int id, string name, int parentid)
        {
            Id = id;
            Name = name;
            ParentId = parentid;
        }

    }

    public class MilkshapeMatrix3x4
    {
        public float[][] v;
        public MilkshapeMatrix3x4()
        {
            v = new float[3][];
            for (int i = 0; i < 3; i++)
            {
                v[i] = new float[4];
            }
        }
    }

    public class MatrixEx   //4x4的矩阵
    {
        public Vector3fEX xBasis, yBasis, zBasis;
        public float[][] v;
        public MatrixEx()
        {
            v = new float[4][];
            for (int i = 0; i < 4; i++)
            {
                v[i] = new float[4];
            }

            v[3][3] = 1.0f;
        }

        public Vector3fEX origin;
        public static MatrixEx Mult(MatrixEx M1, MatrixEx M2)
        {
            MatrixEx tmp = new MatrixEx();

            tmp.v[0][0] = M1.v[0][0] * M2.v[0][0] + M1.v[0][1] * M2.v[1][0] + M1.v[0][2] * M2.v[2][0] + M1.v[0][3] * M2.v[3][0];
            tmp.v[0][1] = M1.v[0][0] * M2.v[0][1] + M1.v[0][1] * M2.v[1][1] + M1.v[0][2] * M2.v[2][1] + M1.v[0][3] * M2.v[3][1];
            tmp.v[0][2] = M1.v[0][0] * M2.v[0][2] + M1.v[0][1] * M2.v[1][2] + M1.v[0][2] * M2.v[2][2] + M1.v[0][3] * M2.v[3][2];
            tmp.v[0][3] = M1.v[0][0] * M2.v[0][3] + M1.v[0][1] * M2.v[1][3] + M1.v[0][2] * M2.v[2][3] + M1.v[0][3] * M2.v[3][3];

            tmp.v[1][0] = M1.v[1][0] * M2.v[0][0] + M1.v[1][1] * M2.v[1][0] + M1.v[1][2] * M2.v[2][0] + M1.v[1][3] * M2.v[3][0];
            tmp.v[1][1] = M1.v[1][0] * M2.v[0][1] + M1.v[1][1] * M2.v[1][1] + M1.v[1][2] * M2.v[2][1] + M1.v[1][3] * M2.v[3][1];
            tmp.v[1][2] = M1.v[1][0] * M2.v[0][2] + M1.v[1][1] * M2.v[1][2] + M1.v[1][2] * M2.v[2][2] + M1.v[1][3] * M2.v[3][2];
            tmp.v[1][3] = M1.v[1][0] * M2.v[0][3] + M1.v[1][1] * M2.v[1][3] + M1.v[1][2] * M2.v[2][3] + M1.v[1][3] * M2.v[3][3];

            tmp.v[2][0] = M1.v[2][0] * M2.v[0][0] + M1.v[2][1] * M2.v[1][0] + M1.v[2][2] * M2.v[2][0] + M1.v[2][3] * M2.v[3][0];
            tmp.v[2][1] = M1.v[2][0] * M2.v[0][1] + M1.v[2][1] * M2.v[1][1] + M1.v[2][2] * M2.v[2][1] + M1.v[2][3] * M2.v[3][1];
            tmp.v[2][2] = M1.v[2][0] * M2.v[0][2] + M1.v[2][1] * M2.v[1][2] + M1.v[2][2] * M2.v[2][2] + M1.v[2][3] * M2.v[3][2];
            tmp.v[2][3] = M1.v[2][0] * M2.v[0][3] + M1.v[2][1] * M2.v[1][3] + M1.v[2][2] * M2.v[2][3] + M1.v[2][3] * M2.v[3][3];

            tmp.v[3][0] = M1.v[3][0] * M2.v[0][0] + M1.v[3][1] * M2.v[1][0] + M1.v[3][2] * M2.v[2][0] + M1.v[3][3] * M2.v[3][0];
            tmp.v[3][1] = M1.v[3][0] * M2.v[0][1] + M1.v[3][1] * M2.v[1][1] + M1.v[3][2] * M2.v[2][1] + M1.v[3][3] * M2.v[3][1];
            tmp.v[3][2] = M1.v[3][0] * M2.v[0][2] + M1.v[3][1] * M2.v[1][2] + M1.v[3][2] * M2.v[2][2] + M1.v[3][3] * M2.v[3][2];
            tmp.v[3][3] = M1.v[3][0] * M2.v[0][3] + M1.v[3][1] * M2.v[1][3] + M1.v[3][2] * M2.v[2][3] + M1.v[3][3] * M2.v[3][3];

            return tmp;
        }

        public MatrixEx ReverseMat(MatrixEx M1)
        {
            //MatrixExOrg.origin

            return M1;
        }
    }

    public class Vector3fEX
    {
        public float x, y, z;
        public Vector3fEX()
        {
            x = 0.0f;
            y = 0.0f;
            z = 0.0f;
        }
        public Vector3fEX(float xx, float yy, float zz)
        {
            x = xx;
            y = yy;
            z = zz;
        }
        public static void cross(Vector3fEX Result, Vector3fEX v1, Vector3fEX v2)
        {
            Result.x = (v1.y * v2.z) - (v1.z * v2.y);
            Result.y = (v1.z * v2.x) - (v1.x * v2.z);
            Result.z = (v1.x * v2.y) - (v1.y * v2.x);
        }
        public static float dot(Vector3fEX v1, Vector3fEX v2)
        {
            return (v1.x * v2.x) + (v1.y * v2.y) + (v1.z + v2.z);
        }
        public virtual float length()
        {
            return (float)System.Math.Sqrt(x * x + y * y + z * z);
        }
    }

    public class Vector4fEX
    {
        public float x, y, z, w;
        public Vector4fEX()
        {
            x = 0.0f;
            y = 0.0f;
            z = 0.0f;
            w = 0.0f;
        }
        public Vector4fEX(float xx, float yy, float zz, float ww)
        {
            x = xx;
            y = yy;
            z = zz;
            w = ww;
        }
    }


    public class TriangleEX
    {
        public int Id;
        public int Weight;//权重值,目前可以设置为1.
        public Vector3fEX TriLocation;
        public Vector3fEX TriNormal;


        public Vector3fEX initTriLocation;
        public Vector3fEX initTriNormal;

        //public float s;//暂时可不用
        //public float t;//暂时可不用
        public int relationbonenum; //跟该三角形有关联的骨骼节点的个数，可以一个可以多个，一般是前者

    }

    public class MS3D
    {
        public List<JointEX> m_Joint = new List<JointEX>();
        public List<TriangleEX> m_TriangleEX = new List<TriangleEX>();

        public bool LoadMS3DFile(string filepath, float scale)
        {
            FileStream fs = new FileStream(filepath, FileMode.Open);
            StreamReader sr = new StreamReader(fs);
            string tmp = sr.ReadLine();

            if (tmp.Contains("version"))
            {
                //sr.ReadLine();
            }

            if (sr.ReadLine() == "nodes")
            {
                string lineData = sr.ReadLine();
                while (lineData != "end")
                {
                    Tokens f = new Tokens(lineData, new char[] { ' ' });
                    JointEX joint = new JointEX(int.Parse(f.elements[0]), f.elements[1], int.Parse(f.elements[2]));
                    m_Joint.Add(joint);
                    lineData = sr.ReadLine();
                }
            }

            if (sr.ReadLine() == "skeleton")
            {
                string skeletonData = sr.ReadLine();
                if (skeletonData.Contains("time"))
                {
                    skeletonData = sr.ReadLine();
                }
                while (skeletonData != "end")
                {
                    Tokens f = new Tokens(skeletonData, new char[] { ' ' });
                    m_Joint[int.Parse(f.elements[0])].location = new Vector3fEX(float.Parse(f.elements[1]) * scale, float.Parse(f.elements[2]) * scale, float.Parse(f.elements[3]) * scale);
                    m_Joint[int.Parse(f.elements[0])].rotation = new Vector3fEX(float.Parse(f.elements[4]), float.Parse(f.elements[5]), float.Parse(f.elements[6]));

                    m_Joint[int.Parse(f.elements[0])].initlocation = new Vector3fEX(float.Parse(f.elements[1]) * scale, float.Parse(f.elements[2]) * scale, float.Parse(f.elements[3]) * scale);//校准时候调用，还原姿态
                    m_Joint[int.Parse(f.elements[0])].initrotation = new Vector3fEX(float.Parse(f.elements[4]), float.Parse(f.elements[5]), float.Parse(f.elements[6]));//校准时候调用，还原姿态

                    skeletonData = sr.ReadLine();
                }
            }
            if (sr.ReadLine() == "triangles")
            {
                string TriangleEXdata = sr.ReadLine();
                while (TriangleEXdata != "end")
                {
                    Tokens f = new Tokens(TriangleEXdata, new char[] { ' ' });
                    if (f.elements[0] == "default.bmp" || f.elements[0] == string.Empty)
                    {
                        TriangleEXdata = sr.ReadLine();
                        continue;
                    }
                    TriangleEX TriangleEX = new TriangleEX();
                    TriangleEX.TriLocation = new Vector3fEX(float.Parse(f.elements[1]) * scale, float.Parse(f.elements[2]) * scale, float.Parse(f.elements[3]) * scale);
                    TriangleEX.TriNormal = new Vector3fEX(float.Parse(f.elements[4]), float.Parse(f.elements[5]), float.Parse(f.elements[6]));

                    TriangleEX.initTriLocation = new Vector3fEX(float.Parse(f.elements[1]) * scale, float.Parse(f.elements[2]) * scale, float.Parse(f.elements[3]) * scale);//校准时候调用，还原姿态
                    TriangleEX.initTriNormal = new Vector3fEX(float.Parse(f.elements[4]), float.Parse(f.elements[5]), float.Parse(f.elements[6]));//校准时候调用，还原姿态

                    TriangleEX.relationbonenum = int.Parse(f.elements[9]);
                    TriangleEX.Id = int.Parse(f.elements[10]);
                    //TriangleEX.Weight = int.Parse(f.elements[11]); //提示格式不正确
                    TriangleEX.Weight = 1;
                    m_TriangleEX.Add(TriangleEX);
                    TriangleEXdata = sr.ReadLine();
                }

                return true;
            }

            return false;
        }

        public void Render()
        {
            for (int i = 0; i < m_TriangleEX.Count; i += 3)
            {
                GL.glBegin(GL.GL_TRIANGLES);
                GL.glNormal3f(m_TriangleEX[i].TriNormal.x, m_TriangleEX[i].TriNormal.y, m_TriangleEX[i].TriNormal.z);
                GL.glVertex3f(m_TriangleEX[i].TriLocation.x, m_TriangleEX[i].TriLocation.y, m_TriangleEX[i].TriLocation.z);
                GL.glVertex3f(m_TriangleEX[i + 1].TriLocation.x, m_TriangleEX[i + 1].TriLocation.y, m_TriangleEX[i + 1].TriLocation.z);
                GL.glVertex3f(m_TriangleEX[i + 2].TriLocation.x, m_TriangleEX[i + 2].TriLocation.y, m_TriangleEX[i + 2].TriLocation.z);
                GL.glEnd();
            }
        }
        //校准时候调用，还原到初始姿态
        public void CarlRender()
        {
            for (int i = 0; i < m_TriangleEX.Count; i += 3)
            {
                GL.glBegin(GL.GL_TRIANGLES);
                GL.glNormal3f(m_TriangleEX[i].initTriNormal.x, m_TriangleEX[i].initTriNormal.y, m_TriangleEX[i].initTriNormal.z);
                GL.glVertex3f(m_TriangleEX[i].initTriLocation.x, m_TriangleEX[i].initTriLocation.y, m_TriangleEX[i].initTriLocation.z);
                GL.glVertex3f(m_TriangleEX[i + 1].initTriLocation.x, m_TriangleEX[i + 1].initTriLocation.y, m_TriangleEX[i + 1].initTriLocation.z);
                GL.glVertex3f(m_TriangleEX[i + 2].initTriLocation.x, m_TriangleEX[i + 2].initTriLocation.y, m_TriangleEX[i + 2].initTriLocation.z);
                GL.glEnd();
            }
        }

        private Vector3fEX VectorIRotate(Vector3fEX in1, MilkshapeMatrix3x4 in2)
        {
            Vector3fEX outVert = new Vector3fEX();
            outVert.x = in1.x * in2.v[0][0] + in1.y * in2.v[1][0] + in1.z * in2.v[2][0];
            outVert.y = in1.x * in2.v[0][1] + in1.y * in2.v[1][1] + in1.z * in2.v[2][1];
            outVert.z = in1.x * in2.v[0][2] + in1.y * in2.v[1][2] + in1.z * in2.v[2][2];
            return outVert;
        }

        private Vector3fEX VectorTransform(Vector3fEX in1, MilkshapeMatrix3x4 in2)
        {
            Vector3fEX outVert = new Vector3fEX();
            outVert.x = (in1.x * in2.v[0][0] + in1.y * in2.v[0][1] + in1.z * in2.v[0][2]) + in2.v[0][3];
            outVert.y = (in1.x * in2.v[1][0] + in1.y * in2.v[1][1] + in1.z * in2.v[1][2]) + in2.v[1][3];
            outVert.z = (in1.x * in2.v[2][0] + in1.y * in2.v[2][1] + in1.z * in2.v[2][2]) + in2.v[2][3];

            return outVert;
        }

        private Vector3fEX VectorITransform(Vector3fEX in1, MilkshapeMatrix3x4 in2)
        {
            Vector3fEX outVect = new Vector3fEX();
            Vector3fEX tmp = new Vector3fEX();
            tmp.x = in1.x - in2.v[0][3];
            tmp.y = in1.y - in2.v[1][3];
            tmp.z = in1.z - in2.v[2][3];
            outVect = VectorIRotate(tmp, in2);
            return outVect;
        }

        bool isfirstrun = true;

        private void UpdateVertex()
        {
            //isfirstrun = false;

            for (int i = 0; i < m_TriangleEX.Count; i++)
            {
                Vector3fEX tmp = new Vector3fEX();
                Vector3fEX vert = new Vector3fEX();
                Vector3fEX norm = new Vector3fEX();

                //这个地方的跟新有问题
                //此处更新的是顶点的最新值
                if (isfirstrun)
                {
                    tmp = VectorITransform(m_TriangleEX[i].initTriLocation, m_Joint[m_TriangleEX[i].Id].m_StaGlobaclMartic);
                    
                }
                else
                {
                    tmp = VectorITransform(m_TriangleEX[i].initTriLocation, m_Joint[m_TriangleEX[i].Id].m_StaGlobaclMartic);
                }
                
                vert = VectorTransform(tmp, m_Joint[m_TriangleEX[i].Id].m_CurGlobMartic);

                m_TriangleEX[i].TriLocation.x = vert.x * 1;//weights[i];  默认权值为1；此处不在乘法计算
                m_TriangleEX[i].TriLocation.y = vert.y * 1;// weights[i];
                m_TriangleEX[i].TriLocation.z = vert.z * 1;// weights[i];

                //此处更新的是法线向量的值，
                tmp = new Vector3fEX(0f, 0f, 0f);
                tmp = VectorIRotate(m_TriangleEX[i].initTriNormal, m_Joint[m_TriangleEX[i].Id].m_StaGlobaclMartic);
                norm = VectorTransform(tmp, m_Joint[m_TriangleEX[i].Id].m_CurGlobMartic);
                m_TriangleEX[i].TriNormal.x = norm.x;
                m_TriangleEX[i].TriNormal.y = norm.y+120;
                m_TriangleEX[i].TriNormal.z = norm.z;


            }

            isfirstrun = false;
        }

        private void QuaternionMatrix(Vector4fEX quaternion, MilkshapeMatrix3x4 matrix)
        {
            matrix.v[0][0] = (float)(1.0f - 2.0 * quaternion.y * quaternion.y - 2.0 * quaternion.z * quaternion.z);
            matrix.v[1][0] = (float)(2.0f * quaternion.x * quaternion.y + 2.0 * quaternion.w * quaternion.z);
            matrix.v[2][0] = (float)(2.0f * quaternion.x * quaternion.x - 2.0 * quaternion.w * quaternion.y);

            matrix.v[0][1] = (float)(2.0f * quaternion.x * quaternion.y - 2.0 * quaternion.w * quaternion.z);
            matrix.v[1][1] = (float)(1.0f - 2.0 * quaternion.x * quaternion.x - 2.0 * quaternion.z * quaternion.z);
            matrix.v[2][1] = (float)(2.0f * quaternion.y * quaternion.z + 2.0 * quaternion.w * quaternion.x);

            matrix.v[0][2] = (float)(2.0f * quaternion.x * quaternion.z + 2.0 * quaternion.w * quaternion.y);
            matrix.v[1][2] = (float)(2.0f * quaternion.y * quaternion.z - 2.0 * quaternion.w * quaternion.x);
            matrix.v[2][2] = (float)(1.0f - 2.0 * quaternion.x * quaternion.x - 2.0 * quaternion.y * quaternion.y);
        }

        private Vector4fEX AngleQuaternion(Vector3fEX angles)
        {
            float angle;
            float sr, sp, sy, cr, cp, cy;
            Vector4fEX quaternion = new Vector4fEX();
            // FIXME: rescale the inputs to 1/2 angle
            angle = (float)angles.z * 0.5f;
            sy = (float)Math.Sin(angle);
            cy = (float)Math.Cos(angle);
            angle = (float)angles.y * 0.5f;
            sp = (float)Math.Sin(angle);
            cp = (float)Math.Cos(angle);
            angle = (float)angles.x * 0.5f;
            sr = (float)Math.Sin(angle);
            cr = (float)Math.Cos(angle);

            quaternion.x = sr * cp * cy - cr * sp * sy; // X
            quaternion.y = cr * sp * cy + sr * cp * sy; // Y
            quaternion.z = cr * cp * sy - sr * sp * cy; // Z
            quaternion.w = cr * cp * cy + sr * sp * sy; // W
            return quaternion;
        }

        //1 ：根据传感器发送来的字符串，将每一个joint pos 和 rotation都更新一遍，然后就开始计算每个jonit的四个矩阵，然后开始计算三角形集合中每个点的最新坐标，然后刷新就行了。重复上述步骤
        //更新joint节点
        public void rebuildJoint(string data)
        {

            //joint1时，顺序是xzy，，rotation也是。。。但是其他的都是对的
            
            //根据传感器发送来的数据，改变joint中的数值
            Tokens f = new Tokens(data, new char[] { ' ', '\t', '|' });
            // xyz   yxz   bvh 格式顺序
            m_Joint[0].location = new Vector3fEX(float.Parse(f.elements[0]), float.Parse(f.elements[2]), float.Parse(f.elements[1]));
            m_Joint[0].rotation = new Vector3fEX(float.Parse(f.elements[4]), float.Parse(f.elements[5]), float.Parse(f.elements[3]));

            //根节点的第一个孩子 righthip
            m_Joint[1].rotation = new Vector3fEX(float.Parse(f.elements[4]), float.Parse(f.elements[3]), float.Parse(f.elements[5]));
            //第二个 lefthip
            m_Joint[2].rotation = new Vector3fEX(float.Parse(f.elements[7]), float.Parse(f.elements[6]), float.Parse(f.elements[8]));
            //第三个 chest
            m_Joint[3].rotation = new Vector3fEX(float.Parse(f.elements[10]), float.Parse(f.elements[9]), float.Parse(f.elements[11]));

            m_Joint[4].rotation = new Vector3fEX(float.Parse(f.elements[13]), float.Parse(f.elements[12]), float.Parse(f.elements[14]));

            m_Joint[5].rotation = new Vector3fEX(float.Parse(f.elements[16]), float.Parse(f.elements[15]), float.Parse(f.elements[17]));

            m_Joint[6].rotation = new Vector3fEX(float.Parse(f.elements[19]), float.Parse(f.elements[18]), float.Parse(f.elements[20]));

            m_Joint[7].rotation = new Vector3fEX(float.Parse(f.elements[22]), float.Parse(f.elements[21]), float.Parse(f.elements[23]));

            m_Joint[8].rotation = new Vector3fEX(float.Parse(f.elements[25]), float.Parse(f.elements[24]), float.Parse(f.elements[26]));

            m_Joint[9].rotation = new Vector3fEX(float.Parse(f.elements[28]), float.Parse(f.elements[27]), float.Parse(f.elements[29]));

            m_Joint[10].rotation = new Vector3fEX(float.Parse(f.elements[31]), float.Parse(f.elements[30]), float.Parse(f.elements[32]));

            m_Joint[11].rotation = new Vector3fEX(float.Parse(f.elements[34]), float.Parse(f.elements[33]), float.Parse(f.elements[35]));

            m_Joint[12].rotation = new Vector3fEX(float.Parse(f.elements[37]), float.Parse(f.elements[36]), float.Parse(f.elements[38]));

            m_Joint[13].rotation = new Vector3fEX(float.Parse(f.elements[40]), float.Parse(f.elements[39]), float.Parse(f.elements[41]));

            m_Joint[14].rotation = new Vector3fEX(float.Parse(f.elements[43]), float.Parse(f.elements[42]), float.Parse(f.elements[44]));
            m_Joint[15].rotation = new Vector3fEX(float.Parse(f.elements[46]), float.Parse(f.elements[45]), float.Parse(f.elements[47]));
            m_Joint[16].rotation = new Vector3fEX(float.Parse(f.elements[49]), float.Parse(f.elements[48]), float.Parse(f.elements[50]));
            m_Joint[17].rotation = new Vector3fEX(float.Parse(f.elements[52]), float.Parse(f.elements[51]), float.Parse(f.elements[53]));
            m_Joint[18].rotation = new Vector3fEX(float.Parse(f.elements[55]), float.Parse(f.elements[54]), float.Parse(f.elements[56]));


            //这里的赋值 跟模型的建造紧密相关，注意父子顺序

            for (int i = 0; i < m_Joint.Count; i++)
            {
                Vector4fEX quat = new Vector4fEX(0.0f, 0.0f, 0.0f, 1.0f);
                Vector3fEX tmp = new Vector3fEX(m_Joint[i].rotation.x, m_Joint[i].rotation.y, m_Joint[i].rotation.z);
                quat = AngleQuaternion(tmp);

                Vector3fEX pos = new Vector3fEX(m_Joint[i].location.x, m_Joint[i].location.y, m_Joint[i].location.z);

                MilkshapeMatrix3x4 matAnimate = new MilkshapeMatrix3x4();
                QuaternionMatrix(quat, matAnimate);
                matAnimate.v[0][3] = pos.x;
                matAnimate.v[1][3] = pos.y;
                matAnimate.v[2][3] = pos.z;


                // animate the local joint matrix using: matLocal = matLocalSkeleton * matAnimate
                if (m_Joint[i].m_CurLocalMartic == null)
                    m_Joint[i].m_CurLocalMartic = new MilkshapeMatrix3x4();
                R_ConcatTransforms(m_Joint[i].m_StaLocalMartic, matAnimate, m_Joint[i].m_CurLocalMartic);
            }


            //计算当前的两个矩阵，
            for (int i = 0; i < m_Joint.Count; i++)
            {
                if (m_Joint[i].ParentId == -1)
                {
                    if (m_Joint[i].m_CurGlobMartic == null)
                        m_Joint[i].m_CurGlobMartic = new MilkshapeMatrix3x4();
                    for (int k = 0; k < 3; k++)
                        for (int l = 0; l < 4; l++)
                            m_Joint[i].m_CurGlobMartic.v[k][l] = m_Joint[i].m_CurLocalMartic.v[k][l];//have question
                }
                else
                {
                    if (m_Joint[i].m_CurGlobMartic == null)
                        m_Joint[i].m_CurGlobMartic = new MilkshapeMatrix3x4();
                    if (m_Joint[m_Joint[i].ParentId].m_CurGlobMartic == null)
                        m_Joint[m_Joint[i].ParentId].m_CurGlobMartic = new MilkshapeMatrix3x4();
                    R_ConcatTransforms(m_Joint[m_Joint[i].ParentId].m_CurGlobMartic, m_Joint[i].m_CurLocalMartic, m_Joint[i].m_CurGlobMartic);
                }
            }

            //更新所有绑定的点
            UpdateVertex();

            Render();
        }

        public void SetupJoints()
        {
            for (int i = 0; i < m_Joint.Count; i++)
            {
                Vector3fEX tmp = new Vector3fEX(m_Joint[i].rotation.x, m_Joint[i].rotation.y, m_Joint[i].rotation.z);
                m_Joint[i].m_StaLocalMartic = new MilkshapeMatrix3x4();
                AngleMatrix(tmp, m_Joint[i].m_StaLocalMartic);

                //最后一列是pos 的值
                m_Joint[i].m_StaLocalMartic.v[0][3] = m_Joint[i].location.x;
                m_Joint[i].m_StaLocalMartic.v[1][3] = m_Joint[i].location.y;
                m_Joint[i].m_StaLocalMartic.v[2][3] = m_Joint[i].location.z;

                if (m_Joint[i].ParentId == -1)
                {
                    if (m_Joint[i].m_StaGlobaclMartic == null)
                    {
                        m_Joint[i].m_StaGlobaclMartic = new MilkshapeMatrix3x4();
                    }
                    for (int k = 0; k < 3; k++)
                        for (int l = 0; l < 4; l++)
                            m_Joint[i].m_StaGlobaclMartic.v[k][l] = m_Joint[i].m_StaLocalMartic.v[k][l];
                }
                else
                {
                    if (m_Joint[i].m_StaGlobaclMartic == null)
                    {
                        m_Joint[i].m_StaGlobaclMartic = new MilkshapeMatrix3x4();
                    }
                    R_ConcatTransforms(m_Joint[m_Joint[i].ParentId].m_StaGlobaclMartic, m_Joint[i].m_StaLocalMartic, m_Joint[i].m_StaGlobaclMartic);
                }

                //SetupTangents();   //ysj++ 我认为这里可以不用
            }

        }
        //矩阵乘法
        private void R_ConcatTransforms(MilkshapeMatrix3x4 in1, MilkshapeMatrix3x4 in2, MilkshapeMatrix3x4 o)
        {
            o.v[0][0] = in1.v[0][0] * in2.v[0][0] + in1.v[0][1] * in2.v[1][0] +
                in1.v[0][2] * in2.v[2][0];
            o.v[0][1] = in1.v[0][0] * in2.v[0][1] + in1.v[0][1] * in2.v[1][1] +
                in1.v[0][2] * in2.v[2][1];
            o.v[0][2] = in1.v[0][0] * in2.v[0][2] + in1.v[0][1] * in2.v[1][2] +
                in1.v[0][2] * in2.v[2][2];
            o.v[0][3] = in1.v[0][0] * in2.v[0][3] + in1.v[0][1] * in2.v[1][3] +
                in1.v[0][2] * in2.v[2][3] + in1.v[0][3];
            o.v[1][0] = in1.v[1][0] * in2.v[0][0] + in1.v[1][1] * in2.v[1][0] +
                in1.v[1][2] * in2.v[2][0];
            o.v[1][1] = in1.v[1][0] * in2.v[0][1] + in1.v[1][1] * in2.v[1][1] +
                in1.v[1][2] * in2.v[2][1];
            o.v[1][2] = in1.v[1][0] * in2.v[0][2] + in1.v[1][1] * in2.v[1][2] +
                in1.v[1][2] * in2.v[2][2];
            o.v[1][3] = in1.v[1][0] * in2.v[0][3] + in1.v[1][1] * in2.v[1][3] +
                in1.v[1][2] * in2.v[2][3] + in1.v[1][3];
            o.v[2][0] = in1.v[2][0] * in2.v[0][0] + in1.v[2][1] * in2.v[1][0] +
                in1.v[2][2] * in2.v[2][0];
            o.v[2][1] = in1.v[2][0] * in2.v[0][1] + in1.v[2][1] * in2.v[1][1] +
                in1.v[2][2] * in2.v[2][1];
            o.v[2][2] = in1.v[2][0] * in2.v[0][2] + in1.v[2][1] * in2.v[1][2] +
                in1.v[2][2] * in2.v[2][2];
            o.v[2][3] = in1.v[2][0] * in2.v[0][3] + in1.v[2][1] * in2.v[1][3] +
                in1.v[2][2] * in2.v[2][3] + in1.v[2][3];
        }

        private void AngleMatrix(Vector3fEX angles, MilkshapeMatrix3x4 matrix)
        {
            float angle;
            float sr, sp, sy, cr, cp, cy;

            angle = angles.z;
            sy = (float)Math.Sin(angle);
            cy = (float)Math.Cos(angle);
            angle = angles.y;
            sp = (float)Math.Sin(angle);
            cp = (float)Math.Cos(angle);
            angle = angles.x;
            sr = (float)Math.Sin(angle);
            cr = (float)Math.Cos(angle);

            // matrix = (Z * Y) * X
            matrix.v[0][0] = cp * cy;
            matrix.v[1][0] = cp * sy;
            matrix.v[2][0] = -sp;
            matrix.v[0][1] = sr * sp * cy + cr * -sy;
            matrix.v[1][1] = sr * sp * sy + cr * cy;
            matrix.v[2][1] = sr * cp;
            matrix.v[0][2] = (cr * sp * cy + -sr * -sy);
            matrix.v[1][2] = (cr * sp * sy + -sr * cy);
            matrix.v[2][2] = cr * cp;
            matrix.v[0][3] = 0.0f;
            matrix.v[1][3] = 0.0f;
            matrix.v[2][3] = 0.0f;
        }

    }
}
