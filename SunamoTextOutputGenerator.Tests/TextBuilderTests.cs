namespace sunamo.Tests.Generator.Text;

public class TextBuilderTests
{
    [Fact]
    public void UndoTest()
    {
        TextBuilder tb = new TextBuilder();
        tb.CanUndo = true;
        string original = "Ahoj";
        
        tb.Append(original);
        tb.Append("Svete");
        
        tb.Undo();
        Assert.Equal(original, tb.ToString());
    }
}
