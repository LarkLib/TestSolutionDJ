using System.Drawing;
using System.Reflection;

namespace TestMinimalAPIWebApplication
{
    internal class TestScottPlot
    {
        internal static void CrtateImageByScottPlot()
        {
            //string[] dataX = new string[] {"12.30","12.31","1.1","1.2","1.3" };
            double[] dataX = new double[] { 1, 2, 3, 4, 5 };
            double[] dataY = new double[] { 1, 4, 9, 16, 25 };
            double[] dataS = new double[] { 6, 9, 5, 12, 30 };
            double[] dataT = new double[] { 2, 6, 12, 13, 12 };

            double[] dataX1 = new double[] { 1, 2, 3 };
            double[] dataX2 = new double[] { 3,4, 5 };
            double[] dataY1 = new double[] { 1, 3, 6 };
            double[] dataY2 = new double[] { 6,18, 20 };

            var plt = new ScottPlot.Plot(400, 300);
            var chartPath = $"{Assembly.GetExecutingAssembly().GetName().Name}ScottPlot.png";
            plt.AddScatter(dataX, dataY, Color.DarkGreen, 5, 20);
            plt.AddScatter(dataX, dataS, Color.DarkBlue, 5, 10);
            plt.AddScatter(dataX, dataT, Color.OrangeRed, 5, 10);

            //同一条线分成几段，分别指定颜色，拼接成一条线，以达到一条线多种颜色的效果
            plt.AddScatter(dataX1, dataY1, Color.Black, 3, 15);
            plt.AddScatter(dataX2, dataY2, Color.Brown, 3, 15);
            plt.SaveFig(chartPath);
            //plt.SaveFig("quickstart.png");
            var base64String = plt.GetImageBase64();


            int count = 10;

            Random rand = new(0);
            double[] xs = ScottPlot.DataGen.Consecutive(count);
            double[] ys = ScottPlot.DataGen.Random(rand, count, 10);
            var cmap = ScottPlot.Drawing.Colormap.Viridis;
            Color[] colors = ys.Select(y => cmap.GetColor(y / 10)).ToArray();

            plt = new ScottPlot.Plot(400, 200);
            for (int i = 0; i < count; i++)
            {
                var bar = plt.AddBar(
                    values: new double[] { ys[i] },
                    positions: new double[] { xs[i] },
                    color: colors[i]
                );
            }

            plt.SaveFig($"{Assembly.GetExecutingAssembly().GetName().Name}ScottPlot.multicolor-bar.png");
        }
    }
}
