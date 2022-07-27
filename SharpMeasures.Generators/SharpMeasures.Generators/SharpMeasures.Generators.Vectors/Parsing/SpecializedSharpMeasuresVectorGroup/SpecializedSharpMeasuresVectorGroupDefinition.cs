namespace SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVectorGroup;

using SharpMeasures.Generators.Unresolved.Scalars;
using SharpMeasures.Generators.Unresolved.Units;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;
using SharpMeasures.Generators.Unresolved.Vectors;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroup;

internal record class SpecializedSharpMeasuresVectorGroupDefinition : SharpMeasuresVectorGroupDefinition, IVectorGroupSpecialization
{
    public IUnresolvedVectorGroupType OriginalVectorGroup { get; }

    public bool InheritDerivations { get; }
    public bool InheritConstants { get; }
    public bool InheritConversions { get; }
    public bool InheritUnits { get; }

    public SpecializedSharpMeasuresVectorGroupDefinition(IUnresolvedVectorGroupType originalVectorGroup, bool inheritDerivations, bool inheritConstants,
        bool inheritConversions, bool inheritUnits, IUnresolvedUnitType unit, IUnresolvedScalarType? scalar, bool implementSum, bool implementDifference,
        IUnresolvedVectorGroupType difference, IUnresolvedUnitInstance? defaultUnit, string? defaultUnitSymbol, bool? generateDocumentation,
        SharpMeasuresVectorGroupLocations locations)
        : base(unit, scalar, implementSum, implementDifference, difference, defaultUnit, defaultUnitSymbol, generateDocumentation, locations)
    {
        OriginalVectorGroup = originalVectorGroup;

        InheritDerivations = inheritDerivations;
        InheritConstants = inheritConstants;
        InheritConversions = inheritConversions;
        InheritUnits = inheritUnits;
    }
}
