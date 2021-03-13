using System;
using DTOs;

namespace BL
{
    public class AnalyzeFinishedEventArgs: EventArgs
    {
        public Measurement DTO { get; set; }
    }
}