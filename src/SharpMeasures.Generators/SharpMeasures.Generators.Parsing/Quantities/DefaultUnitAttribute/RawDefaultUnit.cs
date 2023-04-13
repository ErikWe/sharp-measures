namespace SharpMeasures.Generators.Parsing.Quantities.DefaultUnitAttribute;

using SharpMeasures;

/// <inheritdoc cref="IRawDefaultUnit"/>
internal sealed record class RawDefaultUnit : IRawDefaultUnit
{
    private string? Unit { get; }
    private string? Symbol { get; }

    private IDefaultUnitSyntax? Syntax { get; }

    /// <summary>Instantiates a <see cref="RawDefaultUnit"/>, representing a parsed <see cref="DefaultUnitAttribute"/>.</summary>
    /// <param name="unit"><inheritdoc cref="IRawDefaultUnit.Unit" path="/summary"/></param>
    /// <param name="symbol"><inheritdoc cref="IRawDefaultUnit.Symbol" path="/summary"/></param>
    /// <param name="syntax"><inheritdoc cref="IRawDefaultUnit.Syntax" path="/summary"/></param>
    public RawDefaultUnit(string? unit, string? symbol, IDefaultUnitSyntax? syntax)
    {
        Unit = unit;
        Symbol = symbol;

        Syntax = syntax;
    }

    string? IRawDefaultUnit.Unit => Unit;
    string? IRawDefaultUnit.Symbol => Symbol;

    IDefaultUnitSyntax? IRawDefaultUnit.Syntax => Syntax;
}
