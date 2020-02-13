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

对于LitJson，它充分的阐明了我们Json的核心——数组与对象。对于LitJson，数组使用`List<T>`，对象则直接创建一个类进行处理。对于上面的例子，我们可以构造出如下的类关系

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

## XML

XML也是广泛应用于网络信息交换的一种常见文本格式，他的书写有点类似于我们的html，正如之前所说，他更多的是阐明一种层级关系。例如下文便是一个常见的xml文本的格式。

``` XML
<Computers>
  <Computer ID="11111111" Description="Made in USA">
    <name>Surface</name>
    <price>5000</price>
  </Computer>
  <Computer ID="2222222" Description="Made in USA">
    <name>IBM</name>
    <price>10000</price>
  </Computer>
    <Computer ID="3333333" Description="Made in USA">
    <name>Apple Mac</name>
    <price>10000</price>
  </Computer>
</Computers>
```

在C#中，我们操作XML同样的有许多库，这里我们使用XmlDocument进行操作。

XmlDocument类中的常用方法：

- Load(string path)加载文件路径的Xml
- SelectSingleNode(string node)选中节点
- ChildNodes，属性不是函数，用于获取所有子节点，返回XmlNodeList对象
- HasChildNodes属性，判断是否有子节点
- CreateElement创建新节点
- AppendChild(XmlElement node)追加子节点
- InsertBefore(XmlElement node,XmlElement ChildeNodes)向前插入
- SetAttribute(string name,string value)为指定节点的新建属性并赋值
- InnerText属性，获取内部文本
- Save()保存

``` C#
    XmlDocument myXmlDoc = new XmlDocument();
    //加载xml文件（参数为xml文件的路径）
    myXmlDoc.Load(xmlFilePath);
    //获得第一个姓名匹配的节点（SelectSingleNode）：此xml文件的根节点
    XmlNode rootNode = myXmlDoc.SelectSingleNode("Computers");
    //分别获得该节点的InnerXml和OuterXml信息
    string innerXmlInfo = rootNode.InnerXml.ToString();
    string outerXmlInfo = rootNode.OuterXml.ToString();
    //获得该节点的子节点（即：该节点的第一层子节点）
    XmlNodeList firstLevelNodeList = rootNode.ChildNodes;
    foreach (XmlNode node in firstLevelNodeList)
    {
        //获得该节点的属性集合
        XmlAttributeCollection attributeCol = node.Attributes;
        foreach (XmlAttribute attri in attributeCol)
        {
            //获取属性名称与属性值
            string name = attri.Name;
            string value = attri.Value;
            Console.WriteLine("{0} = {1}", name, value);
        }

        //判断此节点是否还有子节点
        if (node.HasChildNodes)
        {
            //获取该节点的第一个子节点
            XmlNode secondLevelNode1 = node.FirstChild;
            //获取该节点的名字
            string name = secondLevelNode1.Name;
            //获取该节点的值（即：InnerText）
            string innerText = secondLevelNode1.InnerText;
            Console.WriteLine("{0} = {1}", name, innerText);
            //获取该节点的第二个子节点（用数组下标获取）
            XmlNode secondLevelNode2 = node.ChildNodes[1];
            name = secondLevelNode2.Name;
            innerText = secondLevelNode2.InnerText;
            Console.WriteLine("{0} = {1}", name, innerText);
        }
    }
}
    catch (Exception ex)
    {
         Console.WriteLine(ex.ToString());
    }
```

博主不常用xml，更多内容请参考微软官方文档以及https://www.cnblogs.com/zhengwei-cq/p/7242979.html的这篇博文

如果我的文章帮助了您，请您在github.NETCoreGuide项目帮我点一个star，在博客园中点一个关注和推荐。

>[Github](https://github.com/StevenEco/.NetCoreGuide)
>
>[BiliBili主页](https://space.bilibili.com/33311288)
>
>[WarrenRyan'sBlog](https://blog.tity.xyz)
>
>[博客园](https://cnblogs.com/warrenryan)
