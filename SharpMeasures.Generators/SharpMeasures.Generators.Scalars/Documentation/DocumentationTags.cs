namespace SharpMeasures.Generators.Scalars.Documentation;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.SourceBuilding;
using SharpMeasures.Generators.Units;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

internal sealed class DocumentationTags : IDocumentationStrategy, IEquatable<DocumentationTags>
{
    public static DocumentationTags Instance { get; } = new();

    private DocumentationTags() { }

    public string Header() => "Header";

    public string Zero() => "Zero";
    public string Constant(IScalarConstant constant) => $"Constant_{constant.Name}";
    public string UnitBase(IUnitInstance unitInstance) => $"One_{unitInstance.Name}";

    public string WithMagnitude() => "WithMagnitude";

    public string ScalarConstructor() => "Constructor_Scalar";
    public string ScalarAndUnitConstructor() => "Constructor_Scalar_Unit";

    public string Magnitude() => "Magnitude";
    public string InUnit() => "InUnit";
    public string InConstantMultiples(IScalarConstant constant) => $"InMultiplesOf_{constant.Name}";
    public string InSpecifiedUnit(IUnitInstance unitInstance) => $"InUnit_{unitInstance.Name}";

    public string Conversion(NamedType scalar) => $"As_{scalar.Name}";
    public string AntidirectionalConversion(NamedType scalar) => $"From_{scalar.Name}";
    public string CastConversion(NamedType scalar) => $"Operator_CastTo_{scalar.Name}";
    public string AntidirectionalCastConversion(NamedType scalar) => $"Operator_CastFrom_{scalar.Name}";

    public string OperationMethod(IQuantityOperation operation, NamedType other) => $"OperationMethod_{operation.MethodName}_{other.Name}";
    public string MirroredOperationMethod(IQuantityOperation operation, NamedType other) => $"OperationMethod_{operation.MirroredMethodName}_{other.Name}";
    public string OperationOperator(IQuantityOperation operation, NamedType other) => $"Operation_{(operation.Position is OperatorPosition.Left ? "LHS" : "RHS")}_{operation.OperatorType}_{other.Name}";
    public string MirroredOperationOperator(IQuantityOperation operation, NamedType other) => $"Operation_{(operation.Position is OperatorPosition.Right ? "LHS" : "RHS")}_{operation.OperatorType}_{other.Name}";

    public string Process(IQuantityProcess process)
    {
        if (process.ImplementAsProperty || process.ParameterTypes.Count is 0)
        {
            return $"Process_{process.Name}";
        }

        return $"Process_{process.Name}_{ParseProcessSignature(process.ParameterTypes)}";
    }

    public string IsNaN() => "IsNaN";
    public string IsZero() => "IsZero";
    public string IsPositive() => "IsPositive";
    public string IsNegative() => "IsNegative";
    public string IsFinite() => "IsFinite";
    public string IsInfinite() => "IsInfinite";
    public string IsPositiveInfinity() => "IsPositiveInfinity";
    public string IsNegativeInfinity() => "IsNegativeInfinity";

    public string Absolute() => "Absolute";
    public string Sign() => "Sign";

    public string ToStringDocumentation() => "ToString";

    public string EqualsSameTypeMethod() => "Method_Equals_SameType";
    public string EqualsObjectMethod() => "Method_Equals_Object";

    public string EqualitySameTypeOperator() => "Operator_Equality_SameType";
    public string InequalitySameTypeOperator() => "Operator_Inequality_SameType";

    public string GetHashCodeDocumentation() => "GetHashCode";

    public string UnaryPlusMethod() => "Method_UnaryPlus";
    public string NegateMethod() => "Method_Negate";

    public string AddSameTypeMethod() => "Method_Add_SameType";
    public string SubtractSameTypeMethod() => "Method_Subtract_SameType";

    public string AddDifferenceMethod() => "Method_Add_Difference";
    public string SubtractDifferenceMethod() => "Method_Subtract_Difference";

    public string MultiplyScalarMethod() => "Method_Multiply_Scalar";
    public string DivideScalarMethod() => "Method_Divide_Scalar";

