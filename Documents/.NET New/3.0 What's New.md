## .NET Core 3.0
.NET Core 3.0 是 .NET Core 平台的下一主要版本。它新增了许多令人兴奋的功能，如支持使用 Windows 窗体 (WinForms)、Windows Presentation Foundation (WPF) 和实体框架 6 的 Windows 桌面应用程序。对于 Web 开发，它开始支持使用 C# 通过 Razor 组件（旧称为 Blazor）生成客户端 Web 应用程序。此外，它还支持 C# 8.0 和 .NET Standard 2.1。
## IOT
我们正在 .NET Core 3.0. 中添加对物联网 (IoT) 方案的支持。现在可以在 Raspberry Pi 和类似设备上对硬件插针（用于控制设备和读取传感器数据）进行编程，并在所有受支持的 OS 上（例如，使用 Raspberry Pi 或 Arduino）通过串行端口进行通信。我们还在此版本中添加了适用于 ARM64 的 IoT 设备支持，以补充现有 ARM32 功能。

## MachineLearning
.NET Core 3.0 还将完全支持 ML.NET，这是为 .NET 开发人员生成的开放源代码机器学习框架。ML.NET 强力驱动 Azure 机器学习、Windows Defender 和 PowerPoint Design Ideas 等产品。使用 ML.NET，可以将许多常用机器学习方案添加到应用中，如情绪分析、建议、预测、图像分类等。若要了解详细信息，请访问 bit.ly/2OLRGRQ。

我们最近发布了 .NET Core 3.0 的首个预览版。若要详细了解 .NET Core 3.0 和试用预览版，请访问 aka.ms/netcore3preview1。
## WPF
桌面（WinForms 和 WPF）和开放源代码
WinForms 和 WPF 是两个最常用的 .NET 应用程序类型，有数百万开发人员在使用。.NET Core 3.0 开始支持 WinForms 和 WPF，将 Windows 桌面开发引入了 .NET Core。.NET Core 一直以来都是开放源代码的，在 GitHub 中这两个框架与剩余的 .NET Core 一样，也都是开放源代码的。客户将有史以来第一次能够见证这些框架的开放开发，甚至可以通过提问、修复 bug 或帮助在 GitHub 中实时开发新功能，从而提供帮助。WinUI XAML 库也将是开放源代码的。通过 XAML Islands，可以在 WinForms 和 WPF 应用程序中使用这些控件。

许多现有 WinForms 和 WPF 应用程序都使用实体框架来访问数据，因此 .NET Core 也支持实体框架 6。

你可能想要知道，为什么要在 .NET Core 上生成桌面应用程序。很简单：这样才能受益于 .NET Core 中的所有增强功能。不仅可以在最新版框架上生成应用程序，而无需安装 .NET Core，还能将应用程序和 .NET Core 发布到一个 .EXE 中。.NET Core 在设计时考虑到了并行支持，因此可以在一台计算机上安装多个版本，并能将应用程序锁定到设计时定目标到的版本。此外，鉴于这种并行本质，可以改进 .NET Core 中的 API（包括 WinForms 和 WPF），而无需承担损坏应用程序的风险。

## ASP.NET Core 3
不过，.NET Core 3.0 并不都是与桌面相关。还有许多令人兴奋的新功能是针对 Web 设计的。接下来将介绍我们正在开发的几项功能。

客户经常问的一个问题是，如何在 .NET Core 中获得 RPC 体验（就像在 .NET 远程和 Windows Communication Foundation 中一样）。我们正在参与 gRPC (grpc.io) 项目，以确保 gRPC 能够为 .NET 开发人员提供一流支持。

在今年早些时候，我们开始了一项试验，即使用 .NET（我们称之为 Blazor）进行客户端 Web 开发。借助 Blazor，可以编写直接在浏览器中的基于 WebAssembly 的 .NET 运行时内运行的 Web UI 组件，而无需编写一行 JavaScript。使用 Razor 语法创作组件，这些组件与代码一起被编译到常规 .NET 程序集中。然后，程序集和基于 WebAssembly 的 .NET 运行时被下载到浏览器中，仅使用开放式 Web 标准就能执行它们（无需任何插件或代码转换），如图 1 所示。
### Blazor
使用 Blazor 进行客户端 Web 开发 
图 1：使用 Blazor 进行客户端 Web 开发

