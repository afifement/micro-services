

using Weasel.Core;

var builder = WebApplication.CreateBuilder(args);
var assembly = typeof(Program).Assembly;
// Add services to the container

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssemblies(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehavior<,>));

});
builder.Services.AddValidatorsFromAssembly(assembly);
builder.Services.AddCarter();
builder.Services
    .AddMarten(opts =>
    {
        opts.Connection(builder.Configuration.GetConnectionString("Database")!);
        opts.AutoCreateSchemaObjects = AutoCreate.CreateOrUpdate;
        opts.Schema.For<ShoppingCart>().Identity(x => x.UserName);
    })
    .UseLightweightSessions();


builder.Services.AddExceptionHandler<CustomExceptionHandler>();

builder.Services.AddHealthChecks()
                .AddNpgSql(builder.Configuration.GetConnectionString("Database")!);
var app = builder.Build();
 
//configure the Http request pipeline
app.MapCarter();
app.UseExceptionHandler(options => { });
app.UseHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
