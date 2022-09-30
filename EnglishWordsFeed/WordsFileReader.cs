namespace EnglishWordsFeed;

internal class WordsFileReader
{
    public async Task<IEnumerable<string>> ReadAllAsync()
    {
        var words = await File.ReadAllLinesAsync("words_alpha.txt");
        return words.Where(w => w.Length > 4).ToList();
    }
}