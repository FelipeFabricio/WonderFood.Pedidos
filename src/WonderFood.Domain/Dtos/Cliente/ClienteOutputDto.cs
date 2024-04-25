namespace WonderFood.Domain.Dtos.Cliente;

public class ClienteOutputDto
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Email { get; set; }
    public string Cpf { get; set; }
}