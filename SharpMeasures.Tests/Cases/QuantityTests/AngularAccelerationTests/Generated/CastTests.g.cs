#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.AngularAccelerationTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class CastTests
{
    [Theory]
    [ClassData(typeof(AngularAccelerationDataset))]
    public void ToDouble_ShouldMatchMagnitude(AngularAcceleration quantity)
    {
        double result = quantity.ToDouble();

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(AngularAccelerationDataset))]
    public void CastToDouble_ShouldMatchMagnitude(AngularAcceleration quantity)
    {
        double result = (double)quantity;

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(AngularAccelerationDataset))]
    public void ToScalar_ShouldMatchMagnitude(AngularAcceleration quantity)
    {
        Scalar result = quantity.ToScalar();

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(AngularAccelerationDataset))]
    public void CastToScalar_ShouldMatchMagnitude(AngularAcceleration quantity)
    {
        Scalar result = (Scalar)quantity;

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_ShouldMatchMagnitude(double magnitude)
    {
        AngularAcceleration result = AngularAcceleration.FromDouble(magnitude);

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void CastFromDouble_ShouldMatchMagnitude(double magnitude)
    {
        AngularAcceleration result = (AngularAcceleration)magnitude;

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void FromScalar_ShouldMatchMagnitude(Scalar magnitude)
    {
        AngularAcceleration result = AngularAcceleration.FromScalar(magnitude);

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void CastFromScalar_ShouldMatchMagnitude(Scalar magnitude)
    {
        AngularAcceleration result = (AngularAcceleration)magnitude;

        Assert.Equal(magnitude, result.Magnitude, 2);
    }
}
