// Importa a pasta Contexts do projeto, onde o Scaffold criou o EscolaContext.
using APIbackprojeto.Contexts;
using APIbackprojeto.Interfaces;
using APIbackprojeto.Repositories;




// Importa os recursos do Entity Framework Core (ex.: o método UseSqlServer).
using Microsoft.EntityFrameworkCore;

// Cria o "construtor" da aplicação: lê o appsettings.json, o launchSettings.json
// e prepara a lista de serviços que a API vai usar.
var builder = WebApplication.CreateBuilder(args);

// ===== PARTE 1: REGISTRAR OS SERVIÇOS (o que a API vai ter disponível) =====

// Habilita o uso de Controllers na API (as classes da pasta Controllers).
builder.Services.AddControllers();

// Registra o ConcessionariaContext (a "ponte" com o banco) para a API poder usá-lo.
// Sempre que alguém pedir um ConcessionariaContext, a API entrega um pronto.
builder.Services.AddDbContext<ConcessionariaContext>(options =>
    // Diz que o banco é SQL Server e busca a connection string chamada "Concessionaria"
    // dentro da seção ConnectionStrings do appsettings.json.
    options.UseSqlServer(builder.Configuration.GetConnectionString("Concessionaria")));


// NOVO: quando alguém pedir ICursoRepository, a API entrega um CursoRepository.
builder.Services.AddScoped<IMarcaRepository, MarcaRepository>();


// Monta a aplicação com tudo o que foi registrado acima.
// Daqui para baixo não dá mais para registrar serviços.
var app = builder.Build();

// ===== PARTE 2: O CAMINHO DE CADA REQUISIÇÃO (pipeline) =====

// Se alguém acessar pelo endereço http, a API redireciona para https (mais seguro).
app.UseHttpsRedirection();

// Verifica as permissões de acesso (vai ser usado quando tivermos login).
app.UseAuthorization();

// Liga as rotas definidas nos Controllers (ex.: api/curso).
app.MapControllers();

// Rota de TESTE: quando alguém acessar /api/cursos com GET,
// a API recebe o ConcessionariaContext pronto (db), busca todos os registros
// da tabela Marca e devolve como uma lista em JSON.
// Apagar depois do teste: nas próximas aulas usaremos Controllers.
app.MapGet("/api/cursos", (ConcessionariaContext db) => db.Marca.ToList());

// Liga a API e deixa ela esperando requisições (até você parar o projeto).
app.Run();