﻿namespace SharpMeasures.Generators.Scalars.Documentation;

using SharpMeasures.Generators.Quantities.Parsing.DerivedQuantity;
using SharpMeasures.Generators.Units;

using System;
using System.Collections.Generic;

internal sealed class EmptyDocumentation : IDocumentationStrategy, IEquatable<EmptyDocumentation>
{
    public static EmptyDocumentation Instance { get; } = new();

    private EmptyDocumentation() { }

    string IDocumentationStrategy.Header() => string.Empty;
    string IDocumentationStrategy.Zero() => string.Empty;
    string IDocumentationStrategy.Constant(IScalarConstant _) => string.Empty;
    string IDocumentationStrategy.UnitBase(IUnitInstance _) => string.Empty;
    string IDocumentationStrategy.WithMagnitude() => string.Empty;
    string IDocumentationStrategy.FromReciprocal(NamedType _) => string.Empty;
    string IDocumentationStrategy.FromSquare(NamedType _) => string.Empty;
    string IDocumentationStrategy.FromCube(NamedType _) => string.Empty;
    string IDocumentationStrategy.FromSquareRoot(NamedType _) => string.Empty;
    string IDocumentationStrategy.FromCubeRoot(NamedType _) => string.Empty;
    string IDocumentationStrategy.Magnitude() => string.Empty;
    string IDocumentationStrategy.ScalarConstructor() => string.Empty;
    string IDocumentationStrategy.ScalarAndUnitConstructor() => string.Empty;
    string IDocumentationStrategy.InUnit() => string.Empty;
    string IDocumentationStrategy.InConstantMultiples(IScalarConstant _) => string.Empty;
    string IDocumentationStrategy.InSpecifiedUnit(IUnitInstance _) => string.Empty;
    string IDocumentationStrategy.Conversion(NamedType _) => string.Empty;
    string IDocumentationStrategy.AntidirectionalConversion(NamedType _) => string.Empty;
    string IDocumentationStrategy.CastConversion(NamedType _) => string.Empty;
    string IDocumentationStrategy.AntidirectionalCastConversion(NamedType _) => string.Empty;
    string IDocumentationStrategy.Derivation(DerivedQuantitySignature _, IReadOnlyList<string> _2) => string.Empty;
    string IDocumentationStrategy.OperatorDerivation(OperatorDerivation _) => string.Empty;
    string IDocumentationStrategy.IsNaN() => string.Empty;
    string IDocumentationStrategy.IsZero() => string.Empty;
    string IDocumentationStrategy.IsPositive() => string.Empty;
    string IDocumentationStrategy.IsNegative() => string.Empty;
    string IDocumentationStrategy.IsFinite() => string.Empty;
    string IDocumentationStrategy.IsInfinite() => string.Empty;
    string IDocumentationStrategy.IsPositiveInfinity() => string.Empty;
    string IDocumentationStrategy.IsNegativeInfinity() => string.Empty;
    string IDocumentationStrategy.Absolute() => string.Empty;
    string IDocumentationStrategy.Sign() => string.Empty;
    string IDocumentationStrategy.Reciprocal() => string.Empty;
    string IDocumentationStrategy.Square() => string.Empty;
    string IDocumentationStrategy.Cube() => string.Empty;
    string IDocumentationStrategy.SquareRoot() => string.Empty;
    string IDocumentationStrategy.CubeRoot() => string.Empty;
    string IDocumentationStrategy.ToStringDocumentation() => string.Empty;
    string IDocumentationStrategy.EqualsSameTypeMethod() => string.Empty;
    string IDocumentationStrategy.EqualsObjectMethod() => string.Empty;
    string IDocumentationStrategy.EqualitySameTypeOperator() => string.Empty;
    string IDocumentationStrategy.InequalitySameTypeOperator() => string.Empty;
    string IDocumentationStrategy.GetHashCodeDocumentation() => string.Empty;
    string IDocumentationStrategy.UnaryPlusMethod() => string.Empty;
    string IDocumentationStrategy.NegateMethod() => string.Empty;
    string IDocumentationStrategy.AddSameTypeMethod() => string.Empty;
    string IDocumentationStrategy.SubtractSameTypeMethod() => string.Empty;
    string IDocumentationStrategy.AddDifferenceMethod() => string.Empty;
    string IDocumentationStrategy.SubtractDifferenceMethod() => string.Empty;
    string IDocumentationStrategy.MultiplyScalarMethod() => string.Empty;
    string IDocumentationStrategy.DivideScalarMethod() => string.Empty;
    string IDocumentationStrategy.MultiplySameTypeMethod() => string.Empty;
    string IDocumentationStrategy.DivideSameTypeMethod() => string.Empty;
    string IDocumentationStrategy.MultiplyVectorMethod(int dimension) => string.Empty;
    string IDocumentationStrategy.UnaryPlusOperator() => string.Empty;
    string IDocumentationStrategy.NegateOperator() => string.Empty;
    string IDocumentationStrategy.AddSameTypeOperator() => string.Empty;
    string IDocumentationStrategy.SubtractSameTypeOperator() => string.Empty;
    string IDocumentationStrategy.AddDifferenceOperatorLHS() => string.Empty;
    string IDocumentationStrategy.AddDifferenceOperatorRHS() => string.Empty;
    string IDocumentationStrategy.SubtractDifferenceOperatorLHS() => string.Empty;
    string IDocumentationStrategy.MultiplyScalarOperatorLHS() => string.Empty;
    string IDocumentationStrategy.MultiplyScalarOperatorRHS() => string.Empty;
    string IDocumentationStrategy.DivideScalarOperatorLHS() => string.Empty;
    string IDocumentationStrategy.DivideScalarOperatorRHS() => string.Empty;
    string IDocumentationStrategy.MultiplySameTypeOperator() => string.Empty;
    string IDocumentationStrategy.DivideSameTypeOperator() => string.Empty;
    string IDocumentationStrategy.MultiplyVectorOperatorLHS(int _) => string.Empty;
    string IDocumentationStrategy.MultiplyVectorOperatorRHS(int _) => string.Empty;
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
