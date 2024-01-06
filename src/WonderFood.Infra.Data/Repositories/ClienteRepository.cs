using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WonderFood.Core.Entities;
using WonderFood.Core.Interfaces;
using WonderFood.Infra.Data.Context;

namespace WonderFood.Infra.Data.Repositories;

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

    public bool InserirCliente(Cliente cliente)
    {
        _context.Clientes.Add(cliente);
        return _context.SaveChanges() > 0;
    }

    public bool AtualizarCliente(Cliente cliente)
    {
        var clienteCadastrado = ObterClientePorIdComTracking(cliente.Id);
        if (clienteCadastrado == null) return false;
        _context.Clientes.Update(cliente);
        return _context.SaveChanges() > 0;
    }
    
    private Cliente ObterClientePorIdComTracking(Guid id)
    {
        return _context.Clientes.FirstOrDefault(x => x.Id == id);
    }
}