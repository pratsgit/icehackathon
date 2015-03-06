using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EntityModels;
using Microsoft.FoodAndDrink.Services.Tools.Azure;
using EntityExtractor.Extractor;

namespace EntityExtractor
{
    class Program
    {
        static void Main(string[] args)
        {
            // InsertTestEntity();
            RunExtractor();
            // QueryExtractor();

            Console.ReadLine();
        }

        private static void RunExtractor()
        {
            EntityExtraction.Instance.Start();
        }

        private static void QueryExtractor()
        {
            foreach (var e in TableManager.SearchByStartsWith<FashionEntity>(
                ConfigurationSettings.EntityStorageAccount, 
                ExtractorConstants.ExtractedTable, 
                // DateTime.UtcNow.ToEpocTimeByMinuteCategory(TimeSpan.FromMinutes(ExtractorConstants.ExtractionPartionKeyInterval)).ToString(),
                "1",
                "LadiesShoe_Nordstorm"))
            {
                Console.WriteLine(e.RowKey + ":" + e.Title);
            }
        }
    }
}
