using Microsoft.AspNetCore.Cors.Infrastructure;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using WebProject.Filters;

//using WebProject.Hubs;
using WebProject.Models;
using WebProject.Services;
using WebProject.WorkFunction;


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//1.2.4 在Program.cs內以依賴注入的寫法撰寫讀取連線字串的物件
//      ※注意程式的位置必須要在var builder = WebApplication.CreateBuilder(args);這句之後
builder.Services.AddDbContext<GuestModelContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("dbGuestModelConnection")));

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options => {
    //TimeOut時間，清掉就是所有Session清掉
    options.IdleTimeout = TimeSpan.FromMinutes(10);
});

//廢案
//builder.Services.AddSignalR();

builder.Services.AddScoped<FileIOFunction>();
builder.Services.AddScoped<MimaHandler>();
builder.Services.AddScoped<ProductsService>();
builder.Services.AddScoped<MemberServices>();
builder.Services.AddScoped<OrderCarServices>();
builder.Services.AddScoped<OrderServices>();
builder.Services.AddScoped<OrderDetailService>();
builder.Services.AddScoped<StaffServices>();

builder.Services.AddScoped<MemberStatusFilter>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

//1.3.4 在Program.cs撰寫啟用Initializer的程式(要寫在var app = builder.Build();之後)
//      ※這個Initializer的作用是建立一些初始資料在資料庫中以利測試，所以不一定要有Initializer※
using (var scope = app.Services.CreateScope())
{
    var service = scope.ServiceProvider;
    SeedData.Initialize(service);
}
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
//app.UseCors(CorsOptions.AllowAll);
//廢案
//app.MapHub<PushMessage>("/PushMessage");


//啟用Session
app.UseSession();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Products}/{action=Index}");

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();
