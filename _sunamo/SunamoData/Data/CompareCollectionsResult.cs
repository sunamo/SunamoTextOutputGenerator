namespace SunamoTextOutputGenerator;


public class CompareCollectionsResult<T>
{
    internal List<T> OnlyInFirst;
    internal List<T> OnlyInSecond;
    internal List<T> Both;
}