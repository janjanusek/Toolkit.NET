namespace Toolkit.NET.Algorithms.Metaheuristics.SimulatedAnnealing.MedianProblem
{
    public class MedianSolution : ISolution
    {
        public double Cost { get; set; }
        public int[] PickedCandidates { get; set; }
        public double[] PickedCandidatesCosts { get; set; }

        public MedianSolution()
        {

        }
    }
}
