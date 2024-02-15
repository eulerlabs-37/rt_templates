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
		static String TYPENAME = "MathLibraryCS.MathLibrary"; // class
		static void Main() {

            //	byte[] bytesPlain = BinaryEncryptor.DecryptAes(bytesEncr, key, IV);
            byte[] bytesPlain = File.ReadAllBytes("C:\\Users\\offsec\\source\\MathLibraryCS.dll");

            Assembly assembly = Assembly.Load(bytesPlain);

            Type type = assembly.GetType(TYPENAME);

            // var ctors = type.GetConstructors(BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Public);
			var privateCtor = type.GetConstructor(BindingFlags.Instance |
                BindingFlags.NonPublic | BindingFlags.Public, null,
                new Type[] { typeof(int), typeof(int) }, null);

            var obj	= privateCtor.Invoke(new Object[] { 3, 4 });

			var method = type.GetMethod("fibonacci_next", BindingFlags.Instance |
                BindingFlags.NonPublic | BindingFlags.Public, new Type[] { });

			var res = method?.Invoke(obj, null);
			res = method?.Invoke(obj, null);
			res = method?.Invoke(obj, null);
			res = method?.Invoke(obj, null);
			res = method?.Invoke(obj, null);

			res = method?.Invoke(obj, null);

			method = type.GetMethod("fibonacci_current");

			res = method?.Invoke(obj, null);

			Console.WriteLine("Result: " + res);

			Console.ReadLine();
		}
    }
}
