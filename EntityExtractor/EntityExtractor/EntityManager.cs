using EntityModels;
using Microsoft.FoodAndDrink.Services.Tools.Azure;
using Microsoft.WindowsAzure.Storage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityExtractor
{
    public class EntityManager
    {
        /// <summary>
        /// Cloud storage account
        /// </summary>
        /// private static readonly CloudStorageAccount EntityStorageAccount = CloudStorageAccount.Parse("DefaultEndpointsProtocol=https;AccountName=entityextractorstorage;AccountKey=LlVGLKNufD6ud8bfUHJ16pKNxbOjUhXDdzUrMoUNVqLOJaHj6mIJLE3MtVrSCDzbvygAJByeJKj5/CwVqKhEeg==");

        /// <summary>
        /// Get all entities
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        public static IEnumerable<FashionEntity> GetEntities(IEnumerable<KeyValuePair<string, string>> query)
        {
            KeyValuePair<string, string> kv;

            kv = query.FirstOrDefault(kvp => kvp.Key == "top");
            int? top = kv.Key != null ? (int?)int.Parse(kv.Value) : null;

            kv = query.FirstOrDefault(kvp => kvp.Key == "category");
            FashionCategory? category = kv.Key != null ? (FashionCategory?)Enum.Parse(typeof(FashionCategory), kv.Value, true) : null;

            // ToDO : Hard code top and provider for now
            top = 10;
            string provider = "Nordstorm";

            if (category == null)
            {
                throw new ArgumentException("Invalid category");
            }

            return TableManager.SearchByStartsWith<FashionEntity>(
                ConfigurationSettings.EntityStorageAccount,
                ExtractorConstants.ExtractedTable,
                string.Format(ExtractorConstants.ExtractionRowKeyFormat, Enum.GetName(typeof(FashionCategory), category), provider, string.Empty),
                top);
        }
    }
}
