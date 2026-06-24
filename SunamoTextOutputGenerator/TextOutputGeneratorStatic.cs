namespace SunamoTextOutputGenerator;

public class TextOutputGeneratorStatic
{
    public static string CompareList(List<string> both, string firstHeader, List<string> firstList, string secondHeader, List<string> secondList)
    {
        var generator = new TextOutputGenerator();
        generator.List(both, "both");
        generator.List(firstList, firstHeader);
        generator.List(secondList, secondHeader);

        return generator.ToString();
    }

    public static string Dictionary(Dictionary<string, string> dictionary)
    {
        var generator = new TextOutputGenerator();
        generator.Dictionary(dictionary);
        return generator.ToString();
    }

    public static string DictionaryWithCount<TKey, TValue>(Dictionary<TKey, List<TValue>> dictionary)
        where TKey : notnull
    {
        var generator = new TextOutputGenerator();
        foreach (var item in dictionary) generator.List(item.Value, item.Key?.ToString() ?? string.Empty);
        return generator.ToString();
    }
}
