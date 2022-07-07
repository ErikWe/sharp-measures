namespace SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;

public record class DerivedQuantityLocations : AAttributeLocations<DerivedQuantityLocations>
{
    public static DerivedQuantityLocations Empty { get; } = new();

    public MinimalLocation? Expression { get; init; }
    public MinimalLocation? SignatureCollection { get; init; }
    public IReadOnlyList<MinimalLocation> SignatureElements
    {
        get => signatureElements;
        init => signatureElements = value.AsReadOnlyEquatable();
    }

    public MinimalLocation? ImplementOperators { get; init; }
    public MinimalLocation? ImplementAlgebraicallyEquivalentDerivations { get; init; }

    private ReadOnlyEquatableList<MinimalLocation> signatureElements { get; init; } = ReadOnlyEquatableList<MinimalLocation>.Empty;

    protected override DerivedQuantityLocations Locations => this;

    private DerivedQuantityLocations() { }
}
