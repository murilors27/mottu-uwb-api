# 🛰️ Mottu UWB API

API RESTful desenvolvida em ASP.NET Core com integração ao banco de dados Oracle, focada no rastreamento de motos utilizando sensores UWB (Ultra Wideband). O objetivo é facilitar a gestão de veículos em pátios de alta densidade da Mottu, com localização precisa e identificação única de cada moto.

---

## 🚀 Tecnologias Utilizadas

- ASP.NET Core 9
- Entity Framework Core
- Oracle Database (via EF Core Provider)
- Swagger / OpenAPI (Swashbuckle)
- Visual Studio 2022

---

## ⚙️ Como executar o projeto localmente

1. Certifique-se de ter o Oracle Database instalado localmente. Foi utilizado o **Oracle XE 21c**.
2. Configure a string de conexão no arquivo `Program.cs` com as credenciais do seu banco Oracle local (usuário, senha, host, porta e SID).
3. Abra o projeto no **Visual Studio 2022**.
4. No terminal, na raiz do projeto, inicialize as migrations com:
   - Ef Core: execute a criação da migration com o comando para adicionar a primeira estrutura de tabelas.
   - Em seguida, aplique as migrations para criar as tabelas no banco Oracle.
5. Com o banco criado, pressione **F5** ou execute o projeto.
6. O navegador abrirá automaticamente no endereço `http://localhost:5091/swagger` (ou `https://localhost:7040/swagger`), exibindo a interface Swagger para testes.

---

## 🌐 Rotas da API

### 🏍️ Motos

- `GET /api/moto`  
  Lista todas as motos cadastradas com seus sensores.

- `GET /api/moto/{id}`  
  Retorna os dados de uma moto específica pelo ID.

- `POST /api/moto`  
  Cadastra uma nova moto. O campo `IdentificadorUWB` deve ser único.

- `PUT /api/moto/{id}`  
  Atualiza os dados de uma moto existente.

- `DELETE /api/moto/{id}`  
  Remove uma moto cadastrada.

---

### 📡 Sensores

- `GET /api/sensor`  
  Lista todos os sensores cadastrados.

- `GET /api/sensor/{id}`  
  Retorna os dados de um sensor específico.

- `POST /api/sensor`  
  Cadastra um novo sensor.

- `PUT /api/sensor/{id}`  
  Atualiza os dados de um sensor.

- `DELETE /api/sensor/{id}`  
  Remove um sensor do sistema.

---

## 👥 Equipe

| Nome                                | RM       | GitHub                                      |
|-------------------------------------|----------|---------------------------------------------|
| Murilo Ribeiro Santos               | RM555109 | [@murilors27](https://github.com/murilors27) |
| Thiago Garcia Tonato                | RM99404  | [@thiago-tonato](https://github.com/thiago-tonato) |
| Ian Madeira Gonçalves da Silva      | RM555502 | [@IanMadeira](https://github.com/IanMadeira) |

**Curso**: Análise e Desenvolvimento de Sistemas  
**Instituição**: FIAP – Faculdade de Informática e Administração Paulista
