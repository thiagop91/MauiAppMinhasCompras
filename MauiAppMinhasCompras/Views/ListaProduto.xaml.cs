using MauiAppMinhasCompras.Models;
using System.Collections.ObjectModel;

namespace MauiAppMinhasCompras.Views;

public partial class ListaProduto : ContentPage
{

    ObservableCollection<Produto> lista = new ObservableCollection<Produto>();
    ObservableCollection<Produto> listaFiltrada = new ObservableCollection<Produto>();
    public ListaProduto()
	{
		InitializeComponent();
	
        lst_produtos.ItemsSource = lista;
        lst_produtos.ItemsSource = listaFiltrada;

    }

    protected async override void OnAppearing()
    {
        List<Produto> tmp = await App.Db.GetAll();

        tmp.ForEach(i => lista.Add(i));
    }

    private void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        try
        {
            Navigation.PushAsync(new Views.NovoProduto());

        }
        catch (Exception ex)
        {
            DisplayAlert("Ops", ex.Message, "OK");
        }

    }

    private async void txt_seacrh_TextChanged(object sender, TextChangedEventArgs e)
    {

        string q = e.NewTextValue;

        lista.Clear();
        listaFiltrada.Clear();

        List<Produto> tmp = await App.Db.Search(q);

        tmp.ForEach(i => lista.Add(i));
        tmp.ForEach(i => listaFiltrada.Add(i));

        string searchText = e.NewTextValue?.ToLower() ?? string.Empty;
        FiltrarProdutos(searchText);
    }

    private void FiltrarProdutos(string searchText)
    {
        listaFiltrada.Clear();

        if (string.IsNullOrWhiteSpace(searchText))
        {
            foreach (var item in lista)
                listaFiltrada.Add(item);
        }
        else
        {
            foreach (var item in lista.Where(p => p.Descricao.ToLower().Contains(searchText)))
                listaFiltrada.Add(item);
        }
    }


    private void ToolbarItem_Clicked_1(object sender, EventArgs e)
    {
        double soma = lista.Sum(i => i.Total);

        string msg = $"O total é {soma:C}";

        DisplayAlert("Total dos Produtos", msg, "OK");
    }

    private async void MenuItem_Clicked(object sender, EventArgs e)
    {
        {
            MenuItem selecionado = sender as MenuItem;

            Produto p = selecionado.BindingContext as Produto;

            bool confirm = await DisplayAlert(
                "Tem Certeza?", $"Remover {p.Descricao}?", "Sim", "Não");

            if (confirm)
            {
                await App.Db.Delete(p.Id);
                lista.Remove(p);
            }
        }
    }
}