namespace SunamoTextOutputGenerator;

/// <summary>
/// Represents the text output generator type.
/// </summary>
public partial class TextOutputGenerator
{
    private const string headerCharacter = "*";

    /// <summary>
    /// Gets or sets the builder.
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
    /// Undo.
    /// </summary>
    /// <returns>The undo result.</returns>
    public void Undo()
    {
        ThrowEx.NotImplementedMethod();
    }

    /// <summary>
    /// Initializes a new instance of the end run time class.
    /// </summary>
    public void EndRunTime()
    {
        Builder.AppendLine("AppWillBeTerminated");
    }

    /// <summary>
    /// Initializes a new instance of the no data class.
    /// </summary>
    /// <param name=""AppWillBeTerminated"">The app will be terminated.</param>
    public void NoData()
    {
        Builder.AppendLine("NoData");
    }

    /// <summary>
    /// Initializes a new instance of the start run time class.
    /// </summary>
    /// <param name=""NoData"">The no data.</param>
    public void StartRunTime(string text)
    {
        var textLength = text.Length;
        var headerLine = new string(headerCharacter[0], textLength);
        Builder.AppendLine(headerLine);
        Builder.AppendLine(text);
        Builder.AppendLine(headerLine);
    }

    /// <summary>
    /// Initializes a new instance of the count every t class.
    /// </summary>
    /// <param name="headerLine">The header line.</param>
    public void CountEvery<T>(IList<KeyValuePair<T, int>> list)
    {
        foreach (var item in list)
            AppendLine($"{item.Key},{item.Value}x");
    }

    /// <summary>
    /// Initializes a new instance of the append line class.
    /// </summary>
    /// <param name="$"{item.Key}">The item key.</param>
    /// <param name="{item.Value}x"">The item value x.</param>
    public void AppendLine()
    {
        AppendLine(string.Empty);
    }

    /// <summary>
    /// Initializes a new instance of the append line class.
    /// </summary>
    /// <param name="string.Empty">The string empty.</param>
    public void AppendLine(StringBuilder stringBuilder)
    {
        Builder.AppendLine(stringBuilder.ToString());
    }

    /// <summary>
    /// Append.
    /// </summary>
    /// <param name="MethodImplOptions.AggressiveInlining">The method impl options aggressive inlining.</param>
    /// <returns>The append result.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(string text)
    {
        Builder.Append(text);
    }

    /// <summary>
    /// Append line.
    /// </summary>
    /// <param name="MethodImplOptions.AggressiveInlining">The method impl options aggressive inlining.</param>
    /// <returns>The append line result.</returns>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendLine(string text)
    {
        Builder.AppendLine(text);
    }

    /// <summary>
    /// Initializes a new instance of the append line format class.
    /// </summary>
    /// <param name="text">The text.</param>
    public void AppendLineFormat(string text, params string[] arguments)
    {
        Builder.AppendLine();
        AppendLine(string.Format(text, arguments));
    }

    /// <summary>
    /// Initializes a new instance of the append format class.
    /// </summary>
    /// <param name="arguments)">The arguments.</param>
    public void AppendFormat(string text, params string[] arguments)
    {
        AppendLine(string.Format(text, arguments));
    }

    /// <summary>
    /// Initializes a new instance of the header class.
    /// </summary>
    /// <param name="arguments)">The arguments.</param>
    public void Header(string text)
    {
        Builder.AppendLine();
        AppendLine(text);
        Builder.AppendLine();
    }

    /// <summary>
    /// Initializes a new instance of the single char line class.
    /// </summary>
    public void SingleCharLine(char paddingChar, int length)
    {
        Builder.AppendLine(string.Empty.PadLeft(length, paddingChar));
    }

    /// <summary>
    /// Initializes a new instance of the list object class.
    /// </summary>
    /// <param name="paddingChar)">The padding char.</param>
    public void ListObject(IList list)
    {
        var stringList = new List<string>();
        foreach (var item in list)
            stringList.Add(item?.ToString() ?? string.Empty);
        List(stringList);
    }

