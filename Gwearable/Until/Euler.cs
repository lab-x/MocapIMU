
namespace Gwearable
{
    public class Euler
    {
        public double Eul_1;
        public double Eul_2;
        public double Eul_3;

        public Euler()
        {
            Eul_1 = 0;
            Eul_2 = 0;
            Eul_3 = 0;
        }
        public Euler(double a, double b, double c)
        {
            Eul_1 = a;
            Eul_2 = b;
            Eul_3 = c;
        }

        public override string ToString()
        {
            return Eul_1.ToString("0.0000") + " " + Eul_2.ToString("0.0000") + " " + Eul_3.ToString("0.0000");
        }
    }
}
