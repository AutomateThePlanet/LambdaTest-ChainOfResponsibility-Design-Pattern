namespace ChainOfResponsibility.ExceptionAnalysation;

public class HeapPerformanceMetricHandler : PerformanceMetricHandler
{
    public HeapPerformanceMetricHandler()
        : base("JSHeapTotalSize", 1000)
    {
    }
}
