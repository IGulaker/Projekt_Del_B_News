using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

using ADOP_Project_Part_B_News.Models;
using ADOP_Project_Part_B_News.ModelsSampleData;

namespace ADOP_Project_Part_B_News.Services
{
    public class NewsService
    {
        HttpClient httpClient = new HttpClient();
        readonly string apiKey = "4fabf40756e6419bbd834751c06640a5";

        public NewsService()
        {
            httpClient = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate });
            httpClient.DefaultRequestHeaders.Add("user-agent", "News-API-csharp/0.1");
            httpClient.DefaultRequestHeaders.Add("x-api-key", apiKey);
        }

        public async Task<News> GetNewsAsync(NewsCategory category)
        {
            try
            {
                // Använder nyhetskälla från "us" istället för given för att få med bilder:
                //var uri = $"https://newsapi.org/v2/top-headlines?country=se&category={category}";
                var uri = $"https://newsapi.org/v2/top-headlines?country=us&category={category}";

                var httpRequest = new HttpRequestMessage(HttpMethod.Get, uri);
                var response = await httpClient.SendAsync(httpRequest);
                response.EnsureSuccessStatusCode();

                NewsApiData nd = await response.Content.ReadFromJsonAsync<NewsApiData>();
                var news = NewsApiDataToNews(nd);
                news.Category = category;
                return news;
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("ERROR", $"Ett fel inträfade: {ex}", "Ok");
                return null;
            }
        }

        private News NewsApiDataToNews(NewsApiData nData)
        {
            var news = new News()
            {
                Articles = nData.Articles.Select(item => new NewsItem()
                {
                    DateTime = item.PublishedAt,
                    Title = item.Title,
                    Description = item.Description,
                    Url = item.Url,
                    UrlToImage = $"{item.UrlToImage}"
                }).ToList()
            };

            return news;
        }
    }
}
