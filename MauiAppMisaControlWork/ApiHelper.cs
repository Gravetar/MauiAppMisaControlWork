using MauiAppMisaControlWork.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MauiAppMisaControlWork
{
    public class ApiHelper
    {
        private HttpClient _httpClient;
        string baseUrl;

        public ApiHelper()
        {
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) => true;
            _httpClient = new HttpClient(handler);
            //if (DeviceInfo.Platform == DevicePlatform.Android)
            //{
            //    //_httpClient.BaseAddress = new Uri("https://10.0.2.2:49105");
            //}
            //else
            //{
            //    //_httpClient.BaseAddress = new Uri("https://localhost:49105/");
            //}
            baseUrl = DeviceInfo.Platform == DevicePlatform.Android ? "https://10.0.2.2:49105" : "https://localhost:49105/";
        }

        public async Task<List<Locality>?> GetLocalitiesAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{baseUrl}/api/GetLocalities");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                var result = JsonSerializer.Deserialize<List<Locality>>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });

                return result;
            }
            catch (Exception ex)
            {
                // Обработка ошибок, например, вывод в консоль или логирование
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        public async Task<double[]?> GetBudgetsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{baseUrl}/api/GetBudgets");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<double[]>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (Exception ex)
            {
                // Обработка ошибок, например, вывод в консоль или логирование
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        public async Task<double[]?> GetResidantsAsync()
        {
            try
            {
                var response = await _httpClient.GetAsync($"{baseUrl}/api/GetResidants");
                response.EnsureSuccessStatusCode();

                var content = await response.Content.ReadAsStringAsync();

                return JsonSerializer.Deserialize<double[]>(content, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (Exception ex)
            {
                // Обработка ошибок, например, вывод в консоль или логирование
                Console.WriteLine($"Error: {ex.Message}");
                return null;
            }
        }

        public async Task<bool> CreateLocalityAsync(Locality loc)
        {
            try
            {
                JsonContent content = JsonContent.Create(loc);
                var Res = await _httpClient.PostAsync($"{baseUrl}/api/CreateLocality", content);
                return true;
            }
            catch (Exception ex)
            {
                // Обработка ошибок, например, вывод в консоль или логирование
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> UpdateLocalityAsync(Locality loc)
        {
            try
            {
                JsonContent content = JsonContent.Create(loc);
                var Res = await _httpClient.PutAsync($"{baseUrl}/api/UpdateLocality", content);
                return true;
            }
            catch (Exception ex)
            {
                // Обработка ошибок, например, вывод в консоль или логирование
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        public async Task<bool> DeleteLocalityAsync(Locality loc)
        {
            try
            {
                var Res = await _httpClient.DeleteAsync($"{baseUrl}/api/DeleteLocality/" + loc.id.ToString());
                return true;
            }
            catch (Exception ex)
            {
                // Обработка ошибок, например, вывод в консоль или логирование
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }
    }
}
