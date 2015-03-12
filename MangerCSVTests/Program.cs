using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MangerCSVTests
{
    class Program
    {
        static void Main(string[] args)
        {
            List<dynamic> emptyList = new List<dynamic>()
            {

            };

            Console.WriteLine("Empty list example:");
            Console.WriteLine(MangerCSV.CSVHelper.GetCSVString(emptyList));

            List<dynamic> heterogeneousList = new List<dynamic>()
            {
                new 
                {
                    name = "German",
                    birthDate = new DateTime(1988, 3, 31),
                    age = 28,
                    height = 1.71,
                },
                new 
                {
                    name = "Valentina",
                    instrument = new
                    {
                        name = "bass",
                        brand = new 
                        {
                            name = "fender"
                        }
                    }
                },
                new 
                {
                    code = "foo",
                },
            };

            Console.WriteLine("Heterogeneous list example:");
            Console.WriteLine(MangerCSV.CSVHelper.GetCSVString(heterogeneousList));

            List<dynamic> cultureExample = new List<dynamic>()
            {
                new 
                {
                    name = "German",
                    height = 1.71d,
                    birthDate = new DateTime(1988, 3, 31),
                },
            };

            Console.WriteLine("Settings example:");
            Console.WriteLine(MangerCSV.CSVHelper.GetCSVString(inputList: cultureExample, cultureInfo: CultureInfo.CreateSpecificCulture("es-CL"), dateFormat: "yyyy-MM"));

            Console.ReadLine();
        }
    }
}
