﻿using Imobiliaria.Models;
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
	public partial class EnvioMaterial : StackLayout
	{
		public EnvioMaterial (Imovel imovel)
		{
			InitializeComponent ();
		}
	}
}