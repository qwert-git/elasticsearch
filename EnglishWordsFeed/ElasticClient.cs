using System.Text;
using Newtonsoft.Json;

namespace EnglishWordsFeed;

internal class ElasticClient
{
    private readonly HttpClient http;
    private readonly string _index;

    public ElasticClient(string baseUrl, string index)
    {
        http = new HttpClient() { BaseAddress = new Uri("http://localhost:9200") };
        _index = index;
    }

    public async Task AddToIndexAsync(IEnumerable<WordEntity> words)
    {
        await Task.WhenAll(words.Select(s =>
        {
            var json = JsonConvert.SerializeObject(s);
            var content = new StringContent(json, Encoding.UTF8, "application/json");
            
            return http.PostAsync($"/{_index}/_doc/", content);
        }));
    }
}