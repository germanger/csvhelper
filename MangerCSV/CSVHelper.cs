using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MangerCSV
{
    public class CSVHelper
    {
        /// <summary>
        /// Returns a string containing the CSV representing the inputList
        /// </summary>
        /// <param name="inputList">List of dynamic objects.</param>
        /// <param name="cultureInfo">Default is InvariantCulture. This parameter is relevant when converting decimal numbers to string.</param>
        /// <param name="columnDelimiter">Default is ";"</param>
        /// <param name="nullString">Default is "null"</param>
        /// <param name="dateFormat">Default is "yyyy-MM-dd"</param>
        /// <returns></returns>
        public static string GetCSVString(List<dynamic> inputList, CultureInfo cultureInfo = null, string columnDelimiter = ";", string nullString = "null", string dateFormat = "yyyy-MM-dd HH:mm:ss")
        {
            if (cultureInfo == null)
            {
                cultureInfo = CultureInfo.InvariantCulture;
            }

            var outputList = new List<Dictionary<string, object>>();

            foreach (var item in inputList)
            {
                Dictionary<string, object> outputItem = new Dictionary<string, object>();
                flatten(item, outputItem, "");

                outputList.Add(outputItem);
            }

            List<string> headers = outputList.SelectMany(t => t.Keys).Distinct().ToList();

            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine(string.Join(columnDelimiter, headers.ToArray()));

            foreach (var item in outputList)
            {
                string line = "";

                foreach (string header in headers)
                {
                    if (item.ContainsKey(header))
                    {
                        if (item[header] == null)
                        {
                            line = line + nullString;
                        }
                        else if (item[header] is double)
                        {
                            line = line + ((double)item[header]).ToString(cultureInfo);
                        }
                        else if (item[header] is decimal)
                        {
                            line = line + ((decimal)item[header]).ToString(cultureInfo);
                        }
                        else if (item[header] is DateTime)
                        {
                            line = line + ((DateTime)item[header]).ToString(dateFormat);
                        }
                        else
                        {
                            line = line + item[header].ToString();
                        }
                    }

                    line = line + columnDelimiter;
                }

                stringBuilder.AppendLine(line);
            }

            return stringBuilder.ToString();
        }

        private static void flatten(dynamic item, Dictionary<string, object> outputItem, string prefix)
        {
            if (item == null)
            {
                return;
            }

            var properties = item.GetType().GetProperties();

            foreach (PropertyInfo propertyInfo in properties)
            {
                // Base case (primitive)
                if (!propertyInfo.PropertyType.Name.Contains("AnonymousType"))
                {
                    // Root item
                    if (prefix.Equals(""))
                    {
                        outputItem.Add(propertyInfo.Name, propertyInfo.GetValue(item));
                    }
                    // Nested item
                    else
                    {
                        outputItem.Add(prefix + "__" + propertyInfo.Name, propertyInfo.GetValue(item));
                    }
                }
                // Recursive case
                else
                {
                    flatten(propertyInfo.GetValue(item), outputItem, (prefix.Equals("") ? propertyInfo.Name : prefix + "__" + propertyInfo.Name));
                }
            }
        }
    }
}
