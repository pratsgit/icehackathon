namespace EntityExtractor
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using OpenQA.Selenium;
    using OpenQA.Selenium.Firefox;
    using OpenQA.Selenium.Support.UI;
    using System.Threading;

    using EntityModels;

    public class NordstormExtractor : IEntityExtractor
    {
        private const string HomePageUrl = "http://shop.nordstrom.com/";

        private FashionCategory fashionCategory;

        public FashionCategory FashionCategory
        {
            get 
            {
                return this.fashionCategory;
            }
        }

        public FashionProvider FashionProvider
        {
            get 
            { 
                return FashionProvider.Nordstorm;
            }
        }

        public IList<FashionEntity> RunExtractor(FashionCategory fashionCategory, EntityExtractorFilter filterCategory)
        {
            IWebDriver driver = null;

            try
            {
                driver = new FirefoxDriver();
                driver.Navigate().GoToUrl(HomePageUrl);

                string fashionSearchCategory = ExtractorUtility.SearchStringForCategory(fashionCategory);

                var elements = RunExtractor(driver, fashionSearchCategory);

                FashionEntity entity = null;
                foreach (var e in elements.Take(5))
                {
                    if (ParseEntity(FashionCategory.HandBag, e, out entity))
                    {
                        Console.WriteLine(string.Empty);
                        Console.WriteLine(string.Format("Category - {0}", Enum.Parse(typeof(FashionCategory), entity.FashionCategory)));
                        Console.WriteLine(string.Format("Id - {0}", entity.ProviderId));
                        Console.WriteLine(string.Format("Image - {0}", entity.ImageHref));
                        Console.WriteLine(string.Empty);
                    }
                }

                Console.WriteLine("");
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (driver != null)
                {
                    driver.Quit();
                }
            }

            return null;
        }

        private System.Collections.ObjectModel.ReadOnlyCollection<IWebElement> RunExtractor(IWebDriver driver, string fashionSearchCategory)
        {
            IWebElement query = driver.FindElement(By.Name("keyword"));
            query.SendKeys(fashionSearchCategory);

            query.Submit();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(d => d.Title.ToLower().StartsWith(fashionSearchCategory));

            IWebElement selectWebElement = driver.FindElement(By.Name("sort"));
            SelectElement select = new SelectElement(selectWebElement);
            select.SelectByText("Sort by newest");
            Thread.Sleep(TimeSpan.FromMilliseconds(10));

            var elements = driver.FindElements(By.CssSelector("div .fashion-item"));
            return elements;
        }

        /// <summary>
        /// Create fashion entity from selenium web element
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        internal bool ParseEntity(FashionCategory category, IWebElement element, out FashionEntity entityOutput)
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
