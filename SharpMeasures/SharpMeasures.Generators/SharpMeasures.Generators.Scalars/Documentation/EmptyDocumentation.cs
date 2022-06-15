namespace SharpMeasures.Generators.Scalars.Documentation;

using SharpMeasures.Generators.Scalars.Refinement.ScalarConstant;
using SharpMeasures.Generators.Units;

using System;

internal class EmptyDocumentation : IDocumentationStrategy, IEquatable<EmptyDocumentation>
{
    public static EmptyDocumentation Instance { get; } = new();

    private EmptyDocumentation() { }

    string IDocumentationStrategy.Header() => string.Empty;
    string IDocumentationStrategy.Zero() => string.Empty;
    string IDocumentationStrategy.Constant(RefinedScalarConstantDefinition _) => string.Empty;
    string IDocumentationStrategy.UnitBase(UnitInstance _) => string.Empty;
    string IDocumentationStrategy.WithMagnitude() => string.Empty;
    string IDocumentationStrategy.FromReciprocal() => string.Empty;
    string IDocumentationStrategy.FromSquare() => string.Empty;
    string IDocumentationStrategy.FromCube() => string.Empty;
    string IDocumentationStrategy.FromSquareRoot() => string.Empty;
    string IDocumentationStrategy.FromCubeRoot() => string.Empty;
    string IDocumentationStrategy.Magnitude() => string.Empty;
    string IDocumentationStrategy.ScalarConstructor() => string.Empty;
    string IDocumentationStrategy.ScalarAndUnitConstructor() => string.Empty;
    string IDocumentationStrategy.InUnit() => string.Empty;
    string IDocumentationStrategy.InConstantMultiples(RefinedScalarConstantDefinition _) => string.Empty;
    string IDocumentationStrategy.InSpecifiedUnit(UnitInstance _) => string.Empty;
    string IDocumentationStrategy.AsDimensionallyEquivalent(ScalarInterface _) => string.Empty;
    string IDocumentationStrategy.CastToDimensionallyEquivalent(ScalarInterface _) => string.Empty;
    string IDocumentationStrategy.IsNaN() => string.Empty;
    string IDocumentationStrategy.IsZero() => string.Empty;
    string IDocumentationStrategy.IsPositive() => string.Empty;
    string IDocumentationStrategy.IsNegative() => string.Empty;
    string IDocumentationStrategy.IsFinite() => string.Empty;
    string IDocumentationStrategy.IsInfinity() => string.Empty;
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
    string IDocumentationStrategy.UnaryPlusMethod() => string.Empty;
    string IDocumentationStrategy.NegateMethod() => string.Empty;
    string IDocumentationStrategy.MultiplyScalarMethod() => string.Empty;
    string IDocumentationStrategy.DivideScalarMethod() => string.Empty;
    string IDocumentationStrategy.MultiplySameTypeMethod() => string.Empty;
    string IDocumentationStrategy.DivideSameTypeMethod() => string.Empty;
    string IDocumentationStrategy.MultiplyGenericScalarToUnhandledMethod() => string.Empty;
    string IDocumentationStrategy.DivideGenericScalarToUnhandledMethod() => string.Empty;
    string IDocumentationStrategy.MultiplyGenericScalarMethod() => string.Empty;
    string IDocumentationStrategy.DivideGenericScalarMethod() => string.Empty;
    string IDocumentationStrategy.MultiplyVectorMethod(int dimension) => string.Empty;
    string IDocumentationStrategy.UnaryPlusOperator() => string.Empty;
    string IDocumentationStrategy.NegateOperator() => string.Empty;
    string IDocumentationStrategy.MultiplyScalarOperatorLHS() => string.Empty;
    string IDocumentationStrategy.MultiplyScalarOperatorRHS() => string.Empty;
    string IDocumentationStrategy.DivideScalarOperatorLHS() => string.Empty;
    string IDocumentationStrategy.DivideScalarOperatorRHS() => string.Empty;
    string IDocumentationStrategy.MultiplySameTypeOperator() => string.Empty;
    string IDocumentationStrategy.DivideSameTypeOperator() => string.Empty;
    string IDocumentationStrategy.MultiplyIScalarOperator() => string.Empty;
    string IDocumentationStrategy.DivideIScalarOperator() => string.Empty;
    string IDocumentationStrategy.MultiplyVectorOperatorLHS(int _) => string.Empty;
    string IDocumentationStrategy.MultiplyVectorOperatorRHS(int _) => string.Empty;
    string IDocumentationStrategy.DivideVectorOperatorRHS(int _) => string.Empty;
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
