namespace SharpMeasures.Generators;

using SharpMeasures.Generators.Parsing.Scalars;
using SharpMeasures.Generators.Parsing.Units;
using SharpMeasures.Generators.Parsing.Vectors;

internal readonly record struct SharpMeasuresPopulation
{
    public NamedTypePopulation<UnitInterface> Units { get; }
    public NamedTypePopulation<ScalarInterface> Scalars { get; }
    public NamedTypePopulation<VectorInterface> Vectors { get; }

    public SharpMeasuresPopulation(NamedTypePopulation<UnitInterface> units, NamedTypePopulation<ScalarInterface> scalars, NamedTypePopulation<VectorInterface> vectors)
    {
        Units = units;
        Scalars = scalars;
        Vectors = vectors;
    }
}
