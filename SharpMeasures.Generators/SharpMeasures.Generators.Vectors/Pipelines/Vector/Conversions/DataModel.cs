namespace SharpMeasures.Generators.Vectors.Pipelines.Vector.Conversions;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Vectors.Documentation;

using System.Collections.Generic;

internal readonly record struct DataModel
{
    public DefinedType Vector { get; }
    public int Dimension { get; }
    public NamedType? Group { get; }

    public IReadOnlyList<IConvertibleQuantity> Conversions { get; }
    public IReadOnlyList<IConvertibleQuantity> InheritedConversions { get; }

    public ConversionOperatorBehaviour SpecializationForwardsBehaviour { get; }
    public ConversionOperatorBehaviour SpecializationBackwardsBehaviour { get; }

    public IResolvedVectorPopulation VectorPopulation { get; }

    public IVectorDocumentationStrategy Documentation { get; }

    public DataModel(DefinedType vector, int dimension, NamedType? group, IReadOnlyList<IConvertibleQuantity> conversions, IReadOnlyList<IConvertibleQuantity> inheritedConversions, ConversionOperatorBehaviour specializationForwardBehaviour, ConversionOperatorBehaviour specializationBackwardBehaviour,
        IResolvedVectorPopulation vectorPopulation, IVectorDocumentationStrategy documentation)
    {
        Vector = vector;
        Dimension = dimension;
        Group = group;

        Conversions = conversions.AsReadOnlyEquatable();
        InheritedConversions = inheritedConversions.AsReadOnlyEquatable();

        SpecializationForwardsBehaviour = specializationForwardBehaviour;
        SpecializationBackwardsBehaviour = specializationBackwardBehaviour;

        VectorPopulation = vectorPopulation;

        Documentation = documentation;
    }
}
