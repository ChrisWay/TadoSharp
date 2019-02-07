using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Flurl;
using Flurl.Http;
using TadoSharp.Models;

namespace TadoSharp
{
    public class TadoClient
    {
        const string BaseUri = "https://my.tado.com/api/v2";
        const string AuthUri = "https://auth.tado.com/oauth/token";

        readonly string _username;
        readonly string _password;

        public TadoClient(string username, string password)
        {
            _username = username;
            _password = password;
        }

        public async Task<bool> Authenticate()
        {
            var authResult = await AuthUri
                  .PostUrlEncodedAsync(new { client_id = "tado-webapp", grant_type = "password", scope = "home.user", username = _username, password = _password })
                  .ReceiveJson<AuthenticationResponse>();

            return true;
        }
    }
}
