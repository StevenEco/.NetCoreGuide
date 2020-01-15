using System;

namespace clr01
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            Console.WriteLine("Clr via C#");
            int []array = new int[5];
            int []array1 = new int[5];
            array1[3]+=5;
            var testClr = (1==2);
            testClr = !testClr;
            Console.ReadLine();
        }
    }
}
