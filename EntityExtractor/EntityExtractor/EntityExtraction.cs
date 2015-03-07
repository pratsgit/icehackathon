using EntityExtractor.Extractor;
using EntityModels;
using Microsoft.FoodAndDrink.Services.Tools.Azure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityExtractor
{
    /// <summary>
    /// Entity extraction program that runs in background and stores entities in Azure
    /// </summary>
    public class EntityExtraction
    {
        private static EntityExtraction entityExtraction = new EntityExtraction();

        private EntityExtraction()
        {
        }

        public static EntityExtraction Instance
        {
            get
            {
                return entityExtraction;
            }
        }

        /// <summary>
        /// Runs periodically to extract entities called by a scheduler
        /// </summary>
        public void Start()
        {
            this.RunExtractor(FashionProvider.Nordstorm, FashionCategory.HandBag);
            this.RunExtractor(FashionProvider.Nordstorm, FashionCategory.LadiesShoe);
        }

        /// <summary>
        /// Stop request
        /// </summary>
        public void Stop()
        {
            throw new NotImplementedException();
        }

        private void RunExtractor(FashionProvider provider, FashionCategory category)
        {
            IEntityExtractor extractor = EntityExtractorFactory.CreateNewInstance(provider);
            var entities = extractor.RunExtractor(category);

            foreach (var entity in entities)
            {
                InsertEntity(entity);
            }
        }

        private void InsertEntity(FashionEntity entity)
        {
            // Set PartitionKey and RowKey
            long number = DateTime.UtcNow.ToEpocTime() / ExtractorConstants.EpochTimePrimaryKey;
            entity.PartitionKey = (number * ExtractorConstants.EpochTimePrimaryKey).ToString();
            entity.RowKey = string.Format(ExtractorConstants.ExtractionRowKeyFormat, entity.FashionCategory, entity.Provider, entity.ProviderId);

            entity.ETag = "*";

            TableManager.InsertOrReplace(ConfigurationSettings.EntityStorageAccount, ExtractorConstants.ExtractedTable, entity);
        }
    }
}
