using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi;
using RedeSocial.Application.Contracts;
using RedeSocial.Application.Contracts.Documents.Response;
using RedeSocial.Application.Implementations;
using RedeSocial.Application.Validations;
using RedeSocial.Domain.Contracts;
using RedeSocial.Domain.Contracts.Repositories;
using RedeSocial.Domain.Validations;
using RedeSocial.Infrastructure.Data;
using RedeSocial.Infrastructure.Repositories;
using RedeSocial.WebApi.Security;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("bearer", new OpenApiSecurityScheme
    {
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "JWT Authorization header using the Bearer scheme."
    });
    options.AddSecurityRequirement(document => new OpenApiSecurityRequirement
    {
        [new OpenApiSecuritySchemeReference("bearer", document)] = []
    });
});

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<TokenService, TokenService>();
builder.Services.AddScoped<IFriendService, FriendService>();
builder.Services.AddScoped<ICommentService, CommentService>();
builder.Services.AddScoped<ILikeService, LikeService>();
builder.Services.AddScoped<IPostService, PostService>();

builder.Services.AddScoped<IUserValidation, UserValidation>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IFriendRepository, FriendRepository>();
builder.Services.AddScoped<ILikeRepository, LikeRepository>();
builder.Services.AddScoped<IPostRepository, PostRepository>();
builder.Services.AddScoped<ICommentRepository, CommentRepository>();
builder.Services.AddHttpClient<IEnderecoRepository, EnderecoRepository>(client =>
{
    client.BaseAddress = new Uri("https://viacep.com.br");
    client.DefaultRequestHeaders.Add("Accept", "application/json");
    client.DefaultRequestHeaders.Add("Connection", "Keep-alive");
});

builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        b => b.MigrationsAssembly("RedeSocial.Infrastructure"))
);

builder.Services.AddAuthentication(opitions =>
{
    opitions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opitions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    opitions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(opitions =>
{
    opitions.TokenValidationParameters = new TokenValidationParameters
    {
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(TokenSettings.SecretKey)),
        ValidateIssuerSigningKey = true,
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

builder.Services.Configure<ApiBehaviorOptions>(o =>
{
    o.InvalidModelStateResponseFactory = actionContext =>
    {
        var notifications = actionContext.ModelState.Values
            .SelectMany(e => e.Errors.Select(erro => new Notification(erro.ErrorMessage)))
            .ToList();

        var response = new ErroResponse(notifications);
        
        return new ConflictObjectResult(response.Notifications);
    };
});

builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();


app.Run();


