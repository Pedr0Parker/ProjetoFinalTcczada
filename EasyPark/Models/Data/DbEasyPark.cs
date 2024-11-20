using Microsoft.EntityFrameworkCore;

namespace EasyPark.Models.Data
{
	public class DbEasyPark : DbContext
	{
		public DbEasyPark(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseNpgsql(Configuration.GetConnectionString("EasyParkConnection"));
		}
	}
}
