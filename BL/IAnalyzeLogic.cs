using System;
using DTOs;

namespace BL
{
    public interface IAnalyzeLogic
    {
        event EventHandler<AnalyzeFinishedEventArgs> AnalyzeFinishedEvent;
        void Analyze(Measurement DTO);
    }
}