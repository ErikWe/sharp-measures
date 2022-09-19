namespace SharpMeasures.Generators.Units.Parsing.Contexts.Processing;

using SharpMeasures.Generators.Units.Parsing.DerivableUnit;

using System.Collections.Generic;

internal sealed record class DerivableUnitProcessingContext : IDerivableUnitProcessingContext
{
    public DefinedType Type { get; }

    public bool UnitIncludesBiasTerm { get; }

    public bool UnitHasMultipleDerivations { get; }
    public HashSet<string> ReservedDerivationIDs { get; } = new();
    public HashSet<DerivableUnitSignature> ReservedSignatures { get; } = new();

    public DerivableUnitProcessingContext(DefinedType type, bool unitIncludesUnitTerm, bool multipleDefinitions)
    {
        Type = type;

        UnitIncludesBiasTerm = unitIncludesUnitTerm;

        UnitHasMultipleDerivations = multipleDefinitions;
    }
}
