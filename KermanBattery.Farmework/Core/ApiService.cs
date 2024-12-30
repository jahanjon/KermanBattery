using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace RepresentativePanel.Domain.Core
{
    public class ApiService
    {
        public static async Task<T> GetData<T>(string baseUrl, string url, string token = "")
        {
            try
            {
                HttpClient httpClient = new HttpClient(new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (HttpRequestMessage sender, X509Certificate2 cert, X509Chain chain, SslPolicyErrors sslPolicyErrors) => true
                }, disposeHandler: false);
                httpClient.BaseAddress = new Uri(baseUrl);
                if (token != "")
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.Timeout = TimeSpan.FromSeconds(300.0);
                return JsonConvert.DeserializeObject<T>(await httpClient.GetStringAsync(httpClient.BaseAddress?.ToString() + url));
            }
            catch (Exception)
            {
            }

            return default(T);
        }

        public static async Task<T> PostData<T>(string baseUrl, string url, object data, string token = "")
        {
            try
            {
                HttpClient httpClient = new HttpClient(new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = (HttpRequestMessage sender, X509Certificate2 cert, X509Chain chain, SslPolicyErrors sslPolicyErrors) => true
                }, disposeHandler: false);
                httpClient.BaseAddress = new Uri(baseUrl);
                string s = JsonConvert.SerializeObject(data);
                ByteArrayContent byteArrayContent = new ByteArrayContent(Encoding.UTF8.GetBytes(s));
                byteArrayContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                if (token != "")
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                httpClient.Timeout = TimeSpan.FromSeconds(300.0);
                return JsonConvert.DeserializeObject<T>((await httpClient.PostAsync(httpClient.BaseAddress?.ToString() + url, byteArrayContent)).Content.ReadAsStringAsync().Result);
            }
            catch (Exception)
            {
            }

            return default(T);
        }

    }
}
