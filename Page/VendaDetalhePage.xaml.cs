using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Farmacia.App.ViewModel;

namespace Farmacia.App.Page;

public partial class VendaDetalhePage : ContentPage
{
    public VendaDetalhePage(VendaDetalheViewModel detalhe)
    {
        InitializeComponent();
        BindingContext = detalhe;
        
        System.Diagnostics.Debug.WriteLine("VENDA DETALHE PAGE FOI INSTANCIADA");
    }
}