namespace SharpMeasures.Generators.Scalars.ForeignScalarParsing;

using Microsoft.CodeAnalysis;

using SharpMeasures.Generators.Scalars.Parsing;

using System.Collections.Generic;

public sealed class ForeignScalarParser
{
    private List<RawScalarBaseType> ScalarBases { get; } = new();
    private List<RawScalarSpecializationType> ScalarSpecializations { get; } = new();

    private ForeignScalarBaseParser ScalarBaseParser { get; } = new();
    private ForeignScalarSpecializationParser ScalarSpecializationParser { get; } = new();

    public Optional<IEnumerable<INamedTypeSymbol>> TryParse(INamedTypeSymbol typeSymbol)
    {
        (var scalarBase, var scalarBaseSymbols) = ScalarBaseParser.Parse(typeSymbol);

        if (scalarBase.HasValue)
        {
            ScalarBases.Add(scalarBase.Value);

            return new Optional<IEnumerable<INamedTypeSymbol>>(scalarBaseSymbols);
        }

        (var scalarSpecialization, var scalarSpecializationSymbols) = ScalarSpecializationParser.Parse(typeSymbol);

        if (scalarSpecialization.HasValue)
        {
            ScalarSpecializations.Add(scalarSpecialization.Value);

            return new Optional<IEnumerable<INamedTypeSymbol>>(scalarSpecializationSymbols);
        }

        return new Optional<IEnumerable<INamedTypeSymbol>>();
    }

    public IForeignScalarProcesser Finalize() => new ForeignScalarProcesser(new ForeignScalarParsingResult(ScalarBases, ScalarSpecializations));
}
