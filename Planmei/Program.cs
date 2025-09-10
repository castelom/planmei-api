using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Planmei.Domain.Interfaces.Configs;
using Planmei.Domain.Interfaces.Data;
using Planmei.Domain.Interfaces.Repository;
using Planmei.Domain.Interfaces.Services;
using Planmei.Infrastructure.Repository;
using Planmei.Web.Configs;
using Planmei.Web.Middlewares;
using Planmei.Web.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
var jwtKey = builder.Configuration["Jwt:Key"];
var jwtIssuer = builder.Configuration["Jwt:Issuer"];

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "Planmei API", Version = "v1" });

    // Config JWT
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "'Bearer {token}'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy => policy.WithOrigins("http://localhost:5173")
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});

builder.Services.AddHttpContextAccessor();
builder.Services.Configure<CaptchaConfig>(
    builder.Configuration.GetSection("Captcha")
);

builder.Services.Configure<MailboxConfig>(
    builder.Configuration.GetSection("Mailbox")
);

// DI
builder.Services.AddScoped<ICurrentUserService, CurrentUserService>();
builder.Services.AddScoped<IFinancialTransactionService, FinancialTransactionService>();
builder.Services.AddScoped<IFinancialTransactionRepository, FinancialTransactionRepository>();
builder.Services.AddScoped<IEmailService, EmailService>();

builder.Services.AddSingleton<ICaptchaConfig>(sp =>
    sp.GetRequiredService<IOptions<CaptchaConfig>>().Value
);
builder.Services.AddSingleton<IMailboxConfig>(sp =>
    sp.GetRequiredService<IOptions<MailboxConfig>>().Value
);

builder.Services.AddHttpClient<ICaptchaService, CaptchaService>((sp, client) =>
{
    var captchaConfig = sp.GetRequiredService<ICaptchaConfig>();
    client.BaseAddress = new Uri(captchaConfig.BaseUrl);
});

// Config Identity
builder.Services.AddDbContext<IApplicationDbContext, ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login";
    options.AccessDeniedPath = "/Account/AccessDenied";
});


// Config JWT Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtIssuer,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
    };
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("VerifiedUser", policy =>
        policy.RequireClaim("verified_user", "true"));
});


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
app.UseMiddleware<UserContextMiddleware>();
app.UseCors("AllowFrontend");
app.MapControllers();

app.Run();
