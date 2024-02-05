using System;
using ChainOfResponsibility.FourthVersion;

namespace ChainOfResponsibility.ExceptionAnalysation.Contracts;

public interface IExceptionAnalyser
{
    void Analyse(IDriver driver, Exception ex = null, params object[] context);

    void AddExceptionAnalysationHandler<TExceptionAnalysationHandler>(
        TExceptionAnalysationHandler exceptionAnalysationHandler)
        where TExceptionAnalysationHandler : IExceptionAnalysationHandler;

    void RemoveFirstExceptionAnalysationHandler();
}
