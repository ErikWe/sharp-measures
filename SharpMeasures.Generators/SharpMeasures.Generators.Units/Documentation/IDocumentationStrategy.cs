namespace SharpMeasures.Generators.Units.Documentation;

using System.Collections.Generic;

internal interface IDocumentationStrategy
{
    public abstract string Header();

    public abstract string Derivation(IReadOnlyList<NamedType> signature);
    public abstract string FixedUnitInstance(IFixedUnitInstance unitInstance);
    public abstract string DerivedUnitInstance(IDerivedUnitInstance unitInstance);
    public abstract string UnitAliasInstance(IUnitInstanceAlias unitInstance);
    public abstract string BiasedUnitInstance(IBiasedUnitInstance unitInstance);
    public abstract string PrefixedUnitInstance(IPrefixedUnitInstance unitInstance);
    public abstract string ScaledUnitInstance(IScaledUnitInstance unitInstance);

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
