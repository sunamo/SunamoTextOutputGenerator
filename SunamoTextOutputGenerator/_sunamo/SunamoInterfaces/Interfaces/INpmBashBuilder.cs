namespace SunamoTextOutputGenerator._sunamo.SunamoInterfaces.Interfaces;

/// <summary>
/// Interface for building npm bash commands.
/// </summary>
internal interface INpmBashBuilder
{
    /// <summary>
    /// Runs npm install with optional arguments.
    /// </summary>
    /// <param name="arguments">Optional npm install arguments.</param>
    void Install(string? arguments = null);
}
