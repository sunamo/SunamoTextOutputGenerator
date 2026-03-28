namespace SunamoTextOutputGenerator;

/// <summary>
/// Arguments for controlling TextOutputGenerator list formatting behavior.
/// </summary>
public class TextOutputGeneratorArgs
{
    /// <summary>
    /// Gets or sets the delimiter between list entries.
    /// </summary>
    public string Delimiter { get; set; } = Environment.NewLine;

    /// <summary>
    /// Gets or sets a value indicating whether the header should be wrapped with empty lines.
    /// </summary>
    public bool IsHeaderWrappedWithEmptyLines { get; set; } = true;

    /// <summary>
    /// Gets or sets a value indicating whether to insert the count in the header.
    /// </summary>
    public bool IsInsertingCount { get; set; }

    /// <summary>
    /// Gets or sets the text to display when there are no entries.
    /// </summary>
    public string WhenNoEntries { get; set; } = "No entries";

    /// <summary>
    /// Initializes a new instance of the <see cref="TextOutputGeneratorArgs"/> class.
    /// </summary>
    public TextOutputGeneratorArgs()
    {
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="TextOutputGeneratorArgs"/> class with specified options.
    /// </summary>
    /// <param name="isHeaderWrappedWithEmptyLines">Whether the header should be wrapped with empty lines.</param>
    /// <param name="isInsertingCount">Whether to insert the count in the header.</param>
    public TextOutputGeneratorArgs(bool isHeaderWrappedWithEmptyLines, bool isInsertingCount)
    {
        IsHeaderWrappedWithEmptyLines = isHeaderWrappedWithEmptyLines;
        IsInsertingCount = isInsertingCount;
    }
}
