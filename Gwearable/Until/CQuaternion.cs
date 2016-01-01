
namespace Gwearable
{
     public class CQuaternion
    {
        public CQuaternion(double q_0, double q_1, double q_2, double q_3)
        {
            m_q0 = q_0;
            m_q1 = q_1;
            m_q2 = q_2;
            m_q3 = q_3;
        }

        public override string ToString()
        {
            return m_q0.ToString("0.00") + " " + m_q1.ToString("0.00") + " " + m_q2.ToString("0.00") + " " + m_q3.ToString("0.00");
        }


        //Quaternion Constructor -- without input arguments
        public CQuaternion()
        {
            m_q0 = 1;
            m_q1 = 0;
            m_q2 = 0;
            m_q3 = 0;
        }

        public static CQuaternion operator *(CQuaternion quaternion1, CQuaternion quaternion2)
        {
            CQuaternion temp = new CQuaternion();

            temp.m_q0 = quaternion1.m_q0 * quaternion2.m_q0 - quaternion1.m_q1 * quaternion2.m_q1
                        - quaternion1.m_q2 * quaternion2.m_q2 - quaternion1.m_q3 * quaternion2.m_q3;
            temp.m_q1 = quaternion1.m_q0 * quaternion2.m_q1 + quaternion1.m_q1 * quaternion2.m_q0
                        + quaternion1.m_q2 * quaternion2.m_q3 - quaternion1.m_q3 * quaternion2.m_q2;
            temp.m_q2 = quaternion1.m_q0 * quaternion2.m_q2 + quaternion1.m_q2 * quaternion2.m_q0
                        + quaternion1.m_q3 * quaternion2.m_q1 - quaternion1.m_q1 * quaternion2.m_q3;
            temp.m_q3 = quaternion1.m_q0 * quaternion2.m_q3 + quaternion1.m_q3 * quaternion2.m_q0
                        + quaternion1.m_q1 * quaternion2.m_q2 - quaternion1.m_q2 * quaternion2.m_q1;
            return temp;
        }


        //Calculating Conjugate
        public CQuaternion Conjugate()
        {
            this.m_q1 = -m_q1;
            this.m_q2 = -m_q2;
            this.m_q3 = -m_q3;
            return this;
        }
        //Quaternion Calculate Mode
        public double Norm()
        {
            return m_q0 * m_q0 + m_q1 * m_q1 + m_q2 * m_q2 + m_q3 * m_q3;
        }
        //Quaternion Normalize
        public CQuaternion Normalize()
        {
            double temp = Norm();
            this.m_q0 = this.m_q0 / System.Math.Sqrt(temp);
            this.m_q1 = this.m_q1 / System.Math.Sqrt(temp);
            this.m_q2 = this.m_q2 / System.Math.Sqrt(temp);
            this.m_q3 = this.m_q3 / System.Math.Sqrt(temp);
            return this;
        }


        //public static double FourBytes(int a1, int a2, int a3, int a4)
        //{
        //    double a;
        //    a = a1 * System.Math.Pow((double)2, 24) + a2 * System.Math.Pow((double)2, 16) + a3 * System.Math.Pow((double)2, 8) + a4;
        //    if (a > 2147483648)
        //    {
        //        a -= 4294967296;
        //    }
        //    return a;
        //}
        //public static CQuaternion getRawQuat(int[] raw, int count)//count =23;//一个节点一次的数据是23 包含id 回车换行等等信息。
        //{
        //    double q0, q1, q2, q3, mag;
        //    q0 = FourBytes(raw[3], raw[4], raw[5], raw[6]) / System.Math.Pow((double)2, 30);
        //    q1 = FourBytes(raw[7], raw[8], raw[9], raw[10]) / System.Math.Pow((double)2, 30);
        //    q2 = FourBytes(raw[11], raw[12], raw[13], raw[14]) / System.Math.Pow((double)2, 30);
        //    q3 = FourBytes(raw[15], raw[16], raw[17], raw[18]) / System.Math.Pow((double)2, 30);
        //    mag = System.Math.Sqrt(q0 * q0 + q1 * q1 + q2 * q2 + q3 * q3);
        //    CQuaternion result = new CQuaternion(q0 / mag, q1 / mag, q2 / mag, q3 / mag);
        //    return result;
        //}


