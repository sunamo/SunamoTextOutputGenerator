namespace SunamoTextOutputGenerator._public.SunamoData.Data;

/// <summary>
/// Represents the compare collections result t type.
/// </summary>
public class CompareCollectionsResult<T>
{
    /// <summary>
    /// Gets or sets the both.
    /// </summary>
    public List<T>? Both { get; set; }

    /// <summary>
    /// Gets or sets the only in first.
    /// </summary>
    public List<T>? OnlyInFirst { get; set; }

    /// <summary>
    /// Gets or sets the only in second.
    /// </summary>
    public List<T>? OnlyInSecond { get; set; }
}
