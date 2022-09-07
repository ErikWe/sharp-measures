namespace SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;

public record class RawDerivedQuantityDefinition : ARawAttributeDefinition<RawDerivedQuantityDefinition, DerivedQuantityLocations>
{
    public static RawDerivedQuantityDefinition Empty { get; } = new(DerivedQuantityLocations.Empty);

    public string? Expression { get; init; }
    public IReadOnlyList<NamedType?> Signature
    {
        get => signature;
        init => signature = value.AsReadOnlyEquatable();
    }

    public DerivationOperatorImplementation OperatorImplementation { get; init; } = DerivationOperatorImplementation.AlgebraicallyEquivalent;

    public bool Permutations { get; init; }

    private ReadOnlyEquatableList<NamedType?> signature { get; init; } = ReadOnlyEquatableList<NamedType?>.Empty;

    protected override RawDerivedQuantityDefinition Definition => this;

    private RawDerivedQuantityDefinition(DerivedQuantityLocations locations) : base(locations) { }
}
