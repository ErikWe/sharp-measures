namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Vector3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using System;

using Xunit;

public class MathOperationsTests
{
    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void Plus_Method_ShouldBeUnchanged(Vector3 vector)
    {
        Vector3 result = vector.Plus();

        Assert.Equal(vector, result);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void Plus_Operator_ShouldBeUnchanged(Vector3 vector)
    {
        Vector3 result = +vector;

        Assert.Equal(vector, result);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void Negate_Method_ShouldBeScaledByMinusOne(Vector3 vector)
    {
        Vector3 result = vector.Negate();

        Utility.AssertExtra.AssertEqualComponents((IVector3Quantity)(vector * -1), (IVector3Quantity)result);
    }

    [Theory]
    [ClassData(typeof(Vector3Dataset))]
    public void Negated_Operator_ShouldBeScaledByMinusOne(Vector3 vector)
    {
        Vector3 result = -vector;

        Utility.AssertExtra.AssertEqualComponents((IVector3Quantity)(vector * -1), (IVector3Quantity)result);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, Vector3Dataset>))]
    public void AddVector_Method_MagnitudeShouldEqualAddition(Vector3 lhs, Vector3 rhs)
    {
        Vector3 result = lhs.Add(rhs);

        Assert.Equal(lhs.MagnitudeX + rhs.MagnitudeX, result.MagnitudeX, 2);
        Assert.Equal(lhs.MagnitudeY + rhs.MagnitudeY, result.MagnitudeY, 2);
        Assert.Equal(lhs.MagnitudeZ + rhs.MagnitudeZ, result.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, Vector3Dataset>))]
    public void AddVector_Operator_MagnitudeShouldEqualAddition(Vector3 lhs, Vector3 rhs)
    {
        Vector3 result = lhs + rhs;

        Assert.Equal(lhs.MagnitudeX + rhs.MagnitudeX, result.MagnitudeX, 2);
        Assert.Equal(lhs.MagnitudeY + rhs.MagnitudeY, result.MagnitudeY, 2);
        Assert.Equal(lhs.MagnitudeZ + rhs.MagnitudeZ, result.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, Vector3Dataset>))]
    public void SubtractVector_Method_MagnitudeShouldEqualSubtraction(Vector3 lhs, Vector3 rhs)
    {
        Vector3 result = lhs.Subtract(rhs);

        Assert.Equal(lhs.MagnitudeX - rhs.MagnitudeX, result.MagnitudeX, 2);
        Assert.Equal(lhs.MagnitudeY - rhs.MagnitudeY, result.MagnitudeY, 2);
        Assert.Equal(lhs.MagnitudeZ - rhs.MagnitudeZ, result.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, Vector3Dataset>))]
    public void SubtractVector_Operator_MagnitudeShouldEqualSubtraction(Vector3 lhs, Vector3 rhs)
    {
        Vector3 result = lhs - rhs;

        Assert.Equal(lhs.MagnitudeX - rhs.MagnitudeX, result.MagnitudeX, 2);
        Assert.Equal(lhs.MagnitudeY - rhs.MagnitudeY, result.MagnitudeY, 2);
        Assert.Equal(lhs.MagnitudeZ - rhs.MagnitudeZ, result.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, UnhandledDataset>))]
    public void MultiplyUnhandled_Method_MagnitudeShouldEqualMultiplication(Vector3 vector, Unhandled factor)
    {
        Unhandled3 result = vector.Multiply(factor);

        Assert.Equal(vector.MagnitudeX * factor.Magnitude, result.MagnitudeX, 2);
        Assert.Equal(vector.MagnitudeY * factor.Magnitude, result.MagnitudeY, 2);
        Assert.Equal(vector.MagnitudeZ * factor.Magnitude, result.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, UnhandledDataset>))]
    public void MultiplyUnhandled_Operator_MagnitudeShouldEqualMultiplication(Vector3 quantity, Unhandled factor)
    {
        Unhandled3 resultLHS = quantity * factor;
        Unhandled3 resultRHS = factor * quantity;

        Assert.Equal(quantity.MagnitudeX * factor.Magnitude, resultLHS.MagnitudeX, 2);
        Assert.Equal(quantity.MagnitudeY * factor.Magnitude, resultLHS.MagnitudeY, 2);
        Assert.Equal(quantity.MagnitudeZ * factor.Magnitude, resultLHS.MagnitudeZ, 2);

        Assert.Equal(factor.Magnitude * quantity.MagnitudeX, resultRHS.MagnitudeX, 2);
        Assert.Equal(factor.Magnitude * quantity.MagnitudeY, resultRHS.MagnitudeY, 2);
        Assert.Equal(factor.Magnitude * quantity.MagnitudeZ, resultRHS.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, UnhandledDataset>))]
    public void DivideUnhandled_Method_MagnitudeShouldEqualDivision(Vector3 vector, Unhandled divisor)
    {
        Unhandled3 result = vector.Divide(divisor);

        Assert.Equal(vector.MagnitudeX / divisor.Magnitude, result.MagnitudeX, 2);
        Assert.Equal(vector.MagnitudeY / divisor.Magnitude, result.MagnitudeY, 2);
        Assert.Equal(vector.MagnitudeZ / divisor.Magnitude, result.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, UnhandledDataset>))]
    public void DivideUnhandled_Operator_MagnitudeShouldEqualDivision(Vector3 vector, Unhandled divisor)
    {
        Unhandled3 result = vector / divisor;

        Assert.Equal(vector.MagnitudeX / divisor.Magnitude, result.MagnitudeX, 2);
        Assert.Equal(vector.MagnitudeY / divisor.Magnitude, result.MagnitudeY, 2);
        Assert.Equal(vector.MagnitudeZ / divisor.Magnitude, result.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, DoubleDataset>))]
    public void RemainderDouble_Method_MagnitudeShouldEqualRemainder(Vector3 vector, double divisor)
    {
        Vector3 result = vector.Remainder(divisor);

        Assert.Equal(vector.MagnitudeX % divisor, result.MagnitudeX, 2);
        Assert.Equal(vector.MagnitudeY % divisor, result.MagnitudeY, 2);
        Assert.Equal(vector.MagnitudeZ % divisor, result.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, DoubleDataset>))]
    public void RemainderDouble_Operator_MagnitudeShouldEqualRemainder(Vector3 vector, double divisor)
    {
        Vector3 result = vector % divisor;

        Assert.Equal(vector.MagnitudeX % divisor, result.MagnitudeX, 2);
        Assert.Equal(vector.MagnitudeY % divisor, result.MagnitudeY, 2);
        Assert.Equal(vector.MagnitudeZ % divisor, result.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, DoubleDataset>))]
    public void MultiplyDouble_Method_MagnitudeShouldEqualMultiplication(Vector3 vector, double factor)
    {
        Vector3 result = vector.Multiply(factor);

        Assert.Equal(vector.MagnitudeX * factor, result.MagnitudeX, 2);
        Assert.Equal(vector.MagnitudeY * factor, result.MagnitudeY, 2);
        Assert.Equal(vector.MagnitudeZ * factor, result.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, DoubleDataset>))]
    public void MultiplyDouble_Operator_MagnitudeShouldEqualMultiplication(Vector3 vector, double factor)
    {
        Vector3 resultLHS = vector * factor;
        Vector3 resultRHS = factor * vector;

        Assert.Equal(vector.MagnitudeX * factor, resultLHS.MagnitudeX, 2);
        Assert.Equal(vector.MagnitudeY * factor, resultLHS.MagnitudeY, 2);
        Assert.Equal(vector.MagnitudeZ * factor, resultLHS.MagnitudeZ, 2);

        Assert.Equal(factor * vector.MagnitudeX, resultRHS.MagnitudeX, 2);
        Assert.Equal(factor * vector.MagnitudeY, resultRHS.MagnitudeY, 2);
        Assert.Equal(factor * vector.MagnitudeZ, resultRHS.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, DoubleDataset>))]
    public void DivideDouble_Method_MagnitudeShouldEqualDivision(Vector3 vector, double divisor)
    {
        Vector3 result = vector.Divide(divisor);

        Assert.Equal(vector.MagnitudeX / divisor, result.MagnitudeX, 2);
        Assert.Equal(vector.MagnitudeY / divisor, result.MagnitudeY, 2);
        Assert.Equal(vector.MagnitudeZ / divisor, result.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, DoubleDataset>))]
    public void DivideDouble_Operator_MagnitudeShouldEqualDivision(Vector3 vector, double divisor)
    {
        Vector3 result = vector / divisor;

        Assert.Equal(vector.MagnitudeX / divisor, result.MagnitudeX, 2);
        Assert.Equal(vector.MagnitudeY / divisor, result.MagnitudeY, 2);
        Assert.Equal(vector.MagnitudeZ / divisor, result.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, ScalarDataset>))]
    public void RemainderScalar_Method_MagnitudeShouldEqualRemainder(Vector3 vector, Scalar divisor)
    {
        Vector3 result = vector.Remainder(divisor);

        Assert.Equal(vector.MagnitudeX % divisor, result.MagnitudeX, 2);
        Assert.Equal(vector.MagnitudeY % divisor, result.MagnitudeY, 2);
        Assert.Equal(vector.MagnitudeZ % divisor, result.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, ScalarDataset>))]
    public void RemainderScalar_Operator_MagnitudeShouldEqualRemainder(Vector3 vector, Scalar divisor)
    {
        Vector3 result = vector % divisor;

        Assert.Equal(vector.MagnitudeX % divisor, result.MagnitudeX, 2);
        Assert.Equal(vector.MagnitudeY % divisor, result.MagnitudeY, 2);
        Assert.Equal(vector.MagnitudeZ % divisor, result.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, ScalarDataset>))]
    public void MultiplyScalar_Method_MagnitudeShouldEqualMultiplication(Vector3 vector, Scalar factor)
    {
        Vector3 result = vector.Multiply(factor);

        Assert.Equal(vector.MagnitudeX * factor, result.MagnitudeX, 2);
        Assert.Equal(vector.MagnitudeY * factor, result.MagnitudeY, 2);
        Assert.Equal(vector.MagnitudeZ * factor, result.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, ScalarDataset>))]
    public void MultiplyScalar_Operator_MagnitudeShouldEqualMultiplication(Vector3 vector, Scalar factor)
    {
        Vector3 resultLHS = vector * factor;
        Vector3 resultRHS = factor * vector;

        Assert.Equal(vector.MagnitudeX * factor, resultLHS.MagnitudeX, 2);
        Assert.Equal(vector.MagnitudeY * factor, resultLHS.MagnitudeY, 2);
        Assert.Equal(vector.MagnitudeZ * factor, resultLHS.MagnitudeZ, 2);

        Assert.Equal(factor * vector.MagnitudeX, resultRHS.MagnitudeX, 2);
        Assert.Equal(factor * vector.MagnitudeY, resultRHS.MagnitudeY, 2);
        Assert.Equal(factor * vector.MagnitudeZ, resultRHS.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, ScalarDataset>))]
    public void DivideScalar_Method_MagnitudeShouldEqualDivision(Vector3 vector, Scalar divisor)
    {
        Vector3 result = vector.Divide(divisor);

        Assert.Equal(vector.MagnitudeX / divisor, result.MagnitudeX, 2);
        Assert.Equal(vector.MagnitudeY / divisor, result.MagnitudeY, 2);
        Assert.Equal(vector.MagnitudeZ / divisor, result.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, ScalarDataset>))]
    public void DivideScalar_Operator_MagnitudeShouldEqualDivision(Vector3 vector, Scalar divisor)
    {
        Vector3 result = vector / divisor;

        Assert.Equal(vector.MagnitudeX / divisor, result.MagnitudeX, 2);
        Assert.Equal(vector.MagnitudeY / divisor, result.MagnitudeY, 2);
        Assert.Equal(vector.MagnitudeZ / divisor, result.MagnitudeZ, 2);
    }

    [Fact]
    public void MultiplyGeneric_NullFactory_ShouldThrowArgumentNull()
    {
        Func<(double, double, double), Unhandled3>? factory = null;

        Assert.Throws<ArgumentNullException>(() => Vector3.Zero.Multiply(Scalar.Zero, factory!));
    }

    [Fact]
    public void MultiplyGeneric_NullFactor_ShouldThrowArgumentNull()
    {
        IScalarQuantity? factor = null;

        Assert.Throws<ArgumentNullException>(() => Vector3.Zero.Multiply(factor!, (x) => new Unhandled3(x)));
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, ScalarDataset>))]
    public void MultiplyGeneric_MagnitudeShouldEqualMultiplication(Vector3 vector, IScalarQuantity factor)
    {
        Unhandled3 result = vector.Multiply(factor, (x) => new Unhandled3(x));

        Assert.Equal(vector.MagnitudeX * factor.Magnitude, result.MagnitudeX, 2);
        Assert.Equal(vector.MagnitudeY * factor.Magnitude, result.MagnitudeY, 2);
        Assert.Equal(vector.MagnitudeZ * factor.Magnitude, result.MagnitudeZ, 2);
    }

    [Fact]
    public void DivideGeneric_NullFactory_ShouldThrowArgumentNull()
    {
        Func<(double, double, double), Unhandled3>? factory = null;

        Assert.Throws<ArgumentNullException>(() => Vector3.Zero.Divide(Scalar.Zero, factory!));
    }

    [Fact]
    public void DivideGeneric_NullDivisor_ShouldThrowArgumentNull()
    {
        IScalarQuantity? divisor = null;

        Assert.Throws<ArgumentNullException>(() => Vector3.Zero.Divide(divisor!, (x) => new Unhandled3(x)));
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, ScalarDataset>))]
    public void DivideGeneric_MagnitudeShouldEqualDivision(Vector3 vector, IScalarQuantity divisor)
    {
        Unhandled3 result = vector.Divide(divisor, (x) => new Unhandled3(x));

        Assert.Equal(vector.MagnitudeX / divisor.Magnitude, result.MagnitudeX, 2);
        Assert.Equal(vector.MagnitudeY / divisor.Magnitude, result.MagnitudeY, 2);
        Assert.Equal(vector.MagnitudeZ / divisor.Magnitude, result.MagnitudeZ, 2);
    }

    [Fact]
    public void MultiplyIScalar_Null_ShouldThrowArgumentNull()
    {
        IScalarQuantity? factor = null;

        Assert.Throws<ArgumentNullException>(() => Vector3.Zero * factor!);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, ScalarDataset>))]
    public void MultiplyIScalar_MagnitudeShouldEqualMultiplication(Vector3 vector, IScalarQuantity factor)
    {
        Unhandled3 resultLHS = vector * factor;
        Unhandled3 resultRHS = factor * vector;

        Assert.Equal(vector.MagnitudeX * factor.Magnitude, resultLHS.MagnitudeX, 2);
        Assert.Equal(vector.MagnitudeY * factor.Magnitude, resultLHS.MagnitudeY, 2);
        Assert.Equal(vector.MagnitudeZ * factor.Magnitude, resultLHS.MagnitudeZ, 2);

        Assert.Equal(factor.Magnitude * vector.MagnitudeX, resultRHS.MagnitudeX, 2);
        Assert.Equal(factor.Magnitude * vector.MagnitudeY, resultRHS.MagnitudeY, 2);
        Assert.Equal(factor.Magnitude * vector.MagnitudeZ, resultRHS.MagnitudeZ, 2);
    }

    [Fact]
    public void DivideIScalar_Null_ShouldThrowArgumentNull()
    {
        IScalarQuantity? factor = null;

        Assert.Throws<ArgumentNullException>(() => Vector3.Zero / factor!);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Vector3Dataset, ScalarDataset>))]
    public void DivideIScalar_MagnitudeShouldEqualDivision(Vector3 vector, IScalarQuantity divisor)
    {
        Unhandled3 result = vector / divisor;

        Assert.Equal(vector.MagnitudeX / divisor.Magnitude, result.MagnitudeX, 2);
        Assert.Equal(vector.MagnitudeY / divisor.Magnitude, result.MagnitudeY, 2);
        Assert.Equal(vector.MagnitudeZ / divisor.Magnitude, result.MagnitudeZ, 2);
    }
}
