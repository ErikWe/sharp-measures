namespace SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;

public sealed record class DerivedQuantityLocations : AAttributeLocations<DerivedQuantityLocations>, IDerivedQuantityLocations
{
    public static DerivedQuantityLocations Empty { get; } = new();

    public MinimalLocation? Expression { get; init; }
    public MinimalLocation? SignatureCollection { get; init; }
    public IReadOnlyList<MinimalLocation> SignatureElements
    {
        get => signatureElements;
        init => signatureElements = value.AsReadOnlyEquatable();
    }

    public MinimalLocation? OperatorImplementation { get; init; }

    public MinimalLocation? Permutations { get; init; }

    public bool ExplicitlySetExpression => Expression is not null;
    public bool ExplicitlySetSignature => SignatureCollection is not null;

    public bool ExplicitlySetOperatorImplementation => OperatorImplementation is not null;

    public bool ExplicitlySetPermutations => Permutations is not null;

    private IReadOnlyList<MinimalLocation> signatureElements { get; init; } = ReadOnlyEquatableList<MinimalLocation>.Empty;

    protected override DerivedQuantityLocations Locations => this;

    private DerivedQuantityLocations() { }
}