也可以使用 .NET Core 在服务器上运行相同组件，其中所有 UI 交互和 DOM 更新都是通过 SignalR 连接进行处理，如图 2 所示。执行后，组件跟踪 DOM 所需的更新，并通过 SignalR 连接将要应用的这些更新发送到浏览器。UI 事件使用同一连接发送到服务器。此模型的优点有多个：下载大小更小、代码集中在服务器上，以及受益于在 .NET Core 上运行组件的所有功能和性能优势。
### SignalR
使用 SignalR 在服务器上运行 UI Web 组件 
图 2：使用 SignalR 在服务器上运行 UI Web 组件

对于 .NET Core 3.0，我们将把 Blazor 组件模型集成到 ASP.NET Core 中。我们将此集成组件模型称为“Razor 组件”。Razor 组件开启了以下新时代：使用 ASP.NET Core 的可组合 UI，以及使用 .NET 的完整堆栈 Web 开发。对于 .NET Core 3.0，Razor 组件最初作为独立可路由组件，或通过 Razor Pages 和视图使用的组件在服务器上运行。不过，相同组件也可以在 WebAssembly 上进行客户端运行。在开发 .NET Core 3.0 的同时，我们还将继续着手以下工作：支持使用基于解释器的 .NET 运行时在 WebAssembly 上运行 Razor 组件，预计将在后续版本中提供。之后，我们还计划向 WebAssembly 发布对 .NET 代码的完全预编译支持，这将显著提升运行时性能。

## EF Core 3.0
LINQ 是一项用户钟爱的 .NET 功能，可便于编写数据库查询，而无需离开所选的语言，同时还能利用丰富的类型信息来获取 IntelliSense 和编译时类型检查。不过，LINQ 也支持编写数量几乎不限的复杂查询，而这对于 LINQ 提供程序来说，一直都是一项巨大挑战。EF Core 部分解决了此问题，具体方法是支持选择可转换为 SQL 的查询部分，再执行内存中剩余的查询。在某些情况下，这样做是可取的，但在其他许多情况下，这可能会导致非常低效的查询直到应用程序投入生产才被发现。

在 EF Core 3.0 中，我们计划深入更改 LINQ 实现工作原理和测试方式，旨在提高它的可靠性（例如，避免破坏修补程序版本中的查询）；让它能够将更多表达式正确转换为 SQL；在更多情况下生成高效查询；以及防止直到投入生产才被检测到的非常低效查询出现。

我们一直在致力于开发适用于 EF Core 的 Cosmos DB 提供程序，以便开发人员能够熟悉 EF 编程模型，从而轻松地将 Azure Cosmos DB 定目标为应用程序数据库。目标是利用 Cosmos DB 的一些优势，如全局分发、“始终开启”可用性、弹性可伸缩性和低延迟，甚至包括 .NET 开发人员可以更轻松地访问它。此提供程序将针对 Cosmos DB 中的 SQL API 启用大部分 EF Core 功能，如自动更改跟踪、LINQ 和值转换。

我们计划在 EF Core 3.0 中添加的其他功能包括，属性包实体（将数据存储在索引属性（而不是常规属性）中的实体）；能够将数据库视图反向工程为查询类型；以及与新 C# 8.0 功能集成，如 IAsyncEnumerable<T> 支持和可以为 null 的引用类型。

我们理解，对于许多使用旧版 EF 的现有应用程序来说，移植到 EF Core 的工作量巨大。正因为此，我们还移植了 EF 6，以便能够使用 .NET Core。

## .NET Standard 2.1
如果遵循 .NET Standard，可以创建适用于所有 .NET 实现的库，不仅仅局限于 .NET Core，还包括 Xamarin 和 Unity。在 .NET Standard 1.x 中，我们只对跨各种实现已常用的 API 进行了建模。在 .NET Standard 2.0 中，我们专注于简化将现有 .NET Framework 代码移植到 .NET Core 的过程，这样不仅带来了额外的 20,000 个 API，还带来了兼容性模式（可便于从基于 .NET Standard 的库引用 .NET Framework 库，而无需重新编译它们）。对于这两版标准，几乎没有任何新组件，因为所有 API 都是现有 .NET API。

