namespace SharpMeasures.Generators.Scalars.ForeignScalarParsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Scalars.Parsing;

using System;
using System.Collections.Generic;

public sealed class ForeignScalarParser
{
    private static ScalarBaseParser ScalarBaseParser { get; } = new();
    private static ScalarSpecializationParser ScalarSpecializationParser { get; } = new();

    private List<RawScalarBaseType> ScalarBases { get; } = new();
    private List<RawScalarSpecializationType> ScalarSpecializations { get; } = new();

    public (bool Sucess, IEnumerable<INamedTypeSymbol> ReferencedSymbols) TryParse(INamedTypeSymbol typeSymbol)
    {
        (var scalarBase, var scalarBaseSymbols) = ScalarBaseParser.Parse(typeSymbol);

        if (scalarBase.HasValue)
        {
            ScalarBases.Add(scalarBase.Value);

            return (true, scalarBaseSymbols);
        }

        (var scalarSpecialization, var scalarSpecializationSymbols) = ScalarSpecializationParser.Parse(typeSymbol);

        if (scalarSpecialization.HasValue)
        {
            ScalarSpecializations.Add(scalarSpecialization.Value);

            return (true, scalarSpecializationSymbols);
        }

        return (false, Array.Empty<INamedTypeSymbol>());
    }

    public ForeignScalarParsingResult Finalize() => new(ScalarBases, ScalarSpecializations);
}
