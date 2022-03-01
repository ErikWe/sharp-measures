#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.TimeSquaredTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using System;

using Xunit;

public class MathOperationsTests
{
    [Theory]
    [ClassData(typeof(TimeSquaredDataset))]
    public void Plus_Method_ShouldBeUnchanged(TimeSquared quantity)
    {
        TimeSquared result = quantity.Plus();

        Assert.Equal(quantity, result);
    }

    [Theory]
    [ClassData(typeof(TimeSquaredDataset))]
    public void Plus_Operator_ShouldBeUnchanged(TimeSquared quantity)
    {
        TimeSquared result = +quantity;

        Assert.Equal(quantity, result);
    }

    [Theory]
    [ClassData(typeof(TimeSquaredDataset))]
    public void Negate_Method_ShouldBeScaledByMinusOne(TimeSquared quantity)
    {
        TimeSquared result = quantity.Negate();

        Assert.Equal(quantity.Magnitude * -1, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(TimeSquaredDataset))]
    public void Negated_Operator_ShouldBeScaledByMinusOne(TimeSquared quantity)
    {
        TimeSquared result = -quantity;

        Assert.Equal(quantity.Magnitude * -1, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<TimeSquaredDataset, UnhandledDataset>))]
    public void MultiplyUnhandled_Method_MagnitudeShouldEqualMultiplication(TimeSquared quantity, Unhandled factor)
    {
        Unhandled result = quantity.Multiply(factor);

        Assert.Equal(quantity.Magnitude * factor.Magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<TimeSquaredDataset, UnhandledDataset>))]
    public void MultiplyUnhandled_Operator_MagnitudeShouldEqualMultiplication(TimeSquared quantity, Unhandled factor)
    {
        Unhandled resultLHS = quantity * factor;
        Unhandled resultRHS = factor * quantity;

        Assert.Equal(quantity.Magnitude * factor.Magnitude, resultLHS.Magnitude, 2);
        Assert.Equal(factor.Magnitude * quantity.Magnitude, resultRHS.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<TimeSquaredDataset, UnhandledDataset>))]
    public void DivideUnhandled_Method_MagnitudeShouldEqualDivision(TimeSquared quantity, Unhandled divisor)
    {
        Unhandled result = quantity.Divide(divisor);

        Assert.Equal(quantity.Magnitude / divisor.Magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<TimeSquaredDataset, UnhandledDataset>))]
    public void DivideUnhandled_Operator_MagnitudeShouldEqualDivision(TimeSquared quantity, Unhandled divisor)
    {
        Unhandled resultLHS = quantity / divisor;
        Unhandled resultRHS = divisor / quantity;

        Assert.Equal(quantity.Magnitude / divisor.Magnitude, resultLHS.Magnitude, 2);
        Assert.Equal(divisor.Magnitude / quantity.Magnitude, resultRHS.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<TimeSquaredDataset, DoubleDataset>))]
    public void RemainderDouble_Method_MagnitudeShouldEqualRemainder(TimeSquared quantity, double divisor)
    {
        TimeSquared result = quantity.Remainder(divisor);

        Assert.Equal(quantity.Magnitude % divisor, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<TimeSquaredDataset, DoubleDataset>))]
    public void RemainderDouble_Operator_MagnitudeShouldEqualRemainder(TimeSquared quantity, double divisor)
    {
        TimeSquared result = quantity % divisor;

        Assert.Equal(quantity.Magnitude % divisor, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<TimeSquaredDataset, DoubleDataset>))]
    public void MultiplyDouble_Method_MagnitudeShouldEqualMultiplication(TimeSquared quantity, double factor)
    {
        TimeSquared result = quantity.Multiply(factor);

        Assert.Equal(quantity.Magnitude * factor, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<TimeSquaredDataset, DoubleDataset>))]
    public void MultiplyDouble_Operator_MagnitudeShouldEqualMultiplication(TimeSquared quantity, double factor)
    {
        TimeSquared resultLHS = quantity * factor;
        TimeSquared resultRHS = factor * quantity;

        Assert.Equal(quantity.Magnitude * factor, resultLHS.Magnitude, 2);
        Assert.Equal(factor * quantity.Magnitude, resultRHS.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<TimeSquaredDataset, DoubleDataset>))]
    public void DivideDouble_Method_MagnitudeShouldEqualDivision(TimeSquared quantity, double divisor)
    {
        TimeSquared result = quantity.Divide(divisor);

        Assert.Equal(quantity.Magnitude / divisor, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<TimeSquaredDataset, DoubleDataset>))]
    public void DivideDouble_Operator_MagnitudeShouldEqualDivision(TimeSquared quantity, double divisor)
    {
        TimeSquared resultLHS = quantity / divisor;
        FrequencyDrift resultRHS = divisor / quantity;

        Assert.Equal(quantity.Magnitude / divisor, resultLHS.Magnitude, 2);
        Assert.Equal(divisor / quantity.Magnitude, resultRHS.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<TimeSquaredDataset, ScalarDataset>))]
    public void RemainderScalar_Method_MagnitudeShouldEqualRemainder(TimeSquared quantity, Scalar divisor)
    {
        TimeSquared result = quantity.Remainder(divisor);

        Assert.Equal(quantity.Magnitude % divisor.Magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<TimeSquaredDataset, ScalarDataset>))]
    public void RemainderScalar_Operator_MagnitudeShouldEqualRemainder(TimeSquared quantity, Scalar divisor)
    {
        TimeSquared result = quantity % divisor;

        Assert.Equal(quantity.Magnitude % divisor.Magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<TimeSquaredDataset, ScalarDataset>))]
    public void MultiplyScalar_Method_MagnitudeShouldEqualMultiplication(TimeSquared quantity, Scalar factor)
    {
        TimeSquared result = quantity.Multiply(factor);

        Assert.Equal(quantity.Magnitude * factor.Magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<TimeSquaredDataset, ScalarDataset>))]
    public void MultiplyScalar_Operator_MagnitudeShouldEqualMultiplication(TimeSquared quantity, Scalar factor)
    {
        TimeSquared resultLHS = quantity * factor;
        TimeSquared resultRHS = factor * quantity;

        Assert.Equal(quantity.Magnitude * factor.Magnitude, resultLHS.Magnitude, 2);
        Assert.Equal(factor.Magnitude * quantity.Magnitude, resultRHS.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<TimeSquaredDataset, ScalarDataset>))]
    public void DivideScalar_Method_MagnitudeShouldEqualDivision(TimeSquared quantity, Scalar divisor)
    {
        TimeSquared result = quantity.Divide(divisor);

        Assert.Equal(quantity.Magnitude / divisor.Magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<TimeSquaredDataset, ScalarDataset>))]
    public void DivideScalar_Operator_MagnitudeShouldEqualDivision(TimeSquared quantity, Scalar divisor)
    {
        TimeSquared resultLHS = quantity / divisor;
        FrequencyDrift resultRHS = divisor / quantity;

        Assert.Equal(quantity.Magnitude / divisor.Magnitude, resultLHS.Magnitude, 2);
        Assert.Equal(divisor.Magnitude / quantity.Magnitude, resultRHS.Magnitude, 2);
    }

    [Fact]
    public void MultiplyGeneric_NullFactory_ShouldThrowArgumentNull()
    {
        Func<double, Scalar>? factory = null;

        Assert.Throws<ArgumentNullException>(() => new TimeSquared(0).Multiply(Scalar.Zero, factory!));
    }

    [Fact]
    public void MultiplyGeneric_NullFactor_ShouldThrowArgumentNull()
    {
        IScalarQuantity? factor = null;

        Assert.Throws<ArgumentNullException>(() => new TimeSquared(0).Multiply(factor!, (x) => new Scalar(x)));
    }

    [Theory]
    [ClassData(typeof(GenericDataset<TimeSquaredDataset, ScalarDataset>))]
    public void MultiplyGeneric_MagnitudeShouldEqualMultiplication(TimeSquared quantity, IScalarQuantity factor)
    {
        Scalar result = quantity.Multiply(factor, (x) => new Scalar(x));

        Assert.Equal(quantity.Magnitude * factor.Magnitude, result.Magnitude, 2);
    }

    [Fact]
    public void DivideGeneric_NullFactory_ShouldThrowArgumentNull()
    {
        Func<double, Scalar>? factory = null;

        Assert.Throws<ArgumentNullException>(() => new TimeSquared(0).Divide(Scalar.Zero, factory!));
    }

    [Fact]
    public void DivideGeneric_NullDivisor_ShouldThrowArgumentNull()
    {
        IScalarQuantity? divisor = null;

        Assert.Throws<ArgumentNullException>(() => new TimeSquared(0).Divide(divisor!, (x) => new Scalar(x)));
    }

    [Theory]
    [ClassData(typeof(GenericDataset<TimeSquaredDataset, ScalarDataset>))]
    public void DivideGeneric_MagnitudeShouldEqualDivision(TimeSquared quantity, IScalarQuantity divisor)
    {
        Scalar result = quantity.Divide(divisor, (x) => new Scalar(x));

        Assert.Equal(quantity.Magnitude / divisor.Magnitude, result.Magnitude, 2);
    }

    [Fact]
    public void MultiplyIScalar_Null_ShouldThrowArgumentNull()
    {
        IScalarQuantity? factor = null;

        Assert.Throws<ArgumentNullException>(() => new TimeSquared(0) * factor!);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<TimeSquaredDataset, ScalarDataset>))]
    public void MultiplyIScalar_MagnitudeShouldEqualMultiplication(TimeSquared quantity, IScalarQuantity factor)
    {
        Unhandled result = quantity * factor;

        Assert.Equal(quantity.Magnitude * factor.Magnitude, result.Magnitude, 2);
    }

    [Fact]
    public void DivideIScalar_Null_ShouldThrowArgumentNull()
    {
        IScalarQuantity? factor = null;

        Assert.Throws<ArgumentNullException>(() => new TimeSquared(0) / factor!);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<TimeSquaredDataset, ScalarDataset>))]
    public void DivideIScalar_MagnitudeShouldEqualDivision(TimeSquared quantity, IScalarQuantity divisor)
    {
        Unhandled result = quantity / divisor;

        Assert.Equal(quantity.Magnitude / divisor.Magnitude, result.Magnitude, 2);
    }
}