    /// <summary>
    /// Initializes a new instance of the list SB class.
    /// </summary>
    /// <param name="stringList">The string list.</param>
    public void ListSB(StringBuilder stringBuilder, string header)
    {
        Header(header);
        AppendLine(stringBuilder);
    }

    /// <summary>
    /// Initializes a new instance of the list class.
    /// </summary>
    /// <param name="stringBuilder">The string builder.</param>
    public void List(IList<string> list)
    {
        List<string>(list);
    }

    /// <summary>
    /// Initializes a new instance of the list t value class.
    /// </summary>
    /// <typeparam name="string">The type string.</typeparam>
    /// <param name="list">The list.</param>
    public void List<TValue>(IList<TValue> list, string delimiter = "\r\n", string whenNoEntries = "")
    {
        if (list.Count() == 0)
            Builder.AppendLine(whenNoEntries);
        else
            foreach (var item in list)
                Append(item + delimiter);
    }

    /// <summary>
    /// Initializes a new instance of the list t header t value class.
    /// </summary>
    /// <param name="delimiter">The delimiter.</param>
    public void List<THeader, TValue>(IList<TValue> list, THeader header)
        where THeader : IEnumerable<char>
    {
        List(list, header, new TextOutputGeneratorArgs { IsHeaderWrappedWithEmptyLines = true, IsInsertingCount = false });
    }

    /// <summary>
    /// Initializes a new instance of the list class.
    /// </summary>
    /// <param name="list">The list.</param>
    /// <param name="header">The header.</param>
    /// <param name="IsHeaderWrappedWithEmptyLines">The is header wrapped with empty lines.</param>
    public void List(IList<string> list, string header)
    {
        List(list, header, new TextOutputGeneratorArgs { IsHeaderWrappedWithEmptyLines = true, IsInsertingCount = false });
    }

    /// <summary>
    /// Initializes a new instance of the list string class.
    /// </summary>
    /// <param name="list">The list.</param>
    /// <param name="header">The header.</param>
    /// <param name="IsHeaderWrappedWithEmptyLines">The is header wrapped with empty lines.</param>
    public void ListString(string text, string header)
    {
        Header(header);
        AppendLine(text);
        Builder.AppendLine();
    }

    /// <summary>
    /// Initializes a new instance of the list t header t value class.
    /// </summary>
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
    /// Initializes a new instance of the paragraph class.
    /// </summary>
    /// <param name="list">The list.</param>
    /// <param name="args.Delimiter">The args delimiter.</param>
    /// <param name="args.WhenNoEntries">The args when no entries.</param>
    public void Paragraph(StringBuilder stringBuilder, string header)
    {
        var text = stringBuilder.ToString().Trim();
        Paragraph(text, header);
    }

    /// <summary>
    /// Initializes a new instance of the paragraph class.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <param name="header">The header.</param>
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
    /// Dictionary.
    /// </summary>
    /// <param name="dictionary">The dictionary.</param>
    /// <param name="delimiter">The delimiter.</param>
    /// <returns>The dictionary result.</returns>
    public void Dictionary(Dictionary<string, int> dictionary, string delimiter)
    {
        foreach (var item in dictionary)
            Builder.AppendLine(item.Key + delimiter + item.Value);
    }

    /// <summary>
    /// Initializes a new instance of the dictionary key value pair t key t value class.
    /// </summary>
    /// <param name="item.Value">The item value.</param>
    public void DictionaryKeyValuePair<TKey, TValue>(string header, IOrderedEnumerable<KeyValuePair<TKey, TValue>> ordered)
    {
        Header(header);
        foreach (var item in ordered)
            Builder.AppendLine($"{item.Key} {item.Value}");
    }

    /// <summary>
    /// Initializes a new instance of the i grouping class.
    /// </summary>
    /// <param name="{item.Value}"">The item value.</param>
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
