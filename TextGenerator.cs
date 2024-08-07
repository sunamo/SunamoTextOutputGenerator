namespace SunamoTextOutputGenerator;

/// <summary>
///     TextWriterList - instance
///     TextBuilder - instance
///     TextOutputGenerator - instance
///     TextGenerator - static
/// </summary>
public static class TextGenerator
{
    /// <summary>
    ///     Keep as IList, not List because to IList can be casted every List
    /// </summary>
    /// <param name="p"></param>
    /// <returns></returns>
    public static string GenerateListWithPercent(Dictionary<string, List<string>> p)
    {
        return GenerateListWithPercent<string, string>(p);
    }

    public static string GenerateListWithPercent<T, U>(Dictionary<T, List<U>> p)
    {
        var d = new Dictionary<T, List<U>>(p.Count);
        foreach (var item in p) d.Add(item.Key, item.Value);

        return GenerateListWithPercent(d);
    }

    /// <summary>
    ///     24-9-23 nahrazeno za List z IList (pokud je Dictionary, musí být přesný, nestačí IList)
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="U"></typeparam>
    /// <param name="p"></param>
    /// <returns></returns>
    public static string GenerateListWithPercent<T, U>(Dictionary<T, IList<U>> p, IPercentCalculatorTog pc2)
    {
        var overall = 0;

        foreach (var item in p) overall += item.Value.Count();

        var pc = pc2.Create(overall); //new PercentCalculator(overall); ;

        var tog = new TextOutputGenerator();

        var withoutLast = p.Take(p.Count() - 1);

        var p2 = 0;
        var p3 = 0;

        var kvp = p.Last();

        var percent2 = new Dictionary<T, int>();

        foreach (var item in withoutLast)
        {
            p2 = pc.PercentFor(item.Value.Count(), false);

            p3 += p2;

            percent2.Add(item.Key, p2);
        }

        p2 = pc.PercentFor(kvp.Value.Count(), false);
        p3 += p2;
        percent2.Add(kvp.Key, p2);

        var largest = 0;
        T keyLargest = default;

        if (p3 != 0)
        {
            foreach (var item in percent2)
                if (item.Value > largest)
                {
                    largest = item.Value;
                    keyLargest = item.Key;
                    break;
                }

            percent2[keyLargest] = percent2[keyLargest] + (100 - p3);
        }

        foreach (var item in withoutLast)
            //tog.List(withoutLast.First(d => d.Key == item.Key).Value, item.Key + " (" + item.Value + "%)");
            tog.List(item.Value, item.Key + " (" + percent2[item.Key] + "%)");

        //p2 = pc.PercentFor(kvp.Value.Count(), false);

        tog.List(kvp.Value, kvp.Key + " (" + (100 - p2) + "%)");
        return tog.ToString();
    }
}