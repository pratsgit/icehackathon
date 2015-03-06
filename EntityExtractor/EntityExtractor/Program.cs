using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EntityModels;
using Microsoft.FoodAndDrink.Services.Tools.Azure;

namespace EntityExtractor
{
    class Program
    {
        static void Main(string[] args)
        {
            InsertTestEntity();
            // RunExtractor();
            // QueryExtractor();

            Console.ReadLine();
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

        private static void InsertTestEntity()
        {
            FashionEntity entity = new FashionEntity();
            entity.FashionCategory = Enum.GetName(typeof(FashionCategory), FashionCategory.LadiesShoe);
            entity.ImageHref = "test";
            entity.OriginalPrice = 10;
            entity.ETag = "*";

            long number = DateTime.UtcNow.ToEpocTime() / (60 * 60);
            entity.PartitionKey = (number * 60 * 60).ToString();
            entity.RowKey = string.Format(ExtractorConstants.ExtractionRowKeyFormat, Enum.GetName(typeof(FashionCategory), FashionCategory.LadiesShoe), "Nordstorm", DateTime.UtcNow.Ticks);
            
            TableManager.Insert(ConfigurationSettings.EntityStorageAccount, ExtractorConstants.ExtractedTable, entity);
        }

        private static void RunExtractor()
        {
            var extractor = new SeliniumExtractor();
            extractor.RunNordstorm();
            Console.ReadLine();
        }
    }
}
