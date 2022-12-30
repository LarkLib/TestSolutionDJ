using ImageChartsLib;
using System.Reflection;

namespace TestMinimalAPIWebApplication
{

    class TestChartImage
    {
        internal static void CreateChartImage()
        {
            var chartPath = $"{Assembly.GetExecutingAssembly().GetName().Name}ChartImage.png";
            //string chartPath = "/tmp/chart.png";

            new ImageCharts()
                    .cht("bvg") // vertical bar chart
                    .chs("300x300") // 300px x 300px
                    .chd("a:60,40") // 2 data points: 60 and 40
                    .toFile(chartPath);

            Console.WriteLine("Image chart written at " + chartPath);
        }
    }
}
