using BankMore.Auth.Domain.ValueObjects;
using FluentAssertions;

namespace BankMore.Auth.Tests
{
    public class EmailTests
    {

        [Theory]
        [InlineData("teste@email.com")]
        [InlineData("usuario123@dominio.org")]
        public void Deve_Criar_Email_Valido(string endereco)
        {
            var email = new Email(endereco);
            email.Endereco.Should().Be(endereco);
        }

        [Theory]
        [InlineData("invalido")]
        [InlineData("usuario@")]
        [InlineData("")]
        [InlineData(" ")]
        public void Nao_Deve_Criar_Email_Invalido(string endereco)
        {
            Action act = () => new Email(endereco);
            act.Should().Throw<ArgumentException>();
        }
    }
}