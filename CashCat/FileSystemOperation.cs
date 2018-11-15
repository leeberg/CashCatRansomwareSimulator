using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CashCat
{
    public class FileSystemOperation
    {
        /// <summary>
        /// File System Interactions
        /// </summary>

        public void RenameTXTFiles(string path)
        {
            DirectoryInfo d = new DirectoryInfo(path);
            FileInfo[] Files = d.GetFiles("*.txt"); //Getting Txt files
            
            foreach (FileInfo file in Files)
            {
                //Console.WriteLine(file.Name);
                string newfilename = (file.Name).Replace(".txt", ".locky");
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

        public void UnlockLockyFiles(string path)
        {
            DirectoryInfo d = new DirectoryInfo(path);
            FileInfo[] Files = d.GetFiles("*.locky"); //Getting locky files
            
            foreach (FileInfo file in Files)
            {
                //Console.WriteLine(file.Name);
                string newfilename = (file.Name).Replace(".locky", ".txt");
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
}
