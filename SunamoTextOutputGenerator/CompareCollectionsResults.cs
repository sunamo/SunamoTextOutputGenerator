namespace SunamoTextOutputGenerator;

public class CompareCollectionsResults : List<CompareCollectionsResult<string>>
{
    public static string TextOutput(List<string> onlyInFirst, List<string> onlyInSecond, List<string>? both = null)
    {
        return TextOutput(new CompareCollectionsResult<string>
            { Both = both, OnlyInFirst = onlyInFirst, OnlyInSecond = onlyInSecond });
    }

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
