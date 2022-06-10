namespace SharpMeasures.Generators.Units.Parsing.DerivedUnit;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal record class DerivedUnitLocations : AUnitLocations
{
    public static DerivedUnitLocations Empty { get; } = new();

    public MinimalLocation? SignatureCollection { get; init; }
    public ReadOnlyEquatableList<MinimalLocation> SignatureElements { get; init; } = ReadOnlyEquatableList<MinimalLocation>.Empty;

    public MinimalLocation? UnitsCollection { get; init; }
    public ReadOnlyEquatableList<MinimalLocation> UnitsElements { get; init; } = ReadOnlyEquatableList<MinimalLocation>.Empty;

    public bool ExplicitlySetSignature => SignatureCollection is not null;
    public bool ExplicitlySetUnits => UnitsCollection is not null;

    private DerivedUnitLocations() { }
}
