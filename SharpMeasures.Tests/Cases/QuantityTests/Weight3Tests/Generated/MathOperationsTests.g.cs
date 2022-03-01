#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.Weight3Tests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using System;

using Xunit;

public class MathOperationsTests
{
    [Theory]
    [ClassData(typeof(Weight3Dataset))]
    public void Plus_Method_ShouldBeUnchanged(Weight3 quantity)
    {
        Weight3 result = quantity.Plus();

        Assert.Equal(quantity, result);
    }

    [Theory]
    [ClassData(typeof(Weight3Dataset))]
    public void Plus_Operator_ShouldBeUnchanged(Weight3 quantity)
    {
        Weight3 result = +quantity;

        Assert.Equal(quantity, result);
    }

    [Theory]
    [ClassData(typeof(Weight3Dataset))]
    public void Negate_Method_ShouldBeScaledByMinusOne(Weight3 quantity)
    {
        Weight3 result = quantity.Negate();

        Utility.AssertExtra.AssertEqualComponents(quantity * -1, result);
    }

    [Theory]
    [ClassData(typeof(Weight3Dataset))]
    public void Negated_Operator_ShouldBeScaledByMinusOne(Weight3 quantity)
    {
        Weight3 result = -quantity;

        Utility.AssertExtra.AssertEqualComponents(quantity * -1, result);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Weight3Dataset, UnhandledDataset>))]
    public void MultiplyUnhandled_Method_MagnitudeShouldEqualMultiplication(Weight3 quantity, Unhandled factor)
    {
        Unhandled3 result = quantity.Multiply(factor);

        Assert.Equal(quantity.X * factor.Magnitude, result.X, 2);
        Assert.Equal(quantity.Y * factor.Magnitude, result.Y, 2);
        Assert.Equal(quantity.Z * factor.Magnitude, result.Z, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Weight3Dataset, UnhandledDataset>))]
    public void MultiplyUnhandled_Operator_MagnitudeShouldEqualMultiplication(Weight3 quantity, Unhandled factor)
    {
        Unhandled3 resultLHS = quantity * factor;
        Unhandled3 resultRHS = factor * quantity;

        Assert.Equal(quantity.X * factor.Magnitude, resultLHS.X, 2);
        Assert.Equal(quantity.Y * factor.Magnitude, resultLHS.Y, 2);
        Assert.Equal(quantity.Z * factor.Magnitude, resultLHS.Z, 2);

        Assert.Equal(factor.Magnitude * quantity.X, resultRHS.X, 2);
        Assert.Equal(factor.Magnitude * quantity.Y, resultRHS.Y, 2);
        Assert.Equal(factor.Magnitude * quantity.Z, resultRHS.Z, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Weight3Dataset, UnhandledDataset>))]
    public void DivideUnhandled_Method_MagnitudeShouldEqualDivision(Weight3 quantity, Unhandled divisor)
    {
        Unhandled3 result = quantity.Divide(divisor);

        Assert.Equal(quantity.X / divisor.Magnitude, result.X, 2);
        Assert.Equal(quantity.Y / divisor.Magnitude, result.Y, 2);
        Assert.Equal(quantity.Z / divisor.Magnitude, result.Z, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Weight3Dataset, UnhandledDataset>))]
    public void DivideUnhandled_Operator_MagnitudeShouldEqualDivision(Weight3 quantity, Unhandled divisor)
    {
        Unhandled3 result = quantity / divisor;

        Assert.Equal(quantity.X / divisor.Magnitude, result.X, 2);
        Assert.Equal(quantity.Y / divisor.Magnitude, result.Y, 2);
        Assert.Equal(quantity.Z / divisor.Magnitude, result.Z, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Weight3Dataset, DoubleDataset>))]
    public void RemainderDouble_Method_MagnitudeShouldEqualRemainder(Weight3 quantity, double divisor)
    {
        Weight3 result = quantity.Remainder(divisor);

        Assert.Equal(quantity.X % divisor, result.X, 2);
        Assert.Equal(quantity.Y % divisor, result.Y, 2);
        Assert.Equal(quantity.Z % divisor, result.Z, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Weight3Dataset, DoubleDataset>))]
    public void RemainderDouble_Operator_MagnitudeShouldEqualRemainder(Weight3 quantity, double divisor)
    {
        Weight3 result = quantity % divisor;

        Assert.Equal(quantity.X % divisor, result.X, 2);
        Assert.Equal(quantity.Y % divisor, result.Y, 2);
        Assert.Equal(quantity.Z % divisor, result.Z, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Weight3Dataset, DoubleDataset>))]
    public void MultiplyDouble_Method_MagnitudeShouldEqualMultiplication(Weight3 quantity, double factor)
    {
        Weight3 result = quantity.Multiply(factor);

        Assert.Equal(quantity.X * factor, result.X, 2);
        Assert.Equal(quantity.Y * factor, result.Y, 2);
        Assert.Equal(quantity.Z * factor, result.Z, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Weight3Dataset, DoubleDataset>))]
    public void MultiplyDouble_Operator_MagnitudeShouldEqualMultiplication(Weight3 quantity, double factor)
    {
        Weight3 resultLHS = quantity * factor;
        Weight3 resultRHS = factor * quantity;

        Assert.Equal(quantity.X * factor, resultLHS.X, 2);
        Assert.Equal(quantity.Y * factor, resultLHS.Y, 2);
        Assert.Equal(quantity.Z * factor, resultLHS.Z, 2);

        Assert.Equal(factor * quantity.X, resultRHS.X, 2);
        Assert.Equal(factor * quantity.Y, resultRHS.Y, 2);
        Assert.Equal(factor * quantity.Z, resultRHS.Z, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Weight3Dataset, DoubleDataset>))]
    public void DivideDouble_Method_MagnitudeShouldEqualDivision(Weight3 quantity, double divisor)
    {
        Weight3 result = quantity.Divide(divisor);

        Assert.Equal(quantity.X / divisor, result.X, 2);
        Assert.Equal(quantity.Y / divisor, result.Y, 2);
        Assert.Equal(quantity.Z / divisor, result.Z, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Weight3Dataset, DoubleDataset>))]
    public void DivideDouble_Operator_MagnitudeShouldEqualDivision(Weight3 quantity, double divisor)
    {
        Weight3 result = quantity / divisor;

        Assert.Equal(quantity.X / divisor, result.X, 2);
        Assert.Equal(quantity.Y / divisor, result.Y, 2);
        Assert.Equal(quantity.Z / divisor, result.Z, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Weight3Dataset, ScalarDataset>))]
    public void RemainderScalar_Method_MagnitudeShouldEqualRemainder(Weight3 quantity, Scalar divisor)
    {
        Weight3 result = quantity.Remainder(divisor);

        Assert.Equal(quantity.X % divisor, result.X, 2);
        Assert.Equal(quantity.Y % divisor, result.Y, 2);
        Assert.Equal(quantity.Z % divisor, result.Z, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Weight3Dataset, ScalarDataset>))]
    public void RemainderScalar_Operator_MagnitudeShouldEqualRemainder(Weight3 quantity, Scalar divisor)
    {
        Weight3 result = quantity % divisor;

        Assert.Equal(quantity.X % divisor, result.X, 2);
        Assert.Equal(quantity.Y % divisor, result.Y, 2);
        Assert.Equal(quantity.Z % divisor, result.Z, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Weight3Dataset, ScalarDataset>))]
    public void MultiplyScalar_Method_MagnitudeShouldEqualMultiplication(Weight3 quantity, Scalar factor)
    {
        Weight3 result = quantity.Multiply(factor);

        Assert.Equal(quantity.X * factor, result.X, 2);
        Assert.Equal(quantity.Y * factor, result.Y, 2);
        Assert.Equal(quantity.Z * factor, result.Z, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Weight3Dataset, ScalarDataset>))]
    public void MultiplyScalar_Operator_MagnitudeShouldEqualMultiplication(Weight3 quantity, Scalar factor)
    {
        Weight3 resultLHS = quantity * factor;
        Weight3 resultRHS = factor * quantity;

        Assert.Equal(quantity.X * factor, resultLHS.X, 2);
        Assert.Equal(quantity.Y * factor, resultLHS.Y, 2);
        Assert.Equal(quantity.Z * factor, resultLHS.Z, 2);

        Assert.Equal(factor * quantity.X, resultRHS.X, 2);
        Assert.Equal(factor * quantity.Y, resultRHS.Y, 2);
        Assert.Equal(factor * quantity.Z, resultRHS.Z, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Weight3Dataset, ScalarDataset>))]
    public void DivideScalar_Method_MagnitudeShouldEqualDivision(Weight3 quantity, Scalar divisor)
    {
        Weight3 result = quantity.Divide(divisor);

        Assert.Equal(quantity.X / divisor, result.X, 2);
        Assert.Equal(quantity.Y / divisor, result.Y, 2);
        Assert.Equal(quantity.Z / divisor, result.Z, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Weight3Dataset, ScalarDataset>))]
    public void DivideScalar_Operator_MagnitudeShouldEqualDivision(Weight3 quantity, Scalar divisor)
    {
        Weight3 result = quantity / divisor;

        Assert.Equal(quantity.X / divisor, result.X, 2);
        Assert.Equal(quantity.Y / divisor, result.Y, 2);
        Assert.Equal(quantity.Z / divisor, result.Z, 2);
    }

    [Fact]
    public void MultiplyGeneric_NullFactory_ShouldThrowArgumentNull()
    {
        Func<(double, double, double), Unhandled3>? factory = null;

        Assert.Throws<ArgumentNullException>(() => Weight3.Zero.Multiply(Scalar.Zero, factory!));
    }

    [Fact]
    public void MultiplyGeneric_NullFactor_ShouldThrowArgumentNull()
    {
        IScalarQuantity? factor = null;

        Assert.Throws<ArgumentNullException>(() => Weight3.Zero.Multiply(factor!, (x) => new Unhandled3(x)));
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Weight3Dataset, ScalarDataset>))]
    public void MultiplyGeneric_MagnitudeShouldEqualMultiplication(Weight3 quantity, IScalarQuantity factor)
    {
        Unhandled3 result = quantity.Multiply(factor, (x) => new Unhandled3(x));

        Assert.Equal(quantity.X * factor.Magnitude, result.X, 2);
        Assert.Equal(quantity.Y * factor.Magnitude, result.Y, 2);
        Assert.Equal(quantity.Z * factor.Magnitude, result.Z, 2);
    }

    [Fact]
    public void DivideGeneric_NullFactory_ShouldThrowArgumentNull()
    {
        Func<(double, double, double), Unhandled3>? factory = null;

        Assert.Throws<ArgumentNullException>(() => Weight3.Zero.Divide(Scalar.Zero, factory!));
    }

    [Fact]
    public void DivideGeneric_NullDivisor_ShouldThrowArgumentNull()
    {
        IScalarQuantity? divisor = null;

        Assert.Throws<ArgumentNullException>(() => Weight3.Zero.Divide(divisor!, (x) => new Unhandled3(x)));
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Weight3Dataset, ScalarDataset>))]
    public void DivideGeneric_MagnitudeShouldEqualDivision(Weight3 quantity, IScalarQuantity divisor)
    {
        Unhandled3 result = quantity.Divide(divisor, (x) => new Unhandled3(x));

        Assert.Equal(quantity.X / divisor.Magnitude, result.X, 2);
        Assert.Equal(quantity.Y / divisor.Magnitude, result.Y, 2);
        Assert.Equal(quantity.Z / divisor.Magnitude, result.Z, 2);
    }

    [Fact]
    public void MultiplyIScalar_Null_ShouldThrowArgumentNull()
    {
        IScalarQuantity? factor = null;

        Assert.Throws<ArgumentNullException>(() => Weight3.Zero * factor!);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Weight3Dataset, ScalarDataset>))]
    public void MultiplyIScalar_MagnitudeShouldEqualMultiplication(Weight3 quantity, IScalarQuantity factor)
    {
        Unhandled3 resultLHS = quantity * factor;
        Unhandled3 resultRHS = factor * quantity;

        Assert.Equal(quantity.X * factor.Magnitude, resultLHS.X, 2);
        Assert.Equal(quantity.Y * factor.Magnitude, resultLHS.Y, 2);
        Assert.Equal(quantity.Z * factor.Magnitude, resultLHS.Z, 2);

        Assert.Equal(factor.Magnitude * quantity.X, resultRHS.X, 2);
        Assert.Equal(factor.Magnitude * quantity.Y, resultRHS.Y, 2);
        Assert.Equal(factor.Magnitude * quantity.Z, resultRHS.Z, 2);
    }

    [Fact]
    public void DivideIScalar_Null_ShouldThrowArgumentNull()
    {
        IScalarQuantity? factor = null;

        Assert.Throws<ArgumentNullException>(() => Weight3.Zero / factor!);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<Weight3Dataset, ScalarDataset>))]
    public void DivideIScalar_MagnitudeShouldEqualDivision(Weight3 quantity, IScalarQuantity divisor)
    {
        Unhandled3 result = quantity / divisor;

        Assert.Equal(quantity.X / divisor.Magnitude, result.X, 2);
        Assert.Equal(quantity.Y / divisor.Magnitude, result.Y, 2);
        Assert.Equal(quantity.Z / divisor.Magnitude, result.Z, 2);
    }
}
