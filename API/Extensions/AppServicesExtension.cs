
using Application.Branches;
using Application.Core;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Persistence;

namespace API.Extensions
{
    public static class AppServicesExtension
    {
        public static IServiceCollection AddAppServices (this IServiceCollection services,
            IConfiguration config)
        {
            services.AddControllers();

            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddDbContext<DataContext>(opt =>
            {
                opt.UseSqlServer(config.GetConnectionString("def_conn"));
            });

            services.AddCors(opt =>
            {
                opt.AddPolicy("Access-Control-Allow-Origin", policy =>
                {
                    policy.AllowAnyMethod().AllowAnyHeader().WithOrigins("http://localhost:4200");
                });
            });

            services.AddMediatR(typeof(List.Handler));

            services.AddAutoMapper(typeof(MappingProfiles).Assembly);

            //services.AddFluentValidationAutoValidation();
            //services.AddValidatorsFromAssemblyContaining<Create>();

            return services;
        }
    }
}
