﻿using Imobiliaria.Models;
using Imobiliaria.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace Imobiliaria.Services
{
    public class OAuthConfig
    {

        public static Entrar _HomePage;
        public static NavigationPage _NavigationPage;
        public static TabbedPage1 _TabbedPage;
        public static Usuario User;

        public static Action SuccessfulLoginAction
        {
            get
            {
                return new Action(() =>
                {
                    _NavigationPage.Navigation.PopModalAsync();
                    _TabbedPage.CurrentPage = _TabbedPage.Children[1];
                  
                });
            }
        }
    }
}
