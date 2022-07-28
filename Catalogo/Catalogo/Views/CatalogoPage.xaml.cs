using System;
using System.Collections.Generic;
using Catalogo.View;
using Catalogo.ViewModels;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Catalogo.Views
{
    public partial class CatalogoPage : ContentPage
    {
        public ItensCatalogoView itensCatalogo;

        public CatalogoPage()
        {
            InitializeComponent();

            itensCatalogo = new ItensCatalogoView(btnComprar);
            layMain.Children.Add(itensCatalogo);
        }

        async void btnFilter_Clicked(System.Object sender, System.EventArgs e)
        {
            var itensFilter = new ItensFilterView();

            itensFilter.OnIdFiltroChanged += (senderr, id) =>
             {
                 itensCatalogo.IdCategoria = id;
             };

            await PopupNavigation.Instance.PushAsync(itensFilter);
        }

        async void btnComprar_Clicked(System.Object sender, System.EventArgs e)
        {
            await Navigation.PushAsync(new Carrinho());
        }
    }
}

