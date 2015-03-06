using EntityModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityExtractor
{
    public static class ExtractorUtility
    {
        public static string SearchStringForCategory(FashionCategory category)
        {
            switch (category)
            {
                case FashionCategory.Dress:
                    return "dresses";
                case FashionCategory.HandBag:
                    return "handbag";
                case FashionCategory.LadiesShoe:
                    return "shoe";
                default:
                    throw new Exception("invalid category");
            }
        }
    }
}
