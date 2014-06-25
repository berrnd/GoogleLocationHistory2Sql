using System;
using CommandLine;

namespace GoogleLocationHistory2Sql
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            try
            {
                Args parsedArgs = new Args();
                if (Parser.Default.ParseArguments(args, parsedArgs))
                {
                    DateTime maxTimestamp;
                    if (!DateTime.TryParse(parsedArgs.MaxTimestamp, out maxTimestamp))
                        throw new ArgumentException("Is not a parseble DateTime value", "max-timestamp");

                    GoogleLocationHistoryConverter.Convert(parsedArgs.InputFielPath,
                        parsedArgs.OutputFilePath,
                        parsedArgs.FormatString,
                        maxTimestamp,
                        parsedArgs.NullReplaceValue);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Format("{0}\n{1}", ex.GetType().Name, ex.Message));
            }
            finally
            {
                Console.WriteLine();
                Console.WriteLine("Press any key to exit...");
                Console.ReadKey();
            }
        }
    }
}
