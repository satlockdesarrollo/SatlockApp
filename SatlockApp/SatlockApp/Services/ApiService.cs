namespace SatlockApp.Services
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using Models;
    using Newtonsoft.Json;
    using Plugin.Connectivity;
    using Xamarin.Forms;

    public class ApiService
    {

        public async Task<Response> CheckConnection()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Por favor,encienda su configuración de Internet.",
                };
            }

            var pingUrl = Application.Current.Resources["PingUrl"].ToString();

            bool isReachable = await CrossConnectivity.Current.IsRemoteReachable(pingUrl, 5000);

            if (!isReachable)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Comprueba tu conexión a internet.",
                };
            }

            return new Response
            {
                IsSuccess = true,
                Message = "Ok",
            };
        }

        public async Task<Response> ValidateMobile<T>(string urlBase, string serviceprefix, string controller, string parameters, string accessToken, string username)
        {

            try
            {

                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                client.DefaultRequestHeaders.Add("User", username);
                string url_api = urlBase + serviceprefix + controller +parameters;
                var uri = new Uri(string.Format(url_api, string.Empty));
                var response = await client.GetAsync(url_api);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    var error = JsonConvert.DeserializeObject<Response>(result);
                    error.IsSuccess = false;
                    return error;
                }

                var data_result = JsonConvert.DeserializeObject<Response>(result);
                var events = JsonConvert.DeserializeObject<MobileRequest>(data_result.Data.ToString());

                return new Response
                {
                    IsSuccess = true,
                    Message = data_result.Message,
                    Data = events
                };


            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<Response> GetTrips<T>(string urlBase, string serviceprefix, string controller, string accessToken, string username)
        {
            try
            {
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                client.DefaultRequestHeaders.Add("User", username);
                string url_api = urlBase + serviceprefix + controller;
                var uri = new Uri(string.Format(url_api, string.Empty));
                var response = await client.GetAsync(url_api);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    var error = JsonConvert.DeserializeObject<Response>(result);
                    error.IsSuccess = false;
                    return error;
                }

                var data_result = JsonConvert.DeserializeObject<Response>(result);
                var list = JsonConvert.DeserializeObject<List<TripsRequest>>(data_result.Data.ToString());


                return new Response
                {
                    IsSuccess = true,
                    Message = data_result.Message,
                    Data = list
                };


            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        public async Task<Response> CreateTrip<T>(string urlBase, string serviceprefix, string controller, string accessToken, string username , T model)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                string url_api = urlBase + serviceprefix + controller;
                var client = new HttpClient();
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                client.DefaultRequestHeaders.Add("User", username);
                var uri = new Uri(string.Format(url_api, string.Empty));
                var response = await client.PostAsync(uri, content);
                var result = await response.Content.ReadAsStringAsync();


                if (!response.IsSuccessStatusCode)
                {
                    var error = JsonConvert.DeserializeObject<Response>(result);
                    error.IsSuccess = false;
                    return error;
                }

                var data_result = JsonConvert.DeserializeObject<Response>(result);
                data_result.IsSuccess = true;
                return data_result;

            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }

        }

        public async Task<Response> LoginUser<T>(string urlBase, string serviceprefix, string controller, T model)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                string url_api = urlBase + serviceprefix + controller;
                var client = new HttpClient();
                var uri = new Uri(string.Format(url_api, string.Empty));
                var response = await client.PutAsync(uri, content);
                var result = await response.Content.ReadAsStringAsync();


                if (!response.IsSuccessStatusCode)
                {
                    var error = JsonConvert.DeserializeObject<Response>(result);
                    error.IsSuccess = false;
                    return error;
                }

                var data_result = JsonConvert.DeserializeObject<Response>(result);
                data_result.IsSuccess = true;
                return data_result;

            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }
    }
}
