using FBQ.Salud_AccessData.Commands;
using FBQ.Salud_AccessData.Queries;
using FBQ.Salud_Application.Services;
using FBQ.Salud_Application.Validation;
using FBQ.Salud_Domain.Commands;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
        .AddControllers()
        .AddFluentValidation(c =>
        c.RegisterValidatorsFromAssembly(Assembly.GetExecutingAssembly()));

//Fluent Validation
builder.Services.AddValidatorsFromAssemblyContaining<PacienteValidation>();

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
builder.Services.AddScoped<ITurnosRepository, TurnosRepository>();
builder.Services.AddScoped<ITurnoServices, TurnoServices>();
builder.Services.AddScoped<IPacienteRepository, PacienteRepository>();
builder.Services.AddScoped<IPacienteService, PacienteServices>();
builder.Services.AddScoped<IDiagnosticoRepository, DiagnosticoRepository>();
builder.Services.AddScoped<IDiagnosticoServices, DiagnosticoServices>();
builder.Services.AddScoped<IHistoriaClinicaRepository, HistoriaClinicaRepository>();
builder.Services.AddScoped<IHistoriaClinicaServices, HistoriaClinicaServices>();
builder.Services.AddScoped<IPacienteValidationExist, PacienteValidationExist>();


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

