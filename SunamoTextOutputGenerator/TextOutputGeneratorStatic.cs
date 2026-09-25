namespace SunamoTextOutputGenerator;

/// <summary>
/// Static helper methods for generating text output from collections and dictionaries.
/// </summary>
public class TextOutputGeneratorStatic
{
    /// <summary>
    /// Compares two lists and outputs them with headers showing which items are in both, first only, or second only.
    /// </summary>
    /// <param name="both">Items present in both lists.</param>
    /// <param name="firstHeader">Header for the first list.</param>
    /// <param name="firstList">The first list of items.</param>
    /// <param name="secondHeader">Header for the second list.</param>
    /// <param name="secondList">The second list of items.</param>
    /// <returns>Formatted text output comparing the lists.</returns>
    public static string CompareList(List<string> both, string firstHeader, List<string> firstList, string secondHeader, List<string> secondList)
    {
        var generator = new TextOutputGenerator();
        generator.List(both, "both");
        generator.List(firstList, firstHeader);
        generator.List(secondList, secondHeader);

        return generator.ToString();
    }

    /// <summary>
    /// Outputs a string-string dictionary as formatted text.
    /// </summary>
    /// <param name="dictionary">The dictionary to output.</param>
    /// <returns>Formatted text output of the dictionary.</returns>
    public static string Dictionary(Dictionary<string, string> dictionary)
    {
        var generator = new TextOutputGenerator();
        generator.Dictionary(dictionary);
        return generator.ToString();
    }

    /// <summary>
    /// Outputs a dictionary with list values, showing each list under its key as header.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the list elements.</typeparam>
    /// <param name="dictionary">The dictionary to output.</param>
    /// <returns>Formatted text output of the dictionary with counts.</returns>
    public static string DictionaryWithCount<TKey, TValue>(Dictionary<TKey, List<TValue>> dictionary)
        where TKey : notnull
    {
        var generator = new TextOutputGenerator();
        foreach (var item in dictionary) generator.List(item.Value, item.Key?.ToString() ?? string.Empty);
        return generator.ToString();
    }
}
