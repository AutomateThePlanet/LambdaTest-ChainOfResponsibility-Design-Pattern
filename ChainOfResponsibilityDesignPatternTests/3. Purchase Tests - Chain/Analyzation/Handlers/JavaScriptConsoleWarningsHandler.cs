using ChainOfResponsibility.ExceptionAnalysation.Contracts;
using ChainOfResponsibility.FourthVersion;

namespace ChainOfResponsibility.ExceptionAnalysation;

public abstract class JavaScriptConsoleWarningsHandler : IExceptionAnalysationHandler
{
    public abstract string DetailedIssueExplanation { get; }

    protected abstract string TextToSearchInSource { get; }

    public bool IsApplicable(IDriver driver, Exception ex = null, params object[] context)
    {
        if (driver == null)
        {
            throw new ArgumentNullException(nameof(driver));
        }

        var result = driver.ConsoleMessages.Contains(TextToSearchInSource);

        return result;
    }
}
