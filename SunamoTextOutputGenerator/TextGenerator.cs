namespace SunamoTextOutputGenerator;

// TextWriterList - instance, TextBuilder - instance, TextOutputGenerator - instance, TextGenerator - static.
/// <summary>
/// Represents the text generator type.
/// </summary>
public static class TextGenerator
{
    // Keep as IList, not List because to IList can be casted every List.
    /// <summary>
    /// Generate list with percent.
    /// </summary>
    /// <param name="dictionary">The dictionary.</param>
    /// <returns>The generate list with percent result.</returns>
    public static string GenerateListWithPercent(Dictionary<string, List<string>> dictionary)
    {
        return GenerateListWithPercent<string, string>(dictionary);
    }

    /// <summary>
    /// Generate list with percent t key t value.
    /// </summary>
    /// <param name="dictionary">The dictionary.</param>
    /// <returns>The generate list with percent t key t value result.</returns>
    public static string GenerateListWithPercent<TKey, TValue>(Dictionary<TKey, List<TValue>> dictionary)
        where TKey : notnull
    {
        if (dictionary.Count == 0) return string.Empty;
        var converted = new Dictionary<TKey, IList<TValue>>(dictionary.Count);
        foreach (var item in dictionary) converted.Add(item.Key, item.Value);

        return GenerateListWithPercent(converted, new DefaultPercentCalculator());
    }

    /// <summary>
    /// Whole-number percent calculator used when the caller does not supply one.
    /// </summary>
    private sealed class DefaultPercentCalculator : IPercentCalculatorTog
    {
        /// <inheritdoc />
        public double OverallSum { get; set; }

        /// <inheritdoc />
        public double Last { get; set; }

        /// <inheritdoc />
        public IPercentCalculatorTog Create(double overallSum) => new DefaultPercentCalculator { OverallSum = overallSum };

        /// <inheritdoc />
        public void AddOnePercent() => Last += OverallSum == 0 ? 0 : 100d / OverallSum;

        /// <inheritdoc />
        public int PercentFor(double value, bool isLast) => OverallSum == 0 ? 0 : (int)(100d * (value / OverallSum));

        /// <inheritdoc />
        public void ResetComputedSum()
        {
        }
    }

    /// <summary>
    /// Generate list with percent t key t value.
    /// </summary>
    /// <param name="dictionary">The dictionary.</param>
    /// <param name="percentCalculatorFactory">The percent calculator factory.</param>
    /// <returns>The generate list with percent t key t value result.</returns>
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

        generator.List(lastEntry.Value, $"{lastEntry.Key} ({percentMap[lastEntry.Key]}%)");
        return generator.ToString();
    }
}
