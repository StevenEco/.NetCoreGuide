using System;

namespace CoreStudy循环判断
{
    class Program
    {
        #region 判断
        #region if判断语句
        public static void T1()
        {
            Console.WriteLine("if语句测试，请输入cat，dog或者其他");
            string str = Console.ReadLine();
            if(str =="cat")
            {
                Console.WriteLine("you won a car");
            }
            else if(str == "dog")
            {
                Console.WriteLine("you won a boat");
            }
            else
            {
                Console.WriteLine("you lose");
            }
        }
        #endregion
        #region switch判断语句
        public static void T2()
        {
            Console.WriteLine("switch语句测试，请输入选项:");
            Console.WriteLine("Do you love me?");
            Console.WriteLine("A. Yes\t\tB. Choose A\t\tC. Choose B");
            string str = Console.ReadLine();
            switch (str)
            {
                case "A":
                    Console.WriteLine("WOW");
                    break;
                case "B":
                    goto case "A";
                case "C":
                    goto case "B";
                default:
                    Console.WriteLine("error");
                    break;
            }
        }
        #endregion
        #endregion


        #region 循环
        public static void T3()
        {
            // for循环,注意看continue和break；
            Console.WriteLine();
            Console.WriteLine("for循环:");
            for(int i =0;i<7;i++)
            {
                if(i==3)
                {
                    continue;
                }
                else if(i==5)
                {
                    break;
                }
                Console.Write(i + "\t");
            }
            Console.WriteLine();
            Console.WriteLine("while循环:");
            int j = 0;
            while(j<=3)
            {
                // 注意观察j的值
                Console.Write(++j + "\t");
            }
            int k = 5;
            Console.WriteLine();
            Console.WriteLine("do while");
            do
            {
                Console.WriteLine(k);
            }while(k<5);

            int t =0;
            Console.WriteLine("goto:");
            test:
            if(t<3)
            {
                Console.Write(t+"\t");
                t++;
                goto test;
            }
        }
        #endregion

        static void Main(string[] args)
        {
            Console.WriteLine("if语句：");
            T1();
            Console.WriteLine("----------------------------------");
            Console.WriteLine("switch语句：");
            T2();
            Console.WriteLine("----------------------------------");
            Console.WriteLine("循环语句：");
            T3();
            Console.WriteLine();
            Console.WriteLine("----------------------------------");
        }
    }
}
