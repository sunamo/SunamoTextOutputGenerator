namespace SunamoTextOutputGenerator;

public partial class TextOutputGenerator
{
    public void Dictionary(Dictionary<string, List<string>> dictionary)
    {
        foreach (var item in dictionary)
            List(item.Value, item.Key);
    }

    public void Dictionary<THeader, TValue>(Dictionary<THeader, List<TValue>> dictionary, bool isOnlyCountInValue = false)
        where THeader : notnull, IEnumerable<char>
    {
        if (isOnlyCountInValue)
        {
            var list = new List<string>(dictionary.Count);
            foreach (var item in dictionary)
                list.Add($"{item.Key} {item.Value.Count()}");
            List(list);
        }
        else
        {
            foreach (var item in dictionary)
                List(item.Value, item.Key);
        }
    }

    public void Dictionary(Dictionary<string, string> dictionary)
    {
        foreach (var item in dictionary)
            Builder.AppendLine(string.Join("|", item.Key, item.Value));
    }

    public void Dictionary<TKey, TValue>(Dictionary<TKey, TValue> dictionary, string delimiter = "|")
        where TKey : notnull
    {
        foreach (var item in dictionary)
            if (delimiter != "|")
            {
                Header(item.Key?.ToString() ?? string.Empty);
                Builder.AppendLine(string.Join(delimiter, item.Value?.ToString() ?? string.Empty));
                Builder.AppendLine();
            }
            else
            {
                Builder.AppendLine(string.Join(delimiter, item.Key?.ToString() ?? string.Empty, item.Value?.ToString() ?? string.Empty));
            }
    }

    public void PairBullet(string key, string value)
    {
        Builder.AppendLine($"{key}: {value}");
    }

    public string DictionaryBothToStringToSingleLine<TKey, TValue>(Dictionary<TKey, TValue> dictionary, bool isPuttingValueFirst, string delimiter = " ")
        where TKey : notnull
    {
        foreach (var item in dictionary)
        {
            string firstText;
            string secondText;
            if (isPuttingValueFirst)
            {
                firstText = item.Value?.ToString() ?? string.Empty;
                secondText = item.Key?.ToString() ?? string.Empty;
            }
            else
            {
                firstText = item.Key?.ToString() ?? string.Empty;
                secondText = item.Value?.ToString() ?? string.Empty;
            }

            Builder.AppendLine(firstText + delimiter + secondText);
        }

        return Builder.ToString();
    }
}
