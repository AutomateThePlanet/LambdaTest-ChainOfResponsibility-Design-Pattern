using System.Collections.Concurrent;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ChainOfResponsibility.ExceptionAnalysation;
using ChainOfResponsibility.ExceptionAnalysation.Contracts;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Chromium;
using OpenQA.Selenium.DevTools;
using OpenQA.Selenium.DevTools.V120.Performance;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Safari;
using OpenQA.Selenium.Support.Events;
using WebDriverManager.DriverConfigs.Impl;

namespace ChainOfResponsibility.FourthVersion;

public class DriverAdapter : IDriver
{
    private IWebDriver _webDriver;
    private WebDriverWait _webDriverWait;
    private IJavaScriptEngine _monitor;
    private DevToolsSession _devToolsSession;
    private INetwork _networkInterceptor;
    private IExceptionAnalysationHandler _primaryExceptionHandler;

    public DriverAdapter()
    {
        InitializeExceptionAnalysisHandlers();
    }    

    public string Url => _webDriver.Url;

    public string HtmlSource => _webDriver.PageSource;
    public List<string> ConsoleMessages { init; get; } = new List<string>();
    public List<string> JavaScriptErrors { init; get; } = new List<string>();
    public ConcurrentBag<HttpRequestData> RequestsHistory { init; get; } = new ConcurrentBag<HttpRequestData>();
    public ConcurrentBag<HttpResponseData> ResponsesHistory { init; get; } = new ConcurrentBag<HttpResponseData>();

    public async Task Start(Browser browser)
    {
        switch (browser)
        {
            case Browser.Chrome:
                new WebDriverManager.DriverManager().SetUpDriver(new ChromeConfig());
                _webDriver = new ChromeDriver();
                break;
            case Browser.Firefox:
                new WebDriverManager.DriverManager().SetUpDriver(new FirefoxConfig());
                _webDriver = new FirefoxDriver();
                break;
            case Browser.Edge:
                new WebDriverManager.DriverManager().SetUpDriver(new EdgeConfig());
                _webDriver = new EdgeDriver();
                break;
            case Browser.Safari:
                _webDriver = new SafariDriver();
                break;
            case Browser.InternetExplorer:
                new WebDriverManager.DriverManager().SetUpDriver(new InternetExplorerConfig());
                _webDriver = new InternetExplorerDriver();
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(browser), browser, null);
        }

        _webDriver.Manage().Window.Maximize();
        _webDriverWait = new WebDriverWait(_webDriver, TimeSpan.FromSeconds(30));

        _monitor = new JavaScriptEngine(_webDriver);

        _monitor.JavaScriptConsoleApiCalled += (sender, e) =>
        {
            ConsoleMessages.Add(e.MessageContent);
        };

        _monitor.JavaScriptExceptionThrown += (sender, e) =>
        {
            JavaScriptErrors.Add(e.Message);
        };

        await _monitor.StartEventMonitoring();

        if (browser.Equals(Browser.Chrome) || browser.Equals(Browser.Edge))
        {
            _devToolsSession = ((ChromiumDriver)_webDriver).GetDevToolsSession();
            var enableCommand = new EnableCommandSettings();
            await _devToolsSession.SendCommand(enableCommand);
        }

        var requestHandler = new NetworkRequestHandler()
        {
            RequestMatcher = (d) =>
            {
                RequestsHistory.Add(d);
                return true;
            },
            RequestTransformer = (r) => { return r; }
        };
        var responsesHandler = new NetworkResponseHandler()
        {
            ResponseMatcher = (d) =>
            {
                ResponsesHistory.Add(d);
                return true;
            },
            ResponseTransformer = (r) => { return r; }
        };

        _networkInterceptor = _webDriver.Manage().Network;
        _networkInterceptor.AddRequestHandler(requestHandler);
        _networkInterceptor.AddResponseHandler(responsesHandler);
        await _networkInterceptor.StartMonitoring();

