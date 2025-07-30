namespace SunamoTextOutputGenerator.Tests;

public class TextOutputGeneratorTests
{
    [Fact]
    public void IGroupingTest()
    {
        List<string> a = new List<string>();
        a.Add("z\\a");
        a.Add("z\\a");
        a.Add("z\\b");
        a.Add("z\\b");
        a.Add("z\\c");

        var grouped2 = a.GroupBy(d => Path.GetFileNameWithoutExtension(d));

        var sameFn2 = grouped2.Where(d => d.Count() > 1);

        TextOutputGenerator tog = new TextOutputGenerator();
        tog.IGrouping(sameFn2);

        var ts = tog.ToString();
    }
}
