using System.Drawing;
using Bonwerk.Essentials;
using ScottPlot;

namespace Bonwerk.SnooStudy
{
    public static class ProgramConfig
    {
        public const int TitleSize = 24;

        public static Color Color1 => Color.LightGreen;
        public static Color Color2 => Color.OrangeRed;
        public static Color BgColor = ColorHelper.FromHex("07263b");
        
        public static void StylePlot(Plot plot)
        {
            plot.Style(Style.Blue1);
            plot.Style(label: Color.LightBlue, tick: Color.LightBlue, grid: Color.DimGray);
        }
    }
}