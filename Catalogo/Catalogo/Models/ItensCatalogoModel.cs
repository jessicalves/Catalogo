using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using Catalogo.Models;
using Xamarin.Forms;

namespace Catalogo.Model
{
    public class ItensCatalogoModel : INotifyPropertyChanged
    {
        public ObservableCollection<Promocao> Promocoes { get; set; }
        public ObservableCollection<Produto> Produtos { get; set; }
        public ObservableCollection<Produto> ProdutosSemPromocao { get; set; }
        public ObservableCollection<Policy> Policy { get; set; }

        public ItensCatalogoModel(List<Produto> ListProduto, List<Promocao> ListPromocao, Button btnComprar)
        {
            try
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

                    Produtos = new ObservableCollection<Produto>();
                    ProdutosSemPromocao = new ObservableCollection<Produto>();

                    foreach (var produto in ListProduto)
                    {
                        var naoTemPromocao = (from l in ListPromocao where l.Category_id == produto.Category_id select l).FirstOrDefault();

                        if (naoTemPromocao == null)
                        {
                            Produto prod = new Produto()
                            {
                                Id = produto.Id,
                                Name = produto.Name,
                                Description = produto.Description,
                                Photo = produto.Photo,
                                Price = produto.Price,
                                Category_id = produto.Category_id
                            };

                            prod.OnQtdeChanged += (sender, e) =>
                            {
                                double total = 0;
                                foreach (var promo in Promocoes)
                                {
                                    foreach (var it in promo.Produtos)
                                    {
                                        total += it.Qtde * it.Price;
                                    }
                                }
                                btnComprar.Text = $"COMPRAR ➙ R$ {total:C2}";
                            };

                            ProdutosSemPromocao.Add(prod);
                        }

                        if (produto.Category_id == item.Category_id)
                        {
                            Produto prod = new Produto()
                            {
                                Id = produto.Id,
                                Name = produto.Name,
                                Description = produto.Description,
                                Photo = produto.Photo,
                                Price = produto.Price,
                                Category_id = produto.Category_id,
                                Policy = Policy
                            };

                            prod.OnQtdeChanged += (sender, e) =>
                            {
                                double total = 0;
                                foreach (var promo in Promocoes)
                                {
                                    foreach (var it in promo.Produtos)
                                    {
                                        total += it.Qtde * it.Price;
                                    }
                                }
                                btnComprar.Text = $"COMPRAR ➙ {total:C2}";
                            };
                            Produtos.Add(prod);
                        }
                    }

                    Promocoes.Add(new Promocao
                    {
                        Name = item.Name,
                        Category_id = item.Category_id,
                        Produtos = Produtos
                    });
                }

                if (ProdutosSemPromocao.Count > 0)
                {
                    Promocoes.Add(new Promocao
                    {
                        Name = "Produtos",
                        Category_id = null,
                        Produtos = ProdutosSemPromocao
                    });
                }

                ProdutosSemPromocao.CollectionChanged += ProdutosSemPromocao_CollectionChanged;
                Policy.CollectionChanged += Policy_CollectionChanged;
                Produtos.CollectionChanged += Produtos_CollectionChanged;
                Promocoes.CollectionChanged += Promocoes_CollectionChanged;

            }
            catch (Exception e)
            {
                var aux = e;
            }
        }

        private void ProdutosSemPromocao_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            throw new NotImplementedException();
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

