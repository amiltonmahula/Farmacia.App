using Farmacia.App.DTO.Response;
using Farmacia.App.Service;

namespace Farmacia.App.ViewModel;
[QueryProperty(nameof(VendaId),"VendaId")]
public class VendaDetalheViewModel : BaseViewModel
{
    private readonly VendaService _vendaService;
    private VendaResponse? _venda;

    public VendaResponse? Venda
    {
        get => _venda;
        set => SetField(ref _venda, value);
    }
    
    private Guid _vendaId;

    public Guid VendaId
    {
        get => _vendaId; 
        set
        {
            if (SetField(ref _vendaId,value))
            {
                _ = CarregarVendaAsync(value);
            }
        }
    }
    private bool _estaCarregando;

    public bool EstaCarregando
    {
        get => _estaCarregando; 
        set => SetField(ref _estaCarregando, value);
    }
    private string _mensagem = String.Empty;

    public string Mensagem
    {
        get => _mensagem; 
        set => SetField(ref _mensagem, value);
    }

    public VendaDetalheViewModel(VendaService vendaService)
    {
        _vendaService = vendaService;
    }

    public async Task CarregarVendaAsync(Guid id)
    {
        try
        {
            EstaCarregando = true;
            Mensagem = string.Empty;

            Venda = await _vendaService.ObterVendaIdAsync(id);
            if (Venda is null)
                Mensagem = "Não foi possivel encontrar a venda";
        }
        catch (Exception e)
        {
            Mensagem = $"Erro ao carregar venda {e.Message}";
        }
        finally
        {
            EstaCarregando = false;
        }
    }
}