using System;
using System.Collections.Generic;
using static System.Console;
using System.Linq;
using System.Text;
using System.Collections;

namespace ConsoleApp5
{
    class Program
    {
        static void Main(string[] args)
        {
            //1
            /*int grade = int.Parse(ReadLine());
            switch (grade)
            {
                case 1:
                    WriteLine("Excellent");
                    break;
                case 2:
                    WriteLine("Great");
                    break;
                case 3:
                    WriteLine("Great");
                    break;
                default:
                    WriteLine("Good");
                    break;
            }*/

            //2
            /*while (true)
            {
                int a = int.Parse(ReadLine());
                if (a < 0) break;
            }*/

            //3
            /*for (int i = 1; i <= 10; i++)
            {
                if (i % 2 != 0) continue;
                WriteLine(i);
            }*/

            //4
            /*int count = 0;
            for(int i=1; i<=100; i++)
            {
                if (i % 4 == 0 && i % 5 == 0) count++;
            }
            WriteLine(count);*/

            //5
            /*string[] str = { "감자", "고구마", "토마토" };
            foreach (string s in str) WriteLine(s);*/

            //6
            /*string str = "고구마 토마토";
            char[] a = str.ToCharArray();
            Array.Reverse(a);
            foreach (char c in a) Write(c);*/

            //7
            /*List<string> list = new List<string>();
            for (int i = 0; i < 4; i++)
            {
                string s = ReadLine();
                list.Add(s);
            }
            var v = list.OrderBy(x => x.Length).ThenBy(x => x);
            foreach (string s in v) WriteLine(s);*/

            //8
            /*string s;
            char c='c';
            int d=0;
            while (true)
            {
                s = ReadLine();
                bool a = false, b = false, f=false;
                if (s.Length > 2 && s.Length < 100)
                {
                    f = true;

                }
                else
                {
                    WriteLine("다시 입력하세요");
                    goto ONE;
                }

                for(int i=0; i<s.Length; i++)
                {
                    if (s[i].GetType() == c.GetType()) a = true;
                    bool B = int.TryParse(s[i].ToString(), out int r);
                    if (B) b = true;
                }

                if(a && b && f)
                {
                    break;
                }
                else
                {
                    WriteLine("다시 입력하세요");
                }

            ONE:
                WriteLine(a+" "+b+" "+f);
            }*/

            //9
            /*int[] score = new int[5];
            int sum=0;
            int max = int.MinValue, min = int.MaxValue;

            for (int i=0; i<score.Length; i++)
            {
                score[i] = int.Parse(ReadLine());
            }
            
            for (int i = 0; i < score.Length; i++)
            {
                max = Math.Max(score[i], max);
                min = Math.Min(score[i], min);
            }
            
            for (int i = 0; i < score.Length; i++)
            {
                if (score[i] != min && score[i] != max) sum += score[i];
            }

            float f = sum / 3f;
            WriteLine(string.Format("{0:f2}",f));*/

            //10
            string ip = "192.168.1.13";
            string[] str = ip.Split('.');
            byte[] bytes = new byte[4];
            for (int i = 0; i < 4; i++)
            {
                bytes[i] = Convert.ToByte(str[i]);
            }

            uint value = ((uint)bytes[0] << 24 | (uint)bytes[1] << 16 | (uint)bytes[2] << 8 | (uint)bytes[3] << 0);
            WriteLine(Convert.ToString(value, 16));


        }


    }
}
