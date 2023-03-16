namespace SharpMeasures.Generators.Units.Parsing.DerivableUnit;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;

internal sealed record class DerivableUnitLocations : AAttributeLocations<DerivableUnitLocations>, IDerivableUnitLocations
{
    public static DerivableUnitLocations Empty { get; } = new();

    public MinimalLocation? Expression { get; init; }
    public MinimalLocation? DerivationID { get; init; }

    public MinimalLocation? SignatureCollection { get; init; }
    public IReadOnlyList<MinimalLocation> SignatureElements
    {
        get => signatureElementsField;
        init => signatureElementsField = value.AsReadOnlyEquatable();
    }

    public MinimalLocation? Permutations { get; init; }

    public bool ExplicitlySetExpression => Expression is not null;
    public bool ExplicitlySetDerivationID => DerivationID is not null;
    public bool ExplicitlySetSignature => SignatureCollection is not null;
    public bool ExplicitlySetPermutations => Permutations is not null;

    protected override DerivableUnitLocations Locations => this;

    private readonly IReadOnlyList<MinimalLocation> signatureElementsField = ReadOnlyEquatableList<MinimalLocation>.Empty;

    private DerivableUnitLocations() { }
}
