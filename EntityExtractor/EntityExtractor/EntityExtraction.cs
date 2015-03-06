﻿using EntityExtractor.Extractor;
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

            Console.ReadLine();
        }

        private void InsertEntity(FashionEntity entity)
        {
            // Set PartitionKey and RowKey
            long number = DateTime.UtcNow.ToEpocTime() / (60 * 60);
            entity.PartitionKey = (number * 60 * 60).ToString();
            entity.RowKey = string.Format(ExtractorConstants.ExtractionRowKeyFormat, Enum.GetName(typeof(FashionCategory), FashionCategory.LadiesShoe), "Nordstorm", DateTime.UtcNow.Ticks);

            entity.ETag = "*";

            TableManager.Insert(ConfigurationSettings.EntityStorageAccount, ExtractorConstants.ExtractedTable, entity);
        }
    }
}