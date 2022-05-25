namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

public record class InvertibleScalarDefinition : APoweredScalarDefinition<InvertibleScalarParsingData, InvertibleScalarLocations>
{
    internal static InvertibleScalarDefinition Empty { get; } = new();

    private InvertibleScalarDefinition() : base(InvertibleScalarParsingData.Empty) { }
}