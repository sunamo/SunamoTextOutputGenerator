// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoTextOutputGenerator;

public class NpmBashBuilder : INpmBashBuilder
{
    public TextBuilder sb;

    public NpmBashBuilder()
    {
        sb = new TextBuilder();
        sb.prependEveryNoWhite = "";
    }

    public NpmBashBuilder(TextBuilder sb)
    {
        this.sb = sb;
        //this.sb.sb = sb.sb;
    }

    public void I(string args = null)
    {
        Npm("i " + args);
    }

    private void Npm(string remainCommand)
    {
        sb.Append("npm " + remainCommand);
        sb.AppendLine();
    }
}