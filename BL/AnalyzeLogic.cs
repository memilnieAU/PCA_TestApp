using System;
using DTOs;

namespace BL
{
    public class AnalyzeLogic : IAnalyzeLogic
    {
        private Measurement analysisObject;

        public void Analyze(Measurement DTO)
        {
            analysisObject = DTO;
            //Her skal vi analysere dataen
            //ToDo Vi skal have rette dette så vi ikke er låst til "13%"
            analysisObject.ProbabilityProcent = 13;
            OnAnalyzeFinished(new AnalyzeFinishedEventArgs()
            {
                DTO = analysisObject
            });
        }

        protected virtual void OnAnalyzeFinished(AnalyzeFinishedEventArgs e)
        {
            AnalyzeFinishedEvent?.Invoke(this, e);
        }

        public event EventHandler<AnalyzeFinishedEventArgs> AnalyzeFinishedEvent;

    }
}