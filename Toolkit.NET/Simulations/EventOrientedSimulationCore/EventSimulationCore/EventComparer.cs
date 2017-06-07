using System.Collections.Generic;

namespace Toolkit.NET.Simulations.EventOrientedSimulationCore.EventSimulationCore
{
    public class EventComparer : IComparer<Event>
    {
        public int Compare(Event x, Event y)
        {
            return x.EventTime.CompareTo(y.EventTime);
        }
    }
}
