namespace SharpMeasures.Generators.Vectors.Pipelines.DimensionalEquivalence;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities.Utility;
using SharpMeasures.Generators.Vectors.Documentation;

using System.Collections.Generic;

internal readonly record struct DataModel(DefinedType Vector, int Dimension,
    ReadOnlyEquatableDictionary<ResizedGroup, ConversionOperationBehaviour> DimensionalEquivalences, IDocumentationStrategy Documentation)
{
    public DataModel(DefinedType vector, int dimension, IReadOnlyDictionary<ResizedGroup, ConversionOperationBehaviour> dimensionalEquivalences,
        IDocumentationStrategy documentation)
        : this(vector, dimension, dimensionalEquivalences.AsReadOnlyEquatable(), documentation) { }
}
