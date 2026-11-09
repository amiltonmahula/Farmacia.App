using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farmacia.App.Page.AdminPage;

public partial class AdminMainPage : ContentPage
{
    private readonly IServiceProvider _provider;
    public AdminMainPage(IServiceProvider provider)
    {
        _provider = provider;
        InitializeComponent();
        AbrirInicio();
    }
    
    private void MostrarPagina(ContentPage pagina)
    {
        var conteudo = pagina.Content;
        if (conteudo is null)
            return;
        conteudo.BindingContext = pagina.BindingContext;
        pagina.Content = null;
        ContentHost.Content = conteudo;
    }

    private async void AbrirInicio()
    {
        var pagina = _provider.GetRequiredService<DashBoardPage>();
        MostrarPagina(pagina);
        await pagina.InicializarAsync();
    }

    private void DashBoard_OnClicked(object? sender, EventArgs e)
    {
        AbrirInicio();
    }

    private async void Vendas_OnClicked(object? sender, EventArgs e)
    {
        var pagina = _provider.GetRequiredService<HistoricoPage>();
        MostrarPagina(pagina);
        await pagina.InicializarAsync();
    }

    private async void Produtos_OnClicked(object? sender, EventArgs e)
    {
        var pagina = _provider.GetRequiredService<ProdutoPage>();
        MostrarPagina(pagina);
        await pagina.InicializarAsync();
    }

    private async void Funcionario_OnClicked(object? sender, EventArgs e)
    {
        var pagina = _provider.GetRequiredService<FuncionarioPage>();
        MostrarPagina(pagina);
        await pagina.Inicializar();
    }

    private async void Relatorios_OnClicked(object? sender, EventArgs e)
    {
        var pagina = _provider.GetRequiredService<RelatorioPage>();
        MostrarPagina(pagina);
        await pagina.InicializarAsync();
    }
}