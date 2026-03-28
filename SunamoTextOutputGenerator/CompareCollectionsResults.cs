namespace SunamoTextOutputGenerator;

/// <summary>
/// Collection of comparison results with text output generation capabilities.
/// </summary>
public class CompareCollectionsResults : List<CompareCollectionsResult<string>>
{
    /// <summary>
    /// Generates formatted text output from separate lists of comparison results.
    /// </summary>
    /// <param name="onlyInFirst">Items only in the first collection.</param>
    /// <param name="onlyInSecond">Items only in the second collection.</param>
    /// <param name="both">Items in both collections.</param>
    /// <returns>Formatted text output of the comparison.</returns>
    public static string TextOutput(List<string> onlyInFirst, List<string> onlyInSecond, List<string>? both = null)
    {
        return TextOutput(new CompareCollectionsResult<string>
            { Both = both, OnlyInFirst = onlyInFirst, OnlyInSecond = onlyInSecond });
    }

    /// <summary>
    /// Generates formatted text output from a comparison result.
    /// </summary>
    /// <param name="result">The comparison result to output.</param>
    /// <returns>Formatted text output of the comparison.</returns>
    public static string TextOutput(CompareCollectionsResult<string> result)
    {
        if (result != null)
        {
            var generator = new TextOutputGenerator();

            generator.Header("Managed:");

            if (result.OnlyInFirst != null)
                foreach (var item in result.OnlyInFirst) generator.Builder.AppendLine(item);

            generator.Header("Restored:");

            if (result.OnlyInSecond != null)
                foreach (var item in result.OnlyInSecond) generator.Builder.AppendLine(item);

            if (result.Both != null)
            {
                generator.Header("Founded:");

                foreach (var item in result.Both) generator.Builder.AppendLine(item);
            }

            return generator.ToString();
        }

        return string.Empty;
    }
}
