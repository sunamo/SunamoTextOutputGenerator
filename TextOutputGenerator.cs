namespace SunamoTextOutputGenerator;

/// <summary>
/// In Comparing
/// </summary>
public class TextOutputGenerator : ITextOutputGenerator
{
    private readonly static string s_znakNadpisu = AllStringsSE.asterisk;
    public ITextBuilder sb = TextBuilder.Create();
    public string prependEveryNoWhite
    {
        get => sb.prependEveryNoWhite;
        set => sb.prependEveryNoWhite = value;
    }

    public static TextOutputGenerator Create()
    {
        return new TextOutputGenerator();
    }

    #region Static texts
    public void EndRunTime()
    {
        sb.AppendLine(Messages.AppWillBeTerminated);
    }

    /// <summary>
    /// Pouze vypíše "Az budete mit vstupní data, spusťte program znovu."
    /// </summary>
    public void NoData()
    {
        sb.AppendLine(Messages.NoData);
    }


    #endregion

    #region Templates

    /// <summary>
    /// Napíše nadpis A1 do konzole
    /// </summary>
    /// <param name="text"></param>
    public void StartRunTime(string text)
    {
        int delkaTextu = text.Length;
        string hvezdicky = "";
        hvezdicky = new string(s_znakNadpisu[0], delkaTextu);
        //hvezdicky.PadLeft(delkaTextu, znakNadpisu[0]);
        sb.AppendLine(hvezdicky);
        sb.AppendLine(text);
        sb.AppendLine(hvezdicky);
    }

    public void CountEvery<T>(IList<KeyValuePair<T, int>> eq)
    {
        foreach (var item in eq)
        {
            AppendLine(item.Key + AllStringsSE.cs + item.Value + "x");
        }
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

        AppendLine(SH.Format2(text, p));
    }

