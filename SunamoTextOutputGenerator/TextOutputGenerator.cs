namespace SunamoTextOutputGenerator;

/// <summary>
/// Generates formatted text output with headers, lists, paragraphs, and dictionary formatting.
/// </summary>
public partial class TextOutputGenerator
{
    private const string headerCharacter = "*";

    /// <summary>
    /// Gets or sets the StringBuilder used for text accumulation.
    /// </summary>
    public StringBuilder Builder { get; set; } = new();

    /// <summary>
    /// Creates a new instance of <see cref="TextOutputGenerator"/>.
    /// </summary>
    /// <returns>A new TextOutputGenerator instance.</returns>
    public static TextOutputGenerator Create() => new TextOutputGenerator();

    /// <summary>
    /// Returns all accumulated text as a string.
    /// </summary>
    /// <returns>The generated text output.</returns>
    public override string ToString() => Builder.ToString();

    /// <summary>
    /// Undoes the last operation. Not implemented.
    /// </summary>
    public void Undo()
    {
        ThrowEx.NotImplementedMethod();
    }

    /// <summary>
    /// Appends a termination message.
    /// </summary>
    public void EndRunTime()
    {
        Builder.AppendLine("AppWillBeTerminated");
    }

    /// <summary>
    /// Appends a message indicating no data is available.
    /// </summary>
    public void NoData()
    {
        Builder.AppendLine("NoData");
    }

    /// <summary>
    /// Writes a decorated header with the given text surrounded by repeated characters.
    /// </summary>
    /// <param name="text">The header text to display.</param>
    public void StartRunTime(string text)
    {
        var textLength = text.Length;
        var headerLine = new string(headerCharacter[0], textLength);
        Builder.AppendLine(headerLine);
        Builder.AppendLine(text);
        Builder.AppendLine(headerLine);
    }

    /// <summary>
    /// Outputs each key-value pair with the count appended.
    /// </summary>
    /// <typeparam name="T">The type of the key.</typeparam>
    /// <param name="list">The list of key-value pairs to output.</param>
    public void CountEvery<T>(IList<KeyValuePair<T, int>> list)
    {
        foreach (var item in list)
            AppendLine($"{item.Key},{item.Value}x");
    }

    /// <summary>
    /// Appends an empty line.
    /// </summary>
    public void AppendLine()
    {
        AppendLine(string.Empty);
    }

    /// <summary>
    /// Appends the content of a StringBuilder followed by a new line.
    /// </summary>
    /// <param name="stringBuilder">The StringBuilder whose content to append.</param>
    public void AppendLine(StringBuilder stringBuilder)
    {
        Builder.AppendLine(stringBuilder.ToString());
    }

    /// <summary>
    /// Appends text without a trailing new line.
    /// </summary>
    /// <param name="text">The text to append.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(string text)
    {
        Builder.Append(text);
    }

    /// <summary>
    /// Appends text followed by a new line.
    /// </summary>
    /// <param name="text">The text to append.</param>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendLine(string text)
    {
        Builder.AppendLine(text);
    }

    /// <summary>
    /// Appends a blank line, then formatted text followed by a new line.
    /// </summary>
    /// <param name="text">The format string.</param>
    /// <param name="arguments">The format arguments.</param>
    public void AppendLineFormat(string text, params string[] arguments)
    {
        Builder.AppendLine();
        AppendLine(string.Format(text, arguments));
    }

    /// <summary>
    /// Appends formatted text followed by a new line.
    /// </summary>
    /// <param name="text">The format string.</param>
    /// <param name="arguments">The format arguments.</param>
    public void AppendFormat(string text, params string[] arguments)
    {
        AppendLine(string.Format(text, arguments));
    }

    /// <summary>
    /// Appends a header surrounded by empty lines.
    /// </summary>
    /// <param name="text">The header text.</param>
    public void Header(string text)
    {
        Builder.AppendLine();
        AppendLine(text);
        Builder.AppendLine();
    }

    /// <summary>
    /// Appends a line of repeated characters.
    /// </summary>
    /// <param name="paddingChar">The character to repeat.</param>
    /// <param name="length">The number of times to repeat the character.</param>
    public void SingleCharLine(char paddingChar, int length)
    {
        Builder.AppendLine(string.Empty.PadLeft(length, paddingChar));
    }

    /// <summary>
    /// Outputs objects as a list by converting each to string.
    /// </summary>
    /// <param name="list">The list of objects to output.</param>
    public void ListObject(IList list)
    {
        var stringList = new List<string>();
        foreach (var item in list)
            stringList.Add(item?.ToString() ?? string.Empty);
        List(stringList);
    }

    /// <summary>
    /// Outputs a StringBuilder content with a header.
    /// </summary>
    /// <param name="stringBuilder">The StringBuilder content to output.</param>
    /// <param name="header">The header text.</param>
    public void ListSB(StringBuilder stringBuilder, string header)
    {
        Header(header);
        AppendLine(stringBuilder);
    }

    /// <summary>
    /// Outputs a list of strings, one per line.
    /// </summary>
    /// <param name="list">The list of strings to output.</param>
    public void List(IList<string> list)
    {
        List<string>(list);
    }

