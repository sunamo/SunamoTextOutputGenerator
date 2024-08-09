namespace SunamoTextOutputGenerator;

public class CompareCollectionsResults : List<CompareCollectionsResult<string>>
{
    public static string TextOutput(List<string> onlyInFirst, List<string> onlyInSecond, List<string> both = null)
    {
        return TextOutput(new CompareCollectionsResult<string>
            { Both = both, OnlyInFirst = onlyInFirst, OnlyInSecond = onlyInSecond });
    }

    public static string TextOutput(CompareCollectionsResult<string> result)
    {
        if (result != null)
        {
            var textOutputGenerator = new TextOutputGenerator();

            textOutputGenerator.Header("Managed:");

            foreach (var item in result.OnlyInFirst) textOutputGenerator.sb.AppendLine(item);

            textOutputGenerator.Header("Restored:");

            foreach (var item in result.OnlyInSecond) textOutputGenerator.sb.AppendLine(item);

            if (result.Both != null)
            {
                textOutputGenerator.Header("Founded:");

                foreach (var item in result.Both) textOutputGenerator.sb.AppendLine(item);
            }

            return textOutputGenerator.ToString();
        }

        return string.Empty;
    }
}