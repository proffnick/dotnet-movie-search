using Microsoft.EntityFrameworkCore;
using Backend.Data;
using Backend.Services;

var builder = WebApplication.CreateBuilder(args);

/*
    Register required settings containing omdb base url and apikey add controllers
*/

builder.Services.Configure<OmdbApiSettings>(builder.Configuration.GetSection("OmdbApiSettings"));
builder.Services.AddControllers();

/*
    Add Database Context
    connection details are found in appsettings.json
    In larger applications, this could also be abstracted
*/

builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddHttpClient<IOmdbService, OmdbService>();

/*
    Implement Cors policies to allow or disallow API usage
    For larger projects, this can be extended to include other security features
    such as blocking specific ips etc instead of allowing any origin
*/

builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
    });
});

/*
    In larger applications where external or extensive documentation is needed
    Swagger can come in handdy
*/

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

/*
    Implements redirects, use of cors, 
    authorization middleware 
    (in larget application could require authentications totens etc)
    Map controllers and 
    Run the application on call
*/

app.UseHttpsRedirection();
app.UseCors("CorsPolicy");
app.UseAuthorization();
app.MapControllers();
app.Run();