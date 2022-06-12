namespace SharpMeasures.Generators.Vectors.Pipelines.DimensionalEquivalence;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Documentation;
using SharpMeasures.Generators.Quantities.Utility;

using System.Collections.Generic;

internal readonly record struct DataModel(DefinedType Vector, int Dimension,
    ReadOnlyEquatableDictionary<ResizedVectorGroup, ConversionOperationBehaviour> DimensionalEquivalences, DocumentationFile Documentation)
{
    public DataModel(DefinedType vector, int dimension, IReadOnlyDictionary<ResizedVectorGroup, ConversionOperationBehaviour> dimensionalEquivalences,
        DocumentationFile documentation)
        : this(vector, dimension, dimensionalEquivalences.AsReadOnlyEquatable(), documentation) { }
}
