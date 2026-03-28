namespace SunamoTextOutputGenerator._public.SunamoData.Data;

/// <summary>
/// Stores the result of comparing two collections, categorizing items as in both, only first, or only second.
/// </summary>
/// <typeparam name="T">The type of elements being compared.</typeparam>
public class CompareCollectionsResult<T>
{
    /// <summary>
    /// Gets or sets items present in both collections.
    /// </summary>
    public List<T>? Both { get; set; }

    /// <summary>
    /// Gets or sets items present only in the first collection.
    /// </summary>
    public List<T>? OnlyInFirst { get; set; }

    /// <summary>
    /// Gets or sets items present only in the second collection.
    /// </summary>
    public List<T>? OnlyInSecond { get; set; }
}
