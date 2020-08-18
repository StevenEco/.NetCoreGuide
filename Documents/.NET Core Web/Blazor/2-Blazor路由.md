# *.NET Core Blazor* 1-Blazor项目文件分析

>本节内容为Blazor的路由系统

## 简介

路由就是你在浏览器输入框除了网址以外的内容，Blazor已经集成了ASP.NET Core的终结点路由，最典型的配置是将所有请求路由到 Razor 页面，该页面充当 Blazor Server 应用的服务器端部分的主机。

按照约定，“主机”页通常命名为 _Host.cshtml。 主机文件中指定的路由称为回退路由，因为它在路由匹配中以较低的优先级运行。其他路由不匹配时，会考虑回退路由。这让应用能够使用其他控制器和页面，而不会干扰 Blazor Server应用。

## 路由配置模板

如果你创建好了Blazor项目，那么你会在项目文件中看到 **App.razor** 文件，该文件内部存储的就是Blazor的路由模板。当你在浏览器输入

如果我的文章帮助了您，请您在github.NETCoreGuide项目帮我点一个star，在博客园中点一个关注和推荐。

>[Github](https://github.com/StevenEco/.NetCoreGuide)
>
>[BiliBili主页](https://space.bilibili.com/33311288)
>
>[WarrenRyan'sBlog](https://blog.tity.xyz)
>
>[博客园](https://cnblogs.com/warrenryan)