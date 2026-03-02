using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Text.Json;

class Program
{
    static async Task Main(string[] args)
    {
        string url = "https://api.adviceslip.com/advice";

        using (HttpClient client = new HttpClient())
        {
            try
            {
                HttpResponseMessage response = await client.GetAsync(url);
                response.EnsureSuccessStatusCode();

                string json = await response.Content.ReadAsStringAsync();

                using (JsonDocument doc = JsonDocument.Parse(json))
                {
                    string conselho = doc.RootElement
                                          .GetProperty("slip")
                                          .GetProperty("advice")
                                          .GetString();

                    Console.WriteLine("Conselho de Hoje:");
                    Console.WriteLine(conselho);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Erro ao consumir a API:");
                Console.WriteLine(ex.Message);
            }
        }

        Console.ReadKey();
    }
}