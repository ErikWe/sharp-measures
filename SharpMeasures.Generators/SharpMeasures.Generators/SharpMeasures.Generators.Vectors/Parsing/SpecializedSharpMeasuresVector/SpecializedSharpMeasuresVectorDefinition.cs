namespace SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVector;

using SharpMeasures.Generators.Raw.Scalars;
using SharpMeasures.Generators.Raw.Units;
using SharpMeasures.Generators.Raw.Units.UnitInstances;
using SharpMeasures.Generators.Raw.Vectors;
using SharpMeasures.Generators.Raw.Vectors.Groups;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;

internal record class SpecializedSharpMeasuresVectorDefinition : SharpMeasuresVectorDefinition, IIndividualVectorSpecialization
{
    public IRawVectorType OriginalIndividualVector { get; }

    public bool InheritDerivations { get; }
    public bool InheritConstants { get; }
    public bool InheritConversions { get; }
    public bool InheritUnits { get; }

    public SpecializedSharpMeasuresVectorDefinition(IRawVectorType originalIndividualVector, bool inheritDerivations, bool inheritConstants,
        bool inheritConversions, bool inheritUnits, IRawUnitType unit, IRawScalarType? scalar, int dimension, bool implementSum, bool implementDifference,
        IRawVectorGroupType difference, IRawUnitInstance? defaultUnit, string? defaultUnitSymbol, bool? generateDocumentation,
        SharpMeasuresVectorLocations locations)
        : base(unit, scalar, dimension, implementSum, implementDifference, difference, defaultUnit, defaultUnitSymbol, generateDocumentation, locations)
    {
        OriginalIndividualVector = originalIndividualVector;

        InheritDerivations = inheritDerivations;
        InheritConstants = inheritConstants;
        InheritConversions = inheritConversions;
        InheritUnits = inheritUnits;
    }
}
