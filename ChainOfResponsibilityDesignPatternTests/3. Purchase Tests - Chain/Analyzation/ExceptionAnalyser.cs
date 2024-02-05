using ChainOfResponsibility.ExceptionAnalysation.Contracts;
using ChainOfResponsibility.FourthVersion;

namespace ChainOfResponsibility.ExceptionAnalysation;

public class ExceptionAnalyser : IExceptionAnalyser
{
    private readonly List<IExceptionAnalysationHandler> _exceptionAnalysationHandlers;

    public ExceptionAnalyser()
    {
        _exceptionAnalysationHandlers = new List<IExceptionAnalysationHandler>();
    }

    public void RemoveFirstExceptionAnalysationHandler()
    {
        if (_exceptionAnalysationHandlers.Count > 0)
        {
            _exceptionAnalysationHandlers.RemoveAt(0);
        }
    }

    public void Analyse(IDriver driver, Exception ex = null, params object[] context)
    {
        foreach (IExceptionAnalysationHandler exceptionHandler in _exceptionAnalysationHandlers)
        {
            if (exceptionHandler.IsApplicable(driver, ex, context))
            {
                if (driver != null)
                {
                    string url = driver.Url.ToString();
                    throw new AnalyzedTestException(exceptionHandler.DetailedIssueExplanation, url, ex);
                }
                else
                {
                    throw new AnalyzedTestException(exceptionHandler.DetailedIssueExplanation);
                }
            }
        }
    }

    public void AddExceptionAnalysationHandler<TExceptionAnalysationHandler>(
        TExceptionAnalysationHandler exceptionAnalysationHandler)
        where TExceptionAnalysationHandler : IExceptionAnalysationHandler
    {
        _exceptionAnalysationHandlers.Insert(0, exceptionAnalysationHandler);
    }
}
