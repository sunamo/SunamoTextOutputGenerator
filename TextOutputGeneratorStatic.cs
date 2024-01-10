namespace SunamoTextOutputGenerator;

public class TextOutputGeneratorStatic
{
    public static string CompareList(List<string> both, string v1, List<string> reposNames, string v2, List<string> l)
    {
        TextOutputGenerator tog = new TextOutputGenerator();
        tog.List(both, "both");
        tog.List(reposNames, v1);
        tog.List(l, v2);

        return tog.ToString();
    }

    public static string Dictionary(Dictionary<string, string> codeToErrorDescription)
    {
        TextOutputGenerator tog = new TextOutputGenerator();
        tog.Dictionary(codeToErrorDescription);
        return tog.ToString();
    }

    public static string DictionaryWithCount<T, U>(Dictionary<T, List<U>> dict)
    {
        TextOutputGenerator tog = new TextOutputGenerator();
        foreach (var item in dict)
        {
            tog.List(item.Value, item.Key.ToString());
        }
        return tog.ToString();
    }
}
