namespace SharpMeasures.Generators.Units.Parsing.DerivedUnit;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal record class DerivedUnitLocations : AUnitLocations
{
    public static DerivedUnitLocations Empty { get; } = new();

    public MinimalLocation? SignatureID { get; init; }
    
    public MinimalLocation? UnitsCollection { get; init; }
    public ReadOnlyEquatableList<MinimalLocation> UnitsElements { get; init; } = ReadOnlyEquatableList<MinimalLocation>.Empty;

    public bool ExplicitlySetSignatureID => SignatureID is not null;
    public bool ExplicitlySetUnits => UnitsCollection is not null;

    private DerivedUnitLocations() { }
}
