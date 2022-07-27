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
		public CatalogoPage ()
		{
			InitializeComponent ();
			layMain.Children.Add(new ItensCatalogoView(btnComprar));
		}

        async void btnFilter_Clicked(System.Object sender, System.EventArgs e)
        {
			await PopupNavigation.Instance.PushAsync(new ItensFilterView());
		}

        async void btnComprar_Clicked(System.Object sender, System.EventArgs e)
        {
			await Navigation.PushAsync(new Carrinho());
        }
    }
}

