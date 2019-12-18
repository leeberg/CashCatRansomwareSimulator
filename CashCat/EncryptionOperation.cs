using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NETCore.Encrypt;
using System.IO;

namespace CashCat
{
    class EncryptionOperation
    {

        //make this better - should make this "Easy mode"
        private NETCore.Encrypt.Internal.AESKey aesKey = EncryptProvider.CreateAesKey();
   
        public void EncryptFile(string filepath)
        {
            var key = aesKey.Key;
            var iv = aesKey.IV;

            // TODO BACKUP MODE
            string contents = File.ReadAllText(filepath);
            string encrypted = EncryptProvider.AESEncrypt(contents, key);
            File.WriteAllText(filepath, encrypted);
      
        }


        public void DecryptFile(string filepath)
        {
            var key = aesKey.Key;
            var iv = aesKey.IV;

            string contents = File.ReadAllText(filepath);
            string decrypted = EncryptProvider.AESDecrypt(contents, key);
            File.WriteAllText(filepath, decrypted);

            //TODO Error handling - you are out of luck screwed!

        }


    }
}
