namespace SharpMeasures.Generators.Scalars.ForeignScalarParsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Scalars.Parsing;

using System;
using System.Collections.Generic;

public sealed class ForeignScalarParser
{
    private static ScalarBaseParser ScalarBaseParser { get; } = new(alreadyInForeignAssembly: true);
    private static ScalarSpecializationParser ScalarSpecializationParser { get; } = new(alreadyInForeignAssembly: true);

    private List<RawScalarBaseType> ScalarBases { get; } = new();
    private List<RawScalarSpecializationType> ScalarSpecializations { get; } = new();

    public (bool Sucess, IEnumerable<INamedTypeSymbol> ReferencedSymbols) TryParse(INamedTypeSymbol typeSymbol)
    {
        var scalarResult = ScalarBaseParser.Parse(typeSymbol);

        if (scalarResult.HasValue)
        {
            ScalarBases.Add(scalarResult.Value.Definition);

            return (true, scalarResult.Value.ForeignSymbols);
        }

        var specializedScalarResult = ScalarSpecializationParser.Parse(typeSymbol);

        if (specializedScalarResult.HasValue)
        {
            ScalarSpecializations.Add(specializedScalarResult.Value.Definition);

            return (true, specializedScalarResult.Value.ForeignSymbols);
        }

        return (false, Array.Empty<INamedTypeSymbol>());
    }

    public ForeignScalarParsingResult Finalize() => new(ScalarBases, ScalarSpecializations);
}
