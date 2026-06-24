namespace SunamoTextOutputGenerator;

public partial class TextOutputGenerator
{
    private const string headerCharacter = "*";

    public StringBuilder Builder { get; set; } = new();

    public static TextOutputGenerator Create() => new TextOutputGenerator();

    public override string ToString() => Builder.ToString();

    public void Undo()
    {
        ThrowEx.NotImplementedMethod();
    }

    public void EndRunTime()
    {
        Builder.AppendLine("AppWillBeTerminated");
    }

    public void NoData()
    {
        Builder.AppendLine("NoData");
    }

    public void StartRunTime(string text)
    {
        var textLength = text.Length;
        var headerLine = new string(headerCharacter[0], textLength);
        Builder.AppendLine(headerLine);
        Builder.AppendLine(text);
        Builder.AppendLine(headerLine);
    }

    public void CountEvery<T>(IList<KeyValuePair<T, int>> list)
    {
        foreach (var item in list)
            AppendLine($"{item.Key},{item.Value}x");
    }

    public void AppendLine()
    {
        AppendLine(string.Empty);
    }

    public void AppendLine(StringBuilder stringBuilder)
    {
        Builder.AppendLine(stringBuilder.ToString());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(string text)
    {
        Builder.Append(text);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendLine(string text)
    {
        Builder.AppendLine(text);
    }

    public void AppendLineFormat(string text, params string[] arguments)
    {
        Builder.AppendLine();
        AppendLine(string.Format(text, arguments));
    }

    public void AppendFormat(string text, params string[] arguments)
    {
        AppendLine(string.Format(text, arguments));
    }

    public void Header(string text)
    {
        Builder.AppendLine();
        AppendLine(text);
        Builder.AppendLine();
    }

    public void SingleCharLine(char paddingChar, int length)
    {
        Builder.AppendLine(string.Empty.PadLeft(length, paddingChar));
    }

    public void ListObject(IList list)
    {
        var stringList = new List<string>();
        foreach (var item in list)
            stringList.Add(item?.ToString() ?? string.Empty);
        List(stringList);
    }

    public void ListSB(StringBuilder stringBuilder, string header)
    {
        Header(header);
        AppendLine(stringBuilder);
    }

    public void List(IList<string> list)
    {
        List<string>(list);
    }

    public void List<TValue>(IList<TValue> list, string delimiter = "\r\n", string whenNoEntries = "")
    {
        if (list.Count() == 0)
            Builder.AppendLine(whenNoEntries);
        else
            foreach (var item in list)
                Append(item + delimiter);
    }

    public void List<THeader, TValue>(IList<TValue> list, THeader header)
        where THeader : IEnumerable<char>
    {
        List(list, header, new TextOutputGeneratorArgs { IsHeaderWrappedWithEmptyLines = true, IsInsertingCount = false });
    }

    public void List(IList<string> list, string header)
    {
        List(list, header, new TextOutputGeneratorArgs { IsHeaderWrappedWithEmptyLines = true, IsInsertingCount = false });
    }

    public void ListString(string text, string header)
    {
        Header(header);
        AppendLine(text);
        Builder.AppendLine();
    }

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

    public void Paragraph(StringBuilder stringBuilder, string header)
    {
        var text = stringBuilder.ToString().Trim();
        Paragraph(text, header);
    }

    public void Paragraph(string text, string header)
    {
        if (text != string.Empty)
        {
            Builder.AppendLine(header + ":");
            Builder.AppendLine(text);
            Builder.AppendLine();
        }
    }

    public void Dictionary(Dictionary<string, int> dictionary, string delimiter)
    {
        foreach (var item in dictionary)
            Builder.AppendLine(item.Key + delimiter + item.Value);
    }

    public void DictionaryKeyValuePair<TKey, TValue>(string header, IOrderedEnumerable<KeyValuePair<TKey, TValue>> ordered)
    {
        Header(header);
        foreach (var item in ordered)
            Builder.AppendLine($"{item.Key} {item.Value}");
    }

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
