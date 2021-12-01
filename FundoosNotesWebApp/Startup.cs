// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Startup.cs" company="Bridgelabz">
//   Copyright © 2021 Company="BridgeLabz"
// </copyright>
// <creator name="Diptimayee"/>
// --------------------------------------------------------------------------------------------------------------

namespace FundoosNotesWebApp
{
    using System;
    using System.Text;
    using FundooManager.Interface;
    using FundooManager.Manager;
    using FundooRepository.Context;
    using FundooRepository.Interface;
    using FundooRepository.Repository;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.IdentityModel.Tokens;
    using Microsoft.OpenApi.Models;

    /// <summary>
    /// Startup class
    /// </summary>
    public class Startup
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Startup"/> class
        /// </summary>
        /// <param name="configuration"> configuration parameter</param>
        public Startup(IConfiguration configuration)
        {
            this.Configuration = configuration;
        }

        /// <summary>
        /// Gets the configuration
        /// </summary>
        public IConfiguration Configuration { get; }

        //// This method gets called by the runtime. Use this method to add services to the container.

        /// <summary>
        /// ConfigureServices -all services in application
        /// </summary>
        /// <param name="services">services as parameter for Interface ServiceCollection</param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc(); ////adds all mvc services
            ////AddDbContextPool-enable dbpool , saves context as service and can be reused rather creating new instances
            ////UseSqlServer-connect to sql database server
            services.AddDbContextPool<UserContext>(option => option.UseSqlServer(this.Configuration.GetConnectionString("FundooDB")));
            ////Adding Dependency injections
            services.AddTransient<IUserRepository, UserRepository>(); 
            services.AddTransient<IUserManager, UserManager>();
            services.AddTransient<INoteManager, NoteManager>();
            services.AddTransient<INoteRepository, NoteRepository>();
            services.AddTransient<ICollaboratorRepository, CollaboratorRepository>();
            services.AddTransient<ICollaboratorManager, CollaboratorManager>();
            services.AddTransient<ILabelRepository, LabelRepository>();
            services.AddTransient<ILabelManager, LabelManager>();

            ////Adding sessions
            services.AddSession(
                options =>
                {
                    options.IdleTimeout = TimeSpan.FromSeconds(10);
                });
            ////Adding cors
            services.AddCors(options =>
                 options.AddPolicy(
                     "AllowAllHeaders",
                 builder =>
                 {
                     builder.AllowAnyOrigin()
                         .AllowAnyMethod()
                         .AllowAnyHeader();
                 }));
            ////Adding swagger services
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1.0", new OpenApiInfo { Title = "FunDooNotes", Description = "Keep Notes", Version = "1.0" });

                c.AddSecurityDefinition(
                    "Bearer", 
                    new OpenApiSecurityScheme()
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme. \r\n\r\n Enter 'Bearer' [space] and then your token in the text input below.\r\n\r\nExample: \"Bearer 12345abcdef\"",
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
                            new string[]
                            {
                            }
                    }
                });
            });
            services.AddAuthentication(option =>
            {
                option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = false,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["SecretKey"]))  
                };
            });
        }

        //// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.

        /// <summary>
        /// method for Configure
        /// </summary>
        /// <param name="app">app as parameter</param>
        /// <param name="env">host for web application passed as env</param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            ////check if current host is belong to microsoft host or not
            if (env.IsDevelopment()) 
            {
                app.UseDeveloperExceptionPage(); ////helps developer in tracing errors that occure during the developmnt phase
            }
            else
            {
                app.UseExceptionHandler("/Error"); ////middleware to catch exception
                //// The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts(); ////header strict transfer security,
            }
            ////All are middleswares
            app.UseCors("AllowAllHeaders");
            app.UseHttpsRedirection(); ////redirect the http to https
            app.UseStaticFiles(); ////serve staticfiles(html,css,image) http req without any server side processing

            app.UseRouting(); ////match request to endpoint

            app.UseAuthorization();
            app.UseAuthorization();

            ////it execute the matched endpoint
            app.UseEndpoints(endpoints =>
            {
                ////MapcontrollerRoute(),map attributes present in controller
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1.0/swagger.json", "FunDooNote (V 1.0)");
            });
        }
    }
}
