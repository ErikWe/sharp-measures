namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.ImpulseTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class ToVector3Tests
{
    [Theory]
    [ClassData(typeof(GenericDataset<ImpulseDataset, Vector3Dataset>))]
    public void Vector3_Method_ShouldMatchMultiplication(Impulse quantity, Vector3 vector)
    {
        Impulse3 result = quantity.Multiply(vector);

        Assert.Equal(quantity.Magnitude * vector.MagnitudeX, result.MagnitudeX, 2);
        Assert.Equal(quantity.Magnitude * vector.MagnitudeY, result.MagnitudeY, 2);
        Assert.Equal(quantity.Magnitude * vector.MagnitudeZ, result.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ImpulseDataset, DoubleDataset, DoubleDataset, DoubleDataset>))]
    public void DoubleTuple_Method_ShouldMatchMultiplication(Impulse quantity, double x, double y, double z)
    {
        Impulse3 result = quantity.Multiply((x, y, z));

        Assert.Equal(quantity.Magnitude * x, result.MagnitudeX, 2);
        Assert.Equal(quantity.Magnitude * y, result.MagnitudeY, 2);
        Assert.Equal(quantity.Magnitude * z, result.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ImpulseDataset, ScalarDataset, ScalarDataset, ScalarDataset>))]
    public void ScalarTuple_Method_ShouldMatchMultiplication(Impulse quantity, Scalar x, Scalar y, Scalar z)
    {
        Impulse3 result = quantity.Multiply((x, y, z));

        Assert.Equal(quantity.Magnitude * x, result.MagnitudeX, 2);
        Assert.Equal(quantity.Magnitude * y, result.MagnitudeY, 2);
        Assert.Equal(quantity.Magnitude * z, result.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ImpulseDataset, Vector3Dataset>))]
    public void Vector3_Operator_ShouldMatchMultiplication(Impulse quantity, Vector3 vector)
    {
        Impulse3 resultLHS = quantity * vector;
        Impulse3 resultRHS = vector * quantity;

        Assert.Equal(quantity.Magnitude * vector.MagnitudeX, resultLHS.MagnitudeX, 2);
        Assert.Equal(vector.MagnitudeX * quantity.Magnitude, resultRHS.MagnitudeX, 2);

        Assert.Equal(quantity.Magnitude * vector.MagnitudeY, resultLHS.MagnitudeY, 2);
        Assert.Equal(vector.MagnitudeY * quantity.Magnitude, resultRHS.MagnitudeY, 2);

        Assert.Equal(quantity.Magnitude * vector.MagnitudeZ, resultLHS.MagnitudeZ, 2);
        Assert.Equal(vector.MagnitudeZ * quantity.Magnitude, resultRHS.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ImpulseDataset, DoubleDataset, DoubleDataset, DoubleDataset>))]
    public void DoubleTuple_Operator_ShouldMatchMultiplication(Impulse quantity, double x, double y, double z)
    {
        Impulse3 resultLHS = quantity * (x, y, z);
        Impulse3 resultRHS = (x, y, z) * quantity;

        Assert.Equal(quantity.Magnitude * x, resultLHS.MagnitudeX, 2);
        Assert.Equal(x * quantity.Magnitude, resultRHS.MagnitudeX, 2);

        Assert.Equal(quantity.Magnitude * y, resultLHS.MagnitudeY, 2);
        Assert.Equal(y * quantity.Magnitude, resultRHS.MagnitudeY, 2);

        Assert.Equal(quantity.Magnitude * z, resultLHS.MagnitudeZ, 2);
        Assert.Equal(z * quantity.Magnitude, resultRHS.MagnitudeZ, 2);
    }

    [Theory]
    [ClassData(typeof(GenericDataset<ImpulseDataset, ScalarDataset, ScalarDataset, ScalarDataset>))]
    public void ScalarTuple_Operator_ShouldMatchMultiplication(Impulse quantity, Scalar x, Scalar y, Scalar z)
    {
        Impulse3 resultLHS = quantity * (x, y, z);
        Impulse3 resultRHS = (x, y, z) * quantity;

        Assert.Equal(quantity.Magnitude * x, resultLHS.MagnitudeX, 2);
        Assert.Equal(x * quantity.Magnitude, resultRHS.MagnitudeX, 2);

        Assert.Equal(quantity.Magnitude * y, resultLHS.MagnitudeY, 2);
        Assert.Equal(y * quantity.Magnitude, resultRHS.MagnitudeY, 2);

        Assert.Equal(quantity.Magnitude * z, resultLHS.MagnitudeZ, 2);
        Assert.Equal(z * quantity.Magnitude, resultRHS.MagnitudeZ, 2);
    }
}