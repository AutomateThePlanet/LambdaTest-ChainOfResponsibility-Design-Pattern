using System.Collections.Concurrent;
using System.Threading.Tasks;
using OpenQA.Selenium.DevTools.V120.Performance;

namespace ChainOfResponsibility.FourthVersion;

public interface IDriver
{
    public string Url { get; }
    public string HtmlSource { get; }
    public Task Start(Browser browser);
    public void Refresh();
    public void Quit();
    public void GoToUrl(string url);
    public IComponent FindComponent(By locator);
    public List<IComponent> FindComponents(By locator);

    public bool ComponentExists(IComponent component);
    public void ExecuteScript(string script, params object[] args);
    public void DeleteAllCookies();
    public void WaitForAjax();
    public Task<List<Metric>> CaptureMetricsAsync();
    public List<string> ConsoleMessages { init; get; }
    public List<string> JavaScriptErrors { init; get; }
    public ConcurrentBag<HttpRequestData> RequestsHistory { init; get; }
    public ConcurrentBag<HttpResponseData> ResponsesHistory { init; get; }
}
