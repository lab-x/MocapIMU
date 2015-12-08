
namespace Gwearable
{
    public class ArrayData
    {
        public static int[][] arr = new int[17][];

        public static int[] num = new int[17] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        public static int[] ration = new int[17] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };

        public ArrayData()
        {
            //初始化数组
            for (int i = 0; i < 17; i++)
            {
                arr[i] = new int[28];

                for (int j = 0; j < 28; j++)
                {
                    arr[i][j] = 0;
                }
            }
        }

        public static void ResetTimes()
        {
            for (int i = 0; i < 17; i++)
            {
                num[i] = 0;
            }
        }
    }
}
