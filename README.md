# lista
# Guia de Configuração e Estrutura do Projeto
Este projeto implementa um CRUD completo em ASP.NET Core MVC utilizando MySQL como banco de dados. Abaixo estão descritas todas as etapas e ajustes necessários para configurar e executar o sistema.

1. Instalação do Pacote MySQL para Entity Framework Core
No terminal do Visual Studio Code, execute o comando abaixo para instalar o pacote que permite a integração do Entity Framework Core com o MySQL:
```
dotnet add package Pomelo.EntityFrameworkCore.MySql
```

2. Configuração da Connection String
No arquivo appsettings.json, configure a string de conexão com o banco de dados MySQL:
```
"ConnectionStrings": {
  "DefaultConnection": "server=localhost;database=lista;user=ligia;password=Graviola123"
}
```

Altere os valores conforme o seu ambiente.

3. Criação do DbContext
Na pasta Data, crie o arquivo AppDbContext.cs com o seguinte conteúdo:
```
using Microsoft.EntityFrameworkCore;
using lista.Models;

namespace lista.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<ListaModel> Listas { get; set; }
    }
}
```

4. Registro do Serviço do DbContext
No arquivo Program.cs, registre o serviço do DbContext para utilizar o MySQL:
```
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseMySql(
        builder.Configuration.GetConnectionString("DefaultConnection"),
        ServerVersion.AutoDetect(builder.Configuration.GetConnectionString("DefaultConnection"))
    )
);
```
5. Criação da Model
Na pasta Models, crie o arquivo ListaModel.cs:
```
namespace lista.Models;
public class ListaModel
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public DateTime DataCriacao { get; set; }  = DateTime.Now;
}
```
6. Criação do Controller
Na pasta Controllers, crie o arquivo ListaController.cs responsável por intermediar as operações entre a view e o banco de dados:
```
using Microsoft.AspNetCore.Mvc;
using lista.Data;
using lista.Models;
using Microsoft.EntityFrameworkCore;

namespace lista.Controllers
{
    public class ListaController : Controller
    {
        private readonly AppDbContext _context;

        public ListaController(AppDbContext context)
        {
            _context = context;
        }

        // Métodos Index, Create, Edit e Delete implementados para CRUD
    }
}
```
7. Criação das Views
Na pasta Lista, crie as views Index.cshtml, Create.cshtml, Edit.cshtml e Delete.cshtml para listar, cadastrar, editar e excluir registros, respectivamente.
Essas views utilizam o Razor para renderizar formulários e tabelas de dados.

8. Adição do Menu de Navegação
No arquivo _Layout.cshtml, adicione um item de menu para acessar a página de listas:
```
<ul class="navbar-nav flex-grow-1">
    <li class="nav-item">
        <a class="nav-link text-dark" asp-area="" asp-controller="Lista" asp-action="Index">Listas</a>
    </li>
</ul>
```
9. Execução do Projeto
Após realizar todos os ajustes, execute o projeto. O menu "Listas" estará disponível para acessar as funcionalidades de cadastro, edição, exclusão e listagem de registros no banco de dados MySQL.