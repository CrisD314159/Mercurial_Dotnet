using System.Text;
using FluentValidation;
using MercurialBackendDotnet.DB;
using MercurialBackendDotnet.Dto.InputDTO;
using MercurialBackendDotnet.Dto.OutputDTO;
using MercurialBackendDotnet.Exceptions.ExceptionsFilters;
using MercurialBackendDotnet.Model;
using MercurialBackendDotnet.Services.Implementations;
using MercurialBackendDotnet.Services.Interfaces;
using MercurialBackendDotnet.Validations;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Hangfire;
using Hangfire.PostgreSql;

var builder = WebApplication.CreateBuilder(args);

var MercurialFrontend = "mercurialFrontend";
builder.Services.AddCors(opt =>
    opt.AddPolicy(name: MercurialFrontend,
        policy =>
        {
            policy.WithOrigins("https://localhost:3000","http://localhost:3000", "https://mercurial-app.vercel.app" )
            .AllowAnyMethod()
            .AllowAnyHeader()
            .AllowCredentials();
        }
    )
);

// Para tareas en segundo plano, en este caso, las de las notificaciones push
// en la app de nextjs
builder.Services.AddHangfire(config =>
    config.UsePostgreSqlStorage(c =>
        c.UseNpgsqlConnection(builder.Configuration["ConnectionStrings:DBConnection"])
    )
);

builder.Services.AddHangfireServer();

// Esto permite hacer peticiones http
// En este caso, para hacer peticiones a firebase para las notificaciones push
builder.Services.AddHttpClient();

// Para añadir autenticación mediante JWT usando Identity
builder.Services.AddAuthentication( options=>
{
    options.DefaultAuthenticateScheme =  JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme =  JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
   var config = builder.Configuration;
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = config["Jwt:Issuer"],
        ValidAudience = config["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(config["Jwt:Key"] 
        ?? throw new InvalidOperationException("Jwt:Key is not configured")))
    };
});

// Añade autorización 
builder.Services.AddAuthorization();

// Add services to the container.
// Para añadir la base de datos postgres al contexto de la aplicación
builder.Services.AddDbContext<MercurialDBContext> (o =>
    o.UseNpgsql(builder.Configuration["ConnectionStrings:DBConnection"])
);

/// Used to add the identity framework core
builder.Services.AddIdentity<User, IdentityRole<string>>(options =>
{
    options.Password.RequireDigit = true;
    options.Password.RequiredLength = 6;
    options.Password.RequireUppercase = true;
})
.AddEntityFrameworkStores<MercurialDBContext>()
.AddDefaultTokenProviders();



//Services injection
builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<ICheckListService, CheckListService>();
builder.Services.AddScoped<ISubjectService, SubjectService>();
builder.Services.AddScoped<IAssignmentService, AssignmentService>();
builder.Services.AddScoped<ITopicService, TopicService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPushNotificacionService, PushNotificacionService>();

// Adding validations 
// Añade las validaciones de fluent validation
// Esto permite usar inyección de dependencias en vez de instanciar clases
builder.Services.AddScoped<IValidator<CreateUserDTO>, UserValidations>();
builder.Services.AddScoped<IValidator<CreateAssignmentDTO>, AssignmentValidations>();
builder.Services.AddScoped<IValidator<UpdateAssignmentDTO>, UpdateAssignmentValidations>();
builder.Services.AddScoped<IValidator<UpdateSubjectDTO>, SubjectUpdateValidations>();
builder.Services.AddScoped<IValidator<UpdateTopicDTO>, TopicUpdateValidations>();
builder.Services.AddScoped<IValidator<CreateSubjectDTO>, SubjectValidations>();
builder.Services.AddScoped<IValidator<CreateTopicDTO>, TopicValidations>();
builder.Services.AddScoped<IValidator<UpdateUserDTO>, UserUpdateValidations>();
builder.Services.AddScoped<IValidator<AddNodeDTO>, NodeValidations>();
builder.Services.AddScoped<IValidator<UpdateNodeDTO>, NodeUpdateValidations>();
builder.Services.AddScoped<IValidator<ChangePasswordDTO>, PasswordChangeValidations>();

builder.Services.AddControllers();

// Esto inyecta una clase que es un manejador de excepciones
// El manejador de exepciones recibe toda excepción de la app y la filtra 
// con el objetivo de devolver la respuesta adaptada a la excepción
builder.Services.AddControllers(options =>{
    options.Filters.Add<GlobalExceptionFilter>();
});


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseCors(MercurialFrontend);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

// Middlewares para los servicios anteriores

app.UseHangfireDashboard();

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

DotNetEnv.Env.Load();

app.Run();
