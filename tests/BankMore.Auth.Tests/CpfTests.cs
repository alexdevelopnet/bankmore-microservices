using BankMore.Auth.Domain.ValueObjects;
using FluentAssertions;

namespace BankMore.Auth.Tests.Domain;

public class CpfTests
{
    [Theory]
    [InlineData("123.456.789-09")]
    [InlineData("111.444.777-35")]
    public void Deve_Criar_Cpf_Valido(string cpf)
    {
        var vo = new CPF(cpf);
        vo.Numero.Should().Be(cpf.Replace(".", "").Replace("-", ""));
    }

    [Theory]
    [InlineData("000.000.000-00")]
    [InlineData("abc.def.ghi-jk")]
    [InlineData("")]
    public void Deve_Lancar_Excecao_Cpf_Invalido(string cpfInvalido)
    {
        Action acao = () => new CPF(cpfInvalido);
        acao.Should().Throw<ArgumentException>().WithMessage("*inválido*");
    }
}