using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Farmacia.App.ViewModel;

namespace Farmacia.App.Page;

public partial class DashBoardPage : ContentPage
{
    private readonly DashBoardViewModel _dashBoardViewModel;
    public DashBoardPage(DashBoardViewModel dashBoardViewModel)
    {
        InitializeComponent();
        _dashBoardViewModel = dashBoardViewModel;
        BindingContext = _dashBoardViewModel;
    }
    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await InicializarAsync();
    }
    
    public async Task InicializarAsync()
    {
        await _dashBoardViewModel.CarregarDadosAsync();
    }
}