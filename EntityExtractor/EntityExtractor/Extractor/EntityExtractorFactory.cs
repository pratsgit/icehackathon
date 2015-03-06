using EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityExtractor.Extractor
{
    public static class EntityExtractorFactory
    {
        public static IEntityExtractor CreateNewInstance(FashionProvider fashionProvider)
        {
            switch (fashionProvider)
            {
                case FashionProvider.Nordstorm:
                    return new NordstormExtractor();
                case FashionProvider.NetAPorter:
                    throw new NotImplementedException();
                default:
                    throw new InvalidOperationException("Invalid provider input");
            }
        }
    }
}
