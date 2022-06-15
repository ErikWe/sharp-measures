namespace SharpMeasures.Generators.Units.Documentation;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

internal interface IDocumentationStrategy
{
    public abstract string Header();

    public abstract string Derivation(DerivableSignature signature);
    public abstract string Definition(IUnitDefinition definition);

    public abstract string RepresentedQuantity();
    public abstract string Offset();

    public abstract string Constructor();

    public abstract string ScaledBy();
    public abstract string OffsetBy();
    public abstract string WithPrefix();

    public abstract string ToStringDocumentation();

    public abstract string CompareToSameType();

    public abstract string LessThanSameType();
    public abstract string GreaterThanSameType();
    public abstract string LessThanOrEqualSameType();
    public abstract string GreaterThanOrEqualSameType();
}
