using Farmacia.App.Service;

namespace Farmacia.App.ViewModel;

public class DashBoardViewModel : BaseViewModel
{
    private readonly ProdutoService _produtoService;
    private readonly VendaService _vendaService;

    public DashBoardViewModel(ProdutoService produtoService, VendaService vendaService)
    {
        _produtoService = produtoService;
        _vendaService = vendaService;
    }

    private int _totalVendas;

    public int TotalVendas
    {
        get => _totalVendas; 
        set => SetField(ref _totalVendas, value);
    }
    private int _totalProdutos;
    public int TotalProduto
    {
        get => _totalProdutos;
        set => SetField(ref _totalProdutos, value);
    }
    
    public async Task CarregarDadosAsync()
    {
        try
        {
            EstaCarregando = true;
            Mensagem = String.Empty;
            
            var produtos = await _produtoService.ObterProdutoAsync();
            TotalProduto = produtos.Count;
            
            var vendas = await _vendaService.ObterVendaAsync();
            TotalVendas = vendas.Count;
        }
        catch (Exception e)
        {
            Mensagem = $"Erro ao processar o DashBoard {e.Message}";
        }
        finally
        {
            EstaCarregando = false;
        }
    }

    private string _mensagem;
    private bool _estaCarregando;

    public string Mensagem 
    { 
        get => _mensagem; 
        set => SetField(ref _mensagem, value); 
    }

    public bool EstaCarregando
    {
        get => _estaCarregando;
        set => SetField(ref _estaCarregando, value);
    }
}