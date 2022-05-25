namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

public record class SquarableScalarParsingData : APoweredScalarParsingData<SquarableScalarLocations>
{
    internal static SquarableScalarParsingData Empty { get; } = new();

    private SquarableScalarParsingData() : base(SquarableScalarLocations.Empty) { }
}