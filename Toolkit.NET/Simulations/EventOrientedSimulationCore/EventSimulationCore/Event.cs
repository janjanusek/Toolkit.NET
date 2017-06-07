namespace Toolkit.NET.Simulations.EventOrientedSimulationCore.EventSimulationCore
{
    public abstract class Event
    {
        protected readonly EventSimulation Simulation;

        protected Event(EventSimulation paSimulation)
        {
            Simulation = paSimulation;
        }

        public double EventTime { get; private set; }
        public double RelativeEventTime { get; private set; }

        public abstract void Execute();

        public void PlanEventRelativeTime(double paRelativeTime)
        {
            RelativeEventTime = paRelativeTime;
            Simulation.PlanEvent(this, EventTime = (Simulation.CurrentSimulationTime + paRelativeTime));
        }

        public override string ToString()
        {
            return $"{RelativeEventTime} {this.GetType().Name} - ";
        }
    }
}
