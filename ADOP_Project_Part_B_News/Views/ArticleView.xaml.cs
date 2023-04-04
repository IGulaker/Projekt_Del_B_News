using ADOP_Project_Part_B_News.Models;
using ADOP_Project_Part_B_News.Services;
using Microsoft.Maui.ApplicationModel;
using System.Web;

namespace ADOP_Project_Part_B_News.Views
{
    public partial class ArticleView : ContentPage
    {
        NewsService Service;
        NewsCategory Category;
        News NewsForCurrentCategory;

        public ArticleView(NewsCategory category)
        {
            InitializeComponent();

            Title = $"Nyheter inom {category}";
            Category = category;
            Service = new NewsService();
        }
        public ArticleView(string Url)
        {
            InitializeComponent();
            BindingContext = new UrlWebViewSource
            {
                Url = HttpUtility.UrlDecode(Url)
            };
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            MainThread.BeginInvokeOnMainThread(async () => { await LoadNews(); });
        }

        private async Task LoadNews()
        {
            try
            {
                NewsForCurrentCategory = await Service.GetNewsAsync(Category);
                NewsList.ItemsSource = NewsForCurrentCategory.Articles;
            }
            catch (Exception ex)
            {
                await DisplayAlert("ERROR", $"Ett fel inträfade: {ex}", "Ok");
            }
        }

        private async void Button_Refresh_Clicked(object sender, EventArgs e)
        {
            await LoadNews();
        }

        private async void NewsList_HeadlineTapped(object sender, EventArgs e)
        {
            try
            {
                var page = NewsList.SelectedItem as NewsItem;
                var navigationParam = new Dictionary<string, object>
                {
                {"RequestedPage",  page}
                };
                await Shell.Current.GoToAsync($"articlepage", navigationParam);
            }
            catch (Exception ex)
            {
                await DisplayAlert("ERROR", $"Ett fel inträfade: {ex}", "Ok");
            }
        }
    }
}
