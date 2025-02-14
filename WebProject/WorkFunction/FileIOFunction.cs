using Newtonsoft.Json.Linq;
using System.Numerics;
using System.Security.Cryptography;

namespace WebProject.WorkFunction
{
    public class FileIOFunction
    {
        private readonly IWebHostEnvironment _env;
        public FileIOFunction(IWebHostEnvironment env)
        {
            _env = env;
        }

        public bool WriteFileAppend(string fileName, string content)
        {
            string path = _env.ContentRootPath + "/wwwroot/OrderCar";
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

        public int ReadFileCount(string fileName)
        {
            int count = 0;
            string path = _env.ContentRootPath + "/wwwroot/OrderCar"; ;
            if (!System.IO.Directory.Exists(path))
                return count;
            string filePath = Path.Combine(path, fileName);
            if (File.Exists(filePath))
            {
                StreamReader sr = new StreamReader(filePath);
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

        public List<string> ReadFileContent(string fileName)
        {
            List<string> ret = new List<string>();
            
            string path = _env.ContentRootPath + "/wwwroot/OrderCar";
            if (!System.IO.Directory.Exists(path))
                return ret;
            string filePath = Path.Combine(path, fileName);
            if (File.Exists(filePath))
            {
                StreamReader sr = new StreamReader(filePath);
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
