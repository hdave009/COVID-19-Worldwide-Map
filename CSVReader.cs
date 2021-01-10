using System;

namespace COVID_19_Information
{
    public class CSVReader
    {
        public static double[] GetCountryCoordinates(string countryName)
        {
            double latitude = 0;
            double longitude = 0;

            string[] lines = System.IO.File.ReadAllLines(@"Assets\countriesandcoordinates.csv");
            if (CountryIsInList(countryName))
            {
                for (int ln = 0; ln < lines.Length; ln++)
                {
                    if (lines[ln].Split(',')[2] == countryName)
                    {
                        latitude = Convert.ToDouble(lines[ln].Split(',')[0]);
                        longitude = Convert.ToDouble(lines[ln].Split(',')[1]);
                        break;
                    }
                }
            }
            return new double[] { latitude, longitude };
        }

        public static bool CountryIsInList(string countryName)
        {
            bool exists = false;
            string[] lines = System.IO.File.ReadAllLines(@"Assets\countriesandcoordinates.csv");
            for (int i = 0; i < lines.Length; i++)
            {
                string[] fields = lines[i].Split(',');
                if (fields[2] == countryName)
                {
                    exists = true;
                }
            }
            return exists;
        }
    }
}
