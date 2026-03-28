namespace SunamoTextOutputGenerator._public.SunamoInterfaces.Interfaces;

/// <summary>
/// Interface for calculating percentages in text output generation.
/// </summary>
public interface IPercentCalculatorTog
{
    /// <summary>
    /// Gets or sets the overall sum used for percentage calculations.
    /// </summary>
    double OverallSum { get; set; }

    /// <summary>
    /// Gets or sets the last calculated value.
    /// </summary>
    double Last { get; set; }

    /// <summary>
    /// Creates a new instance with the specified overall sum.
    /// </summary>
    /// <param name="overallSum">The total sum for percentage calculation.</param>
    /// <returns>A new percent calculator instance.</returns>
    IPercentCalculatorTog Create(double overallSum);

    /// <summary>
    /// Adds one percent to the computed sum.
    /// </summary>
    void AddOnePercent();

    /// <summary>
    /// Calculates the percentage for a given value.
    /// </summary>
    /// <param name="value">The value to calculate percentage for.</param>
    /// <param name="isLast">Whether this is the last calculation in the sequence.</param>
    /// <returns>The calculated percentage as an integer.</returns>
    int PercentFor(double value, bool isLast);

    /// <summary>
    /// Resets the computed sum to zero.
    /// </summary>
    void ResetComputedSum();
}
