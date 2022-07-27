namespace SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVectorGroupMember;

using SharpMeasures.Generators.Unresolved.Scalars;
using SharpMeasures.Generators.Unresolved.Units;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;
using SharpMeasures.Generators.Unresolved.Vectors;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;

internal record class SharpMeasuresVectorGroupMemberDefinition : SharpMeasuresVectorDefinition, IVectorGroupMember
{
    public IUnresolvedVectorGroupType VectorGroup { get; }

    public SharpMeasuresVectorGroupMemberDefinition(IUnresolvedVectorGroupType vectorGroup, IUnresolvedUnitType unit, IUnresolvedScalarType? scalar, int dimension,
        bool implementSum, bool implementDifference, IUnresolvedVectorGroupType difference, IUnresolvedUnitInstance? defaultUnit, string? defaultUnitSymbol,
        bool? generateDocumentation, SharpMeasuresVectorLocations locations)
        : base(unit, scalar, dimension, implementSum, implementDifference, difference, defaultUnit, defaultUnitSymbol, generateDocumentation, locations)
    {
        VectorGroup = vectorGroup;
    }
}
