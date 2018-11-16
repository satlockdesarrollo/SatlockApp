namespace SatlockApp.Services
{
    using System;
    using System.Collections.Generic;
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

        public async Task<Response> getTrips<T>(string urlBase, string serviceprefix, string controller, string accessToken, string username)
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

        public async Task<Response> Post<T>(string urlBase, string servicePrefix, string controller, T model)
        {
            try
            {
                var request = JsonConvert.SerializeObject(model);
                var content = new StringContent(request, Encoding.UTF8, "application/json");
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format("{0}{1}", servicePrefix, controller);
                var response = await client.PostAsync(url, content);

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString(),
                    };
                }

                var result = await response.Content.ReadAsStringAsync();
                var data_result = JsonConvert.DeserializeObject<Response>(result);

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

        public async Task<Response> loginUser<T>(string urlBase, string serviceprefix, string controller, T model)
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
