using System;
using DL.DTO;

namespace DL.EventArgs
{
    public class RecordFinishedEventArgs: System.EventArgs
    {
        public Measurement measureDTO { get; set; }
    }
}