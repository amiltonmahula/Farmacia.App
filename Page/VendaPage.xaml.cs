using Farmacia.App.Models;
using Farmacia.App.ViewModel;

namespace Farmacia.App.Page;

public partial class VendaPage : ContentPage
{
    private readonly VendaViewModel _viewModel;
    public VendaPage(VendaViewModel vendaViewModel)
    {
        InitializeComponent();
        _viewModel = vendaViewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await ((VendaViewModel)BindingContext).CarregarProdutoAsync();
    }

    private void Pesquisa_OnSearchButtonPressed(object? sender, EventArgs e)
    {
        throw new NotImplementedException();
    }

    private void AdicionarProduto_OnClicked(object? sender, EventArgs e)
    {
        if(sender is Button{CommandParameter: Produto produto})
            _viewModel.AdicionarNoCarrinho(produto);
    }

    private void Remover_OnClicked(object? sender, EventArgs e)
    {
        if(sender is Button{CommandParameter: ItemCarrinho item})
            _viewModel.RemoverDoCarrinho(item);
    }

    private async void ConfirmarVenda_OnClicked(object? sender, EventArgs e)
    {
        bool sucesso = await _viewModel.ConfirmarVendaAsync();

        await DisplayAlertAsync(sucesso ? "Sucesso" : "Atenção", _viewModel.Mensagem, "OK");
    }

    private void DiminuirQuantidade_OnClicked(object? sender, EventArgs e)
    {
        if(sender is Button{CommandParameter: ItemCarrinho item})
            _viewModel.DiminuirQuantidade(item);
    }

    private void AumentarQuantidade_OnClicked(object? sender, EventArgs e)
    {
        if(sender is Button{CommandParameter: ItemCarrinho item})
            _viewModel.AumentarQuantidade(item);
    }
    
    public async Task InicializarAsync()
    {
        await _viewModel.CarregarProdutoAsync();
    }
}