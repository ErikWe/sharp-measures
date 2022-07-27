namespace SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVectorGroup;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Unresolved.Quantities;
using SharpMeasures.Generators.Unresolved.Vectors;

internal record class UnresolvedSpecializedSharpMeasuresVectorGroupDefinition : AAttributeDefinition<SpecializedSharpMeasuresVectorGroupLocations>,
    IUnresolvedVectorGroupSpecialization
{
    public NamedType OriginalVectorGroup { get; }
    NamedType IUnresolvedQuantitySpecialization.OriginalQuantity => OriginalVectorGroup;

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

    public UnresolvedSpecializedSharpMeasuresVectorGroupDefinition(NamedType originalVectorGroup, bool inheritDerivations, bool inheritConstants, bool inheritConversions,
        bool inheritUnits, NamedType? scalar, bool? implementSum, bool? implementDifference, NamedType? difference, string? defaultUnitName, string? defaultUnitSymbol,
        bool? generateDocumentation, SpecializedSharpMeasuresVectorGroupLocations locations)
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
