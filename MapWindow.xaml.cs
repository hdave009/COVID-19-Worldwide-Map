using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Esri.ArcGISRuntime.Geometry;
using Esri.ArcGISRuntime.Mapping;
using Esri.ArcGISRuntime.Symbology;
using System.Drawing;
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
            foreach (EntryModel country in scraper.Countries) {
                double longitude = CSVReader.GetCountryCoordinates(country.Country.ToString())[1];
                double latitude = CSVReader.GetCountryCoordinates(country.Country.ToString())[0];

                if (longitude == 0 && latitude == 0) { }
                else
                {
                    MapPoint point = new MapPoint(longitude, latitude, SpatialReferences.Wgs84);

                    //Create point symbol with outline
                    var pointSymbol = new SimpleMarkerSymbol(SimpleMarkerSymbolStyle.Circle, System.Drawing.Color.Red, 10);

                    //Create point graphic with geometry & symbol
                    var pointGraphic = new Graphic(point, pointSymbol);

                    //Add point graphic to graphic overlay
                    MapGraphics.Graphics.Add(pointGraphic);
                }
            }
        }
    }
}
