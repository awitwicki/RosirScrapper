// See https://aka.ms/new-console-template for more information
using RosirScrapper;
using System.Text;

Console.WriteLine("Hello, World!");

while (true)
{
    try
    {
        var peopleCount = await HtmlScrapper.GetTopOfTheDayPhotoUrl("https://rosir.nq.pl/");
       
        if (peopleCount != null)
        {
            Console.WriteLine(peopleCount);

            // Send to influx
            string query = $"iot,room=rosir,device=api,sensor=count_people count_people={peopleCount}";

            new Task(() =>
            {
                try
                {
                    using (var client = new System.Net.Http.HttpClient())
                    {
                        client.BaseAddress = new Uri("http://localhost:8086/write?db=bots");
                        client.Timeout = TimeSpan.FromSeconds(1);
                        var content = new System.Net.Http.StringContent(query, Encoding.UTF8, "application/Text");
                        var res = client.PostAsync("", content).Result;
                        var tt = res.Content.ReadAsStringAsync().Result;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"query: {query}");
                    Console.WriteLine(ex);
                }
            }).Start();
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine(ex.ToString());
    }

    await Task.Delay(30000);
}