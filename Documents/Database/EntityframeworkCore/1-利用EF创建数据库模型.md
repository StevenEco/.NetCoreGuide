# 利用Entityframework Core创建数据库模型

> 本文中Entityframework Core版本为v3.1.6

## 简介

Entity Framework (EF) Core 是微软轻量化、可扩展、开源和跨平台版的常用 Entity Framework 数据访问技术。
EF Core 可用作对象关系映射程序 (O/RM)，以便于.NET开发人员能够使用 .NET 对象来处理数据库，这样就不必经常编写大部分数据访问代码了。

在.NET整个技术栈方向上，EFCore有着举足轻重的地位，它是使用率第一的数据库访问框架，虽然在某些性能速度上不如Dapper、FreeSql等框架，但是在普适性方面是最好的。EFCore支持LINQ和拓展方法的数据库操作，在代码可读性、便捷性都有很大优势。

## 预先准备

本文的环境是 *.NET Core3.1，Entityframwork Core 3.1.x，MS SQL SERVER2019*。我们首先创建一个控制台应用，利用*dotnet cli、Powershell或VS中的Nuage包管理控制器*在项目目录输入以下指令安装必备的包：

这里使用的是 *dotnet cli*：

``` bash
dotnet add package Microsoft.EntityFrameworkCore
dotnet add package Microsoft.EntityFrameworkCore.Tools
dotnet add package Microsoft.EntityFrameworkCore.SqlServer
dotnet add package Microsoft.EntityFrameworkCore.Relational
dotnet add package Microsoft.EntityFrameworkCore.Design
```

## 数据库上下文

