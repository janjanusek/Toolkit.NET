namespace Toolkit.NET.Simulations.MonteCarloCore
{
    public class ReplicationResult
    {
        public double ExperimentValue { get; set; }

        public ReplicationResult()
        {
            ExperimentValue = 0;
        }

        //public void AddLocalReplicationResult(double paLocalResult) => ExperimentValue += paLocalResult;

        //public double ResultMean(int paReplicationNumber)
        //{
        //    return (double)this.ExperimentValue / paReplicationNumber;
        //}
    }
}
