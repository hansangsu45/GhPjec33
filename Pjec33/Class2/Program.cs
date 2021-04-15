using System;
using System.Net.Sockets;
using System.Text;
using System.Net;
using static System.Console;

namespace Class2
{
    class Program
    {
        static void Main(string[] args)
        {

            // (1) IP 주소와 포트를 지정하고 TCP 연결 
            TcpClient tc = new TcpClient("127.0.0.1", 7000);
            //TcpClient tc = new TcpClient("localhost", 7000);

            // (2) NetworkStream을 얻어옴 
            NetworkStream stream = tc.GetStream();

            while (true)
            {
                string msg = ReadLine();
                if (msg == "END") break;
                byte[] buff = Encoding.UTF8.GetBytes(msg);

                // (3) 스트림에 바이트 데이타 전송
                stream.Write(buff, 0, buff.Length);

                // (4) 스트림으로부터 바이트 데이타 읽기
                byte[] outbuf = new byte[1024];
                int nbytes = stream.Read(outbuf, 0, outbuf.Length);
                string output = Encoding.UTF8.GetString(outbuf, 0, nbytes);

                WriteLine($"{nbytes} bytes: {output}");
            }

            // (5) 스트림과 TcpClient 객체 닫기
            stream.Close();
            tc.Close();
        }
    }
}
