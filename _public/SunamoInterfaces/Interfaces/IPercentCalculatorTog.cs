namespace SunamoTextOutputGenerator._public.SunamoInterfaces.Interfaces;

public interface IPercentCalculatorTog
{
    double _overallSum { get; set; }
    double last { get; set; }
    IPercentCalculatorTog Create(double overallSum);
    void AddOnePercent();
    int PercentFor(double value, bool last);
    void ResetComputedSum();
}