using ChainOfResponsibility.ExceptionAnalysation.Contracts;
using ChainOfResponsibility.FourthVersion;

namespace ChainOfResponsibility.ExceptionAnalysation;

public class PerformanceMetricHandler : IExceptionAnalysationHandler
{
    private string _metricName;
    private double _threshold;

    public PerformanceMetricHandler(string metricName, double threshold)
    {
        this._metricName = metricName;
        this._threshold = threshold;
    }

    public string DetailedIssueExplanation
    {
        get
        {
            return "There were some failed requests.";
        }
    }


    //Timestamp = 18072.3841
    //AudioHandlers = 0
    //Documents = 7
    //Frames = 2
    //JSEventListeners = 197
    //LayoutObjects = 265
    //MediaKeySessions = 0
    //MediaKeys = 0
    //Nodes = 2829
    //Resources = 114
    //ContextLifecycleStateObservers = 21
    //V8PerContextDatas = 4
    //WorkerGlobalScopes = 0
    //UACSSResources = 0
    //RTCPeerConnections = 0
    //ResourceFetchers = 7
    //AdSubframes = 0
    //DetachedScriptStates = 2
    //ArrayBufferContents = 6
    //LayoutCount = 70
    //RecalcStyleCount = 39
    //LayoutDuration = 0.061348
    //RecalcStyleDuration = 0.056053
    //DevToolsCommandDuration = 0.126399
    //ScriptDuration = 0.350914
    //V8CompileDuration = 0.003751
    //TaskDuration = 0.864955
    //TaskOtherDuration = 0.26649
    //ThreadTime = 0.764974
    //ProcessTime = 1.109375
    //JSHeapUsedSize = 9161296
    //JSHeapTotalSize = 14876672
    //FirstMeaningfulPaint = 0
    //DomContentLoaded = 18072.116968
    //NavigationStart = 18069.00376
    public bool IsApplicable(IDriver driver, Exception ex = null, params object[] context)
    {
        if (driver == null)
        {
            throw new ArgumentNullException(nameof(driver));
        }

        var metrics = driver.CaptureMetricsAsync().Result;

        // TODO: we can create several threads.
        var currentMetric = metrics.First(m => m.Name.Equals(_metricName)).Value;
        if (currentMetric > _threshold)
        {
            driver.JavaScriptErrors.ForEach(e => Console.WriteLine(e));
        }

        return driver.JavaScriptErrors.Any();
    }
}
