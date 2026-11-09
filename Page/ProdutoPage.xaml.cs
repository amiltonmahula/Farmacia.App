using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Farmacia.App.ViewModel;

namespace Farmacia.App.Page;

public partial class ProdutoPage : ContentPage
{
    private readonly ProdutosViewModel _viewModel;
    public ProdutoPage(ProdutosViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await ((ProdutosViewModel)BindingContext).CarregarProdutoAsync();
    }

    public async Task InicializarAsync()
    {
        await _viewModel.CarregarProdutoAsync();
    }

    private async void Cadastrar_OnClicked(object? sender, EventArgs e)
    {
        int quantidade = Convert.ToInt32(TxtQuantidade);
        decimal preco = Convert.ToDecimal(TxtPreco);
       
       
    }
}