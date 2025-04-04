using FluentValidation;
using MercurialBackendDotnet.DB;
using MercurialBackendDotnet.Dto.InputDTO;
using MercurialBackendDotnet.Dto.OutputDTO;
using MercurialBackendDotnet.Exceptions.ExceptionsFilters;
using MercurialBackendDotnet.Services.Implementations;
using MercurialBackendDotnet.Services.Interfaces;
using MercurialBackendDotnet.Validations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContextPool<MercurialDBContext> (o =>
    o.UseNpgsql(builder.Configuration.GetConnectionString("DBConnection"))
);
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

app.UseAuthorization();

app.MapControllers();

app.Run();
