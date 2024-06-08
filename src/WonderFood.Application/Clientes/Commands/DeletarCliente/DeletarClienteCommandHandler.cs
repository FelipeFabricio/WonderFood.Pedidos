using MediatR;
using WonderFood.Application.Common.Interfaces;

namespace WonderFood.Application.Clientes.Commands.DeletarCliente;

public class DeletarClienteCommandHandler(IClienteRepository clienteRepository, IUnitOfWork unitOfWork) 
    : IRequestHandler<DeletarClienteCommand, Unit>
{
    public async Task<Unit> Handle(DeletarClienteCommand request, CancellationToken cancellationToken)
    {
        var numeroTelefoneFormatado = request.DadosCliente.NumeroTelefone.Replace(" ", "").Replace("(", "").Replace(")", "").Replace("-", "");
        var cliente = await clienteRepository.ObterClientePorNumeroTelefone(numeroTelefoneFormatado);
        if(cliente is null)
            throw new ArgumentException("Cliente não encontrado");
        
        await clienteRepository.DeletarCliente(cliente.Id);
        await unitOfWork.CommitChangesAsync();
        return Unit.Value;
    }
}