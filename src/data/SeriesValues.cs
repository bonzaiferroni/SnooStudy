namespace Bonwerk.SnooStudy
{
    public class SeriesValues
    {
        public SeriesValues(double[] xs, double[] ys)
        {
            Xs = xs;
            Ys = ys;
        }

        public double[] Xs { get; }
        public double[] Ys { get; }
    }
}