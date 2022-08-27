namespace SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVector;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Quantities.Parsing.DefaultUnit;

internal record class SpecializedSharpMeasuresVectorDefinition : AAttributeDefinition<SpecializedSharpMeasuresVectorLocations>, IVectorSpecialization, IDefaultUnitDefinition
{
    public NamedType OriginalVector { get; }

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

    NamedType IQuantitySpecialization.OriginalQuantity => OriginalVector;

    public SpecializedSharpMeasuresVectorDefinition(NamedType originalVector, bool inheritDerivations, bool inheritConstants, bool inheritConversions, bool inheritUnits, NamedType? scalar, bool? implementSum,
        bool? implementDifference, NamedType? difference, string? defaultUnitName, string? defaultUnitSymbol, bool? generateDocumentation, SpecializedSharpMeasuresVectorLocations locations)
        : base(locations)
    {
        OriginalVector = originalVector;

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
