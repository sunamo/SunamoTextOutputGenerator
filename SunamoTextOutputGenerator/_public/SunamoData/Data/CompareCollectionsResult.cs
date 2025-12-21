namespace SunamoTextOutputGenerator._public.SunamoData.Data;

public class CompareCollectionsResult<T>
{
    public List<T> Both;
    public List<T> OnlyInFirst;
    public List<T> OnlyInSecond;
}