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
    using System.Globalization;

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
                foreach (var e in elements.Take(100))
                {
                    if (ParseEntity(FashionCategory.HandBag, e, out entity))
                    {
                        Console.WriteLine(string.Empty);
                        Console.WriteLine(string.Format("Category - {0}", Enum.Parse(typeof(FashionCategory), entity.FashionCategory)));
                        Console.WriteLine(string.Format("PartitionKey - {0}", entity.PartitionKey));
                        Console.WriteLine(string.Format("RowKey - {0}", entity.RowKey));
                        Console.WriteLine(string.Format("Provider - {0}", entity.Provider));
                        Console.WriteLine(string.Format("Id - {0}", entity.ProviderId));
                        Console.WriteLine(string.Format("Image - {0}", entity.ImageHref));
                        Console.WriteLine(string.Format("ItemHref - {0}", entity.ItemHref));
                        Console.WriteLine(string.Format("Original Price - {0}", entity.OriginalPrice));
                        Console.WriteLine("--------------------------------------------------------------");
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
            Thread.Sleep(TimeSpan.FromMilliseconds(100));

            // Scroll down the browser all the way to end, for the images to show up
            IJavaScriptExecutor jse = (IJavaScriptExecutor)driver;
            jse.ExecuteScript("window.scrollBy(0,2000)", "");
            jse.ExecuteScript("window.scrollBy(0,2000)", "");
            jse.ExecuteScript("window.scrollBy(0,2000)", "");
            jse.ExecuteScript("window.scrollBy(0,2000)", "");
            jse.ExecuteScript("window.scrollBy(0,2000)", "");
            jse.ExecuteScript("window.scrollBy(0,2000)", "");
            jse.ExecuteScript("window.scrollBy(0,2000)", "");

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

                entity.Provider = Enum.GetName(typeof(FashionProvider), FashionProvider.Nordstorm);
                entity.ProviderId = element.GetAttribute("id");
                entity.ItemHref = element.FindElement(By.CssSelector("a")).GetAttribute("href");
                entity.ImageHref = element.FindElement(By.CssSelector("img")).GetAttribute("src");
                entity.Title = element.FindElement(By.CssSelector("div .info")).FindElement(By.CssSelector("a")).Text;
                decimal price = 0;
                if (decimal.TryParse(element.FindElement(By.CssSelector(".price")).Text, NumberStyles.AllowCurrencySymbol | NumberStyles.AllowDecimalPoint | NumberStyles.Currency, new CultureInfo("en-us"), out price))
                {
                    entity.OriginalPrice = (double)price;
                }

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
