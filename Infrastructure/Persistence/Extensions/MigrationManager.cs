using Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Persistence.Extensions;

public static class MigrationManager
{
    public static IHost MigrateDatabase(this IHost webHost)
    {
        using (var scope = webHost.Services.CreateScope())
        {
            using var appContext = scope.ServiceProvider.GetRequiredService<DataContext>();
            try
            {
                appContext.Database.Migrate();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        return webHost;
    }
}