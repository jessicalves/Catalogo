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
        public int IdCategoria;
        public List<Produto> ListProduto;
        public List<Promocao> ListPromocao;
        public Button btnComprar;

        public ItensCatalogoModel(List<Produto> ListProduto, List<Promocao> ListPromocao, Button btnComprar)
        {
            this.ListProduto = ListProduto;
            this.ListPromocao = ListPromocao;
            this.btnComprar = btnComprar;
            Popular();
        }

        private void ProdutosSemPromocao_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ProdutosSemPromocao)));
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

        public void Limpar()
        {
            try
            {
                Promocoes.Clear();
                ProdutosSemPromocao.Clear();
                Produtos.Clear();
                Policy.Clear();
            }
            catch (Exception e)
            {
                var aux = e;
            }
        }

        public void Popular()
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
                                PrecoFormatado = produto.Price.ToString("C"),
                                Category_id = produto.Category_id,
                                Policy = null
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

                            if (IdCategoria != 0)
                            {
                                if (IdCategoria == prod.Category_id)
                                {
                                    ProdutosSemPromocao.Add(prod);
                                }
                            }
                            else
                            {
                                ProdutosSemPromocao.Add(prod);
                            }
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
                                PrecoFormatado = produto.Price.ToString("C"),
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

                            if (IdCategoria != 0)
                            {
                                if (IdCategoria == prod.Category_id)
                                {
                                    Produtos.Add(prod);
                                }
                            }
                            else
                            {
                                Produtos.Add(prod);
                            }

                        }
                    }

                    if(Produtos.Count > 0)
                    {
                        Promocoes.Add(new Promocao
                        {
                            Name = item.Name,
                            Category_id = item.Category_id,
                            Produtos = Produtos
                        });
                    }
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

        public event PropertyChangedEventHandler PropertyChanged;
    }
}

