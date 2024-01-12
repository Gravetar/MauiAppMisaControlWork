using MauiAppMisaControlWork.Models;

namespace MauiAppMisaControlWork.Pages;

public partial class ReadPage : ContentPage
{
    private Locality loc;
    private readonly ApiHelper _apiHelper;
    public ReadPage(Locality locality)
	{
		InitializeComponent();
        loc = locality;
        _apiHelper = new ApiHelper();

        Title = loc.name;

        p_name.Text = loc.name;
        p_residants.Text = loc.numberresidantsth.ToString();
        p_budget.Text = loc.budgetmlrd.ToString();
        p_mayor.Text = loc.mayor;

        if (loc.type == "City")
            p_type.Text = "�����";
        else p_type.Text = "������";
    }

    private async void MainPage(object? sender, EventArgs e)
    {
        await Navigation.PushAsync(new MainPage());
    }

    private async void btnDelete_Clicked(object sender, EventArgs e)
    {
        if (loc != null)
        {
            bool delete_alert = await DisplayAlert("�������� �������", $"�� ������������� ������ ������� ������� {loc.name}?", "��", "���");
            if (delete_alert)
            {
                await _apiHelper.DeleteLocalityAsync(loc);
                await Navigation.PushAsync(new MainPage());
            }
        }
        else
            await DisplayAlert("�������� �������", "������� �� ����������!", "��");
    }

    private async void btnUpdate_Clicked(object sender, EventArgs e)
    {
        if (loc != null)
            await Navigation.PushAsync(new UpdatePage(loc, isFromRead: true));
        else
            await DisplayAlert("��������� �������", "������� �� ����������!", "��");
    }
}