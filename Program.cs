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

var builder = WebApplication.CreateBuilder(args);

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

// Add services to the container.
builder.Services.AddDbContext<MercurialDBContext> (o =>
    o.UseNpgsql(builder.Configuration.GetConnectionString("DBConnection"))
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

// Adding validations

builder.Services.AddScoped<IValidator<CreateUserDTO>, UserValidations>();
builder.Services.AddScoped<IValidator<CreateAssignmentDTO>, AssignmentValidations>();
builder.Services.AddScoped<IValidator<UpdateAssignmentDTO>, UpdateAssignmentValidations>();
builder.Services.AddScoped<IValidator<UpdateSubjectDTO>, SubjectUpdateValidations>();
builder.Services.AddScoped<IValidator<UpdateTopicDTO>, TopicUpdateValidations>();
builder.Services.AddScoped<IValidator<CreateSubjectDTO>, SubjectValidations>();
builder.Services.AddScoped<IValidator<CreateTopicDTO>, TopicValidations>();
builder.Services.AddScoped<IValidator<UpdateUserDTO>, UserUpdateValidations>();

builder.Services.AddControllers();

builder.Services.AddControllers(options =>{
    options.Filters.Add<GlobalExceptionFilter>();
});


// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

DotNetEnv.Env.Load();

app.Run();
