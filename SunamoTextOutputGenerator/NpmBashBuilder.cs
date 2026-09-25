namespace SunamoTextOutputGenerator;

public class NpmBashBuilder : INpmBashBuilder
{
    public TextBuilder Builder { get; set; }

    public NpmBashBuilder()
    {
        Builder = new TextBuilder();
        Builder.PrependEveryNoWhite = "";
    }

    public NpmBashBuilder(TextBuilder textBuilder)
    {
        Builder = textBuilder;
    }

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
