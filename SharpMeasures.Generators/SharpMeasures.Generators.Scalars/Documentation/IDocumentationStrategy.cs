namespace SharpMeasures.Generators.Scalars.Documentation;

using SharpMeasures.Generators.Quantities;
using SharpMeasures.Generators.Units;

internal interface IDocumentationStrategy
{
    public abstract string Header();

    public abstract string Zero();
    public abstract string Constant(IScalarConstant constant);
    public abstract string UnitBase(IUnitInstance unitInstance);

    public abstract string WithMagnitude();

    public abstract string ScalarConstructor();
    public abstract string ScalarAndUnitConstructor();

    public abstract string Magnitude();
    public abstract string InUnit();
    public abstract string InConstantMultiples(IScalarConstant constant);
    public abstract string InSpecifiedUnit(IUnitInstance unitInstance);

    public abstract string Conversion(NamedType scalar);
    public abstract string AntidirectionalConversion(NamedType scalar);
    public abstract string CastConversion(NamedType scalar);
    public abstract string AntidirectionalCastConversion(NamedType scalar);

    public abstract string OperationMethod(IQuantityOperation operation, NamedType other);
    public abstract string MirroredOperationMethod(IQuantityOperation operation, NamedType other);
    public abstract string OperationOperator(IQuantityOperation operation, NamedType other);
    public abstract string MirroredOperationOperator(IQuantityOperation operation, NamedType other);

    public abstract string Process(IQuantityProcess process);

    public abstract string IsNaN();
    public abstract string IsZero();
    public abstract string IsPositive();
    public abstract string IsNegative();
    public abstract string IsFinite();
    public abstract string IsInfinite();
    public abstract string IsPositiveInfinity();
    public abstract string IsNegativeInfinity();

    public abstract string Absolute();
    public abstract string Sign();

    public abstract string ToStringMethod();
    public abstract string ToStringFormat();
    public abstract string ToStringProvider();
    public abstract string ToStringFormatAndProvider();

    public abstract string EqualsSameTypeMethod();
    public abstract string EqualsObjectMethod();

    public abstract string EqualitySameTypeOperator();
    public abstract string InequalitySameTypeOperator();

    public abstract string GetHashCodeDocumentation();

    public abstract string UnaryPlusMethod();
    public abstract string NegateMethod();

    public abstract string AddSameTypeMethod();
    public abstract string SubtractSameTypeMethod();

    public abstract string AddDifferenceMethod();
    public abstract string SubtractDifferenceMethod();

    public abstract string MultiplyScalarMethod();
    public abstract string DivideScalarMethod();

    public abstract string DivideSameTypeMethod();

    public abstract string MultiplyVectorMethod(int dimension);

    public abstract string AddTScalarMethod();
    public abstract string SubtractTScalarMethod();
    public abstract string SubtractFromTScalarMethod();
    public abstract string MultiplyTScalarMethod();
    public abstract string DivideTScalarMethod();
    public abstract string DivideIntoTScalarMethod();

    public abstract string UnaryPlusOperator();
    public abstract string NegateOperator();

    public abstract string AddSameTypeOperator();
    public abstract string SubtractSameTypeOperator();

    public abstract string AddDifferenceOperatorLHS();
    public abstract string AddDifferenceOperatorRHS();
    public abstract string SubtractDifferenceOperatorLHS();

    public abstract string MultiplyScalarOperatorLHS();
    public abstract string MultiplyScalarOperatorRHS();
    public abstract string DivideScalarOperatorLHS();

    public abstract string DivideSameTypeOperator();

    public abstract string MultiplyVectorOperatorLHS(int dimension);
    public abstract string MultiplyVectorOperatorRHS(int dimension);

    public abstract string AddIScalarOperator();
    public abstract string SubtractIScalarOperator();
    public abstract string MultiplyIScalarOperator();
    public abstract string DivideIScalarOperator();

    public abstract string AddUnhandledOperatorLHS();
    public abstract string AddUnhandledOperatorRHS();
    public abstract string SubtractUnhandledOperatorLHS();
    public abstract string SubtractUnhandledOperatorRHS();
    public abstract string MultiplyUnhandledOperatorLHS();
    public abstract string MultiplyUnhandledOperatorRHS();
    public abstract string DivideUnhandledOperatorLHS();
    public abstract string DivideUnhandledOperatorRHS();

    public abstract string CompareToSameType();

    public abstract string LessThanSameType();
    public abstract string GreaterThanSameType();
    public abstract string LessThanOrEqualSameType();
    public abstract string GreaterThanOrEqualSameType();
}