        var firingDriver = new EventFiringWebDriver(_webDriver);
        firingDriver.ExceptionThrown += (o, e) => _primaryExceptionHandler.Analyse(this, e.ThrownException);
    }

    public async Task<List<Metric>> CaptureMetricsAsync()
    {
        var metricsResponse = await _devToolsSession?.SendCommand<GetMetricsCommandSettings, GetMetricsCommandResponse>(new GetMetricsCommandSettings());
        return metricsResponse == null ? new List<Metric>() : metricsResponse.Metrics.ToList();
    }

    public void Quit()
    {
        _devToolsSession?.Dispose();
        _networkInterceptor?.StopMonitoring();
        _monitor?.StopEventMonitoring();
        _webDriver?.Quit();
    }

    public void GoToUrl(string url)
    {
        _webDriver.Navigate().GoToUrl(url);
    }

    public IComponent FindComponent(By locator)
    {
        IWebElement nativeWebElement = 
            _webDriverWait.Until(ExpectedConditions.ElementExists(locator));
        IComponent element = new ComponentAdapter(_webDriver, nativeWebElement, locator);

        ScrollIntoView(element);
        return element;
    }

    public List<IComponent> FindComponents(By locator)
    {
        ReadOnlyCollection<IWebElement> nativeWebElements = 
            _webDriverWait.Until(ExpectedConditions.PresenceOfAllElementsLocatedBy(locator));
        var elements = new List<IComponent>();
        foreach (var nativeWebElement in nativeWebElements)
        {
            IComponent element = new ComponentAdapter(_webDriver, nativeWebElement, locator);
            elements.Add(element);
        }

        return elements;
    }

    public void Refresh()
    {
        _webDriver.Navigate().Refresh();
    }

    public bool ComponentExists(IComponent component)
    {
        try
        {
            _webDriver.FindElement(component.By);

            return true;
        }
        catch
        {
            // The component was not found
            return false;
        }
    }

    public void DeleteAllCookies()
    {
        _webDriver.Manage().Cookies.DeleteAllCookies();
    }

    public void ExecuteScript(string script, params object[] args)
    {
        ((IJavaScriptExecutor)_webDriver).ExecuteScript(script, args);
    }

    public void WaitForAjax()
    {
        _webDriverWait.Until(driver =>
        {
            var script = "return window.jQuery != undefined && jQuery.active == 0";
            return (bool)((IJavaScriptExecutor)driver).ExecuteScript(script);
        });
    }

    private void ScrollIntoView(IComponent element)
    {
        ExecuteScript("arguments[0].scrollIntoView(true);", element.WrappedElement);
    }

    private void InitializeExceptionAnalysisHandlers()
    {
        _primaryExceptionHandler = new ConsoleWarningHandler();
        var heapPerformanceMetricHandler = new HeapPerformanceMetricHandler();
        _primaryExceptionHandler.AddExceptionAnalysationHandler(heapPerformanceMetricHandler);

        var customHtmlExceptionHandler = new HtmlSourceExceptionHandler("404 - File or directory not found.", "IT IS NOT A TEST PROBLEM. THE PAGE DOES NOT EXIST.");
        heapPerformanceMetricHandler.AddExceptionAnalysationHandler(customHtmlExceptionHandler);

        var javaScriptErrorsHandler = new JavaScriptErrorsHandler();
        customHtmlExceptionHandler.AddExceptionAnalysationHandler(javaScriptErrorsHandler);

        var noFailedRequestsHandler = new NoFailedRequestsHandler();
        javaScriptErrorsHandler.AddExceptionAnalysationHandler(noFailedRequestsHandler);

        var serviceUnavailableExceptionHandler = new ServiceUnavailableExceptionHandler();
        noFailedRequestsHandler.AddExceptionAnalysationHandler(serviceUnavailableExceptionHandler);
    }
}
