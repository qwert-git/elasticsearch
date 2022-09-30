using EnglishWordsFeed;
using MoreLinq;

Console.WriteLine("Start feeding...");

var client = new ElasticClient("http://localhost:9200", "words.completion");
var reader = new WordsFileReader();
var allWords = await reader.ReadAllAsync();

Console.WriteLine($"Total words count: {allWords.Count()}");

var wordsEntity = allWords.Select(w => new WordEntity { Word = w, CreatedAt = DateTime.Now });
foreach (var batch in wordsEntity.Batch(100))
{
    try
    {
        await client.AddToIndexAsync(batch);
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Inserting batch failed.. {ex.Message}");
    }
}

Console.WriteLine("Finished!");