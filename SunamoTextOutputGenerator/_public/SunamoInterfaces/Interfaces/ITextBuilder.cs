namespace SunamoTextOutputGenerator._public.SunamoInterfaces.Interfaces;

/// <summary>
/// Represents the i text builder type.
/// </summary>
public interface ITextBuilder
{
    /// <summary>
    /// Gets or sets the can undo.
    /// </summary>
    bool CanUndo { get; set; }

    /// <summary>
    /// Gets or sets the list.
    /// </summary>
    List<string>? List { get; set; }

    /// <summary>
    /// Gets or sets the prepend every no white.
    /// </summary>
    string PrependEveryNoWhite { get; set; }

    /// <summary>
    /// Append.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The append result.</returns>
    void Append(object value);

    /// <summary>
    /// Append.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The append result.</returns>
    void Append(string text);

    /// <summary>
    /// Append line.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The append line result.</returns>
    void AppendLine();

    /// <summary>
    /// Append line.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The append line result.</returns>
    void AppendLine(string text);

    /// <summary>
    /// Clear.
    /// </summary>
    /// <param name="value">The value.</param>
    void Clear();

    /// <summary>
    /// To string.
    /// </summary>
    /// <param name="text">The text.</param>
    string ToString();

    /// <summary>
    /// Undo.
    /// </summary>
    void Undo();
}
