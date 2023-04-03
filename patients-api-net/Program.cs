using patients_api_net.Repository.Patients;
using patients_api_net.Repository.Patients.Interface;
using patients_api_net.Services.Patients;
using patients_api_net.Services.Patients.Interface;

var builder = WebApplication.CreateBuilder(args);
var myAllowSpecificOrigins = "_myAllowSpecificOrigins";

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPatientsService, PatientsService>();
builder.Services.AddScoped<IPatientsRepo, PatientsRepo>();
builder.Services.AddCors(options =>
{
    options.AddPolicy(myAllowSpecificOrigins, policy =>
    {
        policy.WithOrigins().AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{

}

app.UseSwagger();

app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
