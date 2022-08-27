namespace SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVectorGroup;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.DefaultUnit;
using SharpMeasures.Generators.Vectors;

internal record class SpecializedSharpMeasuresVectorGroupDefinition : AAttributeDefinition<SpecializedSharpMeasuresVectorGroupLocations>, IVectorGroupSpecialization, IDefaultUnitDefinition
{
    public NamedType OriginalVectorGroup { get; }

    public bool InheritDerivations { get; }
    public bool InheritConstants { get; }
    public bool InheritConversions { get; }
    public bool InheritUnits { get; }

    public NamedType? Scalar { get; }

    public bool? ImplementSum { get; }
    public bool? ImplementDifference { get; }
    public NamedType? Difference { get; }

    public string? DefaultUnitName { get; }
    public string? DefaultUnitSymbol { get; }

    public bool? GenerateDocumentation { get; }

    IDefaultUnitLocations IDefaultUnitDefinition.DefaultUnitLocations => Locations;

    NamedType IQuantitySpecialization.OriginalQuantity => OriginalVectorGroup;

    public SpecializedSharpMeasuresVectorGroupDefinition(NamedType originalVectorGroup, bool inheritDerivations, bool inheritConstants, bool inheritConversions, bool inheritUnits,
        NamedType? scalar, bool? implementSum, bool? implementDifference, NamedType? difference, string? defaultUnitName, string? defaultUnitSymbol, bool? generateDocumentation,
        SpecializedSharpMeasuresVectorGroupLocations locations)
        : base(locations)
    {
        OriginalVectorGroup = originalVectorGroup;

        InheritDerivations = inheritDerivations;
        InheritConstants = inheritConstants;
        InheritConversions = inheritConversions;
        InheritUnits = inheritUnits;

        Scalar = scalar;

        ImplementSum = implementSum;
        ImplementDifference = implementDifference;
        Difference = difference;

        DefaultUnitName = defaultUnitName;
        DefaultUnitSymbol = defaultUnitSymbol;

        GenerateDocumentation = generateDocumentation;
    }
}
