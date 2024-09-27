using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using EasyPark.Models.Data;
using EasyPark.Models.Repositorios;

namespace EasyPark
{
	public class Startup
	{
		public Startup(IConfiguration configuration)
		{
			Configuration = configuration;
		}

		public IConfiguration Configuration { get; }

		public void ConfigureServices(IServiceCollection services)
		{
			services.AddControllers();

			// Registre o repositório como um serviço
			services.AddTransient<EstacionamentoRepositorio>();

			// Certifique-se de que a configuração está sendo passada
			services.AddDbContext<DbEasyPark>(options =>
				options.UseNpgsql(Configuration.GetConnectionString("EasyParkConnection")));

			// Configurando o DbContext com PostgreSQL
			//services.AddDbContext<DbEasyPark>(options =>
			//	options.UseNpgsql(Configuration.GetConnectionString("EasyParkConnection")));

			// Configuração do Swagger
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "EasyPark", Version = "v1" });
			});
		}

		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			// Usa o Swagger tanto no ambiente de desenvolvimento quanto no de produção
			app.UseSwagger();
			app.UseSwaggerUI(c =>
			{
				c.SwaggerEndpoint("/swagger/v1/swagger.json", "EasyPark V1");
				// Exibe a UI Swagger na raiz da aplicação
				c.RoutePrefix = string.Empty;
			});

			// Ambiente de desenvolvimento
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				// Configuração para produção
				app.UseExceptionHandler("/Home/Error");
				app.UseHsts();
			}

			app.UseHttpsRedirection();
			app.UseRouting();
			app.UseAuthorization();

			// Mapear as rotas para os controllers
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
	