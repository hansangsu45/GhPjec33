using System;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Net.Sockets;
using System.Threading;
using static System.Console;
using System.Threading.Tasks;
class Class1
{
    static void Main(string[] args)
    {
        AsyncEchoServer().Wait();
    }

    async static Task AsyncEchoServer()
    {
        TcpListener listener = new TcpListener(IPAddress.Any, 7120);
        listener.Start();
        while (true)
        {
            // 비동기 Accept                
            TcpClient tc = await listener.AcceptTcpClientAsync().ConfigureAwait(false);

            // 새 쓰레드에서 처리
            await Task.Factory.StartNew(AsyncTcpProcess, tc);
        }
    }

    async static void AsyncTcpProcess(object o)
    {
        TcpClient tc = (TcpClient)o;

        int MAX_SIZE = 1024;  // 가정
        NetworkStream stream = tc.GetStream();

        // 비동기 수신            
        var buff = new byte[MAX_SIZE];
        while (true)
        {
            var readTask = stream.ReadAsync(buff, 0, buff.Length);
            var timeoutTask = Task.Delay(10 * 1000); //10secs
            var doneTask = await Task.WhenAny(timeoutTask, readTask).ConfigureAwait(false);

            if (doneTask == timeoutTask)
            {
                var bytes = Encoding.UTF8.GetBytes("Read Timeout Error");
                await stream.WriteAsync(bytes, 0, bytes.Length);
            }
            else
            {
                int nbytes = readTask.Result;
                if (nbytes > 0)
                {
                    string msg = Encoding.UTF8.GetString(buff, 0, nbytes);
                    WriteLine($"{msg} at {DateTime.Now}");
                    if (msg == "EndServer") break;

                    await stream.WriteAsync(buff, 0, nbytes).ConfigureAwait(false);
                }
            }
        }
        stream.Close();
        tc.Close();
    }
 }