using System;
using ChainOfResponsibility.ExceptionAnalysation.Contracts;
using ChainOfResponsibility.FourthVersion;

namespace ChainOfResponsibility.ExceptionAnalysation;

public abstract class HtmlSourceExceptionHandler : IExceptionAnalysationHandler
{
    public abstract string DetailedIssueExplanation { get; }

    protected abstract string TextToSearchInSource { get; }

    public bool IsApplicable(IDriver driver, Exception ex = null, params object[] context)
    {
        if (driver == null)
        {
            throw new ArgumentNullException(nameof(driver));
        }

        var result = driver.HtmlSource.Contains(TextToSearchInSource);
        return result;
    }
}
