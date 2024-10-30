using Gym_Fees.DataBase;
using Gym_Fees.IRepo;
using Gym_Fees.IService;
using Gym_Fees.Repository;
using Gym_Fees.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using System.Text;
using System.Text.Encodings.Web;

namespace Gym_Fees
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("DBConnection");

            var DatabaseInitialize = new AppDbContext(connectionString);
            DatabaseInitialize.Initialize();

            //       builder.Services.AddAuthentication("BasicAuthentication")
            //.AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
            //       builder.Services.AddTransient<AppDbContext>(provider =>
            //       {
            //           string connectionString = builder.Configuration.GetConnectionString("DBConnection");
            //           return new AppDbContext(connectionString);
            //       });

            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    options.JsonSerializerOptions.PropertyNamingPolicy = null;
                    options.JsonSerializerOptions.Converters.Add(new System.Text.Json.Serialization.JsonStringEnumConverter()); // Enable string enum conversion
                });
            builder.Services.AddTransient<IMemberRepo>(provider => new MemberRepository(connectionString));
            builder.Services.AddTransient<IMemberService>(provider => new MemberService(provider.GetRequiredService<IMemberRepo>()));

            builder.Services.AddTransient<IFileService, FileService>();

            builder.Services.AddTransient<IPendingeditsRepo>(provider => new PendingeditsRepository(connectionString));
            builder.Services.AddTransient<IPendingeditsService>(provider => new PendingeditsService(provider.GetRequiredService<IPendingeditsRepo>()));

            builder.Services.AddTransient<INotificationRepository>(provider => new NotificationRepository(connectionString));
            builder.Services.AddTransient<INotificationServices>(provider => new NotificationServices(provider.GetRequiredService<INotificationRepository>()));

            builder.Services.AddTransient<IPaymentRepo>(provider => new PaymentRepository(connectionString));
            builder.Services.AddTransient<IPaymentService>(provider => new PaymentService(provider.GetRequiredService<IPaymentRepo>()));

            builder.Services.AddTransient<IPendingProgramRepo>(provider => new PendingProgramRepository(connectionString));
            builder.Services.AddTransient<IPendingProgramService>(provider => new PendingProgramService(provider.GetRequiredService<IPendingProgramRepo>()));

            builder.Services.AddTransient<ITrainingProgramRepo>(provider => new TrainingProgramRepository(connectionString));
            builder.Services.AddTransient<ITrainingProgramService>(provider => new TrainingProgramService(provider.GetRequiredService<ITrainingProgramRepo>()));

            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins("*").AllowAnyMethod().AllowAnyHeader(); ;
                });
            });

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder =>
                    {
                        builder.AllowAnyOrigin()
                               .AllowAnyMethod()
                               .AllowAnyHeader();
                    });
            });

            builder.Services.AddControllers();

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();


            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
            }

            app.UseStaticFiles(new StaticFileOptions
            {
                FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "Uploads")),
                RequestPath = "/Resources"
            });

            app.UseCors("AllowAllOrigins");

            app.UseRouting();
            app.MapControllers();
            app.Run();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.UseDeveloperExceptionPage();
            app.MapControllers();

            app.Run();
        }
    }
}
