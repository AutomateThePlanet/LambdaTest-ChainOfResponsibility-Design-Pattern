using System;
using ChainOfResponsibility.FourthVersion;

namespace ChainOfResponsibility.ExceptionAnalysation.Contracts;

public interface IExceptionAnalysationHandler
{
    string DetailedIssueExplanation { get; }
    // HandleRequest
    void Analyse(IDriver driver, Exception ex = null, params object[] context);
    // SetSuccessor
    void AddExceptionAnalysationHandler<TExceptionAnalysationHandler>(
        TExceptionAnalysationHandler exceptionAnalysationHandler)
        where TExceptionAnalysationHandler : IExceptionAnalysationHandler;

    bool IsApplicable(IDriver driver, Exception ex = null, params object[] context);
}
