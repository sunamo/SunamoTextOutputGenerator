namespace SunamoTextOutputGenerator._public.SunamoData.Data;

public class CompareCollectionsResult<T>
{
    public List<T>? Both { get; set; }

    public List<T>? OnlyInFirst { get; set; }

    public List<T>? OnlyInSecond { get; set; }
}
