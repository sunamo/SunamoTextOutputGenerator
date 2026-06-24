namespace SunamoTextOutputGenerator._public.SunamoInterfaces.Interfaces;

public interface ITextBuilder
{
    bool CanUndo { get; set; }

    List<string>? List { get; set; }

    string PrependEveryNoWhite { get; set; }

    void Append(object value);

    void Append(string text);

    void AppendLine();

    void AppendLine(string text);

    void Clear();

    string ToString();

    void Undo();
}
