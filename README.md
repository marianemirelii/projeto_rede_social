# Rede Social DevHub 
Projeto desenvolvido durante a diciplina de Programação Web 2, simulando um aplicativo de rede social semelhante ao twitter.  
A aplicação é desenvolvida em com **.NET**, **REACT** e **SQLite** 

## Tecnologias utilizadas 
* .NET 10
* ASP .NET Core Web Api
* SQLite
* Swagger 
* React 19

## Pré-requisitos


* [.NET SDK](https://dotnet.microsoft.com/download) (10)
* Git


---

## Como Rodar a Aplicação

### Clonar o repositório

```bash
git clone https://github.com/wendersondh/projeto_rede_social.git  

cd MeLevaAi
```

---

### Criar o banco de dados (Migrations)

Entre na pasta back do projeto:

caso não tenha, instale o dotnet-ef:
```bash
dotnet tool install --global dotnet-ef
```

Execute:  
```bash
dotnet ef database update --project RedeSocial.Infrastructure --startup-project RedeSocial.WebApi
```


Caso o banco já exista e gere erro de tabelas duplicadas, apague o arquivo .db do SQLite e execute novamente.

---

### Rodar a API

```bash
dotnet run --project RedeSocial.WebApi
```

A API estará disponível com o swagger em:

http://localhost:5257/swagger/index.html

---

### Rodar o Front

Entre na pasta front e execute:
```bash
npm i 

npm run dev
```