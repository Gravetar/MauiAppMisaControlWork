using MauiAppMisaControlWork.Models;

namespace MauiAppMisaControlWork.Pages;

public partial class UpdatePage : ContentPage
{
    private readonly ApiHelper _apiHelper;
    private bool _isFromRead;

    Locality updatedloc;

    public UpdatePage(Locality loc, bool isFromRead = false)
	{
		InitializeComponent();

        if (isFromRead)
        {
            btnBack.IsVisible = true;
        }

        _apiHelper = new ApiHelper();
        updatedloc = loc;

        p_name.Text = loc.name;
		p_type.SelectedItem = loc.type;
		p_residants.Text = loc.numberresidantsth.ToString();
		p_budget.Text = loc.budgetmlrd.ToString();
		p_mayor.Text = loc.mayor;

        if (loc.type == "City")
            p_type.SelectedItem = "Город";
        else if (loc.type == "Region")
            p_type.SelectedItem = "Регион";
    }
    private async void Update(object? sender, EventArgs e)
    {
        updatedloc.id = updatedloc.id;
        updatedloc.name = p_name.Text;
        updatedloc.numberresidantsth = double.Parse(p_residants.Text);
        updatedloc.budgetmlrd = double.Parse(p_budget.Text);
        updatedloc.mayor = p_mayor.Text;

        if ((string)p_type.SelectedItem == "Город")
            updatedloc.type = "City";
        else if((string)p_type.SelectedItem == "Регион")
            updatedloc.type = "Region";

        await _apiHelper.UpdateLocalityAsync(updatedloc);
        await Navigation.PushAsync(new MainPage());
    }

    private async void MainPage(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }

    private async void btnBack_Clicked(object sender, EventArgs e)
    {
        if (updatedloc != null)
        {
            await Navigation.PushAsync(new ReadPage(updatedloc));
        }
        else
            await DisplayAlert("Просмотр локации", "Локация не обнаружена!", "ОК");
    }
}