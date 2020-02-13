# *.NETCoreCSharp* 中级篇2-3

>本节内容为Linq及其拓展方法、Linq中表达式树的使用

## 简介

语言集成查询(LINQ)是一系列直接将查询功能集成到C#语言的技术统称。数据查询历来都表示为简单的字符串，没有编译时类型检查或IntelliSense支持。此外，需要针对每种类型的数据源了解不同的查询语言：SQL数据库、XML文档、各种Web服务等。借助LINQ，查询成为了最高级的语言构造，就像类、方法和事件一样。可以使用语言关键字和熟悉的运算符针对强类型化对象集合编写查询。LINQ系列技术提供了针对对象(LINQtoObjects)、关系数据库(LINQtoSQL)和XML(LINQtoXML)的一致查询体验。

对于编写查询的开发者来说，LINQ最明显的“语言集成”部分就是查询表达式。查询表达式采用声明性查询语法编写而成。使用查询语法，可以用最少的代码对数据源执行筛选、排序和分组操作。可使用相同的基本查询表达式模式来查询和转换SQL数据库、ADO.NET数据集、XML文档和流以及.NET集合中的数据。

在C#中可为以下对象编写LINQ查询：SQLServer数据库、XML文档、ADO.NET数据集以及支持IEnumerable或泛型`IEnumerable<T>`接口的任何对象集合。此外，第三方也为许多Web服务和其他数据库实现提供了LINQ支持。

## Linq基本关键字介绍

- from ...in:与foreach循环类似，将一个实现了IEnumerable接口的数据进行迭代，通常是from 迭代变量 in 数据源的方式；
- select：将数据选中返回集合（IEnumerable类型）
- where：后面接上相关限制语句，例如where a<3，如果直接返回，则会是一个`IQueryable<T>`数据。
- orderby,ThenBy：排序规则，后面接上排序的依据，例如orderby A.Id ThenBy A.Name
- GroupBy：分组依据，返回值回是一个类似于Dictionary的数据（IGouping）
- join：联接操作在不同序列间创建关联，这些序列在数据源中未被显式模块化。例如，可通过执行联接来查找所有位置相同的客户和分销商。在 LINQ 中，join 子句始终作用于对象集合，而非直接作用于数据库表。

通常来说这几种是最为常用的Linq关键字，还有一些关于Linq查询的方法将在后面拓展方法中进行讲解

## Linq基本操作

对于Linq操作，其实非常类似于我们的SQL语句操作，在以前EF并不完善的时候，Linq To Sql是一种非常好用的数据库操作语句。当然，Linq也可以用于IEnumerable及其派生类型的操作。

我们在这实际的进行一次类似于数据库操作的Linq练习，案例是这样的，学生选课系统，那么存在学生与课程的关系，一个学生可以选多门课程，一个课程也可以有多个学生，对于这种关系，我们在代码中难以操作，因此引入中间表SC，记录学生的选课记录。假定我们不使用类似于EF中的集合去记录，只是单纯的使用代码将一切连接起来。

代码如下：

``` C#
    class Student
    {
        public Student(int id,string name)
        {
            StudentId = id;
            Name = name;
        }
        public int StudentId { get; set; }
        public string Name { get; set; }
    }
    class Course
    {
        public int CourseId { get; set; }
        public string CName { get; set; }
        public Course(int sid,string name)
        {
            CourseId = sid;
            CName = name;
        }
    }
    class SC
    {
        public int CourseId { get; set; }
        public int StudentId { get; set; }
        public SC(int cid,int sid)
        {
            CourseId = cid;
            StudentId = sid;
        }
    }
    class Program
    {
        static void Main(string[] args)
        {
            List<Student> students = new List<Student>()
            {
                new Student(1,"Mike"),
                new Student(2,"Jack"),
                new Student(3,"David")
            };
            List<Course> courses = new List<Course>()
            {
                new Course(1,"CSE"),
                new Course(2,"CN"),
                new Course(3,"SWE")
            };
            List<SC> sCs = new List<SC>()
            {
                new SC(1,1),
                new SC(1,2),
                new SC(1,3),
                new SC(2,3),
                new SC(3,2)
            };
            //筛选名称
            var temp = from stu in students
                       where stu.Name == "Jack"
                       select stu;
            //级联多重查询，查询所有学生选课信息
            var temp1 = from stu in students
                        join scs in sCs on stu.StudentId equals scs.StudentId
                        join c in courses  on scs.CourseId equals c.CourseId
                        select new {stu.Name,c.CName};
            foreach(var t in temp1)
            {
                Console.WriteLine("Name:{0},Course:{1}",t.Name,t.CName);
            }
        }
    }
```

## Lambda表达式简要介绍

Lambda表达式是一种看起来高大上的一种东西，本身我是想与委托一起进行讲解的，但是目前所接除到了我们的Linq拓展方法，里面会涉及一些有关Lambda表达式的操作，尤其是Lambda表达式构造表达式树。

Lambda表达式其实非常的简单，他是一种常见的语法糖，你可以将Lambda表达式称为匿名函数，不过在Linq中，他们常用作一种名为匿名委托的方式。我在本节中不会很详细的进行讲解我们的Lambda表达式，只会告诉你如何在Linq中使用。

在Linq中，Lambda表达式通常长这个样子

``` C#
p => p.Id == id && p.Age > 5;
```

=>这个符号，就是lambda表达式的精髓，这个符号之前的p，是函数的返回值，当然也可能是没有的，不过在Linq中，都是有的，因为我们需要一个匿名委托构造表达式树。而后面的所有，则是这个匿名函数的方法体。通常来说，Linq中lambda表达式的方法体都会是一个类似于where判断型的语句，返回值通常是一个bool类型。

## Linq拓展方法

有了前面lambda表达式的一个简单的概念，我们就可以讲解一下Linq中的拓展方法了，拓展方法提供了许多你使用Linq关键字无法实现的操作。

常见的拓展方法有以下几种：

- Where()
- FirstOrDefault()
- Join()
- GroupBy()
- OrderBy()
- Max/Min()

单从单词意思就能理解这些操作，我们使用的时候只需要使用类似p=>p.id即可。
例如:

``` C#
students.Where(p=>p.Name == "Jack").OrderBy(p=>p.StudentId).FirstOrDefault();
```

如果我的文章帮助了您，请您在github.NETCoreGuide项目帮我点一个star，在博客园中点一个关注和推荐。

>[Github](https://github.com/StevenEco/.NetCoreGuide)
>
>[BiliBili主页](https://space.bilibili.com/33311288)
>
>[WarrenRyan'sBlog](https://blog.tity.xyz)
>
>[博客园](https://cnblogs.com/warrenryan)
