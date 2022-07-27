using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Xamarin.Forms;

namespace Catalogo.Model
{
    public class Produto : INotifyPropertyChanged
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        public double Price { get; set; }
        public int? Category_id { get; set; }
        public double Discount { get; set; }
        public ObservableCollection<Promocao> Promocao { get; set; }
        public ObservableCollection<Policy> Policy { get; set; }
        public int Qtde;
        public ICommand ComandPlus { get; set; }
        public ICommand ComandLess { get; set; }
        public EventHandler OnQtdeChanged;
        public string PrecoFormatado { get; set; }

        public Produto()
        {
            ComandPlus = new Command(IncrementarQtde);
            ComandLess = new Command(DecrementarQtde);
        }

        void IncrementarQtde()
        {
            Qtde++;

            if (Policy != null)
            {
                foreach (var item in Policy)
                {
                    if (item.Min == Qtde)
                    {
                        Discount = item.Discount;
                    }
                }
            }

            OnQtdeChanged.Invoke(this, null);

            OnPropertyChanged(nameof(InfoQtde));
            OnPropertyChanged(nameof(InfoDesconto));
        }

        void DecrementarQtde()
        {
            if (Qtde > 0)
                Qtde--;

            if (Policy != null)
            {
                foreach (var item in Policy)
                {
                    if (item.Min == Qtde)
                    {
                        Discount = item.Discount;
                    }
                }
            }

            if (Qtde == 0)
                Discount = 0;

            OnQtdeChanged.Invoke(this, null);

            OnPropertyChanged(nameof(InfoQtde));
            OnPropertyChanged(nameof(InfoDesconto));
        }

        public string InfoDesconto => $"↓ {Discount}% ";
        public string InfoQtde => $"{Qtde} UN";

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

