using System;
using CommandLine;
using CommandLine.Text;

namespace GoogleLocationHistory2Sql
{
    public class Args
    {
        [Option('i', "input", Required = true, HelpText = "Path to the input JSON file")]
        public string InputFielPath { get; set; }

        [Option('o', "output", Required = true, HelpText = "Path to the output file")]
        public string OutputFilePath { get; set; }

        [Option('f', "format-string", Required = true, HelpText = "The format string, you can use the following placeholders: {timestamp}, {latitude}, {longitude}, {accuracy}")]
        public string FormatString { get; set; }

        [Option('t', "max-timestamp", Required = false, DefaultValue = "2999-12-31 23:59:59", HelpText = "Ignore location points with a timestamp after this date")]
        public string MaxTimestamp { get; set; }
        
        [Option('r', "replace-null", Required = false, DefaultValue = "NULL", HelpText = "Replace value for fields which are NULL or not present")]
        public String NullReplaceValue { get; set; }

        [HelpOption]
        public string GetUsage()
        {
            return HelpText.AutoBuild(this,
              (HelpText current) => HelpText.DefaultParsingErrorsHandler(this, current));
        }
    }
}
