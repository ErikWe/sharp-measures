namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

public record class SquarableScalarDefinition : APoweredScalarDefinition<SquarableScalarParsingData, SquarableScalarLocations>
{
    internal static SquarableScalarDefinition Empty { get; } = new();

    private SquarableScalarDefinition() : base(SquarableScalarParsingData.Empty) { }
}