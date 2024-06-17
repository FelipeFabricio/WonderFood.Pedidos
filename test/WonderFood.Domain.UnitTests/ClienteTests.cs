using FluentAssertions;
using WonderFood.Domain.Entities;
using Xunit;

namespace WonderFood.Domain.UnitTests;

public class ClienteTests
{
    [Theory]
    [InlineData("João", "joao@example.com", "11839555025", "11989154776")]
    [InlineData("Maria", "maria@example.com", "90906706025", "11989154776")]
    [Trait("Domain","Cliente")]
    public void Construtor_DeveCriarCliente_QuandoTodosOsDadosForemValidos(string nome, string email, string cpf, string numeroTelefone)
    {
        // Arrange e Act
        var cliente = new Cliente(nome, email, cpf, numeroTelefone);

        // Assert
        cliente.Nome.Should().Be(nome);
        cliente.Email.Should().Be(email);
        cliente.Cpf.Should().Be(cpf);
        cliente.Id.Should().NotBe(Guid.Empty);
    }
    
    [Theory]
    [InlineData("90906706029")]
    [InlineData("909067060299980")]
    [InlineData("999980")]
    [InlineData("99999999999")]
    [Trait("Domain","Cliente")]
    public void Construtor_DeveLancarArgumentException_QuandoCpfInvalido(string cpf)
    {
        // Arrange, Act & Assert
        Assert.Throws<ArgumentException>(() => new Cliente("Maria", "maria@email.com", cpf, "11989876556"));
    }
    
    [Theory]
    [InlineData("joao.example.com")]
    [InlineData("joao@example")]
    [InlineData("joao")]
    [Trait("Domain","Cliente")]
    public void ValidarEmail_DeveLancarArgumentException_QuandoEmailInvalido(string email)
    {
        // Arrange, Act & Assert
        Assert.Throws<ArgumentException>(() => new Cliente("Maria", email, "11839555025", "11989876556"));
    }
    
    [Theory]
    [InlineData("")]
    [InlineData("     ")]
    [InlineData("Maria999")]
    [InlineData("A")]
    [Trait("Domain","Cliente")]
    public void ValidarNome_DeveLancarArgumentException_QuandoNomeInvalido(string nome)
    {
        // Arrange, Act & Assert
        Assert.Throws<ArgumentException>(() => new Cliente(nome, "maria@email.com", "11839555025", "11989876556"));
    }
    
    [Theory]
    [InlineData("")]
    [InlineData(" ")]
    [InlineData("9999999999")]
    [InlineData("999999999999")]
    [Trait("Domain","Cliente")]
    public void ValidarNumeroTelefone_DeveLancarArgumentException_QuandoTelefoneInvalido(string numeroTelefone)
    {
        // Arrange, Act & Assert
        Assert.Throws<ArgumentException>(() => new Cliente("Felipe", "felipe@email.com", "11839555025", numeroTelefone));
    }
}