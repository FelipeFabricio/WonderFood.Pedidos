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
    
    public Cliente ObterClientePorId(Guid id)
    {
        return _context.Clientes.AsNoTracking().FirstOrDefault(x => x.Id == id);
    }

    public Cliente InserirCliente(Cliente cliente)
    {
        _context.Clientes.Add(cliente);
        _context.SaveChanges();
        return cliente;
    }
}