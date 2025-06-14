# 📝 ToDo List API - ASP.NET Core

Esta é uma API RESTful de gerenciamento de tarefas desenvolvida com ASP.NET Core 9, Entity Framework Core e autenticação JWT.

## 🔐 Funcionalidades

- Cadastro e login de usuários com tokens JWT
- CRUD de tarefas
- Cada usuário só vê suas próprias tarefas
- Filtros por status de tarefa (concluída ou pendente)
- Ordenação por data
- Documentação Swagger
- SQLite como banco de dados

## 📂 Tecnologias

- ASP.NET Core 9
- Entity Framework Core
- AutoMapper
- JWT Authentication
- Swagger UI
- SQLite

## 🛠️ Como rodar localmente

```bash
dotnet restore
dotnet ef database update
dotnet run
```

Acesse: `https://localhost:5001/swagger`

## 📬 Requisições

### Registro

`POST /api/auth/register`

```json
{
  "username": "fulano",
  "email": "fulano123@email.com",
  "password": "suaSenha123"
}
```

### Login

`POST /api/auth/login`

```json
{
  "login": "fulano",
  "password": "suaSenha123"
}
```

Retorna um token que deve ser usado em todas as chamadas autenticadas:

```
Authorization: Bearer SEU_TOKEN
```

### Tarefas

- `GET /api/tasks`
- `GET /api/tasks/filter?isCompleted=true`
- `GET /api/tasks/{id}`
- `POST /api/tasks`
- `PUT /api/tasks/{id}`
- `DELETE /api/tasks/{id}`

## 🚀 Deploy

Versão em produção hospedada no [Railway](https://railway.app/) (link em breve)

---

## 📌 Sobre o projeto

Este projeto foi desenvolvido como parte do meu portfólio para aprendizado e demonstração de conhecimento em .NET backend.

Desenvolvido por [Kaio Mendes da Costa](https://github.com/Mang0-Juic3).
