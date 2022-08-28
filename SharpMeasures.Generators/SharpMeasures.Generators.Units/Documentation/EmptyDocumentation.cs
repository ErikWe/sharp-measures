namespace SharpMeasures.Generators.Units.Documentation;

using SharpMeasures.Generators.Units.UnitInstances;

using System;
using System.Collections.Generic;

internal class EmptyDocumentation : IDocumentationStrategy, IEquatable<EmptyDocumentation>
{
    public static EmptyDocumentation Instance { get; } = new();

    private EmptyDocumentation() { }

    string IDocumentationStrategy.Header() => string.Empty;
    string IDocumentationStrategy.Derivation(IReadOnlyList<NamedType> _) => string.Empty;
    string IDocumentationStrategy.FixedDefinition(IFixedUnit _) => string.Empty;
    string IDocumentationStrategy.DerivedDefinition(IDerivedUnit _) => string.Empty;
    string IDocumentationStrategy.AliasDefinition(IUnitAlias _) => string.Empty;
    string IDocumentationStrategy.BiasedDefinition(IBiasedUnit _) => string.Empty;
    string IDocumentationStrategy.PrefixedDefinition(IPrefixedUnit _) => string.Empty;
    string IDocumentationStrategy.ScaledDefinition(IScaledUnit _) => string.Empty;
    string IDocumentationStrategy.RepresentedQuantity() => string.Empty;
    string IDocumentationStrategy.Bias() => string.Empty;
    string IDocumentationStrategy.Constructor() => string.Empty;
    string IDocumentationStrategy.ScaledBy() => string.Empty;
    string IDocumentationStrategy.WithBias() => string.Empty;
    string IDocumentationStrategy.WithPrefix() => string.Empty;
    string IDocumentationStrategy.ToStringDocumentation() => string.Empty;
    string IDocumentationStrategy.EqualsSameTypeMethod() => string.Empty;
    string IDocumentationStrategy.EqualsObjectMethod() => string.Empty;
    string IDocumentationStrategy.EqualitySameTypeOperator() => string.Empty;
    string IDocumentationStrategy.InequalitySameTypeOperator() => string.Empty;
    string IDocumentationStrategy.GetHashCodeDocumentation() => string.Empty;
    string IDocumentationStrategy.CompareToSameType() => string.Empty;
    string IDocumentationStrategy.LessThanSameType() => string.Empty;
    string IDocumentationStrategy.GreaterThanSameType() => string.Empty;
    string IDocumentationStrategy.LessThanOrEqualSameType() => string.Empty;
    string IDocumentationStrategy.GreaterThanOrEqualSameType() => string.Empty;

    public virtual bool Equals(EmptyDocumentation? other)
    {
        return other is not null;
    }

    public override bool Equals(object? obj) => obj is EmptyDocumentation other && Equals(other);

    public static bool operator ==(EmptyDocumentation? lhs, EmptyDocumentation? rhs) => lhs?.Equals(rhs) ?? rhs is null;
    public static bool operator !=(EmptyDocumentation? lhs, EmptyDocumentation? rhs) => (lhs == rhs) is false;

    public override int GetHashCode() => 0;
}
