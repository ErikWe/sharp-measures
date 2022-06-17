namespace SharpMeasures.Generators.Units.Documentation;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal interface IDocumentationStrategy
{
    public abstract string Header();

    public abstract string Derivation(DerivableSignature signature);
    public abstract string Definition(IUnitDefinition definition);

    public abstract string RepresentedQuantity();
    public abstract string Bias();

    public abstract string Constructor();

    public abstract string ScaledBy();
    public abstract string WithBias();
    public abstract string WithPrefix();

    public abstract string ToStringDocumentation();

    public abstract string EqualsSameTypeMethod();
    public abstract string EqualsObjectMethod();

    public abstract string EqualitySameTypeOperator();
    public abstract string InequalitySameTypeOperator();

    public abstract string GetHashCodeDocumentation();

    public abstract string CompareToSameType();

    public abstract string LessThanSameType();
    public abstract string GreaterThanSameType();
    public abstract string LessThanOrEqualSameType();
    public abstract string GreaterThanOrEqualSameType();
}
