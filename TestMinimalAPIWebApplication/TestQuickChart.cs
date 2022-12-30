using System.Reflection;

namespace TestMinimalAPIWebApplication
{
    class TestQuickChart
    {
        internal static void CreateImageByQuickChart()
        {
            QuickChart.Chart qc = new QuickChart.Chart();

            qc.Width = 500;
            qc.Height = 300;
            qc.Version = "2.9.4";
            qc.Config = @"{
                type: 'bar',
                data: {
                    labels: ['Q1', 'Q2', 'Q3', 'Q4'],
                    datasets: [{
                        label: 'Users',
                        data: [50, 60, 70, 180]
                    }]
                }
            }";
            var rul = qc.GetUrl();
            qc.ToFile($"{Assembly.GetExecutingAssembly().GetName().Name}QuickChart.png");
        }
    }
}
