using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityExtractor
{
    /// <summary>
    /// Filter specifiers for the Entity Extractor
    /// </summary>
    public class EntityExtractorFilter
    {
        public long? MaxLimit { get; set; }

        public string LastExtractedId { get; set; }
    }
}
