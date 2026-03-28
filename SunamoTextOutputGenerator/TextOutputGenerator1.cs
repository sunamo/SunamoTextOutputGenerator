namespace SunamoTextOutputGenerator;

/// <summary>
/// Partial class extending TextOutputGenerator with dictionary output methods.
/// </summary>
public partial class TextOutputGenerator
{
    /// <summary>
    /// Outputs a dictionary where each entry's values are listed under the key as header.
    /// </summary>
    /// <param name="dictionary">The dictionary to output.</param>
    public void Dictionary(Dictionary<string, List<string>> dictionary)
    {
        foreach (var item in dictionary)
            List(item.Value, item.Key);
    }

    /// <summary>
    /// Outputs a generic dictionary with optional count-only mode.
    /// </summary>
    /// <typeparam name="THeader">The header type (must be IEnumerable of char).</typeparam>
    /// <typeparam name="TValue">The type of the list elements.</typeparam>
    /// <param name="dictionary">The dictionary to output.</param>
    /// <param name="isOnlyCountInValue">When true, outputs only key and count instead of full list.</param>
    public void Dictionary<THeader, TValue>(Dictionary<THeader, List<TValue>> dictionary, bool isOnlyCountInValue = false)
        where THeader : notnull, IEnumerable<char>
    {
        if (isOnlyCountInValue)
        {
            var list = new List<string>(dictionary.Count);
            foreach (var item in dictionary)
                list.Add(item.Key + " " + item.Value.Count());
            List(list);
        }
        else
        {
            foreach (var item in dictionary)
                List(item.Value, item.Key);
        }
    }

    /// <summary>
    /// Outputs a string-string dictionary with each entry on one line separated by pipe.
    /// </summary>
    /// <param name="dictionary">The dictionary to output.</param>
    public void Dictionary(Dictionary<string, string> dictionary)
    {
        foreach (var item in dictionary)
            Builder.AppendLine(string.Join("|", item.Key, item.Value));
    }

    /// <summary>
    /// Outputs a generic dictionary with a configurable delimiter.
    /// When delimiter is not pipe, uses header format with each value on separate lines.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <param name="dictionary">The dictionary to output.</param>
    /// <param name="delimiter">The delimiter between key and value.</param>
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
    /// Outputs a key-value pair as a bullet point line.
    /// </summary>
    /// <param name="key">The key text.</param>
    /// <param name="value">The value text.</param>
    public void PairBullet(string key, string value)
    {
        Builder.AppendLine(key + ": " + value);
    }

    /// <summary>
    /// Outputs a dictionary to single lines with both key and value converted to string.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <param name="dictionary">The dictionary to output.</param>
    /// <param name="isPuttingValueFirst">Whether to put the value before the key in output.</param>
    /// <param name="delimiter">The delimiter between key and value.</param>
    /// <returns>The generated text output.</returns>
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
