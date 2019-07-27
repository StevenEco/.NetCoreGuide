using System;

namespace CoreStudy03面向对象
{
    struct Test
    {
        public int x;
        public void test(int x)
        {
            this.x = x;
        }
    }

    class Program
    {
        static void Main(string[] args)
        {

            Test t = new Test();
            t.x = 100;
            object a = t;//装箱
            ((Test)a).test(300);//x还是100不变，为什么
            Console.WriteLine(((Test)a).x);
        }
    }
}
