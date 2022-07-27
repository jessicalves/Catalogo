using System;
using System.Collections.Generic;
using System.Net;
using Catalogo.Models;
using Newtonsoft.Json;
using Rg.Plugins.Popup.Pages;
using Rg.Plugins.Popup.Services;
using Xamarin.Forms;

namespace Catalogo.ViewModels
{
    public partial class ItensFilterView : PopupPage
    {
        public ItensFilterView()
        {
            InitializeComponent();

            WebClient Client = new WebClient();
            var contentFiltros = Client.DownloadString("https://pastebin.com/raw/YNR2rsWe");
            List<Filter> ObjetoFiltro = JsonConvert.DeserializeObject<List<Filter>>(contentFiltros);
            BindingContext = new ItensFilterModel(ObjetoFiltro);
        }

        async void btnFechar_Clicked(System.Object sender, System.EventArgs e)
        {
            await PopupNavigation.Instance.PopAsync();
        }

        void FiltersListView_SelectionChanged(System.Object sender, Xamarin.Forms.SelectionChangedEventArgs e)
        {
            var current = e.CurrentSelection;
        }
    }
}

