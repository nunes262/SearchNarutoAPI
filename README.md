# SearchNarutoAPI

Aplicação **Blazor WebAssembly (.NET 10)** que consome a [Dattebayo API](https://api-dattebayo.vercel.app/docs) para listar e buscar personagens do universo Naruto.

## Funcionalidades

- Listagem paginada dos personagens (`GET /characters?page=N&limit=20`)
- Busca de personagens por nome (`GET /characters?name=...`)
- Página de detalhes por ID (`GET /characters/{id}`)
- Exibição de **nome**, **imagem** (`images`), **clã** (`personal.clan`), **afiliação** (`personal.affiliation`), **jutsus** (`jutsu`) e **patente ninja** (`rank.ninjaRank`)

## Estrutura

```
Models/
  Character.cs            # Modelo do personagem (trata os formatos variáveis da API)
  CharacterResponse.cs    # Resposta paginada da API
Services/
  NarutoService.cs        # Consumo da API via HttpClient
Shared/
  CharacterCard.razor     # Card de personagem
Pages/
  Home.razor              # Tela de listagem e busca
  CharacterDetails.razor  # Detalhes do personagem por ID
```

## Como executar

Pré-requisito: [.NET SDK 10](https://dotnet.microsoft.com/download)

```bash
dotnet run
```

Acesse o endereço exibido no terminal (ex.: `http://localhost:5310`).

> A API fica hospedada no Render (plano gratuito), então a primeira requisição pode demorar alguns segundos.
