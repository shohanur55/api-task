using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ASPGTRTraining.DataAccess
{
    public class ASPGTRTrainingDBContextFactory : IDesignTimeDbContextFactory<ASPGTRTrainingDBContext>
    {
        public ASPGTRTrainingDBContext CreateDbContext(string[] args)
        {
            // Build configuration to access the connection string
       var configuration = new ConfigurationBuilder()
    .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "../ASPGTRTraining.MVC"))
    .AddJsonFile("appsettings.json")
    .Build();

            var optionsBuilder = new DbContextOptionsBuilder<ASPGTRTrainingDBContext>();

            // Configure DbContext with the connection string and other options
            optionsBuilder.UseNpgsql(configuration.GetConnectionString("conn"),
                b => b.MigrationsAssembly("ASPGTRTraining.DataAccess"))
                .UseLowerCaseNamingConvention();

            return new ASPGTRTrainingDBContext(optionsBuilder.Options);
        }
    }
}
