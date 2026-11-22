namespace SunamoTextOutputGenerator;

/// <summary>
/// Represents the text output generator static type.
/// </summary>
public class TextOutputGeneratorStatic
{
    /// <summary>
    /// Compare list.
    /// </summary>
    /// <param name="both">The both.</param>
    /// <param name="firstHeader">The first header.</param>
    /// <param name="firstList">The first list.</param>
    /// <param name="secondHeader">The second header.</param>
    /// <param name="secondList">The second list.</param>
    /// <returns>The compare list result.</returns>
    public static string CompareList(List<string> both, string firstHeader, List<string> firstList, string secondHeader, List<string> secondList)
    {
        var generator = new TextOutputGenerator();
        generator.List(both, "both");
        generator.List(firstList, firstHeader);
        generator.List(secondList, secondHeader);

        return generator.ToString();
    }

    /// <summary>
    /// Dictionary.
    /// </summary>
    /// <param name="dictionary">The dictionary.</param>
    /// <returns>The dictionary result.</returns>
    public static string Dictionary(Dictionary<string, string> dictionary)
    {
        var generator = new TextOutputGenerator();
        generator.Dictionary(dictionary);
        return generator.ToString();
    }

    /// <summary>
    /// Dictionary with count t key t value.
    /// </summary>
    /// <param name="dictionary">The dictionary.</param>
    /// <returns>The dictionary with count t key t value result.</returns>
    public static string DictionaryWithCount<TKey, TValue>(Dictionary<TKey, List<TValue>> dictionary)
        where TKey : notnull
    {
        var generator = new TextOutputGenerator();
        foreach (var item in dictionary) generator.List(item.Value, item.Key?.ToString() ?? string.Empty);
        return generator.ToString();
    }
}
