namespace SharpMeasures.Generators.Unresolved.Scalars;

using SharpMeasures.Generators.Scalars;

public interface IUnresolvedSpecializedScalarType : IUnresolvedScalarType
{
    new public abstract ISpecializedScalarType UnresolvedTarget { get; }

    new public abstract IUnresolvedSpecializedScalar ScalarDefinition { get; }
}
