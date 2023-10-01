using TestTask.API.Extensions;

var builder = WebApplication.CreateBuilder(args);

LogsConfiguration.Configuration(builder.Configuration, builder.Logging);
DbConfiguration.Configuration(builder.Configuration, builder.Services);
ServicesConfiguration.Configuration(builder.Services);
SwaggerConfiguration.Configuration(builder.Services);

var app = builder.Build();

app.RunDbContextMigrations();

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