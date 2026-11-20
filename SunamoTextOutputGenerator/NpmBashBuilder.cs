namespace SunamoTextOutputGenerator;

/// <summary>
/// Represents the npm bash builder type.
/// </summary>
public class NpmBashBuilder : INpmBashBuilder
{
    /// <summary>
    /// Gets or sets the builder.
    /// </summary>
    public TextBuilder Builder { get; set; }

    /// <summary>
    /// Npm bash builder.
    /// </summary>
    /// <returns>The npm bash builder result.</returns>
    public NpmBashBuilder()
    {
        Builder = new TextBuilder();
        Builder.PrependEveryNoWhite = "";
    }

    /// <summary>
    /// Npm bash builder.
    /// </summary>
    /// <param name="textBuilder">The text builder.</param>
    /// <returns>The npm bash builder result.</returns>
    public NpmBashBuilder(TextBuilder textBuilder)
    {
        Builder = textBuilder;
    }

    /// <summary>
    /// Install.
    /// </summary>
    /// <param name="arguments">The arguments.</param>
    /// <returns>The install result.</returns>
    public void Install(string? arguments = null)
    {
        Npm($"i {arguments}");
    }

    private void Npm(string remainingCommand)
    {
        Builder.Append($"npm {remainingCommand}");
        Builder.AppendLine();
    }
}
