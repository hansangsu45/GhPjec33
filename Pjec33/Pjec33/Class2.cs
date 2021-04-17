using System;
using System.Collections.Generic;
using System.Threading;


class Program
{
    static void Main(string[] args)
    {
        /*for (int i = 1; i <= 9; i++)
        {
            for (int j = 1; j <= 9; j++)
            {
                Console.WriteLine(i + " * " + j + " = " + i * j);
            }

            Console.WriteLine();
        }

        long st = DateTime.Now.Ticks;
        long ct = 0;

        while (st + (10000000) > DateTime.Now.Ticks)
        {
            ct++;
        }

        Console.WriteLine(ct + "만큼 반복했습니다.");*/

        /*int[] array = { 1, 2, 3, 4, 5, 6 };

        for (int i = array.Length - 1; i >= 0; i--)
        {
            Console.WriteLine(array[i]);
        }

        int[] array2 = { 1, 2, 3, 4, 5, 6 };

        foreach (int item in array2)
        {
            Console.WriteLine(item);
        }

        List<string> slist = new List<string>();
        slist.Add("a");
        slist.Add("b");
        slist.Add("c");

        foreach (string item in slist)
        {
            Console.WriteLine(item);
        }

        int length = 10;

        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < i + 1; j++)
            {
                Console.Write("*");
            }

            Console.WriteLine();
        }

        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < length - i - 1; j++)
            {
                Console.Write(" ");
            }

            for (int j = 0; j < i + 1; j++)
            {
                Console.Write("*");
            }

            Console.WriteLine();
        }

        for (int i = 0; i < length; i++)
        {
            for (int j = 0; j < length - i - 1; j++)
            {
                Console.Write(" ");
            }

            for (int j = 0; j < i * 2 + 1; j++)
            {
                Console.Write("*");
            }

            Console.WriteLine();
        }

        for (int i = length - 1; i >= 0; i--)
        {
            for (int j = 0; j < length - i - 1; j++)
            {
                Console.Write(" ");
            }

            for (int j = 0; j < i * 2 + 1; j++)
            {
                Console.Write("*");
            }

            Console.WriteLine();
        }*/

        /*while (true)
        {
            Console.Write("숫자를 입력해주세요(짝수를 입력하면 종료): ");
            int inp = int.Parse(Console.ReadLine());
            if (inp % 2 == 0)
            {
                break;
            }
        }

        for (int i = 0; i < 10; i++)
        {
            if (i % 2 == 0) continue;
            if (i == 5) break;
            Console.WriteLine(i);
        }

        for (int i = 0; i < 100; i++)
        {
            for (int j = 0; j < 100; j++)
            {
                if (i == 50 && j == 50)
                {
                    i = 100;
                    break;
                }
            }
        }

        Console.WriteLine();

        string input2 = "감자 고구마 토마토";
        string input3 = "감자,고구마,토마토";
        string[] inputs = input2.Split(new char[] { ' ' });
        string[] inputs2 = input3.Split(',');

        foreach (var item in inputs)
        {
            Console.WriteLine(item);
        }

        foreach (var item in inputs2)
        {
            Console.WriteLine(item);
        }

        string[] array3 = { "감자", "고구마", "토마토", "가지" };
        Console.WriteLine(string.Join(", ", array3));

        int x = 1;
        while (x < 50)
        {
            Console.Clear();
            Console.SetCursorPosition(x, 5);

            if (x % 3 == 0)
                Console.WriteLine(" __@");
            else if (x % 3 == 1)
                Console.WriteLine("_^@");
            else
                Console.WriteLine("^_@");

            Thread.Sleep(100);
            x++;
        }*/

        /*{
            int i = 0;
            while (i < 10)
            {
                Console.WriteLine("출력");
                i++;
            }
        }

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8 - i - 1; j++)
            {
                Console.Write(" ");
            }

            for (int j = 0; j < (i * 2) + 1; j++)
            {
                Console.Write("*");
            }

            Console.WriteLine();
        }*/

        int[] input = new int[5];

        for (int i = 0; i < 5; i++)
        {
            Console.Write("숫자를 입력해주세요: ");
            input[i] = int.Parse(Console.ReadLine());
        }

        int min = int.MaxValue;
        int max = int.MinValue;

        for (int i = 0; i < 5; i++)
        {
            min = Math.Min(min, input[i]);
            max = Math.Max(max, input[i]);
        }

        Console.WriteLine("\n가장 큰 수: " + max);
        Console.WriteLine("가장 작은 수: " + min);

        string start = "1";
        for (int i = 0; i < 20; i++)
        {
            Console.WriteLine((i + 1) + "번째: " + start);
            string end = "";

            char number = start[0];
            int count = 0;
            for (int j = 0; j < start.Length; j++)
            {
                if (number == start[j])
                {
                    count++;
                }
                else
                {
                    end = end + number + count;
                    number = start[j];
                    count = 1;
                }
            }
            end = end + number + count;
            start = end;
        }
    }
}