    public void AppendFormat(string text, params string[] p)
    {
        AppendLine(SH.Format2(text, p));
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

    public override string ToString()
    {
        var ts = sb.ToString();
        return ts;
    }

    #region List
    public void ListObject(IList files1)
    {
        List(CA.ToListString(files1));
    }

    public void ListSB(StringBuilder onlyStart, string v)
    {
        Header(v);
        AppendLine(onlyStart);

    }

    /// <summary>
    /// If you have StringBuilder, use Paragraph()
    /// </summary>
    /// <param name="files1"></param>
    public void List(IList<string> files1)
    {
        List<string>(files1);
    }

    public void List<Value>(IList<Value> files1, string deli = Consts.nl2, string whenNoEntries = Consts.stringEmpty)
    {
        if (files1.Count() == 0)
        {
            sb.AppendLine(whenNoEntries);
        }
        else
        {
            foreach (var item in files1)
            {
                Append(item.ToString() + deli);
            }
            //sb.AppendLine();
        }
    }

    /// <summary>
    ///  must be where Header : IEnumerable<char> (like is string)
    /// </summary>
    /// <typeparam name="Header"></typeparam>
    /// <typeparam name="Value"></typeparam>
    /// <param name="files1"></param>
    /// <param name="header"></param>
    public void List<Header, Value>(IList<Value> files1, Header header) where Header : IEnumerable<char>
    {
        List<Header, Value>(files1, header, new TextOutputGeneratorArgs { headerWrappedEmptyLines = true, insertCount = false });
    }

    public void List(IList<string> files1, string header)
    {
        List<string, string>(files1, header, new TextOutputGeneratorArgs { headerWrappedEmptyLines = true, insertCount = false });
    }

    public void ListString(string list, string header)
    {
        Header(header);
        AppendLine(list);
        sb.AppendLine();
    }

    /// <summary>
    /// Use DictionaryHelper.CategoryParser
    /// </summary>
    /// <typeparam name="Header"></typeparam>
    /// <typeparam name="Value"></typeparam>
    /// <param name="files1"></param>
    /// <param name="header"></param>
    /// <param name="a"></param>
    public void List<Header, Value>(IList<Value> files1, Header header, TextOutputGeneratorArgs a) where Header : IEnumerable<char>
    {
        if (a.insertCount)
        {
            //ThrowEx.Custom("later");
            //header = (Header)((IList<char>)CA.JoinIList<char>(header, " (" + files1.Count() + AllStrings.rb));
        }
        if (a.headerWrappedEmptyLines)
        {
            sb.AppendLine();
        }
        sb.AppendLine(header + AllStringsSE.colon);
        if (a.headerWrappedEmptyLines)
        {
            sb.AppendLine();
        }
        List(files1, a.delimiter, a.whenNoEntries);
    }
    #endregion

    #region Paragraph
    public void Paragraph(StringBuilder wrongNumberOfParts, string header)
    {
        string text = wrongNumberOfParts.ToString().Trim();
        Paragraph(text, header);
    }

    /// <summary>
    /// For ordinary text use Append*
    /// </summary>
    /// <param name="text"></param>
    /// <param name="header"></param>
    public void Paragraph(string text, string header)
    {
        if (text != string.Empty)
        {
            sb.AppendLine(header + AllStringsSE.colon);
            sb.AppendLine(text);
            sb.AppendLine();
        }
    }
    #endregion

    public void Undo()
    {
        sb.Undo();
    }

    #region Dictionary
    public void Dictionary(Dictionary<string, int> charEntity, string delimiter)
    {
        foreach (var item in charEntity)
        {
            sb.AppendLine(item.Key + delimiter + item.Value);
        }
    }

    public void DictionaryKeyValuePair<T1, T2>(string header, IOrderedEnumerable<KeyValuePair<T1, T2>> ordered)
    {
        Header(header);

        foreach (var item in ordered)
        {
            sb.AppendLine(item.Key + AllStringsSE.space + item.Value);
        }

    }

    public void Dictionary(Dictionary<string, List<string>> ls)
    {
        foreach (var item in ls)
        {
            List(item.Value, item.Key);
        }
    }

    public void Dictionary<Header, Value>(Dictionary<Header, List<Value>> ls, bool onlyCountInValue = false) where Header : IEnumerable<char>
    {
        if (onlyCountInValue)
        {
            List<string> d = new List<string>(ls.Count);
            foreach (var item in ls)
            {
                d.Add(item.Key + AllStringsSE.space + item.Value.Count());
            }
            List(d);
        }
        else
        {
            foreach (var item in ls)
            {
                List<Header, Value>(item.Value, item.Key);
            }
        }
    }

    /// <summary>
    /// vše na 1 řádku, oddělí |
    /// </summary>
    /// <param name="v"></param>
    public void Dictionary(Dictionary<string, string> v)
    {
        foreach (var item in v)
        {
            sb.AppendLine(SF.PrepareToSerialization(item.Key, item.Value));
        }
    }

    /// <summary>
    /// vše na 1 řádku, oddělí |
    /// </summary>
    /// <typeparam name="T1"></typeparam>
    /// <typeparam name="T2"></typeparam>
    /// <param name="d"></param>
    /// <param name="deli"></param>
    public void Dictionary<T1, T2>(Dictionary<T1, T2> d, string deli = AllStringsSE.verbar)
    {
        //StringBuilder sb = new StringBuilder();
        foreach (var item in d)
        {
            if (deli != AllStringsSE.verbar)
            {
                Header(item.Key.ToString());

                // vrací mi to na jednom řádku jak key tak všechny value oddělené |.
                sb.AppendLine(SF.PrepareToSerializationExplicitString(new List<string>(new string[] { item.Value.ToString() }), deli));

                sb.AppendLine();
            }
            else
            {
                // vrací mi to na jednom řádku jak key tak všechny value oddělené |.
                sb.AppendLine(SF.PrepareToSerializationExplicitString(new List<string>(new string[] { item.Key.ToString(), item.Value.ToString() }), deli));
            }

        }
    }

    public void PairBullet(string key, string v)
    {
        sb.AppendLine(key + ": " + v);
    }

    public string DictionaryBothToStringToSingleLine<Key, Value>(Dictionary<Key, Value> sorted, bool putValueAsFirst, string delimiter = AllStringsSE.space)
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
