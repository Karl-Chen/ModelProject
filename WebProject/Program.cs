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

//1.2.4 �bProgram.cs���H�̿�`�J���g�k���gŪ���s�u�r�ꪺ����
//      ���`�N�{������m�����n�bvar builder = WebApplication.CreateBuilder(args);�o�y����
builder.Services.AddDbContext<GuestModelContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("dbGuestModelConnection")));

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession(options => {
    //TimeOut�ɶ��A�M���N�O�Ҧ�Session�M��
    options.IdleTimeout = TimeSpan.FromMinutes(10);
});

//�o��
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

//1.3.4 �bProgram.cs���g�ҥ�Initializer���{��(�n�g�bvar app = builder.Build();����)
//      ���o��Initializer���@�άO�إߤ@�Ǫ�l��Ʀb��Ʈw���H�Q���աA�ҥH���@�w�n��Initializer��
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
//�o��
//app.MapHub<PushMessage>("/PushMessage");


//�ҥ�Session
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