        public static Euler Quat2angle(double q_0, double q_1, double q_2, double q_3, int type)
        {
            double r11 = 0;
            double r12 = 0;
            double r21 = 0;
            double r31 = 0;
            double r32 = 0;
            Euler result = new Euler();
            switch (type)
            {
                case 1:			//'zyx
                    r11 = 2 * (q_1 * q_2 + q_0 * q_3);
                    r12 = System.Math.Pow(q_0, 2) + System.Math.Pow(q_1, 2) - System.Math.Pow(q_2, 2) - System.Math.Pow(q_3, 2);
                    r21 = -2 * (q_1 * q_3 - q_0 * q_2);
                    r31 = 2 * (q_2 * q_3 + q_0 * q_1);
                    r32 = System.Math.Pow(q_0, 2) - System.Math.Pow(q_1, 2) - System.Math.Pow(q_2, 2) + System.Math.Pow(q_3, 2);
                    break;
                case 2:			//'zxy'
                    r11 = -2 * (q_1 * q_2 - q_0 * q_3);
                    r12 = System.Math.Pow(q_0, 2) - System.Math.Pow(q_1, 2) + System.Math.Pow(q_2, 2) - System.Math.Pow(q_3, 2);
                    r21 = 2 * (q_2 * q_3 + q_0 * q_1);
                    r31 = -2 * (q_1 * q_3 - q_0 * q_2);
                    r32 = System.Math.Pow(q_0, 2) - System.Math.Pow(q_1, 2) - System.Math.Pow(q_2, 2) + System.Math.Pow(q_3, 2);
                    break;
                case 3:			//'yxz'
                    r11 = 2 * (q_1 * q_3 + q_0 * q_2);
                    r12 = System.Math.Pow(q_0, 2) - System.Math.Pow(q_1, 2) - System.Math.Pow(q_2, 2) + System.Math.Pow(q_3, 2);
                    r21 = -2 * (q_2 * q_3 - q_0 * q_1);
                    r31 = 2 * (q_1 * q_2 + q_0 * q_3);
                    r32 = System.Math.Pow(q_0, 2) - System.Math.Pow(q_1, 2) + System.Math.Pow(q_2, 2) - System.Math.Pow(q_3, 2);
                    break;
                case 4:			 //'yzx'       
                    r11 = -2 * (q_1 * q_3 - q_0 * q_2);
                    r12 = System.Math.Pow(q_0, 2) + System.Math.Pow(q_1, 2) - System.Math.Pow(q_2, 2) - System.Math.Pow(q_3, 2);
                    r21 = 2 * (q_1 * q_2 + q_0 * q_3);
                    r31 = -2 * (q_2 * q_3 - q_0 * q_1);
                    r32 = System.Math.Pow(q_0, 2) - System.Math.Pow(q_1, 2) + System.Math.Pow(q_2, 2) - System.Math.Pow(q_3, 2);
                    break;
                case 5:			//'xyz'
                    r11 = -2 * (q_2 * q_3 - q_0 * q_1);
                    r12 = System.Math.Pow(q_0, 2) - System.Math.Pow(q_1, 2) - System.Math.Pow(q_2, 2) + System.Math.Pow(q_3, 2);
                    r21 = 2 * (q_1 * q_3 + q_0 * q_2);
                    r31 = -2 * (q_1 * q_2 - q_0 * q_3);
                    r32 = System.Math.Pow(q_0, 2) + System.Math.Pow(q_1, 2) - System.Math.Pow(q_2, 2) - System.Math.Pow(q_3, 2);
                    break;
                case 6: //'xzy'
                    r11 = 2 * (q_2 * q_3 + q_0 * q_1);
                    r12 = System.Math.Pow(q_0, 2) - System.Math.Pow(q_1, 2) + System.Math.Pow(q_2, 2) - System.Math.Pow(q_3, 2);
                    r21 = -2 * (q_1 * q_2 - q_0 * q_3);
                    r31 = 2 * (q_1 * q_3 + q_0 * q_2);
                    r32 = System.Math.Pow(q_0, 2) + System.Math.Pow(q_1, 2) - System.Math.Pow(q_2, 2) - System.Math.Pow(q_3, 2);
                    break;
            }
            result.Eul_1 = System.Math.Atan2(r11, r12);
            result.Eul_2 = System.Math.Asin(r21);
            result.Eul_3 = System.Math.Atan2(r31, r32);
            return result;
        }

