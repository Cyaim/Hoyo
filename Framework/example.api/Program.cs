using example.api;
using Hoyo.AutoDependencyInjectionModule.Modules;
using Hoyo.WebCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(c =>
{
    c.Filters.Add(new ActionExecuteFilter());
    c.Filters.Add(new ExceptionFilter());
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// �Զ�ע�����ģ��
builder.Services.AddApplication<AppWebModule>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) _ = app.UseDeveloperExceptionPage();

app.UseHoyoResponseTime();
app.UseAuthorization();

app.MapControllers();

// ����Զ���ע���һЩ�м��.
app.InitializeApplication();

app.Run();
