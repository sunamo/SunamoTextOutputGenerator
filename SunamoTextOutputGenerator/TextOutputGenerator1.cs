namespace SunamoTextOutputGenerator;

public partial class TextOutputGenerator
{
    /// <summary>
    /// Dictionary.
    /// </summary>
    /// <param name="dictionary">The dictionary.</param>
    public void Dictionary(Dictionary<string, List<string>> dictionary)
    {
        foreach (var item in dictionary)
            List(item.Value, item.Key);
    }

    /// <summary>
    /// Dictionary t header t value.
    /// </summary>
    /// <param name="dictionary">The dictionary.</param>
    /// <param name="isOnlyCountInValue">The is only count in value.</param>
    /// <returns>The dictionary t header t value result.</returns>
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

    /// <summary>
    /// Dictionary.
    /// </summary>
    /// <param name="dictionary">The dictionary.</param>
    public void Dictionary(Dictionary<string, string> dictionary)
    {
        foreach (var item in dictionary)
            Builder.AppendLine(string.Join("|", item.Key, item.Value));
    }

    /// <summary>
    /// Dictionary t key t value.
    /// </summary>
    /// <param name="dictionary">The dictionary.</param>
    /// <param name="delimiter">The delimiter.</param>
    /// <returns>The dictionary t key t value result.</returns>
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

    /// <summary>
    /// Pair bullet.
    /// </summary>
    /// <param name="key">The key.</param>
    /// <param name="value">The value.</param>
    public void PairBullet(string key, string value)
    {
        Builder.AppendLine($"{key}: {value}");
    }

    /// <summary>
    /// Dictionary both to string to single line t key t value.
    /// </summary>
    /// <param name="dictionary">The dictionary.</param>
    /// <param name="isPuttingValueFirst">The is putting value first.</param>
    /// <param name="delimiter">The delimiter.</param>
    /// <returns>The dictionary both to string to single line t key t value result.</returns>
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
