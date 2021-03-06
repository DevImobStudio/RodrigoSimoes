﻿using Imobiliaria.Models;
using Imobiliaria.ViewModels;
using Plugin.Geolocator;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.GoogleMaps;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace Imobiliaria.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Maps : StackLayout
	{
        Inicio Inicio { get; set; }
      

        public Maps (Inicio Inicio)
		{
			InitializeComponent ();
            Mapa.MoveToRegion(MapSpan.FromCenterAndRadius(
                                    new Position(-23.0210053, -45.5572689),
                                    Distance.FromMiles(0.5))); 
            this.Inicio = Inicio;
            BindingContext = this.Inicio.viewModel;
            caroussel.ItemsSource = this.Inicio.viewModel.Imovels;
            CarregarDados();
            Mapa.MyLocationEnabled = true;
 
           


            try
            {
                GetUserPosition();
            }
            catch
            {

            }
        }
        

       
        public async  void CarregarDados()
        {

            this.Inicio.viewModel.LoadItemsCommand.Execute(null);
            if (this.Inicio.viewModel.Imovels.Count > 0)
            {

                Mapa.MoveToRegion(MapSpan.FromCenterAndRadius(
                                       new Position(this.Inicio.viewModel.Imovels[0].localizacao.Latitude, this.Inicio.viewModel.Imovels[0].localizacao.Longitude),
                                       Distance.FromMiles(0.5)));
            }
            this.ForceLayout();

        }


          

        private async void GetUserPosition()
    {

        try
        {
            var Location = CrossGeolocator.Current;
            Location.DesiredAccuracy = 50;

            var position = await Location.GetPositionAsync(TimeSpan.FromSeconds(10));



            Mapa.MoveToRegion(MapSpan.FromCenterAndRadius(new Position(position.Latitude, position.Longitude), Distance.FromMiles(1)));

        }
        catch
        {

        }


    }

        private void Caroussel_PositionSelected(object sender, CarouselView.FormsPlugin.Abstractions.PositionSelectedEventArgs e)
        {
            if (!this.Inicio.viewModel.IsBusy)
            {
                if (this.Inicio.viewModel.Imovels != null)
                {
                    if (this.Inicio.viewModel.Imovels.Count > 0)
                    {
                        Imovel imovel = this.Inicio.viewModel.Imovels[e.NewValue];
                        if (imovel == null)
                            return;
                         

                        Mapa.MoveToRegion(MapSpan.FromCenterAndRadius(
                                       new Position(imovel.localizacao.Latitude, imovel.localizacao.Longitude),
                                       Distance.FromMiles(0.5)));
                    }
                }
            }
            
           
          
        }

      

        private async void Mapa_PinClicked(object sender, PinClickedEventArgs e)
        {
            Imovel a = ((Imovel)e.Pin.BindingContext) ;
         //   Imovel imovel = viewModel.LstImoveis.Where(p => p.id == a.id).FirstOrDefault();
            if (a != null)
            {

                caroussel.SelectedItem = a;// this.Inicio.viewModel.LstImoveis.IndexOf(a);
                Mapa.MoveToRegion(MapSpan.FromCenterAndRadius(
                                     new Position(a.localizacao.Latitude, a.localizacao.Longitude),
                                     Distance.FromMiles(0.5)));
            }
           
             //e.Handled = true;
          

        }

        private async void Pin_Clicked(object sender, EventArgs e)
        {
            Pin b = sender as Pin;
            Imovel a = ((Imovel)b.BindingContext);
            if (a != null)
             {

                    Inicio.CarregarDetalhes(a);

            }
             
        }

        public void Bind()
        {
     //       InitializeComponent();
     //       Mapa.ItemsSource = this.Inicio.viewModel.Imovels;
     //       BindingContext = this.Inicio.viewModel;
            caroussel.ItemsSource = this.Inicio.viewModel.Imovels;
            if (this.Inicio.viewModel.Imovels.Count > 0)
            {
                caroussel.SelectedIndex = 0;
                Mapa.MoveToRegion(MapSpan.FromCenterAndRadius(
                                       new Position(this.Inicio.viewModel.Imovels[0].localizacao.Latitude, this.Inicio.viewModel.Imovels[0].localizacao.Longitude),
                                       Distance.FromMiles(0.5)));
            }

            this.ForceLayout();
        }

        private void Caroussel_ItemAppearing(PanCardView.CardsView view, PanCardView.EventArgs.ItemAppearingEventArgs args)
        {
            Imovel imovel = args.Item as Imovel;

            if (imovel != null)
            {
               
                 Mapa.MoveToRegion(MapSpan.FromCenterAndRadius(
                                       new Position(imovel.localizacao.Latitude, imovel.localizacao.Longitude),
                                       Distance.FromMiles(0.5)));
               
                
            }


        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            var objeto = ((sender as Button).CommandParameter) as Imovel;
            if (objeto != null)
            {
              
                Inicio.CarregarDetalhes(objeto);
            }
        }

        /*     private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
             {
                 // var args = (TappedEventArgs)e;
                 Imovel objeto = new Imovel();
                 objeto =((TappedEventArgs)e).Parameter as Imovel;

                 if (objeto != null)
                 {

                     await Inicio.CarregarDetalhes(objeto);

                 }
             }
             */




        private async void TapGestureRecognizer_Tapped(object sender, EventArgs e)
        {
            Imovel objeto = new Imovel();
            objeto = ((TappedEventArgs)e).Parameter as Imovel;

            if (objeto != null)
            {
                Inicio.CarregarDetalhes(objeto);
            }
        }

       
    }
}