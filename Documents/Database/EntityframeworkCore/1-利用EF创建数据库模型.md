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

```
....
林冲大叫一声“啊也！”
....

问:这句话林冲的“啊也”表达了林冲怎样的心里？

答:啊你妈个头啊！

看，一篇文章，给你摘录一段，没前没后，你读不懂，因为有语境，就是语言环境存在，一段话说了什么，要通过上下文(文章的上下文)来推断。
```

事实上EFCore也是这样的，我们在使用EFCore的时候其实是不太关注框架在为我们做了什么。事实上，EFCore中已经保存了各种数据集模型、连接属性等等。

## 模型概述

在EFCore中，一切操作都是使用模型执行数据访问。

## 模型构建

### FluentApi与Attribute

### 实体类型

### 属性类型

### 值映射转换

### 键

### 从属实体（值实体）与无键实体

### 隐藏属性与自动生成属性

### 索引

### 继承

### 关系型数据

#### 一对一

#### 一对多

#### 多对多

## 数据库迁移

## 分散配置关系型数据库