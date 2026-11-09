using System.Collections.ObjectModel;
using System.Windows.Input;
using Farmacia.App.DTO.Response;
using Farmacia.App.Models;
using Farmacia.App.Page;
using Farmacia.App.Service;

namespace Farmacia.App.ViewModel;

public class HistoricoViewModel : BaseViewModel
{
    private readonly VendaService _vendaService;
    public ObservableCollection<VendaResponse> Vendas { get; set; } = new();
    
    public ICommand AbrirDetalhesCommand { get; }
    public HistoricoViewModel(VendaService vendaService)
    {
        _vendaService = vendaService;
        AbrirDetalhesCommand = new Command<VendaResponse>(async (vendas) => AbrirDetalhesAsync(vendas));
    }

    private async Task AbrirDetalhesAsync(VendaResponse vendas)
    {
        if(vendas == null)
            return;

        try
        {
            await Shell.Current.GoToAsync(nameof(VendaDetalhePage),
                new Dictionary<string, object>
                {
                    { "VendaId", vendas.Id }
                });
        }
        catch (Exception e)
        {
            await Shell.Current.DisplayAlert("Erro de navegação", e.Message, "OK");
        }
        finally
        {
            EstaCarregando = false;
        }
        
        
    }

    public async Task CarregarVendaAsync()
    {
        try
        {
            EstaCarregando = true;
            Mensagem = String.Empty;

            var vendas = await _vendaService.ObterVendaAsync();

            //vendas.Clear();
            
            foreach (var venda in vendas)
            {
                Vendas.Add(venda); 
            }
        }
        catch (Exception e)
        {
            Mensagem = $"Erro ao carregar vendas {e.Message}";
        }
        finally
        {
            EstaCarregando = false;
        }
    }

    private string _mensagem;

    public string Mensagem
    {
        get => _mensagem;
        set => SetField(ref _mensagem, value);
    }

    private bool _estaCarregando;
    public bool EstaCarregando { get => _estaCarregando; 
        set => SetField(ref _estaCarregando, value); }
}