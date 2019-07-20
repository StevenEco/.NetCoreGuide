using System;

namespace CoreStudy03面向对象
{
    [Flags]
    public enum Status
    {
        A = 1,
        B = 2,
        C = 4
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine((Status)7);
        }
    }
}
