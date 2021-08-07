using System;
using System.Windows;

namespace COVID_19_Information
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Scraper scraper;
        public MainWindow()
        {
            InitializeComponent();
            scraper = new Scraper();
            DataContext = scraper;
            System.Windows.Threading.DispatcherTimer dispatcherTimer = new System.Windows.Threading.DispatcherTimer();
            dispatcherTimer.Tick += dispatcherTimer_Tick;
            dispatcherTimer.Interval = new TimeSpan(0,1,0);
            dispatcherTimer.Start();

        }

        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            scraper.ScrapeData();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            scraper.ScrapeData();
        }

        private void refreshButton_Click(object sender, RoutedEventArgs e)
        {
            scraper.ScrapeData();
        }

        private void mapButton_Click(object sender, RoutedEventArgs e)
        {
            MapWindow mapWindow = new MapWindow();
            mapWindow.Show();
            this.Close();
        }
    }

}
