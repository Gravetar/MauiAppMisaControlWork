using MauiAppMisaControlWork.Models;
using Microsoft.Maui.Devices.Sensors;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Runtime;
using System.Text;

namespace MauiAppMisaControlWork.Pages;

public partial class StatisticPage : ContentPage
{
    private readonly ApiHelper _apiHelper;

    public List<Statistic> StInfo { get; set; }

    public StatisticPage()
	{
		InitializeComponent();
        _apiHelper = new ApiHelper();
        LoadStatisticAsync();

        BindingContext = this;
    }
    private List<Statistic> ConvertJArrayToStatistics(JArray array)
    {
        var statisticsList = new List<Statistic>();

        foreach (var item in array)
        {
            var category = item["Name"]?.ToString();
            var median = item["Median"]?.Value<double?>() ?? 0;
            var mean = item["Mean"]?.Value<double?>() ?? 0;
            var max = item["Max"]?.Value<double?>() ?? 0;
            var min = item["Min"]?.Value<double?>() ?? 0;

            statisticsList.Add(new Statistic
            {
                name = category,
                median = Math.Round(median, 3),
                mean = Math.Round(mean, 3),
                max = Math.Round(max, 3),
                min = Math.Round(min, 3)
            });
        }

        return statisticsList;
    }

    private async void MainPage(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }

    private async void LoadStatisticAsync()
	{
        List<Statistic> StatisticFromApi = new();

        using (var client = new HttpClient())
        {
            double[]? Budgets = await _apiHelper.GetBudgetsAsync();
            double[]? Residants = await _apiHelper.GetResidantsAsync();
            // URL облачной функции в Yandex Cloud Function
            string functionUrl = "https://functions.yandexcloud.net/d4eekil0t8rto31d5eov";

            JObject jsonObject = new(
            new JProperty("Budgets", new JArray(Budgets)),
            new JProperty("Residants", new JArray(Residants))
            );

            // Content для POST-запроса
            HttpContent content = new StringContent(jsonObject.ToString(), Encoding.UTF8, "application/json");

            // POST-запрос с данными в теле
            HttpResponseMessage Res = await client.PostAsync(functionUrl, content);

            // Проверка успешности ответа
            if (Res.IsSuccessStatusCode)
            {
                // Получение ответа в виде строки
                var StResponse = await Res.Content.ReadAsStringAsync();

                // Десериализация ответа
                var responseJson = JObject.Parse(StResponse);

                // Извлечение resultBudgets и resultResidants
                if (responseJson.TryGetValue("resultBudgets", out var result1Token) && result1Token is JArray resultBudgetsArray &&
                    responseJson.TryGetValue("resultResidants", out var result2Token) && result2Token is JArray resultResidantsArray)
                {
                    // Преобразование данных resultBudgets и resultResidants в элементы Statistic и добавление их в StatisticFromApi
                    StatisticFromApi.AddRange(ConvertJArrayToStatistics(resultBudgetsArray));
                    StatisticFromApi.AddRange(ConvertJArrayToStatistics(resultResidantsArray));
                }
            }
        }
        StInfo = StatisticFromApi;
        OnPropertyChanged(nameof(StInfo));
    }
}