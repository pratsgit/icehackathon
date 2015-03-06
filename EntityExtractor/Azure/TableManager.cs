// --------------------------------------------------------------------------------------------------------------------
// <copyright file="TableManager.cs" company="Microsoft Corporation">
// Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Microsoft.FoodAndDrink.Services.Tools.Azure
{
    using System;
    using System.Collections.Generic;

    using Microsoft.WindowsAzure.Storage;
    using Microsoft.WindowsAzure.Storage.RetryPolicies;
    using Microsoft.WindowsAzure.Storage.Table;

    /// <summary>
    /// Azure table operation manager
    /// </summary>
    public static class TableManager
    {
        /// <summary>
        /// Retrieves a single entity
        /// </summary>
        /// <typeparam name="T">Table entity</typeparam>
        /// <param name="account">Storage account</param>
        /// <param name="name">Table name</param>
        /// <param name="partitionKey">Partition key</param>
        /// <param name="rowKey">Row key</param>
        /// <returns>Table entity object</returns>
        public static T RetrieveSingle<T>(CloudStorageAccount account, string name, string partitionKey, string rowKey) where T : TableEntity, new()
        {
            if (account == null)
            {
                throw new ArgumentNullException("account", "account cannot be null.");
            }

            CloudTableClient cloudTableClient = CreateCloudTableClientWithLinearRetry(account);
            CloudTable table = cloudTableClient.GetTableReference(name);
            TableOperation operation = TableOperation.Retrieve<T>(partitionKey, rowKey);
            TableResult result = table.Execute(operation);

            if ((result == null) || (result.Result == null))
            {
                return default(T);
            }
            else
            {
                return (T)result.Result;
            }
        }

        /// <summary>
        /// Retrieves entities
        /// </summary>
        /// <typeparam name="T">Table entity</typeparam>
        /// <param name="account">Storage account</param>
        /// <param name="name">Table name</param>
        /// <param name="partitionKey">Partition key</param>
        /// <param name="rowKey">Row key</param>
        /// <returns>Table entities</returns>
        public static IEnumerable<T> Retrieve<T>(CloudStorageAccount account, string name, string partitionKey = null, string rowKey = null) where T : ITableEntity, new()
        {
            if (account == null)
            {
                throw new ArgumentNullException("account", "account cannot be null.");
            }

            CloudTableClient cloudTableClient = CreateCloudTableClientWithLinearRetry(account);
            CloudTable table = cloudTableClient.GetTableReference(name);

            TableQuery<T> queryFilter = new TableQuery<T>();
            var partitionKeyFilter = !string.IsNullOrEmpty(partitionKey) ? TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey) : null;
            var rowKeyFilter = !string.IsNullOrEmpty(rowKey) ? TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, rowKey) : null;

            if (partitionKeyFilter != null && rowKeyFilter != null)
            {
                queryFilter.Where(TableQuery.CombineFilters(partitionKeyFilter, TableOperators.And, rowKeyFilter));
            }
            else if (partitionKeyFilter != null)
            {
                queryFilter.Where(partitionKeyFilter);
            }
            else if (rowKeyFilter != null)
            {
                queryFilter.Where(rowKeyFilter);
            }

            return table.ExecuteQuery(queryFilter);
        }

        /// <summary>
        /// Search by starts with in a column
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="account"></param>
        /// <param name="table"></param>
        /// <param name="partitionKey"></param>
        /// <param name="searchString"></param>
        /// <param name="searchColumn"></param>
        /// <returns></returns>
        public static IEnumerable<T> SearchByStartsWith<T>(CloudStorageAccount account, string tableName, string partitionKey, string searchString, string searchColumn = "RowKey") where T : ITableEntity, new()
        {
            if (account == null)
            {
                throw new ArgumentNullException("account");
            }

            if (string.IsNullOrEmpty(tableName))
            {
                throw new ArgumentNullException("tableName");
            }

            if (string.IsNullOrEmpty(partitionKey))
            {
                throw new ArgumentNullException("partitionKey");
            }

            if (string.IsNullOrEmpty(searchString))
            {
                throw new ArgumentNullException("searchString");
            }

            if (string.IsNullOrEmpty(searchColumn))
            {
                throw new ArgumentNullException("searchColumn");
            }

            CloudTableClient cloudTableClient = CreateCloudTableClientWithLinearRetry(account);
            CloudTable table = cloudTableClient.GetTableReference(tableName);
            return table.StartsWith<T>(partitionKey, searchString, searchColumn);
        }

        /// <summary>
        /// Insert an entity into Azure table
        /// </summary>
        /// <param name="account">cloud storage account</param>
        /// <param name="name">table name</param>
        /// <param name="entity">entity to insert</param>
        /// <returns>table result</returns>
        public static TableResult Insert(CloudStorageAccount account, string name, ITableEntity entity)
        {
            if (account == null)
            {
                throw new ArgumentNullException("account", "account cannot be null.");
            }

            ValidateTableEntity(entity);

            CloudTableClient cloudTableClient = CreateCloudTableClientWithLinearRetry(account);
            CloudTable table = cloudTableClient.GetTableReference(name);
            TableOperation operation = TableOperation.Insert(entity);
            return table.Execute(operation);
        }

        /// <summary>
        /// Creates Cloud Table Client with Linear Retry Policy
        /// </summary>
        /// <param name="storageAccount">Cloud Storage Account</param>
        /// <returns>Cloud Table Client</returns>
        private static CloudTableClient CreateCloudTableClientWithLinearRetry(CloudStorageAccount storageAccount)
        {
            if (storageAccount != null)
            {
                CloudTableClient cloudTableClient = storageAccount.CreateCloudTableClient();
                cloudTableClient.DefaultRequestOptions = new TableRequestOptions() { RetryPolicy = new LinearRetry(TimeSpan.FromMilliseconds(200), 2) };
                return cloudTableClient;
            }

            return null;
        }

        /// <summary>
        /// Validates a table entity, its partition and row keys 
        /// </summary>
        /// <param name="entity">Table entity</param>
        private static void ValidateTableEntity(ITableEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity", "Entity cannot be null.");
            }

            if (entity.PartitionKey == null)
            {
                throw new ArgumentNullException("entity", "Entity partition key cannot be null.");
            }

            if (entity.PartitionKey.Trim().Length == 0)
            {
                throw new ArgumentException("Entity partition key cannot be empty.");
            }

            if (entity.RowKey == null)
            {
                throw new ArgumentNullException("entity", "Entity row key cannot be null.");
            }

            if (entity.RowKey.Trim().Length == 0)
            {
                throw new ArgumentException("Entity row key cannot be empty.");
            }
        }
    }
}
