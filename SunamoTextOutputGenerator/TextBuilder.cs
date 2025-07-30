namespace SunamoTextOutputGenerator;

/// <summary>
///     In Comparing
/// </summary>
public class TextBuilder : ITextBuilder
{
    private static Type type = typeof(TextBuilder);
    private bool _canUndo;
    private int _lastIndex = -1;
    private string _lastText = "";
    private readonly bool _useList;
    public StringBuilder sb;

    /// <summary>
    ///     Když někde nastavím na true, musím i zdůvodnit proč
    ///     protože mi potom nefunguje sb.sb
    ///     jako teď když jsem připojoval git do ps
    ///     git počítal s sb ale ps s lines
    /// </summary>
    /// <param name="useList"></param>
    public TextBuilder(bool useList = false)
    {
        _useList = useList;
        if (useList)
            list = new List<string>();
        else
            sb = new StringBuilder();
    }

    public string prependEveryNoWhite { get; set; } = string.Empty;

    /// <summary>
    ///     For PowershellRunner
    /// </summary>
    public List<string> list { get; set; }

    public void Clear()
    {
        if (_useList)
            list.Clear();
        else
            sb.Clear();
    }

    public bool CanUndo
    {
        get
        {
            if (_useList) return false;
            return _canUndo;
        }
        set
        {
            _canUndo = value;
            if (!value)
            {
                _lastIndex = -1;
                _lastText = "";
            }
        }
    }

    public void Undo()
    {
        if (_useList) UndoIsNotAllowed("Undo");
        if (_lastIndex != -1) sb.Remove(_lastIndex, _lastText.Length);
    }

    public void Append(string s)
    {
        if (_useList)
        {
            if (list.Count > 0)
                list[list.Count - 1] += s;
            else
                list.Add(s);
        }
        else
        {
            SetUndo(s);
            sb.Append(prependEveryNoWhite);
            sb.Append(s);
        }
    }

    public void Append(object s)
    {
        var text = s.ToString();
        SetUndo(text);
        Append(text);
    }

    public void AppendLine()
    {
        Append(Environment.NewLine);
    }

    public void AppendLine(string s)
    {
        if (_useList)
        {
            list.Add(prependEveryNoWhite + s);
        }
        else
        {
            SetUndo(s);
            sb.Append(prependEveryNoWhite + s + Environment.NewLine);
        }
    }

    /// <summary>
    ///     If is use List, join it with NL.
    ///     Otherwise return sb
    /// </summary>
    public override string ToString()
    {
        if (_useList)
            return string.Join(Environment.NewLine, list);
        return sb.ToString();
    }

    public static ITextBuilder Create(bool useList = false)
    {
        return new TextBuilder(useList);
    }

    private void UndoIsNotAllowed(string what)
    {
        ThrowEx.IsNotAllowed(what);
    }

    private void SetUndo(string text)
    {
        if (_useList) UndoIsNotAllowed("SetUndo");
        if (CanUndo)
        {
            _lastIndex = sb.Length;
            _lastText = text;
        }
    }
}