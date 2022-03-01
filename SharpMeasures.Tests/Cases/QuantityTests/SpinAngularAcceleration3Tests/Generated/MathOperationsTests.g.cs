#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.SpinAngularAcceleration3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using System;

using Xunit;

public class MathOperationsTests
{
    [Theory]
    [ClassData(typeof(SpinAngularAcceleration3Dataset))]
    public void Plus_Method_ShouldBeUnchanged(SpinAngularAcceleration3 quantity)
    {
        SpinAngularAcceleration3 result = quantity.Plus();

        Assert.Equal(quantity, result);
    }

    [Theory]
    [ClassData(typeof(SpinAngularAcceleration3Dataset))]
    public void Plus_Operator_ShouldBeUnchanged(SpinAngularAcceleration3 quantity)
    {
        SpinAngularAcceleration3 result = +quantity;

        Assert.Equal(quantity, result);
    }

    [Theory]
    [ClassData(typeof(SpinAngularAcceleration3Dataset))]
    public void Negate_Method_ShouldBeScaledByMinusOne(SpinAngularAcceleration3 quantity)
    {
        SpinAngularAcceleration3 result = quantity.Negate();

        Utility.AssertExtra.AssertEqualComponents(quantity * -1, result);
    }

    [Theory]
    [ClassData(typeof(SpinAngularAcceleration3Dataset))]
    public void Negated_Operator_ShouldBeScaledByMinusOne(SpinAngularAcceleration3 quantity)
    {
        SpinAngularAcceleration3 result = -quantity;

        Utility.AssertExtra.AssertEqualComponents(quantity * -1, result);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<SpinAngularAcceleration3Dataset, UnhandledDataset>))]
    public void MultiplyUnhandled_Method_MagnitudeShouldEqualMultiplication(SpinAngularAcceleration3 quantity, Unhandled factor)
    {
        Unhandled3 result = quantity.Multiply(factor);

        Assert.Equal(quantity.MagnitudeX * factor.Magnitude, result.MagnitudeX, 2);
        Assert.Equal(quantity.MagnitudeY * factor.Magnitude, result.MagnitudeY, 2);
        Assert.Equal(quantity.MagnitudeZ * factor.Magnitude, result.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<SpinAngularAcceleration3Dataset, UnhandledDataset>))]
    public void MultiplyUnhandled_Operator_MagnitudeShouldEqualMultiplication(SpinAngularAcceleration3 quantity, Unhandled factor)
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
    [ClassData(typeof(GenericDataset<SpinAngularAcceleration3Dataset, UnhandledDataset>))]
    public void DivideUnhandled_Method_MagnitudeShouldEqualDivision(SpinAngularAcceleration3 quantity, Unhandled divisor)
    {
        Unhandled3 result = quantity.Divide(divisor);

        Assert.Equal(quantity.MagnitudeX / divisor.Magnitude, result.MagnitudeX, 2);
        Assert.Equal(quantity.MagnitudeY / divisor.Magnitude, result.MagnitudeY, 2);
        Assert.Equal(quantity.MagnitudeZ / divisor.Magnitude, result.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<SpinAngularAcceleration3Dataset, UnhandledDataset>))]
    public void DivideUnhandled_Operator_MagnitudeShouldEqualDivision(SpinAngularAcceleration3 quantity, Unhandled divisor)
    {
        Unhandled3 result = quantity / divisor;

        Assert.Equal(quantity.MagnitudeX / divisor.Magnitude, result.MagnitudeX, 2);
        Assert.Equal(quantity.MagnitudeY / divisor.Magnitude, result.MagnitudeY, 2);
        Assert.Equal(quantity.MagnitudeZ / divisor.Magnitude, result.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<SpinAngularAcceleration3Dataset, DoubleDataset>))]
    public void RemainderDouble_Method_MagnitudeShouldEqualRemainder(SpinAngularAcceleration3 quantity, double divisor)
    {
        SpinAngularAcceleration3 result = quantity.Remainder(divisor);

        Assert.Equal(quantity.MagnitudeX % divisor, result.MagnitudeX, 2);
        Assert.Equal(quantity.MagnitudeY % divisor, result.MagnitudeY, 2);
        Assert.Equal(quantity.MagnitudeZ % divisor, result.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<SpinAngularAcceleration3Dataset, DoubleDataset>))]
    public void RemainderDouble_Operator_MagnitudeShouldEqualRemainder(SpinAngularAcceleration3 quantity, double divisor)
    {
        SpinAngularAcceleration3 result = quantity % divisor;

        Assert.Equal(quantity.MagnitudeX % divisor, result.MagnitudeX, 2);
        Assert.Equal(quantity.MagnitudeY % divisor, result.MagnitudeY, 2);
        Assert.Equal(quantity.MagnitudeZ % divisor, result.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<SpinAngularAcceleration3Dataset, DoubleDataset>))]
    public void MultiplyDouble_Method_MagnitudeShouldEqualMultiplication(SpinAngularAcceleration3 quantity, double factor)
    {
        SpinAngularAcceleration3 result = quantity.Multiply(factor);

        Assert.Equal(quantity.MagnitudeX * factor, result.MagnitudeX, 2);
        Assert.Equal(quantity.MagnitudeY * factor, result.MagnitudeY, 2);
        Assert.Equal(quantity.MagnitudeZ * factor, result.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<SpinAngularAcceleration3Dataset, DoubleDataset>))]
    public void MultiplyDouble_Operator_MagnitudeShouldEqualMultiplication(SpinAngularAcceleration3 quantity, double factor)
    {
        SpinAngularAcceleration3 resultLHS = quantity * factor;
        SpinAngularAcceleration3 resultRHS = factor * quantity;

        Assert.Equal(quantity.MagnitudeX * factor, resultLHS.MagnitudeX, 2);
        Assert.Equal(quantity.MagnitudeY * factor, resultLHS.MagnitudeY, 2);
        Assert.Equal(quantity.MagnitudeZ * factor, resultLHS.MagnitudeZ, 2);

        Assert.Equal(factor * quantity.MagnitudeX, resultRHS.MagnitudeX, 2);
        Assert.Equal(factor * quantity.MagnitudeY, resultRHS.MagnitudeY, 2);
        Assert.Equal(factor * quantity.MagnitudeZ, resultRHS.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<SpinAngularAcceleration3Dataset, DoubleDataset>))]
    public void DivideDouble_Method_MagnitudeShouldEqualDivision(SpinAngularAcceleration3 quantity, double divisor)
    {
        SpinAngularAcceleration3 result = quantity.Divide(divisor);

        Assert.Equal(quantity.MagnitudeX / divisor, result.MagnitudeX, 2);
        Assert.Equal(quantity.MagnitudeY / divisor, result.MagnitudeY, 2);
        Assert.Equal(quantity.MagnitudeZ / divisor, result.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<SpinAngularAcceleration3Dataset, DoubleDataset>))]
    public void DivideDouble_Operator_MagnitudeShouldEqualDivision(SpinAngularAcceleration3 quantity, double divisor)
    {
        SpinAngularAcceleration3 result = quantity / divisor;

        Assert.Equal(quantity.MagnitudeX / divisor, result.MagnitudeX, 2);
        Assert.Equal(quantity.MagnitudeY / divisor, result.MagnitudeY, 2);
        Assert.Equal(quantity.MagnitudeZ / divisor, result.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<SpinAngularAcceleration3Dataset, ScalarDataset>))]
    public void RemainderScalar_Method_MagnitudeShouldEqualRemainder(SpinAngularAcceleration3 quantity, Scalar divisor)
    {
        SpinAngularAcceleration3 result = quantity.Remainder(divisor);

        Assert.Equal(quantity.MagnitudeX % divisor, result.MagnitudeX, 2);
        Assert.Equal(quantity.MagnitudeY % divisor, result.MagnitudeY, 2);
        Assert.Equal(quantity.MagnitudeZ % divisor, result.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<SpinAngularAcceleration3Dataset, ScalarDataset>))]
    public void RemainderScalar_Operator_MagnitudeShouldEqualRemainder(SpinAngularAcceleration3 quantity, Scalar divisor)
    {
        SpinAngularAcceleration3 result = quantity % divisor;

        Assert.Equal(quantity.MagnitudeX % divisor, result.MagnitudeX, 2);
        Assert.Equal(quantity.MagnitudeY % divisor, result.MagnitudeY, 2);
        Assert.Equal(quantity.MagnitudeZ % divisor, result.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<SpinAngularAcceleration3Dataset, ScalarDataset>))]
    public void MultiplyScalar_Method_MagnitudeShouldEqualMultiplication(SpinAngularAcceleration3 quantity, Scalar factor)
    {
        SpinAngularAcceleration3 result = quantity.Multiply(factor);

        Assert.Equal(quantity.MagnitudeX * factor, result.MagnitudeX, 2);
        Assert.Equal(quantity.MagnitudeY * factor, result.MagnitudeY, 2);
        Assert.Equal(quantity.MagnitudeZ * factor, result.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<SpinAngularAcceleration3Dataset, ScalarDataset>))]
    public void MultiplyScalar_Operator_MagnitudeShouldEqualMultiplication(SpinAngularAcceleration3 quantity, Scalar factor)
    {
        SpinAngularAcceleration3 resultLHS = quantity * factor;
        SpinAngularAcceleration3 resultRHS = factor * quantity;

        Assert.Equal(quantity.MagnitudeX * factor, resultLHS.MagnitudeX, 2);
        Assert.Equal(quantity.MagnitudeY * factor, resultLHS.MagnitudeY, 2);
        Assert.Equal(quantity.MagnitudeZ * factor, resultLHS.MagnitudeZ, 2);

        Assert.Equal(factor * quantity.MagnitudeX, resultRHS.MagnitudeX, 2);
        Assert.Equal(factor * quantity.MagnitudeY, resultRHS.MagnitudeY, 2);
        Assert.Equal(factor * quantity.MagnitudeZ, resultRHS.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<SpinAngularAcceleration3Dataset, ScalarDataset>))]
    public void DivideScalar_Method_MagnitudeShouldEqualDivision(SpinAngularAcceleration3 quantity, Scalar divisor)
    {
        SpinAngularAcceleration3 result = quantity.Divide(divisor);

        Assert.Equal(quantity.MagnitudeX / divisor, result.MagnitudeX, 2);
        Assert.Equal(quantity.MagnitudeY / divisor, result.MagnitudeY, 2);
        Assert.Equal(quantity.MagnitudeZ / divisor, result.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<SpinAngularAcceleration3Dataset, ScalarDataset>))]
    public void DivideScalar_Operator_MagnitudeShouldEqualDivision(SpinAngularAcceleration3 quantity, Scalar divisor)
    {
        SpinAngularAcceleration3 result = quantity / divisor;

        Assert.Equal(quantity.MagnitudeX / divisor, result.MagnitudeX, 2);
        Assert.Equal(quantity.MagnitudeY / divisor, result.MagnitudeY, 2);
        Assert.Equal(quantity.MagnitudeZ / divisor, result.MagnitudeZ, 2);
    }

    [Fact]
    public void MultiplyGeneric_NullFactory_ShouldThrowArgumentNull()
    {
        Func<(double, double, double), Unhandled3>? factory = null;

        Assert.Throws<ArgumentNullException>(() => SpinAngularAcceleration3.Zero.Multiply(Scalar.Zero, factory!));
    }

    [Fact]
    public void MultiplyGeneric_NullFactor_ShouldThrowArgumentNull()
    {
        IScalarQuantity? factor = null;

        Assert.Throws<ArgumentNullException>(() => SpinAngularAcceleration3.Zero.Multiply(factor!, (x) => new Unhandled3(x)));
    }

    [Theory]
    [ClassData(typeof(GenericDataset<SpinAngularAcceleration3Dataset, ScalarDataset>))]
    public void MultiplyGeneric_MagnitudeShouldEqualMultiplication(SpinAngularAcceleration3 quantity, IScalarQuantity factor)
    {
        Unhandled3 result = quantity.Multiply(factor, (x) => new Unhandled3(x));

        Assert.Equal(quantity.MagnitudeX * factor.Magnitude, result.MagnitudeX, 2);
        Assert.Equal(quantity.MagnitudeY * factor.Magnitude, result.MagnitudeY, 2);
        Assert.Equal(quantity.MagnitudeZ * factor.Magnitude, result.MagnitudeZ, 2);
    }

    [Fact]
    public void DivideGeneric_NullFactory_ShouldThrowArgumentNull()
    {
        Func<(double, double, double), Unhandled3>? factory = null;

        Assert.Throws<ArgumentNullException>(() => SpinAngularAcceleration3.Zero.Divide(Scalar.Zero, factory!));
    }

    [Fact]
    public void DivideGeneric_NullDivisor_ShouldThrowArgumentNull()
    {
        IScalarQuantity? divisor = null;

        Assert.Throws<ArgumentNullException>(() => SpinAngularAcceleration3.Zero.Divide(divisor!, (x) => new Unhandled3(x)));
    }

    [Theory]
    [ClassData(typeof(GenericDataset<SpinAngularAcceleration3Dataset, ScalarDataset>))]
    public void DivideGeneric_MagnitudeShouldEqualDivision(SpinAngularAcceleration3 quantity, IScalarQuantity divisor)
    {
        Unhandled3 result = quantity.Divide(divisor, (x) => new Unhandled3(x));

        Assert.Equal(quantity.MagnitudeX / divisor.Magnitude, result.MagnitudeX, 2);
        Assert.Equal(quantity.MagnitudeY / divisor.Magnitude, result.MagnitudeY, 2);
        Assert.Equal(quantity.MagnitudeZ / divisor.Magnitude, result.MagnitudeZ, 2);
    }

    [Fact]
    public void MultiplyIScalar_Null_ShouldThrowArgumentNull()
    {
        IScalarQuantity? factor = null;

        Assert.Throws<ArgumentNullException>(() => SpinAngularAcceleration3.Zero * factor!);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<SpinAngularAcceleration3Dataset, ScalarDataset>))]
    public void MultiplyIScalar_MagnitudeShouldEqualMultiplication(SpinAngularAcceleration3 quantity, IScalarQuantity factor)
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

    [Fact]
    public void DivideIScalar_Null_ShouldThrowArgumentNull()
    {
        IScalarQuantity? factor = null;

        Assert.Throws<ArgumentNullException>(() => SpinAngularAcceleration3.Zero / factor!);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<SpinAngularAcceleration3Dataset, ScalarDataset>))]
    public void DivideIScalar_MagnitudeShouldEqualDivision(SpinAngularAcceleration3 quantity, IScalarQuantity divisor)
    {
        Unhandled3 result = quantity / divisor;

        Assert.Equal(quantity.MagnitudeX / divisor.Magnitude, result.MagnitudeX, 2);
        Assert.Equal(quantity.MagnitudeY / divisor.Magnitude, result.MagnitudeY, 2);
        Assert.Equal(quantity.MagnitudeZ / divisor.Magnitude, result.MagnitudeZ, 2);
    }
}
