# 🛰️ Mottu UWB API

API RESTful desenvolvida em ASP.NET Core 9 com integração ao PostgreSQL, focada no rastreamento de motos utilizando sensores UWB (Ultra Wideband).
O objetivo é facilitar a gestão de veículos em pátios de alta densidade da Mottu, com localização precisa e identificação única de cada moto.

---

## 🎯 Objetivo do Projeto

Esta solução foi desenvolvida para atender à Global Solution – Advanced Business Development with .NET, criando uma aplicação inovadora que auxilia em períodos de urgência com rastreamento e monitoramento de veículos em tempo real.

### Requisitos atendidos:

- ✅ API REST com boas práticas de programação

- ✅ Persistência em banco de dados relacional (PostgreSQL)

- ✅ Relacionamentos 1:N e N:1 (Moto → Sensores; Moto → Localizações)

- ✅ Documentação interativa com Swagger

- ✅ Uso de Migrations para versionamento do banco

- ✅ Projeto Razor Pages com TagHelpers para visualização básica dos dados

---

## 🚀 Tecnologias Utilizadas

- ASP.NET Core 9
- Entity Framework Core
- PostgreSQL (via EF Core Provider Npgsql)
- Swagger / OpenAPI (Swashbuckle)
- Visual Studio 2022
- Razor Pages + TagHelpers

---

## 🗂️ Modelagem

### Diagrama Entidade-Relacionamento (ERD)

```

erDiagram
    MOTO ||--o{ SENSOR : possui
    MOTO ||--o{ LOCALIZACAO : possui
    MOTO {
        int Id
        string Modelo
        string Cor
        string IdentificadorUWB
        string Status
    }
    SENSOR {
        int Id
        string Codigo
        int MotoId
    }
    LOCALIZACAO {
        int Id
        int MotoId
        double PosX
        double PosY
        DateTime Timestamp
    }
    
```

### Arquitetura da Solução

```

flowchart TD
    Client[Usuário / Razor Pages] --> API[API REST ASP.NET Core]
    API --> Swagger[Swagger UI]
    API --> DB[(PostgreSQL Database)]

```

---

## ⚙️ Como executar o projeto localmente

1. Crie um banco no PostgreSQL, por exemplo:

```
CREATE DATABASE mottu;

```

3. Configure a connection string no appsettings.json:

```

"ConnectionStrings": {
  "PostgresConnection": "Host=localhost;Port=5432;Database=mottu;Username=postgres;Password=postgres"
}

```

3. Execute as migrations:

```

dotnet ef migrations add InitialCreate
dotnet ef database update

```

4. Rode o projeto:

```

dotnet run

```


5. Acesse o Swagger em:

- http://localhost:5091/swagger
- ou https://localhost:7040/swagger

---

## 🌐 Rotas da API
### 🏍️ Motos

| Método | Endpoint                          | Descrição |
|-------------------------------------|----------|----------------------------------------|
| GET | api/moto | Lista todas as motos cadastradas. |
|GET | api/moto/{id} | Retorna uma moto pelo ID. |
|POST | api/moto | Cadastra nova moto (IdentificadorUWB deve ser único). |
|PUT | api/moto/{id} | Atualiza dados de uma moto. |
|DELETE | api/moto/{id} | Remove moto. |


### 📡 Sensores

| Método | Endpoint                          | Descrição |
|-------------------------------------|----------|----------------------------------------|
|GET | api/sensor |  Lista todos os sensores. |
|GET | api/sensor/{id} |  Retorna sensor pelo ID. |
|POST | api/sensor |  Cadastra sensor. |
|PUT | api/sensor/{id} |  Atualiza sensor. |
|DELETE | api/sensor/{id} |  Remove sensor. |

### 📍 Localizações

| Método | Endpoint                          | Descrição |
|-------------------------------------|----------|----------------------------------------|
Método	Endpoint	Descrição
|GET	| api/localizacao	| Lista todas as localizações. |
|GET	| api/localizacao/{id}	| Retorna localização por ID. |
|POST	| api/localizacao	| Registra posição da moto no pátio. |
|PUT	| api/localizacao/{id}	| Atualiza dados da localização. |
|DELETE	| api/localizacao/{id}	| Remove localização. |

---

## 🧪 Exemplos de Testes

### Cadastro de Moto

```

POST /api/moto
{
  "modelo": "Honda CG 160",
  "cor": "Preta",
  "identificadorUWB": "UWB-12345",
  "status": "Disponível"
}

```

### Cadastro de Sensor

```

POST /api/sensor
{
  "codigo": "SENSOR-01",
  "motoId": 1
}

```

### Cadastro de Localização

```

POST /api/localizacao
{
  "motoId": 1,
  "posX": 12.34,
  "posY": 56.78,
  "timestamp": "2025-10-01T03:15:00Z"
}

```

### Retorno esperado (exemplo Moto)

```

{
  "id": 1,
  "modelo": "Honda CG 160",
  "cor": "Preta",
  "identificadorUWB": "UWB-12345",
  "status": "Disponível",
  "sensores": [],
  "localizacoes": []
}

```

---

## 🖥️ Projeto Razor Pages

Foi criada uma página simples que consome a API de motos e lista os registros em uma tabela HTML.

Exemplo de uso de TagHelper no Razor:

```

<form asp-action="Create">
    <label asp-for="IdentificadorUWB"></label>
    <input asp-for="IdentificadorUWB" class="form-control" />
    <button type="submit" class="btn btn-primary">Cadastrar Moto</button>
</form>

```

## 👥 Equipe

| Nome                                | RM       | GitHub                                |
|-------------------------------------|----------|----------------------------------------|
| Murilo Ribeiro Santos               | RM555109 | [@murilors27](https://github.com/murilors27) |
| Thiago Garcia Tonato                | RM99404  | [@thiago-tonato](https://github.com/thiago-tonato) |
| Ian Madeira Gonçalves da Silva      | RM555502 | [@IanMadeira](https://github.com/IanMadeira) |

**Curso**: Análise e Desenvolvimento de Sistemas  
**Instituição**: FIAP – Faculdade de Informática e Administração Paulista
