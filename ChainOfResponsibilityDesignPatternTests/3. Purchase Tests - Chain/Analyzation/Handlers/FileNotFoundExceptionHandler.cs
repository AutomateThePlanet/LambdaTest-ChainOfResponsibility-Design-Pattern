namespace ChainOfResponsibility.ExceptionAnalysation;
public class FileNotFoundExceptionHandler : HtmlSourceExceptionHandler
{
    public override string DetailedIssueExplanation
    {
        get
        {
            return "IT IS NOT A TEST PROBLEM. THE PAGE DOES NOT EXIST.";
        }
    }

    protected override string TextToSearchInSource
    {
        get
        {
            return "404 - File or directory not found.";
        }
    }
}
