using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium.Interactions;//鼠标


namespace UnitTestProject3
{
    [TestClass]
    public class UnitTest1
    {

        private static IWebDriver driver;


        //初始化
        [ClassInitialize]
        public static void InitializeClass(TestContext testContext)
        {
            //打开浏览器
            driver = new FirefoxDriver();

            //隐式等待
            //driver.Manage().Timeouts().ImplicitWait=TimeSpan.FromSeconds(3);

        }
        //测试方法
        [TestMethod]
        public void TestMethod1()
        {

            //打开网址,进入登录页
            driver.Navigate().GoToUrl("https://www.kuaikanmanhua.com/");
            driver.FindElement(By.LinkText("登录")).Click();


            //输入用户名密码，登录
            driver.FindElement(By.XPath("//div[@id='__layout']/div/div/div[2]/div/div/form/div/div/div[2]/span")).Click();
            driver.FindElement(By.XPath("(//input[@type='text'])[2]")).Clear();
            driver.FindElement(By.XPath("(//input[@type='text'])[2]")).SendKeys("17694803242");
            driver.FindElement(By.XPath("//div[@id='__layout']/div/div/div[2]/div/div/form/div[2]/div/span")).Click();
            driver.FindElement(By.XPath("//input[@type='password']")).Clear();
            driver.FindElement(By.XPath("//input[@type='password']")).SendKeys("Ljj123456");
            driver.FindElement(By.XPath("//input[@value='登录']")).Click();
            Thread.Sleep(3000);


            //验证登录成功
            //验证title
            Assert.AreEqual("快看漫画_官方漫画大全免费在线观看", driver.Title);
            //验证网址
            Assert.AreEqual("https://www.kuaikanmanhua.com/", driver.Url);
            //验证登录按钮是否存在
            By by = By.XPath("//input[@value='登录']");
            Assert.AreEqual(false, IsElementPresent(by));
            //验证新页面元素是否存在
            by = By.XPath("//*[@id='BanneContentSlider']");
            Assert.AreEqual(true, IsElementPresent(by));

            

            //退出登录
            //鼠标悬停,只有第一次成功
            //Actions action = new Actions(driver);
            //action.MoveToElement("悬停位置").Perform();
            //driver.FindElement(By.XPath("/html/body/div/div[2]/div/div/div[1]/div[2]/div/div[2]/div[2]/div/div[3]/a[2]/div")).Click();
            //JS点击悬停display元素
            IJavaScriptExecutor js = (IJavaScriptExecutor)driver;
            js.ExecuteScript("arguments[0].click();", driver.FindElement(By.XPath("/html/body/div/div[2]/div/div/div[1]/div[2]/div/div[2]/div[2]/div/div[3]/a[2]/div")));
            Thread.Sleep(1000);


        }
        //判断元素是否存在
        private bool IsElementPresent(By by)
        {
            try
            {
                driver.FindElement(by);
                return true;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

        //清理，每次测试方法结束后运行
        [ClassCleanup]
        public static void CleanupClass()
        {
            try
            {
                //driver.Quit();
                driver.Close();
                driver.Dispose();
            }
            catch (Exception e)
            {
                Console.WriteLine("错误:" + e);
            }
        }








    }
}
