﻿using Imobiliaria.Models;
using Rg.Plugins.Popup.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Imobiliaria.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Pesquisa : ContentPage
	{
        public Inicio Inicio { get; set; }
        List<string> cidades { get; set; }
        List<string> categoria { get; set; }
        List<Opcao> bairros { get; set; }
        List<string> dormitorios { get; set; }
        
        public Pesquisa (Inicio inicio)
		{
			InitializeComponent();
            this.Inicio = inicio;
            BindingContext = this.Inicio.viewModel;

            this.Busca.Text = this.Inicio.viewModel.mPesquisa.busca;
            if (this.Inicio.viewModel.mPesquisa.faixa2 > 0)
            {
                this.Faixa2.Text = this.Inicio.viewModel.mPesquisa.faixa2.ToString("C2");
            }
            if (this.Inicio.viewModel.mPesquisa.faixa2 > 0)
            {
                this.Faixa2.Text = this.Inicio.viewModel.mPesquisa.faixa2.ToString("C2");
            }


            cidades = new List<string>();
            categoria = new List<string>();
            bairros = new List<Opcao>();
            dormitorios = new List<string>();
           
            carregarDados();
            AtualizarPesquisa();
          
        }

        public void carregarDados()
        {
            
            carregarDadosCidade();
            carregarDadosTipo();
            carregarDadosDormitorios();
        
            if (this.Inicio.viewModel.mPesquisa.cidade == null)
            {
                GridBairro.IsEnabled = false;
            }
            else
            {
                if (this.Inicio.viewModel.mPesquisa.cidade == "")
                    GridBairro.IsEnabled = false;
                else
                    GridBairro.IsEnabled = true;
            }

            BindingContext = this.Inicio.viewModel;
        }
        public void carregarDadosCidade()
        {
            cidades.Clear();
            foreach (var i in Inicio.viewModel.LstImoveis)
            {
                if (!cidades.Contains(i.cidade))
                {
                    cidades.Add(i.cidade);
                }
               
            }

            cidades = cidades.Distinct().ToList();
        }

        public void carregarDadosTipo()
        {
            categoria.Clear();
            foreach (var i in Inicio.viewModel.LstImoveis)
            {
                if (!categoria.Contains(i.categoria))
                {
                    categoria.Add(i.categoria);
                }
               
            }

            categoria = categoria.Distinct().ToList();
        }

        public void carregarDadosDormitorios()
        {
            dormitorios = new List<string>() {
                "1",
                "2",
                "3",
                "4",
                "5+",

            };

            Lista.ItemsSource = dormitorios;
        }

       
        public void carregarDadosBairro()
        {

           
            bairros.Clear();
            List<string> a = new List<string>();
            if (this.Inicio.viewModel.mPesquisa.cidade != null)
            {
                
                foreach (var i in Inicio.viewModel.LstImoveis.Where(p => p.cidade == this.Inicio.viewModel.mPesquisa.cidade).ToList())
                {
                    a.Add(i.bairro);
                }
            }
            else
            {
                foreach (var i in Inicio.viewModel.LstImoveis)
                {
                    a.Add(i.bairro);
                }
            }


            foreach (var i in a.Distinct().ToList())
            {

                bairros.Add(new Opcao()
                {
                    opcao = i,
                    check = false
                });

            }

        }

       

        public void AtualizarPesquisa()
        {
            if (this.Inicio.viewModel.mPesquisa.cidade != null)
            {
                Cidade.Text = this.Inicio.viewModel.mPesquisa.cidade;

                GridBairro.IsEnabled = true;

                carregarDadosBairro();
            }
            if (this.Inicio.viewModel.mPesquisa.bairro != null)
            {
                Bairro.Text = "";
                foreach (var i in this.Inicio.viewModel.mPesquisa.bairro)
                {
                    if (Bairro.Text.Length > 100)
                    {
                        break;
                    }
                    Bairro.Text = Bairro.Text + i;
                }
               
            }
            if (this.Inicio.viewModel.mPesquisa.categoria != null)
            {
                Categoria.Text = this.Inicio.viewModel.mPesquisa.categoria;
            }
        }

        private async void Confirmar_Clicked(object sender, EventArgs e)
        {
           this.Inicio.viewModel.mPesquisa.busca = Busca.Text;

            Inicio.viewModel.mPesquisa.dormitorios = new List<string>();
            if (Lista.SelectedItems != null)
            {
                foreach (var i in Lista.SelectedItems)
                {
                    Inicio.viewModel.mPesquisa.dormitorios.Add(i.ToString());
                }
            }

           



            try
            {
                await this.Navigation.PopAsync();
            }
            catch (Exception)
            {

              
            }
            this.Inicio.CarregarPesquisa();

        }

       

        private void Cancelar_Clicked(object sender, EventArgs e)
        {
            this.Inicio.viewModel.mPesquisa.busca = null;
            this.Inicio.viewModel.mPesquisa.categoria = null;
            this.Inicio.viewModel.mPesquisa.cidade = null;
            this.Inicio.viewModel.mPesquisa.dormitorios = null;
            this.Inicio.viewModel.mPesquisa.negocio = null;
            this.Inicio.viewModel.mPesquisa.bairro = null;
            this.Inicio.viewModel.LoadItemsCommand.Execute(null);
         //   Busca.Text = "";

            this.Navigation.PopAsync();
            this.Inicio.CarregarPagina();


        }

        public void SetarBairro(List<Opcao> opcaos)
        {
            this.Inicio.viewModel.mPesquisa.bairro = new List<string>();
            foreach (var i in opcaos.FindAll(p => p.check))
            {
                this.Inicio.viewModel.mPesquisa.bairro.Add(i.opcao);
            }
           
        }


        private void Tipo_Clicked(object sender, EventArgs e)
        {
            Navigation.PushPopupAsync(new ModalOneSelected(this.categoria, "Escolha o Tipo", this,"TIPO"));
        }

        private void Cidade_Clicked(object sender, EventArgs e)
        {
            Navigation.PushPopupAsync(new ModalOneSelected(this.cidades, "Escolha a cidade", this,"CIDADE"));

        }
        private void Bairro_Clicked(object sender, EventArgs e)
        {
            Navigation.PushPopupAsync(new ModalMultiSelect(this.bairros, "Escolha o Bairro", this));
        }

        private void Venda_Clicked(object sender, EventArgs e)
        {
            if (Venda.IsChecked)
            {
                Locacao.IsChecked = false;
                Lancamento.IsChecked = false;
                this.Inicio.viewModel.mPesquisa.negocio = "Venda";
            }
        }

        private void Locacao_Clicked(object sender, EventArgs e)
        {
            if (Locacao.IsChecked)
            {
                Venda.IsChecked = false;
                Lancamento.IsChecked = false;
                this.Inicio.viewModel.mPesquisa.negocio = "Locação";
            }
        }

        private void Lancamento_Clicked(object sender, EventArgs e)
        {
            if (Lancamento.IsChecked)
            {
                Locacao.IsChecked = false;
                Venda.IsChecked = false;
                this.Inicio.viewModel.mPesquisa.negocio = "Lançamento";
            }
        }
    }
}