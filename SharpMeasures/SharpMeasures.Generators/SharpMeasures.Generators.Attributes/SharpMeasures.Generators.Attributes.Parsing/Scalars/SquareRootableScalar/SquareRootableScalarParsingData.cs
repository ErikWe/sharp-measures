namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

public record class SquareRootableScalarParsingData : APoweredScalarParsingData<SquareRootableScalarLocations>
{
    internal static SquareRootableScalarParsingData Empty { get; } = new();

    private SquareRootableScalarParsingData() : base(SquareRootableScalarLocations.Empty) { }
}