using Bogus;
using Bogus.Extensions.Brazil;
using WonderFood.Domain.Dtos.Cliente;
using Xunit;

namespace WonderFood.WebApi.UnitTests.Fixtures;

[CollectionDefinition(nameof(ClienteFixtureCollection))]
public class ClienteFixtureCollection : ICollectionFixture<ClienteFixture>
{
}

public class ClienteFixture
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
}