namespace SunamoTextOutputGenerator;

public class StaticSBNoThread
{
    public static StringBuilder Builder { get; } = new();

    public static void Clear()
    {
        Builder.Clear();
    }

    public static void Append(string text)
    {
        Builder.Append(text);
    }

    public new static string ToString()
    {
        return Builder.ToString();
    }
}
