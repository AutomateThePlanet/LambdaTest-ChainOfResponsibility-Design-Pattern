using ChainOfResponsibility.ExceptionAnalysation.Contracts;
using ChainOfResponsibility.FourthVersion;

namespace ChainOfResponsibility.ExceptionAnalysation;

public abstract class JavaScriptConsoleWarningsHandler : ExceptionAnalysationHandler
{
    protected abstract string TextToSearchInSource { get; }

    public override bool IsApplicable(IDriver driver, Exception ex = null, params object[] context)
    {
        if (driver == null)
        {
            throw new ArgumentNullException(nameof(driver));
        }

        var result = driver.ConsoleMessages.Contains(TextToSearchInSource);

        return result;
    }
}
