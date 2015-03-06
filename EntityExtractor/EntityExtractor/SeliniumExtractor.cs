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

    public class SeliniumExtractor
    {
        public void Run()
        {
            IWebDriver driver = null;

            try
            {
                driver = new FirefoxDriver();
                driver.Navigate().GoToUrl("http://www.google.com");

                IWebElement query = driver.FindElement(By.Name("q"));
                query.SendKeys("cheese");

                query.Submit();

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(d => d.Title.ToLower().StartsWith("cheese"));

                Console.WriteLine(driver.Title);
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
        }

        public void RunNordstorm()
        {
            IWebDriver driver = null;

            try
            {
                driver = new FirefoxDriver();
                driver.Navigate().GoToUrl("http://shop.nordstrom.com/");

                IWebElement query = driver.FindElement(By.Name("keyword"));
                query.SendKeys("handbag");

                query.Submit();

                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
                wait.Until(d => d.Title.ToLower().StartsWith("handbag"));

                IWebElement selectWebElement = driver.FindElement(By.Name("sort"));
                SelectElement select = new SelectElement(selectWebElement);
                select.SelectByText("Sort by newest");
                Thread.Sleep(TimeSpan.FromMilliseconds(10));

                var elements = driver.FindElements(By.CssSelector("div .fashion-item"));

                FashionEntity entity = null;
                foreach (var e in elements.Take(5))
                {
                     if (Nordstorm.TryCreateEntity(FashionCategory.HandBag, e, out entity))
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
        }
    }
}
