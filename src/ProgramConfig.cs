using System.Drawing;
using ScottPlot;

namespace Bonwerk.SnooStudy
{
    public static class ProgramConfig
    {
        public const int TitleSize = 24;

        public static Color RSqColor => Color.LightGreen;
        public static Color AccuracyColor => Color.OrangeRed;
        
        public static void StylePlot(Plot plot)
        {
            plot.Style(Style.Blue1);
            plot.Style(label: Color.LightBlue, tick: Color.LightBlue, grid: Color.DimGray);
        }
    }
}