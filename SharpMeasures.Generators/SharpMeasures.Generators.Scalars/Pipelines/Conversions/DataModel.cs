namespace SharpMeasures.Generators.Scalars.Pipelines.Conversions;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Scalars.Documentation;

using System.Collections.Generic;

internal readonly record struct DataModel
{
    public DefinedType Scalar { get; }

    public IReadOnlyList<IConvertibleQuantity> Conversions { get; }
    public IReadOnlyList<IConvertibleQuantity> InheritedConversions { get; }

    public ConversionOperatorBehaviour SpecializationForwardsBehaviour { get; }
    public ConversionOperatorBehaviour SpecializationBackwardsBehaviour { get; }

    public IResolvedScalarPopulation ScalarPopulation { get; }

    public IDocumentationStrategy Documentation { get; }

    public DataModel(DefinedType scalar, IReadOnlyList<IConvertibleQuantity> conversions, IReadOnlyList<IConvertibleQuantity> inheritedConversions, ConversionOperatorBehaviour specializationForwardsBehaviour, ConversionOperatorBehaviour specializationBackwardsBehaviour, IResolvedScalarPopulation scalarPopulation, IDocumentationStrategy documentation)
    {
        Scalar = scalar;

        Conversions = conversions.AsReadOnlyEquatable();
        InheritedConversions = inheritedConversions.AsReadOnlyEquatable();

        SpecializationForwardsBehaviour = specializationForwardsBehaviour;
        SpecializationBackwardsBehaviour = specializationBackwardsBehaviour;

        ScalarPopulation = scalarPopulation;

        Documentation = documentation;
    }
}
