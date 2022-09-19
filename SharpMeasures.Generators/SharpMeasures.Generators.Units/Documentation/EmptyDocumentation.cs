namespace SharpMeasures.Generators.Units.Documentation;

using System;
using System.Collections.Generic;

internal sealed class EmptyDocumentation : IDocumentationStrategy, IEquatable<EmptyDocumentation>
{
    public static EmptyDocumentation Instance { get; } = new();

    private EmptyDocumentation() { }

    string IDocumentationStrategy.Header() => string.Empty;
    string IDocumentationStrategy.Derivation(IReadOnlyList<NamedType> _) => string.Empty;
    string IDocumentationStrategy.FixedUnitInstance(IFixedUnitInstance _) => string.Empty;
    string IDocumentationStrategy.DerivedUnitInstance(IDerivedUnitInstance _) => string.Empty;
    string IDocumentationStrategy.UnitAliasInstance(IUnitInstanceAlias _) => string.Empty;
    string IDocumentationStrategy.BiasedUnitInstance(IBiasedUnitInstance _) => string.Empty;
    string IDocumentationStrategy.PrefixedUnitInstance(IPrefixedUnitInstance _) => string.Empty;
    string IDocumentationStrategy.ScaledUnitInstance(IScaledUnitInstance _) => string.Empty;
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

    public bool Equals(EmptyDocumentation? other) => other is not null;
    public override bool Equals(object? obj) => obj is EmptyDocumentation other && Equals(other);

    public static bool operator ==(EmptyDocumentation? lhs, EmptyDocumentation? rhs) => lhs?.Equals(rhs) ?? rhs is null;
    public static bool operator !=(EmptyDocumentation? lhs, EmptyDocumentation? rhs) => (lhs == rhs) is false;

    public override int GetHashCode() => 0;
}
