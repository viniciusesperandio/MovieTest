#MovieApp

Este projeto é uma aplicação ASP.NET Core que consome a API do The Movie Database (TMDb) para listar e exibir detalhes de filmes. Utilizando práticas como injeção de dependência, logging, gerenciamento de exceções e testes unitários.

Índice

- Visão Geral
- Requisitos
- Uso
- Logging
- Gerenciamento de Exceções
- Injeção de Dependências
- Executando os Testes
- Licença

Visão Geral

O projeto consiste em duas páginas principais:
1. Lista: Exibe uma lista de filmes com imagem e título.
2. Detalhes: Exibe informações detalhadas sobre um filme específico, incluindo imagem, título e resumo.

Requisitos

- .NET 8.0 SDK
- Visual Studio 2022 ou mais recente com suporte para desenvolvimento ASP.NET

Uso

Iniciar a Aplicação

1. Clone o repositório:

   git clone https://github.com/viniciusesperandio/MovieTest.git

2. Navegue até o diretório do projeto:

   cd MovieApp

3. Restaurar pacotes e compilar o projeto:

   dotnet restore
   dotnet build

4. Executar a aplicação:

   dotnet run

   A aplicação estará disponível em http://localhost:7112.

Logging

O projeto utiliza o logging integrado do ASP.NET Core. Logs são configurados para serem gravados no console. Você pode personalizar a configuração de logging no arquivo Program.cs e configurar diferentes provedores de log conforme necessário.

Gerenciamento de Exceções

Exceções são gerenciadas usando middleware de exceção no ASP.NET Core. O projeto inclui um handler global para capturar e registrar exceções não tratadas. A configuração do middleware pode ser encontrada no arquivo Program.cs.

Exemplo de Handler de Exceção

app.UseExceptionHandler(options => { });

Injeção de Dependências

A aplicação utiliza a injeção de dependências do ASP.NET Core para gerenciar serviços e repositórios. Os serviços são registrados no contêiner de injeção de dependências no arquivo Program.cs.

Exemplo de Registro de Serviço

builder.Services.AddHttpClient<IMovieService, MovieService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["ApiSettings:BaseUrl"]);

    client.DefaultRequestHeaders.Add("Authorization", $"Bearer {builder.Configuration["ApiSettings:ApiToken"]}");
});

Executando os Testes

Os testes são escritos usando xUnit e Moq. Para executar os testes, siga os passos abaixo:

1. Navegue até o diretório de testes:

   cd MovieApp.Tests

2. Restaurar pacotes e executar os testes:

   dotnet restore
   dotnet test

   Os testes serão executados e o resultado será exibido no console.

Licença

Este projeto é licenciado sob a MIT License (LICENSE).