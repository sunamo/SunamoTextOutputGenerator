namespace SunamoTextOutputGenerator;

/// <summary>
/// Represents the compare collections results type.
/// </summary>
public class CompareCollectionsResults : List<CompareCollectionsResult<string>>
{
    /// <summary>
    /// Text output.
    /// </summary>
    /// <param name="onlyInFirst">The only in first.</param>
    /// <param name="onlyInSecond">The only in second.</param>
    /// <param name="both">The both.</param>
    /// <returns>The text output result.</returns>
    public static string TextOutput(List<string> onlyInFirst, List<string> onlyInSecond, List<string>? both = null)
    {
        return TextOutput(new CompareCollectionsResult<string>
            { Both = both, OnlyInFirst = onlyInFirst, OnlyInSecond = onlyInSecond });
    }

    /// <summary>
    /// Text output.
    /// </summary>
    /// <param name="result">The result.</param>
    /// <returns>The text output result.</returns>
    public static string TextOutput(CompareCollectionsResult<string> result)
    {
        if (result is not null)
        {
            var generator = new TextOutputGenerator();

            generator.Header("Managed:");

            if (result.OnlyInFirst is not null)
                foreach (var item in result.OnlyInFirst) generator.Builder.AppendLine(item);

            generator.Header("Restored:");

            if (result.OnlyInSecond is not null)
                foreach (var item in result.OnlyInSecond) generator.Builder.AppendLine(item);

            if (result.Both is not null)
            {
                generator.Header("Founded:");

                foreach (var item in result.Both) generator.Builder.AppendLine(item);
            }

            return generator.ToString();
        }

        return string.Empty;
    }
}
