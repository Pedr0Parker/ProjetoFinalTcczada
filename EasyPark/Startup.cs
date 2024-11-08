using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using EasyPark.Models.Data;
using EasyPark.Models.Repositorios;
using EasyPark.Controllers.Plano;
using EasyPark.Models.RegrasNegocio.Plano;
using EasyPark.Controllers.Usuario;
using EasyPark.Models.RegrasNegocio.Usuario;
using EasyPark.Controllers.VisitaEstacionamento;
using EasyPark.Models.RegrasNegocio.VisitaEstacionamento;
using EasyPark.Controllers.Dependente;
using EasyPark.Models.RegrasNegocio.Dependente;
using EasyPark.Controllers.Funcionario;
using EasyPark.Models.RegrasNegocio.Funcionario;
using EasyPark.Models.RegrasNegocio.Empresa;
using EasyPark.Controllers.Empresa;
using EasyPark.Controllers.Estacionamento;
using EasyPark.Models.RegrasNegocio.Estacionamento;
using EasyPark.Models.RegrasNegocio.SistemaEasyPark;
using EasyPark.Controllers.SistemaEasyPark;

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
			services.AddTransient<DependenteRepositorio>();
			services.AddTransient<DependenteBusinessRule>();
			services.AddTransient<DependenteController>();

			services.AddTransient<EmpresaRepositorio>();
			services.AddTransient<EmpresaBusinessRule>();
			services.AddTransient<EmpresaController>();

			services.AddTransient<EstacionamentoRepositorio>();
			services.AddTransient<EstacionamentoBusinessRule>();
			services.AddTransient<EstacionamentoController>();

			services.AddTransient<FuncionarioRepositorio>();
			services.AddTransient<FuncionarioBusinessRule>();
			services.AddTransient<FuncionarioController>();

			services.AddTransient<PlanoRepositorio>();
			services.AddTransient<PlanoBusinessRule>();
			services.AddTransient<PlanoController>();

			services.AddTransient<SistemaEasyParkRepositorio>();
			services.AddTransient<SistemaEasyParkBusinessRule>();
			services.AddTransient<SistemaEasyParkController>();

			services.AddTransient<UsuarioRepositorio>();
			services.AddTransient<UsuarioBusinessRule>();
			services.AddTransient<UsuarioController>();

			services.AddTransient<VisitaEstacionamentoRepositorio>();
			services.AddTransient<VisitaEstacionamentoBusinessRule>();
			services.AddTransient<VisitaEstacionamentoController>();

			services.AddCors(options =>
			{
				options.AddPolicy("AllowSpecificOrigin",
					builder => builder.WithOrigins("http://127.0.0.1:5500")
									  .AllowAnyHeader()
									  .AllowAnyMethod());
			});

			services.AddControllers();

			// Registre o repositório como um serviço
			services.AddTransient<EstacionamentoRepositorio>();

			// Certifique-se de que a configuração está sendo passada
			services.AddDbContext<DbEasyPark>(options =>
				options.UseNpgsql(Configuration.GetConnectionString("DbEasyParkConnection")));

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

			// Ativando o CORS
			app.UseCors("AllowSpecificOrigin");

			app.UseAuthorization();

			// Mapear as rotas para os controllers
			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});

			//app.UseHttpsRedirection();
			//app.UseRouting();

			//app.UseAuthorization();

			//// Mapear as rotas para os controllers
			//app.UseEndpoints(endpoints =>
			//{
			//	endpoints.MapControllers();
			//});
		}
	}
}
	