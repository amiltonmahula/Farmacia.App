using System.Collections.ObjectModel;
using Farmacia.App.DTO.Request;
using Farmacia.App.Models;
using Farmacia.App.Service;

namespace Farmacia.App.ViewModel;

public class VendaViewModel : BaseViewModel
{
    private readonly VendaService _vendaService;
    private readonly ProdutoService _produtoService;

    public ObservableCollection<Produto> Produtos { get; set; } = new();
    public ObservableCollection<ItemCarrinho> Carrinhos { get; set; } = new();
    
    public VendaViewModel(VendaService vendaService, ProdutoService produtoService)
    {
        _vendaService = vendaService;
        _produtoService = produtoService;
    }

    private string _pesquisa;

    public string Pesquisa
    {
        get => _pesquisa; 
        set 
        {
            if (SetField(ref _pesquisa, value))
                FiltrarProdutos();
        }
    }
    
    private decimal _total;

    public decimal Total
    {
        get => _total; 
        set => SetField(ref _total, value);
    }
    
    private string _mensagem = String.Empty;

    public string Mensagem
    {
        get => _mensagem; 
        set => SetField(ref _mensagem, value);
    }
    
    private bool _estaCarregando;

    public bool EstaCarregando
    {
        get => _estaCarregando; 
        set => SetField(ref _estaCarregando, value);
    }

    private List<Produto> _todosProdutos = new();

    public async Task CarregarProdutoAsync()
    {
        try
        {
            EstaCarregando = true;
            Mensagem = string.Empty;

            var resultado = await _produtoService.ObterProdutoAsync();

            _todosProdutos = resultado.ToList();
            FiltrarProdutos();
        }
        catch (Exception e)
        {
            Mensagem = $"Erro ao Carregar Produtos: {e.Message}";
        } 
        finally
        {
            EstaCarregando = false;
        }
    }

    private void FiltrarProdutos()
    {
        Produtos.Clear();
        var encontrados = _todosProdutos
            .Where(p => string.IsNullOrWhiteSpace(Pesquisa) ||
                        p.Nome.Contains(Pesquisa, StringComparison.OrdinalIgnoreCase));

        foreach (var produto in encontrados)
            Produtos.Add(produto);
    }

    public void AdicionarNoCarrinho(Produto produto)
    {
        if (produto.Quantidade <= 0)
        {
            Mensagem = "Este produto não tem estoque";
        }

        var existente = Carrinhos.FirstOrDefault(item => item.ProdutoId == produto.Id);
        if (existente is not null)
        {
            if (existente.Quantidade >= produto.Quantidade)
            {
                Mensagem = "Quantidade superior ao Stock disponível";
                return;
            }
            existente.Quantidade++;
        }
        else
        {
            Carrinhos.Add(new ItemCarrinho
            {
                ProdutoId = produto.Id,
                Nome = produto.Nome,
                PrecoUnitario = produto.Preco,
                Quantidade = 1
            });
        }

        AtualizarTotal();
        Mensagem = String.Empty;
    }

    public void RemoverDoCarrinho(ItemCarrinho item)
    {
        Carrinhos.Remove(item);
        AtualizarTotal();
    }

    public void AtualizarQuantidade(ItemCarrinho item, int quantidade)
    {
        if (quantidade < 1)
        {
            RemoverDoCarrinho(item);
            return;
        }

        var produto = _todosProdutos.FirstOrDefault(p => p.Id == item.ProdutoId);

        if (produto is null || quantidade > produto.Quantidade)
        {
            Mensagem = "Quantidade superior ao stock";
            return;
        }

        item.Quantidade = quantidade;
        
        //Notifica a interface sobre a alteração do item
        var indice = Carrinhos.IndexOf(item);
        if (indice >= 0)
        {
            Carrinhos.RemoveAt(indice);
            Carrinhos.Insert(indice,item);
        }
        AtualizarTotal();
        Mensagem = String.Empty;
    }
    private void AtualizarTotal()
    {
        Total = Carrinhos.Sum(item => item.SubTotal);
    }

    public void AumentarQuantidade(ItemCarrinho item)
    {
        var produto = _todosProdutos.FirstOrDefault(p => p.Id == item.ProdutoId);

        if (produto is null)
            return;
        if (item.Quantidade >= produto.Quantidade)
        {
            Mensagem = "Stock insuficiente";
            return;
        }
        item.Quantidade++;
        AtualizarTotal();
        Mensagem = String.Empty;
    }
    
    public void DiminuirQuantidade(ItemCarrinho item)
    {
        if (item.Quantidade <= 1)
        {
            RemoverDoCarrinho(item);
            return;
        }

        item.Quantidade--;
        AtualizarTotal();
    }
    
    public async Task<bool> ConfirmarVendaAsync()
    {
        if (!Carrinhos.Any())
        {
            Mensagem = "O carrinho está vázio!";
            return false;
        }

        try
        {
            EstaCarregando = true;
            Mensagem = string.Empty;

            var requerido = new VendaRequest(
                Carrinhos.Select(item => new ItensVendaRequest(item.ProdutoId,
                    item.Quantidade)).ToList()
            );

            var resultado = await _vendaService.CriarVendaAsync(requerido);
            if (resultado is null)
            {
                Mensagem = "Não foi possível confirmar a venda.";
                return false;
            }

            Carrinhos.Clear();
            AtualizarTotal();
            Mensagem = "Venda realizada com sucesso";
            return true;
        }
        catch (Exception e)
        {
            Mensagem = $"Erro ao realizar venda{e.Message}";
            return false;
        }
        finally
        {
            EstaCarregando = false;
        }
    }
}

