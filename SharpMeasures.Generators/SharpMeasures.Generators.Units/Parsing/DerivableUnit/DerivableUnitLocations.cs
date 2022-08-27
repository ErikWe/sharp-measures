namespace SharpMeasures.Generators.Units.Parsing.DerivableUnit;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;

internal record class DerivableUnitLocations : AAttributeLocations<DerivableUnitLocations>, IOpenAttributeLocations<DerivableUnitLocations>
{
    public static DerivableUnitLocations Empty { get; } = new();

    public MinimalLocation? Expression { get; init; }
    public MinimalLocation? DerivationID { get; init; }

    public MinimalLocation? SignatureCollection { get; init; }
    public IReadOnlyList<MinimalLocation> SignatureElements
    {
        get => signatureElements;
        init => signatureElements = value.AsReadOnlyEquatable();
    }

    public bool ExplicitlySetExpression => Expression is not null;
    public bool ExplicitlySetDerivationID => DerivationID is not null;
    public bool ExplicitlySetSignature => SignatureCollection is not null;

    protected override DerivableUnitLocations Locations => this;

    private ReadOnlyEquatableList<MinimalLocation> signatureElements { get; init; } = ReadOnlyEquatableList<MinimalLocation>.Empty;

    private DerivableUnitLocations() { }
}
