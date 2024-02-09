using ChainOfResponsibility.ExceptionAnalysation.Contracts;
using ChainOfResponsibility.FourthVersion;

namespace ChainOfResponsibility.ExceptionAnalysation;

public class NoFailedRequestsHandler : ExceptionAnalysationHandler
{
    public override string DetailedIssueExplanation
    {
        get
        {
            return "There were multiple failed requests.";
        }
    }

    public override bool IsApplicable(IDriver driver, Exception ex = null, params object[] context)
    {
        if (driver == null)
        {
            throw new ArgumentNullException(nameof(driver));
        }

        bool areThereErrorCodes = driver.ResponsesHistory.Any(r => r.StatusCode > 400 && r.StatusCode < 599);

        return areThereErrorCodes;
    }
}