    public string DivideSameTypeMethod() => "Method_Divide_SameType";

    public string MultiplyVectorMethod(int dimension) => $"Method_Multiply_Vector_{dimension}";

    public string AddTScalarMethod() => "Method_Add_TScalar";
    public string SubtractTScalarMethod() => "Method_Subtract_TScalar";
    public string SubtractFromTScalarMethod() => "Method_SubtractFrom_TScalar";
    public string MultiplyTScalarMethod() => "Method_Multiply_TScalar";
    public string DivideTScalarMethod() => "Method_Divide_TScalar";
    public string DivideIntoTScalarMethod() => "Method_DivideInto_TScalar";

    public string UnaryPlusOperator() => "Operator_UnaryPlus";
    public string NegateOperator() => "Operator_negate";

    public string AddSameTypeOperator() => "Operator_Add_SameType";
    public string SubtractSameTypeOperator() => "Operator_Subtract_SameType";

    public string AddDifferenceOperatorLHS() => "Operator_Add_Difference_LHS";
    public string AddDifferenceOperatorRHS() => "Operator_Add_Difference_RHS";
    public string SubtractDifferenceOperatorLHS() => "Operator_Subtract_Difference_LHS";

    public string MultiplyScalarOperatorLHS() => "Operator_Multiply_Scalar_LHS";
    public string MultiplyScalarOperatorRHS() => "Operator_Multiply_Scalar_RHS";
    public string DivideScalarOperatorLHS() => "Operator_Divide_Scalar_LHS";

    public string DivideSameTypeOperator() => "Operator_Divide_SameType";

    public string MultiplyVectorOperatorLHS(int dimension) => $"Operator_MultiplyVector_LHS_{dimension}";
    public string MultiplyVectorOperatorRHS(int dimension) => $"Operator_MultiplyVector_RHS_{dimension}";

    public string AddIScalarOperator() => "Operator_Add_IScalarQuantity";
    public string SubtractIScalarOperator() => "Operator_Subtract_IScalarQuantity";
    public string MultiplyIScalarOperator() => "Operator_Multiply_IScalarQuantity";
    public string DivideIScalarOperator() => "Operator_Divide_IScalarQuantity";

    public string AddUnhandledOperatorLHS() => "Operator_Add_Unhandled_LHS";
    public string AddUnhandledOperatorRHS() => "Operator_Add_Unhandled_RHS";
    public string SubtractUnhandledOperatorLHS() => "Operator_Subtract_Unhandled_LHS";
    public string SubtractUnhandledOperatorRHS() => "Operator_Subtract_Unhandled_RHS";
    public string MultiplyUnhandledOperatorLHS() => "Operator_Multiply_Unhandled_LHS";
    public string MultiplyUnhandledOperatorRHS() => "Operator_Multiply_Unhandled_RHS";
    public string DivideUnhandledOperatorLHS() => "Operator_Divide_Unhandled_LHS";
    public string DivideUnhandledOperatorRHS() => "Operator_Divide_Unhandled_RHS";

    public string CompareToSameType() => "CompareTo_SameType";

    public string LessThanSameType() => "Operator_LessThan_SameType";
    public string GreaterThanSameType() => "Operator_GreaterThan_SameType";
    public string LessThanOrEqualSameType() => "Operator_LessThanOrEqual_SameType";
    public string GreaterThanOrEqualSameType() => "Operator_GreaterThanOrEqual_SameType";

    private static string ParseProcessSignature(IReadOnlyList<NamedType> signature)
    {
        StringBuilder tag = new();

        IterativeBuilding.AppendEnumerable(tag, signature.Select(static (signatureElement) => signatureElement.Name), "_");

        return tag.ToString();
    }

    public bool Equals(DocumentationTags? other) => other is not null;
    public override bool Equals(object? obj) => obj is DocumentationTags other && Equals(other);

    public static bool operator ==(DocumentationTags? lhs, DocumentationTags? rhs) => lhs?.Equals(rhs) ?? rhs is null;
    public static bool operator !=(DocumentationTags? lhs, DocumentationTags? rhs) => (lhs == rhs) is false;

    public override int GetHashCode() => 0;
}
