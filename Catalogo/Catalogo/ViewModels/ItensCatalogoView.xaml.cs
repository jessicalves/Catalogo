﻿using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;
using System.Windows.Input;
using Catalogo.Model;
using Catalogo.Models;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Catalogo.View
{
    public partial class ItensCatalogoView : ContentView
    {
        public ItensCatalogoModel itensCatalogoModel;
        public int IdCategoria
        {
            set
            {
                itensCatalogoModel.IdCategoria = value;
                itensCatalogoModel.Limpar();
                itensCatalogoModel.Popular();
                CvPromocao.SetBinding(ItemsView.ItemsSourceProperty, "Promocoes");
            }
        }

        public ItensCatalogoView(Button btnComprar)
        {
            InitializeComponent();

            WebClient Client = new WebClient();
            var contentProduto = Client.DownloadString("https://pastebin.com/raw/eVqp7pfX");
            var contentPromocao = Client.DownloadString("https://pastebin.com/raw/R9cJFBtG");

            List<Produto> ObjetoProduto = JsonConvert.DeserializeObject<List<Produto>>(contentProduto);
            List<Promocao> ObjetoPromocao = JsonConvert.DeserializeObject<List<Promocao>>(contentPromocao);

            itensCatalogoModel = new ItensCatalogoModel(ObjetoProduto, ObjetoPromocao, btnComprar);

            BindingContext = itensCatalogoModel;  
        }
    }
}

