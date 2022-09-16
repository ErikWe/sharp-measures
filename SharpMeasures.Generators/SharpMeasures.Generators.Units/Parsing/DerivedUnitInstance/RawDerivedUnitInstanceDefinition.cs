namespace SharpMeasures.Generators.Units.Parsing.DerivedUnitInstance;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

using System.Collections.Generic;

internal record class RawDerivedUnitInstanceDefinition : ARawUnitInstance<RawDerivedUnitInstanceDefinition, DerivedUnitInstanceLocations>
{
    public static RawDerivedUnitInstanceDefinition Empty { get; } = new(DerivedUnitInstanceLocations.Empty);

    public string? DerivationID { get; init; }
    public IReadOnlyList<string?> Units
    {
        get => units;
        init => units = value.AsReadOnlyEquatable(); 
    }

    private ReadOnlyEquatableList<string?> units { get; init; } = ReadOnlyEquatableList<string?>.Empty;

    protected override RawDerivedUnitInstanceDefinition Definition => this;

    private RawDerivedUnitInstanceDefinition(DerivedUnitInstanceLocations locations) : base(locations) { }
}
