using EasyPark.Models.Entidades.Usuario;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace EasyPark.Models.Data
{
	public class DbEasyPark : DbContext
	{
		public DbEasyPark(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public DbSet<Usuarios> Usuarios { get; set; }

		protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
		{
			optionsBuilder.UseNpgsql(Configuration.GetConnectionString("DefaultConnection"));
		}
	}
}
