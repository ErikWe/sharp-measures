namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

public record class CubableScalarParsingData : APoweredScalarParsingData<CubableScalarLocations>
{
    internal static CubableScalarParsingData Empty { get; } = new();

    private CubableScalarParsingData() : base(CubableScalarLocations.Empty) { }
}