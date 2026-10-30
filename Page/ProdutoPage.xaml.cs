using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Farmacia.App.ViewModel;

namespace Farmacia.App.Page;

public partial class ProdutoPage : ContentPage
{
    public ProdutoPage(ProdutosViewModel viewModel)
    {
        InitializeComponent();

        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await ((ProdutosViewModel)BindingContext).CarregarProdutoAsync();
    }
}