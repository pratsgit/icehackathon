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
        public static string ExtractedTable = "extractedentity";

        /// <summary>
        /// Extraction partition key interval
        /// </summary>
        public static int ExtractionPartionKeyInterval = 60;

        /// <summary>
        /// Row key format for the entity
        /// {FashionCategory}_{Provider}_{Id}
        /// </summary>
        public static string ExtractionRowKeyFormat = "{0}_{1}_{2}";
    }
}
