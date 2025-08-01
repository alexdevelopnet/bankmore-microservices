<h1 align="center">💰 BankMore</h1>
<h3 align="center">Sistema bancário em microsserviços com .NET 8 + Docker + Kafka</h3>

<p align="center">
  <img src="https://img.shields.io/badge/status-em%20desenvolvimento-yellow" />
  <img src="https://img.shields.io/badge/tecnologia-.NET%208-blue" />
  <img src="https://img.shields.io/badge/licen%C3%A7a-MIT-green" />
</p>

---

## 📌 Descrição

**BankMore** é um sistema bancário moderno construído com arquitetura de **microsserviços** utilizando:
- .NET 8
- CQRS + Kafka
- Docker + Docker Compose
- Repositórios SQL Server e MySQL
- JWT, autenticação segura e testes automatizados

> 💡 Este projeto foi desenvolvido como desafio técnico, com foco em escalabilidade, segurança e boas práticas de engenharia de software.

---

## ⚙️ Funcionalidades

- Cadastro e autenticação de usuários
- Consultas de saldo
- Depósitos e saques com validação
- Transferências bancárias seguras
- Tarifas dinâmicas e controle de idempotência

---

## 🧱 Arquitetura

- Microsserviços com APIs separadas
- CQRS com Commands/Queries
- Kafka como barramento de eventos
- SQLite (local) e MySQL (Docker)
- Repositório genérico + D.I. condicional
- Testes automatizados com xUnit

---

## 🛠️ Tecnologias

- ✅ ASP.NET Core 8
- ✅ Entity Framework Core
- ✅ Kafka
- ✅ Docker / Docker Compose
- ✅ SQLite / SQL Server / MySQL
- ✅ xUnit
- ✅ JWT
- ✅ Swagger

---

## 📸 Imagens

<p align="center">
  <img src="https://via.placeholder.com/800x400?text=Dashboard+BankMore" alt="Dashboard" />
</p>

---

## 🚀 Executar com Docker

```bash
docker-compose up --build
Acesse: http://localhost:5000/swagger

✍️ Autor
AlexDevelopNet
🔗 https://www.linkedin.com/in/alex-feitoza-6056a5237/
📧 contato: alexdevelopnet@gmail.com

📝 License
MIT © Alex Feitoza

🌍 English version
<details> <summary>Click to expand</summary>
💰 BankMore
BankMore is a modern banking system built using microservices with:

.NET 8

CQRS + Kafka

Docker + Docker Compose

SQL Server / MySQL / SQLite

JWT authentication and automated tests

🔧 Features
User registration and login

Balance query

Deposit and withdraw

Secure transfers with fee system

Idempotency control

📦 Architecture
Clean Architecture & CQRS

Command/Query separation

Kafka event stream

Generic repositories with conditional DI

Full Docker support

🛠️ Tech Stack
ASP.NET Core 8

EF Core

Kafka

Docker

SQLite / SQL Server / MySQL

xUnit

JWT

Swagger

📄 How to run
bash
Copy
Edit
docker-compose up --build
Go to: http://localhost:5000/swagger

👨‍💻 Author
AlexDevelopNet
🔗 linkedin.com/in/alex-feitoza-6056a5237/  
---

## 🐳 Como Rodar com Docker

```bash
# 1. Clone o projeto
git clone https://github.com/alexdevelopnet/BankMore.alex.feitoza.git

# 2. Acesse a pasta do projeto
cd BankMore.alex.feitoza

# 3. Suba os containers
docker-compose up -d
Configuração JWT
As configurações estão no appsettings.json:

json
Copy
Edit
"JwtSettings": {
  "SecretKey": "minha-chave-super-secreta-para-bankmore-1234567890",
  "Issuer": "BankMore.Issuer",
  "Audience": "BankMore.Audience",
  "ExpiresInMinutes": 60
}
📚 Exemplos de Endpoints
POST /api/usuarios: Cria nova conta

POST /api/login: Retorna token JWT

POST /api/movimentacoes: Faz depósito ou saque

POST /api/transferencias: Realiza transferência

GET /api/saldo: Consulta saldo

🛣️ Roadmap
 Criar testes unitários para movimentações

 Implementar cobrança de tarifas com Kafka

 Adicionar cache (Redis)

 Melhorar logs e monitoramento

 Criar painel de observabilidade (futuro)

🤝 Contribuindo
Quer contribuir com o projeto?

⭐ Dê uma estrela no repositório

🔄 Fork e crie sua branch

🧪 Teste bem sua alteração

📩 Envie um Pull Request

🙋‍♂️ Ou comente na Issue de colaboração

🔓 Licença
Este projeto está licenciado sob os termos da MIT License.

✍️ Sobre o autor
Sou o Alex Nunes Feitoza, desenvolvedor .NET com mais de 14 anos de experiência. Apesar de toda a bagagem, sou novo nesse universo de exposição e compartilhamento online.

Admiro muito gigantes como Macoratti, mas me identifico demais com o jeito humilde, divertido e verdadeiro da Fernanda Kipper — que me inspirou a dar esse primeiro passo.

Se você também é como eu, está convidado a evoluir esse projeto junto!
🔗 LinkedIn
https://www.linkedin.com/in/alex-feitoza-6056a5237/
 
Você tem permissão para usar, copiar, modificar, mesclar, publicar, distribuir, sublicenciar e/ou vender cópias deste software, desde que mantenha o aviso de copyright e a permissão incluídos no software.

**Aviso:** O software é fornecido "no estado em que se encontra", sem garantia de qualquer tipo, expressa ou implícita.


## 🛣️ Roadmap

- [ ] Criar testes unitários para movimentações
- [ ] Implementar cobrança de tarifas com Kafka
- [ ] Adicionar cache (Redis)
- [ ] Melhorar logs e monitoramento
- [ ] Criar painel de observabilidade (futuro)
