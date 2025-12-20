// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoTextOutputGenerator;
/// <summary>
///     In Comparing
/// </summary>
public partial class TextOutputGenerator
{
    private static readonly string s_znakNadpisu = "*";
    // při převádění na nugety jsem to změnil na ITextBuilder stringBuilder = TextBuilder.Create();
    // ale asi to byla blbost, teď mám v _sunamo Create() která je ale null místo abych použil ctor
    // takže vracím nazpět.
    //public TextBuilder stringBuilder = new TextBuilder();
    public StringBuilder stringBuilder = new();
    //public string prependEveryNoWhite
    //{
    //    get => stringBuilder.prependEveryNoWhite;
    //    set => stringBuilder.prependEveryNoWhite = value;
    //}
    public static TextOutputGenerator Create()
    {
        return new TextOutputGenerator();
    }

    public override string ToString()
    {
        var ts = stringBuilder.ToString();
        return ts;
    }

    public void Undo()
    {
        ThrowEx.NotImplementedMethod();
    //stringBuilder.Undo();
    }

    public void EndRunTime()
    {
        stringBuilder.AppendLine("AppWillBeTerminated");
    }

    /// <summary>
    ///     Pouze vypíše "Az budete mit vstupní data, spusťte program znovu."
    /// </summary>
    public void NoData()
    {
        stringBuilder.AppendLine("NoData");
    }

    /// <summary>
    ///     Napíše nadpis A1 do konzole
    /// </summary>
    /// <param name = "text"></param>
    public void StartRunTime(string text)
    {
        var delkaTextu = text.Length;
        var hvezdicky = "";
        hvezdicky = new string (s_znakNadpisu[0], delkaTextu);
        //hvezdicky.PadLeft(delkaTextu, znakNadpisu[0]);
        stringBuilder.AppendLine(hvezdicky);
        stringBuilder.AppendLine(text);
        stringBuilder.AppendLine(hvezdicky);
    }

    public void CountEvery<T>(IList<KeyValuePair<T, int>> eq)
    {
        foreach (var item in eq)
            AppendLine(item.Key + "," + item.Value + "x");
    }

    public void AppendLine()
    {
        AppendLine(string.Empty);
    }

    public void AppendLine(StringBuilder text)
    {
        stringBuilder.AppendLine(text.ToString());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(string text)
    {
        stringBuilder.Append(text);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendLine(string text)
    {
        stringBuilder.AppendLine(text);
    }

    public void AppendLineFormat(string text, params string[] p)
    {
        stringBuilder.AppendLine();
        AppendLine(string.Format(text, p));
    }

    public void AppendFormat(string text, params string[] p)
    {
        AppendLine(string.Format(text, p));
    }

    public void Header(string v)
    {
        stringBuilder.AppendLine();
        AppendLine(v);
        stringBuilder.AppendLine();
    }

    public void SingleCharLine(char paddingChar, int v)
    {
        stringBuilder.AppendLine(string.Empty.PadLeft(v, paddingChar));
    }

    public void ListObject(IList files1)
    {
        var list = new List<string>();
        foreach (var item in files1)
            list.Add(item.ToString());
        List(list);
    }

    public void ListSB(StringBuilder onlyStart, string v)
    {
        Header(v);
        AppendLine(onlyStart);
    }

    /// <summary>
    ///     If you have StringBuilder, use Paragraph()
    /// </summary>
    /// <param name = "files1"></param>
    public void List(IList<string> files1)
    {
        List<string>(files1);
    }

    public void List<Value>(IList<Value> files1, string deli = "\r\n", string whenNoEntries = "")
    {
        if (files1.Count() == 0)
            stringBuilder.AppendLine(whenNoEntries);
        else
            foreach (var item in files1)
                Append(item + deli);
    //stringBuilder.AppendLine();
    }

    /// <summary>
    ///     must be where Header : IEnumerable<char> (like is string)
    /// </summary>
    /// <typeparam name = "Header"></typeparam>
    /// <typeparam name = "Value"></typeparam>
    /// <param name = "files1"></param>
    /// <param name = "header"></param>
    public void List<Header, Value>(IList<Value> files1, Header header)
        where Header : IEnumerable<char>
    {
        List(files1, header, new TextOutputGeneratorArgs { headerWrappedEmptyLines = true, insertCount = false });
    }

    public void List(IList<string> files1, string header)
    {
        List(files1, header, new TextOutputGeneratorArgs { headerWrappedEmptyLines = true, insertCount = false });
    }

    public void ListString(string list, string header)
    {
        Header(header);
        AppendLine(list);
        stringBuilder.AppendLine();
    }

    /// <summary>
    ///     Use DictionaryHelper.CategoryParser
    /// </summary>
    /// <typeparam name = "Header"></typeparam>
    /// <typeparam name = "Value"></typeparam>
    /// <param name = "files1"></param>
    /// <param name = "header"></param>
    /// <param name = "a"></param>
    public void List<Header, Value>(IList<Value> files1, Header header, TextOutputGeneratorArgs a)
        where Header : IEnumerable<char>
    {
        if (a.insertCount)
        {
        //throw new Exception("later");
        //header = (Header)((IList<char>)CA.JoinIList<char>(header, " (" + files1.Count() + ")"));
        }

        if (a.headerWrappedEmptyLines)
            stringBuilder.AppendLine();
        stringBuilder.AppendLine(header + ":");
        if (a.headerWrappedEmptyLines)
            stringBuilder.AppendLine();
        List(files1, a.delimiter, a.whenNoEntries);
    }

    public void Paragraph(StringBuilder wrongNumberOfParts, string header)
    {
        var text = wrongNumberOfParts.ToString().Trim();
        Paragraph(text, header);
    }

    /// <summary>
    ///     For ordinary text use Append*
    /// </summary>
    /// <param name = "text"></param>
    /// <param name = "header"></param>
    public void Paragraph(string text, string header)
    {
        if (text != string.Empty)
        {
            stringBuilder.AppendLine(header + ":");
            stringBuilder.AppendLine(text);
            stringBuilder.AppendLine();
        }
    }

    public void Dictionary(Dictionary<string, int> charEntity, string delimiter)
    {
        foreach (var item in charEntity)
            stringBuilder.AppendLine(item.Key + delimiter + item.Value);
    }

    public void DictionaryKeyValuePair<T1, T2>(string header, IOrderedEnumerable<KeyValuePair<T1, T2>> ordered)
    {
        Header(header);
        foreach (var item in ordered)
            stringBuilder.AppendLine(item.Key + " " + item.Value);
    }

    public void IGrouping(IEnumerable<IGrouping<string, string>> g)
    {
        var dictionary = IGroupingToDictionary(g);
        Dictionary(dictionary);
    }

    private Dictionary<string, List<string>> IGroupingToDictionary(IEnumerable<IGrouping<string, string>> g)
    {
        var list = new Dictionary<string, List<string>>();
        foreach (var item in g)
            list.Add(item.Key, item.ToList());
        return list;
    }
}