        public static CQuaternion angle2Quat(double r1, double r2, double r3, int type)
        {
            double[] ang = { r1, r2, r3 };
            double[] cang = { System.Math.Cos(ang[0] / 2), System.Math.Cos(ang[1] / 2), System.Math.Cos(ang[2] / 2) };
            double[] sang = { System.Math.Sin(ang[0] / 2), System.Math.Sin(ang[1] / 2), System.Math.Sin(ang[2] / 2) };
            CQuaternion q = new CQuaternion();

            switch (type)
            {
                case 1:      //'zyx'
                    q.m_q0 = cang[0] * cang[1] * cang[2] + sang[0] * sang[1] * sang[2];
                    q.m_q1 = cang[0] * cang[1] * sang[2] - sang[0] * sang[1] * cang[2];
                    q.m_q2 = cang[0] * sang[1] * cang[2] + sang[0] * cang[1] * sang[2];
                    q.m_q3 = sang[0] * cang[1] * cang[2] + cang[0] * sang[1] * sang[2];
                    break;

                case 2: //'zxy'
                    q.m_q0 = cang[0] * cang[1] * cang[2] - sang[0] * sang[1] * sang[2];
                    q.m_q1 = cang[0] * sang[1] * cang[2] - sang[0] * cang[1] * sang[2];
                    q.m_q2 = cang[0] * cang[1] * sang[2] + sang[0] * sang[1] * cang[2];
                    q.m_q3 = cang[0] * sang[1] * sang[2] + sang[0] * cang[1] * cang[2];
                    break;

                case 3: //'yxz'
                    q.m_q0 = cang[0] * cang[1] * cang[2] + sang[0] * sang[1] * sang[2];
                    q.m_q1 = cang[0] * sang[1] * cang[2] + sang[0] * cang[1] * sang[2];
                    q.m_q2 = sang[0] * cang[1] * cang[2] - cang[0] * sang[1] * sang[2];
                    q.m_q3 = cang[0] * cang[1] * sang[2] - sang[0] * sang[1] * cang[2];
                    break;

                case 4: //'yzx'
                    q.m_q0 = cang[0] * cang[1] * cang[2] - sang[0] * sang[1] * sang[2];
                    q.m_q1 = cang[0] * cang[1] * sang[2] + sang[0] * sang[1] * cang[2];
                    q.m_q2 = cang[0] * sang[1] * sang[2] + sang[0] * cang[1] * cang[2];
                    q.m_q3 = cang[0] * sang[1] * cang[2] - sang[0] * cang[1] * sang[2];
                    break;

                case 5: //'xyz'
                    q.m_q0 = cang[0] * cang[1] * cang[2] - sang[0] * sang[1] * sang[2];
                    q.m_q1 = cang[0] * sang[1] * sang[2] + sang[0] * cang[1] * cang[2];
                    q.m_q2 = cang[0] * sang[1] * cang[2] - sang[0] * cang[1] * sang[2];
                    q.m_q3 = cang[0] * cang[1] * sang[2] + sang[0] * sang[1] * cang[2];
                    break;

                case 6: //'xzy'
                    q.m_q0 = cang[0] * cang[1] * cang[2] + sang[0] * sang[1] * sang[2];
                    q.m_q1 = sang[0] * cang[1] * cang[2] - cang[0] * sang[1] * sang[2];
                    q.m_q2 = cang[0] * cang[1] * sang[2] - sang[0] * sang[1] * cang[2];
                    q.m_q3 = cang[0] * sang[1] * cang[2] + sang[0] * cang[1] * sang[2];
                    break;
                    
            }

            return q;

        }

