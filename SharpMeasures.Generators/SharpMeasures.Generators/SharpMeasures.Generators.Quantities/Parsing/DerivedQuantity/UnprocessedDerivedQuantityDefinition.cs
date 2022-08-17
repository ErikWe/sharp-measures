namespace SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;

public record class UnprocessedDerivedQuantityDefinition : AUnprocessedAttributeDefinition<UnprocessedDerivedQuantityDefinition, DerivedQuantityLocations>
{
    public static UnprocessedDerivedQuantityDefinition Empty { get; } = new(DerivedQuantityLocations.Empty);

    public string? Expression { get; init; }
    public IReadOnlyList<NamedType?> Signature
    {
        get => signature;
        init => signature = value.AsReadOnlyEquatable();
    }

    public bool ImplementOperators { get; init; }
    public bool ImplementAlgebraicallyEquivalentDerivations { get; init; }

    private ReadOnlyEquatableList<NamedType?> signature { get; init; } = ReadOnlyEquatableList<NamedType?>.Empty;

    protected override UnprocessedDerivedQuantityDefinition Definition => this;

    private UnprocessedDerivedQuantityDefinition(DerivedQuantityLocations locations) : base(locations) { }
}
