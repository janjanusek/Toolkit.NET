namespace Toolkit.NET.Algorithms.PathFinding
{
    public class Path
    {
        public float Distance { get; set; }
        public INode From { get; set; }
        public INode To { get; set; }

        public INode Oposite(INode paNode) => paNode == From ? To : From;
    }
}