# 🛰️ Mottu UWB API

API RESTful desenvolvida em ASP.NET Core 9 com integração ao Oracle XE 21c, focada no rastreamento de motos utilizando sensores UWB (Ultra Wideband).
O objetivo é facilitar a gestão de veículos em pátios de alta densidade da Mottu, com localização precisa e identificação única de cada moto.

---

## 🎯 Objetivo do Projeto

Esta solução foi desenvolvida para atender à Global Solution – Advanced Business Development with .NET, criando uma aplicação inovadora que auxilia em períodos de urgência com rastreamento e monitoramento de veículos em tempo real.

### Requisitos atendidos:

- ✅ API REST com boas práticas de programação

- ✅ Persistência em banco de dados relacional (Oracle)

- ✅ Relacionamento 1:N (Moto → Sensores)

- ✅ Documentação com Swagger

- ✅ Uso de Migrations para versionamento do banco

- ✅ Projeto Razor Pages com TagHelpers para visualização básica dos dados

---

## 🚀 Tecnologias Utilizadas

- ASP.NET Core 9
- Entity Framework Core
- Oracle Database (via EF Core Provider)
- Swagger / OpenAPI (Swashbuckle)
- Visual Studio 2022
- Razor Pages + TagHelpers

---

## 🗂️ Modelagem

### Diagrama Entidade-Relacionamento (ERD)

```

erDiagram
    MOTO ||--o{ SENSOR : possui
    MOTO {
        int Id
        string IdentificadorUWB
        string Modelo
    }
    SENSOR {
        int Id
        string Codigo
        int MotoId
    }
    
```

### Arquitetura da Solução

```

flowchart TD
    Client[Usuário / Razor Pages] --> API[API REST ASP.NET Core]
    API --> Swagger[Swagger UI]
    API --> DB[(Oracle Database)]

```

---

## ⚙️ Como executar o projeto localmente

1. Instale o Oracle XE 21c (ou outro Oracle disponível).

2. Configure a connection string no Program.cs ou appsettings.json.

- Exemplo:

```

User Id=APP;Password=APP;Data Source=localhost:1521/XEPDB1;

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

---

## 🧪 Exemplos de Testes

### Cadastro de Moto

```

POST /api/moto
{
  "identificadorUWB": "UWB-12345",
  "modelo": "Honda CG 160"
}

```

### Cadastro de Sensor vinculado à Moto

```

POST /api/sensor
{
  "codigo": "SENSOR-01",
  "motoId": 1
}

```

### Retorno esperado

```

{
  "id": 1,
  "codigo": "SENSOR-01",
  "motoId": 1
}

```

Também é possível testar todas as rotas via Swagger.

---

## 🖥️ Projeto Razor Pages

Para demonstrar Razor e TagHelpers, foi criada uma página simples que consome a API de motos e lista os registros em uma tabela HTML.
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
