# *.NETCoreCSharp* 中级篇2-6
>本节内容为Json和XML操作

## 简介
Json和XML文本是计算机网络通信中常见的文本格式，其中Json其实就是JavaScript中的数组与对象，体现了一种面向对象的方式，而XML则是一种可标记语言，类似于我们的html标签，他更多的是体现一种层级关系。
但无论哪一种文本格式，我们都有学习的必要。


## JSON
首先，介绍一下Json:
Json其实就是JavaScript里面的对象和数组，通过不同的组合，可以构成许多不同的数据结构。其中使用花括号的是对象，中括号的是数组，例如:
``` json
{
"data":
    {
        "people":
        [
            {"name":"ppppp","age":18}
        ]
    },
"status":0
}
```
这里面，data就是一个对象，而people就是一个数组。

如果你要处理Json数据，你在nuget上可以找到许多适宜的库，在这里，我是用的是LitJson，这可能是一个比较少见的库，但是我觉得很好用。

这里我给出我们的免费api地址[https://www.sojson.com/api/weather.html](https://www.sojson.com/api/weather.html),这里你可以请求到我们的json文本。

对于LitJson，它充分的阐明了我们Json的核心——数组与对象。对于LitJson，数组使用List<T>，对象则直接创建一个类进行处理。对于上面的例子，我们可以构造出如下的类关系
``` C#
public class data
{
    public List<People> people{ get; set; }
    public int status{ get; set; }
}
// 下面操作将json文本转换成data的对象
Main()
{
    JsonContent jsonc = JsonMapper.ToObject<data>(json);
    foreach(var t in data)
}
```

更多内容可以看我的这篇博文[Json处理实战](https://www.cnblogs.com/WarrenRyan/p/10398638.html)，以及[LitJson的官网](https://litjson.net/)
