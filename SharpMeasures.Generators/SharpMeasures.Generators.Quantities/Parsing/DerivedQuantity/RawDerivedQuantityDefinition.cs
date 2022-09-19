namespace SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;

public sealed record class RawDerivedQuantityDefinition : ARawAttributeDefinition<RawDerivedQuantityDefinition, DerivedQuantityLocations>
{
    public static RawDerivedQuantityDefinition FromSymbolic(SymbolicDerivedQuantityDefinition symbolicDefinition) => new RawDerivedQuantityDefinition(symbolicDefinition.Locations) with
    {
        Expression = symbolicDefinition.Expression,
        Signature = symbolicDefinition.Signature.AsNamedTypes(),
        OperatorImplementation = symbolicDefinition.OperatorImplementation,
        Permutations = symbolicDefinition.Permutations
    };

    public string? Expression { get; init; }
    public IReadOnlyList<NamedType?> Signature
    {
        get => signature;
        init => signature = value.AsReadOnlyEquatable();
    }

    public DerivationOperatorImplementation OperatorImplementation { get; init; } = DerivationOperatorImplementation.Suitable;

    public bool Permutations { get; init; }

    private IReadOnlyList<NamedType?> signature { get; init; } = ReadOnlyEquatableList<NamedType?>.Empty;

    protected override RawDerivedQuantityDefinition Definition => this;

    private RawDerivedQuantityDefinition(DerivedQuantityLocations locations) : base(locations) { }
}
