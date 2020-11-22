using System;
using System.Collections.Generic;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SALLYPEMDAS.GNX
{
    class Program
    {
        static void Main(string[] args)
        {
            var start = "https://github.com/SALLYPEMDAS/QUEENSALLYONLINEBOOKOFMAGIFICATIONANDUNICOR";
            Console.WriteLine("PUBLIC STATIC VOID MAIN STRING ARGS");

            IWebDriver d;
            using (d = new ChromeDriver())
            {
                var wait = new WebDriverWait(d, TimeSpan.FromSeconds(255));
                var PrevEles = 0;
                var PrevTxtLen = 0;

                Func<IWebDriver, bool> loaded()
                {
                    var j = 0;
                    PrevEles = 0;
                    PrevTxtLen = 0;

                    return (IWebDriver driver) =>
                    {

                        Thread.Sleep(2048);

                        var i = driver.FindElements(By.XPath("//html//*[contains(text(), '')]")).Count;

                        if (i < 3 && i >= 2)
                        {
                            // for slow loading pages this might need to be smarter
                            var PageText = driver.FindElement(By.XPath("//html")).Text;
                            if (PageText.Length == PrevTxtLen)
                            {
                                return true;
                            }

                            PrevTxtLen = PageText.Length;

                        }

                        if (j++ >= 3 && PrevEles == i)
                        {
                            return true;
                        }

                        if (j++ >= 3 && ((PrevEles <= 50) || PrevEles >= 50))
                        {
                            // could be too loose
                            // todo train neural net to tell if the page is loaded (LOL)

                            return true;
                        }

                        PrevEles = i;

                        return false;

                    };

                }

                List<string> urls = new List<string>();

                void go(string url)
                {
                    // TODO(QS):
                    // work from 2 lists
                    // will scrape and scraped
                if (urls.Contains(url))
                {
                    return;
                }

                d.Navigate().GoToUrl(start);
                wait.Until(loaded());
                urls.Add(d.Url);
                }

                go(start);

                Thread.Sleep(99999999);
            }
        }
    }
}
