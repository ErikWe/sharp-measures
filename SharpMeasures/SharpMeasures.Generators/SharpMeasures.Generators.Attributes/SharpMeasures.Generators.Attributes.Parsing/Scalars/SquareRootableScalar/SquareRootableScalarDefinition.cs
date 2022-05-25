namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

public record class SquareRootableScalarDefinition : APoweredScalarDefinition<SquareRootableScalarParsingData, SquareRootableScalarLocations>
{
    internal static SquareRootableScalarDefinition Empty { get; } = new();

    private SquareRootableScalarDefinition() : base(SquareRootableScalarParsingData.Empty) { }
}