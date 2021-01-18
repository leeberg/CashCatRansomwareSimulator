using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CashCat
{
    public class FileSystemOperation
    {
        /// <summary>
        /// File System Interactions
        /// </summary>
        /// 

        private RansomFiles RandomwareFileList = new RansomFiles();


        public void WriteLog(string logMessage)
        {
                    
            string exePath = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

            try
            {
                using (StreamWriter w = File.AppendText(exePath + "\\" + "CashCat.log"))
                {
                    Log(logMessage, w);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("failed to log :/");
            }

        }


        public void LogKeyData(string privatekey, string publickey)
        {

            string currentdatetime = (DateTime.Now.ToString("yyyy-dd-M-HH-mm-ss") + "-KEY.log");
            var keylogfile = File.Create(currentdatetime);
            
            using (StreamWriter outputFile = new StreamWriter(keylogfile))
            {
                outputFile.WriteLine("Welcome to your CashCat Key Backup Log File!");
                outputFile.WriteLine("Private Key: " + privatekey);
                outputFile.WriteLine("Public Key: " + publickey);
            }
     
        }

        public void Log(string logMessage, TextWriter txtWriter)
        {
            try
            {
                logMessage = (DateTime.Now.ToLongTimeString() + " : " + logMessage);
                txtWriter.WriteLine(logMessage);

            }
            catch (Exception ex)
            {
            }
        }


        public void startstopFileDump()
        {

        }


        public FileInfo[] GetLockyFileCount(string path)
        {
            DirectoryInfo d = new DirectoryInfo(path);
            FileInfo[] Files = d.GetFiles("*.locky"); //Getting locky files
            return Files;
        }

        public List<FileInfo> GetRansomedFileCount(string path)
        {
            DirectoryInfo d = new DirectoryInfo(path);
            FileInfo[] Files = d.GetFiles("*");

            List<FileInfo> RansomwareFiles = new List<FileInfo>();

            int fileCount = 0;

            foreach (FileInfo file in Files)
            {
            
                if(RandomwareFileList.fileExtensions.Contains(file.Extension))
                {
                    RansomwareFiles.Add(file);
                }
                
            }

            return RansomwareFiles;
        }


       

        public FileInfo[] GetTXTFileCount (string path)
        {
            DirectoryInfo d = new DirectoryInfo(path);
            FileInfo[] Files = d.GetFiles("*.txt"); //Getting Txt files
            return Files;
        }

        public void LockTXTFile(FileInfo file, string extension)
        {
            string oldfilename = file.Name;

            // todo optional mode? 
            //string newExtension = RandomwareFileList.GetRandomFileExtension();

            string newExtension = extension;


            string newfilename = (file.Name).Replace(".txt", newExtension);
            string oldfileExtension = file.Extension;
            string newfilefullname = (file.FullName).Replace(".txt", newExtension);

            try
            {
                System.IO.File.Move(file.Name, newfilename);
            }
            catch
            {
                //can't touch this
            }
        }


        public void UnlockRansomwareFile(FileInfo file)
        {
            //Console.WriteLine(file.Name);
              
          
            string newfilename = (file.Name).Replace(file.Extension, ".txt");
            // string newfilefullname = (file.FullName).Replace(".locky", ".txt");
            try
            {
                System.IO.File.Move(file.Name, newfilename);
            }
            catch
            {
                //can't touch this
            }
        }


    }
}