在 .NET Standard 2.1 中，这一情况已有所改变：我们添加了约 3,000 个几乎全新的 API，它们作为 .NET Core 开放源代码开发的一部分引入。通过将它们添加到标准，我们将它们引入所有 .NET Standard 实现。

这些新 API 包括：
Span<T>：在 .NET Core 2.1 中，我们添加了 Span<T>，这是类似数组的类型，允许以统一方式表示托管和非托管内存，并支持在不复制的情况下进行切片。Span<T> 是 .NET Core 2.1 中与性能最为相关的改进的核心。因为它允许以更高效的方式管理缓冲，所以可以有助于减少分配和复制。若要详细了解此类型，请务必阅读 Stephen Toub 关于 Span<T> 的精彩文章 (msdn.com/magazine/mt814808)。
ValueTask 和 ValueTask<T>：在 .NET Core 2.1 中，基础内容中最重要的功能相关改进，可支持高性能方案 (bit.ly/2HfIXob)，还能让 async/await 更高效。 ValueTask<T> 已有，可便于在操作同步完成时返回结果，而无需分配新 Task<T>。在 .NET Core 2.1 中，我们进一步改进了此功能，同时提高了它的可用性，让它有对应的非泛型 ValueTask，以便在必须以异步方式完成操作的情况下减少分配，这是 Socket 和 NetworkStream 等类型现在利用的功能。
常规实用 API：由于 .NET Core 是开放源代码的，因此我们跨基类库添加了许多小功能，如用于合并哈希代码的 System.HashCode，或 System.String 上的新重载。.NET Core 中约有 800 名新成员，几乎所有这些成员都已添加到 .NET Standard 2.1 中。
如需了解更多详情，请查看 .NET Standard 2.1 公告 (bit.ly/2RCW2fX)。

## C# 8.0

C# 8.0 是下一版 C#，它在几个主要方面改进了语言。可以为 null 的引用类型有助于防止 null 引用异常，并改进了 null 安全编码做法。可以选择启用下列功能：在将 null 分配到类型字符串（举个例子）的变量或参数时看到警告。若要可以为 null，必须使用“string?”可以为 null 的引用类型。

异步流对异步数据流执行的操作，就是 async/await 对单个异步结果执行的操作。新框架类型 IAsyncEnumerable<T> 是 IEnumerable<T> 的异步版本，同样也能执行 foreach 和 yield return：

 ``` C#
public static async IAsyncEnumerable<T> FilterAsync<T>(
  this IAsyncEnumerable<T> source,
  Func<T, Task<bool>> predicate)
{
  await foreach (T element in source)
  {
    if (await predicate(element)) yield return element;
  }
}
```

除了其他功能外，借助默认接口成员实现，接口可以添加新成员，而无需中断现有实现者。Switch 表达式可确保模式匹配更为简洁，不仅可以递归模式，还能将模式深入挖掘到测试值。如需了解 C# 8.0 的更多详情，请访问 aka.ms/csharp8。

## .NET Framework 和 .NET Core 将如何发展？

.NET Framework 是在超过 10 亿台计算机上安装的 .NET 实现，因此需要尽可能保持兼容性。因此，它的更新速度慢于 .NET Core。甚至安全修复和 bug 修复都可能会导致应用程序中断，因为应用程序依赖旧行为。我们将确保 .NET Framework 始终支持最新的网络协议、安全标准和 Windows 功能。

.NET Core 是开放源代码、跨平台且快速更新的 .NET 版本。鉴于这种并行本质，可以对它应用我们无法冒险对 .NET Framework 应用的更改。也就是说，随着时间推移，.NET Core 会增添新 API 和语言功能，而 .NET Framework 则不会。

如果现有 .NET Framework 应用程序，且无需利用任何 .NET Core 功能，就不应该有迁移到 .NET Core 的压力。.NET Framework 和 .NET Core 都将完全受支持；.NET Framework 始终是 Windows 的一部分。甚至在 Microsoft 内部，我们都有很多基于 .NET Framework 的大型产品线，并将一直基于 .NET Framework。但展望未来，.NET Core 和 .NET Framework 包含的功能将会有所不同。

本文取自https://www.cnblogs.com/leolion/p/10585834.html