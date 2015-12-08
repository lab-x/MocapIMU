using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Gwearable
{
    public class CustomFile
    {
        /************************************************************************/
        /* 记录文档操作                                                         */
        /************************************************************************/
        private static FileStream fs;
        private static StreamWriter sw;

        public void CreateFile(string filename)
        {
            fs = new FileStream(filename, FileMode.Append);
            sw = new StreamWriter(fs, Encoding.ASCII);
        }

        public void WriteFile(string data)
        {
            sw.WriteLine(data);
        }

        public void StartWriteData()
        {
            Global.isWriteable = true;
        }

        public void EndWriteData()
        {
            Global.isWriteable = false;
            CloseFile();
        }

        public void CloseFile()
        {
            sw.WriteLine("Frames " + Global.TotalFrameNum.ToString());
            sw.Close();
            fs.Close();
        }
    }
}
