using System.Collections.ObjectModel;

using Farmacia.App.Models;
using Farmacia.App.Service;

namespace Farmacia.App.ViewModel;

public class ProdutosViewModel : BaseViewModel
{
    private readonly ProdutoService _service;

    private List<Produto> _todosProduto = new();

    public ObservableCollection<Produto> Produtos { get; } = new();

    private string _pesquisa = string.Empty;

    public string Pesquisa
    {
        get => _pesquisa ;
        set
        {
            if (SetField(ref _pesquisa, value))
                FiltrarProdutos();
        }
    }

    private bool _estaCarregando;
    public bool EstaCarregando
    {
        get => _estaCarregando ;
        set => SetField(ref _estaCarregando, value);
    }

    private string _mensagemErro;
    public string MensagemErro
    {
        get => _mensagemErro ;
        set => SetField(ref _mensagemErro, value);
    }
    private void FiltrarProdutos()
    {
        Produtos.Clear();
        
        var termo = Pesquisa.Trim();

        var filtradas = _todosProduto
            .Where(p => string.IsNullOrWhiteSpace(termo) || p.Nome.Contains(termo,
                                                             StringComparison.OrdinalIgnoreCase)
                                                         ||
                                                         p.CodigoDeBarra.Contains(termo,
                                                             StringComparison.OrdinalIgnoreCase)).ToList();

        foreach (var produto in filtradas)
            Produtos.Add(produto);
    }
    //Construtor da classe
    public ProdutosViewModel(ProdutoService service)
    {
        _service = service;
    }

    public async Task CarregarProdutoAsync()
    {
        try
        {
            EstaCarregando = true;
            MensagemErro = string.Empty;

            var resultado = await _service.ObterProdutoAsync();

            _todosProduto = resultado.ToList();
            
            FiltrarProdutos();
            
            if (Produtos.Count == 0)
            {
                MensagemErro = "API devolveu, mas a interface não pegou";
            }
            else
                MensagemErro = $"Produtos Carregados{Produtos.Count}";
        }
        catch (Exception e)
        {
            MensagemErro = $"Erro ao carregar produto; {e.Message}";
        }
        finally
        {
            EstaCarregando = false;
        }
    }
}