using ChainOfResponsibility.ExceptionAnalysation.Contracts;
using ChainOfResponsibility.FourthVersion;

namespace ChainOfResponsibility.ExceptionAnalysation;

public abstract class ExceptionAnalysationHandler : IExceptionAnalysationHandler
{
    private IExceptionAnalysationHandler _nextExceptionAnalysationHandler;

    public abstract string DetailedIssueExplanation { get; }

    public abstract bool IsApplicable(IDriver driver, Exception ex = null, params object[] context);

    public void Analyse(IDriver driver, Exception ex = null, params object[] context)
    {
        if (IsApplicable(driver, ex, context))
        {
            if (driver != null)
            {
                string url = driver.Url.ToString();
                throw new AnalyzedTestException(DetailedIssueExplanation, url, ex);
            }
            else
            {
                throw new AnalyzedTestException(DetailedIssueExplanation);
            }
        }
        else if (_nextExceptionAnalysationHandler != null)
        {
            _nextExceptionAnalysationHandler.Analyse(driver, ex, context);
        }
    }

    public void AddExceptionAnalysationHandler<TExceptionAnalysationHandler>(
        TExceptionAnalysationHandler exceptionAnalysationHandler)
        where TExceptionAnalysationHandler : IExceptionAnalysationHandler
    {
        _nextExceptionAnalysationHandler = exceptionAnalysationHandler;
    }
}
