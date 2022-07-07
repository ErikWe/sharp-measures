namespace SharpMeasures.Generators.Scalars;

public interface ISpecializedScalarType : IScalarType
{
    new public abstract ISpecializedScalar ScalarDefinition { get; }
}
