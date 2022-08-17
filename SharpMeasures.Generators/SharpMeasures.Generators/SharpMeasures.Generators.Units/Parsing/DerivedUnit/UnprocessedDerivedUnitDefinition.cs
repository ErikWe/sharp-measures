namespace SharpMeasures.Generators.Units.Parsing.DerivedUnit;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

using System.Collections.Generic;

internal record class UnprocessedDerivedUnitDefinition : AUnprocessedUnitDefinition<UnprocessedDerivedUnitDefinition, DerivedUnitLocations>
{
    public static UnprocessedDerivedUnitDefinition Empty { get; } = new();

    public string? DerivationID { get; init; }
    public IReadOnlyList<string?> Units
    {
        get => units;
        init => units = value.AsReadOnlyEquatable(); 
    }

    private ReadOnlyEquatableList<string?> units { get; init; } = ReadOnlyEquatableList<string?>.Empty;

    protected override UnprocessedDerivedUnitDefinition Definition => this;

    private UnprocessedDerivedUnitDefinition() : base(DerivedUnitLocations.Empty) { }
}
