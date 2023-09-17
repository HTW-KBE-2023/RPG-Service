using API.Port.Database;
using Microsoft.EntityFrameworkCore;

namespace API.Utility
{
    public static class WebApplicationBuilderExtensions
    {
        public static void AddDatabaseConfiguration(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<RPGContext>(options =>
            {
                var connection = builder.Configuration.GetConnectionString("MySQL");
                options.UseMySql(connection, ServerVersion.AutoDetect(connection));
            });
            builder.Services.AddTransient<DbInitialiser>();
        }
    }
}