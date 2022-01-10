using System;
using System.Runtime.InteropServices;

namespace CoreStudy01
{


    #region 提高拓展部分（内容超纲，仅供参考，需要掌握面向对象技能点)
    // 比较下列四个结构体的存储区别
    #region 结构体代码
    /*
        S1:   i(4 byte)j(4byte)
              -----------------
                  k(8byte)

        size: 16 byte
    */
    public struct S1
    {
        public int i { get; set; }
        public int j { get; set; }
        public long k { get; set; }
    }
    /*
        S2:    i(4byte)empty(4byte)
              ---------------------
                    k(8byte)
              ---------------------
               k(4byte)empty(4byte)

        size: 24 byte
    */
    public struct S2
    {
        public int i { get; set; }
        public long j { get; set; }
        public int k { get; set; }
    }
    /*
        S3:   i(1 byte)empty(7byte)
              ---------------------
              str(4byte)empty(4 byte)
              ---------------------
                    k(8byte)

        size: 24 byte
    */
    public struct S3
    {
        public string str { get; set; }
        public char i { get; set; }
        public long k { get; set; }
    }
    /*
        S4:   i(4 byte)empty(4byte)
              ---------------------
                    k(8 byte)
              ---------------------
                    S1(16byte)

        size: 32 byte
    */
    public struct S4
    {
        public int i { get; set; }
        public S1 j { get; set; }
        public long k { get; set; }
    }
    #endregion

    #region 运算符重载将+变成*+
    public struct Test
    {
        //Test相加变成i1*j2+j2*i1
        public int i { get; set; }
        public int j { get; set; }
        public static int operator +(Test t1, Test t2)
        {
            return t1.i*t2.j + t2.i*t1.j;
        }
        
    }

    #endregion
    #endregion
    class Program
    {
        
        #region 变量定义及转换
        private static void T1()
        {
            //变量定义的原则是不可以使用系统关键字,不可以以数字开头
            //注意后面带f
            float f = 1.3f;
            string str = "I Love Microsoft";
            string number = "135";
            int a = 1;
            char b = 'A';
            //隐式类型转换
            a = b;
            Console.WriteLine(a);
            //显式转换的两种方法
            a = Convert.ToInt32(number);
            str = a.ToString();
            a = int.Parse(number);
            Console.WriteLine(a);
        }
        #endregion
        #region 运算符操作
        private static void T2()
        {
            double x = 3;
            double y = 5;
            //加减乘除与数学无异 x+y,x-y,x*y,x/y,字符串只支持加法进行拼接
            Console.WriteLine(y/x);
            // 求余运算，即求余数
            Console.WriteLine(y%x);
            int x1 = 9;
            int y1 = 7;
            //按位运算
            Console.WriteLine("^:{0}\t&:{1}\t|:{2}\t~:{3}\t>>:{4}\t<<:{5}",x1^y1,x1&y1,x1|y1,~y1,x1>>1,x1<<1);
            /*
            ^:14    &:1     |:15    ~:-10   >>:4    <<:18
            9的二进制是0000 1001,
            7的二进制是0000 0111，
            ^是异或,result=1110,就是说异是不同返回1，相同是0，或就是只要有1就返回1。
            &是与,  result=0001,也就是相同返回1，不同为0
            |是或,  result=1111,除了两个都为0，否则返回1
            ~称为按位取反，我们表示符号是用四个0表示，运算规则就是正数的反码，补码都是其本身的源码，
            负数的反码是符号位不变，本身的0变1,1变0，补码就是反码+1，
            最后进行补码取反时连同符号位一起变得到的反码就是结果
            流程如下：0000 0111 --> 0000 1000 --> 0000 1001 --> 1111 0110 = -8
            >>称为右移,右移一位流程如下 0000 1001 --> 0000 0100 = 4
            <<称为左移,左移一位流程如下 0000 1001 --> 0000 10010 = 18
             */ 
        }
        #endregion
        #region 数组操作
        public static void T3()
        {
            //必须指定数组初始大小
            int [] a = new int [5];
            //将数组初始化五个数组成
            int [] b = new int []{1,2,3,4,5};
            //通过索引获取内容，从0开始
            Console.WriteLine(b[0]);
            // 多维数组定义
            int[,] matrix = new int[3, 4];
        }
        #endregion
        #region 计算结构体大小
        public static void TQ1()
        {
            S1 s1 = new S1();
            S2 s2 = new S2();
            S3 s3 = new S3();
            S4 s4 = new S4();
            int i = Marshal.SizeOf(s1);
            int j = Marshal.SizeOf(s2);
            int k = Marshal.SizeOf(s3);
            int l = Marshal.SizeOf(s4);
            Console.WriteLine("Size: S1: {0}\tS2: {1}\tS3: {2}\tS4: {3}",i,j,k,l);
        }

        #endregion
        #region 运算符重载
        public static void TQ2()
        {
            Test t1 = new Test();
            t1.i = 2;
            t1.j = 3;
            Test t2 = new Test();
            t2.i = 5;
            t2.j = 6;
            Console.WriteLine(t1+t2);
        }

        #endregion
        
        static void Main(string[] args)
        {
            // 变量定义及转换
            Console.WriteLine("变量定义及转换");
            T1();
            Console.WriteLine("---------------------------------------");
            //位运算符使用
            Console.WriteLine("位运算符使用");
            T2();
            Console.WriteLine("---------------------------------------");
            //数组
            Console.WriteLine("数组使用");
            T3();
            Console.WriteLine("---------------------------------------");
            
            // 结构体
            Console.WriteLine("结构体");
            TQ1();
            Console.WriteLine("---------------------------------------");
            //运算符重载
            Console.WriteLine("运算符重载");
            TQ2();
            Console.WriteLine("---------------------------------------");
        }
    }
}