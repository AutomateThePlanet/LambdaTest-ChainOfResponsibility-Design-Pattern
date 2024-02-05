using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ChainOfResponsibility.ExceptionAnalysation;

namespace ChainOfResponsibility.ExceptionAnalysation;
public class ServiceUnavailableExceptionHandler : HtmlSourceExceptionHandler
{
    public override string DetailedIssueExplanation
    {
        get
        {
            return "IT IS NOT A TEST PROBLEM. THE SERVICE IS UNAVAILABLE.";
        }
    }
    protected override string TextToSearchInSource
    {
        get
        {
            return "HTTP Error 503. The service is unavailable.";
        }
    }
}
