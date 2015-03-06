using EntityModels;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityExtractor
{
    public class Nordstorm
    {
        /// <summary>
        /// Create fashion entity from selenium web element
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        public static bool TryCreateEntity(FashionCategory category, IWebElement element, out FashionEntity entityOutput)
        {
            entityOutput = null;

            try
            {
                FashionEntity entity = new FashionEntity();
                float val = 0;
                entity.FashionCategory = Enum.GetName(typeof(FashionCategory), category);

                entity.ProviderId = element.GetAttribute("id");
                //// entity.ItemHref = element.FindElement(By.CssSelector("a .fashion-href")).GetAttribute("href");
                entity.ImageHref = element.FindElement(By.CssSelector("img")).GetAttribute("src");

                ////entity.Title = element.FindElement(By.CssSelector("div .info")).FindElement(By.CssSelector("a .title")).Text;
                ////if (float.TryParse(element.FindElement(By.CssSelector("div .info")).FindElement(By.CssSelector("regular")).Text, NumberStyles.Currency, CultureInfo.InvariantCulture, out val))
                ////{
                ////    entity.OriginalPrice = val;
                ////}

                entityOutput = entity;

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
