using Newtonsoft.Json.Linq;
using System.Numerics;
using System.Security.Cryptography;

namespace WebProject.WorkFunction
{
    public class FileIOFunction
    {
        public static bool WriteFileAppend(string strdir, string fileName, string content)
        {
            string path = strdir;
            if (!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);
            string filePath = Path.Combine(path, fileName);
            try
            {
                // 使用 StreamWriter 來寫入文本檔案
                using (StreamWriter sw = new StreamWriter(filePath, true))
                {
                    sw.WriteLine(content);
                    sw.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("寫入檔案時出錯：" + e.Message);
                return false;
            }
            return true;
        }

        public static int ReadFileCount(string strdir, string fileName)
        {
            int count = -1;
            string path = strdir;
            if (!System.IO.Directory.Exists(path))
                return count;
            string filePath = Path.Combine(path, fileName);
            StreamReader sr = new StreamReader(filePath);
            if (File.Exists(filePath))
            {
                count = 0;
                string line = "";
                while ((line = sr.ReadLine()) != null)
                {
                    count++;
                }
                sr.Close();
            }
            return count;
        }

        public static List<string> ReadFileContent(string strdir, string fileName)
        {
            List<string> ret = new List<string>();
            
            string path = strdir;
            if (!System.IO.Directory.Exists(path))
                return ret;
            string filePath = Path.Combine(path, fileName);
            StreamReader sr = new StreamReader(filePath);
            if (File.Exists(filePath))
            {
                string line = "";
                while ((line = sr.ReadLine()) != null)
                {
                    ret.Add(line);
                }
            }
            return ret;
        }
    }
}
