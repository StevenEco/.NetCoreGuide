# *.NET Core ASP.NET Core* Basic 1-1
>本节内容为WebHost与项目配置

## 项目配置文件
我们可以很清楚的发现在我们的文件中含有一个Json文件——*appsettings.json*，实际上，这个文件就是我们项目的默认配置文件，它内部含有了默认的一些设置，当然你也可以自己进行添加或者修改。这里我们不展开讲述。我们会在本文的后部分进行讲解如何读取、操作配置文件。

## 项目主入口Program
在ASP.NET Core2.X的空项目中，你会发现有以下两个类存在——StarUp、Program，其中Program类里面就是我们ASP.NET Core的入口（Main函数）。我们可以看一下Program类
``` C#
namespace ASP.NET_Core_Study
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>();
    }
}
```
我们可以发现，Program中使用了一个 *CreateWebHostBuilder* 函数进行程序的初始化，里面就涉及到了我们的WebHost。

那么什么是WebHost呢？你可以简单的理解为就是我们的Web项目的服务主机，它是ASP.NET Core的核心，它调用了CreateDefaultBuilder方法进行创建。而泛型函数使用的StartUp则是我们服务的配置类。

当然，WebHost进行服务主机创建的时候你也可以使用 *WebHost.Start()* 进行手动的配置与创建。这里我们只针对官方默认的方法进行学习与分析。

首先看到CreateDefaultBuilder()的源码，这个你可以在Github或者是ReSharp的反编译功能查看到，这里我是直接从GitHub公开的源码复制
``` C#
 /// <summary>
        /// Initializes a new instance of the <see cref="WebHostBuilder"/> class with pre-configured defaults.
        /// </summary>
        /// <remarks>
        ///   The following defaults are applied to the returned <see cref="WebHostBuilder"/>:
        ///     use Kestrel as the web server and configure it using the application's configuration providers,
        ///     set the <see cref="IHostingEnvironment.ContentRootPath"/> to the result of <see cref="Directory.GetCurrentDirectory()"/>,
        ///     load <see cref="IConfiguration"/> from 'appsettings.json' and 'appsettings.[<see cref="IHostingEnvironment.EnvironmentName"/>].json',
        ///     load <see cref="IConfiguration"/> from User Secrets when <see cref="IHostingEnvironment.EnvironmentName"/> is 'Development' using the entry assembly,
        ///     load <see cref="IConfiguration"/> from environment variables,
        ///     load <see cref="IConfiguration"/> from supplied command line args,
        ///     configure the <see cref="ILoggerFactory"/> to log to the console and debug output,
        ///     and enable IIS integration.
        /// </remarks>
        /// <param name="args">The command line args.</param>
        /// <returns>The initialized <see cref="IWebHostBuilder"/>.</returns>
        public static IWebHostBuilder CreateDefaultBuilder(string[] args)
        {
            var builder = new WebHostBuilder();
            if (string.IsNullOrEmpty(builder.GetSetting(WebHostDefaults.ContentRootKey)))
            {
                builder.UseContentRoot(Directory.GetCurrentDirectory());
            }
            if (args != null)
            {
                builder.UseConfiguration(new ConfigurationBuilder().AddCommandLine(args).Build());
            }
            builder.ConfigureAppConfiguration((hostingContext, config) =>
            {
                var env = hostingContext.HostingEnvironment;
                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                      .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true);
                if (env.IsDevelopment())
                {
                    var appAssembly = Assembly.Load(new AssemblyName(env.ApplicationName));
                    if (appAssembly != null)
                    {
                        config.AddUserSecrets(appAssembly, optional: true);
                    }
                }
                config.AddEnvironmentVariables();
                if (args != null)
                {
                    config.AddCommandLine(args);
                }
            })
            .ConfigureLogging((hostingContext, logging) =>
            {
                logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                logging.AddConsole();
                logging.AddDebug();
                logging.AddEventSourceLogger();
            }).
            UseDefaultServiceProvider((context, options) =>
            {
                options.ValidateScopes = context.HostingEnvironment.IsDevelopment();
            });
            ConfigureWebDefaults(builder);
            return builder;
        }

```
你在这里可以非常清楚的看到，这个函数他会取寻找appsettings.json以及appsettings.{env.EnvironmentName}.json。因此在使用默认的方法进行项目实战时，我们不应该修改这个文件的文件名。并且我们可以发现，这个文件是分两部分的，一部分是我们的发布版本（publish），一部分是我们的开发版本。他们的文件名是不同的，在源码中我们也可以找到这些语句。同时我们的builder调用了UseConfiguration方法，这是一个参数配置的方法，用于接收我们从控制台输入的一些参数。

