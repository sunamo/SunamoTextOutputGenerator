namespace SunamoTextOutputGenerator._public.SunamoInterfaces.Interfaces;

public interface ITextBuilder
{
    bool CanUndo { get; set; }
    List<string> list { get; set; }
    string prependEveryNoWhite { get; set; }
    void Append(object s);
    void Append(string s);
    void AppendLine();
    void AppendLine(string s);
    void Clear();
    string ToString();
    void Undo();
}