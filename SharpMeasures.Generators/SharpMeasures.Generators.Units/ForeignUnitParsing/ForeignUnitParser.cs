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
        var result = UnitParser.Parse(typeSymbol);

        if (result.HasValue)
        {
            Units.Add(result.Value.Definition);

            return (true, result.Value.ForeignSymbols);
        }

        return (false, Array.Empty<INamedTypeSymbol>());
    }

    public ForeignUnitParsingResult Finalize() => new(Units);
}