        public static Matrix Quat2Matrix(CQuaternion q)
        {
            Matrix RotMat = new Matrix(3, 3);
            double sqw = q.m_q0 * q.m_q0;
            double sqx = q.m_q1 * q.m_q1;
            double sqy = q.m_q2 * q.m_q2;
            double sqz = q.m_q3 * q.m_q3;
            RotMat[0, 0] = sqw + sqx - sqy - sqz;
            RotMat[1, 1] = sqw - sqx + sqy - sqx;
            RotMat[2, 2] = sqw - sqx - sqy + sqz;
            double xy = q.m_q1 * q.m_q2;
            double zw = q.m_q3 * q.m_q0;
            RotMat[0, 1] = 2.0 * (xy - zw);
            RotMat[1, 0] = 2.0 * (xy + zw);
            double xz = q.m_q1 * q.m_q3;
            double yw = q.m_q2 * q.m_q0;
            RotMat[0, 2] = 2.0 * (xz + yw);
            RotMat[2, 0] = 2.0 * (xz - yw);
            double xw = q.m_q1 * q.m_q0;
            double yz = q.m_q2 * q.m_q3;
            RotMat[1, 2] = 2.0 * (yz - xw);
            RotMat[2, 1] = 2.0 * (yz + xw);
            return RotMat;
        }
        
        public static CQuaternion Matrix2Quat(Matrix a)
        {
            CQuaternion q = new CQuaternion(1,0,0,0);
            double trace = a[0,0] + a[1,1] + a[2,2]; // I removed + 1.0f; see discussion with Ethan
            double s;
            if( trace > 0 ) 
            {// I changed M_EPSILON to 0
                s = 0.5 / System.Math.Sqrt(trace+ 1);
                q.m_q0 = 0.25 / s;
                q.m_q1 = ( a[2,1] - a[1,2] ) * s;
                q.m_q2 = ( a[0,2] - a[2,0] ) * s;
                q.m_q3 = ( a[1,0] - a[0,1] ) * s;
            } 
            else 
            {
                if ( a[0,0] > a[1,1] && a[0,0] > a[2,2] ) 
                {
                    s = 2.0 * System.Math.Sqrt( 1.0 + a[0,0] - a[1,1] - a[2,2]);
                    q.m_q0 = (a[2,1] - a[1,2] ) / s;
                    q.m_q1 = 0.25 * s;
                    q.m_q2 = (a[0,1] + a[1,0] ) / s;
                    q.m_q3 = (a[0,2] + a[2,0] ) / s;
                } 
                else if (a[1,1] > a[2,2]) 
                {
                    s = 2.0 * System.Math.Sqrt( 1.0 + a[1,1] - a[0,0] - a[2,2]);
                    q.m_q0 = (a[0,2] - a[2,0] ) / s;
                    q.m_q1 = (a[0,1] + a[1,0] ) / s;
                    q.m_q2 = 0.25f * s;
                    q.m_q3 = (a[1,2] + a[2,1] ) / s;
                } 
                else 
                {
                    s = 2.0 * System.Math.Sqrt( 1.0 + a[2,2] - a[0,0] - a[1,1] );
                    q.m_q0 = (a[1,0] - a[0,1] ) / s;
                    q.m_q1 = (a[0,2] + a[2,0] ) / s;
                    q.m_q2 = (a[1,2] + a[2,1] ) / s;
                    q.m_q3 = 0.25 * s;
                }
            }
            return q;
        }

        public double m_q0;
        public double m_q1;
        public double m_q2;
        public double m_q3;

        //public Segment[] BodySegment = new Segment[23];

    }
}


