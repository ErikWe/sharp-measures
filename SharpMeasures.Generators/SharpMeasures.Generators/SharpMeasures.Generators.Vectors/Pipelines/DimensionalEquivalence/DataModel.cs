namespace SharpMeasures.Generators.Vectors.Pipelines.DimensionalEquivalence;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Utility;
using SharpMeasures.Generators.Vectors.Documentation;

using System.Collections.Generic;

internal readonly record struct DataModel(DefinedType Vector, int Dimension,
    ReadOnlyEquatableDictionary<ResizedGroup, ConversionOperatorBehaviour> DimensionalEquivalences, IDocumentationStrategy Documentation)
{
    public DataModel(DefinedType vector, int dimension, IReadOnlyDictionary<ResizedGroup, ConversionOperatorBehaviour> dimensionalEquivalences,
        IDocumentationStrategy documentation)
        : this(vector, dimension, dimensionalEquivalences.AsReadOnlyEquatable(), documentation) { }
}
