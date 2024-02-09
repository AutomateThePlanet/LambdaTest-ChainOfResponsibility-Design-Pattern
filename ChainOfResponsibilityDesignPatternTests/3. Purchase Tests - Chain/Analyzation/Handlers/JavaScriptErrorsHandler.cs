using ChainOfResponsibility.ExceptionAnalysation.Contracts;
using ChainOfResponsibility.FourthVersion;

namespace ChainOfResponsibility.ExceptionAnalysation;

public class JavaScriptErrorsHandler : ExceptionAnalysationHandler
{
    public override string DetailedIssueExplanation
    {
        get
        {
            return "There were multiple JavaScript errors, check console logs for more information.";
        }
    }

    public override bool IsApplicable(IDriver driver, Exception ex = null, params object[] context)
    {
        if (driver == null)
        {
            throw new ArgumentNullException(nameof(driver));
        }

        if (driver.JavaScriptErrors.Any())
        {
            driver.JavaScriptErrors.ForEach(e => Console.WriteLine(e));
        }

        return driver.JavaScriptErrors.Any();
    }
}
