namespace SharpMeasures.Generators.Scalars;

using SharpMeasures.Equatables;
using SharpMeasures.Generators.Quantities;

using System.Collections.Generic;

public interface IScalarPopulation : IQuantityPopulation
{
    public abstract IReadOnlyDictionary<NamedType, IScalarType> Scalars { get; }
    public abstract IReadOnlyDictionary<NamedType, IBaseScalarType> BaseScalarByScalarType { get; }
    public abstract IReadOnlyDictionary<NamedType, SpecializedScalarPopulation> SpecializationsByScalarType { get; }
}

public class SpecializedScalarPopulation : ReadOnlyEquatableDictionary<NamedType, ISpecializedScalarType>
{
    public SpecializedScalarPopulation(IReadOnlyDictionary<NamedType, ISpecializedScalarType> items) : base(items) { }
}
