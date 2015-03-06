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
        }
    }
}
