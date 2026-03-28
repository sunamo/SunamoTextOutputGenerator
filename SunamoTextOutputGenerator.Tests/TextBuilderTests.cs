namespace sunamo.Tests.Generator.Text;

/// <summary>
/// Tests for the TextBuilder class.
/// </summary>
public class TextBuilderTests
{
    /// <summary>
    /// Tests that undo reverts the last append operation.
    /// </summary>
    [Fact]
    public void UndoTest()
    {
        TextBuilder textBuilder = new TextBuilder();
        textBuilder.CanUndo = true;
        string original = "Ahoj";

        textBuilder.Append(original);
        textBuilder.Append("Svete");

        textBuilder.Undo();
        Assert.Equal(original, textBuilder.ToString());
    }
}
