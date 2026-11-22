namespace SunamoTextOutputGenerator;

/// <summary>
/// Represents the text builder type.
/// </summary>
public class TextBuilder : ITextBuilder
{
    private bool canUndo;
    private int lastIndex = -1;
    private string lastText = "";
    private readonly bool isUsingList;

    /// <summary>
    /// Gets or sets the builder.
    /// </summary>
    public StringBuilder Builder { get; set; } = new();

    /// <summary>
    /// Text builder.
    /// </summary>
    /// <returns>The text builder result.</returns>
    public TextBuilder(bool isUsingList = false)
    {
        this.isUsingList = isUsingList;
        if (isUsingList)
            List = new List<string>();
    }

    /// <summary>
    /// Gets or sets the prepend every no white.
    /// </summary>
    public string PrependEveryNoWhite { get; set; } = string.Empty;

    /// <summary>
    /// Gets or sets the list.
    /// </summary>
    public List<string>? List { get; set; }

    /// <summary>
    /// Clear.
    /// </summary>
    /// <returns>The clear result.</returns>
    public void Clear()
    {
        if (isUsingList)
            List!.Clear();
        else
            Builder.Clear();
    }

    /// <summary>
    /// The can undo.
    /// </summary>
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

    /// <summary>
    /// Undo.
    /// </summary>
    /// <param name="!value">The value.</param>
    /// <returns>The undo result.</returns>
    public void Undo()
    {
        if (isUsingList) UndoIsNotAllowed("Undo");
        if (lastIndex != -1) Builder.Remove(lastIndex, lastText.Length);
    }

    /// <summary>
    /// Append.
    /// </summary>
    /// <returns>The append result.</returns>
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

    /// <summary>
    /// Append.
    /// </summary>
    /// <param name="text">The text.</param>
    /// <returns>The append result.</returns>
    public void Append(object value)
    {
        var text = value.ToString() ?? string.Empty;
        SetUndo(text);
        Append(text);
    }

    /// <summary>
    /// Initializes a new instance of the append line class.
    /// </summary>
    /// <param name="PrependEveryNoWhite">The prepend every no white.</param>
    public void AppendLine()
    {
        Append(Environment.NewLine);
    }

    /// <summary>
    /// Append line.
    /// </summary>
    /// <param name="value">The value.</param>
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

    /// <summary>
    /// To string.
    /// </summary>
    /// <param name="isUsingList">The is using list.</param>
    /// <returns>The to string result.</returns>
    public override string ToString()
    {
        if (isUsingList)
            return string.Join(Environment.NewLine, List!);
        return Builder.ToString();
    }

    /// <summary>
    /// Initializes a new instance of the create class.
    /// </summary>
    /// <param name="text">The text.</param>
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
