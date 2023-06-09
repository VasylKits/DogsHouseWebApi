using Domain.Entities;
using Infrastructure.Persistence.Context;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Infrastructure.Persistence.Extensions;

public static class DogsInitializer
{
    public static IHost Seed(this IHost webHost)
    {
        using var scope = webHost.Services.CreateScope();
        {
            using var context = scope.ServiceProvider.GetRequiredService<DataContext>();

            try
            {
                context.Database.EnsureCreated();

                var dogs = context.Dogs.FirstOrDefault();

                if (dogs is null)
                {
                    context.Dogs.AddRange(
                        new Dog { Name = "Neo", Color = "red & amber", TailLength = 22, Weight = 32 },
                        new Dog { Name = "Jessy", Color = "black & white", TailLength = 7, Weight = 14 },
                        new Dog { Name = "Doggy", Color = "red", TailLength = 173, Weight = 33 });
                }

                context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        return webHost;
    }
}