using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Catalogo.Model
{
    public class Promocao : INotifyPropertyChanged
    {
        public ObservableCollection<Policy> Policy { get; set; }
        public ObservableCollection<Produto> Produtos { get; set; }

        public string Name { get; set; }

        public int Category_id { get; set; }

        public List<Policy> Policies { get; set; }

        public Promocao()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

