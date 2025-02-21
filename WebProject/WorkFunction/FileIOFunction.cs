using Newtonsoft.Json.Linq;
using System.Numerics;
using System.Security.Cryptography;
using WebProject.ViewModels;

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
                    //sw.Close();
                    sw.Flush();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("寫入檔案時出錯：" + e.Message);
                return false;
            }
            return true;
        }

        public async Task<bool> WriteFileOverWrite(string fileName, VMOrderCar orderCar)
        {
            string path = _env.ContentRootPath + "/wwwroot/OrderCar";
            if (!System.IO.Directory.Exists(path))
                System.IO.Directory.CreateDirectory(path);
            string filePath = Path.Combine(path, fileName);
            string content = VMOrderCarToString(orderCar);
            string[] contents = content.Split("\n");
            try
            {
                // 使用 StreamWriter 來寫入文本檔案
                //await using (FileStream fs = new FileStream(filePath, FileMode.Create, FileAccess.Write, FileShare.ReadWrite))
                //await using (StreamWriter sw = new StreamWriter(fs))
                //{
                //    for (int i = 0; i < contents.Length; i++)
                //        await sw.WriteLineAsync(contents[i]);
                //    await sw.FlushAsync();
                //    //sw.Close();
                //}
                await File.WriteAllTextAsync(filePath, content);
            }
            catch (Exception e)
            {
                Console.WriteLine("寫入檔案時出錯：" + e.Message);
                await Task.Delay(100);
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
                sr.Close();
            }
            return ret;
        }

        private string VMOrderCarToString(VMOrderCar orderCar)
        {
            string ret = "";
            foreach (var item in orderCar.item)
            {
                ret += item.product + "," + item.count + "\n";
            }
            //ret = ret.Substring(0, ret.Length - 1);
            return ret;
        }
    }
}
