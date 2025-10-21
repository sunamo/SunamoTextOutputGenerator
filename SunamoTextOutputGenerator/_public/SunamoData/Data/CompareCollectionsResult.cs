// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoTextOutputGenerator._public.SunamoData.Data;

public class CompareCollectionsResult<T>
{
    public List<T> Both;
    public List<T> OnlyInFirst;
    public List<T> OnlyInSecond;
}