namespace SharpMeasures.Generators.Units.ForeignUnitParsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Units.Parsing;

using System;
using System.Collections.Generic;

public sealed class ForeignUnitParser
{
    private List<RawUnitType> Units { get; } = new();

    public (bool Success, IEnumerable<INamedTypeSymbol> ReferencedSymbols) TryParse(INamedTypeSymbol typeSymbol)
    {
        (var unit, var unitReferencedSymbols) = UnitParser.Parse(typeSymbol);

        if (unit.HasValue)
        {
            Units.Add(unit.Value);

            return (true, unitReferencedSymbols);
        }

        return (false, Array.Empty<INamedTypeSymbol>());
    }

    public ForeignUnitParsingResult Finalize() => new(Units);
}
