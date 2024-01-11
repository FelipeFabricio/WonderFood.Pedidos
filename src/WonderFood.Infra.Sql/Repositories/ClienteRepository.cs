using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WonderFood.Core.Entities;
using WonderFood.Core.Interfaces;
using WonderFood.Core.Interfaces.Repository;
using WonderFood.Infra.Sql.Context;

namespace WonderFood.Infra.Sql.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly WonderFoodContext _context;
    private readonly IMapper _mapper;
    
    public ClienteRepository(WonderFoodContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public IEnumerable<Cliente> ObterTodosClientes()
    {
        return _context.Clientes.AsNoTracking().AsEnumerable();
    }

    public Cliente ObterClientePorId(Guid id)
    {
        return _context.Clientes.AsNoTracking().FirstOrDefault(x => x.Id == id);
    }

    public void InserirCliente(Cliente cliente)
    {
        _context.Clientes.Add(cliente);
        _context.SaveChanges();
    }

    public void AtualizarCliente(Cliente cliente)
    {
        var clienteCadastrado = ObterClientePorId(cliente.Id);
        if (clienteCadastrado == null) throw new Exception("Cliente não encontrado.");
        _context.Clientes.Update(cliente);
        _context.SaveChanges();
    }
}