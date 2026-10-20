using PersonalFinanceMng.Application.Extensions;
using PersonalFinanceMng.Infrastructure;
using Serilog;
using Serilog.Formatting.Compact;

var builder = WebApplication.CreateBuilder(args);

// Configuração do Serilog
builder.Host.UseSerilog((context, configuration) =>
{
    configuration.ReadFrom.Configuration(context.Configuration);

    if (context.HostingEnvironment.IsProduction())
    {
        configuration.WriteTo.Console(new CompactJsonFormatter());
    }
    else
    {
        configuration.WriteTo.Console();
    }
});

// Registra os controllers e documentação
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Injeção de dependência das camadas
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseSerilogRequestLogging();

// Configurando Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Personal Finance App v1");
    });
}

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

Log.Information("Iniciando a aplicação... Logger working");

app.Run();