using Microsoft.EntityFrameworkCore;
using BloodsyncAPI.Data;
using System.Text.Json;
using System.Text.Json.Serialization;
using BloodsyncAPI.Repositories;
using BloodsyncAPI.Helpers;
using BloodsyncAPI.Middleware;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<BloodsyncAPIContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BloodsyncAPIContext") ?? throw new InvalidOperationException("Connection string 'BloodsyncAPIContext' not found."), sqlOption => sqlOption.UseNetTopologySuite()));

builder.Services.AddCors(options =>
{
    options.AddPolicy("CROSPolicy",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyHeader()
                   .AllowAnyMethod();
        });
});

builder.Services.AddMvc().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault;
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter(JsonNamingPolicy.CamelCase));
    options.JsonSerializerOptions.Converters.Add(new NetTopologySuite.IO.Converters.GeoJsonConverterFactory());
});

builder.Services.AddControllers();
builder.Services.AddScoped<IGenericRepos, GenericRepos>();
builder.Services.AddScoped<ISpecificRepos, SpecificRepos>();
builder.Services.AddAutoMapper(typeof(Program).Assembly);

var app = builder.Build();

app.UseCors("CROSPolicy");

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseMiddleware<ApiKeyAuth>();
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
