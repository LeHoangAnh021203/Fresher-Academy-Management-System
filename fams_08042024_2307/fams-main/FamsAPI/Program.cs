using DataLayer;
using DataLayer.Repositories;
using FamsAPI.IServices;
using FamsAPI.Services;
using Microsoft.EntityFrameworkCore;
using AutoMapper;
using FamsAPI.ViewModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace FamsAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            //AutoMapper
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            builder.Services.AddEndpointsApiExplorer();

            builder.Services.AddAuthorization();

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("SuperAdmin", policy =>
                {
                    policy.RequireAssertion(context =>
                    {
                        var user = context.User;
                        var roleClaim = user.FindFirst("Role");
                        if (roleClaim != null && roleClaim.Value == "1")
                        {
                            return true;
                        }
                        return false;
                    });
                });
                options.AddPolicy("AdminAccessPolicy", policy =>
                {
                    policy.RequireAssertion(context =>
                    {
                        var user = context.User;
                        var roleClaim = user.FindFirst("Role");
                        if (roleClaim != null && (roleClaim.Value == "2" || roleClaim.Value == "1"))
                        {
                            return true;
                        }
                        return false;
                    });
                });
                //Add more role if you want...
            });

            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Type = SecuritySchemeType.Http,
                    Scheme = "bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Enter the JWT token obtained from the login endpoint",
                    Name = "Authorization"
                });
                options.AddSecurityRequirement(new OpenApiSecurityRequirement
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
            builder.Services.AddAuthentication(item =>
            {
                item.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                item.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(item =>
            {
                item.RequireHttpsMetadata = true;
                item.SaveToken = true;
                item.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes("c2VydmVwZXJmZWN0bHljaGVlc2VxdWlja2NvYWNoY29sbGVjdHNsb3Bld2lzZWNhbWU=")),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero
                };
            });           

            builder.Services.AddDbContext<FAMSDBContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("Local"));
            });

            //Repository
            builder.Services.AddScoped(typeof(GenericRepository<>));
            builder.Services.AddScoped<ClassRepository>();
            builder.Services.AddScoped<ClassUserRepository>();
            builder.Services.AddScoped<LearningObjectiveRepository>();
            builder.Services.AddScoped<SyllabusObjectiveRepository>();
            builder.Services.AddScoped<SyllabusRepository>();
            builder.Services.AddScoped<TrainingContentRepository>();
            builder.Services.AddScoped<TrainingProgramRepository>();
            builder.Services.AddScoped<TrainingProgramSyllabusRepository>();
            builder.Services.AddScoped<TrainingUnitRepository>();
            builder.Services.AddScoped<UserPermissionRepository>();
            builder.Services.AddScoped<UserRepository>();
            builder.Services.AddScoped<AssessmentRepository>();
            builder.Services.AddScoped<RefreshTokenRepository>();
            builder.Services.AddScoped<TrainingCalendarRepository>();

            //Interfaces + Services
            builder.Services.AddScoped<IClass, ClassServices>();
            builder.Services.AddScoped<IClassUser, ClassUserServices>();
            builder.Services.AddScoped<ILearningObjective, LearningObjectiveServices>();
            builder.Services.AddScoped<ISyllabusObjective, SyllabusObjectiveServices>();
            builder.Services.AddScoped<ISyllabus, SyllabusServices>();
            builder.Services.AddScoped<ITrainingContent, TrainingContentServices>();
            builder.Services.AddScoped<ITrainingProgram, TrainingProgramService>();
            builder.Services.AddScoped<ITrainingProgramSyllabus, TrainingProgramSyllabusServices>();
            builder.Services.AddScoped<ITrainingUnit, TrainingUnitServices>();
            builder.Services.AddScoped<IUserPermission, UserPermissionServices>();
            builder.Services.AddScoped<IAssessment, AssessmentService>();
            builder.Services.AddScoped<IUser, UserServices>();
            builder.Services.AddScoped<IAssessment, AssessmentService>();
            builder.Services.AddScoped<IRefreshHandler, RefreshHandler>();
            builder.Services.AddScoped<ITrainingCalendar, TrainingCalendarServices>();
            builder.Services.AddScoped<FAMSDBContext, FAMSDBContext>();

            //CORS handler
            var CORS_CONFIG = "_CORS_CONFIG";
            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: CORS_CONFIG,
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;

                var refreshHandler = serviceProvider.GetRequiredService<IRefreshHandler>();

                refreshHandler.RemoveAllRefreshToken();
            }

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseCors(CORS_CONFIG);
            app.UseAuthentication();
            app.UseAuthorization();
            app.MapControllers();
            app.Run();
        }
    }
}
