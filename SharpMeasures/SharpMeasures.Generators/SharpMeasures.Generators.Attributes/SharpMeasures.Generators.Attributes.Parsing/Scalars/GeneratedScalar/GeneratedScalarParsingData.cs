namespace SharpMeasures.Generators.Attributes.Parsing.Scalars;

public record class GeneratedScalarParsingData : AAttributeParsingData<GeneratedScalarLocations>
{
    internal static GeneratedScalarParsingData Empty { get; } = new();

    public bool SpecifiedVector { get; init; }
    public bool ExplicitlyDisabledDocumentation { get; init; }

    private GeneratedScalarParsingData() : base(GeneratedScalarLocations.Empty) { }
}