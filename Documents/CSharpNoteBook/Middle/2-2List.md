# *.NET Core CSharp* 中级篇 2-2

## 全文目录

[（博客园）.NET Core Guide](https://www.cnblogs.com/WarrenRyan/p/12367312.html)

[（Github）.NET Core Guide](https://github.com/StevenEco/.NetCoreGuide)

>本节内容为List，ArrayList，和Dictionary

## 简介

在此前的文章中我们学习了数组的使用，但是数组有一个很大的问题就是存储空间不足，我们通常的解决方法就是定义一个绝对够用的数组，这通常很大，但是这样就造成了内存的损失。我们总是希望有一个根据需求动态更变的数组进行存储。在上一节中的综合题中已经隐隐约约引出了List的概念。这一讲我们会详细的讲解List。

同时，有时候我们希望数组不单单的存储我们的数据。例如我希望有那么一些数据：

某人的成绩单如下：

- 语文 80分
- 数学 90分
- 英语 87分

对于这些数据，我们使用数组并不能很好的反馈这些成绩，这个时候我们需要使用我们的字典进行存储。

## List、ArrayList

### ArrayList

正如上文所言，数组是一段连续存储空间，访问速度非常快，但是必须指定大小，这个时候我们可以使用ArrayList进行使用。ArrayList是位于System.Collections的一个类，继承与IList接口，提供了数据的操作。它比数组更优的地方是，它不需要指定任何的大小和类型，直接使用即可。

``` CSharp
ArrayList al = new ArrayList();
al.Add("test");
al.Add(1234);
//修改数据
al[1] = 4;
//移除数据
al.RemoveAt(0);
//插入数据
al.Insert(0, "qwe");
```

看起来非常好用对吧，可以插入不同数据并且修改。但是其实这是非常损失性能的一个操作。因为在ArrayList中插入不同类型的数据是是允许的，但是在处理后续数据的时候，ArrayList会将内部所有的数据当成Object类型进行处理，因此在每一个数据进行遍历的时候，都会发生装箱与拆箱的操作，在上一讲我们讨论过，频繁的装拆箱是极其损耗性能的。因此，ArrayList在实际情况下并不经常使用。

## 泛型List

为了解决ArrayList中类型不同导致的不安全和装拆箱，我们使用泛型List类。List类是ArrayList类的泛型等效类，它的大部分用法都与ArrayList相似，因为List类也继承了IList接口。最关键的区别在于，在声明List集合时，我们同时需要为其声明List集合内数据的对象类型，也就是泛型参数。我们在
初级篇的综合习题中已经隐约引出了关于List的部分内容。对于List，它的定义如下：

``` CSharp
List<T> list = new List<T>();
list.Add(new T());
list[0];
list.Remove(T);
```

对于List，它实现了一个非常重要的接口——IEnumerable,这意味着List支持使用foreach循环进行遍历内部元素。不过使用foreach的时候，下列操作时不合法的：

``` CSharp
foreach(var item in MyList)
{
    MyList.Remove(item);//不过我相信没有人那么干，但是....
    //这种操作我不止一次见过有人问我
    if(item.something == something)
    {
        MyList.Remove(item);
    }

}
```

这个时候，你需要往回仔细的回忆我们之前foreach循环的讲解，在foreach循环中通过这种方式动态的删除一个元素是不合法的，为什么？因为foreach循环会调用MoveNext()方法，你可以想象一下一个节点连着一个节点成为了一串集合体，你每次只能向后访问一个节点，也就意味着你必须知晓前一个节点才可以访问后一个节点，假设你访问到某节点的时候，你删除了它，那么后续的节点访问都无法被访问。有没有解决的方法呢？当然有，但是你只能使用for循环，List中有一个属性叫做Count，这个代表着当前List中所拥有的所有元素的个数，并且List实现了索引器，也就是说，List可以通过类似于MyList[0]的方式访问，这个时候，你使用for循环动态删除应当如下：

``` CSharp
for(int i =0;i<MyList.Count;i++)
{
    if(MyList[i].something == something)
    {
        MyList.Remove(MyList[i]);
    }
}
```

## Dictionary字典

你肯定有过简介中提到过的需求。很多时候单纯的索引值没有办法给我们提供更多的信息，我们总是倾向于使用一个键值对的方式进行存储数据。那么Dictionary将会很好的解决你的问题。它的基本结构是由两个泛型参数进行修饰，Dictionary<TKey,TValue>，前面是键的类型，后面是值的类型，你也可以把Dictionary理解成一种特殊的集合。它的使用如下：

``` CSharp
Dictionary<string,string> dict = new Dictionary<string,string>();
dict.Add("广东","广州");
dict.Add("江西","南昌");
dict["江西"];
dict.remove("广东");
```

通常来说，我们很少使用foreach直接访问Dictionary，因为迭代的结果就是一个个键值对，一般Dictionary的Value以List居多，因此一般都是迭代Key。

Dictionary大部分操作和List是接近的，这里就不过多阐述。

## IEnumerable与IList接口

这两个接口时集合（List）的实现的重要接口，IEnumerable提供了迭代功能，IList提供了相应的集合操作，我们从元数据中就可以很好的学习他们。

### IEnumerable接口

它在元数据的定义如下：

``` CSharp
    public interface IEnumerable<out T> : IEnumerable
    {
        //
        // 摘要:
        //     Returns an enumerator that iterates through the collection.
        //
        // 返回结果:
        //     An enumerator that can be used to iterate through the collection.
        IEnumerator<T> GetEnumerator();
    }
```

我们可以很清楚的发现泛型参数中有out关键字修饰，也就是说，我们的IEnumerable是支持协变的。我们可以很轻松的将IEnumerable类型的数据转换成其他数据，例如：

``` CSharp
IEnumerable<string> strs = new IEnumerable<string>();
IEnumerable<object> obj = strs;
```

因此我通常在使用的时候，我会推荐使用IEnumerable来代替List的一些数据操作。

### IList接口

老规矩，先看看元数据

``` CSharp
 public interface IList<T> : ICollection<T>, IEnumerable<T>, IEnumerable
 {
     //省略
 }
 ```

 这里就可以发现IList并不支持协变，属于不变式，那么下列用法是不合法的：

 ``` CSharp
IList<string> strs = new IList<string>();
IList<object> obj = strs;
```

如果我的文章帮助了您，请您在github .NET Core Guide项目帮我点一个star，在博客园中点一个关注和推荐。


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
