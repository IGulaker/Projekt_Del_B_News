using ADOP_Project_Part_B_News.Models;
using ADOP_Project_Part_B_News.Views;

namespace ADOP_Project_Part_B_News;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		foreach(NewsCategory category in Enum.GetValues(typeof(NewsCategory)))
		{
			var sc = new ShellContent
			{
				Title = category.ToString(),
				Route = category.ToString(),
				ContentTemplate = new DataTemplate(() => new ArticleView(category))

			};
			Items.Add(sc);
		}

		Routing.RegisterRoute("articlepage", typeof(ArticlePage));
    }
}
