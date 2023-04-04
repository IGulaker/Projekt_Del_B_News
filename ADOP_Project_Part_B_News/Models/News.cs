using System.Collections.Generic;
using System.Xml.Serialization;

namespace ADOP_Project_Part_B_News.Models
{
    public enum NewsCategory
    {
        business, entertainment, general, health, science, sports, technology
    }

    [XmlRoot("News", Namespace = "http://mynamespace/test/")] //ths to be able to deserialize the sample data
    public class News
    {
        public NewsCategory Category { get; set; }
        public List<NewsItem> Articles { get; set; }
    }
}
