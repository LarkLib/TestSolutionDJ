using AngleSharp;
using AngleSharp.Dom;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;

namespace TestDownloadWebPageConsoleApp
{
    public record Book(string category, string name, string url);
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var p = new Program();
            //await p.DownloadGulong();//古龙全集
            await p.DownloadGuShiWenWang("https://www.gushiwen.cn/guwen/book.aspx?id=303");//牡丹亭 原文
            //await p.DownloadGuShiWenWang("https://www.gushiwen.cn/guwen/book_406ec46ca000.aspx");//酉阳杂俎 原文
            //await p.DownloadGuShiWenWang("https://www.gushiwen.cn/guwen/book_46653FD803893E4F2E04DEE24C8F49C4.aspx");//博物志
            //await p.DownloadGuShiWenWang("https://www.gushiwen.cn/guwen/book_1bd76a1c3d01.aspx");//论语
            //await p.DownloadZhongHuanDianCang("https://www.zhonghuadiancang.com/wenxueyishu/16737/");//酉阳杂俎译注
            //await p.DownloadZhongHuanDianCang("https://www.zhonghuadiancang.com/wenxueyishu/zibuyu/");//子不语
            //await p.DownloadGuShiWenWang("https://www.gushiwen.cn/guwen/book_46653FD803893E4F44793CB19FDDA58B.aspx");//新齐谐(子不语)原文
            //await p.DownloadZhongHuanDianCang("https://www.zhonghuadiancang.com/leishuwenji/taipingyulan/");//太平御览
        }

        private async Task DownloadGulong()
        {
            var books = new List<Book>();
            var stopwatch = new Stopwatch();
            var d1 = await GetWebPages("http://www.gulongshuwu.com/");
            var c1 = d1.QuerySelectorAll("dl.cat_box");
            foreach (var cc in c1)
            {
                var category = cc.QuerySelector("dt").TextContent;
                var ca1 = cc.QuerySelectorAll("dd a");
                foreach (AngleSharp.Html.Dom.IHtmlAnchorElement caa1 in ca1)
                {
                    var url = caa1.Href;
                    if (caa1.Href.Contains("#"))
                    {
                        url = url.Split('#')[0];
                        if (books.Any(book => book.url.Equals(url))) continue;
                    }
                    books.Add(new Book(category.Replace("《", "").Replace("》", ""), caa1.TextContent.Replace("《", "").Replace("》", ""), url));
                }
            }

            //var a1 = d1.QuerySelectorAll("a[target='_blank']").Where(link => link.GetAttribute("title")?.Contains("《") == true && link.ParentElement.QuerySelector("span")?.TextContent.Contains("年") == true).ToList();
            var a1 = books;
            var semaphore = new SemaphoreSlim(10); // Limit to 5 concurrent tasks
            var tasks = a1.Select(async aa1 =>
            {
                await semaphore.WaitAsync();
                try
                {
                    var fileName = $"{aa1.category}-{aa1.name}.txt";
                    //var aa1 = aaa as AngleSharp.Html.Dom.IHtmlAnchorElement;
                    if (File.Exists(fileName))
                    {
                        Console.WriteLine($"{fileName}已经存在！");
                        return;
                    }
                    var contentBuilder = new StringBuilder();

                    Console.WriteLine($"{fileName}开始下载：");
                    stopwatch.Start();

                    var d2 = await GetWebPages(aa1.url);
                    var a2 = d2.QuerySelectorAll("dl.cat_box a");

                    foreach (AngleSharp.Html.Dom.IHtmlAnchorElement aa2 in a2)
                    {
                        if (string.IsNullOrEmpty(aa2.Href))
                        {
                            contentBuilder.AppendLine(aa2.TextContent);
                            continue;
                        }
                        contentBuilder.AppendLine(aa2.TextContent);
                        var d3 = await GetWebPages(aa2.Href);
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
                    File.WriteAllText($"古龙全集\\{fileName}", contentBuilder.ToString());
                    contentBuilder = null;

                    Console.WriteLine($"{fileName}下载完成：用时{(int)stopwatch.ElapsedMilliseconds}ms");
                    stopwatch.Stop();
                }
                finally
                {
                    semaphore.Release();
                }
            });

            await Task.WhenAll(tasks);
        }
        //古诗文网 https://www.gushiwen.cn/
        private async Task DownloadGuShiWenWang(string downloadUrl)
        {
            var books = new List<Book>();
            var stopwatch = new Stopwatch();
            var contentBuilder = new StringBuilder();
            var d1 = await GetWebPages(downloadUrl);
            var category = d1.QuerySelector("div.cont h1 span")?.TextContent;
            contentBuilder.AppendLine($"{category}({downloadUrl})");
            contentBuilder.AppendLine(d1.QuerySelector("div.cont p")?.TextContent);
            var c1 = d1.QuerySelectorAll("div.bookcont ul span a");
            foreach (AngleSharp.Html.Dom.IHtmlAnchorElement cc in c1)
            {
                books.Add(new Book(category, cc.TextContent, cc.Href));
            }

            var a1 = books;
            var fileName = $"{category}.txt";
            if (File.Exists(fileName)) File.Delete(fileName);
            Console.WriteLine($"{fileName}开始下载：");
            stopwatch.Start();

            foreach (var aa1 in a1)
            {
                Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")} {aa1.name} 开始下载。");
                contentBuilder.AppendLine();
                contentBuilder.AppendLine(aa1.name);

                var d2 = await GetWebPages(aa1.url);
                var p2 = d2.QuerySelectorAll("div.contson p");
                foreach (var pp2 in p2)
                {
                    contentBuilder.AppendLine(pp2.TextContent);
                }
                Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")} {aa1.name} 开始下载。");
            }

            File.AppendAllText($"{fileName}", contentBuilder.ToString());
            contentBuilder = null;

            stopwatch.Stop();
            Console.WriteLine($"{fileName}下载完成：用时{(int)stopwatch.ElapsedMilliseconds}ms");
        }
        //中华典藏 https://www.zhonghuadiancang.com
        private async Task DownloadZhongHuanDianCang(string downloadUrl)
        {
            var books = new List<Book>();
            var stopwatch = new Stopwatch();
            var contentBuilder = new StringBuilder();
            var d1 = await GetWebPages(downloadUrl);
            var category = d1.QuerySelector("div.panel-heading h1")?.TextContent;
            contentBuilder.AppendLine($"{category}({downloadUrl})");
            var summary = d1.QuerySelector("p.m-summary")?.TextContent;
            contentBuilder.AppendLine(summary);
            var c1 = d1.QuerySelectorAll("ul#booklist li a");
            foreach (AngleSharp.Html.Dom.IHtmlAnchorElement cc in c1)
            {
                books.Add(new Book(category, cc.TextContent, cc.Href));
            }

            var a1 = books;
            var fileName = $"{category.Split(' ')[0]}.txt";
            if (File.Exists(fileName)) File.Delete(fileName);
            Console.WriteLine($"{fileName}开始下载：");
            stopwatch.Start();

            foreach (var aa1 in a1)
            {
                //var aa1 = aaa as AngleSharp.Html.Dom.IHtmlAnchorElement;
                Console.WriteLine($"{DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff")} {aa1.name} 开始下载。");
                var d2 = await GetWebPages(aa1.url);
                var title = d2.QuerySelector("div.panel-footer")?.TextContent?.Trim();
                contentBuilder.AppendLine();
                contentBuilder.AppendLine(title);
                var p2 = d2.QuerySelectorAll("div#content p");
                foreach (var pp2 in p2)
                {
                    contentBuilder.AppendLine(pp2.TextContent?.Trim());
                }
            }

            File.AppendAllText($"{fileName}", contentBuilder.ToString());
            contentBuilder = null;

            stopwatch.Stop();
            Console.WriteLine($"{fileName}下载完成：用时{(int)stopwatch.ElapsedMilliseconds}ms");
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