什么是数据库上下文？我在初学EFCore之时也有这种困惑。在EFCore中，*DbContext*的实现类、子类就是一个数据库上下文。数据库上下文主要是作为承前启后的左右存在，它保存着一切支撑数据库与程序访问的内容。例如以下（节选自知乎用户[@付鹏](https://www.zhihu.com/question/26387327)的部分内容）：

``` bash
....
林冲大叫一声“啊也！”
....

问:这句话林冲的“啊也”表达了林冲怎样的心里？

答:啊你妈个头啊！

看，一篇文章，给你摘录一段，没前没后，你读不懂，因为有语境，就是语言环境存在，一段话说了什么，要通过上下文(文章的上下文)来推断。
```

事实上EFCore也是这样的，我们在使用EFCore的时候其实是不太关注框架在为我们做了什么。事实上，EFCore中已经保存了各种数据集模型、连接属性等等。

![dbcontext](https://images.cnblogs.com/cnblogs_com/WarrenRyan/1643641/o_200723074231aspnetcore_dbcontext_1.png)

在实际运用中，我们通常这样去实现这样一个数据库上下文：

``` C#
class TestContext : DbContext
{
    public DbSet<Test> Tests { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}
```

一般来说写到这个程度也就可以了，我们利用一个类去继承*DbContext*类，这个类就是我们的数据库上下文了。在这个类中，我们可以配置数据库的相关内容,同时也可以重写部分函数逻辑。

我们可以看到有一个DbSet类型的属性，这就是我们常说的模型实体，在这里他也是一个数据集合。

而OnConfiguring方法就是数据库上下文的配置函数了，里面可以配置数据库的连接、拦截器等等各种东西。OnModelCreating主要用于描述各个实体类之间的关系和实体内部的设置。

我们操作数据库实际上都是在通过数据库上下文进行操作。

## 模型构建

在EFCore中，无论你采用何种方式构建数据库，我们都应该利用类对模型进行构建，已达到O/RM的效果。这里我们先提出一个简单的问题：

现在有一个简单的学生选课系统的数据库需要构建，已知涉及到了

- 教师数据
- 学生数据
- 课程数据
- 选课数据

请利用EFCore知识进行构建。

这个问题只是在此处提一嘴，因为后面我们的开发都是基于这个假定的数据库模型进行操作构建。

### FluentApi与Attribute

首先我们介绍FluentApi和Attribute，这里不会涉及太多的代码，代码方面都在后面的文章中进行阐述。这两个东西主要用于标注实体的特性，例如实体映射的表名、列名等。

FlunetApi通过配置领域类来覆盖默认的约定。在EFCore中，我们通过DbModelBuilder类来使用FluentApi，它的功能比数据注释属性更强大，并且在二者冲突或相同时，EFCore会优先选择FluentApi中所定义的内容。通常我们使用FluentApi都是在OnModelCreating使用，利用DbModelBuilder中的方法和lambda表达式，实现配置整个模型类。举个简单的例子：

``` C#
protected override void OnModelCreating(DbModelBuilder modelBuilder)
{
    // 指定模型
    modelBuilder.Entity<TestModel>()
        .ToTable("xxx");
    modelBuilder.Entity<TestModel>()
        .HasKey(p=>p.Id);
}
```

也就是我们通过modelBuilder来指定领域模型类进行设定，这种方式相对来说我比较的推荐，因为它能有效减少问题和方便我们进行维护。

Attribute是通过特性标签的方法来进行设置相关配置，特性标签的文章见：[.NET Core CSharp 中级篇2-8 特性标签](https://www.cnblogs.com/WarrenRyan/p/11484963.html)。通过特性标签去注解领域模型的好处就在于没有额外的配置代码在其他的文件中，有点类似于数据库模型完整对应整个的领域模型类。不过使用Attribute进行操作的时候，对于外键之类的东西，处理起来会非常麻烦，因此不推荐完全使用Attribute的方式对模型进行操作注解。示例如下：

``` C#
[Table("xxx")]
public class TestModel
{
    [Column("TestId"),Key]
    public int Id {get;set;}
}
```

至此，文章已经对数据模型的两种配置方式进行了一个非常简单的讲解，接下来，我们将会利用这两种配置方式构建一个简单的数据库。

### 实体类型

在上下文中包含一种类型的DbSet，这意味着它包含在 EF Core 的模型中;我们通常将此类类型称为实体。EFCore 可以从/向数据库中读取和写入实体实例，如果使用的是关系数据库，EFCore可以通过迁移为实体创建表。也就是说，模型和数据库表是可以画等价关系的。

实体常见的操作是指定表名（或视图）、指定键、指定列和不匹配。对应的FluentApi函数和Attribute如下：

```C#
// 指定表名 name为表名，Schema为架构名
[Table(name:"",Schema ="")]
modelBuilder.Entity<Test>().ToTable("",schema:"")//或ToView
// 指定主键、外键
[Key]、[ForeignKey]
modelBuilder.Entity<Test>().HasKey()//或HasForeignKey、HasNoKey

// 指定列
[Column("")]
modelBuilder.Entity<Test>().ToProperty()

// 不匹配(不检索)
[NotMapped]
modelBuilder.Entity<Test>().Ingore()//排除属性
modelBuilder.Ignore<Test>();
```

对于定义实体，我们有三种方式可以进行定义：

#### 通过DbSet声明

在数据库上下文中声明的非私有DbSet属性会被EFCore识别成实体模型，DbSet就是数据库映射到程序中的数据集合，对此集合的一切操作都会被视为对数据库的操作。

定义DbSet如下，利用泛型指定实体模型：

``` C#
public class Context : DbContext
{
    public virtual DbSet<TestModel> TestModels { get; set;}
}
```

当DbSet所设置的模型下没有使用FluentApi或Attribute时，DbSet的属性名则会对应数据库中实体表的名称。

#### 通过导航属性

导航属性就是一种类之间的关系属性，EFCore在构建我们的数据库模型的时候，并不是单单只盯着被显式声明的内容，EFCore会像递归一样一层一层的进行遍历查找，将有关系的内容统统认证为实体模型。

例如：

```C#
public class A
{
    public virtual B B { get; set;}
}
public class B
{
    public virtual A A { get; set;}
}
public class Context : DbContext
{
    public virtual DbSet<A> A { get; set;}
}
```

像以上这种情况，很明显A，B之间有着一一对应的关系，因此EFCore会将A和B都作为模型实体进行加载，而并不会因为B未声明而不加载。

#### 通过OnModelCreating声明

我们也可以通过重写OnModelCreating声明一个模型类，例如

``` C#
protected override void OnModelCreating(DbModelBuilder modelBuilder)
{
    // 指定模型
    modelBuilder.Entity<TestModel>();
}
```

综上所述，EFCore对模型的查找是只要有所提及，无论是显示的利用DbSet或是利用导航属性，亦或是在OnModelCreating中有所提及，都会被作为模型类进行操作。不过在这里，除非是你非常不需要显示声明，否则，建议使用DbSet进行显示声明，避免对开发带来不必要的麻烦。

不过有时候我们不希望我们模型和数据库对应，因为有些模型只是为了辅助存在或作为一个子属性存在，在数据库中并不存在这个表，那么我们可以使用 *[NotMapped]* 标记，这样该类就不会被EFCore所识别为模型。

#### 示例代码

前文中我们提到了一个学生选课系统的概念模型，这里我们进行一个原始的建库操作。这里会交叉使用FluentApi和Attribute的方式进行书写以方便读者理解。

首先我们建立简单的领域模型

```C#
public enum Gender
{
    Male,
    Female
}

[Table("Test_Student")]
public class Student
{
    public int Id { get; set;}
    public string Name { get; set; }
    public Gender Gender { get; set; }
    public string Remark { get; set; }
    public string StudentId{ get; set; }
}

[Table("Test_Teacher")]
public class Teacher
{
    public int Id {get;set;}
    public string Name { get;set; }
    public Gender Gender { get; set; }
    public string Remark { get; set; }
}

public class Course
{
    public int Id {get;set;}
    public string Name {get;set;}
    public string CourseId{ get; set; }
}
public class Context : DbContext
{
    public virtual DbSet<Student> Students { get; set;}
    public virtual DbSet<Teacher> Teachers { get; set;}
    public virtual DbSet<Course> Courses { get; set;}
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Course>().ToTable("Test_Student");
    }
}
```

这样我们就大概做好了一个简单的数据库框架，不过这个数据模型是不符合我们实际设计的模型的，这里只作为讲解作用，并不作为实际项目的设计。

### 属性类型和转换

光有表设置显然是不够的，我们还需要对表中的字段和属性进行设置。表中的字段，如果是非引用类型，都会被识别成数据库中的列，引用类型在表中通常会作为导航属性存在。按照约定，具有getter和setter的所有公共属性都将包括在模型中映射到表中的列。接下来仍然会分为FluentApi和Attribute进行编写。

在数据库中，我们对一个列的常见操作通常就是操作列名、列类型、排序规则、可空等。这里进行一个归纳和整理

#### 属性类型示例

这里先用数据注解Attribute的方式进行操作，限于篇幅，在此仅对一个类进行操作，读者可根据自己需求对其他类进行编写。

``` C#
[Table("Test_Student")]
public class Student
{
    public int Id { get; set;}
    //属性不能为空
    [Required]
    // 列名，排序顺序，列类型
    [Column(name:"Name",Order =1,TypeName ="varchar(25)")]
    //等效于[Column(name:"",Order =1,TypeName ="varchar(25)"),Required]
    //属性后接等号就是默认值的设置方法
    public string Name { get; set; } = "abcd";
    public Gender Gender { get; set; }
    // 也可以通过设置maxLength属性进行限制长度
    [Column("Remark"),MaxLength(150)]
    public string Remark { get; set; }
    public string StudentId{ get; set; }
}
```

设置可空类型则是在类型后加？号，但是通过数据标注的方式进行设置的方法作者查阅了文档和资料并没有发现，只有FluentApi可以设置。应该是微软直接默认就是空，因此无需指定设置。

这里是FluentApi的方式

``` C#
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Student>().ToTable("Test_Student");
        modelBuilder.Entity<Student>().Property(p => p.Name)
            //非空
            .IsRequired()
            //列名
            .HasColumnName("Name")
            //列类型
            .HasColumnType("varchar(25)")
            //默认值
            .HasDefaultValue("");
        modelBuilder.Entity<Student>().Property(p => p.Remark)
            //可空
            .IsRequired()
            //列名
            .HasColumnName("Remark")
            //可使用.IsFixedLength()设置为定长
            //列最大长度
            .HasMaxLength(150);
    }
```

#### 值映射转换

值转换提供了在写入数据库前的一次数据处理，试想一下，假如你不希望将加密过程放在业务层，或者说你习惯用枚举去存储一些值，但是在数据库中实际需要使用枚举名而不是枚举值，那么我们就需要进行值映射转换。

值转换只在FluentApi中提供，我们可以在OnModelCreating中进行定义，例如：

```C#
protected override void OnModelCreating(ModelBuilder modelBuilder)
{
    modelBuilder.Entity<Student>().Property(p => p.Gender)
        .HasConversion(
            p => p.ToString(),
            p => (Gender)Enum.Parse(typeof(Gender), p));
}
```

在这里，第一个lambda表达式表示你的模型出，第二个则是数据库映射回你的模型类所需的处理。你也可以使用ValueConverter类来定义，例如：

``` C#
var converter = new ValueConverter<string, string>(
    p => p.ToString(),
    p => (Gender)Enum.Parse(typeof(Gender), p));
```

将此对象传入HasConversion方法亦可。

### 键与索引

键是数据库最精华的部分了。

#### 主键和备用（候选）键

#### 外键

#### 索引

### 从属实体（值实体）与无键实体

### 隐藏属性与自动生成属性

### 继承

### 关系型数据

#### 一对一

#### 一对多

#### 多对多

## 数据库迁移

## 分散配置关系型数据库
