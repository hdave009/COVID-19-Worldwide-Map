using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace COVID_19_Information
{
	public class Scraper
	{
		private Dictionary<string, EntryModel> _countries = new Dictionary<string,EntryModel>();

		public Dictionary<string, EntryModel> Countries
		{
			get { return _countries; }
			set { _countries = value; }
		}

		private ObservableCollection<EntryModel> countryModels = new ObservableCollection<EntryModel>();

		public ObservableCollection<EntryModel> CountryModels
		{
			get { return countryModels; }
		}

		public void ScrapeData()
		{
			_countries.Clear();
			string url = "https://www.worldometers.info/coronavirus/#countries";
			var web = new HtmlWeb();
			var doc = web.Load(url);

			var table = doc.DocumentNode.SelectSingleNode("//*[@id='main_table_countries_today']");
			var countries = table.SelectNodes("//*[@id='main_table_countries_today']/tbody[1]/tr");

            for (int i = 8; i < countries.Count; i++)
            {
				try
				{
					var country = countries[i];
					var info = country.InnerText.Split('\n');

					var countryName = info[2];
					long totalCases = Convert.ToInt64(string.Join("", info[3].Split(',')));
					long totalDeaths = Convert.ToInt64(string.Join("", info[5].Split(',')));
					long totalRecovered = Convert.ToInt64(string.Join("", info[7].Split(',')));
					long activeCases = Convert.ToInt64(string.Join("", info[9].Split(',')));

					var model = new EntryModel { Country = countryName, TotalCases = totalCases, TotalDeaths = totalDeaths, TotalRecovered = totalRecovered, ActiveCases = activeCases };

					_countries.Add(countryName, model);
					countryModels.Add(model);
				} catch (Exception)
                {
					continue;
                }
            }
        }

	}
}