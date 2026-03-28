namespace SunamoTextOutputGenerator.Tests;

/// <summary>
/// Tests for the TextOutputGeneratorStatic class.
/// </summary>
public class TextOutputGeneratorStaticTests
{
    /// <summary>
    /// Tests that CompareList produces correct output with both, first, and second lists.
    /// </summary>
    [Fact]
    public void CompareListTest()
    {
        var both = new List<string> { "a" };
        var firstList = new List<string> { "a", "c" };
        var secondList = new List<string> { "a", "d" };

        string result = TextOutputGeneratorStatic.CompareList(both, "firstList", firstList, "secondList", secondList);

        Assert.Contains("both", result);
        Assert.Contains("firstList", result);
        Assert.Contains("secondList", result);
        Assert.Contains("a", result);
        Assert.Contains("c", result);
        Assert.Contains("d", result);
    }

    /// <summary>
    /// Tests that Dictionary produces formatted output from a string-string dictionary.
    /// </summary>
    [Fact]
    public void DictionaryTest()
    {
        var dictionary = new Dictionary<string, string>
        {
            { "key1", "value1" },
            { "key2", "value2" }
        };

        string result = TextOutputGeneratorStatic.Dictionary(dictionary);

        Assert.Contains("key1", result);
        Assert.Contains("value1", result);
        Assert.Contains("key2", result);
        Assert.Contains("value2", result);
    }
}
