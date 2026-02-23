using Ecommerce.API.Models;
using Ecommerce.API.Interfaces;
using Ecommerce.API.Enums;

namespace Ecommerce.API.Services;

public class ClienteService : IClienteService
{
    private readonly List<Cliente> _clientes = new List<Cliente>();
    private int _nextId = 1;
    private readonly IHistoricoService _historicoService;
    public ClienteService(IHistoricoService historicoService)
    {
        _historicoService = historicoService;
    }
    public async Task<IEnumerable<Cliente>> ObterTodosClientesAsync()
    {
        return _clientes;
    }
    public async Task<Cliente>? ObterClientePorIdAsync(int id)
    {
        return _clientes.FirstOrDefault(c => c.Id == id);
    }
    public async Task<Cliente> AdicionarClienteAsync(Cliente cliente)
    {
        cliente.Id = _nextId++;
        _clientes.Add(cliente);
        await _historicoService.RegistrarHistoricoAsync(nameof(Cliente), cliente.Id, AcaoHistorico.Criacao, $"Cliente {cliente.Nome} criado.");
        return cliente;
    }
    public async Task<bool> AtualizarClienteAsync(int id, Cliente cliente)
    {
        var clienteExistente = _clientes.FirstOrDefault(c => c.Id == id);
        if (clienteExistente == null) return false;

        clienteExistente.Nome = cliente.Nome;
        clienteExistente.Cpf = cliente.Cpf;
        await _historicoService.RegistrarHistoricoAsync(nameof(Cliente), clienteExistente.Id, AcaoHistorico.Alteracao, $"Cliente {clienteExistente.Nome} atualizado.");
        return true;
    }
}
