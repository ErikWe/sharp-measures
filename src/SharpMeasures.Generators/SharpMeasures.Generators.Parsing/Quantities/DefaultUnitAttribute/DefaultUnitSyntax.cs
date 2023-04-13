namespace SharpMeasures.Generators.Parsing.Quantities.DefaultUnitAttribute;

using Microsoft.CodeAnalysis;

using SharpMeasures;

/// <inheritdoc cref="IDefaultUnitSyntax"/>
internal sealed record class DefaultUnitSyntax : IDefaultUnitSyntax
{
    private Location Unit { get; }
    private Location Symbol { get; }

    /// <summary>Instantiates a <see cref="DefaultUnitSyntax"/>, representing syntactical information about a parsed <see cref="DefaultUnitAttribute"/>.</summary>
    /// <param name="unit"></param>
    /// <param name="symbol"></param>
    public DefaultUnitSyntax(Location unit, Location symbol)
    {
        Unit = unit;
        Symbol = symbol;
    }

    Location IDefaultUnitSyntax.Unit => Unit;
    Location IDefaultUnitSyntax.Symbol => Symbol;
}
