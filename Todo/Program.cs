using Todo.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();//Registra o suporte a controllers (permite usar [ApiController], [HttpGet], etc.)
builder.Services.AddDbContext<AppDbContext>();//Torna a conexao um servico do ASP.NET

var app = builder.Build();

app.MapControllers();//Faz o ASP.NET escutar e responder os endpoints definidos nos controllers
//Um endpoint = rota + método HTTP (GET, POST, PUT, DELETE).

app.Run();
