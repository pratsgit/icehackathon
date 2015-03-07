using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityExtractor
{
    public static class ExtractorConstants
    {
        /// <summary>
        /// Table name where entities are stored
        /// </summary>
        public const string ExtractedTable = "extractedentity";

        /// <summary>
        /// Extraction partition key interval
        /// </summary>
        public const int ExtractionPartionKeyInterval = 60;

        /// <summary>
        /// Row key format for the entity
        /// {FashionCategory}_{Provider}_{Id}
        /// </summary>
        public const string ExtractionRowKeyFormat = "{0}_{1}_{2}";

        /// <summary>
        /// Primary key constant
        /// </summary>
        public static readonly int EpochTimePrimaryKey = 24 * 60 * 60;
    }
}
