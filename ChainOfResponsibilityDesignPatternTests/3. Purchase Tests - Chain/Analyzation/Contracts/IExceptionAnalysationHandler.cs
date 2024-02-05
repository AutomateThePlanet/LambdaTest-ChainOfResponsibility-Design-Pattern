using System;
using ChainOfResponsibility.FourthVersion;

namespace ChainOfResponsibility.ExceptionAnalysation.Contracts;

public interface IExceptionAnalysationHandler
{
    string DetailedIssueExplanation { get; }

    bool IsApplicable(IDriver driver, Exception ex = null, params object[] context);
}
