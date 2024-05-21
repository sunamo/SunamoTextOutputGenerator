namespace
#if SunamoCollectionsGeneric
SunamoCollectionsGeneric
#elif SunamoDevCode
SunamoDevCode
#elif SunamoCollections
SunamoCollections
#elif SunamoFileSystem
SunamoFileSystem
#else
SunamoTextOutputGenerator
#endif
;
public class TextOutputGeneratorArgs
{
    public bool headerWrappedEmptyLines = true;
    public bool insertCount = false;
    public string whenNoEntries = "No entries";
    public string delimiter = Environment.NewLine;
    public TextOutputGeneratorArgs()
    {
    }
    public TextOutputGeneratorArgs(bool headerWrappedEmptyLines, bool insertCount)
    {
        this.headerWrappedEmptyLines = headerWrappedEmptyLines;
        this.insertCount = insertCount;
    }
}