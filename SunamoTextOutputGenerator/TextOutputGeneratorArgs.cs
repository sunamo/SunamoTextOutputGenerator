// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoTextOutputGenerator;

public class TextOutputGeneratorArgs
{
    public string delimiter = Environment.NewLine;
    public bool headerWrappedEmptyLines = true;
    public bool insertCount;
    public string whenNoEntries = "No entries";

    public TextOutputGeneratorArgs()
    {
    }

    public TextOutputGeneratorArgs(bool headerWrappedEmptyLines, bool insertCount)
    {
        this.headerWrappedEmptyLines = headerWrappedEmptyLines;
        this.insertCount = insertCount;
    }
}