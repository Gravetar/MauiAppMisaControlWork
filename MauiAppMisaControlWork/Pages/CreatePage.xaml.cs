using MauiAppMisaControlWork.Models;

namespace MauiAppMisaControlWork.Pages;

public partial class CreatePage : ContentPage
{
    private readonly ApiHelper _apiHelper;

    public CreatePage()
	{
		InitializeComponent();
        _apiHelper = new ApiHelper();
    }
    private async void MainPage(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }

    private async void Create(object? sender, EventArgs e)
    {
        Locality createdloc = new Locality();
        createdloc.name = p_name.Text;
        createdloc.numberresidantsth = double.Parse(p_residants.Text);
        createdloc.budgetmlrd = double.Parse(p_budget.Text);
        createdloc.mayor = p_mayor.Text;

        if ((string)p_type.SelectedItem == "Город")
            createdloc.type = "City";
        else if ((string)p_type.SelectedItem == "Регион")
            createdloc.type = "Region";

        await _apiHelper.CreateLocalityAsync(createdloc);
        await Navigation.PushAsync(new MainPage());
    }
}