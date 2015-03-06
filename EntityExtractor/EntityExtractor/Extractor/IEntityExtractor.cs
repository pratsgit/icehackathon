using EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityExtractor
{
    public interface IEntityExtractor
    {
        /// <summary>
        /// Returns the fashion category
        /// </summary>
        FashionCategory FashionCategory { get; }

        /// <summary>
        /// Returns the fashion provider
        /// </summary>
        FashionProvider FashionProvider { get; }

        /// <summary>
        /// Run entity extractor
        /// </summary>
        /// <param name="fashionCategory"></param>
        /// <param name="filterCategory"></param>
        /// <returns></returns>
        IList<FashionEntity> RunExtractor(FashionCategory fashionCategory, EntityExtractorFilter filterCategory = null);
    }
}
