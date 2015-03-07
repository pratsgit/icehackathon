using Microsoft.WindowsAzure.Storage.Table;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.FoodAndDrink.Services.Tools.Azure
{
    public static class CloudExtensions
    {
        public static IEnumerable<TElement> StartsWith<TElement>(this CloudTable table, string partitionKey, string searchStr, string columnName = "RowKey", int? top = null) where TElement : ITableEntity, new()
        {
            if (string.IsNullOrEmpty(searchStr)) return null;

            char lastChar = searchStr[searchStr.Length - 1];
            char nextLastChar = (char)((int)lastChar + 1);
            string nextSearchStr = searchStr.Substring(0, searchStr.Length - 1) + nextLastChar;

            string filterString = string.Empty;

            if (!string.IsNullOrEmpty(searchStr))
            {
                filterString = TableQuery.CombineFilters(
                    TableQuery.GenerateFilterCondition(columnName, QueryComparisons.GreaterThanOrEqual, searchStr),
                    TableOperators.And,
                    TableQuery.GenerateFilterCondition(columnName, QueryComparisons.LessThan, nextSearchStr)
                    );
            }

            if (!string.IsNullOrEmpty(partitionKey))
            {
                filterString = TableQuery.CombineFilters(
                    TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, partitionKey),
                    TableOperators.And,
                    filterString);
            }

            var query = new TableQuery<TElement>().Where(filterString);

            if (top.HasValue && top.Value > 0)
            {
                query.Take(top.Value);
            }

            return table.ExecuteQuery<TElement>(query);
        }
    }
}
