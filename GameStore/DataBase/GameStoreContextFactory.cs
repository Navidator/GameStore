using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace GameStore.DataBase
{
    public class GameStoreContextFactory : IDesignTimeDbContextFactory<GameStoreContext>
    {
        public GameStoreContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false)
                .Build();

            var connectionString = config.GetConnectionString("DefaultConnection");

            var optionsBuilder = new DbContextOptionsBuilder<GameStoreContext>()
                .UseSqlServer(connectionString);

            return new GameStoreContext(optionsBuilder.Options);
        }
    }
}
