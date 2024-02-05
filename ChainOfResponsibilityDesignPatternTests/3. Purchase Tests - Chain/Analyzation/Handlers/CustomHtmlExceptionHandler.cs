namespace ChainOfResponsibility.ExceptionAnalysation;

public class CustomHtmlExceptionHandler : HtmlSourceExceptionHandler
{
    public CustomHtmlExceptionHandler(string textToSearchInSource, string detailedIssueExplanation)
    {
        TextToSearchInSource = textToSearchInSource;
        DetailedIssueExplanation = detailedIssueExplanation;
    }

    public CustomHtmlExceptionHandler()
    {
    }

    public override string DetailedIssueExplanation { get; }

    protected override string TextToSearchInSource { get; }
}
