namespace SharpMeasures.Generators.Vectors.Parsing.SpecializedSharpMeasuresVector;

using SharpMeasures.Generators.Unresolved.Scalars;
using SharpMeasures.Generators.Unresolved.Units;
using SharpMeasures.Generators.Unresolved.Units.UnitInstances;
using SharpMeasures.Generators.Unresolved.Vectors;
using SharpMeasures.Generators.Vectors.Parsing.Abstraction;
using SharpMeasures.Generators.Vectors.Parsing.SharpMeasuresVector;

internal record class SpecializedSharpMeasuresVectorDefinition : SharpMeasuresVectorDefinition, IIndividualVectorSpecialization
{
    public IUnresolvedIndividualVectorType OriginalIndividualVector { get; }

    public bool InheritDerivations { get; }
    public bool InheritConstants { get; }
    public bool InheritConversions { get; }
    public bool InheritUnits { get; }

    public SpecializedSharpMeasuresVectorDefinition(IUnresolvedIndividualVectorType originalIndividualVector, bool inheritDerivations, bool inheritConstants,
        bool inheritConversions, bool inheritUnits, IUnresolvedUnitType unit, IUnresolvedScalarType? scalar, int dimension, bool implementSum, bool implementDifference,
        IUnresolvedVectorGroupType difference, IUnresolvedUnitInstance? defaultUnit, string? defaultUnitSymbol, bool? generateDocumentation,
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
