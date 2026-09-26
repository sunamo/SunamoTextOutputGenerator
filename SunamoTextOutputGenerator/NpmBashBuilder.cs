namespace SunamoTextOutputGenerator;

/// <summary>
/// Builder for generating npm bash commands.
/// </summary>
public class NpmBashBuilder : INpmBashBuilder
{
    /// <summary>
    /// Gets or sets the underlying text builder.
    /// </summary>
    public TextBuilder Builder { get; set; }

    /// <summary>
    /// Initializes a new instance of the <see cref="NpmBashBuilder"/> class.
    /// </summary>
    public NpmBashBuilder()
    {
        Builder = new TextBuilder();
        Builder.PrependEveryNoWhite = "";
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="NpmBashBuilder"/> class with an existing text builder.
    /// </summary>
    /// <param name="textBuilder">The text builder to use.</param>
    public NpmBashBuilder(TextBuilder textBuilder)
    {
        Builder = textBuilder;
    }

    /// <summary>
    /// Runs npm install with optional arguments.
    /// </summary>
    /// <param name="arguments">Optional npm install arguments.</param>
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
