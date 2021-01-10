using System;

namespace COVID_19_Information
{
    class FormatConversion
    {
        /// <summary>
        /// Will remove commas from numbers and return a long.
        /// </summary>
        /// <param name="numberAsString"></param>
        /// <returns></returns>
        public static long ConvertAndFormat(string numberAsString)
        {
            numberAsString = numberAsString.Trim();
            if (numberAsString != "")
            {
                for (int i = 0; i < numberAsString.Length; i++)
                {
                    if (numberAsString[i] == ',')
                    {
                        numberAsString = numberAsString.Remove(i, 1);
                    }
                }
                return Int64.Parse(numberAsString);

            }
            else
            {
                return 0;
            }

        }
    }
}