并且，我们可以看到他还进行了服务的替换UseDefaultServiceProvider以及根目录的设置UseContentRoot，源码如下
``` C#

public static IWebHostBuilder UseContentRoot(this IWebHostBuilder hostBuilder, string contentRoot)
{
    if (contentRoot == null)
    throw new ArgumentNullException(nameof (contentRoot));
    return hostBuilder.UseSetting(WebHostDefaults.ContentRootKey, contentRoot);
}

public IWebHostBuilder UseSetting(string key, string value)
{
    this._config[key] = value;
    return (IWebHostBuilder) this;
}
```

说到这里，我需要引出我们ASP.NET Core的两种启动方式。一种是使用我们微软官方的IIS进行项目的部署，他的本质是将dll注入到IIS服务中。当然，我们知道IIS是Windows上的Web服务器，如果我们要进行跨平台的开发，我们可以使用微软开发的Kestrel，源码中的ConfigureWebDefaults函数的源码我们就可以看到相关的操作
``` C#
internal static void ConfigureWebDefaults(IWebHostBuilder builder)
        {
            builder.UseKestrel((builderContext, options) =>
            {
                options.Configure(builderContext.Configuration.GetSection("Kestrel"));
            })
            .ConfigureServices((hostingContext, services) =>
            {
                // Fallback
                services.PostConfigure<HostFilteringOptions>(options =>
                {
                    if (options.AllowedHosts == null || options.AllowedHosts.Count == 0)
                    {
                        // "AllowedHosts": "localhost;127.0.0.1;[::1]"
                        var hosts = hostingContext.Configuration["AllowedHosts"]?.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                        // Fall back to "*" to disable.
                        options.AllowedHosts = (hosts?.Length > 0 ? hosts : new[] { "*" });
                    }
                });
                // Change notification
                services.AddSingleton<IOptionsChangeTokenSource<HostFilteringOptions>>(
                            new ConfigurationChangeTokenSource<HostFilteringOptions>(hostingContext.Configuration));

                services.AddTransient<IStartupFilter, HostFilteringStartupFilter>();

                services.AddRouting();
            })
            .UseIIS()
            .UseIISIntegration();
        }

```
我们可以发现，在这里，它会从配置文件中读取相关的配置进行配置我们启动使用的服务选项。UseKestrel函数对一些服务进行了注册，以便后续的使用。这里我不打算进行详细的讲解，因为这一节内容只是希望你能够对项目的启动有一个初步的认识，你需要知道的是，目前位置，我们发现了ASP.NET Core程序会在实例化WebHost之前配置好一切，并且我们可以使用一个开源跨平台的Web服务器Kestrel。

## StartUp
StartUp类是一个配置性质的类，它里面有Configure(IApplicationBuilder app, IHostingEnvironment env)以及ConfigureServices(IServiceCollection services)方法，这两个方法中ConfigureServices是我们的服务配置方法或者说是容器配置，它主要用于配置我们依赖注入的类，而Configure方法是我们配置中间件的地方，这两个概念我会在后一两篇文章中进行非常详细的讲解。


## 总结
本篇文章内容并不是很多，主要是让你知道ASP.NET Core其实只是一个简答的控制台程序，只是它调用了一些服务而已。

总体来说我们ASP.NET Core的WebHost创建的过程是

- 配置好服务器UseKestrel()
- 配置好根目录UseContentRoot()
- 添加配置文件源ConfigureAppConfiguration((context, builder) => builder.AddJsonFile("appsetting.json", true, true)) 
- 注册日志记录到控制台ConfigureLogging(builder => builder.AddConsole())
- 配置好StartUp类，进行依赖注入和中间件的配置UseStartup<Startup>()
- 生成WebHost，Build()返回一个WebHost对象
- 运行WebHost，Run()将WebHost启动

当然IWebHostBuilder接口也有一些其他的常见方法，这里我做一个简单的列举
- UseUrls，配置启动的Ip地址
- UseSetting，使用新的文件替换默认appsettings.json
- UseShutdownTimeout，自动关机
- UseEnvironment，配置启动时的环境（生产Or开发）

这几个是我经常使用的函数，其他的函数你可以在微软官方API文档或者是ASP.NET Core的源码中进行学习，并不难。

# Reference
[Asp.netCore 运行](https://neverc.cnblogs.com/p/7988226.html)

如果我的文章帮助了您，请您在github.NETCoreGuide项目帮我点一个star，在博客园中点一个关注和推荐。

>[Github](https://github.com/StevenEco/.NetCoreGuide)
>
>[BiliBili主页](https://space.bilibili.com/33311288)
>
>[WarrenRyan'sBlog](https://blog.tity.xyz)
>
>[博客园](https://cnblogs.com/warrenryan)
