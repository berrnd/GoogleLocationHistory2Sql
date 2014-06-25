using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace GoogleLocationHistory2Sql
{
    public class GoogleLocationHistoryConverter
    {
        public static void Convert(string inputFilePath, string outputFilePath, string formatString, DateTime maxImportDate, string nullReplaceValue)
        {
            if (!File.Exists(inputFilePath))
                throw new FileNotFoundException(String.Format("Input file not found: {0}", inputFilePath));

            Console.WriteLine("Parsing JSON file...");

            int counter = 0;
            TextReader jsonFile = new StreamReader(inputFilePath);
            dynamic json = JsonConvert.DeserializeObject(jsonFile.ReadToEnd());
            List<string> importedtimestamps = new List<string>();

            using (StreamWriter writer = new StreamWriter(outputFilePath))
            {
                foreach (dynamic location in json.locations)
                {
                    DateTime timestamp = DateTimeFromUnixTimestampMillis(long.Parse(((string)location.timestampMs).Replace("\"", "")));
                    string timestampS = timestamp.ToString("yyyy-MM-dd hh:mm:ss");
                    double latitude = double.Parse(location.latitudeE7.ToString()) / 10000000;
                    double longitude = double.Parse(location.longitudeE7.ToString()) / 10000000;

                    double accuracy;
                    if (location.accuracy != null)
                        accuracy = double.Parse(location.accuracy.ToString());
                    else
                        accuracy = -1;

                    if (timestamp < maxImportDate)
                    {
                        if (!importedtimestamps.Contains(timestampS))
                        {
                            string accuracyS;
                            if (accuracy == -1)
                                accuracyS = nullReplaceValue;
                            else
                                accuracyS = accuracy.ToString().Replace(",", ".");

                            formatString = formatString.Replace("{timestamp}", "{0}")
                                .Replace("{latitude}", "{1}")
                                .Replace("{longitude}", "{2}")
                                .Replace("{accuracy}", "{3}");

                            string insert = String.Format(formatString, timestampS, latitude.ToString().Replace(",", "."), longitude.ToString().Replace(",", "."), accuracyS);
                            writer.WriteLine(insert);
                        }
                    }

                    importedtimestamps.Add(timestampS);

                    counter++;
                    writer.Flush();
                    Console.Write(String.Format("\r{0} of {1} location points processed", counter, json.locations.Count));
                }
            }

            Console.WriteLine();
            Console.WriteLine("Ready");
        }

        private static readonly DateTime UnixEpoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);

        private static DateTime DateTimeFromUnixTimestampMillis(long milliseconds)
        {
            return UnixEpoch.AddMilliseconds(milliseconds);
        }
    }
}
