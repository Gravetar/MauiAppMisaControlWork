using MauiAppMisaControlWork.Models;
using MauiAppMisaControlWork.Pages;
using Microsoft.Maui.Controls.Platform;
using Microsoft.Maui.Devices.Sensors;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MauiAppMisaControlWork
{
    public partial class MainPage : ContentPage
    {
        private readonly ApiHelper _apiHelper;
        public List<Locality>? Locations { get; set; }

        public MainPage()
        {
            InitializeComponent();

            _apiHelper = new ApiHelper();
            LoadLocalities();
            BindingContext = this;
        }

        private async void LoadLocalities()
        {
            Locations = await _apiHelper.GetLocalitiesAsync();
            OnPropertyChanged(nameof(Locations));
        }

        private async void Statistic(object? sender, EventArgs e)
        {
            await Navigation.PushAsync(new StatisticPage());
        }

        private async void btnCreate_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new CreatePage());
        }

        private async void btnDelete_Clicked(object sender, EventArgs e)
        {
            Locality deleted_locality = (Locality)locationList.SelectedItem;

            if (deleted_locality != null)
            {
                bool delete_alert = await DisplayAlert("Удаление локации", $"Вы действительно хотите удалить локацию {deleted_locality.name}?", "Да", "Нет");
                if (delete_alert)
                {
                    await _apiHelper.DeleteLocalityAsync((Locality)locationList.SelectedItem);
                    LoadLocalities();
                }
            }
            else
                await DisplayAlert("Удаление локации", "Выберите пожалуйста локацию из списка!", "ОК");
        }

        private async void btnUpdate_Clicked(object sender, EventArgs e)
        {
            if ((Locality)locationList.SelectedItem != null)
            await Navigation.PushAsync(new UpdatePage((Locality)locationList.SelectedItem));
            else
                await DisplayAlert("Изменение локации", "Выберите пожалуйста локацию из списка!", "ОК");
        }

        private async void btnRead_Clicked(object sender, EventArgs e)
        {
            Locality loc = (Locality)locationList.SelectedItem;

            if (loc != null)
            {
                await Navigation.PushAsync(new ReadPage(loc));
            }
            else
                await DisplayAlert("Просмотр локации", "Выберите пожалуйста локацию из списка!", "ОК");
        }
    }

}
