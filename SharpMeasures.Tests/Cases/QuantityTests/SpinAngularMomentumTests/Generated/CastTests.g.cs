#nullable enable

namespace ErikWe.SharpMeasures.Tests.Cases.QuantityTests.SpinAngularMomentumTests;

using ErikWe.SharpMeasures.Quantities;
using ErikWe.SharpMeasures.Tests.Datasets;

using Xunit;

public class CastTests
{
    [Theory]
    [ClassData(typeof(SpinAngularMomentumDataset))]
    public void ToDouble_ShouldMatchMagnitude(SpinAngularMomentum quantity)
    {
        double result = quantity.ToDouble();

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(SpinAngularMomentumDataset))]
    public void CastToDouble_ShouldMatchMagnitude(SpinAngularMomentum quantity)
    {
        double result = (double)quantity;

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(SpinAngularMomentumDataset))]
    public void ToScalar_ShouldMatchMagnitude(SpinAngularMomentum quantity)
    {
        Scalar result = quantity.ToScalar();

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(SpinAngularMomentumDataset))]
    public void CastToScalar_ShouldMatchMagnitude(SpinAngularMomentum quantity)
    {
        Scalar result = (Scalar)quantity;

        Assert.Equal(quantity.Magnitude, result, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void FromDouble_ShouldMatchMagnitude(double magnitude)
    {
        SpinAngularMomentum result = SpinAngularMomentum.FromDouble(magnitude);

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(DoubleDataset))]
    public void CastFromDouble_ShouldMatchMagnitude(double magnitude)
    {
        SpinAngularMomentum result = (SpinAngularMomentum)magnitude;

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void FromScalar_ShouldMatchMagnitude(Scalar magnitude)
    {
        SpinAngularMomentum result = SpinAngularMomentum.FromScalar(magnitude);

        Assert.Equal(magnitude, result.Magnitude, 2);
    }

    [Theory]
    [ClassData(typeof(ScalarDataset))]
    public void CastFromScalar_ShouldMatchMagnitude(Scalar magnitude)
    {
        SpinAngularMomentum result = (SpinAngularMomentum)magnitude;

        Assert.Equal(magnitude, result.Magnitude, 2);
    }
}
