using AutoMapper;
using $safeprojectname$.Middlewares;
using $safeprojectname$.Models;
using $ext_safeprojectname$.Application.Application.Services;
using $ext_safeprojectname$.Application.Infrastructure;
using $ext_safeprojectname$.Application.Logging;
using $ext_safeprojectname$.Data;
using $ext_safeprojectname$.Infrastructure.Auth;
using $ext_safeprojectname$.Infrastructure.Persistence;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using $ext_safeprojectname$.Infrastructure.Caching;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;

namespace $safeprojectname$
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder
                            .AllowAnyOrigin()
                            .AllowAnyHeader()
                            .AllowAnyMethod();
                    });
            });

            services.AddEntityFrameworkSqlServer().AddDbContext<EAI_GatewayContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            //Adding Swagger Example 
            services.AddSingleton<BadRequestGetExample>();
            services.AddSingleton<InternalErrorGetExample>();
            services.AddSingleton<PermissionGetExample>();


            services.AddScoped<IApplicationService, ApplicationService>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            services.AddScoped<DbContext, EAI_GatewayContext>();
            services.AddScoped<ITokenBuilder, TokenBuilder>();
            services.AddScoped<ILoggerBuilder, LoggerBuilder>();
            services.AddScoped<ICachingHandler, CachingHandler>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            //******Basic Authentication
            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);


            //******* JWT Authentication
            /* services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, (o) =>
             {
                 o.TokenValidationParameters = new TokenValidationParameters()
                 {
                     IssuerSigningKey = TokenAuthOption.Key,
                     ValidAudience = TokenAuthOption.Audience,
                     ValidIssuer = TokenAuthOption.Issuer,
                     ValidateIssuerSigningKey = true,
                     ValidateLifetime = true,
                     ValidateIssuer = true,
                     ValidateAudience = true,
                     ClockSkew = TimeSpan.FromMinutes(0)
                 };
             });*/

            //*******Auto Mapper
            services.AddAutoMapper(typeof(Startup));

            //*******Swagger Config
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "You api title", Version = "v1", Description = "write your description" });

                //basic Auth
                c.AddSecurityDefinition("basic", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "basic",
                    In = ParameterLocation.Header,
                    Description = "Basic Authorization header using the Bearer scheme."
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                          new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "basic"
                                }
                            },
                            new string[] {}
                    }
                });

                //baarer Auth
                /*
                                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                                {
                                    Description =
                                    "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
                                    Name = "Authorization",
                                    In = ParameterLocation.Header,
                                    Type = SecuritySchemeType.ApiKey,
                                    Scheme = "Bearer"
                                });

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
                                });*/


                // adds any string you like to the request headers - in this case, a correlation id
                c.OperationFilter<AddHeaderOperationFilter>("correlationId", "Correlation Id for the request", false);

                // please add xml documntion file form proprites>bulid
                /*var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);*/

                // Adds "(Auth)" to the summary so that you can see which endpoints have Authorization                                                                              
                // or use the generic method, e.g. c.OperationFilter<AppendAuthorizeToSummaryOperationFilter<MyCustomAttribute>>();
                c.OperationFilter<AppendAuthorizeToSummaryOperationFilter>();    
            });

            //services.AddDistributedMemoryCache();   
            // use this for memory cache
            services.AddStackExchangeRedisCache(option =>
            {
                option.Configuration = Configuration.GetConnectionString("RedisConnection");
            });
            // Caching Response
            services.AddResponseCaching();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = ctx => new CustomValidationResult();
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.MapWhen(context => context.Request.Path.Value.ToLower().Contains("swagger"),
                appBuilder =>
                {
                    if (env.IsDevelopment())
                    {
                        appBuilder.UseStaticFiles();
                        appBuilder.UseSwagger();
                        appBuilder.UseSwaggerUI(c => { c.SwaggerEndpoint("../swagger/v1/swagger.json", "My Application v1"); });
                    }
                });

            app.UseMiddleware<LoggingMiddleware>();
            app.UseMiddleware<ExceptionMiddleware>();
            //app.UseAuthentication();
            app.UseResponseCaching();
            app.UseStaticFiles();
           /* app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
            });*/
        }
    }
}