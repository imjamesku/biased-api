using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WebApi.Services;

namespace WebApi.Helpers
{
    public class PostgresDataContext : DataContext
    {
        ISecretService secretService;
        public PostgresDataContext(IConfiguration configuration) : base(configuration)
        {
            this.secretService = new SecretService();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
        {
            // connect to sqlite database
            string connectionString = secretService.GetSecret("pg-connection-string");
            options.UseNpgsql(connectionString);
            // options.UseNpgsql(Configuration.GetConnectionString("WebApiDatabase"));
        }
    }
}