namespace SharpMeasures.Generators.Units.Parsing.DerivedUnit;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

using System.Collections.Generic;

internal record class RawDerivedUnitDefinition : ARawUnitDefinition<RawDerivedUnitDefinition, DerivedUnitLocations>
{
    public static RawDerivedUnitDefinition Empty { get; } = new();

    public string? SignatureID { get; init; }
    public IReadOnlyList<string?> Units
    {
        get => units;
        init => units = value.AsReadOnlyEquatable(); 
    }

    private ReadOnlyEquatableList<string?> units { get; init; } = ReadOnlyEquatableList<string?>.Empty;

    protected override RawDerivedUnitDefinition Definition => this;

    private RawDerivedUnitDefinition() : base(DerivedUnitLocations.Empty) { }
}
