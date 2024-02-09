using System;
using ChainOfResponsibility.ExceptionAnalysation.Contracts;
using ChainOfResponsibility.FourthVersion;

namespace ChainOfResponsibility.ExceptionAnalysation;

public abstract class UrlExceptionHandler : ExceptionAnalysationHandler
{
    protected abstract string TextToSearchInUrl { get; }

    public override bool IsApplicable(IDriver driver, Exception ex = null, params object[] context)
    {
        if (driver == null)
        {
            throw new ArgumentNullException(nameof(driver));
        }

        var result = driver.Url.Contains(TextToSearchInUrl);
        return result;
    }
}
