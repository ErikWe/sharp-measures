namespace SharpMeasures.Generators.Units.Documentation;

using SharpMeasures.Generators.Units.Parsing.Abstractions;

using System;

internal class EmptyDocumentation : IDocumentationStrategy, IEquatable<EmptyDocumentation>
{
    public static EmptyDocumentation Instance { get; } = new();

    private EmptyDocumentation() { }

    string IDocumentationStrategy.Header() => string.Empty;
    string IDocumentationStrategy.Derivation(DerivableSignature _) => string.Empty;
    string IDocumentationStrategy.Definition(IUnitDefinition _) => string.Empty;
    string IDocumentationStrategy.RepresentedQuantity() => string.Empty;
    string IDocumentationStrategy.Offset() => string.Empty;
    string IDocumentationStrategy.Constructor() => string.Empty;
    string IDocumentationStrategy.ScaledBy() => string.Empty;
    string IDocumentationStrategy.OffsetBy() => string.Empty;
    string IDocumentationStrategy.WithPrefix() => string.Empty;
    string IDocumentationStrategy.ToStringDocumentation() => string.Empty;
    string IDocumentationStrategy.CompareToSameType() => string.Empty;
    string IDocumentationStrategy.LessThanSameType() => string.Empty;
    string IDocumentationStrategy.GreaterThanSameType() => string.Empty;
    string IDocumentationStrategy.LessThanOrEqualSameType() => string.Empty;
    string IDocumentationStrategy.GreaterThanOrEqualSameType() => string.Empty;

    public virtual bool Equals(EmptyDocumentation? other)
    {
        return other is not null;
    }

    public override bool Equals(object obj)
    {
        if (obj is EmptyDocumentation other)
        {
            return Equals(other);
        }

        return false;
    }

    public static bool operator ==(EmptyDocumentation? lhs, EmptyDocumentation? rhs) => lhs?.Equals(rhs) ?? rhs is null;
    public static bool operator !=(EmptyDocumentation? lhs, EmptyDocumentation? rhs) => (lhs == rhs) is false;

    public override int GetHashCode() => 0;
}
