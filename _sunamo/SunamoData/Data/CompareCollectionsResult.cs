namespace SunamoTextOutputGenerator;


public class CompareCollectionsResult<T>
{
    public List<T> OnlyInFirst;
    public List<T> OnlyInSecond;
    public List<T> Both;
}