namespace SharpMeasures.Generators.Units.Parsing.Contexts.Processing;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Units.Parsing.DerivableUnit;

using System.Collections.Generic;

internal record class DerivableUnitProcessingContext : SimpleProcessingContext, IDerivableUnitProcessingContext
{
    public bool UnitIncludesBiasTerm { get; }

    public bool UnitHasMultipleDerivations { get; }
    public HashSet<string> ReservedDerivationIDs { get; } = new();
    public HashSet<DerivableUnitSignature> ReservedSignatures { get; } = new();

    public DerivableUnitProcessingContext(DefinedType type, bool unitIncludesUnitTerm, bool multipleDefinitions) : base(type)
    {
        UnitIncludesBiasTerm = unitIncludesUnitTerm;

        UnitHasMultipleDerivations = multipleDefinitions;
    }
}
