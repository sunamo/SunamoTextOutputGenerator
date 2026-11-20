namespace SunamoTextOutputGenerator;

/// <summary>
/// Represents the text output generator args type.
/// </summary>
public class TextOutputGeneratorArgs
{
    /// <summary>
    /// Gets or sets the delimiter.
    /// </summary>
    public string Delimiter { get; set; } = Environment.NewLine;

    /// <summary>
    /// Gets or sets the is header wrapped with empty lines.
    /// </summary>
    public bool IsHeaderWrappedWithEmptyLines { get; set; } = true;

    /// <summary>
    /// Gets or sets the is inserting count.
    /// </summary>
    public bool IsInsertingCount { get; set; }

    /// <summary>
    /// Gets or sets the when no entries.
    /// </summary>
    public string WhenNoEntries { get; set; } = "No entries";

    /// <summary>
    /// Initializes a new instance of the text output generator args class.
    /// </summary>
    public TextOutputGeneratorArgs()
    {
    }

    /// <summary>
    /// Text output generator args.
    /// </summary>
    /// <returns>The text output generator args result.</returns>
    public TextOutputGeneratorArgs(bool isHeaderWrappedWithEmptyLines, bool isInsertingCount)
    {
        IsHeaderWrappedWithEmptyLines = isHeaderWrappedWithEmptyLines;
        IsInsertingCount = isInsertingCount;
    }
}
