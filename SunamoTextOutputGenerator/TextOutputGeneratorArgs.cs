namespace SunamoTextOutputGenerator;

public class TextOutputGeneratorArgs
{
    public string Delimiter { get; set; } = Environment.NewLine;

    public bool IsHeaderWrappedWithEmptyLines { get; set; } = true;

    public bool IsInsertingCount { get; set; }

    public string WhenNoEntries { get; set; } = "No entries";

    public TextOutputGeneratorArgs()
    {
    }

    public TextOutputGeneratorArgs(bool isHeaderWrappedWithEmptyLines, bool isInsertingCount)
    {
        IsHeaderWrappedWithEmptyLines = isHeaderWrappedWithEmptyLines;
        IsInsertingCount = isInsertingCount;
    }
}
