using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace BinaryEncryptor
{
    internal class Programm
    {
        const String DLLPATH = "C:\\Users\\offsec\\source\\rt_templates\\ReflectionEncrypt\\MathLibraryCS.dll";

        static void Main(string[] args) {
            string dllPath = DLLPATH;
            Console.WriteLine($"Usage {AppDomain.CurrentDomain.FriendlyName} <path_to_dll>");
            Console.WriteLine("If no path_to_dll is defined then default value is used: " + DLLPATH);

            if (args.Length == 1) dllPath = DLLPATH;

            byte[] bytesPlain = File.ReadAllBytes(dllPath);
            byte[] key = BinaryEncryptor.GenerateRandomKey();
            byte[] IV = BinaryEncryptor.GenerateRandomIV();
            byte[] bytesEncrypted = BinaryEncryptor.EncryptAes(bytesPlain, key, IV);
            BinaryEncryptor.SaveToFile(bytesEncrypted, key, IV, "encrypted.txt");
        }
    }
}
