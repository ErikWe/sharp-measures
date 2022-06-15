namespace SharpMeasures.Generators.Scalars.Documentation;

using SharpMeasures.Generators.Scalars.Refinement.ScalarConstant;
using SharpMeasures.Generators.Units;

using System;

internal class DocumentationTags : IDocumentationStrategy, IEquatable<DocumentationTags>
{
    public static DocumentationTags Instance { get; } = new();

    private DocumentationTags() { }

    public string Header() => "Header";

    public string Zero() => "Zero";
    public string Constant(RefinedScalarConstantDefinition definition) => $"Constant_{definition.Name}";
    public string UnitBase(UnitInstance unitInstance) => $"One_{unitInstance.Name}";

    public string WithMagnitude() => "WithMagnitude";

    public string FromReciprocal() => "FromReciprocal";
    public string FromSquare() => "FromSquare";
    public string FromCube() => "FromCube";
    public string FromSquareRoot() => "FromSquareRoot";
    public string FromCubeRoot() => "FromCubeRoot";

    public string ScalarConstructor() => "Constructor_Scalar";
    public string ScalarAndUnitConstructor() => "Constructor_Scalar_Unit";

    public string Magnitude() => "Magnitude";
    public string InUnit() => "InUnit";
    public string InConstantMultiples(RefinedScalarConstantDefinition definition) => $"InMultiplesOf_{definition.Name}";
    public string InSpecifiedUnit(UnitInstance unitInstance) => $"InUnit_{unitInstance.Name}";

    public string AsDimensionallyEquivalent(ScalarInterface scalar) => $"As_{scalar.ScalarType.Name}";
    public string CastToDimensionallyEquivalent(ScalarInterface scalar) => $"Operator_Cast_{scalar.ScalarType.Name}";

    public string IsNaN() => "IsNaN";
    public string IsZero() => "IsZero";
    public string IsPositive() => "IsPositive";
    public string IsNegative() => "IsNegative";
    public string IsFinite() => "IsFinite";
    public string IsInfinity() => "IsInfinity";
    public string IsPositiveInfinity() => "IsPositiveInfinity";
    public string IsNegativeInfinity() => "IsNegativeInfinity";

    public string Absolute() => "Absolute";
    public string Sign() => "Sign";

    public string Reciprocal() => "Reciprocal";
    public string Square() => "Square";
    public string Cube() => "Cube";
    public string SquareRoot() => "SquareRoot";
    public string CubeRoot() => "CubeRoot";

    public string ToStringDocumentation() => "ToString";

    public string UnaryPlusMethod() => "Method_UnaryPlus";
    public string NegateMethod() => "Method_Negate";

    public string MultiplyScalarMethod() => "Method_Multiply_Scalar";
    public string DivideScalarMethod() => "Method_Divide_Scalar";

    public string MultiplySameTypeMethod() => "Method_Multiply_SameType";
    public string DivideSameTypeMethod() => "Method_Divide_SameType";

    public string MultiplyGenericScalarToUnhandledMethod() => "Method_Multiply_GenericScalar_Unhandled";
    public string DivideGenericScalarToUnhandledMethod() => "Method_Divide_GenericScalar_Unhandled";

    public string MultiplyGenericScalarMethod() => "Method_Multiply_GenericScalar";
    public string DivideGenericScalarMethod() => "Method_Divide_GenericScalar";

    public string MultiplyVectorMethod(int dimension) => $"Method_Multiply_Vector_{dimension}";

    public string UnaryPlusOperator() => "Operator_UnaryPlus";
    public string NegateOperator() => "Operator_negate";

    public string MultiplyScalarOperatorLHS() => "Operator_Multiply_Scalar_LHS";
    public string MultiplyScalarOperatorRHS() => "Operator_Multiply_Scalar_RHS";
    public string DivideScalarOperatorLHS() => "Operator_Divide_Scalar_LHS";
    public string DivideScalarOperatorRHS() => "Operator_Divide_Scalar_RHS";

    public string MultiplySameTypeOperator() => "Operator_Multiply_SameType";
    public string DivideSameTypeOperator() => "Operator_Divide_SameType";

    public string MultiplyIScalarOperator() => "Operator_Multiply_IScalar";
    public string DivideIScalarOperator() => "Operator_Divide_IScalar";

    public string MultiplyVectorOperatorLHS(int dimension) => $"Operator_MultiplyVector_LHS_{dimension}";
    public string MultiplyVectorOperatorRHS(int dimension) => $"Operator_MultiplyVector_RHS_{dimension}";
    public string DivideVectorOperatorRHS(int dimension) => $"Operator_DivideVector_RHS_{dimension}";

    public string CompareToSameType() => "CompareTo_SameType";

    public string LessThanSameType() => "Operator_LessThan_SameType";
    public string GreaterThanSameType() => "Operator_GreaterThan_SameType";
    public string LessThanOrEqualSameType() => "Operator_LessThanOrEqual_SameType";
    public string GreaterThanOrEqualSameType() => "Operator_GreaterThanOrEqual_SameType";

    public virtual bool Equals(DocumentationTags? other)
    {
        return other is not null;
    }

    public override bool Equals(object obj)
    {
        if (obj is DocumentationTags other)
        {
            return Equals(other);
        }

        return false;
    }

    public static bool operator ==(DocumentationTags? lhs, DocumentationTags? rhs) => lhs?.Equals(rhs) ?? rhs is null;
    public static bool operator !=(DocumentationTags? lhs, DocumentationTags? rhs) => (lhs == rhs) is false;

    public override int GetHashCode() => 0;
}
