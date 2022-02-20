using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Text;
using SwipeNFT.API.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SwipeNFT.API.Middleware;
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

namespace SwipeNFT.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ConnectionService.Set(configuration);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services
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

            services.AddControllers();
            services.AddDbContext<AuthenticationContext>(options =>
                options.UseSqlServer(ConnectionService.connectionString));
            services.AddDefaultIdentity<AppUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<AuthenticationContext>();

            services.Configure<IdentityOptions>(options =>
                options.Password.RequiredLength = 8
            );


            //JWT Authentication

            var key = Encoding.UTF8.GetBytes(Configuration.GetValue<string>("AppSettings:JWTSecret"));

            services.AddAuthentication(x =>
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

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
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
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseMiddleware<CustomExceptionHandlingMiddleware>();
            if (env.IsDevelopment())
            {
                app.UseSwagger();

                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "SwipeNFT API v.1");
                    c.RoutePrefix = string.Empty;
                });
            }

            app.UseCors(builder =>
                builder.WithOrigins(Configuration.GetValue<string>("AppSettings:ClientUrl"))
                    .AllowAnyHeader()
                    .AllowAnyMethod());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
