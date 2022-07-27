namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

internal class ScalarPopulation : IScalarPopulation
{
    public IReadOnlyDictionary<NamedType, IScalarType> Scalars => scalars;

    private ReadOnlyEquatableDictionary<NamedType, IScalarType> scalars { get; }

    IReadOnlyDictionary<NamedType, IQuantityType> IQuantityPopulation.Quantities => Scalars.Transform(static (scalar) => scalar as IQuantityType);

    public ScalarPopulation(IReadOnlyDictionary<NamedType, IScalarType> scalars)
    {
        this.scalars = scalars.AsReadOnlyEquatable();
    }
}
