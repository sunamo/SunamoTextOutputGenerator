namespace SunamoTextOutputGenerator;

public class StaticSBNoThread
{
    public static StringBuilder stringBuilder = new();

    public static void Clear()
    {
        stringBuilder.Clear();
    }

    public static void Append(string t)
    {
        stringBuilder.Append(t);
    }

    public new static string ToString()
    {
        return stringBuilder.ToString();
    }
}