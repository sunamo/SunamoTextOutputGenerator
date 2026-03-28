namespace SunamoTextOutputGenerator.Tests;

/// <summary>
/// Tests for the TextOutputGenerator class.
/// </summary>
public class TextOutputGeneratorTests
{
    /// <summary>
    /// Tests that IGrouping correctly formats grouped strings.
    /// </summary>
    [Fact]
    public void IGroupingTest()
    {
        List<string> list = new List<string>();
        list.Add("z\\a");
        list.Add("z\\a");
        list.Add("z\\b");
        list.Add("z\\b");
        list.Add("z\\c");

        var grouped = list.GroupBy(text => Path.GetFileNameWithoutExtension(text));

        var duplicates = grouped.Where(group => group.Count() > 1);

        TextOutputGenerator generator = new TextOutputGenerator();
        generator.IGrouping(duplicates);

        var result = generator.ToString();

        Assert.NotNull(result);
        Assert.Contains("a", result);
        Assert.Contains("b", result);
    }
}
