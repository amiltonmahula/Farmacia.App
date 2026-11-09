using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Farmacia.App.ViewModel;

namespace Farmacia.App.Page;

public partial class RelatorioPage : ContentPage
{
    private readonly ProdutosViewModel _produtosViewModel;
    //private readonly VendaViewModel _vendaViewModel;
    public RelatorioPage(ProdutosViewModel produtosViewModel, VendaViewModel vendaViewModel)
    {
        _produtosViewModel = produtosViewModel;
        //_vendaViewModel = vendaViewModel;
        InitializeComponent();

        BindingContext = _produtosViewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await ((ProdutosViewModel)BindingContext).CarregarProdutoAsync();
    }

    public async Task InicializarAsync()
    {
        await _produtosViewModel.CarregarProdutoAsync();
    }
}