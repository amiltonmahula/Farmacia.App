using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Farmacia.App.Models;

public class ItemCarrinho : INotifyPropertyChanged
{
    public Guid ProdutoId { get; set; }

    public string Nome { get; set; }
    
    public decimal PrecoUnitario { get; set; }
    private int _quantidade = 1;
    public int Quantidade
    {
        get => _quantidade;
        set
        {
            if (_quantidade == value)
                return;
            _quantidade = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(SubTotal));
        }
    }

    public decimal SubTotal => PrecoUnitario * Quantidade;
    
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected bool SetField<T>(ref T field, T value, [CallerMemberName] string? propertyName = null)
    {
        if (EqualityComparer<T>.Default.Equals(field, value)) return false;
        field = value;
        OnPropertyChanged(propertyName);
        return true;
    }
}