
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModDashboard
{
    public class DataReader
    {
        /// <summary>
        ///  中间数据文件
        /// </summary>
        public string DataFile
        {
            get; set;
        }


        /// <summary>
        ///  上一次读到第几行
        /// </summary>
        public int Offset
        {
            get { return m_off; }
            set { m_off = value; }
        }
        int m_off = -1;

        /// <summary>
        ///  每行为一个二维数表
        /// </summary>
        public List<Tuple<float, float>> Datas
        {
            get;
            set;
        }

        public void InitOffset()
        {
            using (FileStream file = new FileStream(DataFile, FileMode.Open))
            {
                file.Seek(Offset == -1 ? 0 : Offset, SeekOrigin.Begin);
                Offset = (int)file.Length;

            }
        }

        public void Read()
        {
            if (string.IsNullOrEmpty(DataFile))
                return;
            if (!File.Exists(DataFile))
                return;
            try
            {
                if (Datas == null)
                    Datas = new List<Tuple<float, float>>();
                else
                    Datas.Clear();
                string[] linecount = File.ReadAllLines(DataFile);

                FileStream file = new FileStream(DataFile, FileMode.Open);
                file.Seek(Offset, SeekOrigin.Begin);
                if (file.Length < Offset)
                {
                    Offset = 0;
                    file.Close();
                    return;
                }
                byte[] byData = new byte[file.Length - Offset];
                char[] charData = new char[file.Length - Offset];
                if (byData.Length == 0)
                {
                    file.Close();
                    return;
                }
                else
                {
                    file.Read(byData, 0, byData.Length); //byData传进来的字节数组,用以接受FileStream对象中的数据,第2个参数是字节数组中开始写入数据的位置,它通常是0,表示从数组的开端文件中向数组写数据,最后一个参数规定从文件读多少字符.
                }
                file.Close();

                Decoder d = Encoding.Default.GetDecoder();
                d.GetChars(byData, 0, byData.Length, charData, 0);
                Offset = Offset + (int)byData.Length;
                string str = charData.ToString();
                str = new string(charData);
                string[] lines = str.Split(new char[] { '\r', '\n' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string line in lines)
                {
                    string[] contents = line.Split(new string[] { "30001:" }, StringSplitOptions.RemoveEmptyEntries);
                    if (contents.Length > 1)
                    {
                        string data = contents[1];
                        ///数据，以空格分隔，取两个数字
                        string[] values = data.Split(new char[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);

                        if (values.Length > 0)
                        {
                            /// 第一个数字
                            float first = 0;
                            float.TryParse(values[0], out first);
                            /// 第二个数字
                            float second = 0;
                            if (values.Length > 1)
                                float.TryParse(values[1], out second);
                            Tuple<float, float> result = new Tuple<float, float>(first * float.Parse("0.01"), second * float.Parse("0.01"));

                            Datas.Add(result);
                        }
                    }
                }

            }
            catch (IOException e)
            {
            }
        }
    }
}