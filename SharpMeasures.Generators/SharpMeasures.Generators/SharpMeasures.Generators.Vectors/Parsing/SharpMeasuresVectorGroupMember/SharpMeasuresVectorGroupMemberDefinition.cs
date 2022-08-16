namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroupMember;

using SharpMeasures.Generators.Raw.Scalars;
using SharpMeasures.Generators.Raw.Units;
using SharpMeasures.Generators.Raw.Units.UnitInstances;
using SharpMeasures.Generators.Raw.Vectors.Groups;
using SharpMeasures.Generators.Vectors.Groups;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;

internal record class SharpMeasuresVectorGroupMemberDefinition : SharpMeasuresVectorDefinition, IVectorGroupMember
{
    public IRawVectorGroupType VectorGroup { get; }

    public SharpMeasuresVectorGroupMemberDefinition(IRawVectorGroupType vectorGroup, IRawUnitType unit, IRawScalarType? scalar, int dimension,
        bool implementSum, bool implementDifference, IRawVectorGroupType difference, IRawUnitInstance? defaultUnit, string? defaultUnitSymbol,
        bool? generateDocumentation, SharpMeasuresVectorLocations locations)
        : base(unit, scalar, dimension, implementSum, implementDifference, difference, defaultUnit, defaultUnitSymbol, generateDocumentation, locations)
    {
        VectorGroup = vectorGroup;
    }
}
