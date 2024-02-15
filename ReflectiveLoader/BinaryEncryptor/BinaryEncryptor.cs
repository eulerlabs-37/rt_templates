using System;
using System.Reflection;
using System.Text;
using System.Security.Cryptography;

namespace ReflectiveLoader
{
    public class BinaryEncryptor
    {
        public static void SaveToFile(byte[] bytes, byte[] key, byte[] IV, String filename, bool generateCode = false)
        {
            String keyFile = filename + ".key";
            String IVFile = filename + ".IV";


            if (generateCode)
            {
                StringBuilder hex = new StringBuilder(bytes.Length * 6 + 1000); // TODO: fix this mess urgently
                String generatedCodeFile = filename + ".cs";

                int i = 0;

                hex.AppendFormat("byte[] key = [");
                for (; i < (key.Length - 1); i++)
                {
                    hex.AppendFormat("0x{0:x2}, ", key[i]);
                }
                hex.AppendFormat("0x{0:x2}];\n\n", key[i]);

                hex.AppendFormat("byte[] IV = [");
                for (i = 0; i < (IV.Length - 1); i++)
                {
                    hex.AppendFormat("0x{0:x2}, ", IV[i]);
                }
                hex.AppendFormat("0x{0:x2}];\n\n", IV[i]);


                hex.Append("byte[] bytesEncr = [");
                for (i = 0; i < (bytes.Length - 1); i++)
                {
                    if (i % 16 == 0) hex.Append("\n\t\t");
                    hex.AppendFormat("0x{0:x2}, ", bytes[i]);
                }
                hex.AppendFormat("0x{0:x2}];\n\n", bytes[i]);

                File.WriteAllText(generatedCodeFile, hex.ToString());
            }

            File.WriteAllBytes(filename, bytes);
            File.WriteAllBytes(keyFile, key);
            File.WriteAllBytes(IVFile, IV);
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