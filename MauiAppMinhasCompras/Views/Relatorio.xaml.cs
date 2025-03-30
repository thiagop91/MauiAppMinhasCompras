using MauiAppMinhasCompras.Models;

namespace MauiAppMinhasCompras.Views;

public partial class Relatorio : ContentPage
{
	public Relatorio()
	{
        InitializeComponent();
	}

    private async void ToolbarItem_Clicked(object sender, EventArgs e)
    {
        await Navigation.PopAsync();
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        {
            try
            {
                DateTime dataInicial = dp_dataInicial.Date;
                DateTime dataFinal = dp_dataFinal.Date;

                if (dataInicial > dataFinal)
                {
                    await DisplayAlert("Erro", "A data inicial não pode ser maior que a data final.", "OK");
                    return;
                }

                // Buscar produtos no banco de dados filtrados pela data
                List<Produto> produtosFiltrados = await App.Db.GetAll();
                var produtosNoPeriodo = produtosFiltrados.Where(p => p.DataCadastro >= dataInicial && p.DataCadastro <= dataFinal).ToList();

                lst_relatorioProdutos.ItemsSource = produtosNoPeriodo;
            }
            catch (Exception ex)
            {
                await DisplayAlert("Ops", ex.Message, "OK");
            }
        }
    }
}