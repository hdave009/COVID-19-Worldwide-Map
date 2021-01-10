using HtmlAgilityPack;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;

namespace COVID_19_Information
{
	public class Scraper
	{
		private ObservableCollection<EntryModel> _countries = new ObservableCollection<EntryModel>();

		public ObservableCollection<EntryModel> Countries
		{
			get { return _countries; }
			set { _countries = value; }
		}

		public void ScrapeData()
		{
			_countries.Clear();
			string url = "https://www.worldometers.info/coronavirus/#countries";
			var web = new HtmlWeb();
			var doc = web.Load(url);

			var table = doc.DocumentNode.SelectSingleNode("//*[@id='main_table_countries_today']/tbody[1]");
			var countries = table.SelectNodes("//*[@id='main_table_countries_today']/tbody/tr");

			countries.RemoveAt(7);
			countries.RemoveAt(220);

			for (int i = 1; i<countries.Count; i++) 
			{
				var countryName = HttpUtility.HtmlDecode(countries[i].SelectSingleNode("//*[@id='main_table_countries_today']/tbody/tr["+i.ToString()+"]/td[1]").InnerText);
				long totalCases = FormatConversion.ConvertAndFormat(HttpUtility.HtmlDecode(countries[i].SelectSingleNode("//*[@id='main_table_countries_today']/tbody/tr[" + i.ToString() + "]/td[2]").InnerText));
				long totalDeaths = FormatConversion.ConvertAndFormat(HttpUtility.HtmlDecode(countries[i].SelectSingleNode("//*[@id='main_table_countries_today']/tbody/tr[" + i.ToString() + "]/td[4]").InnerText));
				var totalRecovered = FormatConversion.ConvertAndFormat(HttpUtility.HtmlDecode(countries[i].SelectSingleNode("//*[@id='main_table_countries_today']/tbody/tr[" + i.ToString() + "]/td[6]").InnerText));
				var activeCases = FormatConversion.ConvertAndFormat(HttpUtility.HtmlDecode(countries[i].SelectSingleNode("//*[@id='main_table_countries_today']/tbody/tr[" + i.ToString() + "]/td[7]").InnerText));

				_countries.Add(new EntryModel { Country = countryName, TotalCases = totalCases, TotalDeaths = totalDeaths, TotalRecovered = totalRecovered, ActiveCases = activeCases });
			}
		}

	}
}