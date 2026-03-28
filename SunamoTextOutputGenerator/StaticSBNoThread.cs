namespace SunamoTextOutputGenerator;

/// <summary>
/// Static non-thread-safe StringBuilder wrapper for simple text accumulation.
/// </summary>
public class StaticSBNoThread
{
    /// <summary>
    /// Gets the shared StringBuilder instance.
    /// </summary>
    public static StringBuilder Builder { get; } = new();

    /// <summary>
    /// Clears all accumulated text.
    /// </summary>
    public static void Clear()
    {
        Builder.Clear();
    }

    /// <summary>
    /// Appends text to the builder.
    /// </summary>
    /// <param name="text">The text to append.</param>
    public static void Append(string text)
    {
        Builder.Append(text);
    }

    /// <summary>
    /// Returns all accumulated text as a string.
    /// </summary>
    /// <returns>The accumulated text.</returns>
    public new static string ToString()
    {
        return Builder.ToString();
    }
}
