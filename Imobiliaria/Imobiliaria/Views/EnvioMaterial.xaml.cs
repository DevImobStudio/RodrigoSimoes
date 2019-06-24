﻿using Imobiliaria.Models;
using Plugin.Toast;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Imobiliaria.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class EnvioMaterial : StackLayout
	{
        Imovel imovel { get; set; }

        public EnvioMaterial (Imovel imovel)
		{
			InitializeComponent ();
            this.imovel = imovel;
		}

       

        private async void Enviar_Clicked(object sender, EventArgs e)
        {
            //Sample post to a restful api url, remember to use the namespace System.Net.Http for HttpClient()
            var myHttpClient = new HttpClient();
            var uri = new Uri("https://www.api.rodrigosimoesimoveis.com.br/post/lead/");

            //Since this sample restful api url accepts any json structure, we can structure the data like this
            HttpContent formContent = new FormUrlEncodedContent(new Dictionary<string, string>
            {
                { "api_secret", "EYTAtsANMai5U66RDgZcdyuuDGxP2szoat8r"},
                { "nome", Nome.Text },
                { "email", this.Email.Text },
                { "whatsapp", Whatsapp.Text},
                { "id_imovel", this.imovel.id },
                { "titulo_imovel", this.imovel.titulo }
            });
            formContent.Headers.ContentType = new MediaTypeHeaderValue("application/x-www-form-urlencoded");
            var response = await myHttpClient.PostAsync(uri.ToString(), formContent);

            if (response.IsSuccessStatusCode) {
               
                string respContent = await response.Content.ReadAsStringAsync();
                if (respContent.Contains("200"))
                {
                    CrossToastPopUp.Current.ShowToastMessage("Dados enviados com sucesso!");
                }
                else
                {
                    CrossToastPopUp.Current.ShowToastMessage("Erro ao enviar dados, tente novamente");
                }
            }
            else
            {
                CrossToastPopUp.Current.ShowToastMessage("Erro ao enviar dados, tente novamente");
            }
               
        }
       
    }
}