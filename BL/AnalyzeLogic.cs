using System;
using DTOs;

namespace BL
{
    public class AnalyzeLogic : IAnalyzeLogic
    {

        #region Event
        public event EventHandler<AnalyzeFinishedEventArgs> AnalyzeFinishedEvent;
        protected virtual void OnAnalyzeFinished(AnalyzeFinishedEventArgs e)
        {
            AnalyzeFinishedEvent?.Invoke(this, e);
        }
        #endregion

        private Measurement analysisObject;

        public AnalyzeLogic(EventHandler<AnalyzeFinishedEventArgs> handleAnalyzeFinishedEvent)
        {
            AnalyzeFinishedEvent += handleAnalyzeFinishedEvent;
        }

        public Measurement Analyze(Measurement DTO)
        {
            analysisObject = DTO;
            analysisObject.ProbabilityProcent = Analyze1();
            OnAnalyzeFinished(new AnalyzeFinishedEventArgs()
            {
                DTO = analysisObject
            });
            return analysisObject;
        }

        //ToDo Vi skal have rette dette så vi ikke er låst til "13%"
        //Her skal vi analysere dataen
        private int Analyze1()
        {
            return 13;
        }



    }
}