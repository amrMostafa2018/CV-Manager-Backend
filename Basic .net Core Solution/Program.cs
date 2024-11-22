using Basic.Application;
using Basic.Infrastructure;
using Basic_.net_Core_Solution;
using System.Diagnostics;
using Serilog;
using Serilog.Enricher.ClientInfo;
using Serilog.Enrichers.Sensitive;
using Serilog.Enrichers.Span;
using Serilog.Exceptions;
using Serilog.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services
    .AddProblemDetails(options =>
        options.CustomizeProblemDetails = ctx =>
        {
            ctx.ProblemDetails.Extensions.TryAdd("traceId", Activity.Current?.TraceId.ToString() ?? ctx.HttpContext.TraceIdentifier);
        });


builder.Services.AddApplicationServices();
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddWebServices();

builder.Host.UseSerilog((context, conf) =>
{
    conf.ReadFrom.Configuration(context.Configuration)
    .Enrich.FromLogContext()
    .Enrich.WithEnvironmentName()
    .Enrich.WithMachineName()
    .Enrich.WithProperty("ApplicationName", context.HostingEnvironment.ApplicationName)
    .Enrich.WithExceptionDetails()
    .Enrich.With<ActivityEnricher>()
    .Enrich.WithSensitiveDataMasking(
        options =>
        {
            options.ExcludeProperties.Add("ParentId");
            options.Mode = MaskingMode.Globally;
            options.MaskingOperators = new List<IMaskingOperator>
            {
                new CreditCardMaskingOperator()
            };
        })
    .Enrich.WithClientIp()
    .Enrich.WithClientAgent()
    .WriteTo.Seq(builder.Configuration["SeqConfiguration:Url"]!)

    .WriteTo.File($"Logs/{context.HostingEnvironment.ApplicationName}.txt", rollingInterval: RollingInterval.Day);
});
builder.Services.AddCors(opt =>
{
    opt.AddPolicy(name: "CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
