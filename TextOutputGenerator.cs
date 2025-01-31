namespace SunamoTextOutputGenerator;

/// <summary>
///     In Comparing
/// </summary>
public class TextOutputGenerator //: ITextOutputGenerator
{
    private static readonly string s_znakNadpisu = "*";

    // při převádění na nugety jsem to změnil na ITextBuilder sb = TextBuilder.Create();
    // ale asi to byla blbost, teď mám v _sunamo Create() která je ale null místo abych použil ctor
    // takže vracím nazpět.
    //public TextBuilder sb = new TextBuilder();
    public StringBuilder sb = new();
    //public string prependEveryNoWhite
    //{
    //    get => sb.prependEveryNoWhite;
    //    set => sb.prependEveryNoWhite = value;
    //}


    public static TextOutputGenerator Create()
    {
        return new TextOutputGenerator();
    }

    public override string ToString()
    {
        var ts = sb.ToString();
        return ts;
    }

    public void Undo()
    {
        ThrowEx.NotImplementedMethod();
        //sb.Undo();
    }

    #region Static texts

    public void EndRunTime()
    {
        sb.AppendLine("AppWillBeTerminated");
    }

    /// <summary>
    ///     Pouze vypíše "Az budete mit vstupní data, spusťte program znovu."
    /// </summary>
    public void NoData()
    {
        sb.AppendLine("NoData");
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
        sb.AppendLine(hvezdicky);
        sb.AppendLine(text);
        sb.AppendLine(hvezdicky);
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
        sb.AppendLine(text.ToString());
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void Append(string text)
    {
        sb.Append(text);
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public void AppendLine(string text)
    {
        sb.AppendLine(text);
    }

    public void AppendLineFormat(string text, params string[] p)
    {
        sb.AppendLine();
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
        sb.AppendLine();
        AppendLine(v);
        sb.AppendLine();
    }

    public void SingleCharLine(char paddingChar, int v)
    {
        sb.AppendLine(string.Empty.PadLeft(v, paddingChar));
    }

    #endregion

    #region List

    public void ListObject(IList files1)
    {
        var l = new List<string>();
        foreach (var item in files1) l.Add(item.ToString());
        List(l);
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
            sb.AppendLine(whenNoEntries);
        else
            foreach (var item in files1)
                Append(item + deli);
        //sb.AppendLine();
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
        sb.AppendLine();
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

        if (a.headerWrappedEmptyLines) sb.AppendLine();
        sb.AppendLine(header + ":");
        if (a.headerWrappedEmptyLines) sb.AppendLine();
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
            sb.AppendLine(header + ":");
            sb.AppendLine(text);
            sb.AppendLine();
        }
    }

    #endregion

    #region Dictionary

    public void Dictionary(Dictionary<string, int> charEntity, string delimiter)
    {
        foreach (var item in charEntity) sb.AppendLine(item.Key + delimiter + item.Value);
    }

    public void DictionaryKeyValuePair<T1, T2>(string header, IOrderedEnumerable<KeyValuePair<T1, T2>> ordered)
    {
        Header(header);
        foreach (var item in ordered) sb.AppendLine(item.Key + " " + item.Value);
    }

    public void IGrouping(IEnumerable<IGrouping<string, string>> g)
    {
        var d = IGroupingToDictionary(g);
        Dictionary(d);
    }

    private Dictionary<string, List<string>> IGroupingToDictionary(IEnumerable<IGrouping<string, string>> g)
    {
        var l = new Dictionary<string, List<string>>();
        foreach (var item in g) l.Add(item.Key, item.ToList());
        return l;
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
            var d = new List<string>(ls.Count);
            foreach (var item in ls) d.Add(item.Key + " " + item.Value.Count());
            List(d);
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
        foreach (var item in v) sb.AppendLine(string.Join("|", item.Key, item.Value));
    }

    /// <summary>
    ///     vše na 1 řádku, oddělí |
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="d"></param>
    /// <param name="deli"></param>
    public void Dictionary<T1, T2>(Dictionary<T1, T2> d, string deli = "|")
    {
        //StringBuilder sb = new StringBuilder();
        foreach (var item in d)
            if (deli != "|")
            {
                Header(item.Key.ToString());
                // vrací mi to na jednom řádku jak key tak všechny value oddělené |.
                sb.AppendLine(string.Join(deli, item.Value.ToString()));
                sb.AppendLine();
            }
            else
            {
                // vrací mi to na jednom řádku jak key tak všechny value oddělené |.
                sb.AppendLine(string.Join(deli, item.Key.ToString(),
                    item.Value.ToString())); // SF.PrepareToSerializationExplicitString(new List<string>(), deli));
            }
    }

    public void PairBullet(string key, string v)
    {
        sb.AppendLine(key + ": " + v);
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

            sb.AppendLine(first + delimiter + second);
        }

        return sb.ToString();
    }

    #endregion
}