using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using EntityModels;
using Microsoft.FoodAndDrink.Services.Tools.Azure;
using EntityExtractor.Extractor;

using HtmlAgilityPack;

namespace EntityExtractor
{
    class Program
    {
        static void Main(string[] args)
        {
            // ExtractUsingHtmlAgilityPack();

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
                "LadiesShoe_Nordstorm",
                10))
            {
                Console.WriteLine(e.RowKey + ":" + e.Title);
            }
        }

        private static void ExtractUsingHtmlAgilityPack()
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument document = web.Load(@"http://shop.nordstrom.com/s/valentino-rockstud-camera-crossbody-bag/3912322?origin=keywordsearch");
            
            IList<HtmlNode> nodes = document.QuerySelector("#image-viewer").QuerySelectorAll("img");
            IEnumerable<string> relatedImages = nodes.ToList().ConvertAll<string>(n => n.GetAttributeValue("src", string.Empty).Replace("Mini", "Large")).Distinct();

            Console.ReadLine();
        }
    }
}
