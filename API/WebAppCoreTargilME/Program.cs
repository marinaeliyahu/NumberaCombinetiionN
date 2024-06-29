
using WebAppCoreTargilME.ApiServices;
using WebAppCoreTargilME.Mapper;
using WebAppCoreTargilME.BusinessLogic;
namespace WebAppCoreTargilME
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
             
            builder.Services.AddControllers();
            builder.Services.AddTransient<INumberCombinationsService, NumberCombinationsService>();
            builder.Services.AddTransient<INumberCombinationsBL, NumberCombinationsBL>();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddAutoMapper(c =>
            {
                c.AddProfile(new DomainToViewModelMappingProfile());
            });
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
                app.UseDeveloperExceptionPage();
                app.UseCors(builder => builder
                .AllowAnyHeader()
                .WithOrigins(new string[] { "http://localhost:4200", "https://localhost:4200" })
                .AllowCredentials()
                .AllowAnyMethod());
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}