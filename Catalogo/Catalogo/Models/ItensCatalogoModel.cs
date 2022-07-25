using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using Xamarin.Forms;

namespace Catalogo.Model
{
    public class ItensCatalogoModel : INotifyPropertyChanged
    {
        public ObservableCollection<Promocao> Promocoes { get; set; }
        public ObservableCollection<Produto> Produtos { get; set; }
        public ObservableCollection<Policy> Policy { get; set; }

        public ItensCatalogoModel(List<Produto> ListProduto, List<Promocao> ListPromocao)
        {

            Promocoes = new ObservableCollection<Promocao>();

            foreach (var item in ListPromocao)
            {
                Policy = new ObservableCollection<Policy>();

                foreach (var policy in item.Policies)
                {
                    Policy.Add(new Policy
                    {
                        Min = policy.Min,
                        Discount = policy.Discount
                    });
                }

                Policy.CollectionChanged += Policy_CollectionChanged;

                Produtos = new ObservableCollection<Produto>();

                foreach (var produto in ListProduto)
                {
                    if (produto.Category_id == item.Category_id)
                    {
                        Produtos.Add(new Produto
                        {
                            Id = produto.Id,
                            Name = produto.Name,
                            Description = produto.Description,
                            Photo = produto.Photo,
                            Price = produto.Price,
                            Category_id = produto.Category_id,
                            Policy = Policy
                        });

                        Produtos.CollectionChanged += Produtos_CollectionChanged;
                    }
                }

                Promocoes.Add(new Promocao
                {
                    Name = item.Name,
                    Category_id = item.Category_id,
                    Produtos = Produtos
                });
            }

            Promocoes.CollectionChanged += Promocoes_CollectionChanged;
        }

        private void Policy_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Policy)));
        }

        private void Promocoes_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Promocoes)));
        }

        private void Produtos_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(Produtos)));
        }

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

