using MedicinApi.Data;
using MedicinApi.Helpers;
using MedicinApi.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;
using System.Threading.Tasks;

namespace MedicinApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            ConnectionString = Configuration.GetConnectionString("SqlServer");
        }

        public static string ConnectionString;

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors();

            var appSettingsSection = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettingsSection);

            //var appSettings = appSettingsSection.Get<AppSettings>();
            var key = Encoding.ASCII.GetBytes("d3e84769b4fce34f69a850e42b25282c");  //appSettings.Secret
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.Events = new JwtBearerEvents
                {
                    OnTokenValidated = context =>
                    {
                        var userRepository = context.HttpContext.RequestServices.GetRequiredService<IUserRepository>();
                        var userId = context.Principal.Identity.Name;
                        var user = userRepository.GetById(userId);
                        if (user == null)
                        {
                            // return unauthorized if user no longer exists
                            context.Fail("Unauthorized");
                        }
                        return Task.CompletedTask;
                    }
                };
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            services.AddTransient<IMedicoRepository, MedicoRepository>();
            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IEspecialidadeRepository, EspecialidadeRepository>();
            services.AddTransient<IEspecialidadeMedicoRepository, EspecialidadeMedicoRepository>();
            services.AddControllers();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "MedicinApi",
                    Description = "A Service to search doctors",
                    TermsOfService = new Uri("https://github.com/reismmatheus"),
                    Contact = new OpenApiContact
                    {
                        Name = "Matheus Reis",
                        Email = "reis.mmatheus@gmail.com",
                        Url = new Uri("https://github.com/reismmatheus"),
                    }
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer {Token}')",
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
                        Array.Empty<string>()
                    }
                });
            });

            ConnectionString = "Data Source=DESKTOP-M9CNHCN\\SQLEXPRESS;Initial Catalog=medicin-api;Integrated Security=True";

            services.AddDbContext<DatabaseContext>(options =>
                options.UseSqlServer("Data Source=DESKTOP-M9CNHCN\\SQLEXPRESS;Initial Catalog=medicin-api;Integrated Security=True")); //ConnectionString
            services.AddMemoryCache();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseSwagger(c =>
            {
                c.SerializeAsV2 = true;
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "MedicinApi");
                c.RoutePrefix = string.Empty;
            });

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
