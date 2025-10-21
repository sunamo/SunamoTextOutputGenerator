// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy

namespace SunamoTextOutputGenerator;

/// <summary>
///     In Comparing
/// </summary>
public class TextOutputGenerator //: ITextOutputGenerator
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

    #region Static texts

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

    #endregion

    #region Templates

    /// <summary>
    ///     Napíše nadpis A1 do konzole
    /// </summary>
    /// <param name="text"></param>
    public void StartRunTime(string text)
    {
        var delkaTextu = text.Length;
        var hvezdicky = "";
        hvezdicky = new string(s_znakNadpisu[0], delkaTextu);
        //hvezdicky.PadLeft(delkaTextu, znakNadpisu[0]);
        stringBuilder.AppendLine(hvezdicky);
        stringBuilder.AppendLine(text);
        stringBuilder.AppendLine(hvezdicky);
    }

    public void CountEvery<T>(IList<KeyValuePair<T, int>> eq)
    {
        foreach (var item in eq) AppendLine(item.Key + "," + item.Value + "x");
    }

    #endregion

    #region AppendLine

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

    #endregion

    #region Other adding methods

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

    #endregion

    #region List

    public void ListObject(IList files1)
    {
        var list = new List<string>();
        foreach (var item in files1) list.Add(item.ToString());
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
    /// <param name="files1"></param>
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
    /// <typeparam name="Header"></typeparam>
    /// <typeparam name="Value"></typeparam>
    /// <param name="files1"></param>
    /// <param name="header"></param>
    public void List<Header, Value>(IList<Value> files1, Header header) where Header : IEnumerable<char>
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
    /// <typeparam name="Header"></typeparam>
    /// <typeparam name="Value"></typeparam>
    /// <param name="files1"></param>
    /// <param name="header"></param>
    /// <param name="a"></param>
    public void List<Header, Value>(IList<Value> files1, Header header, TextOutputGeneratorArgs a)
        where Header : IEnumerable<char>
    {
        if (a.insertCount)
        {
            //throw new Exception("later");
            //header = (Header)((IList<char>)CA.JoinIList<char>(header, " (" + files1.Count() + ")"));
        }

        if (a.headerWrappedEmptyLines) stringBuilder.AppendLine();
        stringBuilder.AppendLine(header + ":");
        if (a.headerWrappedEmptyLines) stringBuilder.AppendLine();
        List(files1, a.delimiter, a.whenNoEntries);
    }

    #endregion

    #region Paragraph

    public void Paragraph(StringBuilder wrongNumberOfParts, string header)
    {
        var text = wrongNumberOfParts.ToString().Trim();
        Paragraph(text, header);
    }

    /// <summary>
    ///     For ordinary text use Append*
    /// </summary>
    /// <param name="text"></param>
    /// <param name="header"></param>
    public void Paragraph(string text, string header)
    {
        if (text != string.Empty)
        {
            stringBuilder.AppendLine(header + ":");
            stringBuilder.AppendLine(text);
            stringBuilder.AppendLine();
        }
    }

    #endregion

    #region Dictionary

    public void Dictionary(Dictionary<string, int> charEntity, string delimiter)
    {
        foreach (var item in charEntity) stringBuilder.AppendLine(item.Key + delimiter + item.Value);
    }

    public void DictionaryKeyValuePair<T1, T2>(string header, IOrderedEnumerable<KeyValuePair<T1, T2>> ordered)
    {
        Header(header);
        foreach (var item in ordered) stringBuilder.AppendLine(item.Key + " " + item.Value);
    }

    public void IGrouping(IEnumerable<IGrouping<string, string>> g)
    {
        var dictionary = IGroupingToDictionary(g);
        Dictionary(dictionary);
    }

    private Dictionary<string, List<string>> IGroupingToDictionary(IEnumerable<IGrouping<string, string>> g)
    {
        var list = new Dictionary<string, List<string>>();
        foreach (var item in g) list.Add(item.Key, item.ToList());
        return list;
    }

    public void Dictionary(Dictionary<string, List<string>> ls)
    {
        foreach (var item in ls) List(item.Value, item.Key);
    }

    public void Dictionary<Header, Value>(Dictionary<Header, List<Value>> ls, bool onlyCountInValue = false)
        where Header : IEnumerable<char>
    {
        if (onlyCountInValue)
        {
            var dictionary = new List<string>(ls.Count);
            foreach (var item in ls) dictionary.Add(item.Key + " " + item.Value.Count());
            List(dictionary);
        }
        else
        {
            foreach (var item in ls) List(item.Value, item.Key);
        }
    }

    /// <summary>
    ///     vše na 1 řádku, oddělí |
    /// </summary>
    /// <param name="v"></param>
    public void Dictionary(Dictionary<string, string> v)
    {
        foreach (var item in v) stringBuilder.AppendLine(string.Join("|", item.Key, item.Value));
    }

    /// <summary>
    ///     vše na 1 řádku, oddělí |
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="d"></param>
    /// <param name="deli"></param>
    public void Dictionary<T1, T2>(Dictionary<T1, T2> dictionary, string deli = "|")
    {
        //StringBuilder stringBuilder = new StringBuilder();
        foreach (var item in dictionary)
            if (deli != "|")
            {
                Header(item.Key.ToString());
                // vrací mi to na jednom řádku jak key tak všechny value oddělené |.
                stringBuilder.AppendLine(string.Join(deli, item.Value.ToString()));
                stringBuilder.AppendLine();
            }
            else
            {
                // vrací mi to na jednom řádku jak key tak všechny value oddělené |.
                stringBuilder.AppendLine(string.Join(deli, item.Key.ToString(),
                    item.Value.ToString())); // SF.PrepareToSerializationExplicitString(new List<string>(), deli));
            }
    }

    public void PairBullet(string key, string v)
    {
        stringBuilder.AppendLine(key + ": " + v);
    }

    public string DictionaryBothToStringToSingleLine<Key, Value>(Dictionary<Key, Value> sorted, bool putValueAsFirst,
        string delimiter = " ")
    {
        foreach (var item in sorted)
        {
            string first, second = null;
            if (putValueAsFirst)
            {
                first = item.Value.ToString();
                second = item.Key.ToString();
            }
            else
            {
                first = item.Key.ToString();
                second = item.Value.ToString();
            }

            stringBuilder.AppendLine(first + delimiter + second);
        }

        return stringBuilder.ToString();
    }

    #endregion
}