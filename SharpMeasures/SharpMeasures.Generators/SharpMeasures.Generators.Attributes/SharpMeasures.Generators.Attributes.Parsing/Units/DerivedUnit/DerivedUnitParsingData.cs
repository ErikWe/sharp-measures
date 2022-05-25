namespace SharpMeasures.Generators.Attributes.Parsing.Units;

public record class DerivedUnitParsingData : AUnitParsingData<DerivedUnitLocations>
{
    internal static DerivedUnitParsingData Empty { get; } = new();

    private DerivedUnitParsingData() : base(DerivedUnitLocations.Empty) { }
}
