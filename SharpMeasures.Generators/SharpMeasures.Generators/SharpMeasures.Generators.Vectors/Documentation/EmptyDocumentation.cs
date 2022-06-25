﻿namespace SharpMeasures.Generators.Vectors.Documentation;

using SharpMeasures.Generators.Units;
using SharpMeasures.Generators.Vectors.Refinement.VectorConstant;

using System;

internal class EmptyDocumentation : IDocumentationStrategy, IEquatable<EmptyDocumentation>
{
    public static EmptyDocumentation Instance { get; } = new();
    
    private EmptyDocumentation() { }

    string IDocumentationStrategy.Header() => string.Empty;
    string IDocumentationStrategy.Zero() => string.Empty;
    string IDocumentationStrategy.Constant(RefinedVectorConstantDefinition _) => string.Empty;
    string IDocumentationStrategy.WithScalarComponents() => string.Empty;
    string IDocumentationStrategy.WithVectorComponents() => string.Empty;
    string IDocumentationStrategy.ComponentsConstructor() => string.Empty;
    string IDocumentationStrategy.ScalarsConstructor() => string.Empty;
    string IDocumentationStrategy.VectorConstructor() => string.Empty;
    string IDocumentationStrategy.ScalarsAndUnitConstructor() => string.Empty;
    string IDocumentationStrategy.VectorAndUnitConstructor() => string.Empty;
    string IDocumentationStrategy.CastFromComponents() => string.Empty;
    string IDocumentationStrategy.Component(int _) => string.Empty;
    string IDocumentationStrategy.Components() => string.Empty;
    string IDocumentationStrategy.ComponentMagnitude(int _) => string.Empty;
    string IDocumentationStrategy.InUnit() => string.Empty;
    string IDocumentationStrategy.InConstantMultiples(RefinedVectorConstantDefinition _) => string.Empty;
    string IDocumentationStrategy.InSpecifiedUnit(UnitInstance _) => string.Empty;
    string IDocumentationStrategy.AsDimensionallyEquivalent(IVector _) => string.Empty;
    string IDocumentationStrategy.CastToDimensionallyEquivalent(IVector _) => string.Empty;
    string IDocumentationStrategy.IsNaN() => string.Empty;
    string IDocumentationStrategy.IsZero() => string.Empty;
    string IDocumentationStrategy.IsFinite() => string.Empty;
    string IDocumentationStrategy.IsInfinite() => string.Empty;
    string IDocumentationStrategy.Magnitude() => string.Empty;
    string IDocumentationStrategy.SquaredMagnitude() => string.Empty;
    string IDocumentationStrategy.ScalarMagnitude() => string.Empty;
    string IDocumentationStrategy.ScalarSquaredMagnitude() => string.Empty;
    string IDocumentationStrategy.Normalize() => string.Empty;
    string IDocumentationStrategy.Transform() => string.Empty;
    string IDocumentationStrategy.ToStringDocumentation() => string.Empty;
    string IDocumentationStrategy.EqualsSameTypeMethod() => string.Empty;
    string IDocumentationStrategy.EqualsObjectMethod() => string.Empty;
    string IDocumentationStrategy.EqualitySameTypeOperator() => string.Empty;
    string IDocumentationStrategy.InequalitySameTypeOperator() => string.Empty;
    string IDocumentationStrategy.GetHashCodeDocumentation() => string.Empty;
    string IDocumentationStrategy.Deconstruct() => string.Empty;
    string IDocumentationStrategy.UnaryPlusMethod() => string.Empty;
    string IDocumentationStrategy.NegateMethod() => string.Empty;
    string IDocumentationStrategy.AddSameTypeOperator() => string.Empty;
    string IDocumentationStrategy.SubtractSameTypeOperator() => string.Empty;
    string IDocumentationStrategy.AddDifferenceOperatorLHS() => string.Empty;
    string IDocumentationStrategy.AddDifferenceOperatorRHS() => string.Empty;
    string IDocumentationStrategy.SubtractDifferenceOperatorLHS() => string.Empty;
    string IDocumentationStrategy.AddSameTypeMethod() => string.Empty;
    string IDocumentationStrategy.SubtractSameTypeMethod() => string.Empty;
    string IDocumentationStrategy.SubtractFromSameTypeMethod() => string.Empty;
    string IDocumentationStrategy.AddDifferenceMethod() => string.Empty;
    string IDocumentationStrategy.SubtractDifferenceMethod() => string.Empty;
    string IDocumentationStrategy.MultiplyScalarMethod() => string.Empty;
    string IDocumentationStrategy.DivideScalarMethod() => string.Empty;
    string IDocumentationStrategy.UnaryPlusOperator() => string.Empty;
    string IDocumentationStrategy.NegateOperator() => string.Empty;
    string IDocumentationStrategy.MultiplyScalarOperatorLHS() => string.Empty;
    string IDocumentationStrategy.MultiplyScalarOperatorRHS() => string.Empty;
    string IDocumentationStrategy.DivideScalarOperatorLHS() => string.Empty;

    public virtual bool Equals(EmptyDocumentation? other)
    {
        return other is not null;
    }

    public override bool Equals(object? obj) => obj is EmptyDocumentation other && Equals(other);

    public static bool operator ==(EmptyDocumentation? lhs, EmptyDocumentation? rhs) => lhs?.Equals(rhs) ?? rhs is null;
    public static bool operator !=(EmptyDocumentation? lhs, EmptyDocumentation? rhs) => (lhs == rhs) is false;

    public override int GetHashCode() => 0;
}
