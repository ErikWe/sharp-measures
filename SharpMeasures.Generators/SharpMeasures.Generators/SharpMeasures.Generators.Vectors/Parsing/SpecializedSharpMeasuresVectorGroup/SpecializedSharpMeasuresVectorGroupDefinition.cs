namespace SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVectorGroup;

using SharpMeasures.Generators.Raw.Scalars;
using SharpMeasures.Generators.Raw.Units;
using SharpMeasures.Generators.Raw.Units.UnitInstances;
using SharpMeasures.Generators.Raw.Vectors.Groups;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroup;

internal record class SpecializedSharpMeasuresVectorGroupDefinition : SharpMeasuresVectorGroupDefinition, IVectorGroupSpecialization
{
    public IRawVectorGroupType OriginalVectorGroup { get; }

    public bool InheritDerivations { get; }
    public bool InheritConstants { get; }
    public bool InheritConversions { get; }
    public bool InheritUnits { get; }

    public SpecializedSharpMeasuresVectorGroupDefinition(IRawVectorGroupType originalVectorGroup, bool inheritDerivations, bool inheritConstants,
        bool inheritConversions, bool inheritUnits, IRawUnitType unit, IRawScalarType? scalar, bool implementSum, bool implementDifference,
        IRawVectorGroupType difference, IRawUnitInstance? defaultUnit, string? defaultUnitSymbol, bool? generateDocumentation,
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
