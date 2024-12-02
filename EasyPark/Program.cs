var builder = WebApplication.CreateBuilder(args);

// Adiciona a classe Startup
var startup = new EasyPark.Startup(builder.Configuration);
startup.ConfigureServices(builder.Services);

var app = builder.Build();

// Chama o m�todo Configure da classe Startup
var env = app.Environment;
startup.Configure(app, env);

// Inicia a aplica��o
app.Run();
