// using System;
// using System.Net.Http;
// using System.Net.Http.Headers;
// using System.Threading.Tasks;

// class Program
// {
//     static async Task Main(string[] args)
//     {
//         var url = "http://quiet-toad-openly.ngrok-free.app/gitstack";
//         //var url = "https://quiet-toad-openly.ngrok-free.app/registration/login/?next=/gitstack/";
//        using (HttpClient client = new HttpClient(new HttpClientHandler { AllowAutoRedirect = true }))
//         {
//             // Set the ngrok-skip-browser-warning header
//             client.DefaultRequestHeaders.Add("ngrok-skip-browser-warning", "any-value");

//             // Set a custom User-Agent header
//             client.DefaultRequestHeaders.UserAgent.ParseAdd("CustomUserAgent");

//             try
//             {
//                 HttpResponseMessage response = await client.GetAsync(url);
//                 response.EnsureSuccessStatusCode(); // Throw if not a success code.

//                 string responseBody = await response.Content.ReadAsStringAsync();
//                 Console.WriteLine(responseBody);
//             }
//             catch (HttpRequestException e)
//             {
//                 Console.WriteLine($"Request error: {e.Message}");
//             }
//         }
//     }
// }


using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

class Program
{
    static async Task Main(string[] args)
    {
        var url = "http://quiet-toad-openly.ngrok-free.app/gitstack";

        using (HttpClient client = new HttpClient(new HttpClientHandler { AllowAutoRedirect = false }))
        {
            // Set the ngrok-skip-browser-warning header
            client.DefaultRequestHeaders.Add("ngrok-skip-browser-warning", "any-value");

            // Set a custom User-Agent header
            client.DefaultRequestHeaders.UserAgent.ParseAdd("CustomUserAgent");

            try
            {
                HttpResponseMessage response = await client.GetAsync(url);

                while ((int)response.StatusCode >= 300 && (int)response.StatusCode < 400)
                {
                    var newUrl = response.Headers.Location.ToString();
                    Console.WriteLine($"Redirected to: {newUrl}");

                    response = await client.GetAsync(newUrl);
                }

                response.EnsureSuccessStatusCode(); // Throw if not a success code.

                string responseBody = await response.Content.ReadAsStringAsync();
                Console.WriteLine(responseBody);
            }
            catch (HttpRequestException e)
            {
                Console.WriteLine($"Request error: {e.Message}");
            }
        }
    }
}
