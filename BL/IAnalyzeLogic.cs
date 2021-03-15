using System;
using DTOs;

namespace BL
{
    public interface IAnalyzeLogic
    {
        event EventHandler<AnalyzeFinishedEventArgs> AnalyzeFinishedEvent;
        Measurement Analyze(Measurement DTO);
    }
}