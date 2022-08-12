namespace SharpMeasures.Generators.Vectors.Documentation;

using SharpMeasures.Generators.Unresolved.Units.UnitInstances;
using SharpMeasures.Generators.Unresolved.Vectors;

using System;

internal class EmptyDocumentation : IVectorGroupDocumentationStrategy, IIndividualVectorDocumentationStrategy, IEquatable<EmptyDocumentation>
{
    public static EmptyDocumentation Instance { get; } = new();
    
    private EmptyDocumentation() { }

    string IIndividualVectorDocumentationStrategy.Header() => string.Empty;
    string IIndividualVectorDocumentationStrategy.Zero() => string.Empty;
    string IIndividualVectorDocumentationStrategy.Constant(IVectorConstant _) => string.Empty;
    string IIndividualVectorDocumentationStrategy.WithScalarComponents() => string.Empty;
    string IIndividualVectorDocumentationStrategy.WithVectorComponents() => string.Empty;
    string IIndividualVectorDocumentationStrategy.ComponentsConstructor() => string.Empty;
    string IIndividualVectorDocumentationStrategy.ScalarsConstructor() => string.Empty;
    string IIndividualVectorDocumentationStrategy.VectorConstructor() => string.Empty;
    string IIndividualVectorDocumentationStrategy.ScalarsAndUnitConstructor() => string.Empty;
    string IIndividualVectorDocumentationStrategy.VectorAndUnitConstructor() => string.Empty;
    string IIndividualVectorDocumentationStrategy.CastFromComponents() => string.Empty;
    string IIndividualVectorDocumentationStrategy.Component(int _) => string.Empty;
    string IIndividualVectorDocumentationStrategy.Components() => string.Empty;
    string IIndividualVectorDocumentationStrategy.ComponentMagnitude(int _) => string.Empty;
    string IIndividualVectorDocumentationStrategy.InUnit() => string.Empty;
    string IIndividualVectorDocumentationStrategy.InConstantMultiples(IVectorConstant _) => string.Empty;
    string IIndividualVectorDocumentationStrategy.InSpecifiedUnit(IUnresolvedUnitInstance _) => string.Empty;
    string IIndividualVectorDocumentationStrategy.Conversion(IUnresolvedVectorGroupMemberType _) => string.Empty;
    string IIndividualVectorDocumentationStrategy.CastConversion(IUnresolvedVectorGroupMemberType _) => string.Empty;
    string IIndividualVectorDocumentationStrategy.IsNaN() => string.Empty;
    string IIndividualVectorDocumentationStrategy.IsZero() => string.Empty;
    string IIndividualVectorDocumentationStrategy.IsFinite() => string.Empty;
    string IIndividualVectorDocumentationStrategy.IsInfinite() => string.Empty;
    string IIndividualVectorDocumentationStrategy.Magnitude() => string.Empty;
    string IIndividualVectorDocumentationStrategy.SquaredMagnitude() => string.Empty;
    string IIndividualVectorDocumentationStrategy.ScalarMagnitude() => string.Empty;
    string IIndividualVectorDocumentationStrategy.ScalarSquaredMagnitude() => string.Empty;
    string IIndividualVectorDocumentationStrategy.Normalize() => string.Empty;
    string IIndividualVectorDocumentationStrategy.Transform() => string.Empty;
    string IIndividualVectorDocumentationStrategy.ToStringDocumentation() => string.Empty;
    string IIndividualVectorDocumentationStrategy.EqualsSameTypeMethod() => string.Empty;
    string IIndividualVectorDocumentationStrategy.EqualsObjectMethod() => string.Empty;
    string IIndividualVectorDocumentationStrategy.EqualitySameTypeOperator() => string.Empty;
    string IIndividualVectorDocumentationStrategy.InequalitySameTypeOperator() => string.Empty;
    string IIndividualVectorDocumentationStrategy.GetHashCodeDocumentation() => string.Empty;
    string IIndividualVectorDocumentationStrategy.Deconstruct() => string.Empty;
    string IIndividualVectorDocumentationStrategy.UnaryPlusMethod() => string.Empty;
    string IIndividualVectorDocumentationStrategy.NegateMethod() => string.Empty;
    string IIndividualVectorDocumentationStrategy.AddSameTypeOperator() => string.Empty;
    string IIndividualVectorDocumentationStrategy.SubtractSameTypeOperator() => string.Empty;
    string IIndividualVectorDocumentationStrategy.AddDifferenceOperatorLHS() => string.Empty;
    string IIndividualVectorDocumentationStrategy.AddDifferenceOperatorRHS() => string.Empty;
    string IIndividualVectorDocumentationStrategy.SubtractDifferenceOperatorLHS() => string.Empty;
    string IIndividualVectorDocumentationStrategy.AddSameTypeMethod() => string.Empty;
    string IIndividualVectorDocumentationStrategy.SubtractSameTypeMethod() => string.Empty;
    string IIndividualVectorDocumentationStrategy.AddDifferenceMethod() => string.Empty;
    string IIndividualVectorDocumentationStrategy.SubtractDifferenceMethod() => string.Empty;
    string IIndividualVectorDocumentationStrategy.MultiplyScalarMethod() => string.Empty;
    string IIndividualVectorDocumentationStrategy.DivideScalarMethod() => string.Empty;
    string IIndividualVectorDocumentationStrategy.UnaryPlusOperator() => string.Empty;
    string IIndividualVectorDocumentationStrategy.NegateOperator() => string.Empty;
    string IIndividualVectorDocumentationStrategy.MultiplyScalarOperatorLHS() => string.Empty;
    string IIndividualVectorDocumentationStrategy.MultiplyScalarOperatorRHS() => string.Empty;
    string IIndividualVectorDocumentationStrategy.DivideScalarOperatorLHS() => string.Empty;

    public virtual bool Equals(EmptyDocumentation? other)
    {
        return other is not null;
    }

    public override bool Equals(object? obj) => obj is EmptyDocumentation other && Equals(other);

    public static bool operator ==(EmptyDocumentation? lhs, EmptyDocumentation? rhs) => lhs?.Equals(rhs) ?? rhs is null;
    public static bool operator !=(EmptyDocumentation? lhs, EmptyDocumentation? rhs) => (lhs == rhs) is false;

    public override int GetHashCode() => 0;
}
