// EN: Variable names have been checked and replaced with self-descriptive names
// CZ: Názvy proměnných byly zkontrolovány a nahrazeny samopopisnými názvy
namespace SunamoTextOutputGenerator;
/// <summary>
///     In Comparing
/// </summary>
public partial class TextOutputGenerator
{
    public void Dictionary(Dictionary<string, List<string>> ls)
    {
        foreach (var item in ls)
            List(item.Value, item.Key);
    }

    public void Dictionary<Header, Value>(Dictionary<Header, List<Value>> ls, bool onlyCountInValue = false)
        where Header : IEnumerable<char>
    {
        if (onlyCountInValue)
        {
            var dictionary = new List<string>(ls.Count);
            foreach (var item in ls)
                dictionary.Add(item.Key + " " + item.Value.Count());
            List(dictionary);
        }
        else
        {
            foreach (var item in ls)
                List(item.Value, item.Key);
        }
    }

    /// <summary>
    ///     vše na 1 řádku, oddělí |
    /// </summary>
    /// <param name = "v"></param>
    public void Dictionary(Dictionary<string, string> v)
    {
        foreach (var item in v)
            stringBuilder.AppendLine(string.Join("|", item.Key, item.Value));
    }

    /// <summary>
    ///     vše na 1 řádku, oddělí |
    /// </summary>
    /// <typeparam name = "T1"></typeparam>
    /// <typeparam name = "T2"></typeparam>
    /// <param name = "d"></param>
    /// <param name = "deli"></param>
    public void Dictionary<T1, T2>(Dictionary<T1, T2> dictionary, string deli = "|")
    {
        //StringBuilder stringBuilder = new StringBuilder();
        foreach (var item in dictionary)
            if (deli != "|")
            {
                Header(item.Key.ToString());
                // vrací mi to na jednom řádku jak key tak všechny value oddělené |.
                stringBuilder.AppendLine(string.Join(deli, item.Value.ToString()));
                stringBuilder.AppendLine();
            }
            else
            {
                // vrací mi to na jednom řádku jak key tak všechny value oddělené |.
                stringBuilder.AppendLine(string.Join(deli, item.Key.ToString(), item.Value.ToString())); // SF.PrepareToSerializationExplicitString(new List<string>(), deli));
            }
    }

    public void PairBullet(string key, string v)
    {
        stringBuilder.AppendLine(key + ": " + v);
    }

    public string DictionaryBothToStringToSingleLine<Key, Value>(Dictionary<Key, Value> sorted, bool putValueAsFirst, string delimiter = " ")
    {
        foreach (var item in sorted)
        {
            string first, second = null;
            if (putValueAsFirst)
            {
                first = item.Value.ToString();
                second = item.Key.ToString();
            }
            else
            {
                first = item.Key.ToString();
                second = item.Value.ToString();
            }

            stringBuilder.AppendLine(first + delimiter + second);
        }

        return stringBuilder.ToString();
    }
}