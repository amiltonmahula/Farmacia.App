using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Farmacia.App.Service;

namespace Farmacia.App.Page;

public partial class MainPage : ContentPage
{
    private readonly IServiceProvider _provider;
    private readonly UsuarioService _usuarioService;

    public MainPage(IServiceProvider provider, UsuarioService usuarioService)
    {
        _provider = provider;
        _usuarioService = usuarioService;
        InitializeComponent();
        AbrirInicio();
    }

    private void Inicio_OnClicked(object? sender, EventArgs e)
    {
        AbrirInicio();
    }

    private async void AbrirInicio()
    {
        var pagina = _provider.GetRequiredService<DashBoardPage>();
        MostrarPagina(pagina);
        await pagina.InicializarAsync();
    }

    //Serve para alterar entre páginas
    private void MostrarPagina(ContentPage pagina)
    {
        var conteudo = pagina.Content;
        if (conteudo == null)
            return;
        conteudo.BindingContext = pagina.BindingContext;
        pagina.Content = null;
        ContentHost.Content = conteudo;
    }

    private async void Venda_OnClicked(object? sender, EventArgs e)
    {
        try
        {
            var pagina = _provider.GetRequiredService<VendaPage>();
            MostrarPagina(pagina);
            await pagina.InicializarAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Erro",$"Erro ao carregar histórico{ex.Message}","Ok");
        }
        
    }

    private async void Historico_OnClicked(object? sender, EventArgs e)
    {
        try
        {
            var pagina = _provider.GetRequiredService<HistoricoPage>();
            
            MostrarPagina(pagina);

            await pagina.InicializarAsync();
        }
        catch (Exception ex)
        {
            await DisplayAlertAsync("Erro",$"Erro ao carregar histórico{ex.Message}","Ok");
        }
    }

    private async void Produto_OnClicked(object? sender, EventArgs e)
    {
        try
        {
            var pagina = _provider.GetRequiredService<ProdutoPage>();
                    MostrarPagina(pagina);
                    await pagina.InicializarAsync();
        }
        catch (Exception ex)
        {
             await DisplayAlertAsync("Erro",$"Erro ao carregar Produto{ex.Message}","Ok");
        }
    }

    private async void Logout_OnClicked(object? sender, EventArgs e)
    {
        bool Confirmar = await DisplayAlertAsync("Terminar sessão", "Tens a certeza?", "Sim", "Não");
        if(!Confirmar)
            return;
        await _usuarioService.LogOut();
        await Shell.Current.GoToAsync("//LoginPage");
    }
}