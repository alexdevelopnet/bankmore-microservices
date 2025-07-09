# Clone o projeto
git clone https://github.com/alexdevelopnet/BankMore.alex.feitoza.git
cd BankMore.alex.feitoza

# Suba os containers com banco de dados e dependências
docker-compose up --build


Abra no navegador:
📎 http://localhost:5000/swagger

🔐 Segurança
Endpoints protegidos via JWT

Sem tráfego de dados sensíveis entre microsserviços

Controle de acesso e validade de token rigoroso

Senhas com hash e salt

🧪 Testes
✅ Testes de unidade (camada de domínio e aplicação)

✅ Testes de integração (repositórios e serviços)

Cobertura validando as regras de negócio essenciais

📄 Exemplo de Token JWT
Após autenticação bem-sucedida, um token como este é retornado:

json
Copy
Edit
{
  "id": "3f9a5cdd-b1fa-4e03-9d2a-b00000000001",
  "numero": "12345",
  "exp": 9999999999,
  "iss": "BankMore.Issuer",
  "aud": "BankMore.Audience"
}
Copie o token e use no botão Authorize 🔒 da interface Swagger para autenticar.

📤 Próximas Evoluções
Microsserviço de tarifas com consumo Kafka

Produção e consumo de eventos com KafkaFlow

Métricas com Prometheus e OpenTelemetry

Cache Redis para consultas de saldo

Publicação CI/CD em Azure DevOps ou GitHub Actions

👨‍💻 Autor
Desenvolvido por Alex Nunes Feitoza
Arquiteto e desenvolvedor especializado em .NET, sistemas distribuídos, mensageria e boas práticas.

🔗 LinkedIn
https://www.linkedin.com/in/alex-feitoza-6056a5237/
