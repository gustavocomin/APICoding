using API_Coding;
using IoC;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Repository.Config.Db;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(serverOptions =>
{
    serverOptions.ListenAnyIP(7020);
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<DataContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("APICoding")));

builder.Services.AdicionarDependencias();

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "API Coding", Version = "v1" });

    // Caminho para o arquivo XML de documentação
    var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
    c.IncludeXmlComments(xmlPath);

    c.SchemaFilter<EnumSchemaFilter>();
});

var app = builder.Build();

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/index.html", true);
        return;
    }
    await next();
});


app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "API_Coding v1");
    c.RoutePrefix = "swagger";
});

app.UseAuthorization();
app.MapControllers();

app.Run();
