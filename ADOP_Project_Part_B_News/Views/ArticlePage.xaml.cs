using ADOP_Project_Part_B_News.Models;
using System.Collections.ObjectModel;

namespace ADOP_Project_Part_B_News.Views;

public partial class ArticlePage : ContentPage, IQueryAttributable
{
    public NewsItem RequestedPage { get; private set; }

    public ArticlePage()
    {
        InitializeComponent();
        BindingContext = this;
    }

    protected override void OnNavigatedTo(NavigatedToEventArgs args)
    {
        try
        {
            base.OnNavigatedTo(args);

            Title += $"{RequestedPage.Title}";
        }
        catch (Exception ex)
        {
            DisplayAlert("ERROR", $"Ett fel inträfade: {ex}", "Ok");
        }
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        RequestedPage = query["RequestedPage"] as NewsItem;
        OnPropertyChanged("RequestedPage");
    }
}