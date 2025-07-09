# 💰 BankMore - Sistema de Conta Corrente


**BankMore** é um projeto backend baseado em microsserviços, construído com .NET 8 e arquitetura DDD + CQRS. Ele simula uma plataforma bancária moderna com foco em segurança, escalabilidade e boas práticas de engenharia de software.

> Este projeto nasceu de um desafio técnico, mas estou abrindo-o para estudo, colaboração e crescimento profissional de toda a comunidade.

---

## 🚀 Funcionalidades Implementadas

- ✅ Cadastro de conta corrente (com CPF e senha protegida por salt/hash)
- ✅ Autenticação com JWT
- ✅ Realização de movimentações (depósitos e saques)
- ✅ Transferências entre contas (com idempotência)
- ✅ Consulta de saldo em tempo real
- ✅ API segura com autenticação JWT

---

## 🧠 Tecnologias e Práticas Usadas

- .NET 8 com MediatR e Dapper
- Arquitetura **DDD (Domain Driven Design)**
- **CQRS** (Command Query Responsibility Segregation)
- Swagger com documentação
- Autenticação via **JWT**
- Camada de persistência desacoplada por interfaces
- Repositórios separados para **MySQL** e **SQL Server**
- Configuração de ambiente com Docker e Docker Compose

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
