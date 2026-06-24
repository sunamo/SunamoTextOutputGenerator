namespace SunamoTextOutputGenerator._public.SunamoInterfaces.Interfaces;

public interface IPercentCalculatorTog
{
    double OverallSum { get; set; }

    double Last { get; set; }

    IPercentCalculatorTog Create(double overallSum);

    void AddOnePercent();

    int PercentFor(double value, bool isLast);

    void ResetComputedSum();
}
