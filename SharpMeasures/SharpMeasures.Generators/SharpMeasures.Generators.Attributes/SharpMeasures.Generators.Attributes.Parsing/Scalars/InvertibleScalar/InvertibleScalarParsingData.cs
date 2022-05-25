namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

public record class InvertibleScalarParsingData : APoweredScalarParsingData<InvertibleScalarLocations>
{
    internal static InvertibleScalarParsingData Empty { get; } = new();

    private InvertibleScalarParsingData() : base(InvertibleScalarLocations.Empty) { }
}