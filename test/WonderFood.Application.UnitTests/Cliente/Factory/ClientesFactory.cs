using WonderFood.Application.Clientes.Commands.InserirCliente;
using WonderFood.Application.Clientes.Queries.ObterCliente;
using WonderFood.Domain.Dtos.Cliente;

namespace WonderFood.Application.UnitTests.Cliente.Factory;

public static class ClientesFactory
{
    const string Nome = "Felipe";
    const string Email = "felipe@gmail.com";
    const string Cpf = "330.605.720-70";
    private const string NumeroTelefone = "11987765454";

    public static InserirClienteCommand CriarInserirClienteCommand(
        string nome = Nome,
        string email = Email,
        string cpf = Cpf,
        string numeroTelefone = NumeroTelefone)
    {
        return new InserirClienteCommand(
            new InserirClienteInputDto()
            {
                Nome = nome,
                Email = email,
                Cpf = cpf,
                NumeroTelefone = numeroTelefone
            });
    }
    
    public static ObterClienteQuery CriarObterClienteQuery(
        Guid id = default)
    {
        return new ObterClienteQuery(id);
    }
    
    public static Domain.Entities.Cliente CriarCliente(
        Guid id = default,
        string nome = Nome,
        string email = Email,
        string cpf = Cpf,
        string numeroTelefone = NumeroTelefone)
    {
        return new Domain.Entities.Cliente(nome, email, cpf, numeroTelefone);
    }
}