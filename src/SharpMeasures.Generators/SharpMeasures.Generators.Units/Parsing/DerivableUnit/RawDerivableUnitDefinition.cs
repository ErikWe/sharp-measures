namespace SharpMeasures.Generators.Units.Parsing.DerivableUnit;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Attributes.Parsing;

using System.Collections.Generic;

internal sealed record class RawDerivableUnitDefinition : ARawAttributeDefinition<RawDerivableUnitDefinition, DerivableUnitLocations>
{
    public static RawDerivableUnitDefinition FromSymbolic(SymbolicDerivableUnitDefinition symolicDefinition) => new RawDerivableUnitDefinition(symolicDefinition.Locations) with
    {
        DerivationID = symolicDefinition.DerivationID,
        Expression = symolicDefinition.Expression,
        Signature = symolicDefinition.Signature?.AsNamedTypes(),
        Permutations = symolicDefinition.Permutations
    };

    public string? DerivationID { get; init; }
    public string? Expression { get; init; }
    public IReadOnlyList<NamedType?>? Signature
    {
        get => signatureField;
        init => signatureField = value?.AsReadOnlyEquatable();
    }

    public bool Permutations { get; init; }

    private readonly IReadOnlyList<NamedType?>? signatureField;

    protected override RawDerivableUnitDefinition Definition => this;

    private RawDerivableUnitDefinition(DerivableUnitLocations locations) : base(locations) { }
}
