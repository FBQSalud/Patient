using FBQ.Salud_AccessData.Commands;
using FBQ.Salud_AccessData.Queries;
using FBQ.Salud_Application.Services;
using FBQ.Salud_Application.Validation;
using FBQ.Salud_Domain.Commands;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddControllers().AddJsonOptions(x =>
                                x.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles);
builder.Services.AddDbContext<FbqSaludDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddEndpointsApiExplorer();
// Set the comments path for the Swagger JSON and UI.
var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

builder.Services.AddSwaggerGen(c => c.IncludeXmlComments(xmlPath));

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
//Repository
builder.Services.AddTransient<ITurnosRepository, TurnosRepository>();
builder.Services.AddTransient<ITurnoServices, TurnoServices>();
builder.Services.AddTransient<IPacienteRepository, PacienteRepository>();
builder.Services.AddTransient<IPacienteService, PacienteServices>();
builder.Services.AddTransient<IHistoriaClinicaRepository, HistoriaClinicaRepository>();
builder.Services.AddTransient<IHistoriaClinicaServices, HistoriaClinicaServices>();
builder.Services.AddTransient<IPacienteValidationExist, PacienteValidationExist>();


//Cors
builder.Services.AddCors(c =>
{
    c.AddPolicy("AllowOrigin", options => options
                                                .AllowAnyOrigin()
                                                .AllowAnyMethod()
                                                .AllowAnyHeader());
});

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

