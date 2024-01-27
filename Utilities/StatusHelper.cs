using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TrucksLOG.Utilities
{
    public class StatusHelper
    {
        public enum Tour_Status
        {
            Unknown,
            OnTour,
            Canceled,
            Rejected,
            Paused,
            Completed
        }
    }
}
