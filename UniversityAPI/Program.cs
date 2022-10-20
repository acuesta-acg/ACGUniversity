using Acg.University.BL.Contratos;
using Acg.University.BL.Servicios;
using Acg.University.DAL.SqlServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.OpenApi.Models;
using Serilog;
using Acg.University.DAL.SqlServer.Migrations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Info Base de datos

builder.Services.AddDbContext<UniversityDbContext>(opc =>
{
    opc.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=ACGUniversity;Integrated Security=True");
});

//  -------   Configuración del CORS    -----------------

builder.Services.AddCors(opc =>
{
    opc.AddDefaultPolicy(p =>
    {
        p.WithOrigins("https://localhost:7119")
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

//  -----------------------------------------------------

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Host.UseSerilog(((ctx, lc) => lc.ReadFrom.Configuration(ctx.Configuration)));

/*
 * Verbose
 * Debug
 * Information
 * Warning
 * Error
 * Fatal
 */

builder.Services.AddLocalization(opc => opc.ResourcesPath = "Resources");

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen(opciones =>
{
    opciones.SwaggerDoc("v1", new OpenApiInfo { Title = "ACG Univeristy", Version = "v1" });
    opciones.SwaggerDoc("v2", new OpenApiInfo { Title = "ACG Univeristy", Version = "v2" });

    // ---------   Configuracíon para utilizar la seguridad JWT con Swagger    ------------------

    opciones.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Introducir un token JWT valido",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });

    opciones.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });

    // ------------------------------------------------------------------------------
});

// ---------  Nuestros Servicios --------
builder.Services.AddScoped<IServPersonas, ServPersonas>();
builder.Services.AddScoped<IServUsuarios, ServUsuarios>();

// --------    Middleware para el versionado  ------------------

builder.Services.AddApiVersioning(opciones =>
{
    opciones.ReportApiVersions = true;
    opciones.DefaultApiVersion = new ApiVersion(1, 0);
    opciones.AssumeDefaultVersionWhenUnspecified = true;
    opciones.ApiVersionReader = ApiVersionReader.Combine(
        new QueryStringApiVersionReader("version"),
        new HeaderApiVersionReader("x-api-ver"),
        new MediaTypeApiVersionReader("ver")
        );
});

builder.Services.AddVersionedApiExplorer(opciones =>
{
    opciones.GroupNameFormat = "'v'VVV";
    opciones.SubstituteApiVersionInUrl = true;
});

// --------------------------------------------------------------

// ----------- Middleware para la autenticación JWT  --------------

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opc => {
    opc.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["jwt:Issuer"],
        ValidAudience = builder.Configuration["jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jwt:Key"]))
    };
});

// ---------------------------------------------------------------

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    //app.UseSwaggerUI();

    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "ACG University API v1");
        c.SwaggerEndpoint("/swagger/v2/swagger.json", "ACG University API v2");
    });
}


// --------------------     configuración para la internacionalización    -----------------

var culturas = new[] { "es-ES", "es", "en-US", "en" };
var localizationOptions =
    new RequestLocalizationOptions().SetDefaultCulture(culturas[0])
    .AddSupportedCultures(culturas)
    .AddSupportedUICultures(culturas);
//localizationOptions.ApplyCurrentCultureToResponseHeaders = true;

app.UseRequestLocalization(localizationOptions);

// ----------------------------------------------------------------------------------------

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseSerilogRequestLogging();

app.Run();
