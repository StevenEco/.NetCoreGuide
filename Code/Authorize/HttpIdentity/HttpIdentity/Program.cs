using HttpIdentity.Handlers;
using HttpIdentity.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews(); 

// BA协议
builder.Services.AddAuthentication(BasicAuthenticationScheme.DefaultScheme)
                 .AddScheme<BasicAuthenticationOption, BasicAuthenticationHandler>(BasicAuthenticationScheme.DefaultScheme, 
                 p=>p.Realm = "localhost:5263");


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}

// BA协议
app.UseBA();

app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
