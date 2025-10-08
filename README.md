# Challenge API

Este repositório contém uma API ASP.NET Core dividida em camadas: `Challenge.Api`, `Challenge.Application`, `Challenge.Domain` e `Challenge.Infrastructure`.

## Principais pontos

- TargetFramework: .NET 8 (veja `Challenge.Api/Challenge.Api.csproj`).
- A API usa JWT para autenticação e Swagger para documentação.

## Como rodar localmente (pré-requisitos)

- .NET 8 SDK instalado

1) Restaurar e buildar a solução:

   dotnet restore
   dotnet build -c Release

2) Rodar a API (a partir da pasta `Challenge.Api`):

   dotnet run --project Challenge.Api -c Release

Execução via Docker (recomendada para ambientes isolados):

1) Construir a imagem Docker (a partir da pasta `Challenge.Api`):

   docker build -t challenge-api:latest .

2) Executar o container:

   docker run -d -p 8080:80 --name challenge-api challenge-api:latest

   A API estará disponível em [http://localhost:8080](http://localhost:8080)

## Notas sobre variáveis de ambiente e configuração

- As configurações padrão estão em `appsettings.json` e `appsettings.Development.json` dentro de `Challenge.Api`.
- Para habilitar JWT/segurança, configure as chaves e variáveis esperadas pelo middleware (`JwtMiddleware` e serviços relacionados) — ver `Challenge.Application/Services/AuthTokenService.cs` para detalhes de implementação.

## Sugestões / Próximos passos

- Adicionar um `docker-compose.yml` se você precisar de dependências (banco, redis, etc.).
- Adicionar instruções de CI/CD para build e push da imagem.
