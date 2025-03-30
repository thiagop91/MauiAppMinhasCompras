using SQLite;
using System.ComponentModel;

namespace MauiAppMinhasCompras.Models
{
    public class Produto
    {
        string _descricao;
        DateTime _dataCadastro;


        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string Descricao
        {
            get => _descricao;
            set
            {
                if (value == null)
                {
                    throw new Exception("Por favor, preencha a descrição");
                }
                _descricao = value;
            }
        }
        public double Quantidade { get; set; }
        public double Preco { get; set; }
        public double Total { get => Quantidade * Preco; }

        public DateTime DataCadastro
        {
            get => _dataCadastro;
            set
            {
                _dataCadastro = value;
                OnPropertyChanged(nameof(DataCadastro));
            }

        }
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }

}
