using Microsoft.EntityFrameworkCore;
using Task.Repository;
using Task.Service;
using Task.Service.Interfaces.Repository;
using Task.Service.Interfaces.Services;

namespace Task.Api.Configuration
{
    public static class DependencyConfiguration
    {
        public static void ConfigureDependency(this WebApplicationBuilder builder)
        {
            if (builder == null) throw new ArgumentNullException(nameof(builder));

            var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddDbContext<TaskDbContext>(options => options.UseSqlServer(connectionString));
        }
    }
}