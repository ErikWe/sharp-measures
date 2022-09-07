namespace SharpMeasures.Generators.Quantities;

public interface IOperatorDerivation
{
    public abstract NamedType Result { get; }

    public abstract OperatorType OperatorType { get; }

    public abstract NamedType LeftHandSide { get; }
    public abstract NamedType RightHandSide { get; }
}
