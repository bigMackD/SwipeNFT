using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using SwipeNFT.Contracts.Request.Command.Authentication;
using SwipeNFT.Contracts.Request.Query.Users;
using SwipeNFT.Contracts.Response.Authentication;
using SwipeNFT.Contracts.Response.Users;
using SwipeNFT.DAL.Context;
using SwipeNFT.DAL.Models.Authentication;
using SwipeNFT.Infrastructure.CommandHandlers.Authentication;
using SwipeNFT.Infrastructure.QueryHandlers.Users;
using SwipeNFT.Shared.Infrastructure.CommandHandler;
using SwipeNFT.Shared.Infrastructure.QueryHandler;

namespace SwipeNFT.API.Extensions
{
    public static class RegisterStartupServicesExtension 
    {
        public static WebApplicationBuilder RegisterSwaggerServices(this WebApplicationBuilder builder)
        {

            builder.Services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "SwipeNFT",
                    Description = "SwipeNFT API",
                    Contact = new OpenApiContact
                    {
                        Name = "Maciej Drozdowicz",
                        Email = "maciek.d@me.com",
                    }
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. <BR/>
                      Enter 'Bearer' [space] and then your token in the text input below.  <BR/>
                      Example: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);

                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });

            return builder;
        }

        public static WebApplicationBuilder RegisterJWTServices(this WebApplicationBuilder builder)
        {
            //JWT Authentication
            var key = Encoding.UTF8.GetBytes(builder.Configuration.GetValue<string>("AppSettings:JWTSecret"));

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = false;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });

            builder.Services.Configure<IdentityOptions>(options =>
                options.Password.RequiredLength = 8);

            return builder;
        }
        public static WebApplicationBuilder RegisterIoC(this WebApplicationBuilder builder)
        {
            builder.Services
                .AddScoped<IAsyncCommandHandler<RegisterUserCommand, RegisterUserResponse>,
                    RegisterUserCommandHandler>()
                .AddScoped<IAsyncCommandHandler<LoginUserCommand, LoginUserResponse>,
                    LoginUserCommandHandler>()
                .AddScoped<IAsyncCommandHandler<DisableUserCommand, DisableUserResponse>,
                    DisableUserCommandHandler>()
                .AddScoped<IAsyncCommandHandler<EnableUserCommand, EnableUserResponse>,
                    EnableUserCommandHandler>()
                .AddScoped<IAsyncQueryHandler<GetUsersQuery, GetUsersResponse>,
                    GetUsersQueryHandler>()
                .AddScoped<IAsyncQueryHandler<GetUserProfileQuery, GetUserProfileResponse>,
                    GetUserProfileQueryHandler>()
                .AddScoped<IAsyncQueryHandler<GetUserDetailsQuery, UserDetailsResponse>,
                    UserDetailsQueryHandler>();

            return builder;
        }

        public static WebApplicationBuilder RegisterAuthenticationContextServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<AuthenticationContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("IdentityConnection")));
            builder.Services.AddDefaultIdentity<AppUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AuthenticationContext>();

            return builder;
        }

        public static WebApplicationBuilder RegisterSerilogLoggingServices(this WebApplicationBuilder builder)
        {
            var assemblyName = typeof(Program).Assembly.GetName().Name;
            builder.Host.UseSerilog((ctx, lc) => lc
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .Enrich.FromLogContext()
                .Enrich.WithMachineName()
                .Enrich.WithProperty("Assembly", assemblyName)
                .WriteTo.File("C:/logs/logs.txt")
                .WriteTo.Seq(serverUrl: builder.Configuration.GetValue<string>("AppSettings:SeqUrl"))
                .WriteTo.Console()
            );

            return builder;
        }
    }
}
