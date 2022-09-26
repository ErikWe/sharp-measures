namespace SharpMeasures.Generators.Vectors.Documentation;

using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Units;

using System;
using System.Collections.Generic;

internal sealed class EmptyDocumentation : IGroupDocumentationStrategy, IVectorDocumentationStrategy, IEquatable<EmptyDocumentation>
{
    public static EmptyDocumentation Instance { get; } = new();
    
    private EmptyDocumentation() { }

    string IVectorDocumentationStrategy.Header() => string.Empty;
    string IVectorDocumentationStrategy.Zero() => string.Empty;
    string IVectorDocumentationStrategy.Constant(IVectorConstant _) => string.Empty;
    string IVectorDocumentationStrategy.WithScalarComponents() => string.Empty;
    string IVectorDocumentationStrategy.WithVectorComponents() => string.Empty;
    string IVectorDocumentationStrategy.ComponentsConstructor() => string.Empty;
    string IVectorDocumentationStrategy.ScalarsConstructor() => string.Empty;
    string IVectorDocumentationStrategy.VectorConstructor() => string.Empty;
    string IVectorDocumentationStrategy.ScalarsAndUnitConstructor() => string.Empty;
    string IVectorDocumentationStrategy.VectorAndUnitConstructor() => string.Empty;
    string IVectorDocumentationStrategy.CastFromComponents() => string.Empty;
    string IVectorDocumentationStrategy.Component(int _) => string.Empty;
    string IVectorDocumentationStrategy.Components() => string.Empty;
    string IVectorDocumentationStrategy.ComponentMagnitude(int _) => string.Empty;
    string IVectorDocumentationStrategy.InUnit() => string.Empty;
    string IVectorDocumentationStrategy.InConstantMultiples(IVectorConstant _) => string.Empty;
    string IVectorDocumentationStrategy.InSpecifiedUnit(IUnitInstance _) => string.Empty;
    string IVectorDocumentationStrategy.Conversion(NamedType _) => string.Empty;
    string IVectorDocumentationStrategy.AntidirectionalConversion(NamedType _) => string.Empty;
    string IVectorDocumentationStrategy.CastConversion(NamedType _) => string.Empty;
    string IVectorDocumentationStrategy.AntidirectionalCastConversion(NamedType _) => string.Empty;
    string IVectorDocumentationStrategy.Derivation(DerivedQuantitySignature _, IReadOnlyList<string> _2) => string.Empty;
    string IVectorDocumentationStrategy.OperatorDerivation(OperatorDerivation _) => string.Empty;
    string IVectorDocumentationStrategy.IsNaN() => string.Empty;
    string IVectorDocumentationStrategy.IsZero() => string.Empty;
    string IVectorDocumentationStrategy.IsFinite() => string.Empty;
    string IVectorDocumentationStrategy.IsInfinite() => string.Empty;
    string IVectorDocumentationStrategy.Magnitude() => string.Empty;
    string IVectorDocumentationStrategy.ScalarMagnitude() => string.Empty;
    string IVectorDocumentationStrategy.ScalarSquaredMagnitude() => string.Empty;
    string IVectorDocumentationStrategy.Normalize() => string.Empty;
    string IVectorDocumentationStrategy.Transform() => string.Empty;
    string IVectorDocumentationStrategy.ToStringDocumentation() => string.Empty;
    string IVectorDocumentationStrategy.EqualsSameTypeMethod() => string.Empty;
    string IVectorDocumentationStrategy.EqualsObjectMethod() => string.Empty;
    string IVectorDocumentationStrategy.EqualitySameTypeOperator() => string.Empty;
    string IVectorDocumentationStrategy.InequalitySameTypeOperator() => string.Empty;
    string IVectorDocumentationStrategy.GetHashCodeDocumentation() => string.Empty;
    string IVectorDocumentationStrategy.Deconstruct() => string.Empty;
    string IVectorDocumentationStrategy.UnaryPlusMethod() => string.Empty;
    string IVectorDocumentationStrategy.NegateMethod() => string.Empty;
    string IVectorDocumentationStrategy.AddSameTypeOperator() => string.Empty;
    string IVectorDocumentationStrategy.SubtractSameTypeOperator() => string.Empty;
    string IVectorDocumentationStrategy.AddDifferenceOperatorLHS() => string.Empty;
    string IVectorDocumentationStrategy.AddDifferenceOperatorRHS() => string.Empty;
    string IVectorDocumentationStrategy.SubtractDifferenceOperatorLHS() => string.Empty;
    string IVectorDocumentationStrategy.AddSameTypeMethod() => string.Empty;
    string IVectorDocumentationStrategy.SubtractSameTypeMethod() => string.Empty;
    string IVectorDocumentationStrategy.AddDifferenceMethod() => string.Empty;
    string IVectorDocumentationStrategy.SubtractDifferenceMethod() => string.Empty;
    string IVectorDocumentationStrategy.MultiplyScalarMethod() => string.Empty;
    string IVectorDocumentationStrategy.DivideScalarMethod() => string.Empty;
    string IVectorDocumentationStrategy.UnaryPlusOperator() => string.Empty;
    string IVectorDocumentationStrategy.NegateOperator() => string.Empty;
    string IVectorDocumentationStrategy.MultiplyScalarOperatorLHS() => string.Empty;
    string IVectorDocumentationStrategy.MultiplyScalarOperatorRHS() => string.Empty;
    string IVectorDocumentationStrategy.DivideScalarOperatorLHS() => string.Empty;

    string IGroupDocumentationStrategy.Header() => string.Empty;
    string IGroupDocumentationStrategy.ScalarFactoryMethod(int _) => string.Empty;
    string IGroupDocumentationStrategy.VectorFactoryMethod(int _) => string.Empty;
    string IGroupDocumentationStrategy.ComponentsFactoryMethod(int _) => string.Empty;

    public bool Equals(EmptyDocumentation? other) => other is not null;
    public override bool Equals(object? obj) => obj is EmptyDocumentation other && Equals(other);

    public static bool operator ==(EmptyDocumentation? lhs, EmptyDocumentation? rhs) => lhs?.Equals(rhs) ?? rhs is null;
    public static bool operator !=(EmptyDocumentation? lhs, EmptyDocumentation? rhs) => (lhs == rhs) is false;

    public override int GetHashCode() => 0;
}
