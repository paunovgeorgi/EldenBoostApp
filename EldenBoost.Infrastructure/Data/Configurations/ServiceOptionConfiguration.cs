using EldenBoost.Infrastructure.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EldenBoost.Infrastructure.Data.Configurations
{
    public class ServiceOptionConfiguration : IEntityTypeConfiguration<ServiceOption>
    {
        public void Configure(EntityTypeBuilder<ServiceOption> builder)
        {
            builder.HasData(GenerateServiceOptions());
        }

        private ServiceOption[] GenerateServiceOptions()
        {
            return new ServiceOption[]
            {
                new ServiceOption { Id = 1, Name = "Margit the Fell Omen", Price = 10.00M, ServiceId = 2 },
                new ServiceOption { Id = 2, Name = "Godrick the Grafted", Price = 12.00M, ServiceId = 2 },
                new ServiceOption { Id = 3, Name = "Red Wolf of Radagon", Price = 15.00M, ServiceId = 2 },
                new ServiceOption { Id = 4, Name = "Leonine Misbegotten", Price = 15.00M, ServiceId = 2 },
                new ServiceOption { Id = 5, Name = "Malenia, Blade of Miquella", Price = 25.00M, ServiceId = 2},
                new ServiceOption { Id = 6, Name = "Stormveil Castle", Price = 39.00M, ServiceId = 8},
                new ServiceOption { Id = 7, Name = "Volcano Manor", Price = 49.00M, ServiceId = 8},
                new ServiceOption { Id = 9, Name = "Crumbling Farum Azula", Price = 59.00M, ServiceId = 8},
            };
        }
    }
}
