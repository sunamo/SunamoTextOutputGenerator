namespace SunamoTextOutputGenerator;

public class TextBuilder : ITextBuilder
{
    private bool canUndo;
    private int lastIndex = -1;
    private string lastText = "";
    private readonly bool isUsingList;

    public StringBuilder Builder { get; set; } = new();

    public TextBuilder(bool isUsingList = false)
    {
        this.isUsingList = isUsingList;
        if (isUsingList)
            List = new List<string>();
    }

    public string PrependEveryNoWhite { get; set; } = string.Empty;

    public List<string>? List { get; set; }

    public void Clear()
    {
        if (isUsingList)
            List!.Clear();
        else
            Builder.Clear();
    }

    public bool CanUndo
    {
        get
        {
            if (isUsingList) return false;
            return canUndo;
        }
        set
        {
            canUndo = value;
            if (!value)
            {
                lastIndex = -1;
                lastText = "";
            }
        }
    }

    public void Undo()
    {
        if (isUsingList) UndoIsNotAllowed("Undo");
        if (lastIndex != -1) Builder.Remove(lastIndex, lastText.Length);
    }

    public void Append(string text)
    {
        if (isUsingList)
        {
            if (List!.Count > 0)
                List[List.Count - 1] += text;
            else
                List.Add(text);
        }
        else
        {
            SetUndo(text);
            Builder.Append(PrependEveryNoWhite);
            Builder.Append(text);
        }
    }

    public void Append(object value)
    {
        var text = value.ToString() ?? string.Empty;
        SetUndo(text);
        Append(text);
    }

    public void AppendLine()
    {
        Append(Environment.NewLine);
    }

    public void AppendLine(string text)
    {
        if (isUsingList)
        {
            List!.Add(PrependEveryNoWhite + text);
        }
        else
        {
            SetUndo(text);
            Builder.Append(PrependEveryNoWhite + text + Environment.NewLine);
        }
    }

    public override string ToString()
    {
        if (isUsingList)
            return string.Join(Environment.NewLine, List!);
        return Builder.ToString();
    }

    public static ITextBuilder Create(bool isUsingList = false)
    {
        return new TextBuilder(isUsingList);
    }

    private void UndoIsNotAllowed(string operationName)
    {
        ThrowEx.IsNotAllowed(operationName);
    }

    private void SetUndo(string text)
    {
        if (isUsingList) UndoIsNotAllowed("SetUndo");
        if (CanUndo)
        {
            lastIndex = Builder.Length;
            lastText = text;
        }
    }
}
