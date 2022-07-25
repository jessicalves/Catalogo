using System;
using System.Collections.Generic;
using Catalogo.View;
using Xamarin.Forms;

namespace Catalogo.Views
{	
	public partial class CatalogoPage : ContentPage
	{	
		public CatalogoPage ()
		{
			InitializeComponent ();
			layMain.Children.Add(new ItensCatalogoView());
		}
	}
}

