namespace SharpMeasures.Generators.Vectors.Processing;

using SharpMeasures.Generators.Attributes.Parsing;
using SharpMeasures.Generators.Attributes.Parsing.Scalars;
using SharpMeasures.Generators.Diagnostics;
using SharpMeasures.Generators.Scalars.Processing.Diagnostics;
using SharpMeasures.Generators.Scalars;
using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors;

using System.Linq;

internal class GeneratedVectorProcessingContext : IProcessingContext
{
    public DefinedType Type { get; }

    public NamedTypePopulation<UnitInterface> UnitPopulation { get; }
    public NamedTypePopulation<ScalarInterface> ScalarPopulation { get; }

    public GeneratedVectorProcessingContext(DefinedType type, NamedTypePopulation<UnitInterface> unitPopulation, NamedTypePopulation<ScalarInterface> scalarPopulation)
    {
        Type = type;

        UnitPopulation = unitPopulation;
        ScalarPopulation = scalarPopulation;
    }
}

internal class GeneratedVectorProcesser
{

}
