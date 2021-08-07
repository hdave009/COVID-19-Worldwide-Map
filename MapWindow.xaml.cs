using System;
using System.Collections.Generic;
using System.Windows;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Symbology;
using Esri.ArcGISRuntime.UI;

namespace COVID_19_Information
{
    /// <summary>
    /// Interaction logic for MapWindow.xaml
    /// </summary>
    public partial class MapWindow : Window
    {
        Scraper scraper;
        public MapWindow()
        {
            InitializeComponent();
            scraper = new Scraper();
            Map covidMap = new Map(Basemap.CreateImageryWithLabelsVector());
            mapView.Map = covidMap;
            DrawAllPointsOnMap();
        }

        

        private void tableButton_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            this.Close();
            mainWindow.Show();
        }

        private void DrawAllPointsOnMap() 
        {
            scraper.ScrapeData();
            var countries = scraper.Countries;
            foreach (KeyValuePair<string, EntryModel> country in countries) {
                double longitude = CSVReader.GetCountryCoordinates(country.Value.Country.ToString())[1];
                double latitude = CSVReader.GetCountryCoordinates(country.Value.Country.ToString())[0];

                if (longitude == 0 && latitude == 0) { }
                else
                {
                    MapPoint point = new MapPoint(longitude, latitude, SpatialReferences.Wgs84);

                    //Create point symbol with outline
                    int pointSize = country.Value.ActiveCases > 0 ? (int) Math.Log(country.Value.ActiveCases): 0;
                    
                    var pointSymbol = new SimpleMarkerSymbol(SimpleMarkerSymbolStyle.Circle, System.Drawing.Color.Red, pointSize);

                    //Create point graphic with geometry & symbol
                    var pointGraphic = new Graphic(point, pointSymbol);

                    //Add point graphic to graphic overlay
                    MapGraphics.Graphics.Add(pointGraphic);
                }
            }
        }
    }
}
