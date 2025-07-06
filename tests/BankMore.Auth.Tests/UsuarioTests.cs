using BankMore.Auth.Domain.ValueObjects;
using BankMore.Auth.Domain.Entities;
using FluentAssertions;

namespace BankMore.Auth.Tests.Domain
{
    public class UsuarioTests
    {
        [Fact]
        public void Deve_Criar_Usuario_Valido()
        {
            // Arrange
            var cpf = new CPF("111.444.777-35");
            var nome = "Alex Feitoza";
            var email = "alex@bankmore.com";
            var senhaHash = "HASH_SEGURA";

            // Act
            var usuario = Usuario.Criar(nome, cpf, (Email)email, senhaHash);

            // Assert
            usuario.Should().NotBeNull();
            usuario.Nome.Should().Be(nome);
            usuario.Cpf.Numero.Should().Be(cpf.Numero);
            usuario.Email.Should().Be(email);
            usuario.Ativo.Should().BeTrue();
            usuario.SenhaHash.Should().Be(senhaHash);
            usuario.CriadoEm.Should().BeBefore(DateTime.UtcNow.AddSeconds(1));
             
        }
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Nao_Deve_Criar_Usuario_Com_Nome_Invalido(string nomeInvalido)
        {
            var cpf = new CPF("111.444.777-35");

            Action act = () => Usuario.Criar(nomeInvalido, cpf, (Email)"email@dominio.com", "senha");

            act.Should().Throw<ArgumentException>()
                .WithMessage("Nome é obrigatório");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Nao_Deve_Criar_Usuario_Com_Email_Invalido(string emailInvalido)
        {
            var cpf = new CPF("111.444.777-35");

            Action act = () => Usuario.Criar("Nome", cpf, (Email)emailInvalido, "senha");

            act.Should().Throw<ArgumentException>()
                .WithMessage("Email é obrigatório");
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        public void Nao_Deve_Criar_Usuario_Com_Senha_Invalida(string senhaInvalida)
        {
            var cpf = new CPF("111.444.777-35");

            Action act = () => Usuario.Criar("Nome", cpf, (Email)"email@dominio.com", senhaInvalida);

            act.Should().Throw<ArgumentException>()
                .WithMessage("Senha é obrigatória");
        }
    }
}
