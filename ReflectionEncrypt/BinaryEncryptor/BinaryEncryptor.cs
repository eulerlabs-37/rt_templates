using System;
using System.Reflection;
using System.Text;
using System.Security.Cryptography;

namespace BinaryEncryptor
{
    public class BinaryEncryptor
    {
        public static void SaveToFile(byte[] bytes, byte[] key, byte[] IV, String filename)
        {
            StringBuilder hex = new StringBuilder(bytes.Length * 6 + 1000); // 0xd420
            hex.Append("// Encrypted: ");
            hex.Append("byte[] bytes = [");

            int i = 0;
            for (; i < (bytes.Length - 1); i++)
            {
                if (i % 16 == 0) hex.Append("\n\t\t");
                hex.AppendFormat("0x{0:x2}, ", bytes[i]);
            }
            hex.AppendFormat("0x{0:x2}];\n\n", bytes[i]);


            hex.AppendFormat("byte[] key = [");
            for (i = 0; i < (key.Length - 1); i++)
            {
                hex.AppendFormat("0x{0:x2}, ", key[i]);
            }
            hex.AppendFormat("0x{0:x2}];\n", key[i]);


            hex.AppendFormat("byte[] IV = [");
            for (i = 0; i < (IV.Length - 1); i++)
            {
                hex.AppendFormat("0x{0:x2}, ", IV[i]);
            }

            hex.AppendFormat("0x{0:x2}];\n", IV[i]);


            Console.WriteLine(hex.ToString());
            File.WriteAllText(filename, hex.ToString());
        }

        public static byte[] GenerateRandomKey()
        {
            using (Aes aes = Aes.Create())
            {
                aes.GenerateKey();
                return aes.Key;
            }
        }

        public static byte[] GenerateRandomIV()
        {
            using (Aes aes = Aes.Create())
            {
                aes.GenerateIV();
                return aes.IV;
            }
        }

        public static byte[] EncryptAes(byte[] data, byte[] key, byte[] iv)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                using (MemoryStream msEncrypt = new MemoryStream())
                {
                    using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, aesAlg.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        csEncrypt.Write(data, 0, data.Length);
                        csEncrypt.FlushFinalBlock();
                    }
                    return msEncrypt.ToArray();
                }
            }
        }

        public static byte[] DecryptAes(byte[] encryptedData, byte[] key, byte[] iv)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.Key = key;
                aesAlg.IV = iv;

                using (MemoryStream msDecrypt = new MemoryStream())
                {
                    using (CryptoStream csDecrypt = new CryptoStream(msDecrypt, aesAlg.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        csDecrypt.Write(encryptedData, 0, encryptedData.Length);
                        csDecrypt.FlushFinalBlock();
                    }
                    return msDecrypt.ToArray();
                }
            }
        }
    }
}