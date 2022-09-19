namespace SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;

public sealed record class OperatorDerivation
{
    public NamedType Result { get; }

    public OperatorType OperatorType { get; }

    public NamedType LeftHandSide { get; }
    public NamedType RightHandSide { get; }

    public OperatorDerivation(NamedType result, OperatorType operatorType, NamedType leftHandSide, NamedType rightHandSide)
    {
        Result = result;

        OperatorType = operatorType;

        LeftHandSide = leftHandSide;
        RightHandSide = rightHandSide;
    }
}
