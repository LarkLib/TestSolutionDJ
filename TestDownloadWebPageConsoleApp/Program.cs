using AngleSharp;
using AngleSharp.Dom;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace TestDownloadWebPageConsoleApp
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var stopwatch = new Stopwatch();
            var p = new Program();
            var d1 = await p.GetWebPages("http://www.gulongshuwu.com/");
            var a1 = d1.QuerySelectorAll("a[target='_blank']").Where(link => link.GetAttribute("title")?.Contains("《") == true && link.ParentElement.QuerySelector("span")?.TextContent.Contains("年") == true).ToList();
            var semaphore = new SemaphoreSlim(10); // Limit to 5 concurrent tasks
            var tasks = a1.Select(async aaa =>
            {
                await semaphore.WaitAsync();
                try
                {
                    var aa1 = aaa as AngleSharp.Html.Dom.IHtmlAnchorElement;
                    if (File.Exists($"古龙全集\\{aa1.TextContent}.txt"))
                    {
                        Console.WriteLine($"{aa1.TextContent}已经存在！");
                        return;
                    }
                    var contentBuilder = new StringBuilder();

                    Console.WriteLine($"{aa1.TextContent}开始下载：");
                    stopwatch.Start();

                    var d2 = await p.GetWebPages(aa1.Href);
                    var a2 = d2.QuerySelectorAll("dl.cat_box a");

                    foreach (AngleSharp.Html.Dom.IHtmlAnchorElement aa2 in a2)
                    {
                        if (string.IsNullOrEmpty(aa2.Href))
                        {
                            contentBuilder.AppendLine(aa2.TextContent);
                            continue;
                        }
                        contentBuilder.AppendLine(aa2.TextContent);
                        var d3 = await p.GetWebPages(aa2.Href);
                        var p2 = d3.QuerySelectorAll("div.entry p").ToList();

                        foreach (var pp2 in p2)
                        {
                            if (!pp2.TextContent.Contains("一秒钟记住"))
                            {
                                contentBuilder.AppendLine(pp2.TextContent);
                            }
                        }
                    }

                    if (!Directory.Exists("古龙全集")) Directory.CreateDirectory("古龙全集");
                    File.WriteAllText($"古龙全集\\{aa1.TextContent}.txt", contentBuilder.ToString());
                    contentBuilder = null;

                    Console.WriteLine($"{aa1.TextContent}下载完成：用时{(int)stopwatch.ElapsedMilliseconds}ms");
                    stopwatch.Stop();
                }
                finally
                {
                    semaphore.Release();
                }
            });

            await Task.WhenAll(tasks);
        }
        internal async Task<IDocument> GetWebPages(string address)
        {
            try
            {
                var config = Configuration.Default.WithDefaultLoader();
                //var address = "http://www.gulongshuwu.com/";
                var context = BrowsingContext.New(config);
                var document = await context.OpenAsync(address);
                return document;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;

        }
    }
}

//var stopwatch = new Stopwatch();
//var clientIndex = new HttpClient();
//var indexPage = "http://www.gulongshuwu.com/";
//var indexContent = await clientIndex.GetStringAsync(indexPage);
//var htmlDocument = new HtmlDocument();
//htmlDocument.LoadHtml(indexContent);
//var dl = htmlDocument.DocumentNode.SelectNodes("//dl");
//var ddElements = dl.Nodes().Where((node) => node.Name == "dd").ToArray();
//var lastChapter = 214;
//foreach (var node in ddElements)
//{
//    var a = node.Element("a");
//    if (a == null || !a.InnerText.StartsWith("第")) continue;
//    var chapter = int.Parse(a.InnerText.Substring(1, a.InnerText.IndexOf("章") - 1));
//    if (chapter <= lastChapter) continue;
//    File.AppendAllText("d:\\麻衣神算子.txt", $"{a.InnerText}{Environment.NewLine}");
//    Console.WriteLine(a.InnerText);
//    var clientContent = new WebDownload();
//    var content = clientContent.DownloadString($"http://www.zhuaji.org/read/2471/{a.Attributes["href"].Value}");
//    var contentDocument = new HtmlDocument();
//    contentDocument.LoadHtml(content);
//    var divContent = contentDocument.GetElementbyId("content");
//    var divA = divContent.SelectNodes("a")?.ToArray();
//    if (divA != null && divA.Any()) foreach (var aItem in divA) aItem.Remove();
//    var temContent = divContent.InnerHtml.Replace("&nbsp;", " ").Replace("<br><br>", "<br>").Replace("<br>", Environment.NewLine);
//    var textContent = Regex.Replace(temContent, "&#\\w+;", "");
//    File.AppendAllText("d:\\麻衣神算子.txt", $"{textContent}{Environment.NewLine}");
//}
//return;

//Parallel.ForEach(dbList, new ParallelOptions() { MaxDegreeOfParallelism = MaxDbThreadCount }, db =>
//{
//    var codeList = GetCodeListByDatabase(db);
//    Parallel.ForEach(codeList, new ParallelOptions() { MaxDegreeOfParallelism = MaxCodeThreadCount }, code =>
//    {
//        {
//            var stopwatch = new Stopwatch();
//            stopwatch.Start();
//            string stockContent = null;
//            for (int i = 0; i < 5; i++)
//            {
//                try
//                {
//                    var client = new WebDownload(5 * 60 * 1000) { Encoding = Encoding.GetEncoding(936) };
//                    var stockAddress = string.Format("http://quotes.money.163.com/service/chddata.html?code={0}{1}&start=19021231&end=20180526&fields=TCLOSE;HIGH;LOW;TOPEN;LCLOSE;CHG;PCHG;TURNOVER;VOTURNOVER;VATURNOVER;TCAP;MCAP", code.StartsWith("sh") ? 0 : 1, code.Substring(2, 6));
//                    stockContent = client.DownloadString(stockAddress);
//                    break;
//                }
//                catch (WebException)
//                {
//                    Thread.Sleep(15000);
//                }
//            }
//            var rows = 0;
//            if (!string.IsNullOrEmpty(stockContent))
//            {
//                rows = SaveStockDaily163ContentToDB(db, code, stockContent);
//            }
//            Console.WriteLine($"{db},GetDaily163,{code},{rows},{(int)stopwatch.ElapsedMilliseconds}ms");
//        }
//    }
//    );
//}
//);