using Bogus;
using Bogus.Extensions.Brazil;
using WonderFood.Core.Dtos;
using WonderFood.Core.Dtos.Cliente;
using Xunit;

namespace WonderFood.WebApi.UnitTests.Fixtures;

[CollectionDefinition(nameof(ClienteFixtureCollection))]
public class ClienteFixtureCollection : ICollectionFixture<ClienteFixture>
{
}

public class ClienteFixture : IDisposable
{
    public ClienteOutputDto GerarClienteOutputDtoValido()
    {
        return new Faker<ClienteOutputDto>("pt_BR")
            .RuleFor(c => c.Id, f => f.Random.Guid())
            .RuleFor(c => c.Cpf, f => f.Person.Cpf())
            .RuleFor(c => c.Email, f => f.Person.Email)
            .RuleFor(c => c.Nome, f => f.Person.FullName)
            .Generate();
    }
    
    public InserirClienteInputDto GerarInserirClienteInputDtoValido()
    {
        return new Faker<InserirClienteInputDto>("pt_BR")
            .RuleFor(c => c.Cpf, f => f.Person.Cpf())
            .RuleFor(c => c.Email, f => f.Person.Email)
            .RuleFor(c => c.Nome, f => f.Person.FullName)
            .Generate();
    }
    
    public AtualizarClienteInputDto GerarAtualizarClienteInputDtoValido()
    {
        return new Faker<AtualizarClienteInputDto>("pt_BR")
            .RuleFor(c => c.Id, f => f.Random.Guid())
            .RuleFor(c => c.Cpf, f => f.Person.Cpf())
            .RuleFor(c => c.Email, f => f.Person.Email)
            .RuleFor(c => c.Nome, f => f.Person.FullName)
            .Generate();
    }

    public IEnumerable<ClienteOutputDto> GerarListaClienteOutputDtoValido()
    {
        var cliente = new Faker<ClienteOutputDto>("pt_BR")
            .RuleFor(c => c.Id, f => f.Random.Guid())
            .RuleFor(c => c.Cpf, f => f.Person.Cpf())
            .RuleFor(c => c.Email, f => f.Person.Email)
            .RuleFor(c => c.Nome, f => f.Person.FullName);
        return cliente.Generate(3);
    }

    public void Dispose()
    {
    }
}