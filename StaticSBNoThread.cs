namespace SunamoShared.Helpers.Text;
public class StaticSBNoThread
{
    public static StringBuilder sb = new StringBuilder();

    public static void Clear()
    {
        sb.Clear();
    }

    public static void Append(string t)
    {
        sb.Append(t);
    }

    public new static string ToString()
    {
        return sb.ToString();
    }
}