    /// <summary>
    /// Outputs a list of values with a specified delimiter.
    /// </summary>
    /// <typeparam name="TValue">The type of the list elements.</typeparam>
    /// <param name="list">The list of values to output.</param>
    /// <param name="delimiter">The delimiter between entries.</param>
    /// <param name="whenNoEntries">Text to display when the list is empty.</param>
    public void List<TValue>(IList<TValue> list, string delimiter = "\r\n", string whenNoEntries = "")
    {
        if (list.Count() == 0)
            Builder.AppendLine(whenNoEntries);
        else
            foreach (var item in list)
                Append(item + delimiter);
    }

    /// <summary>
    /// Outputs a list with a header. Header must implement IEnumerable of char (like string).
    /// </summary>
    /// <typeparam name="THeader">The header type (must be IEnumerable of char).</typeparam>
    /// <typeparam name="TValue">The type of the list elements.</typeparam>
    /// <param name="list">The list of values to output.</param>
    /// <param name="header">The header text.</param>
    public void List<THeader, TValue>(IList<TValue> list, THeader header)
        where THeader : IEnumerable<char>
    {
        List(list, header, new TextOutputGeneratorArgs { IsHeaderWrappedWithEmptyLines = true, IsInsertingCount = false });
    }

    /// <summary>
    /// Outputs a list of strings with a header.
    /// </summary>
    /// <param name="list">The list of strings to output.</param>
    /// <param name="header">The header text.</param>
    public void List(IList<string> list, string header)
    {
        List(list, header, new TextOutputGeneratorArgs { IsHeaderWrappedWithEmptyLines = true, IsInsertingCount = false });
    }

    /// <summary>
    /// Outputs a string value with a header.
    /// </summary>
    /// <param name="text">The text to output.</param>
    /// <param name="header">The header text.</param>
    public void ListString(string text, string header)
    {
        Header(header);
        AppendLine(text);
        Builder.AppendLine();
    }

    /// <summary>
    /// Outputs a list with a header and formatting options.
    /// </summary>
    /// <typeparam name="THeader">The header type (must be IEnumerable of char).</typeparam>
    /// <typeparam name="TValue">The type of the list elements.</typeparam>
    /// <param name="list">The list of values to output.</param>
    /// <param name="header">The header text.</param>
    /// <param name="args">Formatting arguments controlling output behavior.</param>
    public void List<THeader, TValue>(IList<TValue> list, THeader header, TextOutputGeneratorArgs args)
        where THeader : IEnumerable<char>
    {
        if (args.IsHeaderWrappedWithEmptyLines)
            Builder.AppendLine();
        Builder.AppendLine(header + ":");
        if (args.IsHeaderWrappedWithEmptyLines)
            Builder.AppendLine();
        List(list, args.Delimiter, args.WhenNoEntries);
    }

    /// <summary>
    /// Outputs a paragraph from a StringBuilder with a header.
    /// </summary>
    /// <param name="stringBuilder">The StringBuilder containing the paragraph text.</param>
    /// <param name="header">The header text.</param>
    public void Paragraph(StringBuilder stringBuilder, string header)
    {
        var text = stringBuilder.ToString().Trim();
        Paragraph(text, header);
    }

    /// <summary>
    /// Outputs a paragraph with a header. Only outputs if text is not empty.
    /// </summary>
    /// <param name="text">The paragraph text.</param>
    /// <param name="header">The header text.</param>
    public void Paragraph(string text, string header)
    {
        if (text != string.Empty)
        {
            Builder.AppendLine(header + ":");
            Builder.AppendLine(text);
            Builder.AppendLine();
        }
    }

    /// <summary>
    /// Outputs a dictionary of string-int pairs with a delimiter.
    /// </summary>
    /// <param name="dictionary">The dictionary to output.</param>
    /// <param name="delimiter">The delimiter between key and value.</param>
    public void Dictionary(Dictionary<string, int> dictionary, string delimiter)
    {
        foreach (var item in dictionary)
            Builder.AppendLine(item.Key + delimiter + item.Value);
    }

    /// <summary>
    /// Outputs ordered key-value pairs with a header.
    /// </summary>
    /// <typeparam name="TKey">The type of the key.</typeparam>
    /// <typeparam name="TValue">The type of the value.</typeparam>
    /// <param name="header">The header text.</param>
    /// <param name="ordered">The ordered key-value pairs to output.</param>
    public void DictionaryKeyValuePair<TKey, TValue>(string header, IOrderedEnumerable<KeyValuePair<TKey, TValue>> ordered)
    {
        Header(header);
        foreach (var item in ordered)
            Builder.AppendLine($"{item.Key} {item.Value}");
    }

    /// <summary>
    /// Outputs grouped strings as a dictionary.
    /// </summary>
    /// <param name="grouping">The grouped strings to output.</param>
    public void IGrouping(IEnumerable<IGrouping<string, string>> grouping)
    {
        var dictionary = IGroupingToDictionary(grouping);
        Dictionary(dictionary);
    }

    private Dictionary<string, List<string>> IGroupingToDictionary(IEnumerable<IGrouping<string, string>> grouping)
    {
        var dictionary = new Dictionary<string, List<string>>();
        foreach (var item in grouping)
            dictionary.Add(item.Key, item.ToList());
        return dictionary;
    }
}
