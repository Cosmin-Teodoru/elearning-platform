using System.Text;
using Elearn.Application.Logic;
using Elearn.Application.LogicInterfaces;
using Elearn.Application.ServiceContracts;
using Elearn.GrpcService.Client;
using Elearn.Shared.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.IdentityModel.Tokens;
using Shared.Auth;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
AppContext.SetSwitch("System.Net.Http.SocketsHttpHandler.Http2UnencryptedSupport", true);
builder.Services.AddGrpc();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<ILectureService, LectureGrpcClient>();
builder.Services.AddScoped<ICommentService, CommentGrpcClient>();
builder.Services.AddScoped<IUserService, UserGrpcClient>();
builder.Services.AddScoped<ISearchService, SearchGrpcClient>();
builder.Services.AddScoped<IQuestionService, QuestionGrpcClient>();
builder.Services.AddScoped<ITeacherService, TeacherGrpcClient>();
builder.Services.AddScoped<IModeratorService, ModeratorGrpcClient>();
builder.Services.AddScoped<ILectureVoteService, LectureVoteGrpcClient>();
builder.Services.AddScoped<IUniversityService, UniversityGrpcClient>();
builder.Services.AddScoped<IAnswerService, AnswerGrpcClient>();
builder.Services.AddScoped<IStudentService, StudentGrpcClient>();
builder.Services.AddScoped<ICountryService, CountryGrpcClient>();
builder.Services.AddScoped<ICommentLogic, CommentLogic>();
builder.Services.AddScoped<ILectureLogic, LectureLogic>();
builder.Services.AddScoped<IUserLogic, UserLogic>();
builder.Services.AddScoped<ISearchLogic, SearchLogic>();
builder.Services.AddScoped<IAuthLogic, AuthLogic>();
builder.Services.AddScoped<IAnswerLogic,AnswerLogic>();
builder.Services.AddScoped<ILectureVoteLogic, LectureVoteLogic>();
builder.Services.AddScoped<IQuestionLogic,QuestionLogic>();
builder.Services.AddScoped<IUniversityLogic, UniversityLogic>();
builder.Services.AddScoped<ICountryLogic, CountryLogic>();
builder.Services.AddGrpcClient<AnswerGrpcClient>();
builder.Services.AddGrpcClient<SearchGrpcClient>();
builder.Services.AddGrpcClient<CommentGrpcClient>();
builder.Services.AddGrpcClient<LectureGrpcClient>();
builder.Services.AddGrpcClient<UserGrpcClient>();
builder.Services.AddGrpcClient<QuestionGrpcClient>();
builder.Services.AddGrpcClient<TeacherGrpcClient>();
builder.Services.AddGrpcClient<LectureVoteGrpcClient>();
builder.Services.AddGrpcClient<UniversityGrpcClient>();
builder.Services.AddGrpcClient<ModeratorGrpcClient>();
builder.Services.AddGrpcClient<StudentGrpcClient>();
builder.Services.AddGrpcClient<StudentGrpcClient>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});
AuthorizationPolicies.AddPolicies(builder.Services);


var app = builder.Build();
app.UseCors(x => x
    .AllowAnyMethod()
    .AllowAnyHeader()
    .SetIsOriginAllowed(origin => true) // allow any origin
    .AllowCredentials());
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();

app.UseRouting();

app.UseAuthorization();

app.UseGrpcWeb(new GrpcWebOptions { DefaultEnabled = true });
app.UseEndpoints(endpoints =>
{
    endpoints.MapGrpcService<AnswerGrpcClient>().EnableGrpcWeb();
    endpoints.MapGrpcService<CommentGrpcClient>().EnableGrpcWeb();
    endpoints.MapGrpcService<LectureGrpcClient>().EnableGrpcWeb();
    endpoints.MapGrpcService<UserGrpcClient>().EnableGrpcWeb();
    endpoints.MapGrpcService<QuestionGrpcClient>().EnableGrpcWeb();
    endpoints.MapGrpcService<TeacherGrpcClient>().EnableGrpcWeb();
    endpoints.MapGrpcService<SearchGrpcClient>().EnableGrpcWeb();
    endpoints.MapGrpcService<ModeratorGrpcClient>().EnableGrpcWeb();
});
app.UseHttpsRedirection();

app.MapControllers();

app.Run();