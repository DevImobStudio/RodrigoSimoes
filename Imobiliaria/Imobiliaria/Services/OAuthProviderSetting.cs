﻿using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Auth;

namespace Imobiliaria.Services
{
    public class AuthenticationState
    {
        public static OAuth2Authenticator Authenticator;
    }



    public class OAuthProviderSetting
    {
        public string ClientId { get; private set; }
        public string ConsumerKey { get; private set; }
        public string ConsumerSecret { get; private set; }
        public string RequestTokenUrl { get; private set; }
        public string AccessTokenUrl { get; private set; }
        public string AuthorizeUrl { get; private set; }
        public string CallbackUrl { get; private set; }

     


        public enum OauthIdentityProvider
        {
            GOOGLE,
            FACEBOOK,
 
        }

      
        public OAuth2Authenticator LoginWithProvider(string Provider)
        {
            OAuth2Authenticator auth = null;
            switch (Provider)
            {
                case "Google":
                     
                    {
                        auth = new OAuth2Authenticator(
                                    // For Google login, for configure refer http://www.c-sharpcorner.com/article/register-identity-provider-for-new-oauth-application/
                                    "759941497164-sihog7v5sf14lq30r76nr9tmgsf6e9nh.apps.googleusercontent.com",
                                   "imobiliaria-239615",
                                   "https://www.googleapis.com/auth/admin.directory.customer.readonly",
                                   new Uri("https://accounts.google.com/o/oauth2/auth"),
                                   new Uri("urn:ietf:wg:oauth:2.0:oob"),
                                   new Uri("https://oauth2.googleapis.com/token"),
                                   isUsingNativeUI: true
                                  
                                   
                                    );

                        break;
                    }
                case "FaceBook":
               {
                    auth = new OAuth2Authenticator(
                    clientId: "456228191618171",  // For Facebook login, for configure refer http://www.c-sharpcorner.com/article/register-identity-provider-for-new-oauth-application/
                    scope: "",
                    authorizeUrl: new Uri("https://m.facebook.com/dialog/oauth/"), // These values do not need changing
                    redirectUrl: new Uri("http://www.facebook.com/connect/login_success.html")// These values do not need changing
                    
                    );
                    break;
               }
            }
            return auth;

        }

       
    }
}
