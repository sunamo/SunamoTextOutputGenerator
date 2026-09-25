namespace SunamoTextOutputGenerator;

/// <summary>
/// Static text generation utilities.
/// TextWriterList - instance, TextBuilder - instance, TextOutputGenerator - instance, TextGenerator - static.
/// </summary>
public static class TextGenerator
{
    /// <summary>
    /// Generates a list with percent distribution from a string dictionary.
    /// Keep as IList, not List because to IList can be casted every List.
    /// </summary>
    /// <param name="dictionary">The dictionary of category names to their string values.</param>
    /// <returns>Formatted text with percentage distribution.</returns>
    public static string GenerateListWithPercent(Dictionary<string, List<string>> dictionary)
    {
        return GenerateListWithPercent<string, string>(dictionary);
    }

    /// <summary>
    /// Generates a list with percent distribution from a generic dictionary with List values.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the list elements.</typeparam>
    /// <param name="dictionary">The dictionary to process.</param>
    /// <returns>Formatted text with percentage distribution.</returns>
    public static string GenerateListWithPercent<TKey, TValue>(Dictionary<TKey, List<TValue>> dictionary)
        where TKey : notnull
    {
        var converted = new Dictionary<TKey, List<TValue>>(dictionary.Count);
        foreach (var item in dictionary) converted.Add(item.Key, item.Value);

        return GenerateListWithPercent(converted);
    }

    /// <summary>
    /// Generates a list with percent distribution from a generic dictionary with IList values.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the list elements.</typeparam>
    /// <param name="dictionary">The dictionary to process.</param>
    /// <param name="percentCalculatorFactory">Factory for creating percent calculators.</param>
    /// <returns>Formatted text with percentage distribution.</returns>
    public static string GenerateListWithPercent<TKey, TValue>(Dictionary<TKey, IList<TValue>> dictionary, IPercentCalculatorTog percentCalculatorFactory)
        where TKey : notnull
    {
        var overallCount = 0;

        foreach (var item in dictionary) overallCount += item.Value.Count();

        var percentCalculator = percentCalculatorFactory.Create(overallCount);

        var generator = new TextOutputGenerator();

        var withoutLast = dictionary.Take(dictionary.Count() - 1);

        var currentPercent = 0;
        var totalPercent = 0;

        var lastEntry = dictionary.Last();

        var percentMap = new Dictionary<TKey, int>();

        foreach (var item in withoutLast)
        {
            currentPercent = percentCalculator.PercentFor(item.Value.Count(), false);

            totalPercent += currentPercent;

            percentMap.Add(item.Key, currentPercent);
        }

        currentPercent = percentCalculator.PercentFor(lastEntry.Value.Count(), false);
        totalPercent += currentPercent;
        percentMap.Add(lastEntry.Key, currentPercent);

        var largestPercent = 0;
        TKey? largestKey = default;

        if (totalPercent != 0)
        {
            foreach (var item in percentMap)
                if (item.Value > largestPercent)
                {
                    largestPercent = item.Value;
                    largestKey = item.Key;
                    break;
                }

            if (largestKey is not null)
            {
                percentMap[largestKey] = percentMap[largestKey] + (100 - totalPercent);
            }
        }

        foreach (var item in withoutLast)
            generator.List(item.Value, $"{item.Key} ({percentMap[item.Key]}%)");

        generator.List(lastEntry.Value, $"{lastEntry.Key} ({100 - currentPercent}%)");
        return generator.ToString();
    }
}
