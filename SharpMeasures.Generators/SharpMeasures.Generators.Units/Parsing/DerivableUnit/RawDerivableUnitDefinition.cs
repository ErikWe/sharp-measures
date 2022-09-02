namespace SharpMeasures.Generators.Units.Parsing.DerivableUnit;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;

internal record class RawDerivableUnitDefinition : ARawAttributeDefinition<RawDerivableUnitDefinition, DerivableUnitLocations>
{
    public static RawDerivableUnitDefinition Empty { get; } = new();

    public string? Expression { get; init; }
    public string? DerivationID { get; init; }
    public IReadOnlyList<NamedType?>? Signature
    {
        get => signature;
        init => signature = value?.AsReadOnlyEquatable();
    }

    public bool Permutations { get; init; }

    private ReadOnlyEquatableList<NamedType?>? signature { get; init; }

    protected override RawDerivableUnitDefinition Definition => this;

    private RawDerivableUnitDefinition() : base(DerivableUnitLocations.Empty) { }
}
