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
        private NETCore.Encrypt.Internal.RSAKey rsaKey = EncryptProvider.CreateRsaKey(RsaSize.R4096);    //default is 2048

       
        public string getRSAPublicKey()
        {
            return rsaKey.PublicKey;
        }
        public string getRSAPrivateKey()
        {
            return rsaKey.PrivateKey;
        }


        public void EncryptFileAES(string filepath)
        {
            var key = aesKey.Key;
            var iv = aesKey.IV;

            // TODO BACKUP MODE
            string contents = File.ReadAllText(filepath);
            string encrypted = EncryptProvider.AESEncrypt(contents, key, iv);
            File.WriteAllText(filepath, encrypted);
      
        }


        public void EncryptFileRSA(string filepath)
        {
            var publicKey = rsaKey.PublicKey;

            // TODO BACKUP MODE
            string contents = File.ReadAllText(filepath);
            string encrypted = EncryptProvider.RSAEncrypt(publicKey, contents);
            File.WriteAllText(filepath, encrypted);

        }

        public void DecryptFileRSA(string filepath)
        {
            var privateKey = rsaKey.PrivateKey;

            string contents = File.ReadAllText(filepath);
            string decrypted = EncryptProvider.RSADecrypt(privateKey, contents);
            File.WriteAllText(filepath, decrypted);

        }


        public void DecryptFileAES(string filepath)
        {
            var key = aesKey.Key;
            var iv = aesKey.IV;

            string contents = File.ReadAllText(filepath);
            string decrypted = EncryptProvider.AESDecrypt(contents, key, iv);
            File.WriteAllText(filepath, decrypted);

            //TODO Error handling - you are out of luck!

        }


    }
}
