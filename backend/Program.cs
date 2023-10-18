using backend.Configurations;
using backend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<DropboxSignConfig>();
//TODO: Implement DI apikey through appsettings
//builder.Services.Configure<DropboxSignConfig>(builder.Configuration.GetSection("DropboxSignConfig"));
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddScoped<IDropboxSignService, DropboxSignService>();
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
