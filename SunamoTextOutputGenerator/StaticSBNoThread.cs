namespace SunamoTextOutputGenerator;

/// <summary>
/// Represents the static SB no thread type.
/// </summary>
public class StaticSBNoThread
{
    /// <summary>
    /// Gets the builder.
    /// </summary>
    public static StringBuilder Builder { get; } = new();

    /// <summary>
    /// Clear.
    /// </summary>
    /// <returns>The clear result.</returns>
    public static void Clear()
    {
        Builder.Clear();
    }

    /// <summary>
    /// Initializes a new instance of the append class.
    /// </summary>
    public static void Append(string text)
    {
        Builder.Append(text);
    }

    /// <summary>
    /// Initializes a new instance of the to string class.
    /// </summary>
    /// <param name="text">The text.</param>
    public new static string ToString()
    {
        return Builder.ToString();
    }
}
