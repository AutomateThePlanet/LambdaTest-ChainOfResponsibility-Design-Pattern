using ChainOfResponsibility.FourthVersion;

namespace ChainOfResponsibility.ExceptionAnalysation;

public class HtmlSourceExceptionHandler : ExceptionAnalysationHandler
{
    private readonly string _textToSearchInSource;
    public HtmlSourceExceptionHandler(string textToSearchInSource, string detailedIssueExplanation)
    {
        _textToSearchInSource = textToSearchInSource;
        DetailedIssueExplanation = detailedIssueExplanation;
    }

    public override string DetailedIssueExplanation { get; }

    public override bool IsApplicable(IDriver driver, Exception ex = null, params object[] context)
    {
        if (driver == null)
        {
            throw new ArgumentNullException(nameof(driver));
        }

        var result = driver.HtmlSource.Contains(_textToSearchInSource);
        return result;
    }
}
