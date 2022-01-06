# *.NET Core CSharp*初级篇 1-4

## 全文目录

[（博客园）.NET Core Guide](https://www.cnblogs.com/WarrenRyan/p/12367312.html)

[（Github）.NET Core Guide](https://github.com/StevenEco/.NetCoreGuide)

>本节内容为this、索引器、静态、常量以及只读

## 简介

在之前的课程中，我们谈论过了静态函数和字段的一小部分知识，本节内容中，我们将详细的讲解关于对象操作的例子，以及更加深入的解释面向对象。

## 常量

常量，顾名思义，就是一直为同一个值的变量，并且值不可以被改变。在C#中，定义一个常量需要使用const关键字声明。常量并不占用内存的空间。在C#程序编译的时候，编译器会把任何使用了该常量替换成这个值。

因为常量并不存储在内存中，因此常量只允许使用内置的数值类型，例如：bool、char、string、enum。并且声明的同时必须对常量进行初始化。

例如我们应用的版本号，通常在应用编译完成之后都是以一个常量存在，也不需要对他进行操作。看下列代码。

``` CSharp
public const string Version="v2.1.1"

public string getString(string msg)
{
    return "Copyright@2019" + msg + Version;
}
```

上述函数代码在编译时，将会变成：

``` CSharp
public string getString(string msg)
{
    return "Copyright@2019" + msg + "V2.1.1";
}
```

因为常量的上述特性，如果Y程序集使用了X程序集中的这个Version常量，如果X修改了该常量为“2.1.2”并重新编译，若Y不重新编译，Y中常量还是“2.1.1”，因为该常量会被直接固化于Y中并用常量值替换变量名。因此需要y进行重新编译才会使用新的常量值。

## 静态

静态是一个很常用的语法，我们可以在类中构造各种静态成员，例如静态字段、函数等等。再C#中定义静态成员的方法是使用修饰符static，调用的时候只需要使用“类名.成员名”。

在之前的课程中，我顺带提过一次，静态是一个只初始化执行一次，属于全体共有的一个东西，也可以说是该静态成员属于类本体所有，而不是每一个对象所有。我们就从静态构造函数、静态字段、静态函数这三块进行一个详细的讲解。

### 静态构造函数

我们之前以及对构造函数进行过一个简单的介绍，构造函数是在类被初始化（实例化）的时候调用，并且每创建一个对象就会调用一次构造函数。

而静态构造函数是每一个类型执行一次，也就是这个类型的所有对象公用一个静态构造函数。这区别与普通构造函数的一个对象执行一次。并且对于静态构造函数而言，一个类只允许有一个静态构造函数，并且必须无参。

静态构造函数在你访问一个类型的静态成员的时候，或者实例化一个类型的时候，编译器会自动的调用静态构造函数。

特别的，因为该初始化的构造函数（静态构造函数）属于所有变量共有并且会调用，那么假设该构造函数报错，那么这个类将再程序剩余生命周期内无法再被使用。

### 静态字段

静态字段也是一样，属于一切成员公有，在任何地方你都可以不实例化类的情况下对静态字段操作。

对于静态字段的初始化，分为两种情况：

- 假定类存在一个静态构造函数，那么静态字段在静态构造函数被调用的一瞬间就会初始化；
- 假定不存在静态构造函数，那么静态字段将会被类型被使用之前的一瞬间初始化（最晚执行)，或者更早，在运行时的某一时间（并无法确定）被初始化。

静态字段初始化的顺序则与定义时的顺序一致，例如：

``` CSharp
class A
{
    public static int X = Y;
    public static int Y = 15;
}
.....
Console.WriteLine("X:{0},Y:{1}",X,Y)
```

上例中，X,Y的初始化顺序是X先被初始化，此时Y没有初始化，则是0，因此输出是X：0，Y：15。

### 静态函数

与之前一样，静态函数可以在不实例化类的情况下调用，但是注意，在静态函数中，不允许使用任何非静态的字段。调用的时候直接使用类名.函数名()即可。

### 静态类

如果一个类，被声明为静态类，那么该类不可以被实例化，也不可以被继承，同时不可以包含非静态成员。非静态类中，可以包含静态成员。

## 只读

只读用于字段的访问控制，使用readonly关键字，通常情况下也可以使用无set访问器的属性进行实现。

``` CSharp
class A
{
    public string test{get;}
    public readonly string _test;
}
```

### 静态成员的生命周期

从程序开始初始化到程序结束，因此滥用静态会导致性能问题。

## this关键字

在C#中，this关键字表示操作的当前对象，在类里面，如果你使用this关键字，则this指代的是你每次操作对象时的当前对象。特别的，如果你的函数形参名称和字段名相同，并且你需要同时操作这个两个变量，那么你需要使用this关键字去指明你的操作对象，例如：

``` CSharp
class A
{
    private string data;
    public string Data{get{return data;}}

    public void SetData(string data)
    {
        //this.data表示是当前对象的字段data
        this.data = data;
    }
}
```

## 索引器

在之前的数组操作中，相信大家都发现了数组的访问通过了一个中括号和下标的方式进行访问，这个东西我们称为索引器。但是在类中的索引器可以以任何类型作为索引的key，这使得整个类的拓展性变得很好。

如何去定义一个索引器呢？这里就需要用到我们的this关键字了。定义的方式有点类似我们对于属性的定义

``` CSharp
public class A
{
    public double[] arry{get;set;}
    public double this [int index]
    {
        get
        {
            return arry[index];
        }
        set
        {
            arry[index] = value;
        }
    }
}
```

通过索引器，我们可以自己定义各种不同的索引方式，而不用拘泥于下标访问

## 习题

1.请问下列代码输出什么？为什么？

``` CSharp
class A
{
    public static A a = new A();
    public static int X = 3();
    A()
    {
        Console.WriteLine(X);
    }
}
class Program
{
    static void Main()
    {
        Console.WriteLine(A.X);
    }
}
```

2.试着使用索引器，写出一个二维数组的索引访问，要求实现倒序访问（即a[0]访问最后一位）


<br/>
<p id="PSignature" style="padding-top: 10px; padding-right: 10px; padding-bottom: 10px; padding-left: 60px; background: url(&quot;https://www.cnblogs.com/images/cnblogs_com/ECJTUACM-873284962/1318325/o_o_122329534672560.png&quot;) #e5f1f4 no-repeat 1% 50%; font-family: 微软雅黑; font-size: 12px; border: #e0e0e0 1px dashed"> <br>
        作　　者：<strong><span style="font-size: 12px; color: red"><a href="https://www.cnblogs.com/WarrenRyan/" target="_blank">WarrenRyan</a></span></strong>
        <br>
        出　　处：<a href="https://www.cnblogs.com/WarrenRyan/" target="_blank">https://www.cnblogs.com/WarrenRyan/</a>
        <br>
        关于作者：热爱数学、热爱机器学习，喜欢弹钢琴的不知名小菜鸡。
        <br>
        版权声明：本文版权归作者所有，欢迎转载，但未经作者同意必须保留此段声明，且在文章页面明显位置给出原文链接。若需商用，则必须联系作者获得授权。
        <br>
        特此声明：所有评论和私信都会在第一时间回复。也欢迎园子的大大们指正错误，共同进步。或者<a href="http://msg.cnblogs.com/msg/send/WarrenRyan">直接私信</a>我
        <br>
        声援博主：如果您觉得文章对您有帮助，可以点击文章右下角<strong><span style="color: #ff0000; font-size: 18pt">【<a id="post-up">推荐</a>】</span></strong>一下。您的鼓励是作者坚持原创和持续写作的最大动力！
        <br>
        <br>
        <br>
        博主一些其他平台：
        <br>
        <strong><a>微信公众号：寤言不寐</a></strong>
        <br>
        <strong><a href="https://space.bilibili.com/33311288" target="_blank">BiBili——小陈的学习记录</a></strong>
        <br>
        <strong><a href="https://github.com/StevenEco" target="_blank">Github——StevenEco</a></strong>
        <br>
        <strong><a href="https://space.bilibili.com/667199655" target="_blank">BiBili——记录学习的小陈（计算机考研纪实）</a></strong>
        <br>
        <strong><a href="https://juejin.cn/user/3756401007016173" target="_blank">掘金——小陈的学习记录</a></strong>
        <br>
        <strong><a href="https://space.bilibili.com/33311288" target="_blank">知乎——小陈的学习记录</a></strong>
        <br>
    </p>
<h1>联系方式：</h1>
<a style="font-family: 微软雅黑; font-size: 18px;" href="mailto:cxtionch@gmail.com">电子邮件：cxtionch@live.com</a>
<br/>
<br/>
<p style="font-family: 微软雅黑; font-size: 18px;">社交媒体联系二维码：</p>
<img style=" width: 100%" src="https://images.cnblogs.com/cnblogs_com/WarrenRyan/2090249/o_220106070541_%E4%B8%AA%E4%BA%BA%E4%BF%A1%E6%81%AF%E6%A0%8F.jpg"/>
